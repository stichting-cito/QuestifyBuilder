Imports System.IO.Compression
Imports System.IO
Imports Cito.Tester.Common

Public Class LogicFileHelper

    Public Const QuestifyThemeMimeType As String = "application/x-zip-questify-theme"
    Private Const MaxImageWidth = 1920
    Private Const MaxImageHeight = 1080

    Public Shared Function FileIsQuestifyTheme(ByVal fileName As String) As Boolean
        Dim errorMessages As New List(Of String)
        Return FileIsQuestifyTheme(fileName, errorMessages)
    End Function

    Public Shared Function FileIsQuestifyTheme(ByVal fileName As String, ByRef errorMessages As List(Of String)) As Boolean
        Dim isQuestifyTheme As Boolean = False
        Try
            Using archive As ZipArchive = ZipFile.OpenRead(fileName)
                For Each file As ZipArchiveEntry In archive.Entries
                    If file.FullName.Equals("manifest.json") Then
                        isQuestifyTheme = True
                        Exit For
                    End If
                Next
            End Using
        Catch e As FileNotFoundException
            errorMessages.Add($"File '{fileName}' not found")
        Catch e As NotSupportedException
            errorMessages.Add($"File '{fileName}' contains an invalid format")
        Catch e As InvalidDataException
            errorMessages.Add($"File '{fileName}' could not be interpreted as a zip archive")
        Catch e As Exception
            errorMessages.Add($"Error while validating file '{fileName}'")
        End Try

        Return errorMessages.Count = 0 AndAlso isQuestifyTheme
    End Function

    Public Shared Function ValidateGenericResourceToBeImportedIntoBank_FileExtension(selectedFileUri As Uri, ByRef rawBytes As Byte(), code As String) As String
        If rawBytes Is Nothing Then
            If FileHelper.HasMatchingFileExtensionAndMimeType(code) Then
                Return String.Empty
            End If
            rawBytes = FileHelper.MakeByteArrayFromFile(selectedFileUri.LocalPath)
        End If

        If Not FileHelper.HasMatchingFileExtensionAndMimeType(code, rawBytes) Then
            Return My.Resources.LogicFileHelper_CodeMissingOrIncorrectExtension
        End If

        Return String.Empty
    End Function

    Public Shared Function ValidateGenericResourceToBeImportedIntoBank_FileSize(selectedFileUri As Uri, rawBytes As Byte(), mediaType As String, Optional ByRef knownFileSizes As Dictionary(Of Uri, System.Drawing.Size) = Nothing) As String
        If FileHelper.IsImage(selectedFileUri.LocalPath) Then
            Dim size As System.Drawing.Size = Nothing
            If knownFileSizes IsNot Nothing AndAlso knownFileSizes.ContainsKey(selectedFileUri) Then
                size = knownFileSizes(selectedFileUri)
            End If
            If size = Nothing Then
                If rawBytes Is Nothing Then rawBytes = FileHelper.MakeByteArrayFromFile(selectedFileUri.LocalPath)
                size = New MediaDimensionsHelper().GetDimensions(mediaType, rawBytes)
                If knownFileSizes IsNot Nothing AndAlso Not knownFileSizes.ContainsKey(selectedFileUri) Then knownFileSizes.Add(selectedFileUri, size)
            End If

            If size.Height > MaxImageHeight OrElse size.Width > MaxImageWidth Then
                Return String.Format(My.Resources.LogicFileHelper_ImageSizeTooLarge, size.Width, size.Height, MaxImageWidth, MaxImageHeight)
            End If
        End If

        Return String.Empty
    End Function
End Class
