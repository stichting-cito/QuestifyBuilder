using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.Factories;

namespace Questify.Builder.UI.Wpf.Presentation.AnnouncementDialog.ViewModels
{
    [ExportViewModel("AnnouncementDialog.SendAnnouncementViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class SendAnnouncementViewModel : ViewModelBase
    {
        static SendAnnouncementViewModel()
        {
        }

        static readonly PropertyChangedEventArgs LoginPermissionEndTimeArgs = ObservableHelper.CreateArgs<SendAnnouncementViewModel>(x => x.LoginPermissionEndTime);

        private void DoSendAnnouncement()
        {
            DateTime loginPermission = (LoginPermissionEndTime.DataValue < new DateTime(1900, 1, 1)
                ? new DateTime(1900, 1, 1)
                : LoginPermissionEndTime.DataValue);
            AuthorizationFactory.Instance.SetMaintenanceWindow(loginPermission);
        }

        private bool CanSendAnnouncement()
        {
            return !HasErrors;
        }

        private void WithDrawAnnouncement()
        {
            AuthorizationFactory.Instance.SetMaintenanceWindow(new DateTime(1900, 1, 1));
        }

        private readonly IViewAwareStatus _viewAwareStatusService;
        private IAnnouncementViewModel _announcementDialogViewModel;

        public DataWrapper<DateTime> LoginPermissionEndTime { get; private set; }

        [ImportingConstructor]
        public SendAnnouncementViewModel(IViewAwareStatus viewAwareStatusService, IMessageBoxService messageBoxService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            InitProperties();
            InitCommands();
        }
        private void InitProperties()
        {
            LoginPermissionEndTime = new DataWrapper<DateTime>(this, LoginPermissionEndTimeArgs);
            DateTime? loginPermissionDefaultEndTime = AuthorizationFactory.Instance.GetMaintenanceWindow();
            if (loginPermissionDefaultEndTime.HasValue)
            {
                LoginPermissionEndTime.DataValue = loginPermissionDefaultEndTime.Value;
            }
            else
            {
                LoginPermissionEndTime.DataValue = DateTime.Now;
            }
        }

        private void InitCommands()
        {
            SendAnnouncement = new SimpleCommand<object, object>(o => CanSendAnnouncement(), o => DoSendAnnouncement());
            SendRenouncement = new SimpleCommand<object, object>(o => WithDrawAnnouncement());
        }

        public SimpleCommand<object, object> SendAnnouncement { get; private set; }
        public SimpleCommand<object, object> SendRenouncement { get; private set; }

        public bool HasErrors
        {
            get { return !LoginPermissionEndTime.IsValid; }
        }

        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                _announcementDialogViewModel = ((IWorkSpaceAware)_viewAwareStatusService.View).WorkSpaceContextualData.DataValue as IAnnouncementViewModel;
            }
        }
    }
}
