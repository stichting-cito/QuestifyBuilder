
Namespace ContentModel.Scoring
    Public Interface IOrderScoringManipulator : Inherits IScoreManipulator

        Sub Clear()

        Sub SetKey(key As String, order As Integer)

        Function GetKeyStatus() As IDictionary(Of String, Integer)

        Sub RemoveKey(key As String)


        Function IsValid(ByVal key As Integer) As Boolean

    End Interface
End Namespace