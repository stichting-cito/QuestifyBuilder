Imports Cito.Tester.Common

Public NotInheritable Class ProtocolHandlerFactory

    <Obsolete("GetHandler is obsolete use 'GetReaderHandler' instead.", False)> _
    Public Shared Function GetHandler(scheme As String) As IResourceReader
        Return GetReaderHandler(scheme)
    End Function

    Public Shared Function GetReaderHandler(scheme As String) As IResourceReader
        Dim handler As IResourceReader = Nothing

        Select Case scheme
            Case "file"
                handler = New FileProtocolHandler
            Case "http"
                handler = New HttpProtocolHandler
            Case Else
                Throw New NotImplementedException
        End Select
        Return handler
    End Function

    Public Shared Function GetWriterHandler(scheme As String) As IResourceWriter
        Dim handler As IResourceWriter = Nothing

        Select Case scheme
            Case "file"
                handler = New FileProtocolHandler
            Case Else
                Throw New NotImplementedException
        End Select
        Return handler
    End Function

    Private Sub New()
    End Sub
End Class
