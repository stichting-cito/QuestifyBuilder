using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Cinch;
using Cito.Tester.ContentModel;
using Enums;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Security;
using wCtrl = System.Windows.Forms.Control;
using wContainerCtrl = System.Windows.Forms.ContainerControl;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.MetaDataWorkSpace, typeof(MetaData))]
    public partial class MetaData : IMetaDataControl
    {

        private bool _disposed;
        private ResourceEntity _resourceEntity;
        private AssessmentItem _assessmentItem;



        public MetaData()
        {
            InitializeComponent();

            SetEventBinding();

            Loaded += (s, e) => Window.GetWindow(this).Closing += (s2, e2) =>
            {
                if (!e2.Cancel)
                {
                    Dispose();
                }
            };
        }

        ~MetaData()
        {

            try
            {
                Dispatcher.InvokeIfRequired(() => ((ICinchDisposable)DataContext).Dispose());
            }
            catch (InvalidAsynchronousStateException ex)
            {
            }
            Dispose(false);
        }

        private void SetEventBinding()
        {
            ItemNameCtrl.SwitchItemTemplate += HandleSwitchTemplate;
            ItemNameCtrl.ChangeItemCode += HandleChangeItemCode;
            ResourceNameCtrl.OpenResourcePropertyDialogButtonClicked += HandleOpenResourcePropertyDialogButtonClicked;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (ItemNameCtrl != null)
                    {
                        ItemNameCtrl.SwitchItemTemplate -= HandleSwitchTemplate;
                        ItemNameCtrl.ChangeItemCode -= HandleChangeItemCode;
                        ItemNameCtrl.Dispose();
                    }

                    if (ResourceNameCtrl != null)
                    {
                        ResourceNameCtrl.OpenResourcePropertyDialogButtonClicked -= HandleOpenResourcePropertyDialogButtonClicked;
                        ResourceNameCtrl.Dispose();
                    }

                    ResourceCustomPropsCtrl.Dispose();

                    CustomPropertiesHost.Child = null;
                    CustomPropertiesHost.Dispose();
                    CustomPropertiesHost = null;

                    ItemInfoHost.Child = null;
                    ItemInfoHost.Dispose();
                    ItemInfoHost = null;

                    ItemMetaInfoHost.Child = null;
                    ItemMetaInfoHost.Dispose();
                    ItemMetaInfoHost = null;
                }
                _disposed = true;
            }
        }


        void HandleOpenResourcePropertyDialogButtonClicked(object sender, EventArgs e)
        {
            DoOpenResourcePropertyDialog();
        }

        private void HandleChangeItemCode(object sender, EventArgs e)
        {
            DoItemRename();
        }

        private void HandleSwitchTemplate(object sender, EventArgs e)
        {
            DoSwitchTemplate();
        }


        public void Update(AssessmentItem assessmentItem, ResourceEntity resourceEntity)
        {
            _resourceEntity = resourceEntity;
            _assessmentItem = assessmentItem;

            ItemNameCtrl.AssessmentItem = _assessmentItem;
            ItemNameCtrl.ToggleCodeField(_resourceEntity.IsNew); ItemNameCtrl.HideOrShowItemId();
            ItemNameCtrl.BankId = _resourceEntity.BankId;
            ResourceNameCtrl.ResourceEntity = _resourceEntity;
            ResourceCustomPropsCtrl.CustomPropertyTypeFilter = ResourceTypeEnum.ItemResource;
            ResourceCustomPropsCtrl.ResourceEntity = _resourceEntity;
            if (_resourceEntity.IsNew)
                ItemNameCtrl.SetFocusOnCodeField();
            else
                ItemNameCtrl.SetFocusOnTitleField();

            ItemNameCtrl.ToggleChangeCodeButton(PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ChangeItemCode, _resourceEntity.BankId, 0));
        }

        public Action DoItemRename { get; set; }


        public Action DoSwitchTemplate { get; set; }

        public Action DoOpenResourcePropertyDialog { get; set; }


        [Bindable(true), Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand RenameItem
        {
            get { return (ICommand)GetValue(RenameItemProperty); }
            set { SetValue(RenameItemProperty, value); }
        }

        public static readonly DependencyProperty RenameItemProperty =
    DependencyProperty.Register("RenameItem", typeof(ICommand), typeof(MetaData), new PropertyMetadata(null));



        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(MetaData),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

        public void DoPreSaveTasks()
        {
            Validate(ItemNameCtrl);
            Validate(ResourceNameCtrl);
            Validate(ResourceCustomPropsCtrl);
            AddRemovedCustomBankPropertyValuesToTracker();
            ResourceNameCtrl.EndEdit();
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void DoPostSaveTasks()
        {
            if (ResourceCustomPropsCtrl != null)
            {
                ResourceCustomPropsCtrl.DoPostSaveTasks();
            }
        }


        void Validate(wCtrl c)
        {
            if (c.Controls != null)
            {
                foreach (wCtrl e in c.Controls)
                {
                    Validate(e);
                }
            }

            wContainerCtrl container = c as wContainerCtrl;
            if (container != null)
            {
                container.ValidateChildren();
            }
        }

        private void GridSplitter_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.OldFocus == null)
                CustomPropertiesHost.Focus();
        }

        private void AddRemovedCustomBankPropertyValuesToTracker()
        {
            if (ResourceCustomPropsCtrl.RemovedEntities.Any())
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

                foreach (CustomBankPropertyValueEntity removedEntity in ResourceCustomPropsCtrl.RemovedEntities.OfType<CustomBankPropertyValueEntity>())
                {
                    _resourceEntity.CustomBankPropertyValueCollection.Remove(removedEntity);
                }

                var freeValuesToRemove = new List<CustomBankPropertyValueEntity>();
                foreach (FreeValueCustomBankPropertyValueEntity propertyVal in _resourceEntity.CustomBankPropertyValueCollection.OfType<FreeValueCustomBankPropertyValueEntity>())
                {
                    if (string.IsNullOrEmpty(propertyVal.Value))
                    {
                        _resourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(propertyVal);
                        freeValuesToRemove.Add(propertyVal);
                    }
                }

                foreach (var freeValueToRemove in freeValuesToRemove)
                {
                    _resourceEntity.CustomBankPropertyValueCollection.Remove(freeValueToRemove);
                }
            }
        }
    }
}
