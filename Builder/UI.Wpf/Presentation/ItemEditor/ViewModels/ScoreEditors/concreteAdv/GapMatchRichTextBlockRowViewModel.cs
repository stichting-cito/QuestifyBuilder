using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class GapMatchRichTextBlockRowViewModel : GapMatchBlockRowViewModel
    {

        static readonly PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<GapMatchRichTextBlockRowViewModel>(x => x.Choices);


        public GapMatchRichTextBlockRowViewModel(GapMatchRichTextScoringParameter scoringParameter, IValidatingChoiceArrayScoringManipulator<string> scoreManipulator, string scoreKey)
            : base(scoringParameter, scoreManipulator, scoreKey)
        {
            Choices = new DataWrapper<List<ChoiceViewModel>>(this, ChoicesArgs);
            if (scoringParameter.Value != null)
            {
                Choices.DataValue = scoringParameter.Gaps.Select(sub => new ChoiceViewModel { Id = sub.Key, Value = GetFriendlyNameOfMovableElement(sub) }).ToList();
            }
            Choices.DataValue.Insert(0, new ChoiceViewModel() { Id = _clearEntryChoiceId, Value = "" });

            var matchMaxRule = GapMatchValidationMatchMaxRule("DataValue", GetUiString("GapMatch.ValidateMatchMax"));
            Value.AddRule(matchMaxRule);
        }


        protected override string GetName()
        {
            var name = (from par in ScoringParameter.Value where par.Id == ScoreKey select ((GapTextRichTextParameter)par.InnerParameters.FirstOrDefault(i => i.Name == GapMatchRichTextScoringParameter.GapControlName)).Value).FirstOrDefault();
            return name;
        }


        private string GetFriendlyNameOfMovableElement(KeyValuePair<string, Dictionary<string, string>> sub)
        {
            string name = string.Empty;
            name = WebUtility.HtmlDecode(sub.Value.FirstOrDefault(t => t.Key == GapMatchScoringParameter.GapMatchLabel).Value);
            if (string.IsNullOrWhiteSpace(name))
            {
                name = sub.Key;
            }
            return WebUtility.HtmlDecode(name);
        }

    }
}
