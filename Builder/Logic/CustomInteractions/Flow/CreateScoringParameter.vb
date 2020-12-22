Imports System.Activities
Imports Cito.Tester.ContentModel

Namespace CustomInteractions.Flow

    Public NotInheritable Class CreateScoringParameter
        Inherits CodeActivity(Of ScoringParameter)

        Public Property CustomInterActionScoreParameter As InArgument(Of ScoringTypeBase)
        Public Property Counter As InArgument(Of Integer)

        Public Property FindingOverride As InArgument(Of String)

        Public Property ControllerId As InArgument(Of String)

        Public Property ScoringLabel As InArgument(Of String)

        Protected Overrides Function Execute(context As CodeActivityContext) As ScoringParameter
            Dim toConvert As ScoringTypeBase = context.GetValue(CustomInterActionScoreParameter)
            Dim nr As Integer = context.GetValue(Counter)
            Dim findOverride As String = context.GetValue(FindingOverride)
            Dim contId As String = context.GetValue(ControllerId)
            Dim scoreLabel As String = context.GetValue(ScoringLabel)

            Return ScoreParameterAdapter.Adapt(nr, toConvert, findOverride, contId, scoreLabel)
        End Function
    End Class
End Namespace