using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{

    [ExportViewModel("ItemEditor.ScoreEditors.MCScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class MCScoringViewModel : ScoringViewModel<MultiChoiceScoringParameter>
    {
        static PropertyChangedEventArgs ScorableItemsArgs = ObservableHelper.CreateArgs<MCScoringViewModel>(x => x.ScorableItems);
        private IViewAwareStatus _viewAwareStatusService;
        private IChoiceScoringManipulator _scoringManipulator;


        [ImportingConstructor]
        public MCScoringViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            HandleToggle = new SimpleCommand<object, string>(key => ManipulateKey(key));
            if (Designer.IsInDesignMode)
            {
                ScorableItems.Add("A", false);
                ScorableItems.Add("B", false);
                ScorableItems.Add("C", true);
                ScorableItems.Add("D", false);
            }
        }

        internal void ManipulateKey(string key)
        {

            Debug.Assert(ScorableItems != null, "Should not occur");

            if (ScorableItems[key])
            {
                _scoringManipulator.RemoveKey(key);
            }
            else
            {
                _scoringManipulator.SetKey(key);
            }

            ScorableItems = new SortedDictionary<string, bool>(_scoringManipulator.GetKeyStatus(), StringComparer.CurrentCultureIgnoreCase);
            NotifyPropertyChanged("ScorableItems");
        }

        private void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;
                var data = (Tuple<ScoringParameter, Solution>)workspaceData.WorkSpaceContextualData.DataValue;

                Init(scorePrm: data.Item1, solution: data.Item2);

                AddRule(Get_NoValueRule(Msg_NoKey));

                AddRule(Get_TooLittleValueRule(Msg_TooLittleKey));

                AddRule(Get_TooManyValueRule(Msg_TooManyKeys));

                ScorableItems = new SortedDictionary<string, bool>(_scoringManipulator.GetKeyStatus(), StringComparer.CurrentCultureIgnoreCase);
                NotifyPropertyChanged("ScorableItems");
            }
        }


        public SimpleCommand<object, string> HandleToggle { private set; get; }

        public SortedDictionary<string, bool> ScorableItems { private set; get; }


        internal SimpleRule Get_NoValueRule(string msg)
        {
            return new SimpleRule("ScorableItems", msg, vm =>
            {
                var mcVM = (MCScoringViewModel)vm;
                if (mcVM.ScorableItems == null) return true;
                var anySelected = mcVM.ScorableItems.Any(e => e.Value);
                return !anySelected;
            });
        }

        internal SimpleRule Get_TooLittleValueRule(string msg)
        {
            return new SimpleRule("ScorableItems", msg, vm =>
            {
                var mcVM = (MCScoringViewModel)vm;
                if (mcVM.ScorableItems == null) return true;
                var cnt = mcVM.ScorableItems.Count(x => x.Value);
                return !(cnt >= ScoreParameter.MinChoices || cnt == 0);

            });
        }

        internal SimpleRule Get_TooManyValueRule(string msg)
        {
            return new SimpleRule("ScorableItems", msg, vm =>
            {
                var mcVM = (MCScoringViewModel)vm;
                if (mcVM.ScorableItems == null) return true;
                var cnt = mcVM.ScorableItems.Count(x => x.Value);
                return !(cnt <= mcVM.ScoreParameter.MaxChoices || mcVM.ScoreParameter.MaxChoices == 0 || (mcVM.ScoreParameter.IsSingleChoice && mcVM.ScoreParameter.IsSingleValue == false));
            });
        }


        protected override void CreateScoringManipulator(MultiChoiceScoringParameter scoreParam, Solution solution)
        {
            _scoringManipulator = scoreParam.GetScoreManipulator(solution);
        }
    }
}