using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Cinch;
using Enums;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [ExportViewModel("ItemEditor.ItemConceptStructureVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ItemConceptStructureViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {

        static readonly PropertyChangedEventArgs ConceptPropertiesArgs = ObservableHelper.CreateArgs<ItemConceptStructureViewModel>(x => x.ConceptProperties);
        static readonly PropertyChangedEventArgs ConceptStructurePartsArgs = ObservableHelper.CreateArgs<ItemConceptStructureViewModel>(x => x.ConceptStructureParts);
        static readonly PropertyChangedEventArgs HasValidConceptPropertySelectionArgs = ObservableHelper.CreateArgs<ItemConceptStructureViewModel>(x => x.HasValidConceptPropertySelection);
        static readonly PropertyChangedEventArgs SelectedConceptPropertyArgs = ObservableHelper.CreateArgs<ItemConceptStructureViewModel>(x => x.SelectedConceptProperty);
        static readonly PropertyChangedEventArgs SelectedConceptStructurePartArgs = ObservableHelper.CreateArgs<ItemConceptStructureViewModel>(x => x.SelectedConceptStructurePart);



        private List<ConceptStructureCustomBankPropertyEntity> _conceptProperties;
        private List<ConceptStructurePartCustomBankPropertyEntity> _conceptStructureParts = new List<ConceptStructurePartCustomBankPropertyEntity>();
        private IEnumerable<EntityBase2> _customProperties;
        private IItemEditorViewModel _itemEditorVM;
        private readonly IViewAwareStatus _viewAwareStatusService;
        private Dictionary<Guid, Tuple<Guid, HashSet<String>>> _customPropertyValuesBeforeEditing = null;

        private bool _viewLoadedIsExecuting = false;



        [ImportingConstructor]
        public ItemConceptStructureViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;

            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;

            SelectedConceptProperty = new DataWrapper<ConceptStructureCustomBankPropertyEntity>(this, SelectedConceptPropertyArgs, SelectedConceptPropertyChangedCallBack);
            SelectedConceptStructurePart = new DataWrapper<ConceptStructurePartCustomBankPropertyEntity>(this, SelectedConceptStructurePartArgs, SelectedConceptStructurePartChangedCallBack);
        }



        public List<ConceptStructureCustomBankPropertyEntity> ConceptProperties
        {
            get { return _conceptProperties; }
            set
            {
                _conceptProperties = value;
                NotifyPropertyChanged(ConceptPropertiesArgs);
            }
        }

        public List<ConceptStructurePartCustomBankPropertyEntity> ConceptStructureParts
        {
            get { return _conceptStructureParts; }
            set
            {
                _conceptStructureParts = value;
                NotifyPropertyChanged(ConceptStructurePartsArgs);
            }
        }

        public bool HasValidConceptPropertySelection
        {
            get
            {
                if (SelectedConceptProperty.DataValue == null)
                    return false;

                return SelectedConceptProperty.DataValue.CustomBankPropertyId != Guid.Empty;
            }
            set { }
        }

        public DataWrapper<ConceptStructureCustomBankPropertyEntity> SelectedConceptProperty { get; private set; }

        public DataWrapper<ConceptStructurePartCustomBankPropertyEntity> SelectedConceptStructurePart { get; private set; }



        private static void AddConceptStructurePartIfApplicable(ref List<ConceptStructurePartCustomBankPropertyEntity> conceptParts, ConceptStructurePartCustomBankPropertyEntity conceptStructurePart)
        {

            if (conceptStructurePart.ConceptType.ApplicableToMask == (int)((ConceptTypeApplicableTo)conceptStructurePart.ConceptType.ApplicableToMask | ConceptTypeApplicableTo.Item))
                conceptParts.Add(conceptStructurePart);
        }

        private List<ConceptStructurePartCustomBankPropertyEntity> GetConceptStructureParts(ConceptStructureCustomBankPropertyEntity conceptStructureCustomBankProperty)
        {
            var conceptParts = new List<ConceptStructurePartCustomBankPropertyEntity>();

            foreach (var conceptStructurePartCustomBankPropertyEntity in conceptStructureCustomBankProperty.ConceptStructurePartCustomBankPropertyCollection)
            {
                AddConceptStructurePartIfApplicable(ref conceptParts, conceptStructurePartCustomBankPropertyEntity);
            }

            return conceptParts.OrderBy(cp => cp.Name).ToList();
        }

        private List<ConceptStructureCustomBankPropertyEntity> GetConceptStructureProperties()
        {
            var conceptProperties = _customProperties.OfType<ConceptStructureCustomBankPropertyEntity>().ToList();

            conceptProperties.Insert(0, new ConceptStructureCustomBankPropertyEntity(Guid.Empty) { Name = GetResourceText("NoSelection") });

            return conceptProperties.ToList();
        }

        private static string GetResourceText(string key)
        {
            const string prefix = "ItemEditor.ItemConceptStructureViewModel.";
            var app = Application.Current;

            return (string)app.FindResource(prefix + key);
        }

        private void SelectedConceptPropertyChangedCallBack()
        {
            var conceptCustomBankPropertyValue = _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().SingleOrDefault();

            if (!HasValidConceptPropertySelection)
            {
                if (conceptCustomBankPropertyValue != null && !_viewLoadedIsExecuting)
                {
                    _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Remove(conceptCustomBankPropertyValue);
                    _itemEditorVM.CustomPropertiesValueCollectionRemovedEntitiesTracker.Add(conceptCustomBankPropertyValue); _itemEditorVM.AssessmentItem.DataValue.Solution.ClearConceptFinding();
                    SelectedConceptStructurePart.DataValue = null;
                }

                NotifyPropertyChanged(HasValidConceptPropertySelectionArgs);
                return;
            }

            var resourceId = _itemEditorVM.ItemResourceEntity.DataValue.ResourceId;
            var customBankPropertyId = SelectedConceptProperty.DataValue.CustomBankPropertyId;

            if (conceptCustomBankPropertyValue == null)
            {
                conceptCustomBankPropertyValue = new ConceptStructureCustomBankPropertyValueEntity(resourceId,
                                                                                   customBankPropertyId);

                _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Add(conceptCustomBankPropertyValue);
            }

            if (conceptCustomBankPropertyValue.CustomBankPropertyId != SelectedConceptProperty.DataValue.CustomBankPropertyId)
            {
                _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Remove(conceptCustomBankPropertyValue);

                var newConceptCustomBankPropertyValue = new ConceptStructureCustomBankPropertyValueEntity(resourceId,
                                                                                   customBankPropertyId);

                _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Add(newConceptCustomBankPropertyValue);
                _itemEditorVM.AssessmentItem.DataValue.Solution.ClearConceptFinding();
            }

            ConceptStructureParts = GetConceptStructureParts(SelectedConceptProperty.DataValue);

            if (conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection.Any())
            {
                var conceptStructureCustomBankPropertySelectedPartEntity =
                    conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection.FirstOrDefault();

                var valueToSet = ConceptStructureParts.FirstOrDefault(cs => conceptStructureCustomBankPropertySelectedPartEntity != null &&
                                                                            cs.ConceptStructurePartCustomBankPropertyId == conceptStructureCustomBankPropertySelectedPartEntity.ConceptStructurePartId);

                if (!object.ReferenceEquals(SelectedConceptStructurePart.DataValue, valueToSet))
                    SelectedConceptStructurePart.DataValue = valueToSet;
            }

            if (SelectedConceptStructurePart.DataValue == null)
                SelectedConceptStructurePart.DataValue = ConceptStructureParts.FirstOrDefault();

            NotifyPropertyChanged(HasValidConceptPropertySelectionArgs);
        }

        private void SelectedConceptStructurePartChangedCallBack()
        {
            if (_viewLoadedIsExecuting)
            {
                return;
            }

            if (SelectedConceptStructurePart?.DataValue != null)
            {
                CheckResetCustomPropertyValuesBeforeEditing(SelectedConceptStructurePart.DataValue.CustomBankPropertyId);
            }

            if (SelectedConceptStructurePart?.DataValue == null || SelectedConceptStructurePart.DataValue.ConceptStructurePartCustomBankPropertyId == Guid.Empty)
                return;

            var conceptStructureValue = _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().FirstOrDefault();

            if (conceptStructureValue != null)
            {
                var selectedPart = conceptStructureValue.ConceptStructureCustomBankPropertySelectedPartCollection.FirstOrDefault() ??
                                   conceptStructureValue.ConceptStructureCustomBankPropertySelectedPartCollection.AddNew();
                selectedPart.ConceptStructurePartId = SelectedConceptStructurePart.DataValue.ConceptStructurePartCustomBankPropertyId;
            }

            if (!_viewLoadedIsExecuting)
            {
                _itemEditorVM.AssessmentItem.DataValue.Solution.ClearConceptFinding();

                if (conceptStructureValue != null)
                {
                    conceptStructureValue.IsDirty = true;
                }
            }
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                _viewLoadedIsExecuting = true;
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;

                var data = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                _itemEditorVM = data;
                _customProperties = _itemEditorVM.CustomBankProperties;
            }

            ConceptProperties = GetConceptStructureProperties();
            AddCustomPropertyValuesBeforeEditing();

            SelectedConceptStructurePart.DataValue = null;

            var conceptValue = _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().FirstOrDefault();
            var valueToSet = (conceptValue == null) ? ConceptProperties.First() : ConceptProperties.FirstOrDefault(cp => cp.CustomBankPropertyId == conceptValue.CustomBankPropertyId);
            SelectedConceptProperty.DataValue = valueToSet;

            SelectedConceptPropertyChangedCallBack();

            _viewLoadedIsExecuting = false;
            NotifyPropertyChanged(HasValidConceptPropertySelectionArgs);
        }

        private void AddCustomPropertyValuesBeforeEditing()
        {
            var conceptCustomBankPropertyValue = _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().SingleOrDefault();
            if (conceptCustomBankPropertyValue != null)
            {
                AddCustomPropertyValuesBeforeEditing(conceptCustomBankPropertyValue.CustomBankPropertyId, GetSelectedConceptValues(conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection));
            }
            else if (SelectedConceptProperty != null && SelectedConceptProperty.DataValue != null)
            {
                AddCustomPropertyValuesBeforeEditing(SelectedConceptProperty.DataValue.CustomBankPropertyId, new HashSet<string>());
            }
        }

        private void AddCustomPropertyValuesBeforeEditing(Guid customPropertyId, HashSet<string> valuesBeforeEditing)
        {
            if (_customPropertyValuesBeforeEditing == null)
            {
                _customPropertyValuesBeforeEditing = new Dictionary<Guid, Tuple<Guid, HashSet<string>>>();
            }
            if (!_customPropertyValuesBeforeEditing.ContainsKey(customPropertyId))
            {
                _customPropertyValuesBeforeEditing.Add(customPropertyId, new Tuple<Guid, HashSet<string>>(_itemEditorVM.ItemResourceEntity.DataValue.ResourceId, valuesBeforeEditing));
            }
        }

        private HashSet<string> GetSelectedConceptValues(EntityCollection<ConceptStructureCustomBankPropertySelectedPartEntity> selectedParts)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (ConceptStructureCustomBankPropertySelectedPartEntity selectedPart in selectedParts)
            {
                result.Add(selectedPart.ConceptStructurePartId.ToString());
            }
            return result;
        }

        private void CheckResetCustomPropertyValuesBeforeEditing(Guid customBankPropertyId)
        {
            if (_customPropertyValuesBeforeEditing == null)
            {
                ResetCustomPropertyValuesBeforeEditing();
            }
            else if (!_customPropertyValuesBeforeEditing.ContainsKey(customBankPropertyId))
            {
                ResetCustomPropertyValuesBeforeEditing();
            }
            else
            {
                Tuple<Guid, HashSet<string>> beforeEditing = _customPropertyValuesBeforeEditing[customBankPropertyId];
                if (beforeEditing.Item1 != _itemEditorVM.ItemResourceEntity.DataValue.ResourceId)
                {
                    ResetCustomPropertyValuesBeforeEditing();
                }
            }
        }

        public void DoPreSaveTasks()
        {
            if (SelectedConceptProperty != null && SelectedConceptProperty.DataValue != null)
            {
                var customBankPropertyId = SelectedConceptProperty.DataValue.CustomBankPropertyId;

                CheckResetCustomPropertyValuesBeforeEditing(customBankPropertyId);

                if (_customPropertyValuesBeforeEditing != null && _customPropertyValuesBeforeEditing.ContainsKey(customBankPropertyId))
                {
                    var conceptCustomBankPropertyValue = _itemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().SingleOrDefault();
                    if (conceptCustomBankPropertyValue != null)
                    {
                        HashSet<string> selectedConceptPartsHashset = GetSelectedConceptValues(conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection);
                        conceptCustomBankPropertyValue.IsDirty = !(_customPropertyValuesBeforeEditing[customBankPropertyId].Item2.SetEquals(selectedConceptPartsHashset));
                        if (!conceptCustomBankPropertyValue.IsDirty)
                        {
                            if (conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection.RemovedEntitiesTracker != null)
                            {
                                conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection.RemovedEntitiesTracker.Clear();
                                conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection.RemovedEntitiesTracker = null;
                            }

                            foreach (ConceptStructureCustomBankPropertySelectedPartEntity selectedPart in conceptCustomBankPropertyValue.ConceptStructureCustomBankPropertySelectedPartCollection)
                            {
                                selectedPart.IsDirty = false;
                                selectedPart.IsNew = false;
                            }

                            IPredicate filter = (ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId == customBankPropertyId);
                            List<int> indexes = _itemEditorVM.CustomPropertiesValueCollectionRemovedEntitiesTracker.FindMatches(filter);
                            if (indexes.Count > 0)
                            {
                                foreach (int index in indexes.OrderByDescending(i => i))
                                {
                                    _itemEditorVM.CustomPropertiesValueCollectionRemovedEntitiesTracker.RemoveAt(indexes[index]);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ResetCustomPropertyValuesBeforeEditing()
        {
            _customPropertyValuesBeforeEditing = null;
            AddCustomPropertyValuesBeforeEditing();
        }

        public void DoPostSaveTasks()
        {
            ResetCustomPropertyValuesBeforeEditing();
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void KillView()
        {
        }

        protected override void OnDispose()
        {
            _conceptProperties = null;
            _conceptStructureParts = null;
            _customProperties = null;
            _customPropertyValuesBeforeEditing = null;

            if (_itemEditorVM != null)
            {
                _itemEditorVM = null;
            }

            base.OnDispose();
        }

    }
}