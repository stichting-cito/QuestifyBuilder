using System;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    internal interface IBlockRowViewModel
    {
        bool CorrectInteractionResponseIsDefinedByOneDistinctValue { get; }

        Cinch.DataWrapper<GapComparisonType> ComparisonType { get; }

        int Index { get; set; }

        int? FactSetNumber { get; }

        string Name { get; }

        Type ScoringParameterType { get; }

        string ScoreKey { get; }

        ScoringParameter ScoringParameter { get; }

        bool IsMatch(int? factSetNumber, ScoringMapKey scoringMapKey);

        void RemoveValueFromScore();

        void SetValueOnStartingEdit();
    }
}
