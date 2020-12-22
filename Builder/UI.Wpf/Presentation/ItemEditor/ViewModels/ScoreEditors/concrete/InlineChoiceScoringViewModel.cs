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
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.ScoreEditors.InlineChoiceScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class InlineChoiceScoringViewModel : ScoringViewModel<InlineChoiceScoringParameter>
    {

        public string Label { get; private set; }

        static PropertyChangedEventArgs LabelArgs = ObservableHelper.CreateArgs<InlineChoiceScoringViewModel>(x => x.Label);

        private string _selectedChoice;

        public string SelectedChoice
        {
            get { return _selectedChoice; }
            set
            {
                if (value != null && _selectedChoice != value)
                {
                    _selectedChoice = value;
                    _scoringManipulator.SetKey(value);
                    NotifyPropertyChanged(SelectedChoiceArgs);
                }
            }
        }

        static PropertyChangedEventArgs SelectedChoiceArgs = ObservableHelper.CreateArgs<InlineChoiceScoringViewModel>(x => x.SelectedChoice);

        public List<InlineScoringChoiceViewModel> Choices { get; private set; }

        static PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<InlineChoiceScoringViewModel>(x => x.Choices);

        private IViewAwareStatus _viewAwareStatusService;

        private IChoiceScoringManipulator _scoringManipulator;

        [ImportingConstructor]
        public InlineChoiceScoringViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
        }

        private void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;
                var data = (Tuple<ScoringParameter, Solution>)workspaceData.WorkSpaceContextualData.DataValue;
                Init(scorePrm: data.Item1, solution: data.Item2);

                var inlineChoiceScoringParameter = data.Item1 as InlineChoiceScoringParameter;

                Label = !string.IsNullOrEmpty(inlineChoiceScoringParameter.Label) ? inlineChoiceScoringParameter.Label : "Default";
                NotifyPropertyChanged(LabelArgs);

                if (inlineChoiceScoringParameter.Value != null)
                {
                    Choices = inlineChoiceScoringParameter.Value.Select(sub => new InlineScoringChoiceViewModel() { Id = sub.Id, Value = System.Net.WebUtility.HtmlDecode(sub.InnerParameters.Cast<PlainTextParameter>().FirstOrDefault().Value) }).ToList();
                    NotifyPropertyChanged(ChoicesArgs);
                }

                var status = _scoringManipulator.GetKeyStatus();
                SelectedChoice = status.FirstOrDefault(s => s.Value).Key;
            }
        }

        public class InlineScoringChoiceViewModel
        {
            public string Id { get; set; }

            public string Value { get; set; }
        }

        protected override void CreateScoringManipulator(InlineChoiceScoringParameter scoreParam, Solution solution)
        {
            _scoringManipulator = scoreParam.GetScoreManipulator(solution);
        }
    }
}