using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cito.Tester.Common;
using Enums;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.Enums;
using Questify.Builder.Logic.ItemHarmonization.Factory;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Services.TasksService.Properties;
using Questify.Builder.Services.TasksService.TaskClasses;
using NLog;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Security;
using SD.LLBLGen.Pro.ORMSupportClasses;
using ItemResourceEntity = Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity;
using System.Configuration;
using Questify.Builder.Model.ContentModel.FactoryClasses;

namespace Questify.Builder.Services.TasksService.ItemHarmonization
{
    internal class ItemHarmonizationTask : QuestifyBuilderTaskBase
    {
        private readonly BuilderTaskProgressItem _itemProgressItem;
        private readonly List<ItemResourceEntity> _items;
        private readonly List<string> _templates;
        private HarmonizeOptions _options;
        private readonly ILogger _logger = LogManager.GetLogger(nameof(ItemHarmonizationTask));
        private int? _harmonizationItemBatchSize;

        internal ItemHarmonizationTask()
            : this(new List<ItemResourceEntity>(), new List<string>(), true, HarmonizeOptions.All)
        {

        }

        internal ItemHarmonizationTask(IEnumerable<ItemResourceEntity> items, List<string> templates, bool logTheActions, HarmonizeOptions options)
        {
            _items = items?.ToList();
            _templates = templates;
            _options = options;
            ExecutionParams = new ItemHarmonizationExecutionParams(logTheActions);
            _itemProgressItem = new BuilderTaskProgressItem("Item", _items?.Count ?? 1);
            var itemHarmonizationProgress = new BuilderTaskProgress { State = BuilderTaskProgress.ExecutionState.Running };
            Progress = itemHarmonizationProgress;
            itemHarmonizationProgress.ProgressItems.Add(_itemProgressItem);
            TaskResult = new BuilderTaskResult();
        }

        public List<ItemLayoutTemplateResourceEntity> ItemLayoutTemplateResourceEntities { get; set; }

        internal void StartItemHarmonization(object state)
        {
            try
            {
                if (_items != null || ItemLayoutTemplateResourceEntities?.Any() == true)
                {
                    if (_items == null)
                    {
                        HarmonizeTemplates();
                    }
                    else
                    {
                        SynchronizeItems(_items, _templates);
                    }

                    if (Progress.State == BuilderTaskProgress.ExecutionState.Running)
                    {
                        Progress.State = BuilderTaskProgress.ExecutionState.Finished;
                        TaskResult.TaskTermination = BuilderTaskResult.TaskTerminationState.Completed;
                    }
                    else
                    {
                        TaskResult.TaskTermination = BuilderTaskResult.TaskTerminationState.Cancelled;
                    }
                }
                else
                {
                    TaskResult.TaskTermination = BuilderTaskResult.TaskTerminationState.Completed;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error during harmonization process");
                Progress.State = BuilderTaskProgress.ExecutionState.Finished;
                TaskResult.TaskTermination = BuilderTaskResult.TaskTerminationState.Halted;
                TaskResult.Exceptions.Add(e.Message);
            }
        }

        public void StartHarmonizationAfterImport(object state, List<Guid> templateGuids, List<string> itemCodes, int bankId)
        {
            _logger.Info(new string('=', 100));
            _logger.Info("Starting Harmonization after import.");

            Progress.State = BuilderTaskProgress.ExecutionState.Running;

            var request = new ResourceRequestDTO(); ItemLayoutTemplateResourceEntities = ResourceFactory.Instance.GetResourcesByIdsWithOption(templateGuids, new ItemLayoutTemplateResourceEntityFactory(), request).OfType<ItemLayoutTemplateResourceEntity>().ToList();
            HarmonizeTemplates();

            _logger.Info($"Harmonizing imported items: {itemCodes.Count}");
            if (itemCodes?.Any() == true)
            {
                var itemRequest = new ItemResourceRequestDTO() { WithDependencies = true };
                _items.AddRange(ResourceFactory.Instance.GetItemsByCodes(itemCodes.ToList(), bankId, itemRequest));
            }

            if (_items.Any())
                _templates.AddRange(_items.Select(i => i.ItemLayoutTemplateUsedName));

            _logger.Info($"Going to harmonize {_items?.Count} items templates");
            _itemProgressItem.TotalCount = _items.Count;

            StartItemHarmonization(state);
        }

        private void HarmonizeTemplates()
        {
            _itemProgressItem.TotalCount = ItemLayoutTemplateResourceEntities.Count;
            _itemProgressItem.ProcessedCount = 0;
            var itemSync = new Harmonizer(_options);
            var exceptions = new ConcurrentBag<string>();
            var bankBranchHelper = new BankBranchIdHelper();

            _logger.Info($"Starting harmonization for {ItemLayoutTemplateResourceEntities.Count} templates");
            var templateNames = ItemLayoutTemplateResourceEntities.Select(ilt => ilt.Name).OrderBy(s => s).ToArray();
            _logger.Debug($"The following templates will be harmonized:{Environment.NewLine}{string.Join($"{Environment.NewLine}-", templateNames)}");
            foreach (var template in ItemLayoutTemplateResourceEntities.ToList())
            {
                if (IsCancelled())
                    return;

                _options = template.ItemType.Equals("inline", StringComparison.InvariantCultureIgnoreCase) ? HarmonizeOptions.Inline : HarmonizeOptions.Base;
                using (itemSync = new Harmonizer(_options))
                {
                    _logger.Info($"Starting harmonization for template {template.Name}");
                    HarmonizeTemplate(bankBranchHelper, template, itemSync, exceptions);
                }

                _itemProgressItem.ProcessedCount++;
                _logger.Info($"Harmonization for template {template.Name} finished");
                _logger.Info(new string('+', 30));
            }

            if (exceptions.Any())
            {
                _logger.Error("==================================== Exceptions during harmonization =======================================");
                foreach (string exception in exceptions)
                {
                    _logger.Error(exception);
                }
            }

            _logger.Info("==================================== Done =======================================");
        }

        private HarmonizeOptions GetHarmonizeOptions(string template)
        {
            var i = ItemLayoutTemplateResourceEntities?.FirstOrDefault(ilt => ilt.Name.Equals(template, StringComparison.InvariantCultureIgnoreCase));
            return i != null ? GetHarmonizeOptions(i) : _options;
        }

        private HarmonizeOptions GetHarmonizeOptions(ItemLayoutTemplateResourceEntity i)
        {
            if (i != null && i.ItemType.Equals(ItemTypeEnum.Inline.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return HarmonizeOptions.Inline;
            }
            return HarmonizeOptions.Base;
        }

        private void HarmonizeTemplate(BankBranchIdHelper bankBranchHelper, ItemLayoutTemplateResourceEntity template,
            Harmonizer itemSync, ConcurrentBag<string> exceptions)
        {
            var bankIds = bankBranchHelper.GetBankBrancheIds(template.BankId, false, true,
    TestBuilderPermissionTarget.ItemLayoutTemplateEntity, TestBuilderPermissionAccess.DALRead);

            ResourceFactory.Instance.GetReferencesForResource(template);

            Dictionary<string, List<Guid>> itemsInBanks = new Dictionary<string, List<Guid>>();
            foreach (var bankId in bankIds)
            {
                var splittedResourceList = template.ReferencedResourceCollection.Where(dr => dr.Resource is ItemResourceEntity && dr.Resource.BankId == bankId).Select(dr => dr.ResourceId).ToList().SplitList(HarmonizationBatchSize());
                var idx = 1;
                if (splittedResourceList.Any())
                {
                    splittedResourceList.ForEach(rl =>
                        {
                            itemsInBanks.Add($"{bankId}-{idx}", rl);
                            idx += 1;
                        });
                }
                else
                {
                    itemsInBanks.Add($"{bankId}-{idx}", null);
                }
            }

            var harmonizeOptions = GetHarmonizeOptions(template);
            var tasks = new List<Task>();
            foreach (var bankKey in itemsInBanks.Keys)
            {
                if (IsCancelled())
                    return;

                int bankId = 0;
                int.TryParse(bankKey.Substring(0, bankKey.IndexOf("-", 0)), out bankId);
                string bankName = BankFactory.Instance.GetBankPath(bankId);
                var itemsToProcess = itemsInBanks[bankKey];
                if (itemsToProcess != null && itemsToProcess.Any())
                {
                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            _logger.Info(
                                $"Harmonizing {itemsInBanks[bankKey].Count} items in bank {bankId}-{bankName} for template {template.Name}");

                            var nrProcessed = 0;
                            foreach (ItemResourceEntity itemResource in ResourceFactory.Instance.GetResourcesByIdsWithOption(itemsToProcess, new ItemResourceEntityFactory(), new ResourceRequestDTO()))
                            {
                                if (IsCancelled())
                                    return;

                                HarmonizeItem(new List<string>() { template.Name }, itemResource, itemSync, template.Name,
                                    exceptions, harmonizeOptions);
                                nrProcessed++;

                                if (nrProcessed % 10 == 0)
                                    _logger.Info(
                                        $"Processed {nrProcessed} of {itemsToProcess.Count} items for template {template.Name} in bank {bankId}-{bankName}");
                            }

                            if (nrProcessed > 0)
                            {
                                ResourceFactory.Instance.GetResourcesByIdsWithOption(itemsToProcess, new ItemResourceEntityFactory(), new ResourceRequestDTO());
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.Error(e,
                                $"Error while harmonizing bank {bankId}-{bankName} for template {template?.Name}");
                            exceptions.Add(e.Message);
                        }
                    }));
                }
                else
                {
                    _logger.Info($"No items found for template {template.Name} in bank {bankId}-{bankName}");
                }
            }

            try
            {
                if (tasks.Any())
                {
                    Task.WaitAll(tasks.ToArray());
                }
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    _logger.Error(e, $"Error while harmonizing banks for template {template?.Name}: {e.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error while harmonizing banks for template {template?.Name}: {ex.Message}");
            }

            itemsInBanks.Clear();
            foreach (var task in tasks)
            {
                task.Dispose();
            }
        }

        private bool IsCancelled()
        {
            if (ExecutionParams.CancellationPending)
            {
                _logger.Info(Resources.ProcessCancelled);
                return true;
            }

            return false;
        }

        private int HarmonizationBatchSize()
        {
            if (!_harmonizationItemBatchSize.HasValue)
            {
                int result = 0;
                int.TryParse(ConfigurationManager.AppSettings["HarmonizationItemBatchSize"], out result);
                if (result > 0)
                {
                    _harmonizationItemBatchSize = result;
                }
                else
                {
                    _harmonizationItemBatchSize = 500;
                }
            }

            return _harmonizationItemBatchSize.Value;
        }

        private RelationPredicateBucket GetSearchBucket(Guid templateId, int bankId)
        {
            var bucket = new RelationPredicateBucket();
            bucket.PredicateExpression.Add(new FieldCompareSetPredicate(ItemResourceFields.ResourceId,
                null, DependentResourceFields.ResourceId, null, SetOperator.In,
                DependentResourceFields.DependentResourceId == templateId));

            bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemResourceFields.BankId, null, ComparisonOperator.Equal, bankId));
            return bucket;
        }

        private void SynchronizeItems(IList<ItemResourceEntity> items, List<string> templates)
        {
            _itemProgressItem.TotalCount = items.Count() + 1;
            _itemProgressItem.ProcessedCount = 0;
            var nrFixed = 0;
            var itemSync = new Harmonizer(_options);
            var exceptions = new ConcurrentBag<string>();

            if (templates != null && templates.Any(t => !string.IsNullOrEmpty(t)))
                _logger.Info($"Harmonizing {templates.Distinct().Count()} templates");

            foreach (var itemsPerBank in items.GroupBy(i => i.GetFullBankPath() ?? i.BankId.ToString()))
            {
                _logger.Info($"Start harmonization for bank: {itemsPerBank.Key}, NrOfItems: {itemsPerBank.Count()}");

                foreach (var template in templates?.Distinct().ToList() ?? new List<string>() { "" })
                {
                    _logger.Info($"Start harmonization for template '{template}'");
                    HarmonizeOptions harmonizeOptions = GetHarmonizeOptions(template);

                    var itemsInBankForTemplate = itemsPerBank.Where(i => TemplateUsedByItem(i, template)).ToList();
                    foreach (var itemResource in itemsInBankForTemplate)
                    {
                        if (HarmonizeItem(templates, itemResource, itemSync, template, exceptions, harmonizeOptions))
                        {
                            nrFixed++;
                        }

                        _itemProgressItem.ProcessedCount++;
                    }
                }

                _logger.Info($"Finished harmonization for bank '{itemsPerBank.Key}'");
            }

            _logger.Info($"Harmonization done. #items processed: {_itemProgressItem.ProcessedCount}. #items updated: {nrFixed}. #exceptions: {exceptions.Count}.");
            _logger.Info(new string('=', 100));

            _itemProgressItem.ProgressItemCode = Resources.HarmonizingReady;
            _itemProgressItem.IncrementProcessedCount();
            TaskResult.Exceptions.AddRange(exceptions.ToArray());


            TaskResult.Info.Add(templates?.Any() == true
    ? $"For item layout templates {string.Join(", ", templates.ToArray())} {_itemProgressItem.ProcessedCount} items have been processed."
    : $"{_itemProgressItem.ProcessedCount} items have been processed.");
        }

        private bool HarmonizeItem(List<string> templates, ItemResourceEntity itemResource, Harmonizer itemSync, string template,
            ConcurrentBag<string> exceptions, HarmonizeOptions harmonizeOptions)
        {
            try
            {
                if (IsCancelled())
                    return false;

                var msg = string.Format(Resources.HarmonizingResource, itemResource.Name);
                _itemProgressItem.ProgressItemCode = msg;
                _logger.Trace(msg);

                bool result;
                if (templates == null || !templates.Any(string.IsNullOrEmpty))
                {
                    result = itemSync.Harmonize(itemResource);
                }
                else if (!string.IsNullOrEmpty(template))
                {
                    result = itemSync.Harmonize(itemResource, template, harmonizeOptions);
                }
                else
                {
                    result = itemSync.Harmonize(templates, itemResource, harmonizeOptions);
                }

                if (result)
                {
                    itemResource.IsDirty = true;
                    ResourceFactory.Instance.UpdateItemResource(itemResource, false, false, true, true);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception while harmonizing item {itemResource.Name} for template {template}");
                _logger.Info($"{ex.Message}");
                exceptions.Add($"{ex.Message} - {ex.InnerException} - {ex.StackTrace}");

                return false;
            }
            finally
            {
                if (_itemProgressItem.ProcessedCount % 50 == 0 && _itemProgressItem.ProcessedCount > 0)
                {
                    _logger.Info($"Processed {_itemProgressItem.ProcessedCount} of {_itemProgressItem.TotalCount}");
                }
            }
        }

        private bool TemplateUsedByItem(ItemResourceEntity itemResourceEntity, string template)
        {
            if (string.IsNullOrEmpty(template)) return true;
            IEnumerable<ResourceEntity> dependentEntities;
            if (itemResourceEntity.DependentResourceCollection == null || itemResourceEntity.DependentResourceCollection.Count == 0)
                dependentEntities = ResourceFactory.Instance.GetDependenciesForResource(itemResourceEntity).OfType<ResourceEntity>().ToList();
            else
                dependentEntities = itemResourceEntity.DependentResourceCollection.Select(dr => dr.DependentResource).ToList();

            return dependentEntities.Any(dr => dr.Name.Equals(template));
        }
    }
}