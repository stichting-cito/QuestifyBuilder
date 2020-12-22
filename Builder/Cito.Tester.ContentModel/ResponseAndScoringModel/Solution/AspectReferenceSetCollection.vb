Public Class AspectReferenceSetCollection
    Inherits List(Of AspectReferenceCollection)

    Public Function GetMaxScore() As Integer
        Dim maxScore As Integer = 0
        For Each aspectCollection As AspectReferenceCollection In Me
            maxScore += aspectCollection.GetMaxScore()
        Next
        Return maxScore
    End Function
End Class
