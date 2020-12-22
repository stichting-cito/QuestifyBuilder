using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    [ExportViewModel("ItemEditor.V2ScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class V2ScoringViewModel : V2ScoringBase
    {


        static V2ScoringViewModel()
        {
            EnableMeCommand = new SimpleCommand<object, object>((o) => true
                                                                , (o) => V2ScoringViewModel.LoadMeInstead = true);
        }

        internal readonly static ICommand EnableMeCommand;

        internal static bool LoadMeInstead { get; set; }


        [ImportMany(typeof(IScoringParameterWorkspaceFactory))] private IEnumerable<Lazy<IScoringParameterWorkspaceFactory, IEditorForType>> factories = null;
        private Dictionary<Type, Lazy<IScoringParameterWorkspaceFactory, IEditorForType>> factoryDictionary;



        [ImportingConstructor]
        public V2ScoringViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            KeyDefinitionScoreViewModels = new ObservableCollection<KeyFindingGroupScoreViewModel>();
        }



        public ObservableCollection<KeyFindingGroupScoreViewModel> KeyDefinitionScoreViewModels { private set; get; }



        public override void OnViewIsLoaded()
        {
            UpdateFactories();

            WalkScoringParameters(Solution, ScoringParameters);
        }

        void UpdateFactories()
        {
            factoryDictionary = new Dictionary<Type, Lazy<IScoringParameterWorkspaceFactory, IEditorForType>>();
            foreach (var fact in factories)
            {
                factoryDictionary.Add(fact.Metadata.ValueHoldingType, fact);
            }
        }

        private void WalkScoringParameters(Solution solution, IEnumerable<ScoringParameter> prms)
        {
            foreach (var scorePrm in prms)
            {
                Lazy<IScoringParameterWorkspaceFactory, IEditorForType> _out;
                if (factoryDictionary.TryGetValue(scorePrm.GetType(), out _out))
                {
                    var fact = _out.Value;
                    Debug.Assert(fact.CanHandle(scorePrm));

                    var findingViewModel = KeyDefinitionScoreViewModels.FirstOrDefault(fvm => fvm.KeyFindingId == scorePrm.FindingId);
                    if (findingViewModel == null)
                    {

                        var findingId = !string.IsNullOrEmpty(scorePrm.FindingId) ? scorePrm.FindingId : "default";

                        solution.GetFindingOrMakeIt(findingId);
                        var jit = new CreateObjectJIT<KeyFinding>(solution.Findings.FindById(findingId), () => solution.GetFindingOrMakeIt(findingId));

                        findingViewModel = new KeyFindingGroupScoreViewModel(jit);
                        KeyDefinitionScoreViewModels.Add(findingViewModel);
                    }
                    findingViewModel.ScoreEditorsViews.Add(fact.Create(scorePrm, solution));
                }
            }
        }
    }
}

