using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class HotTextCorrectionBlockRowViewModel : StringBlockRowViewModel
    {

        public HotTextCorrectionBlockRowViewModel(HotTextCorrectionScoringParameter scoringParameter, IGapScoringManipulator<string> scoreManipulator, string scoreKey, int index)
            : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
        }


        protected override string GetName()
        {
            string name = ((HotTextCorrectionScoringParameter)ScoringParameter).TextToCorrect;

            if (string.IsNullOrEmpty(name))
            {
                name = string.Format("{0}.{1}", base.GetName(), ScoreKey);
            }

            return name;
        }
    }
}
