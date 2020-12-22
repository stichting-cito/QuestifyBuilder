

Namespace ContentModel.Scoring
    Public Interface IChoiceArrayScoringManipulator : Inherits IScoreManipulator

        Sub Clear()

        Sub SetKey(key As String, value As NoValueType(Of String))

        Function GetKeyStatus() As IDictionary(Of String, NoValueType(Of String))

        Sub RemoveKey(key As String)

    End Interface
End Namespace



