Namespace ContentModel.Scoring
    Public Interface IChoiceScoringManipulator : Inherits IScoreManipulator

        Sub Clear()

        Sub SetKey(key As String)

        Function GetKeyStatus() As IDictionary(Of String, Boolean)

        Sub RemoveKey(key As String)

        Sub SetKeyWithDefaultValue(key As String)

        Function IsValid(ByVal value As String) As Boolean

    End Interface
End Namespace