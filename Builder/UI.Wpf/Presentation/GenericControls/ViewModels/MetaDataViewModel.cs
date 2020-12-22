using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.GenericControls.ConsumerInterfaces;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.GenericControls.ViewModels
{

    [ExportViewModel("GenericControls.MetaDataVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class MetaDataViewModel : ViewModelBase
    {

        static MetaDataViewModel()
        {
            InitRules();
        }

        static readonly PropertyChangedEventArgs AllowNameEditArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.AllowNameEdit);
        static readonly PropertyChangedEventArgs AllowTitleEditArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.AllowTitleEdit);
        static readonly PropertyChangedEventArgs OpenResourcePropertyDialogButtonVisibleArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.OpenResourcePropertyDialogButtonVisible);
        static readonly PropertyChangedEventArgs ResourcePropertiesArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.ResourceProperties);
        static readonly PropertyChangedEventArgs IsVersionVisibleArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.IsVersionVisible);
        static readonly PropertyChangedEventArgs NameArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.Name);
        static readonly PropertyChangedEventArgs TitleArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.Title);

        static SimpleRule _nameRequiredStringFieldRule;
        static SimpleRule _titleRequiredStringFieldRule;
        static SimpleRule _resourceNameNoSequentialDotRule;
        static SimpleRule _nameNoIllegalCharacterAllowed;

        private static void InitRules()
        {
            _nameRequiredStringFieldRule = new SimpleRule("DataValue", GetResource("GenericControls.Metadata.Rule.Name.Required"),
                 domainObject =>
                 {
                     var obj = (DataWrapper<String>)domainObject;
                     return obj.DataValue != null && String.IsNullOrWhiteSpace(obj.DataValue);
                 });

            _nameNoIllegalCharacterAllowed = new SimpleRule("DataValue", GetResource("GenericControls.Metadata.Rule.Name.NoIllegalCharacters").ToString(),
    domainObject =>
    {
        var obj = (DataWrapper<string>)domainObject;
        return obj.DataValue != null && (obj.DataValue.IndexOfAny(IllegalChars.ToCharArray()) > -1);
    });

            _titleRequiredStringFieldRule = new SimpleRule("DataValue", GetResource("GenericControls.Metadata.Rule.Title.Required").ToString(),
                 domainObject =>
                 {
                     var obj = (DataWrapper<String>)domainObject;
                     return obj.DataValue != null && String.IsNullOrWhiteSpace(obj.DataValue);
                 });

            _resourceNameNoSequentialDotRule = new SimpleRule("DataValue", GetResource("GenericControls.Metadata.Rule.Name.NoDoubleDots").ToString(),
    domainObject =>
    {
        var obj = (DataWrapper<string>)domainObject;
        return (obj.DataValue != null && obj.DataValue.Replace(" ", "").Contains(".."));
    });
        }



        private readonly IMessageBoxService _messageBoxService;
        private readonly IViewAwareStatus _viewAwareStatusService;
        private readonly ISourceTextEditorObjectFactory _sourceTextEditorObjectFactory;
        private IMetadataViewConsumer _metaDataViewConsumerVM;

        private const string IllegalChars = @"<>""#%{}|\/^~[]';?:@=&^$+()!,`*";


        public List<StateEntity> States { get; private set; }
        public IResourcePropertyDialogService ResourcePropertyDialogService { get; private set; }
        public DataWrapper<IPropertyEntity> ResourceProperties { get; private set; }
        public DataWrapper<bool> OpenResourcePropertyDialogButtonVisible { get; private set; }

        public DataWrapper<string> Name { get; set; }
        public DataWrapper<string> Title { get; private set; }
        public DataWrapper<bool> AllowNameEdit { get; private set; }
        public DataWrapper<bool> AllowTitleEdit { get; private set; }
        public DataWrapper<bool> IsVersionVisible { get; private set; }




        [ImportingConstructor]
        public MetaDataViewModel(IViewAwareStatus viewAwareStatusService, IMessageBoxService messageBoxService, IResourcePropertyDialogService resourcePropertyDialogService, ISourceTextEditorObjectFactory sourceTextEditorObjectFactory)
        {
            _sourceTextEditorObjectFactory = sourceTextEditorObjectFactory;

            _messageBoxService = messageBoxService;

            ResourcePropertyDialogService = resourcePropertyDialogService;

            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            InitProperties();
            InitCommands();
        }

        private void InitProperties()
        {
            var objFactory = _sourceTextEditorObjectFactory;

            ResourceProperties = new DataWrapper<IPropertyEntity>(this, ResourcePropertiesArgs);
            ResourceProperties.PropertyChanged += (s, e) =>
            {
                if (ResourceProperties.DataValue != null)
                {
                    if (ResourceProperties.DataValue.IsNew) ResourceProperties.DataValue.ModifiedDate = DateTime.Now;
                    if (ResourceProperties.DataValue.IsNew) ResourceProperties.DataValue.CreationDate = DateTime.Now;
                }
            };

            States = new List<StateEntity>() { new StateEntity() { StateId = 0, Title = string.Empty } };
            States.AddRange(objFactory.GetAvailableStates().OfType<StateEntity>().ToList());

            Name = new DataWrapper<string>(this, NameArgs);
            Name.AddRule(_nameRequiredStringFieldRule);
            Name.AddRule(_resourceNameNoSequentialDotRule);
            Name.AddRule(_nameNoIllegalCharacterAllowed);
            Name.PropertyChanged += (s, e) => { ResourceProperties.DataValue.Name = Name.DataValue; };
            Title = new DataWrapper<string>(this, TitleArgs);
            Title.AddRule(_titleRequiredStringFieldRule);
            Title.PropertyChanged += (s, e) => { ResourceProperties.DataValue.Title = Title.DataValue; };
            OpenResourcePropertyDialogButtonVisible = new DataWrapper<bool>(this, OpenResourcePropertyDialogButtonVisibleArgs);
            AllowNameEdit = new DataWrapper<bool>(this, AllowNameEditArgs);
            AllowTitleEdit = new DataWrapper<bool>(this, AllowTitleEditArgs);
            IsVersionVisible = new DataWrapper<bool>(this, IsVersionVisibleArgs);
        }

        private void InitCommands()
        {
            OpenResourcePropertiesDialog = new SimpleCommand<object, object>(o => DoOpenResourcePropertiesDialog());
        }

        private static string GetResource(string name)
        {
            var data = (Application.Current != null) ? Application.Current.TryFindResource(name) : null;
            return (data != null) ? data.ToString() : name;
        }




        public SimpleCommand<object, object> OpenResourcePropertiesDialog { get; private set; }


        private void DoOpenResourcePropertiesDialog()
        {
            if (ResourceProperties.DataValue.IsNew)
                _messageBoxService.ShowInformation((string)Application.Current.FindResource("GenericControls.Metadata.ActionNotAllowedWhenNew"));
            else
                ResourcePropertyDialogService.Show(_metaDataViewConsumerVM.ResourceToEditMetadataFor.Id, _metaDataViewConsumerVM.ResourceToEditMetadataFor.GetType(), 4);
        }


        public bool HasErrors
        {
            get { return !Name.IsValid || !Title.IsValid; }
        }



        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;

                _metaDataViewConsumerVM = (IMetadataViewConsumer)workspaceData.WorkSpaceContextualData.DataValue;

                ResourceProperties.DataValue = _metaDataViewConsumerVM.ResourceToEditMetadataFor;

                AllowNameEdit.DataValue = _metaDataViewConsumerVM.EditNameAllowed;
                AllowTitleEdit.DataValue = _metaDataViewConsumerVM.EditTitleAllowed;
                OpenResourcePropertyDialogButtonVisible.DataValue = _metaDataViewConsumerVM.OpenResourcePropertyDialogButtonVisible;

                if (ResourceProperties.DataValue != null)
                {
                    Name.DataValue = ResourceProperties.DataValue.Name;
                    Title.DataValue = ResourceProperties.DataValue.Title;
                    Debug.Assert(Name.DataValue != null);
                    Debug.Assert(Title.DataValue != null);
                }

                IsVersionVisible.DataValue = _metaDataViewConsumerVM.ResourceToEditMetadataFor is IVersionable;
            }
        }

    }
}
