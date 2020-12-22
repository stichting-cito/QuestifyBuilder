Imports System.IO
Imports Questify.Builder.Logic.Service.Factories

Public Class NoPreviewResourceViewer

    Dim _tempFileNames As List(Of String) = New List(Of String)

    Private Sub PlayFile()
        If Not IsResourceDataAvailable Then
            Dim resourceData = ResourceFactory.Instance.GetResourceData(Me.DataSource)
            Me.DataSource.ResourceData = resourceData
        End If

        Dim tempFile = SaveAsTempFile()
        If (tempFile Is Nothing) Then
            Return
        End If
        _tempFileNames.Add(tempFile)
        Try
            Process.Start(tempFile)
        Catch ex As System.ComponentModel.Win32Exception
            Dim msg As String = $"Could not open this file - no application is associated with files of type '{Path.GetExtension(tempFile)}'"
            MsgBox(msg, MsgBoxStyle.Critical, "No application associated")
        End Try
    End Sub

    Private Function SaveAsTempFile() As String
        If Not IsResourceDataAvailable Then
            Return Nothing
        End If

        Dim tempFileName = Path.GetTempFileName()
        Dim extension As String = Path.GetExtension(Me.DataSource.ResourceData.Resource.Name)
        tempFileName = Path.ChangeExtension(tempFileName, extension)
        File.WriteAllBytes(tempFileName, Me.DataSource.ResourceData.BinData())
        Return tempFileName
    End Function

    Protected Overrides Sub DataBind()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If

        For Each tempFileName As String In _tempFileNames
            Try
                File.Delete(tempFileName)
                File.Delete(Path.ChangeExtension(tempFileName, "tmp"))
            Catch ex As Exception
            End Try
        Next

        MyBase.Dispose(disposing)
    End Sub

    Private Sub OpenButton_Click(sender As Object, e As EventArgs) Handles OpenButton.Click
        PlayFile()
    End Sub
End Class
