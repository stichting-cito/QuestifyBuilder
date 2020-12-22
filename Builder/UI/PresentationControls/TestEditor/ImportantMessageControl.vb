Public Class ImportantMessageControl

    Public Event Close(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub ButtonClose_Click(sender As System.Object, e As System.EventArgs) Handles ButtonClose.Click
        RaiseEvent Close(Me, New System.EventArgs)
    End Sub

    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
            TestEditorMessage.Text = _message
        End Set
    End Property

End Class
