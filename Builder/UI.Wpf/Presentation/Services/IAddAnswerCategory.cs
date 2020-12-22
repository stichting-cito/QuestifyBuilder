using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    internal interface IAddAnswerCategory
    {

        void AddAnswerCategory(CombinedScoringMapKey combinedScoringMapKey, Solution solution);

    }
}
