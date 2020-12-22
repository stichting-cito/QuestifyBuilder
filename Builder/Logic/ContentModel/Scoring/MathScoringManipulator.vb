Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Friend Class MathScoringManipulator
        Inherits GapScoringManipulatorDecorator(Of MathScoringParameter, String)

        Public Sub New(param As MathScoringParameter, decoree As IGapScoringManipulator(Of String))
            MyBase.New(param, decoree)
        End Sub

        Protected Overrides Function GetDisplayValueForKey(ByVal key As String) As String
            Return My.Resources.Formula
        End Function

    End Class
End Namespace