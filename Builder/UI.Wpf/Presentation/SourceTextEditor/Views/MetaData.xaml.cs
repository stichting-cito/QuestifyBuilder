using System.Windows;
using System.Windows.Controls;
using Cinch;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.MetadataWorkSpace, typeof(MetaData))]
    public partial class MetaData : UserControl, IMetaDataControl
    {

        ResourceEntity _resourceEntity;



        public MetaData()
        {
            InitializeComponent();
        }



        public void Update(ResourceEntity resourceEntity)
        {
            _resourceEntity = resourceEntity;
            ResourceCustomPropsCtrl.ResourceEntity = _resourceEntity;
        }

        public void PreSaveTasks()
        {
            ResourceCustomPropsCtrl.Validate();

            if (ResourceCustomPropsCtrl.RemovedEntities.Count > 0)
            {
                if (_resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker == null)
                {
                    _resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker = new Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection();
                }
                else
                {
                    _resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Clear();
                }

                _resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.AddRange(ResourceCustomPropsCtrl.RemovedEntities);
            }
        }



        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(MetaData),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
