using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activation;
using System.Threading;
using Cito.Tester.Common;
using Enums;
using NLog;
using Questify.Builder.Logic.Enums;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Services.TasksService.ItemHarmonization;
using Questify.Builder.Services.TasksService.TaskClasses;

namespace Questify.Builder.Services.TasksService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class QuestifyBuilderTasksService : IQuestifyBuilderTasksService
    {
        private static Dictionary<Guid, QuestifyBuilderTaskBase> _activeBuilderTasks = new Dictionary<Guid, QuestifyBuilderTaskBase>();

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static readonly object _activeBuilderTasksAccessLocker = new object();
        internal static Dictionary<Guid, QuestifyBuilderTaskBase> ActiveBuilderTasks { get { return _activeBuilderTasks; } }

        public BuilderTaskProgress PollProgress(BuilderTaskSessionTicket builderTaskSessionTicket)
        {
            BuilderTaskProgress progress = null;

            try
            {
                lock (_activeBuilderTasksAccessLocker)
                {
                    if (ActiveBuilderTasks.ContainsKey(builderTaskSessionTicket.Id))
                    {
                        progress = ActiveBuilderTasks[builderTaskSessionTicket.Id].Progress;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error occured in PollProgress.");
            }

            return progress;
        }

        public void RequestCancellation(BuilderTaskSessionTicket builderTaskSessionTicket)
        {
            lock (_activeBuilderTasksAccessLocker)
            {
                if (_activeBuilderTasks.ContainsKey(builderTaskSessionTicket.Id))
                {
                    _activeBuilderTasks[builderTaskSessionTicket.Id].ExecutionParams.CancellationPending = true;
                }
            }
        }

        public Stream GetLogFileStream(BuilderTaskSessionTicket builderTaskSessionTicket)
        {
            lock (_activeBuilderTasksAccessLocker)
            {
                if (_activeBuilderTasks.ContainsKey(builderTaskSessionTicket.Id))
                {
                    if (_activeBuilderTasks[builderTaskSessionTicket.Id].Progress.State != BuilderTaskProgress.ExecutionState.Running)
                    {
                        if (_activeBuilderTasks[builderTaskSessionTicket.Id].ExecutionParams.LogTheActions && !string.IsNullOrEmpty(_activeBuilderTasks[builderTaskSessionTicket.Id].ExecutionParams.TempLogFileName))
                        {
                            return new FileStream(_activeBuilderTasks[builderTaskSessionTicket.Id].ExecutionParams.TempLogFileName, FileMode.Open);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("The GetLogFileStream method cannot be called while the task is still running");
                    }
                }
            }

            return null;
        }

        public BuilderTaskResult GetTaskResult(BuilderTaskSessionTicket builderTaskSessionTicket)
        {
            BuilderTaskResult taskResult = null;

            lock (_activeBuilderTasksAccessLocker)
            {
                if (_activeBuilderTasks.ContainsKey(builderTaskSessionTicket.Id))
                {
                    if (_activeBuilderTasks[builderTaskSessionTicket.Id].Progress.State != BuilderTaskProgress.ExecutionState.Running)
                    {
                        taskResult = _activeBuilderTasks[builderTaskSessionTicket.Id].TaskResult;
                    }
                    else
                    {
                        throw new InvalidOperationException("The GetTaskResult method cannot be called while the task is still running");
                    }
                }
            }
            return taskResult;
        }

        public void Cleanup(BuilderTaskSessionTicket builderTaskSessionTicket)
        {
            lock (_activeBuilderTasksAccessLocker)
            {
                if (_activeBuilderTasks.ContainsKey(builderTaskSessionTicket.Id))
                {
                    _activeBuilderTasks[builderTaskSessionTicket.Id].Dispose();
                    _activeBuilderTasks.Remove(builderTaskSessionTicket.Id);
                }
            }
        }

        public BuilderTaskSessionTicket HarmonizeWithItemLayoutTemplates(List<Guid> templateGuids, bool logTheActions)
        {
            var harmonizationSessionTicket = new BuilderTaskSessionTicket();
            HarmonizeTemplates(templateGuids, harmonizationSessionTicket);
            return harmonizationSessionTicket;
        }

        public BuilderTaskSessionTicket HarmonizeItems(IEnumerable<Guid> itemResourceIds, bool logTheActions)
        {
            var harmonizationSessionTicket = new BuilderTaskSessionTicket();
            var items = ResourceFactory.Instance.GetResourcesByIdsWithOption(itemResourceIds.ToList(), new ItemResourceEntityFactory(), new ResourceRequestDTO()).OfType<ItemResourceEntity>().ToList();
            HarmonizeItems(logTheActions, harmonizationSessionTicket, items);
            return harmonizationSessionTicket;
        }

        private static void HarmonizeItems(bool logTheActions, BuilderTaskSessionTicket harmonizationSessionTicket, List<ItemResourceEntity> items)
        {
            var itemHarmonizer = new ItemHarmonizationTask(items, null, logTheActions, HarmonizeOptions.All);
            lock (_activeBuilderTasksAccessLocker)
            {
                _activeBuilderTasks.Add(harmonizationSessionTicket.Id, itemHarmonizer);
            }
            ThreadPool.QueueUserWorkItem(itemHarmonizer.StartItemHarmonization, harmonizationSessionTicket);
        }

        private static void HarmonizeTemplates(List<Guid> templateGuids, BuilderTaskSessionTicket harmonizationSessionTicket)
        {
            var templates = ResourceFactory.Instance.GetResourcesByIdsWithOption(templateGuids, new ItemLayoutTemplateResourceEntityFactory(), new ResourceRequestDTO()).OfType<ItemLayoutTemplateResourceEntity>().ToList();
            var options = templates.Any(t => t.ItemType.Equals(ItemTypeEnum.Inline.ToString(), StringComparison.CurrentCultureIgnoreCase))
                ? HarmonizeOptions.All
                : HarmonizeOptions.Base;

            var itemHarmonizer = new ItemHarmonizationTask(null, templates.Select(t => t.Name).ToList(), true, options)
            {
                ItemLayoutTemplateResourceEntities = templates
            };

            lock (_activeBuilderTasksAccessLocker)
            {
                _activeBuilderTasks.Add(harmonizationSessionTicket.Id, itemHarmonizer);
            }
            ThreadPool.QueueUserWorkItem(itemHarmonizer.StartItemHarmonization, harmonizationSessionTicket);
        }

        private static void HarmonizeTemplatesAndItems(int bankId, List<Guid> templateGuids, IEnumerable<string> itemCodes, BuilderTaskSessionTicket harmonizationSessionTicket)
        {
            var itemHarmonizer = new ItemHarmonizationTask();
            lock (_activeBuilderTasksAccessLocker)
            {
                _activeBuilderTasks.Add(harmonizationSessionTicket.Id, itemHarmonizer);
            }

            ThreadPool.QueueUserWorkItem(state =>
                {
                    itemHarmonizer.StartHarmonizationAfterImport(state, templateGuids, itemCodes.ToList(), bankId);
                }, harmonizationSessionTicket);
        }

        public BuilderTaskSessionTicket HarmonizeAfterImport(int bankId, IEnumerable<Guid> templateGuids, IEnumerable<string> itemCodes)
        {
            var harmonizationSessionTicket = new BuilderTaskSessionTicket();
            HarmonizeTemplatesAndItems(bankId, templateGuids.ToList(), itemCodes, harmonizationSessionTicket);
            return harmonizationSessionTicket;
        }
    }
}
