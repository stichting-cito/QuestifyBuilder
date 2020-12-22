Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Friend NotInheritable Class ScoreParameterAdapter
        Private Sub New()
        End Sub
        Private Shared ReadOnly KnownAdapters As Dictionary(Of Type, IScoreParameterAdapter)

        Shared Sub New()
            KnownAdapters = New Dictionary(Of Type, IScoreParameterAdapter)() From { _
                {GetType(MathMlScoring), New MathMlAdapter()}, _
                {GetType(IntegerScoring), New IntegerAdapter()}, _
                {GetType(DecimalScoring), New DecimalAdapter()}, _
                {GetType(ChoiceScoring), New ChoiceAdapter()}, _
                {GetType(CoordinateScoring), New CoordinateAdapter()}, _
                {GetType(GeogebraScoring), New GeogebraAdapter()} _
            }
        End Sub

        Public Shared Function Adapt(sequenceNr As Integer, scoringTypeBase As ScoringTypeBase, findingOverride As String, controllerId As String, scoringLabel As String) As ScoringParameter
            Dim key = scoringTypeBase.[GetType]()
            Dim adapter As IScoreParameterAdapter

            If KnownAdapters.TryGetValue(key, adapter) Then
                Return adapter.Adapt(sequenceNr, scoringTypeBase, findingOverride, controllerId, scoringLabel)
            End If

            Debug.Assert(False)
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
