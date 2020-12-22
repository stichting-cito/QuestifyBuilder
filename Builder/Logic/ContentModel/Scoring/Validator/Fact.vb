Public Class Fact
    Public Sub New()
        Values = New HashSet(Of value)(New valueEqualityComparer())
    End Sub
    Public Id As String
    Public ReadOnly Values As HashSet(Of Value)
End Class