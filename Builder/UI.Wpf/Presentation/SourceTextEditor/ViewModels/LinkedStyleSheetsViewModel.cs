using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels
{

    [ExportViewModel("SourceTextEditor.LinkedStyleSheetsVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class LinkedStyleSheetsViewModel : ViewModelBase
    {

        static readonly PropertyChangedEventArgs LinkedStyleSheetResourcesArgs = ObservableHelper.CreateArgs<LinkedStyleSheetsViewModel>(x => x.LinkedStyleSheetResources); static readonly PropertyChangedEventArgs SelectedStyleSheetArgs = ObservableHelper.CreateArgs<LinkedStyleSheetsViewModel>(x => x.SelectedStyleSheet);


        private readonly IViewAwareStatus _viewAwareStatusService;
        private ISourceTextEditorViewModel _sourceTextEditorVM;
        private IPropertyEntity _sourceTextResourceEntity;




        [ImportingConstructor]
        public LinkedStyleSheetsViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;


            InitProperties();
            InitCommmands();
        }



        private void InitProperties()
        {
            LinkedStyleSheetResources = new DataWrapper<System.Collections.ObjectModel.ObservableCollection<Questify.Builder.Model.ContentModel.EntityClasses.DependentResourceEntity>>(this, LinkedStyleSheetResourcesArgs);
            LinkedStyleSheetResources.DataValue = new System.Collections.ObjectModel.ObservableCollection<Questify.Builder.Model.ContentModel.EntityClasses.DependentResourceEntity>();

            SelectedStyleSheet = new DataWrapper<Questify.Builder.Model.ContentModel.EntityClasses.DependentResourceEntity>(this, SelectedStyleSheetArgs);
        }



        public DataWrapper<System.Collections.ObjectModel.ObservableCollection<Questify.Builder.Model.ContentModel.EntityClasses.DependentResourceEntity>> LinkedStyleSheetResources { get; private set; }
        public DataWrapper<Questify.Builder.Model.ContentModel.EntityClasses.DependentResourceEntity> SelectedStyleSheet { get; set; }



        public SimpleCommand<object, object> AddStyleSheetLinkCommand { get; private set; }
        public SimpleCommand<object, object> RemoveStyleSheetLinkCommand { get; private set; }

        private void InitCommmands()
        {
            AddStyleSheetLinkCommand = new SimpleCommand<object, object>(o => AddStyleSheetLink());
            RemoveStyleSheetLinkCommand = new SimpleCommand<object, object>(o => RemoveStyleSheetLink());
        }

        private void AddStyleSheetLink()
        {
            ISourceTextEditorObjectFactory objFactory = ViewModelRepository.Instance.Resolver.Container.GetExport<ISourceTextEditorObjectFactory>().Value;
            GenericResourceDto styleSheetToLink = objFactory.SelectStyleSheetToLink(_sourceTextEditorVM.BankId.DataValue, _sourceTextEditorVM.ContextIdentifier.DataValue, _sourceTextEditorVM.ResourceManager.DataValue);

            if (styleSheetToLink != null && LinkedStyleSheetResources.DataValue.FirstOrDefault(x => x.DependentResource.Name == styleSheetToLink.Name) == null)
            {
                DependentResourceEntity dep = new DependentResourceEntity(((GenericResourceEntity)_sourceTextResourceEntity).ResourceId, styleSheetToLink.ResourceId);

                IItemEditorObjectFactory itemObjFactory = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
                dep.DependentResource = itemObjFactory.GetGenericResource(styleSheetToLink.BankId, styleSheetToLink.Name);

                LinkedStyleSheetResources.DataValue.Add(dep);
                _sourceTextResourceEntity.DependentResourceCollection.Add(dep);

                _sourceTextEditorVM.StylesheetsCollectionChanged();
            }
        }

        private void RemoveStyleSheetLink()
        {
            DependentResourceEntity depResource = LinkedStyleSheetResources.DataValue.FirstOrDefault(x => x.DependentResource.Name == SelectedStyleSheet.DataValue.DependentResource.Name);

            if (depResource != null)
            {
                if (_sourceTextResourceEntity.DependentResourceCollection.RemovedEntitiesTracker == null)
                {
                    _sourceTextResourceEntity.DependentResourceCollection.RemovedEntitiesTracker = new Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection();
                }

                LinkedStyleSheetResources.DataValue.Remove(depResource);
                _sourceTextResourceEntity.DependentResourceCollection.RemovedEntitiesTracker.Add(depResource);
                _sourceTextResourceEntity.DependentResourceCollection.Remove(depResource);

                _sourceTextEditorVM.StylesheetsCollectionChanged();
            }
        }



        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;

                var workspaceData = (IWorkSpaceAware)view;

                _sourceTextEditorVM = (ISourceTextEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                _sourceTextEditorVM.Updated += (s, e) => { if (e.StringValue == "DoUpdate") Update(); };
                if (!_sourceTextEditorVM.IsLoading) Update();
            }
        }

        private void Update()
        {
            if (_sourceTextEditorVM.HasError.DataValue) return;
            _sourceTextResourceEntity = _sourceTextEditorVM.GenericResourceEntity.DataValue;

            var stylesheetDependentResources = _sourceTextResourceEntity.DependentResourceCollection.Where(x => x.DependentResource.GetType() == typeof(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity) && ((Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity)x.DependentResource).MediaType == "text/css");
            LinkedStyleSheetResources.DataValue.Clear();
            stylesheetDependentResources.ToList().ForEach(x => LinkedStyleSheetResources.DataValue.Add(x));
        }

    }
}
