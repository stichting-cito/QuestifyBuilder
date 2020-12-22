using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [ExportViewModel("ItemEditor.ItemTreeStructureVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ItemTreeStructureViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {

        static readonly PropertyChangedEventArgs TreeArgs = ObservableHelper.CreateArgs<ItemTreeStructureViewModel>(x => x.Tree);
        static readonly PropertyChangedEventArgs TreeStructureCustomBankPropertyEntitiesArgs = ObservableHelper.CreateArgs<ItemTreeStructureViewModel>(x => x.TreeStructureCustomBankPropertyEntities);
        static readonly PropertyChangedEventArgs HasValidTreePropertySelectionArgs = ObservableHelper.CreateArgs<ItemTreeStructureViewModel>(x => x.IsValidTreeStructureSelected);
        static readonly PropertyChangedEventArgs SelectedTreePropertyArgs = ObservableHelper.CreateArgs<ItemTreeStructureViewModel>(x => x.SelectedTreeStructureProperty);



        private readonly IViewAwareStatus _viewAwareStatusService;
        private IEnumerable<EntityBase2> _customProperties = null;
        private IItemEditorViewModel _itemEditorVm = null;
        private Dictionary<Guid, Tuple<Guid, HashSet<String>>> _customPropertyValuesBeforeEditing = null;
        private bool _isLoading = false;


        [ImportingConstructor]
        public ItemTreeStructureViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;

            Tree = new DataWrapper<ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel>>(this, TreeArgs);
            SelectedTreeStructureProperty = new DataWrapper<TreeStructureCustomBankPropertyEntity>(this, SelectedTreePropertyArgs, SelectedTreePropertyChangedCallBack);
            TreeStructureCustomBankPropertyEntities = new DataWrapper<List<TreeStructureCustomBankPropertyEntity>>(this, TreeStructureCustomBankPropertyEntitiesArgs);

            CheckedCommand = new SimpleCommand<object, TreeStructurePartCustomBankPropertyViewModel>(DoChecked);
            UncheckedCommand = new SimpleCommand<object, TreeStructurePartCustomBankPropertyViewModel>(DoUnchecked);
        }



        public DataWrapper<ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel>> Tree { get; private set; }
        public DataWrapper<TreeStructureCustomBankPropertyEntity> SelectedTreeStructureProperty { get; private set; }
        public DataWrapper<List<TreeStructureCustomBankPropertyEntity>> TreeStructureCustomBankPropertyEntities { get; private set; }

        public SimpleCommand<object, TreeStructurePartCustomBankPropertyViewModel> CheckedCommand { get; private set; }
        public SimpleCommand<object, TreeStructurePartCustomBankPropertyViewModel> UncheckedCommand { get; private set; }

        public bool IsValidTreeStructureSelected
        {
            get
            {
                if (SelectedTreeStructureProperty == null || SelectedTreeStructureProperty.DataValue == null)
                    return false;

                return SelectedTreeStructureProperty.DataValue.CustomBankPropertyId != Guid.Empty;
            }
            set { }
        }



        private void DoChecked(TreeStructurePartCustomBankPropertyViewModel checkedTreeStructurePartCustomBankPropertyViewModel)
        {
            var resourceId = _itemEditorVm.ItemResourceEntity.DataValue.ResourceId;
            var customBankPropertyId = SelectedTreeStructureProperty.DataValue.CustomBankPropertyId;
            var treeCustomBankPropertyValue = _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().SingleOrDefault();

            if (treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.FirstOrDefault(x => x.ResourceId == resourceId && x.CustomBankPropertyId == customBankPropertyId && x.TreeStructurePartId == checkedTreeStructurePartCustomBankPropertyViewModel.TreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId) == null)
            {
                var selectedTreeStructurePart = new TreeStructureCustomBankPropertySelectedPartEntity(checkedTreeStructurePartCustomBankPropertyViewModel.TreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId, resourceId, customBankPropertyId);
                treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(selectedTreeStructurePart);
            }

            if (checkedTreeStructurePartCustomBankPropertyViewModel.Parent != null) checkedTreeStructurePartCustomBankPropertyViewModel.Parent.IsChecked.DataValue = true;
            treeCustomBankPropertyValue.IsDirty = true;
        }

        private void DoUnchecked(TreeStructurePartCustomBankPropertyViewModel uncheckedTreeStructurePartCustomBankPropertyViewModel)
        {
            var treeCustomBankPropertyValue = _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().SingleOrDefault();
            var selectedPart = ConvertTreeStructurePartToTreeStructureSelectedPart(uncheckedTreeStructurePartCustomBankPropertyViewModel.TreeStructurePartCustomBankPropertyEntity, false);
            if (selectedPart != null)
            {
                _itemEditorVm.CustomPropertiesValueCollectionRemovedEntitiesTracker.Add(selectedPart); treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.Remove(selectedPart);
                foreach (var child in uncheckedTreeStructurePartCustomBankPropertyViewModel.Children.DataValue)
                {
                    child.IsChecked.DataValue = false; DoUnchecked(child);
                }
                treeCustomBankPropertyValue.IsDirty = true;
            }
        }

        private void SelectedTreePropertyChangedCallBack()
        {
            if (_isLoading)
            {
                return;
            }

            if (SelectedTreeStructureProperty?.DataValue != null)
            {
                CheckResetCustomPropertyValuesBeforeEditing(SelectedTreeStructureProperty.DataValue.CustomBankPropertyId);
            }
            Tree.DataValue = CreateTreeStructure(SelectedTreeStructureProperty.DataValue);
            var treeCustomBankPropertyValue = _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().SingleOrDefault();

            if (!IsValidTreeStructureSelected)
            {
                if (treeCustomBankPropertyValue != null)
                {
                    _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Remove(treeCustomBankPropertyValue);
                    _itemEditorVm.CustomPropertiesValueCollectionRemovedEntitiesTracker.Add(treeCustomBankPropertyValue);
                }

                NotifyPropertyChanged(HasValidTreePropertySelectionArgs);
                return;
            }

            var resourceId = _itemEditorVm.ItemResourceEntity.DataValue.ResourceId;
            var customBankPropertyId = SelectedTreeStructureProperty.DataValue.CustomBankPropertyId;

            if (treeCustomBankPropertyValue == null)
            {
                treeCustomBankPropertyValue = new TreeStructureCustomBankPropertyValueEntity(resourceId, customBankPropertyId);

                _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Add(treeCustomBankPropertyValue);
            }

            if (treeCustomBankPropertyValue.CustomBankPropertyId != SelectedTreeStructureProperty.DataValue.CustomBankPropertyId)
            {
                if (treeCustomBankPropertyValue.CustomBankProperty != null)
                {
                    foreach (var customBankPropertyValue in treeCustomBankPropertyValue.CustomBankProperty.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>())
                    {
                        for (var i = customBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.Count - 1; i >= 0; i--)
                        {
                            _itemEditorVm.CustomPropertiesValueCollectionRemovedEntitiesTracker.Add(customBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection[i]); customBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.Remove(customBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection[i]);
                        }
                    }
                }

                _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Remove(treeCustomBankPropertyValue);

                var newTreeCustomBankPropertyValue = new TreeStructureCustomBankPropertyValueEntity(resourceId, customBankPropertyId);

                _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Add(newTreeCustomBankPropertyValue);

                ResetCustomPropertyValuesBeforeEditing();
            }

            foreach (var treeStructureCustomBankPropertySelectedPart in treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection)
            {
                var nodeDepth = 0;
                var treeStructurePartCustomBankPropertyViewModel = FindNode(ConvertTreeStructureSelectedPartToTreeStructurePart(treeStructureCustomBankPropertySelectedPart), Tree.DataValue, ref nodeDepth);

                if (treeStructurePartCustomBankPropertyViewModel != null) treeStructurePartCustomBankPropertyViewModel.IsChecked.DataValue = true;
            }

            NotifyPropertyChanged(HasValidTreePropertySelectionArgs);
        }

        private TreeStructurePartCustomBankPropertyViewModel FindNode(TreeStructurePartCustomBankPropertyEntity nodeToFind, ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel> nodes, ref int nodeDepth)
        {
            foreach (var node in nodes)
            {
                if (node.TreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId == nodeToFind.TreeStructurePartCustomBankPropertyId)
                    return node;
            }

            foreach (var node in nodes)
            {
                nodeDepth += 1;
                var result = FindNode(nodeToFind, node.Children.DataValue, ref nodeDepth);

                if (result == null)
                    continue;
                else
                    return result;
            }

            return null;
        }

        private ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel> GetAllViewModels(ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel> nodes)
        {
            var allViewModels = new List<TreeStructurePartCustomBankPropertyViewModel>();

            if (nodes != null && nodes.Count > 0)
            {
                foreach (var node in nodes)
                {
                    allViewModels.Add(node);
                    allViewModels.AddRange(GetAllViewModels(node.Children.DataValue));
                }
            }

            return new ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel>(allViewModels);
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        void viewAwareStatusService_ViewLoaded()
        {
            _isLoading = true;
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;
                var data = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;

                _itemEditorVm = data;
                _customProperties = _itemEditorVm.CustomBankProperties;
            }

            TreeStructureCustomBankPropertyEntities.DataValue = GetTreeStructureProperties();
            AddCustomPropertyValuesBeforeEditing();

            var treeValue = _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().FirstOrDefault();

            SelectedTreeStructureProperty.DataValue = (treeValue == null ? TreeStructureCustomBankPropertyEntities.DataValue.FirstOrDefault() : TreeStructureCustomBankPropertyEntities.DataValue.FirstOrDefault(x => x.CustomBankPropertyId == treeValue.CustomBankPropertyId));
            _isLoading = false;

            SelectedTreePropertyChangedCallBack();

            NotifyPropertyChanged(HasValidTreePropertySelectionArgs);
        }

        private ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel> CreateTreeStructure(TreeStructureCustomBankPropertyEntity treeStructureCustomBankPropertyEntity)
        {
            var roots = FindRoots();
            return new ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel>(roots.Select(r => new TreeStructurePartCustomBankPropertyViewModel(r)).ToArray());
        }

        private IEnumerable<TreeStructurePartCustomBankPropertyEntity> FindRoots()
        {
            var roots = new List<TreeStructurePartCustomBankPropertyEntity>();
            if (SelectedTreeStructureProperty.DataValue == null || SelectedTreeStructureProperty.DataValue.TreeStructurePartCustomBankPropertyCollection.Count <= 0)
            {
                return roots;
            }

            foreach (var treeStructurePartCustomBankPropertyEntity in SelectedTreeStructureProperty.DataValue.TreeStructurePartCustomBankPropertyCollection)
            {
                var rootTreeStructurePartCustomBankPropertyEntity = treeStructurePartCustomBankPropertyEntity;

                foreach (var treeStructurePartCustomBankPropertyEntity2 in SelectedTreeStructureProperty.DataValue.TreeStructurePartCustomBankPropertyCollection)
                {
                    if (treeStructurePartCustomBankPropertyEntity == treeStructurePartCustomBankPropertyEntity2)
                    {
                        continue;
                    }

                    var child = treeStructurePartCustomBankPropertyEntity2.ChildTreeStructurePartCustomBankPropertyCollection.FirstOrDefault(i => i.ChildTreeStructurePartCustomBankPropertyId == treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId);

                    if (child == null)
                    {
                        continue;
                    }

                    rootTreeStructurePartCustomBankPropertyEntity = null; break;
                }

                if (rootTreeStructurePartCustomBankPropertyEntity != null)
                {
                    roots.Add(rootTreeStructurePartCustomBankPropertyEntity);
                }
            }

            return roots;
        }

        private TreeStructurePartCustomBankPropertyEntity ConvertChildTreeStructurePartToTreeStructurePart(ChildTreeStructurePartCustomBankPropertyEntity childTreeStructurePartCustomBankPropertyEntity)
        {
            return childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.First(x => x.TreeStructurePartCustomBankPropertyId == childTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId);
        }

        private TreeStructurePartCustomBankPropertyEntity ConvertTreeStructureSelectedPartToTreeStructurePart(TreeStructureCustomBankPropertySelectedPartEntity treeStructureCustomBankPropertySelectedPartEntity)
        {
            return SelectedTreeStructureProperty.DataValue.TreeStructurePartCustomBankPropertyCollection.First(x => x.TreeStructurePartCustomBankPropertyId == treeStructureCustomBankPropertySelectedPartEntity.TreeStructurePartId);
        }

        private TreeStructureCustomBankPropertySelectedPartEntity ConvertTreeStructurePartToTreeStructureSelectedPart(TreeStructurePartCustomBankPropertyEntity treeStructurePartCustomBankPropertyEntity, bool throwNotfound = true)
        {
            var treeCustomBankPropertyValue = _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().SingleOrDefault();

            foreach (var treeStructureSelectedPart in SelectedTreeStructureProperty.DataValue.TreeStructurePartCustomBankPropertyCollection)
            {
                var result = treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.FirstOrDefault(x => x.TreeStructurePartId == treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId);
                if (result != null)
                    return result;
            }
            if (throwNotfound) throw new ArgumentException("Could convert TreeStructurePartCustomBankPropertyEntity into TreeStructureCustomBankPropertySelectedPartEntity.");
            return null;
        }


        private List<TreeStructureCustomBankPropertyEntity> GetTreeStructureProperties()
        {
            var treeProperties = _customProperties.OfType<TreeStructureCustomBankPropertyEntity>().ToList();
            var treePropertiesWithChildren = BankFactory.Instance.GetCustomBankProperties(treeProperties.Select(x => x.CustomBankPropertyId).ToList());

            treePropertiesWithChildren.Insert(0, new TreeStructureCustomBankPropertyEntity(Guid.Empty) { Name = Application.Current.FindResource("ItemEditor.ItemTreeStructureViewModel.NoSelection").ToString() });

            return treePropertiesWithChildren.Select(x => (TreeStructureCustomBankPropertyEntity)x).ToList();
        }

        private void AddCustomPropertyValuesBeforeEditing()
        {
            var treeCustomBankPropertyValue = _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().SingleOrDefault();
            if (treeCustomBankPropertyValue != null)
            {
                AddCustomPropertyValuesBeforeEditing(treeCustomBankPropertyValue.CustomBankPropertyId, GetSelectedTreeValues(treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection));
            }
            else if (SelectedTreeStructureProperty?.DataValue != null)
            {
                AddCustomPropertyValuesBeforeEditing(SelectedTreeStructureProperty.DataValue.CustomBankPropertyId, new HashSet<string>());
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
                _customPropertyValuesBeforeEditing.Add(customPropertyId, new Tuple<Guid, HashSet<string>>(_itemEditorVm.ItemResourceEntity.DataValue.ResourceId, valuesBeforeEditing));
            }
        }

        private HashSet<string> GetSelectedTreeValues(EntityCollection<TreeStructureCustomBankPropertySelectedPartEntity> selectedParts)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (TreeStructureCustomBankPropertySelectedPartEntity selectedPart in selectedParts)
            {
                result.Add(selectedPart.TreeStructurePartId.ToString());
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
                if (beforeEditing.Item1 != _itemEditorVm.ItemResourceEntity.DataValue.ResourceId)
                {
                    ResetCustomPropertyValuesBeforeEditing();
                }
            }
        }

        public void DoPreSaveTasks()
        {
            if (SelectedTreeStructureProperty == null || SelectedTreeStructureProperty.DataValue == null)
            {
                return;
            }

            var customBankPropertyId = SelectedTreeStructureProperty.DataValue.CustomBankPropertyId;

            CheckResetCustomPropertyValuesBeforeEditing(customBankPropertyId);

            if (_customPropertyValuesBeforeEditing == null ||
                !_customPropertyValuesBeforeEditing.ContainsKey(customBankPropertyId))
            {
                return;
            }

            var treeCustomBankPropertyValue = _itemEditorVm.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().SingleOrDefault();

            if (treeCustomBankPropertyValue == null)
            {
                return;
            }

            HashSet<string> selectedTreePartsHashset = GetSelectedTreeValues(treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection);
            treeCustomBankPropertyValue.IsDirty = !(_customPropertyValuesBeforeEditing[customBankPropertyId].Item2.SetEquals(selectedTreePartsHashset));

            if (treeCustomBankPropertyValue.IsDirty)
            {
                return;
            }

            if (treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.RemovedEntitiesTracker != null)
            {
                treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.RemovedEntitiesTracker.Clear();
                treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection.RemovedEntitiesTracker = null;
            }

            foreach (TreeStructureCustomBankPropertySelectedPartEntity selectedPart in treeCustomBankPropertyValue.TreeStructureCustomBankPropertySelectedPartCollection)
            {
                selectedPart.IsDirty = false;
                selectedPart.IsNew = false;
            }

            IPredicate filter = (TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId == customBankPropertyId);
            List<int> indexes = _itemEditorVm.CustomPropertiesValueCollectionRemovedEntitiesTracker.FindMatches(filter);
            if (indexes.Count <= 0)
            {
                return;
            }

            foreach (int index in indexes.OrderByDescending(i => i))
            {
                _itemEditorVm.CustomPropertiesValueCollectionRemovedEntitiesTracker.RemoveAt(indexes[index]);
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
            _customProperties = null;
            _customPropertyValuesBeforeEditing = null;

            if (_itemEditorVm != null)
            {
                _itemEditorVm = null;
            }

            base.OnDispose();
        }


    }
}
