Imports System.Net
Imports Cito.Tester.Common

Public Class HttpProtocolHandler
    Implements IResourceReader

    Public Function GetStream(uri As Uri) As StreamResource Implements IResourceReader.GetStream
        Dim request As WebRequest = WebRequest.Create(uri)
        Dim response As WebResponse = request.GetResponse()

        Return New StreamResource(response.GetResponseStream, response.ContentLength)
    End Function

    Public Function GetReadOnlyStream(uri As Uri) As StreamResource Implements IResourceReader.GetReadonlyStream
        Throw New NotSupportedException()
    End Function

End Class
