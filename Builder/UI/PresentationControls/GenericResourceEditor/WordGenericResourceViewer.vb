Imports System.IO
Imports Cito.Tester.Common

Public Class WordGenericResourceViewer
    Private _previewFragmentPath As String = String.Empty

    Protected Overrides Sub DataBind()
        If Me.IsResourceDataAvailable Then
            _previewFragmentPath = Path.Combine(TempStorageHelper.GetTempStoragePath, String.Format("{0}.{1}", Guid.NewGuid.ToString, IO.Path.GetExtension(Me.DataSource.Name)))
            If FileHelper.MakeFileFromByteArray(_previewFragmentPath, DirectCast(Me.DataSource.ResourceData.BinData, Byte())) Then
                WordFragmentPreviewPanel.PreviewSource = _previewFragmentPath
            End If
        End If

        Me.Refresh()
    End Sub


    Private Sub CleanUpPreviewFragment()
        TempStorageHelper.CleanTempStorage(New DirectoryInfo(_previewFragmentPath))
    End Sub

End Class
