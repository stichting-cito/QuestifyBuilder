Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Interface IScoreParameterAdapter
        Function Adapt(parameterNr As Integer, input As ScoringTypeBase, findingOverride As String, controllerId As String, scoringLabel As String) As ScoringParameter
    End Interface
End Namespace
