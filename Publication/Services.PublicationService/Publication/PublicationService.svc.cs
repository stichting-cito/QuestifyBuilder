
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Resources;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using Cito.Tester.Common;
using Microsoft.Ajax.Utilities;
using Questify.Builder.Configuration;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ResourceManager;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Logging;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Security;
using Questify.Builder.Services.PublicationService.Properties;

namespace Questify.Builder.Services.PublicationService.Publication
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PublicationService : IPublicationService
    {
        private class PublicationParameters
        {
            public Dictionary<string, string> ConfigurationOptions;
            public int BankId;
            public IList<string> TestNames;
            public IList<string> TestPackageNames;
        }

        private static readonly Dictionary<string, PublicationTaskProgress> PublicationProgress = new Dictionary<string, PublicationTaskProgress>();

        public static IEnumerable<IPublicationHandler> PublicationHandlers { get; set; }

        public IList<PublicationHandlerIdentifier> GetAvailablePublicationHandlers(
    int bankId,
    IList<string> testNames,
    IList<string> testPackageNames)
        {
            var result = new List<PublicationHandlerIdentifier>();
            bool selectionHasDifferentAssessmentTestViewTypes;
            var availableHandlerData = GetAvailablePublicationHandlerElements(bankId, testNames, testPackageNames, false, out selectionHasDifferentAssessmentTestViewTypes);
            var availableHandlers = availableHandlerData.Item2;
            var availableConfigHandlers = availableHandlerData.Item1;
            var index = 0;
            foreach (var availableHandler in availableHandlers)
            {
                var availableConfigHandler = availableConfigHandlers[index];

                var handlerToAdd = new PublicationHandlerIdentifier(availableHandler.UserFriendlyName,
                                        availableConfigHandler.Type, availableHandler.FileExtension);
                result.Add(handlerToAdd);
                index++;
            }

            var unavailableHandlers = availableHandlerData.Item3;
            var unavailableBecause = availableHandlerData.Item4;
            index = 0;

            foreach (var unavailableHandler in unavailableHandlers)
            {
                var handlerToAdd = new PublicationHandlerIdentifier(unavailableHandler.UserFriendlyName, unavailableBecause[index]);
                result.Add(handlerToAdd);
                index++;
            }

            if (selectionHasDifferentAssessmentTestViewTypes)
            {
                result.Add(new PublicationHandlerIdentifier("", Resources.PublicationService_SelectedTestsOrTestPackagesDoesNotShareTestType));
            }

            return result;
        }

        public IList<TestPreviewHandlerIdentifier> GetAllTestPreviewHandlers()
        {
            var config = GetPublicationHandlersConfiguration();
            var handlers = config.Handlers.Cast<PublicationHandlerElement>().OrderBy(h => h.Order);
            return GetTestPreviewHandlers(handlers, null);
        }

        public IList<TestPreviewHandlerIdentifier> GetAvailableTestPreviewHandlers(int bankId, IList<string> testNames,
    IList<string> testPackages)
        {
            bool ignoreBoolParam;
            var publicationHanlderInfo = GetAvailablePublicationHandlerElements(bankId, testNames, testPackages, true, out ignoreBoolParam);
            return GetTestPreviewHandlers(publicationHanlderInfo.Item1, publicationHanlderInfo.Item2);
        }

        public Dictionary<string, string> GetConfigurationOptions(
    string publicationHandlerType,
    int bankId,
    IList<string> testNames,
    IList<string> testPackageNames)
        {
            var config = GetPublicationHandlersConfiguration();
            var publicationHandlerElement =
                config.Handlers.Cast<PublicationHandlerElement>()
                    .FirstOrDefault(phe => phe.Type == publicationHandlerType);

            var publicationHandler = TryGetPublicationHandler(publicationHandlerElement);

            if (publicationHandler == null)
            {
                return new Dictionary<string, string>();
            }

            return publicationHandler.GetConfigurationOptions(bankId, testNames, testPackageNames);
        }

        public IList<PublicationHandlerIdentifier> GetAllPublicationHandlers(int bankId, IList<string> testNames,
    IList<string> testPackageNames)
        {
            var config = GetPublicationHandlersConfiguration();
            var result = new List<PublicationHandlerIdentifier>();
            foreach (var handler in config.Handlers.Cast<PublicationHandlerElement>().OrderBy(h => h.Order))
            {
                var handlerInstance = TryGetPublicationHandler(handler);
                if (handlerInstance != null)
                {
                    var handlerToAdd = new PublicationHandlerIdentifier(handler.ToString(), handler.Type, handlerInstance.FileExtension);
                    result.Add(handlerToAdd);
                }
            }
            return result;
        }

        public string Publicize(
    string publicationHandlerType,
    Dictionary<string, string> configurationOptions,
    int bankId,
    IList<string> testNames,
    IList<string> testPackageNames,
    bool isForPreview,
    string customName)
        {
            var taskProgress = new PublicationTaskProgress(Guid.NewGuid().ToString(), 0, 0);
            PublicationProgress[taskProgress.TaskId] = taskProgress;
            var requestUri = OperationContext.Current.RequestContext.RequestMessage.Headers.To.AbsoluteUri;
            var baseUrl = requestUri.Substring(0, requestUri.LastIndexOf('/') + 1);
            baseUrl = baseUrl.Replace("/Publication/", "/");

            var index = OperationContext.Current.IncomingMessageHeaders.FindHeader("QbCredentials",
"http://www.Questify.eu");
            if (index >= 0)
            {
                var ident =
                    OperationContext.Current.IncomingMessageHeaders.GetHeader<TestBuilderIdentity>("QbCredentials",
                        "http://www.Questify.eu");
                var tbPrincipal = new TestBuilderPrincipal(ident);

                configurationOptions.Add("RequestedByUser", tbPrincipal.Identity.Name);
            }

            var ctx = System.Web.HttpContext.Current;

            new Thread(() =>
                    {
                        System.Web.HttpContext.Current = ctx;

                        var parameters = new PublicationParameters
                        {
                            BankId = bankId,
                            ConfigurationOptions = configurationOptions,
                            TestNames = testNames ?? new List<string>(),
                            TestPackageNames = testPackageNames ?? new List<string>()
                        };

                        DoPublicize(publicationHandlerType, parameters, baseUrl, taskProgress, isForPreview, customName);
                    }).Start();

            return taskProgress.TaskId;
        }

        public PublicationTaskProgress GetProgress(string taskId)
        {
            return PublicationProgress.ContainsKey(taskId) ? PublicationProgress[taskId] : null;
        }

        public void FinishPublication(string taskId)
        {
            if (PublicationProgress.ContainsKey(taskId))
            {

                var progress = PublicationProgress[taskId];
                var directoriesToCleanUp = new HashSet<string>();
                foreach (var file in progress.Files)
                {
                    var fileInfo = new FileInfo(file);
                    File.Delete(file);
                    if (new DirectoryInfo(HostingEnvironment.MapPath("~/Publicaties")).FullName !=
                        fileInfo.DirectoryName.TrimEnd('\\'))
                    {
                        directoriesToCleanUp.Add(fileInfo.DirectoryName);
                    }
                }

                foreach (var directory in directoriesToCleanUp)
                {
                    Directory.Delete(directory);
                }

                PublicationProgress.Remove(taskId);
            }
        }

        public string GetItemOutput(string publicationHandlerType, int bankId, string itemCode)
        {
            var returnValue = string.Empty;
            var config = GetPublicationHandlersConfiguration();
            var publicationHandlerElement = config.Handlers.Cast<PublicationHandlerElement>().FirstOrDefault(phe => phe.Type == publicationHandlerType);
            if (publicationHandlerElement == null)
            {
                return returnValue;
            }

            var publicationHandler = TryGetPublicationHandler(publicationHandlerElement);
            if (publicationHandler == null)
            {
                return returnValue;
            }

            var itemResource = (ItemResourceEntity)ResourceFactory.Instance.GetResourceByNameWithOption(bankId, itemCode, new ResourceRequestDTO());
            var assessmentItem = itemResource.GetAssessmentItem();
            if (assessmentItem != null)
            {
                using (var resourceManager = new DataBaseResourceManager(bankId))
                {
                    returnValue = publicationHandler.PublishItem(assessmentItem, resourceManager);
                }
            }

            return returnValue;
        }

        public List<ConceptProcessingLabelEntry> GetConceptRelatedResponseProcessingForReportingPurposes(string publicationHandlerType, Guid itemResourceId)
        {
            var conceptProcessingEntries = new List<ConceptProcessingLabelEntry>();
            var config = GetPublicationHandlersConfiguration();
            var publicationHandlerElement = config.Handlers.Cast<PublicationHandlerElement>().FirstOrDefault(phe => phe.Type == publicationHandlerType);

            if (publicationHandlerElement == null)
            {
                return conceptProcessingEntries;
            }

            var publicationHandler = TryGetPublicationHandler(publicationHandlerElement);
            if (!(publicationHandler is IQTI21PublicationHandlerExtensions qti21PublicationHandlerExtensions))
            {
                return conceptProcessingEntries;
            }

            var itemResource = (ItemResourceEntity)ResourceFactory.Instance.GetResourceByIdWithOption(itemResourceId, new ItemResourceEntityFactory(), new ResourceRequestDTO());
            var assessmentItem = itemResource.GetAssessmentItem();
            if (assessmentItem == null)
            {
                return conceptProcessingEntries;
            }

            using (var resourceManager = new DataBaseResourceManager(itemResource.BankId))
            {
                var qti21ProcessingFragment = qti21PublicationHandlerExtensions.GetConceptRelatedResponseProcessingForReportingPurposes(assessmentItem, resourceManager);

                if (qti21ProcessingFragment != null)
                {
                    foreach (var node in qti21ProcessingFragment.Descendants("setOutcomeValue").Where(x => x.Attributes().Any(y => y.Name == "idfirstfact")))
                    {
                        conceptProcessingEntries.Add(new ConceptProcessingLabelEntry(node.Attribute("conceptcode").Value, node.Attribute("identifier").Value, node.Attribute("idfirstfact").Value));
                    }
                }
            }

            return conceptProcessingEntries;
        }

        protected virtual PublicationConfiguration GetPublicationHandlersConfiguration()
        {
            return ConfigurationManager.GetSection("publicationHandlers") as PublicationConfiguration;
        }

        private void DoPublicize(
            string publicationHandlerType,
            PublicationParameters parameters,
            string baseUrl,
            PublicationTaskProgress taskProgress,
            bool isForPreview,
            string customName)
        {
            var packagePublicationTaskProgress = new PublicationTaskProgress(Guid.NewGuid().ToString(), 0, 0);

            try
            {
                var config = GetPublicationHandlersConfiguration();
                var publicationHandlerElement =
                    config.Handlers.Cast<PublicationHandlerElement>()
                        .FirstOrDefault(phe => phe.Type == publicationHandlerType);

                var publicationHandler = TryGetPublicationHandler(publicationHandlerElement);
                if (publicationHandler == null)
                {
                    return;
                }

                publicationHandler.StartProgress += (s, e) => taskProgress.Total = e.NumberOfResources;
                publicationHandler.Progress += (s, e) =>
                {
                    taskProgress.Progress = e.ProgessValue ?? 0;
                    taskProgress.ProgressString = e.StatusMessage;
                };

                var exportPath = GetExportPath(parameters.TestNames, parameters.TestPackageNames, publicationHandler);

                var properties = new Dictionary<string, string>() {
                    { "BankId", parameters.BankId.ToString() },
                    { "TestNames", string.Join("; ", parameters.TestNames) },
                    { "TestPackageNames", string.Join("; ", parameters.TestPackageNames)},
                    { "ConfigurationOptions", $"{{{string.Join(", ", parameters.ConfigurationOptions.Select(pp => $"{pp.Key} : {pp.Value}"))}}}" }
                };

                LogHelper.TrackEvent(EventsToTrack.PublicationRequest, properties);

                taskProgress.Succeeded = parameters.TestPackageNames.Count > 0 ?
                   publicationHandler.Publish(parameters.ConfigurationOptions, parameters.BankId, new List<string>(), parameters.TestPackageNames, exportPath, isForPreview, customName) :
                   publicationHandler.Publish(parameters.ConfigurationOptions, parameters.BankId, parameters.TestNames, new List<string>(), exportPath, isForPreview, customName);

                if (taskProgress.Succeeded)
                {
                    AddPublicationUrls(taskProgress, publicationHandler);
                    AddPublicationLocations(baseUrl, taskProgress, publicationHandler);
                }
                taskProgress.Errors = publicationHandler.Errors;
                taskProgress.Warnings = publicationHandler.Warnings;
            }
            catch (Exception e)
            {
                var sb =
                    new StringBuilder(taskProgress.Errors).AppendLine()
                        .AppendLine(e.Message)
                        .AppendLine(e.InnerException?.Message ?? string.Empty)
                        .AppendLine();
                taskProgress.Errors = sb.ToString();
            }
            finally
            {
                taskProgress.Finished = true;
                packagePublicationTaskProgress.Finished = true;
            }
        }

        private static void AddPublicationLocations(string baseUrl, PublicationTaskProgress taskProgress,
            IPublicationHandler publicationHandler)
        {
            foreach (var exportedFile in publicationHandler.ExportedFiles)
            {
                string repoLocation = ConfigurationManager.AppSettings["QprLocation"];
                if (!string.IsNullOrEmpty(repoLocation) && exportedFile.Value.Contains(repoLocation))
                {
                    taskProgress.PublicationLocations.Add(exportedFile.Value);
                    continue;
                }

                taskProgress.Files.Add(
                    Directory.Exists(exportedFile.Value)
                        ? exportedFile.Value + "/" + exportedFile.Key
                        : exportedFile.Value);
                taskProgress.PublicationLocations.Add(baseUrl + "Publicaties/" + exportedFile.Key);
            }
        }

        private static void AddPublicationUrls(PublicationTaskProgress taskProgress,
             IPublicationHandler publicationHandler)
        {
            foreach (var url in publicationHandler.Urls)
            {
                taskProgress.PublicationUrls.Add(url.Value);
            }
        }

        private static string GetExportPath(IList<string> testNames, IList<string> testPackageNames,
            IPublicationHandler publicationHandler)
        {
            var exportPath = HostingEnvironment.MapPath("~/Publicaties/");
            if (testPackageNames.Any())
            {
                var singlePackage = testPackageNames.Count == 1;
                exportPath = singlePackage
                    ? exportPath +
                      string.Format("{0}-{1}{2}", testPackageNames.First(), Guid.NewGuid(),
                          publicationHandler.FileExtension)
                    : exportPath;
            }
            else if (testNames.Any())
            {
                var singleTest = testNames.Count == 1;
                exportPath = singleTest
                    ? exportPath +
                      string.Format("{0}-{1}{2}", testNames.First(), Guid.NewGuid(),
                          publicationHandler.FileExtension)
                    : exportPath;
            }
            else
            {
                exportPath = exportPath +
                             string.Format("{0}-{1}{2}", "PublicationPackage", Guid.NewGuid(),
                                 publicationHandler.FileExtension);
            }
            return exportPath;
        }

        private static IList<string> SupportedTypesForTests(int bankId, IEnumerable<string> testNames,
            IEnumerable<string> testPackageNames)
        {
            var result = new List<string>();

            foreach (TestPackageResourceEntity testPackageResource in ResourceFactory.Instance.GetResourcesByNamesWithOption(bankId, testPackageNames.ToList(), new ResourceRequestDTO()).OfType<TestPackageResourceEntity>())
            {
                var serializedTestPackage = ResourceFactory.Instance.GetResourceData(testPackageResource).BinData;
                var testPackage = TestPackageFactory.ReturnTestPackageModelFromByteArray(serializedTestPackage);
                result.AddRange(testPackage.IncludedViews);
            }

            foreach (AssessmentTestResourceEntity testResource in ResourceFactory.Instance.GetResourcesByNamesWithOption(bankId, testNames.ToList(), new ResourceRequestDTO()).OfType<AssessmentTestResourceEntity>())
            {
                var serializedTest = ResourceFactory.Instance.GetResourceData(testResource).BinData;
                var deserializedTest = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(serializedTest, true);
                result.AddRange(deserializedTest.AssessmentTestv2.IncludedViews);
            }

            var availablePublicationHandlers = PublicationHandlers.Where(p => !p.CheckItemsSupportedViews()).SelectMany(p => p.SupportedViews).ToList();

            using (var resourceManager = new DataBaseResourceManager(bankId))
            {
                PublicationHandlers.Where(p => p.CheckItemsSupportedViews()).ForEach(p =>
                                {
                                    if (ResourceFactory.Instance.GetResourcesByNamesWithOption(bankId, testNames.ToList(), new ResourceRequestDTO()).OfType<AssessmentTestResourceEntity>().All(t => t.CheckItemsSupportedViews(result.Distinct().ToList(), resourceManager)))
                                    {
                                        availablePublicationHandlers.AddRange(p.SupportedViews);
                                    }
                                });
            }

            return result.Distinct().Where(s => availablePublicationHandlers.Contains(s)).ToList();
        }

        private Tuple<List<PublicationHandlerElement>, List<IPublicationHandler>, List<IPublicationHandler>, List<string>> GetAvailablePublicationHandlerElements
    (int bankId, IList<string> testNameList, IList<string> testPackageNameList, bool isPreview, out bool selectionHasDifferentAssessmentTestViewTypes)
        {
            var availablePublicationHandlerElements = new List<PublicationHandlerElement>();
            var availablePublicationHandlers = new List<IPublicationHandler>();

            var unavailablePublicationHandlers = new List<IPublicationHandler>();
            var unavailableBecause = new List<string>();
            var result = Tuple.Create(availablePublicationHandlerElements, availablePublicationHandlers,
                unavailablePublicationHandlers, unavailableBecause);

            var config = GetPublicationHandlersConfiguration();
            var testNames = testNameList ?? new List<string>();
            var testPackageNames = testPackageNameList ?? new List<string>();

            var supportedTypes = SupportedTypesForTests(bankId, testNames, testPackageNames);

            selectionHasDifferentAssessmentTestViewTypes = !supportedTypes.Any();

            var restrictedPackagePublication = PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute,
                                                    TestBuilderPermissionTarget.NamedTask,
                                                    TestBuilderPermissionNamedTask.RestrictedPackagePublication,
                                                    bankId,
                                                    0);
            var allowPublicationToServer = PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute,
                                                    TestBuilderPermissionTarget.NamedTask,
                                                    TestBuilderPermissionNamedTask.AllowPublicationToServer,
                                                    bankId,
                                                    0);

            foreach (var handler in config.Handlers.Cast<PublicationHandlerElement>().OrderBy(h => h.Order))
            {
                var publicationHandlerAllowed = !handler.RequireRestrictedPackagePublicationPermission || restrictedPackagePublication;

                if (publicationHandlerAllowed)
                {
                    if (!allowPublicationToServer && handler.ToString().ToLower().Contains("server"))
                    {
                        continue;
                    }
                    GetAllowedHandlers(isPreview, handler, supportedTypes, testPackageNames, testNames, result);
                }
            }

            return result;
        }

        private void GetAllowedHandlers(bool isPreview, PublicationHandlerElement handler, IList<string> supportedTypes,
            IList<string> testPackageNames, IList<string> testNames, Tuple<List<PublicationHandlerElement>, List<IPublicationHandler>, List<IPublicationHandler>, List<string>> result)
        {
            var publicationHandler = TryGetPublicationHandler(handler);
            if (publicationHandler == null)
            {
                return;
            }

            var handlerQualifiesForHandlingSelectedEntities = false;

            var reasonForNotQualifing = string.Empty;

            var handlerSupportsViewType = supportedTypes.Any(st => publicationHandler.SupportedViews.Select(s => s.ToString()).Contains(st));
            if (handlerSupportsViewType)
            {
                handlerQualifiesForHandlingSelectedEntities = isPreview;
                if (!handlerQualifiesForHandlingSelectedEntities)
                {
                    var testPackageMode = testPackageNames.Count > 0;

                    if (testPackageMode)
                    {
                        HandleTestPackages(testPackageNames, publicationHandler,
                            ref handlerQualifiesForHandlingSelectedEntities, ref reasonForNotQualifing);
                    }
                    else
                    {
                        HandleTestNames(testNames, publicationHandler, ref handlerQualifiesForHandlingSelectedEntities,
                            ref reasonForNotQualifing);
                    }
                }
            }

            if (handlerQualifiesForHandlingSelectedEntities)
            {
                result.Item2.Add(publicationHandler);
                result.Item1.Add(handler);
            }
            else
            {
                if (!string.IsNullOrEmpty(reasonForNotQualifing))
                {
                    result.Item3.Add(publicationHandler);
                    result.Item4.Add(reasonForNotQualifing);
                }
            }
        }

        private static void HandleTestNames(IList<string> testNames, IPublicationHandler publicationHandler,
            ref bool handlerQualifiesForHandlingSelectedEntities, ref string reasonForNotQualifing)
        {
            if (testNames.Count == 0 && publicationHandler.CanHandleBanks)
            {
                handlerQualifiesForHandlingSelectedEntities = true;
                return;
            }

            if (testNames.Count == 1 && publicationHandler.CanHandleSingleTests)
            {
                handlerQualifiesForHandlingSelectedEntities = true;
                return;
            }

            if (testNames.Count > 1 && publicationHandler.CanHandleMultipleTests)
            {
                handlerQualifiesForHandlingSelectedEntities = true;
                return;
            }

            reasonForNotQualifing =
                    testNames.Count == 1
                        ? Resources
                            .PublicationService_HandlerDoesNotSupportSingleTest
                        : Resources
                            .PublicationService_HandlerDoesNotSupportMultipleTests;
        }

        private static void HandleTestPackages(IList<string> testPackageNames, IPublicationHandler publicationHandler,
           ref bool handlerQualifiesForHandlingSelectedEntities, ref string reasonForNotQualifing)
        {
            if (testPackageNames.Count == 1 && publicationHandler.CanHandleSingleTestPackages)
            {
                handlerQualifiesForHandlingSelectedEntities = true;
            }
            else
            {
                if (testPackageNames.Count > 1 && publicationHandler.CanHandleMultipleTestPackages)
                {
                    handlerQualifiesForHandlingSelectedEntities = true;
                }
                else
                {
                    reasonForNotQualifing =
                        testPackageNames.Count == 1
                            ? Resources
                                .PublicationService_HandlerDoesNotSupportSingleTestPackage
                            : Resources
                                .PublicationService_HandlerDoesNotSupportMultipleTestPackages;
                }
            }
        }

        private IList<TestPreviewHandlerIdentifier> GetTestPreviewHandlers(IEnumerable<PublicationHandlerElement> publicationHandlerElements, IReadOnlyList<IPublicationHandler> publicationHandlers)
        {
            var result = new List<TestPreviewHandlerIdentifier>();
            var resourceDictionary = GetResources();
            var index = 0;
            foreach (var publicationHandlerElement in publicationHandlerElements.OrderBy(h => h.Order))
            {
                foreach (TestPreviewer testpreviewer in publicationHandlerElement.TestPreviewers)
                {
                    var name = testpreviewer.Name;
                    if (resourceDictionary.ContainsKey(testpreviewer.Name))
                    {
                        name = resourceDictionary[testpreviewer.Name];
                    }
                    var extension = string.Empty;
                    if (publicationHandlers != null && publicationHandlers.Count > index)
                    {
                        var publicationhandler = publicationHandlers[index];
                        extension = publicationhandler.FileExtension;
                    }
                    var handlerToAdd = new TestPreviewHandlerIdentifier(name, publicationHandlerElement.Type,
                        testpreviewer.Url, testpreviewer.ClickOnce, testpreviewer.DefaultClient, extension);
                    result.Add(handlerToAdd);
                }
                index++;
            }
            return result;
        }

        private Dictionary<string, string> GetResources()
        {
            var resources = new Dictionary<string, string>();
            var assembly = GetType().Assembly;
            var manifests = assembly.GetManifestResourceNames();
            if (manifests.Length == 1 && assembly.GetManifestResourceStream(manifests[0]) != null)
            {
                using (var reader = new ResourceReader(assembly.GetManifestResourceStream(manifests[0])))
                {
                    var dict = reader.GetEnumerator();
                    while (dict.MoveNext())
                    {
                        if (dict.Value is string)
                        {
                            resources.Add(dict.Key.ToString(), dict.Value.ToString());
                        }
                    }
                }
            }
            return resources;
        }

        private IPublicationHandler TryGetPublicationHandler(PublicationHandlerElement config)
        {
            var plugin = PublicationHandlers.FirstOrDefault(p => p.GetType().FullName == config.Type);

            if (plugin == null)
            {
                return plugin;
            }

            if (plugin.HandlerConfig == null)
                plugin.HandlerConfig = config.HandlerConfig;

            return plugin;
        }
    }
}
