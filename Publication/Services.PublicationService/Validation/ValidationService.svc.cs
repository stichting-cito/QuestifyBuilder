
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.HelperFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using Enums;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Services.PublicationService.VersionCheck;

namespace Questify.Builder.Services.PublicationService.Validation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ValidationService : IValidationService
    {
        private static readonly Dictionary<string, ValidationTaskProgress> ValidationProgress = new Dictionary<string, ValidationTaskProgress>();
        public static IEnumerable<IValidateHandler> Validators { get; set; }

        public bool AtLeastOneHandlerAvailable(int bankId, IList<string> testNames)
        {
            return InitializeSupportedValidationHandlers(bankId, testNames).Any();
        }

        public IList<ValidationHandlerIdentifier> Validate(int bankId, IList<string> testNames)
        {
            var handlers = InitializeSupportedValidationHandlers(bankId, testNames);
            foreach (var handler in handlers)
            {
                ValidationProgress.Add(handler.TaskId, new ValidationTaskProgress(handler.TaskId, 0, 0));
            }

            var ctx = System.Web.HttpContext.Current;

            new Thread(() =>
            {
                System.Web.HttpContext.Current = ctx;
                DoValidation(handlers);
            }).Start();

            return handlers;
        }

        public ValidationTaskProgress GetProgress(string taskId)
        {
            return ValidationProgress.ContainsKey(taskId) ? ValidationProgress[taskId] : null;
        }

        public void FinishValidation(string taskId)
        {
            if (ValidationProgress.ContainsKey(taskId))
            {
                ValidationProgress.Remove(taskId);
            }
        }

        public Version GetCurrentVersion()
        {
            var info = new VersionInfo();
            return info.GetCurrentVersion();
        }

        protected virtual IList<ValidationHandlerIdentifier> InitializeSupportedValidationHandlers(int bankId, IList<string> testNames)
        {
            var tests = testNames.Select(tm => ResourceFactory.Instance.GetResourceByNameWithOption(bankId, tm, new ResourceRequestDTO() { WithDependencies = true })).OfType<AssessmentTestResourceEntity>();
            var result = new List<ValidationHandlerIdentifier>();

            foreach (var validator in Validators)
            {
                validator.Collection = tests.Select(t => t.ConvertResourceEntityToDto<AssessmentTestResourceEntity, AssessmentTestResourceDto>(false))
                                            .OfType<ResourceDto>().ToList();

                validator.BankId = bankId;

                if (validator.IsDatasourceSupported())
                {
                    var newHandler = new ValidationHandlerIdentifier(validator);
                    result.Add(newHandler);
                }
            }

            return result;
        }

        private static void DoValidation(IEnumerable<ValidationHandlerIdentifier> handlers)
        {
            foreach (var handler in handlers)
            {
                var validationDecorator = handler.Handler as IValidateHandler;
                var taskProgress = ValidationProgress[handler.TaskId];
                validationDecorator.StartProgress += (s, e) =>
                {
                    taskProgress.Total += e.NumberOfResources;
                    taskProgress.Progress = 0;
                };

                validationDecorator.Progress += (s, e) =>
                {
                    taskProgress.Progress = e.ProgessValue ?? taskProgress.Progress + 1;
                    taskProgress.ProgressString = e.StatusMessage;
                };

                try
                {
                    taskProgress.ValidationResult = validationDecorator.Validate();
                    taskProgress.Finished = true;
                    taskProgress.IsReportAvailable = validationDecorator.IsReportAvailable;
                    taskProgress.Report = validationDecorator.IsReportAvailable
                        ? validationDecorator.GenerateReport()
                        : string.Empty;
                    taskProgress.ResultText = validationDecorator.ResultText;
                }
                catch (Exception e)
                {
                    taskProgress.ValidationResult = ValidationResult.NotValid;
                    taskProgress.Finished = true;
                    var sb =
                        new StringBuilder(taskProgress.Errors).AppendLine()
                            .AppendLine(e.Message)
                            .AppendLine(e.InnerException?.Message ?? string.Empty)
                            .AppendLine();
                    taskProgress.Errors = sb.ToString();
                    taskProgress.ResultText = "Error: " + sb;
                }
            }
        }
    }
}
