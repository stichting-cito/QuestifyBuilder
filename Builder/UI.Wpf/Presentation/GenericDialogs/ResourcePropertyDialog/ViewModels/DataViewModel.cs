using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Text;
using System.Windows;
using Cinch;
using Cito.Tester.Common;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Security;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{
    [ExportViewModel("ResourcePropertyDialog.DataViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class DataViewModel : ViewModelBase
    {

        static readonly PropertyChangedEventArgs FileNameArgs = ObservableHelper.CreateArgs<DataViewModel>(x => x.FileName);
        static readonly PropertyChangedEventArgs ExportSourceEnabledArgs = ObservableHelper.CreateArgs<DataViewModel>(x => x.ExportSourceEnabled);
        static readonly PropertyChangedEventArgs BrowseEnabledArgs = ObservableHelper.CreateArgs<DataViewModel>(x => x.BrowseEnabled);



        public IResourcePropertyDialogObjectFactory ResourcePropertyDialogObjectFactory { get; private set; }

        private readonly IViewAwareStatus _viewAwareStatusService;
        private readonly IOpenFileService _openFileService;
        private readonly ISaveFileService _saveFileService;
        private readonly IMessageBoxService _messageBoxService;
        private IResourcePropertyDialogViewModel _resourcePropertyDialogVM;



        [ImportingConstructor]
        public DataViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            InitDataWrappers();

            InitCommands();

            ResourcePropertyDialogObjectFactory = ViewModelRepository.Instance.Resolver.Container.GetExport<IResourcePropertyDialogObjectFactory>().Value;

            _openFileService = ViewModelRepository.Instance.Resolver.Container.GetExport<IOpenFileService>().Value;
            _saveFileService = ViewModelRepository.Instance.Resolver.Container.GetExport<ISaveFileService>().Value;
            _messageBoxService = ViewModelRepository.Instance.Resolver.Container.GetExport<IMessageBoxService>().Value;

            Mediator.Instance.RegisterHandler<EventArgs>("DataViewModel_ClearFileNameTextBox", (e) => ClearFileName());

            ExportSourceEnabled = new DataWrapper<bool>(this, ExportSourceEnabledArgs);
            BrowseEnabled = new DataWrapper<bool>(this, BrowseEnabledArgs);
        }



        private void ClearFileName()
        {
            FileName.DataValue = string.Empty;
        }

        private void InitDataWrappers()
        {
            FileName = new DataWrapper<string>(this, FileNameArgs);
            FileName.PropertyChanged += FileName_PropertyChanged;
        }

        private void FileName_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _resourcePropertyDialogVM.PathToNewResource = FileName.DataValue;
        }

        private void InitCommands()
        {
            Browse = new SimpleCommand<object, object>(o => DoBrowse());
            ExportSource = new SimpleCommand<object, object>(o => DoExportSource());
        }

        private string DetermineDialogFilter()
        {
            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is TestPackageResourceEntity)
                return (string)Application.Current.FindResource("DataViewModel.TestPackageResourceFilter");
            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is AssessmentTestResourceEntity)
                return (string)Application.Current.FindResource("DataViewModel.TestResourceFilter");
            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is ItemResourceEntity)
                return (string)Application.Current.FindResource("DataViewModel.ItemResourceFilter");
            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is GenericResourceEntity)
                return (string)Application.Current.FindResource("DataViewModel.GenericResourceFilter");
            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is ControlTemplateResourceEntity)
                return (string)Application.Current.FindResource("DataViewModel.ControlTemplateResourceFilter");
            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is ItemLayoutTemplateResourceEntity)
                return (string)Application.Current.FindResource("DataViewModel.ItemLayoutResourceFilter");
            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is AspectResourceEntity)
                return (string)Application.Current.FindResource("DataViewModel.AspectResourceFilter");

            return string.Empty;
        }

        private void DetermineDataImportExport()
        {
            if (_resourcePropertyDialogVM != null && _resourcePropertyDialogVM.PropertyEntity.DataValue is ResourceEntity)
            {
                BrowseEnabled.DataValue = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Import, TestBuilderPermissionTarget.Any, _resourcePropertyDialogVM.PropertyEntity.DataValue.BankId);
                ExportSourceEnabled.DataValue = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.Any, _resourcePropertyDialogVM.PropertyEntity.DataValue.BankId);
            }
        }

        private void DoBrowse()
        {
            _openFileService.Filter = DetermineDialogFilter();
            var result = _openFileService.ShowDialog(null);

            if (!result.HasValue || !result.Value) return;

            FileName.DataValue = _openFileService.FileName;
            _resourcePropertyDialogVM.PropertyEntity.DataValue.IsDirty = true;

            if (_resourcePropertyDialogVM.PropertyEntity.DataValue.GetType().Name == typeof(AssessmentTestResourceEntity).Name)
            {
                GiveIdentifierFieldTheValueOfNameField();
            }
        }

        private void GiveIdentifierFieldTheValueOfNameField()
        {
            var newResourceData = FileHelper.MakeByteArrayFromFile(FileName.DataValue);
            var assessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(newResourceData, true);
            var assessmentTestV2 = assessmentTestModelInfo.AssessmentTestv2;

            if (_resourcePropertyDialogVM.PropertyEntity.DataValue.Name == assessmentTestV2.Identifier) return;

            _resourcePropertyDialogVM.IdentifierAndCodeFieldDiffer = true;
            _messageBoxService.ShowInformation("DataViewModel.IdentifierAndCodeFieldDiffer");

            assessmentTestV2.Identifier = _resourcePropertyDialogVM.PropertyEntity.DataValue.Name;
            byte[] serializedAssessmentTestModel = SerializeHelper.XmlSerializeToByteArray(assessmentTestV2);

            _resourcePropertyDialogVM.PropertyEntity.DataValue.ResourceData.BinData = null;
            _resourcePropertyDialogVM.PropertyEntity.DataValue.ResourceData.BinData = serializedAssessmentTestModel;
        }

        private void DoExportSource()
        {
            var shouldLog = DetermineIfShouldLog();

            var resourceType = string.Empty;
            var defaultResourceFileName = FileName.DataValue;
            if (string.IsNullOrEmpty(defaultResourceFileName))
            {
                defaultResourceFileName = GetResourceFileName();
            }

            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is AssessmentTestResourceEntity)
            {
                resourceType = Properties.Resources.ResourceTypeTests;
            }
            else if (_resourcePropertyDialogVM.PropertyEntity.DataValue is ItemResourceEntity)
            {
                resourceType = Properties.Resources.ResourceTypeItems;
            }
            else if (_resourcePropertyDialogVM.PropertyEntity.DataValue is TestPackageResourceEntity)
            {
                resourceType = Properties.Resources.ResourceTypeTestPackages;
            }
            else if (_resourcePropertyDialogVM.PropertyEntity.DataValue is GenericResourceEntity)
            {
                if (!string.IsNullOrWhiteSpace(((GenericResourceEntity)_resourcePropertyDialogVM.PropertyEntity.DataValue).MediaType))
                {
                    shouldLog = true;
                    resourceType = Properties.Resources.ResourceTypeMedia;
                }
            }
            else if (_resourcePropertyDialogVM.PropertyEntity.DataValue is DataSourceResourceEntity)
            {
                if (((DataSourceResourceEntity)_resourcePropertyDialogVM.PropertyEntity.DataValue).ResourceType.IndexOf("selection", 0, StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    shouldLog = true;
                    resourceType = Properties.Resources.ResourceTypeSelections;
                }
            }

            _saveFileService.Filter = DetermineDialogFilter();
            _saveFileService.FileName = defaultResourceFileName;

            var result = _saveFileService.ShowDialog(null);

            if (result.HasValue && result.Value)
            {
                var hasNoData = _resourcePropertyDialogVM.PropertyEntity.DataValue.ResourceData == null;

                if (hasNoData)
                {
                    _resourcePropertyDialogVM.PropertyEntity.DataValue.ResourceData = new ResourceDataEntity
                    {
                        BinData = ResourcePropertyDialogObjectFactory.GetBinData(_resourcePropertyDialogVM.PropertyEntity.DataValue.Id)
                    };
                }

                FileHelper.MakeFileFromByteArray(_saveFileService.FileName, _resourcePropertyDialogVM.PropertyEntity.DataValue.ResourceData.BinData);

                _messageBoxService.ShowInformation((string)Application.Current.FindResource("DataViewModel.ResourceExported"));

                if (hasNoData)
                {
                    _resourcePropertyDialogVM.PropertyEntity.DataValue.ResourceData = null;
                }
            }
        }

        private string GetResourceFileName()
        {
            string name = _resourcePropertyDialogVM.PropertyEntity.DataValue.Name;
            string extension = "xml";

            if (_resourcePropertyDialogVM.PropertyEntity.DataValue is GenericResourceEntity)
            {
                extension = string.Empty;
            }
            else if (_resourcePropertyDialogVM.PropertyEntity.DataValue is ControlTemplateResourceEntity)
            {
                extension = "code";
            }
            else if (_resourcePropertyDialogVM.PropertyEntity.DataValue is ItemLayoutTemplateResourceEntity)
            {
                extension = "xhtml";
            }
            else if (_resourcePropertyDialogVM.PropertyEntity.DataValue is AspectResourceEntity)
            {
                extension = "css";
            }

            string result = name;
            if (!string.IsNullOrEmpty(extension))
            {
                result = $"{name}.{extension}";
            }
            return result;
        }

        private bool DetermineIfShouldLog()
        {
            bool result = (_resourcePropertyDialogVM.PropertyEntity.DataValue is AssessmentTestResourceEntity)
                          || _resourcePropertyDialogVM.PropertyEntity.DataValue is ItemResourceEntity
                          || _resourcePropertyDialogVM.PropertyEntity.DataValue is TestPackageResourceEntity;
            return result;
        }

        private string GetFullBankPath(BankEntity bank)
        {
            if (bank == null)
            {
                return string.Empty;
            }

            var bankPathBuilder = new StringBuilder();
            bankPathBuilder.Append(bank.Name);

            var bnk = bank;
            while (bnk.ParentBankId.HasValue)
            {
                bnk = BankFactory.Instance.GetBank(bnk.ParentBankId.Value);
                bankPathBuilder.Insert(0, string.Concat(bnk.Name, " -> "));
            }

            return bankPathBuilder.ToString();
        }

        private void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;

                _resourcePropertyDialogVM = (IResourcePropertyDialogViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                DetermineDataImportExport();
            }
        }



        public DataWrapper<String> FileName { get; private set; }

        public SimpleCommand<object, object> Browse { get; private set; }
        public SimpleCommand<object, object> ExportSource { get; private set; }
        public DataWrapper<bool> ExportSourceEnabled { get; set; }
        public DataWrapper<bool> BrowseEnabled { get; set; }

    }
}