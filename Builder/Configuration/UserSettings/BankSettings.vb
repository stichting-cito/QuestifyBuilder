Public Class BankSettings


    Public Sub New()
    End Sub

    Public Sub New(bankId As Integer)
        _BankId = bankId
    End Sub



    Public Property BankId As Integer
    Public Property GridSettings As New List(Of GridSettings)

End Class
