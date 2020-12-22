using System;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.DataTemplate
{
    class BlockRowViewModel_Without_DataTemplate : IBlockRowViewModel
    {
        public bool CorrectInteractionResponseIsDefinedByOneDistinctValue
        {
            get { throw new NotImplementedException(); }
        }

        public Cinch.DataWrapper<GapComparisonType> ComparisonType
        {
            get { throw new NotImplementedException(); }
        }

        public int Index
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public Type ScoringParameterType
        {
            get { throw new NotImplementedException(); }
        }

        public string ScoreKey
        {
            get { throw new NotImplementedException(); }
        }

        public ScoringParameter ScoringParameter
        {
            get { throw new NotImplementedException(); }
        }

        public int? FactSetNumber
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsMatch(int? factSetNumber, ScoringMapKey scoringMapKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveValueFromScore()
        {
            throw new NotImplementedException();
        }

        public void SetValueOnStartingEdit()
        {
            throw new NotImplementedException();
        }
    }
}
