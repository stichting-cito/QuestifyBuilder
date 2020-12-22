using System;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    public interface IBaseScoreFactoryTest
    {
        void Can_Handle_SpecificScoringParameterParameter();
        void CanNot_Handle_NULL_ScoringParameter();
        void CanNot_Handle_someOtherScoreParam();
        void WorkspaceData_IsUsableByViewModel();

        Func<Cinch.TestViewAwareStatus, object> CreateViewModel { get; set; }
    }
}
