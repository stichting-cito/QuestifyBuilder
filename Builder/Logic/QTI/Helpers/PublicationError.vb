Namespace QTI.Helpers
    Public Class PublicationError
        Public Property Message As String
        Public Property ExceptionType As String
        Public Property EntityProcessed As String

        Public Sub New(message As String, exceptionType As String, entityProcessed As String)
            _Message = message
            _ExceptionType = exceptionType
            _EntityProcessed = entityProcessed
        End Sub

        Public Sub New()
        End Sub
    End Class
End NameSpace