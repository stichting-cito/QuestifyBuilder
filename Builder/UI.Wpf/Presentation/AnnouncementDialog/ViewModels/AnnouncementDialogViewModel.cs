using System;
using System.ComponentModel;
using Cinch;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.AnnouncementDialog.ViewModels
{
    internal class AnnouncementDialogViewModel : ViewModelBase, IAnnouncementViewModel
    {
        static readonly PropertyChangedEventArgs SelectedTabArgs = ObservableHelper.CreateArgs<AnnouncementDialogViewModel>(x => x.SelectedTab);
        static readonly PropertyChangedEventArgs SendAnnouncementWorkspaceArgs = ObservableHelper.CreateArgs<AnnouncementDialogViewModel>(x => x.SendAnnouncementWorkspace);
        static readonly PropertyChangedEventArgs MaintenanceControlCommandsTabVisibleArgs = ObservableHelper.CreateArgs<AnnouncementDialogViewModel>(x => x.SendAnnouncementTabVisible);

        public IAnnouncementService AnnouncementService { get; private set; }
        public ICustomMessageBoxService CustomMessageBoxService { get; private set; }

        public AnnouncementDialogViewModel()
        {
            InitDataWrappers();

            InitCommands();

            CustomMessageBoxService = ViewModelRepository.Instance.Resolver.Container.GetExport<ICustomMessageBoxService>().Value;
            AnnouncementService = ViewModelRepository.Instance.Resolver.Container.GetExport<IAnnouncementService>().Value;

            SetTabsVisibility();
        }
        private void InitCommands()
        {
            Close = new SimpleCommand<object, object>(o => DoCancel());
        }

        private void InitDataWrappers()
        {
            SelectedTab = new DataWrapper<int>(this, SelectedTabArgs);
            SendAnnouncementTabVisible = new DataWrapper<bool>(this, MaintenanceControlCommandsTabVisibleArgs);
            SendAnnouncementWorkspace = new DataWrapper<WorkspaceData>(this, SendAnnouncementWorkspaceArgs) { DataValue = new WorkspaceData(null, Constants.SendAnnouncementWorkspace, this, String.Empty, false) };
        }

        public DataWrapper<string> WindowTitle { get; set; }
        public DataWrapper<int> SelectedTab { get; private set; }
        public DataWrapper<bool> SendAnnouncementTabVisible { get; private set; }
        public DataWrapper<WorkspaceData> SendAnnouncementWorkspace { get; private set; }

        public SimpleCommand<object, object> Close { get; private set; }

        private void DoCancel()
        {
            RaiseCloseRequest(true);
        }

        internal void SetTabsVisibility()
        {
            SendAnnouncementTabVisible.DataValue = true;
        }
    }
}
