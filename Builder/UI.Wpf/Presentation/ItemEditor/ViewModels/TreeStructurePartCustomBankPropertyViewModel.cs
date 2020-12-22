using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [ExportViewModel("ItemEditor.TreeStructurePartCustomBankPropertyVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TreeStructurePartCustomBankPropertyViewModel : ViewModelBase
    {

        private TreeStructurePartCustomBankPropertyEntity _treeStructurePartCustomBankPropertyEntity; private TreeStructurePartCustomBankPropertyViewModel _parentTreeStructurePartCustomBankPropertyViewModel;



        static readonly PropertyChangedEventArgs ChildrenArgs = ObservableHelper.CreateArgs<TreeStructurePartCustomBankPropertyViewModel>(x => x.Children);
        static readonly PropertyChangedEventArgs IsSelectedArgs = ObservableHelper.CreateArgs<TreeStructurePartCustomBankPropertyViewModel>(x => x.IsSelected);
        static readonly PropertyChangedEventArgs IsCheckedArgs = ObservableHelper.CreateArgs<TreeStructurePartCustomBankPropertyViewModel>(x => x.IsChecked);
        static readonly PropertyChangedEventArgs IsExpandedArgs = ObservableHelper.CreateArgs<TreeStructurePartCustomBankPropertyViewModel>(x => x.IsExpanded);
        static readonly PropertyChangedEventArgs NameArgs = ObservableHelper.CreateArgs<TreeStructurePartCustomBankPropertyViewModel>(x => x.Name);



        public TreeStructurePartCustomBankPropertyViewModel(TreeStructurePartCustomBankPropertyEntity rootTreeStructurePartCustomBankPropertyEntity)
    : this(rootTreeStructurePartCustomBankPropertyEntity, null)
        {
        }

        private TreeStructurePartCustomBankPropertyViewModel(TreeStructurePartCustomBankPropertyEntity treeStructurePartCustomBankPropertyEntity, TreeStructurePartCustomBankPropertyViewModel parent)
        {
            if (treeStructurePartCustomBankPropertyEntity != null)
            {
                _treeStructurePartCustomBankPropertyEntity = treeStructurePartCustomBankPropertyEntity;
                _parentTreeStructurePartCustomBankPropertyViewModel = parent;

                Children = new DataWrapper<ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel>>(this, ChildrenArgs);
                IsSelected = new DataWrapper<bool>(this, IsSelectedArgs);
                IsChecked = new DataWrapper<bool>(this, IsCheckedArgs);
                IsExpanded = new DataWrapper<bool>(this, IsExpandedArgs);
                Name = new DataWrapper<string>(this, NameArgs);
                Name.DataValue = string.Format("{0} - {1}", _treeStructurePartCustomBankPropertyEntity.Name, _treeStructurePartCustomBankPropertyEntity.Title);

                Children.DataValue = GetChildren(treeStructurePartCustomBankPropertyEntity);
            }
        }



        public DataWrapper<ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel>> Children { get; private set; }
        public DataWrapper<bool> IsSelected { get; set; }
        public DataWrapper<bool> IsChecked { get; set; }
        public DataWrapper<bool> IsExpanded { get; set; }
        public DataWrapper<string> Name { get; private set; }

        public int VisualOrder { get; set; }

        public TreeStructurePartCustomBankPropertyViewModel Parent
        {
            get { return _parentTreeStructurePartCustomBankPropertyViewModel; }
        }

        public TreeStructurePartCustomBankPropertyEntity TreeStructurePartCustomBankPropertyEntity
        {
            get { return _treeStructurePartCustomBankPropertyEntity; }
        }


        private ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel> GetChildren(TreeStructurePartCustomBankPropertyEntity treeStructurePartCustomBankPropertyEntity)
        {
            var children = new List<TreeStructurePartCustomBankPropertyViewModel>();

            foreach (var child in treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.OrderBy(x => x.VisualOrder))
            {
                var convertedTreeStructurePart = ConvertChildTreeStructurePartCustomBankPropertyEntityToTreeStructurePartCustomBankPropertyEntity(child);

                if (convertedTreeStructurePart != null) children.Add(new TreeStructurePartCustomBankPropertyViewModel(convertedTreeStructurePart, this));
            }

            return new ReadOnlyCollection<TreeStructurePartCustomBankPropertyViewModel>(children);
        }

        private TreeStructurePartCustomBankPropertyEntity ConvertChildTreeStructurePartCustomBankPropertyEntityToTreeStructurePartCustomBankPropertyEntity(ChildTreeStructurePartCustomBankPropertyEntity childTreeStructurePartCustomBankPropertyEntity)
        {
            return childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.FirstOrDefault(x => x.TreeStructurePartCustomBankPropertyId == childTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId);
        }


    }
}
