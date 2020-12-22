using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using Cinch;
using Cito.Tester.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Versioning;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{
    [ExportViewModel("ResourcePropertyDialog.VersionDifferencesViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class VersionDifferencesViewModel : ViewModelBase
    {

        static readonly PropertyChangedEventArgs MetaDataCompareResultArgs = ObservableHelper.CreateArgs<VersionDifferencesViewModel>(x => x.VersionDifferences);
        static readonly PropertyChangedEventArgs OldValueColumnTitleArgs = ObservableHelper.CreateArgs<VersionDifferencesViewModel>(x => x.OldValueColumnTitle);
        static readonly PropertyChangedEventArgs NewValueColumnTitleArgs = ObservableHelper.CreateArgs<VersionDifferencesViewModel>(x => x.NewValueColumnTitle);




        public DataWrapper<List<MetaDataCompareResult>> VersionDifferences { get; private set; }
        public DataWrapper<string> OldValueColumnTitle { get; private set; }
        public DataWrapper<string> NewValueColumnTitle { get; private set; }



        public SimpleCommand<object, object> Ok { get; private set; }


        [ImportingConstructor]
        public VersionDifferencesViewModel(MetaDataCompareResult metaDataCompareResult, string oldVersion, string newVersion)
        {
            InitDataWrappers();
            InitCommands();

            VersionDifferences.DataValue = new List<MetaDataCompareResult>() { metaDataCompareResult };

            InitGroupDescription();

            OldValueColumnTitle.DataValue = String.Format("{0} {1}", Application.Current.FindResource("VersionDifferences.ListView.Column.OldVersionColumnHeaderPrefix").ToString(), oldVersion);
            NewValueColumnTitle.DataValue = String.Format("{0} {1}", Application.Current.FindResource("VersionDifferences.ListView.Column.OldVersionColumnHeaderPrefix").ToString(), newVersion);
        }

        [ImportingConstructor]
        public VersionDifferencesViewModel(ResourceHistoryEntity resourceHistoryEntity1, ResourceHistoryEntity resourceHistoryEntity2, Type type, ResourceManagerBase resourceManager)
        {
            InitDataWrappers();
            InitCommands();

            VersionDifferences.DataValue = new List<MetaDataCompareResult>();
            VersionDifferences.DataValue.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, type, resourceManager));

            InitGroupDescription();

            OldValueColumnTitle.DataValue = String.Format("{0} {1}", Application.Current.FindResource("VersionDifferences.ListView.Column.OldVersionColumnHeaderPrefix").ToString(), resourceHistoryEntity1.Version);
            NewValueColumnTitle.DataValue = String.Format("{0} {1}", Application.Current.FindResource("VersionDifferences.ListView.Column.OldVersionColumnHeaderPrefix").ToString(), resourceHistoryEntity2.Version);
        }

        private void InitGroupDescription()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(VersionDifferences.DataValue);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void InitDataWrappers()
        {
            VersionDifferences = new DataWrapper<List<MetaDataCompareResult>>(this, MetaDataCompareResultArgs);
            OldValueColumnTitle = new DataWrapper<string>(this, OldValueColumnTitleArgs);
            NewValueColumnTitle = new DataWrapper<string>(this, OldValueColumnTitleArgs);
        }

        private void InitCommands()
        {
            Ok = new SimpleCommand<object, object>(o => RaiseCloseRequest(true));
        }

        private void CanClose(EventToCommandArgs e)
        {
            ((CancelEventArgs)e.EventArgs).Cancel = false;
        }

    }
}
