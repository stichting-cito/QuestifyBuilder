using System.Linq;
using System.Net;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class HotTextBlockRowViewModel : MultiResponseBlockRowViewModel
    {

        public HotTextBlockRowViewModel(HotTextScoringParameter scoringParameter, IChoiceScoringManipulator scoreManipulator, string scoreKey)
            : base(scoringParameter, scoreManipulator, scoreKey)
        {
        }


        protected override string GetName()
        {
            var subParam = ScoringParameter.Value.FirstOrDefault(x => x.Id == ScoreKey);
            if (subParam != null)
            {
                var contentLabel = subParam.TryGetParameterByName<PlainTextParameter>(HotTextScoringParameter.ContentLabel);
                if (contentLabel != null)
                {
                    return WebUtility.HtmlDecode(contentLabel.Value);
                }
            }

            return string.Format("{0}.{1}", base.GetName(), ScoreKey);
        }
    }
}
