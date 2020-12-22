Public Class Factset
    Public Sub New()
        facts = New HashSet(Of Fact)(New factEqualityComparer())
    End Sub
    Public ReadOnly Facts As HashSet(Of Fact)
End Class
