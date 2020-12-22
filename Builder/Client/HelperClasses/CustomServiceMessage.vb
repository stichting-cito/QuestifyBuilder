Public Class CustomServiceMessage
    Private ReadOnly _messageText As String
    Private ReadOnly _resourceName As String
    Private ReadOnly _params() As Object

    Public Sub New(resourceName As String, ParamArray params() As Object)
        _resourceName = resourceName
        _params = params
    End Sub

    Public Sub New(messageText As String)
        _messageText = messageText
    End Sub

    Public Overrides Function ToString() As String
        If Not String.IsNullOrEmpty(_resourceName) Then
            If (_params IsNot Nothing AndAlso _params.Length > 0) Then
                Return String.Format(My.Resources.ResourceManager.GetObject(_resourceName).ToString(), _params)
            Else
                Return My.Resources.ResourceManager.GetObject(_resourceName).ToString()
            End If
        End If

        Return _messageText
    End Function
End Class
