using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ItemProcessing;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    abstract class V2ScoringBase : ViewModelBase, IViewModel2ViewCommandSupport
    {

        static PropertyChangedEventArgs ScoreEditorsArgs = ObservableHelper.CreateArgs<V2ScoringBase>(x => x.ConceptEditorsViews); static PropertyChangedEventArgs ScoreVisibilityArgs = ObservableHelper.CreateArgs<V2ScoringBase>(x => x.ScoreVisibility);
        static PropertyChangedEventArgs ConceptParametersPresentArgs = ObservableHelper.CreateArgs<V2ScoringBase>(x => x.ConceptParametersPresent);


        private IViewAwareStatus _viewAwareStatusService;
        protected IItemEditorViewModel _ItemEditorVM; private ItemLayoutAdapter _itemLayoutAdapterForItem;
        private IEnumerable<ScoringParameter> _scoringParameters;
        private const string DynamicCollAutoScoringIdentifier = "autoScoring";

        private readonly IConceptScoringWorkspaceFactory _conceptScoring = new ConceptScoringFactory();



        [ImportingConstructor]
        public V2ScoringBase(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;

            ConceptEditorsViews = new ObservableCollection<WorkspaceData>();
            ScoreVisibility = new DataWrapper<System.Windows.Visibility>(this, ScoreVisibilityArgs);
        }



        public Solution Solution
        {
            get { return _ItemEditorVM.AssessmentItem.DataValue.Solution; }
        }

        public IEnumerable<ScoringParameter> ScoringParameters
        {
            get { return _scoringParameters; }
        }

        public ObservableCollection<WorkspaceData> ConceptEditorsViews { private set; get; }

        public bool ConceptParametersPresent { get; private set; }

        public DataWrapper<System.Windows.Visibility> ScoreVisibility { private set; get; }



        private void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                Mediator.Instance.Register(this);

                var view = _viewAwareStatusService.View;
                IWorkSpaceAware workspaceData = (IWorkSpaceAware)view;
                _ItemEditorVM = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;

                Solution solution = _ItemEditorVM.AssessmentItem.DataValue.Solution;
                ScoreVisibility.DataValue = solution.AutoScoring ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

                _scoringParameters = _ItemEditorVM.ParameterSetCollection.DataValue.DeepFetchInlineScoringParameters();

                solution.FixRemovedScoringParameters(_scoringParameters);
                EnrichScoringParametersWithDesignerSettings();
                WalkConceptParameters(solution, _scoringParameters);
                ConceptParametersPresent = ConceptEditorsViews.Count > 0;
                NotifyPropertyChanged(ConceptParametersPresentArgs);

                OnViewIsLoaded();
            }
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            Mediator.Instance.Unregister(this);

            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        protected void EnrichScoringParametersWithDesignerSettings()
        {
            foreach (var scoringParameter in _scoringParameters.Where(p => p is StringScoringParameter))
            {
                var parameterSet = _ItemEditorVM.ParameterSetCollection.DataValue.FirstOrDefault(s => s != null && s.InnerParameters.Any(p => p is XHtmlParameter));
                if (parameterSet == null)
                {
                    continue;
                }

                var inlineElements = parameterSet.InnerParameters.Where(p => p is XHtmlParameter).Select(p => ((XHtmlParameter)p).GetInlineElements());
                string scoringPrmId = scoringParameter.InlineId;
                if (scoringParameter is HotTextCorrectionScoringParameter && scoringPrmId.StartsWith("Input_"))
                {
                    scoringPrmId = scoringPrmId.Substring(6);
                }
                var inlineElementCollection = inlineElements.FirstOrDefault(x => x != null && scoringPrmId != null && x.ContainsKey(scoringPrmId));
                var inlineLayoutTemplate = string.Empty;
                if (inlineElementCollection != null)
                {
                    var inlineElement = inlineElementCollection[scoringPrmId];
                    inlineLayoutTemplate = inlineElement.LayoutTemplateSourceName;
                }
                if (string.IsNullOrEmpty(inlineLayoutTemplate))
                {
                    continue;
                }

                var parameterSetCollectionHelper = new ParameterSetCollectionHelper(_ItemEditorVM.ResourceManager.DataValue, inlineLayoutTemplate);
                var parametersFromTemplate = parameterSetCollectionHelper.GetExtractedParameters();

                if (parametersFromTemplate.Any())
                {
                    var parameterCollection = parametersFromTemplate.First(p => p.InnerParameters.Any(i => i is StringScoringParameter));
                    var stringScoringParameter = parameterCollection.InnerParameters.First(i => i is StringScoringParameter);
                    scoringParameter.DesignerSettings.AddRange(stringScoringParameter.DesignerSettings);
                }
            }
        }

        private void WalkConceptParameters(Solution solution, IEnumerable<ScoringParameter> parameters)
        {
            if (parameters == null || !parameters.Any()) return;
            var selectedConcept = _ItemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().FirstOrDefault();

            if ((selectedConcept != null) &&
                (selectedConcept.ConceptStructureCustomBankPropertySelectedPartCollection.Count != 0) &&
                _ItemEditorVM.IsConceptDefinedOnBankBranch)
            {
                Debug.Assert(_conceptScoring.CanHandle(parameters), "A score param was unable to be handled");
                ConceptEditorsViews.Add(_conceptScoring.Create(parameters, solution, _ItemEditorVM));
            }
        }



        public void DoPreSaveTasks()
        {
            Solution.SortSolution(_scoringParameters);
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void DoPostSaveTasks()
        {
        }

        public void KillView()
        {
        }

        protected override void OnDispose()
        {
            _ItemEditorVM = null;
            _itemLayoutAdapterForItem = null;
            _scoringParameters = null;
            Mediator.Instance.Unregister(this);

            base.OnDispose();
        }



        [MediatorMessageSink(Constants.SolutionGroupChanged)]
        public void HandleSolutionGroupChanged(bool fixRemovedScoringPrms)
        {
            OnHandleSolutionGroupChanged(fixRemovedScoringPrms);
        }

        [MediatorMessageSink(Constants.AutoScoringChanged)]
        public void HandleAutoScoringChanged(bool autoScoringOff)
        {
            OnHandleAutoScoringChanged(autoScoringOff);
        }

        virtual protected void OnHandleSolutionGroupChanged()
        {
            OnHandleSolutionGroupChanged(true);
        }

        virtual protected void OnHandleSolutionGroupChanged(bool fixRemovedScoringPrms)
        {
            Solution solution = _ItemEditorVM.AssessmentItem.DataValue.Solution;
            if (fixRemovedScoringPrms) solution.FixRemovedScoringParameters(_scoringParameters);
            ConceptEditorsViews.Clear();
            WalkConceptParameters(solution, _scoringParameters);
            ConceptParametersPresent = ConceptEditorsViews.Count > 0;
            NotifyPropertyChanged(ConceptParametersPresentArgs);
        }

        virtual protected void OnHandleAutoScoringChanged(bool autoScoring)
        {
            if (autoScoring)
            {
                var pcToDelete = _ItemEditorVM.ParameterSetCollection.DataValue.Where(pc => pc.IsDynamicCollection && pc.Id.Equals(DynamicCollAutoScoringIdentifier, StringComparison.InvariantCultureIgnoreCase)).ToList();
                pcToDelete.ForEach(pc => _ItemEditorVM.ParameterSetCollection.DataValue.Remove(pc));

                _scoringParameters = _ItemEditorVM.ParameterSetCollection.DataValue.DeepFetchInlineScoringParameters();
            }
            else
            {
                if (!_scoringParameters.Any(sp => sp is AspectScoringParameter))
                {
                    if (!_ItemEditorVM.ParameterSetCollection.DataValue.Any(pc => pc.IsDynamicCollection && pc.Id.Equals(DynamicCollAutoScoringIdentifier, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        ParameterCollection prmColl = new ParameterCollection() { Id = DynamicCollAutoScoringIdentifier, IsDynamicCollection = true };
                        AspectScoringParameter newPrm = new AspectScoringParameter() { AutoScoringOffPrm = true, ControllerId = "autoScoringOffController", FindingOverride = GetFindingOverride(), Name = "aspectScoring", SingleAspectScoringEditor = true };
                        newPrm.Value = new ParameterSetCollection { new ParameterCollection() { Id = "1" } };
                        prmColl.InnerParameters.Add(newPrm);
                        _ItemEditorVM.ParameterSetCollection.DataValue.Add(prmColl);
                    }

                    _scoringParameters = _ItemEditorVM.ParameterSetCollection.DataValue.DeepFetchInlineScoringParameters();
                }
            }

            ScoreVisibility.DataValue = autoScoring ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _ItemEditorVM.ReloadScoring();
        }

        private string GetFindingOverride()
        {
            var iltAdapter = GetItemLayoutAdapterForResourceEntity(_itemLayoutAdapterForItem);
            if (iltAdapter != null)
            {
                return iltAdapter.GetInlineFindingOverride();
            }

            return string.Empty;
        }

        private ItemLayoutAdapter GetItemLayoutAdapterForResourceEntity(ItemLayoutAdapter adapter)
        {
            if (adapter == null && !string.IsNullOrEmpty(_ItemEditorVM.ItemResourceEntity.DataValue.ItemLayoutTemplateUsedName))
            {
                _itemLayoutAdapterForItem = new ItemLayoutAdapter(_ItemEditorVM.ItemResourceEntity.DataValue.ItemLayoutTemplateUsedName, null, Handle_TestSessionContext_ResourceNeeded);
            }
            return _itemLayoutAdapterForItem;
        }

        private void Handle_TestSessionContext_ResourceNeeded(object sender, ResourceNeededEventArgs e)
        {
            if (e.Command == ResourceNeededCommand.Resource)
            {
                var request = new ResourceRequestDTO { WithDependencies = false, WithCustomProperties = false };
                var r = e.TypedResourceType != null ? _ItemEditorVM.ResourceManager.DataValue.GetTypedResource(e.ResourceName, e.TypedResourceType, request) : _ItemEditorVM.ResourceManager.DataValue.GetResource(e.ResourceName, e.StreamProcessingDelegate, request);

                e.BinaryResource = r;
            }
            else
            {
                e.BinaryResource = new BinaryResource(new object());
            }
        }



        public abstract void OnViewIsLoaded();

    }
}