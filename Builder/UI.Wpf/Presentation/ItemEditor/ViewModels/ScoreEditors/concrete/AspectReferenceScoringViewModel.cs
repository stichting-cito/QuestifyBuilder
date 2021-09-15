using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.HelperClasses;
using Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.AspectReferenceScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AspectReferenceScoringViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {
        static readonly PropertyChangedEventArgs AspectEditControlEventArgs = ObservableHelper.CreateArgs<AspectReferenceScoringViewModel>(x => x.EditControl);
        static readonly PropertyChangedEventArgs AspectRefArgs = ObservableHelper.CreateArgs<AspectReferenceScoringViewModel>(x => x.AspectRef); static readonly PropertyChangedEventArgs AspectEditorVisibleArgs = ObservableHelper.CreateArgs<AspectReferenceScoringViewModel>(x => x.AspectEditorVisible); static readonly PropertyChangedEventArgs AspectExpanderHeaderArgs = ObservableHelper.CreateArgs<AspectReferenceScoringViewModel>(x => x.AspectExpanderHeader); static readonly PropertyChangedEventArgs EnableMaxScoreTextBoxArgs = ObservableHelper.CreateArgs<AspectReferenceScoringViewModel>(x => x.EnableMaxScoreTextBox);

        private readonly IViewAwareStatus _viewAwareStatusService;
        private AspectEditorSingleView _view;
        private ResourceEntity _resourceEntity;
        string _aspectIdentifier;
        private string _controllerId;
        IItemEditorViewModel _itemEditorVm = null; IAspectReferencesScoringViewModel _aspectRefsViewModel = null;
        private Solution _itemSolution;
        private bool _showAspectIdentifierInHeader = false;

        [ImportingConstructor]
        public AspectReferenceScoringViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;

            AspectRef = new DataWrapper<AspectReference>(this, AspectRefArgs);
            EditControl = new DataWrapper<object>(this, AspectEditControlEventArgs);
            EditControl.PropertyChanged += HandleEditControlPropertyChanged;
            AspectEditorVisible = new DataWrapper<bool>(this, AspectEditorVisibleArgs)
            {
                DataValue = false
            };
            AspectExpanderHeader = new DataWrapper<string>(this, AspectExpanderHeaderArgs);
            EnableMaxScoreTextBox = new DataWrapper<bool>(this, EnableMaxScoreTextBoxArgs);
        }

        void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                IWorkSpaceAware workspaceData = (IWorkSpaceAware)view;
                _view = (AspectEditorSingleView)view;
                _view.SetViewModel(this);

                var dataValue = (Tuple<IItemEditorViewModel, IAspectReferencesScoringViewModel, string, string, bool, bool, bool>)workspaceData.WorkSpaceContextualData.DataValue;
                _itemEditorVm = dataValue.Item1;
                _aspectRefsViewModel = dataValue.Item2;
                _aspectIdentifier = dataValue.Item3;
                _controllerId = dataValue.Item4;
                _showAspectIdentifierInHeader = dataValue.Item6;
                EnableMaxScoreTextBox.DataValue = dataValue.Item7;

                _itemEditorVm.Updated += ItemEditor_Updated;
                if (!_itemEditorVm.IsLoading)
                    Update();

                if (dataValue.Item5)
                {
                    _view.MaxScoreTextBox.Focus();
                }
            }
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated;
            }

            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        public DataWrapper<AspectReference> AspectRef { private set; get; }
        public DataWrapper<object> EditControl { get; }
        public DataWrapper<bool> AspectEditorVisible { private set; get; }
        public DataWrapper<string> AspectExpanderHeader { private set; get; }
        public DataWrapper<bool> EnableMaxScoreTextBox { private set; get; }

        public void DoPostSaveTasks()
        {
        }

        public void DoPreSaveTasks()
        {
            ICommandSupport CmdSupp = _view as ICommandSupport;
            if (CmdSupp != null)
                CmdSupp.DoPreSaveTasks();
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void MaxScoreUpdate()
        {
            if (_aspectRefsViewModel != null)
            {
                _aspectRefsViewModel.UpdateMaxScore(AspectRef.DataValue.SourceName);
            }
        }

        public void KillView()
        {
            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }
        }
        protected override void OnDispose()
        {
            if (EditControl != null)
            {
                EditControl.PropertyChanged -= HandleEditControlPropertyChanged;
            }

            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated;
                _itemEditorVm = null;
            }

            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }

            base.OnDispose();
        }

        private void ItemEditor_Updated(object sender, StringEventArgs e)
        {
            if (e.StringValue == "DoUpdate")
                Update();
        }

        private void HandleEditControlPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _itemEditorVm.EditorChange(((DataWrapper<object>)sender).DataValue);
        }

        internal void Update()
        {
            if (_itemEditorVm.HasError.DataValue)
            {
                return;
            }

            _itemSolution = _itemEditorVm.AssessmentItem.DataValue.Solution;
            IEnumerable<ScoringParameter> scoringParameters = _itemEditorVm.ParameterSetCollection.DataValue.DeepFetchInlineScoringParameters();
            _itemSolution.FixRemovedScoringParameters(scoringParameters);
            AddAspect();

            if (AspectRef.DataValue != null)
            {
                AspectReferenceEditorBehavior behavior = new AspectReferenceEditorBehavior(_itemEditorVm.ItemResourceEntity.DataValue, (AspectResourceEntity)_resourceEntity, _itemEditorVm.ResourceManager.DataValue, _itemEditorVm.ContextIdentifier.DataValue, AspectRef.DataValue);
                _view.UpdateXhtmlEditor(behavior);
            }

            AspectEditorVisible.DataValue = true;
            if (_showAspectIdentifierInHeader)
            {
                AspectExpanderHeader.DataValue = $"Aspect '{_aspectIdentifier}'";
            }
            else
            {
                AspectExpanderHeader.DataValue = Properties.Resources.AspectScoreEditorLabel;
            }

            _itemEditorVm.EnableElementsOnCompletion();
        }

        internal void AddAspect()
        {
            AspectReferenceCollection refColl = _itemSolution.AspectReferenceSetCollection.FirstOrDefault(ars => ars.Id.Equals(_controllerId, StringComparison.InvariantCultureIgnoreCase));
            if (refColl == null)
            {
                refColl = new AspectReferenceCollection(_controllerId);
                _itemSolution.AspectReferenceSetCollection.Add(refColl);
            }

            if (refColl.Items.Any(ar => ar.SourceName.Equals(_aspectIdentifier, StringComparison.InvariantCultureIgnoreCase)))
            {
                AspectRef.DataValue = refColl.Items.First(ar => ar.SourceName.Equals(_aspectIdentifier, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                if (_resourceEntity == null || !_resourceEntity.Name.Equals(_aspectIdentifier, StringComparison.InvariantCultureIgnoreCase))
                {
                    _resourceEntity = AspectHelper.GetAspect(_aspectIdentifier, ((ItemEditorViewModel)_itemEditorVm).ItemResourceEntity.DataValue.BankId);
                }

                if (_resourceEntity != null && _resourceEntity.GetType() == typeof(AspectResourceEntity))
                {
                    AspectReference aspectRef = new AspectReference
                    {
                        SourceName = _resourceEntity.Name,
                        MaxScore = ((AspectResourceEntity)_resourceEntity).RawScore,
                        Description = string.Empty
                    };

                    AspectRef.DataValue = aspectRef;
                }

                refColl.Items.Add(AspectRef.DataValue);
            }
        }
    }
}
