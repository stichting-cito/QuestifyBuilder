Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports Cito.Tester.Common

Public Class FileProtocolHandler
    Implements IResourceReader, IResourceWriter

    Public Function GetStream(uri As Uri) As StreamResource Implements IResourceReader.GetStream
        Return GetStream(uri, FileAccess.ReadWrite)
    End Function

    Public Function GetReadonlyStream(uri As Uri) As StreamResource Implements IResourceReader.GetReadonlyStream
        Return GetStream(uri, FileAccess.Read)
    End Function

    Private Function GetStream(uri As Uri, fileAccess As FileAccess) As StreamResource
        If uri Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_UriNotSet)
        End If

        Dim stream As FileStream

        If File.Exists(uri.LocalPath) Then
            stream = File.Open(uri.LocalPath, FileMode.Open, fileAccess, FileShare.ReadWrite)
            Return New StreamResource(stream, stream.Length)
        Else
            Return Nothing
        End If
    End Function

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
    Public Sub PutStream(uri As Uri, stream As StreamResource) Implements IResourceWriter.PutStream
        If uri Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_UriNotSet)
        End If

        ReflectionHelper.CheckIsNotNothing(stream, "Resource stream")

        Dim destinationStream As FileStream
        destinationStream = File.Create(uri.LocalPath, 1024, FileOptions.WriteThrough)

        Dim buffer(1023) As Byte
        Dim count As Integer = buffer.Length

        Do
            count = stream.ResourceObject.Read(buffer, 0, count)
            If count = 0 Then Exit Do
            destinationStream.Write(buffer, 0, count)
        Loop

        destinationStream.Close()

    End Sub
End Class
