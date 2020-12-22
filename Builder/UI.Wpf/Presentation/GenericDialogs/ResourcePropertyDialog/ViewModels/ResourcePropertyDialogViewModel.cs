using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Cinch;
using Cito.Tester.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.GenericControls.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.Security;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{
    internal class ResourcePropertyDialogViewModel : ViewModelBase, IResourcePropertyDialogViewModel
    {

        static readonly PropertyChangedEventArgs PropertyIdArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.PropertyId);
        static readonly PropertyChangedEventArgs SelectedTabArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.SelectedTab);
        static readonly PropertyChangedEventArgs PropertyEntityEventArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.PropertyEntity);
        static readonly PropertyChangedEventArgs GeneralWorkspaceArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.GeneralWorkspace);
        static readonly PropertyChangedEventArgs DependenciesWorkspaceArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.DependenciesWorkspace);
        static readonly PropertyChangedEventArgs ReferencesWorkspaceArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.ReferencesWorkspace);
        static readonly PropertyChangedEventArgs DataWorkspaceArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.DataWorkspace);
        static readonly PropertyChangedEventArgs HistoryWorkspaceArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.HistoryWorkspace);
        static readonly PropertyChangedEventArgs ContextIdentifierEventArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.ContextIdentifier);
        static readonly PropertyChangedEventArgs ResourceManagerArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.ResourceManager);

        static readonly PropertyChangedEventArgs GeneralTabVisibleArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.GeneralTabVisible);
        static readonly PropertyChangedEventArgs DependenciesTabVisibleArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.DependenciesTabVisible);
        static readonly PropertyChangedEventArgs ReferencesTabVisibleArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.ReferencesTabVisible);
        static readonly PropertyChangedEventArgs DataTabVisibleArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.DataTabVisible);
        static readonly PropertyChangedEventArgs HistoryTabVisibleArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.HistoryTabVisible);
        static readonly PropertyChangedEventArgs WindowTitleChangedEventArgs = ObservableHelper.CreateArgs<ResourcePropertyDialogViewModel>(x => x.WindowTitle);



        private string _msgPendingChangesWishToLeave;
        private string _msgPleaseConfirm;
        private string _msgErrorCaption;
        private string _msgSaveError;

        public string PathToNewResource { get; set; }
        public bool IdentifierAndCodeFieldDiffer { get; set; }
        public IResourcePropertyDialogObjectFactory ResourcePropertyDialogObjectFactory { get; private set; }
        public IResourcePropertyDialogService ResourcePropertyDialogService { get; private set; }
        public ICustomMessageBoxService CustomMessageBoxService { get; private set; }
        public Type PropertyType { get; set; }


        public ResourcePropertyDialogViewModel()
        {
            InitDataWrappers();

            InitCommands();

            CustomMessageBoxService = ViewModelRepository.Instance.Resolver.Container.GetExport<ICustomMessageBoxService>().Value;
            ResourcePropertyDialogService = ViewModelRepository.Instance.Resolver.Container.GetExport<IResourcePropertyDialogService>().Value;
            ResourcePropertyDialogObjectFactory = ViewModelRepository.Instance.Resolver.Container.GetExport<IResourcePropertyDialogObjectFactory>().Value;

            GetStrings();
        }

        private void GetStrings()
        {
            const string prefix = "ResourcePropertyDialog.ResourcePropertyDialogViewModel.";
            var app = Application.Current;

            _msgErrorCaption = (string)app.FindResource("Dialog.ErrorCaption");
            _msgPleaseConfirm = (string)app.FindResource("Dialog.PleaseConfirmCaption");
            _msgPendingChangesWishToLeave = (string)app.FindResource(prefix + "PendingChangesWishToLeave");
            _msgSaveError = (string)app.FindResource("ResourcePropertyDialog.Command.Ok.Error");
        }

        private void InitCommands()
        {
            WindowClosing = new SimpleCommand<object, EventToCommandArgs>(CanClose);
            Ok = new SimpleCommand<object, object>(o => CanDoOk(), o => DoOk());
            Cancel = new SimpleCommand<object, object>(o => DoCancel());
            Apply = new SimpleCommand<object, object>(o => CanDoApply(), o => DoApply());
        }

        private void InitDataWrappers()
        {
            SelectedTab = new DataWrapper<int>(this, SelectedTabArgs);
            PropertyEntity = new DataWrapper<IPropertyEntity>(this, PropertyEntityEventArgs);
            GeneralTabVisible = new DataWrapper<bool>(this, GeneralTabVisibleArgs);
            DependenciesTabVisible = new DataWrapper<bool>(this, DependenciesTabVisibleArgs);
            ReferencesTabVisible = new DataWrapper<bool>(this, ReferencesTabVisibleArgs);
            DataTabVisible = new DataWrapper<bool>(this, DataTabVisibleArgs);
            HistoryTabVisible = new DataWrapper<bool>(this, HistoryTabVisibleArgs);
            ResourceManager = new DataWrapper<ResourceManagerBase>(this, ResourceManagerArgs);
            ContextIdentifier = new DataWrapper<int?>(this, ContextIdentifierEventArgs);

            PropertyId = new DataWrapper<Guid>(this, PropertyIdArgs); PropertyId.PropertyChanged += (s, e) => HandlePropertyIdChanged();

            GeneralWorkspace = new DataWrapper<WorkspaceData>(this, GeneralWorkspaceArgs) { DataValue = new WorkspaceData(null, GenericControls.Constants.MetadataWorkSpace, this, "", false) };
            DependenciesWorkspace = new DataWrapper<WorkspaceData>(this, DependenciesWorkspaceArgs) { DataValue = new WorkspaceData(null, Constants.DependenciesWorkSpace, this, "", false) };
            ReferencesWorkspace = new DataWrapper<WorkspaceData>(this, ReferencesWorkspaceArgs) { DataValue = new WorkspaceData(null, Constants.ReferencesWorkSpace, this, "", false) };
            DataWorkspace = new DataWrapper<WorkspaceData>(this, DataWorkspaceArgs) { DataValue = new WorkspaceData(null, Constants.DataWorkSpace, this, "", false) };
            HistoryWorkspace = new DataWrapper<WorkspaceData>(this, HistoryWorkspaceArgs) { DataValue = new WorkspaceData(null, Constants.HistoryWorkSpace, this, "", false) };

            WindowTitle = new DataWrapper<string>(this, WindowTitleChangedEventArgs);
        }

        private string GetWindowTitle()
        {
            return string.Format(Application.Current.FindResource("ResourcePropertyDialog.ResourcePropertyDialogViewModel.PropertiesOf").ToString(), PropertyEntity.DataValue.Name);
        }


        public DataWrapper<Guid> PropertyId { get; private set; }
        public DataWrapper<string> WindowTitle { get; set; }
        public DataWrapper<int> SelectedTab { get; private set; }
        public DataWrapper<bool> GeneralTabVisible { get; private set; }
        public DataWrapper<bool> DependenciesTabVisible { get; private set; }
        public DataWrapper<bool> ReferencesTabVisible { get; private set; }
        public DataWrapper<bool> DataTabVisible { get; private set; }
        public DataWrapper<bool> HistoryTabVisible { get; private set; }
        public DataWrapper<IPropertyEntity> PropertyEntity { get; private set; }
        public DataWrapper<WorkspaceData> GeneralWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> DependenciesWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> ReferencesWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> DataWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> HistoryWorkspace { get; private set; }
        public DataWrapper<ResourceManagerBase> ResourceManager { get; private set; }
        public DataWrapper<int?> ContextIdentifier { get; private set; }



        public SimpleCommand<object, EventToCommandArgs> WindowClosing { get; private set; }
        public SimpleCommand<object, object> Ok { get; private set; }
        public SimpleCommand<object, object> Cancel { get; private set; }
        public SimpleCommand<object, object> Apply { get; private set; }


        private bool CanDoOk()
        {
            return HasEditPermission;
        }

        private bool CanDoApply()
        {
            return HasEditPermission;
        }

        private void DoOk()
        {
            if (HasChanges)
            {
                if (Save())
                {
                    RaiseCloseRequest(true);
                }
            }
            else
            {
                RaiseCloseRequest(true);
            }
        }

        private void DoCancel()
        {
            RaiseCloseRequest(false);
        }

        private void DoApply()
        {
            if (HasChanges)
            {
                Save();
            }
        }

        private bool Save()
        {
            if (HasErrors)
            {
                CustomMessageBoxService.ShowError(Application.Current.FindResource("ResourcePropertyDialog.ResourcePropertyDialogViewModel.HasErrors").ToString(), string.Empty);
                return false;
            }

            var entity = ResourceToEditMetadataFor as ResourceEntity;
            if (entity != null && entity.RequiresMajorVersionIncrement() && !IncrementMajorVersion(entity))
            {
                return false;
            }

            var result = ResourcePropertyDialogObjectFactory.SaveResourcePropertyDialog(PropertyEntity.DataValue, PathToNewResource, IdentifierAndCodeFieldDiffer);

            Mediator.Instance.NotifyColleagues("DataViewModel_ClearFileNameTextBox", new EventArgs());
            Mediator.Instance.NotifyColleagues("ResourceEditor_RefreshGridAndSelectResource", new EventArgs<IPropertyEntity>(PropertyEntity.DataValue));

            if (!string.IsNullOrEmpty(result))
            {
                CustomMessageBoxService.ShowError(string.Format(_msgSaveError, result), _msgErrorCaption);
                return false;
            }
            else
            {
                PropertyEntity.DataValue.RemovedDependentEntities
                    .Clear();
            }

            return true;
        }

        private static bool IncrementMajorVersion(ResourceEntity entity)
        {
            var dialogService = ViewModelRepository.Instance.Resolver.Container.GetExport<IMajorVersionDialogService>().Value;
            return dialogService.Show(entity);
        }

        private void CanClose(EventToCommandArgs e)
        {
            var args = ((CancelEventArgs)e.EventArgs);
            args.Cancel = HasEditPermission && StopBeforeLoosePendingChanges();

            if (!args.Cancel && ResourceManager.DataValue != null)
            {
                TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(ResourceManager.DataValue);
            }
        }

        internal void HandlePropertyIdChanged()
        {
            if (PropertyType == null)
            {
                throw new ArgumentException("PropertyType cannot be null!");
            }

            DoActualLoadOnResourceId(PropertyId.DataValue, PropertyType);

            SetTabsVisibility();
        }

        internal void DoActualLoadOnResourceId(Guid id, Type type)
        {
            try
            {
                PropertyEntity.DataValue =
                    ResourcePropertyDialogObjectFactory.GetRequiredObjectsForPropertyEntityWithId(id, type);
                ResourceManager.DataValue =
                    ResourcePropertyDialogObjectFactory.GetResourceManager(PropertyEntity.DataValue.BankId);

                if (ResourceManager.DataValue == null)
                {
                    TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(ResourceManager.DataValue);
                    ContextIdentifier.DataValue =
                        TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(ResourceManager.DataValue);
                }

                WindowTitle.DataValue = GetWindowTitle();

                Debug.Assert(PropertyEntity.DataValue != null,
                    string.Format("Failed to find entity of type {0} and id '{1}'", type.Name, id.ToString()));
            }
            catch (Exception ex)
            {
                CustomMessageBoxService.ShowError(ex.Message, _msgErrorCaption);
            }

            ValidateType();
        }

        private void ValidateType()
        {
            if (PropertyEntity.DataValue == null)
            {
                throw new ArgumentException("Unsupported type: " + PropertyEntity.GetType());
            }
        }

        internal void SetTabsVisibility()
        {
            GeneralTabVisible.DataValue = true;
            ReferencesTabVisible.DataValue = true;
            HistoryTabVisible.DataValue = true;

            if (PropertyEntity.DataValue is ResourceEntity)
            {
                DependenciesTabVisible.DataValue = true;
                DataTabVisible.DataValue = true;
            }
            else if (PropertyEntity.DataValue is CustomBankPropertyEntity)
            {
                DependenciesTabVisible.DataValue = false;
                DataTabVisible.DataValue = false;
                HistoryTabVisible.DataValue = false;
            }
            else
            {
                throw new ArgumentException("This type is not supported: " + PropertyEntity.GetType());
            }
        }

        private bool StopBeforeLoosePendingChanges()
        {
            if (HasChanges)
            {
                var result = CustomMessageBoxService.ShowYesNoCancel(_msgPendingChangesWishToLeave, _msgPleaseConfirm,
                    CustomDialogIcons.Exclamation);

                if (result == CustomDialogResults.Yes)
                {
                    if (HasErrors)
                    {
                        CustomMessageBoxService.ShowError(
                            Application.Current
                                .FindResource("ResourcePropertyDialog.ResourcePropertyDialogViewModel.HasErrors")
                                .ToString(), string.Empty);
                        return true;
                    }

                    Save();

                    return false;
                }

                if (result == CustomDialogResults.No)
                {
                    return false;
                }

                if (result == CustomDialogResults.Cancel)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool HasErrors
        {
            get { return GeneralWorkspace.DataValue.ViewModelInstance != null && ((MetaDataViewModel)GeneralWorkspace.DataValue.ViewModelInstance).HasErrors; }
        }

        public bool HasChanges
        {
            get { return (PropertyEntity.DataValue != null && (PropertyEntity.DataValue.IsDirty || PropertyEntity.DataValue.HasChangesInTopology() || IsDependentResourceCollectionChanged())); }
        }

        public bool HasEditPermission
        {
            get
            {
                var d = new Dictionary<Type, TestBuilderPermissionTarget>
                {
                    {typeof(ItemResourceEntity), TestBuilderPermissionTarget.ItemEntity},
                    {typeof(DataSourceResourceEntity), TestBuilderPermissionTarget.DataSourceEntity},
                    {typeof(AssessmentTestResourceEntity), TestBuilderPermissionTarget.TestEntity},
                    {typeof(TestPackageResourceEntity), TestBuilderPermissionTarget.TestPackageEntity},
                    {typeof(AspectResourceEntity), TestBuilderPermissionTarget.AspectEntity},
                    {typeof(GenericResourceEntity), TestBuilderPermissionTarget.MediaEntity},
                    {typeof(CustomBankPropertyEntity), TestBuilderPermissionTarget.CustomBankPropertyEntity},
                    {typeof(ConceptStructureCustomBankPropertyEntity), TestBuilderPermissionTarget.CustomBankPropertyEntity},
                    {typeof(FreeValueCustomBankPropertyEntity), TestBuilderPermissionTarget.CustomBankPropertyEntity},
                    {typeof(ListCustomBankPropertyEntity), TestBuilderPermissionTarget.CustomBankPropertyEntity},
                    {typeof(RichTextValueCustomBankPropertyEntity), TestBuilderPermissionTarget.CustomBankPropertyEntity},
                    {typeof(TreeStructureCustomBankPropertyEntity), TestBuilderPermissionTarget.CustomBankPropertyEntity},
                    {typeof(ItemLayoutTemplateResourceEntity), TestBuilderPermissionTarget.ItemLayoutTemplateEntity},
                    {typeof(ControlTemplateResourceEntity), TestBuilderPermissionTarget.ControlTemplateEntity},
                };

                return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, d[PropertyEntity.DataValue.GetType()], PropertyEntity.DataValue.BankId);
            }
        }

        private bool IsDependentResourceCollectionChanged()
        {
            foreach (DependentResourceEntity dependedEntity in PropertyEntity.DataValue.RemovedDependentEntities)
            {
                var found = false;
                foreach (var removedDependedEntity in PropertyEntity.DataValue.DependentResourceCollection)
                {
                    found = dependedEntity.DependentResourceId == removedDependedEntity.DependentResourceId;
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    return true;
                }
            }

            return false;
        }


        public IPropertyEntity ResourceToEditMetadataFor
        {
            get { return PropertyEntity.DataValue; }
            set { PropertyEntity.DataValue = value; }
        }

        public bool EditNameAllowed
        {
            get { return false; }
        }

        public bool EditTitleAllowed
        {
            get { return true; }
        }

        public bool OpenResourcePropertyDialogButtonVisible
        {
            get { return false; }
        }

    }
}
