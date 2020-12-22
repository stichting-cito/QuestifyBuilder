using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.LinkedStyleSheetsWorkSpace, typeof(LinkedStyleSheets))]
    public partial class LinkedStyleSheets : UserControl, IWorkSpaceAware
    {



        public LinkedStyleSheets()
        {
            InitializeComponent();

        }



        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(LinkedStyleSheets),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }



        private void stylesheetslv_Initialized(object sender, EventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(stylesheetslv.ItemsSource);

            if (view.CanSort)
            {
                view.SortDescriptions.Add(new SortDescription("DependentResource.Name", ListSortDirection.Ascending));
            }
        }
    }
}
