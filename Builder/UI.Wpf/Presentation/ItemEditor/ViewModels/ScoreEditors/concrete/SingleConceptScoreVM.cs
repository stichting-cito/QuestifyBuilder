using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cinch;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    class SingleConceptScoreVM : ViewModelBase, IConceptScoringBrowserScoreContainer
    {
        static PropertyChangedEventArgs ScoreItemsArgs = ObservableHelper.CreateArgs<SingleConceptScoreVM>(x => x.Score); static PropertyChangedEventArgs SelectedArgs = ObservableHelper.CreateArgs<SingleConceptScoreVM>(x => x.Selected); static PropertyChangedEventArgs InputEnabledArgs = ObservableHelper.CreateArgs<SingleConceptScoreVM>(x => x.InputEnabled);
        private readonly List<IConceptScoringBrowserHierarchyPart> _parents = new List<IConceptScoringBrowserHierarchyPart>();
        private readonly string _partName;
        private readonly string _id;
        private readonly IConceptScoreManipulator _manipulator;
        private readonly bool _isInitializeState;

        public SingleConceptScoreVM(EncodingScoreHierarchyPartVM parent, string id, int? score, IConceptScoreManipulator manipulator)
        {
            _parents.Add(parent);
            _partName = parent.PartName;
            _id = id;
            _manipulator = manipulator;

            Selected = new DataWrapper<bool>(this, SelectedArgs, () =>
                {
                    foreach (var p in _parents)
                    {
                        if (p.IsSelected != Selected.DataValue)
                        {
                            p.IsSelected = Selected.DataValue;
                            var part = p as EncodingScoreHierarchyPartVM;
                            if (part != null) part.Parent.Selected.DataValue = true;
                        }
                    }

                    if (Selected.DataValue && !Score.DataValue.HasValue)
                    {
                        Score.DataValue = 0;
                    }

                    if (!Selected.DataValue && Score.DataValue.HasValue)
                    {
                        Score.DataValue = null;
                    }
                });

            Score = new DataWrapper<int?>(this, ScoreItemsArgs, () =>
            {
                _manipulator.SetScore(_partName, _id, Score.DataValue);
                if (!_isInitializeState)
                {
                    foreach (var p in _parents)
                    {
                        var part = p as EncodingScoreHierarchyPartVM;

                        if (part != null && part.Parent.ConceptScorePart != null)
                        {
                            var conceptScorePart = part.Parent.ConceptScorePart.SingleOrDefault(i => i.ConceptId == _id);
                            if (conceptScorePart != null)
                            {
                                conceptScorePart.IntScore = GetHighestChildScore(part.Parent.Children);
                            }
                        }
                    }
                }
            });

            InputEnabled = new DataWrapper<bool>(this, InputEnabledArgs, () =>
                {
                    if (Selected.DataValue && !InputEnabled.DataValue)
                    {
                        Score.DataValue = null;
                    }
                });

            _isInitializeState = true;
            Score.DataValue = score;
            _isInitializeState = false;
        }

        private int GetHighestChildScore(List<EncodingScoreHierarchyPartVM> children)
        {
            int intScore = 0;
            foreach (var child in children)
            {
                if (child.ConceptScorePart != null)
                {
                    var conceptScoringBrowserScoreContainer = child.ConceptScorePart.SingleOrDefault(c => c.ConceptId == _id);
                    if (conceptScoringBrowserScoreContainer != null)
                    {
                        if (conceptScoringBrowserScoreContainer.IntScore != null && conceptScoringBrowserScoreContainer.IntScore > intScore)
                        {
                            intScore = (int)conceptScoringBrowserScoreContainer.IntScore;
                        }
                    }
                }
            }
            return intScore;
        }

        public void AddParent(IConceptScoringBrowserHierarchyPart parent)
        {
            _parents.Add(parent);
        }

        public DataWrapper<int?> Score { get; private set; }

        string IConceptScoringBrowserScoreContainer.ConceptId
        {
            get { return _id; }
        }

        int? IConceptScoringBrowserScoreContainer.IntScore
        {
            get { return Score.DataValue; }
            set { Score.DataValue = value; }
        }

        public DataWrapper<bool> Selected { get; private set; }

        public DataWrapper<bool> InputEnabled { get; private set; }

    }
}
