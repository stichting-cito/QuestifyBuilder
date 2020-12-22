using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using Cinch;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.VersionDialog.ViewModels
{
    internal class MajorVersionDialogViewModel : ViewModelBase, IMajorVersionDialogViewModel
    {
        private readonly IVersionable _versionableResource;


        static readonly PropertyChangedEventArgs RemarksChangedEventArgs = ObservableHelper.CreateArgs<MajorVersionDialogViewModel>(x => x.Label);



        public ICustomMessageBoxService CustomMessageBoxService { get; private set; }
        public IMajorVersionDialogService MajorVersionDialogService { get; private set; }

        public DataWrapper<string> Label { get; private set; }



        private static readonly SimpleRule RemarkRequiredStringFieldRule;



        [ImportingConstructor]
        public MajorVersionDialogViewModel(IVersionable versionableResource)
        {
            _versionableResource = versionableResource;

            InitDataWrappers();

            InitCommands();
        }

        static MajorVersionDialogViewModel()
        {
            RemarkRequiredStringFieldRule = new SimpleRule("DataValue", Application.Current.FindResource("MajorVersionDialog.MajorVersionDialogViewModel.Remark.Required").ToString(),
                 domainObject =>
                 {
                     var obj = (DataWrapper<String>)domainObject;
                     return string.IsNullOrEmpty(obj.DataValue);
                 });
        }



        private void InitCommands()
        {
            OkCommand = new SimpleCommand<object, object>(o => CanOk(), o => DoOk());
            CancelCommand = new SimpleCommand<object, object>(o => DoCancel());
        }

        private bool CanOk()
        {
            return Label.IsValid;
        }

        private void InitDataWrappers()
        {
            Label = new DataWrapper<string>(this, RemarksChangedEventArgs) { DataValue = _versionableResource.MajorVersionLabel };

        }

        private void DoOk()
        {
            if (Save())
            {
                RaiseCloseRequest(true);
            }
        }

        private void DoCancel()
        {
            RaiseCloseRequest(false);
        }

        private bool Save()
        {
            _versionableResource.MajorVersionLabel = Label.DataValue;
            _versionableResource.Version = GetNewVersion();

            return true;
        }

        private string GetNewVersion()
        {
            Version version = null;
            string versionString = _versionableResource.Version;
            if (string.IsNullOrEmpty(versionString))
            {
                versionString = "0.1";
            }
            var success = Version.TryParse(versionString, out version);

            if (success)
                return String.Format("{0}", version.Major + 1);
            throw new ArgumentException("VersionableEntity.Version does not contain a valid versionnumber.");
        }



        public SimpleCommand<object, object> OkCommand { get; private set; }
        public SimpleCommand<object, object> CancelCommand { get; private set; }


    }
}
