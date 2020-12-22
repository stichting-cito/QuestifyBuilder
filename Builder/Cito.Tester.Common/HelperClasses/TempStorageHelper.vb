Imports System.IO
Imports System.Threading

Public Class TempStorageHelper
    Const FOLDER_NAME As String = "Q_TEMP"


    Public Shared Function GetTempStoragePath(createIfNotExist As Boolean) As String
        Dim qTemp As String = Path.Combine(Path.GetTempPath, FOLDER_NAME)
        If createIfNotExist AndAlso Not Directory.Exists(qTemp) Then
            Directory.CreateDirectory(qTemp)
        End If
        Return qTemp
    End Function


    Public Shared Function GetTempFilename() As String
        Return Path.Combine(GetTempStoragePath(), Path.GetFileName(Path.GetTempFileName()))
    End Function


    Public Shared Function GetTempStoragePath() As String
        Return GetTempStoragePath(True)
    End Function


    Public Shared Function GetTemporaryFileName(extension As String) As String
        Dim tmpFilename = Path.GetTempFileName()

        If Not String.IsNullOrEmpty(extension) Then
            tmpFilename = Path.ChangeExtension(tmpFilename, extension)
        End If

        Return Path.GetFileName(tmpFilename)
    End Function




    Public Shared Function CleanTempStorage(directory As DirectoryInfo) As Boolean
        Dim returnValue As Boolean = True
        If directory IsNot Nothing AndAlso IO.Directory.Exists(directory.FullName) Then
            Dim attempt As Integer = 0
            Do
                Try
                    directory.Delete(True)
                    Exit Do
                Catch ex As IOException When attempt < 3
                    Thread.Sleep(250)
                Catch ex As Exception
                    returnValue = False
                    Exit Do
                End Try
                attempt += 1
            Loop
            If Not returnValue Then
                Try
                    For Each File As FileInfo In directory.GetFiles
                        Try
                            If File.Exists Then
                                IO.File.SetAttributes(File.FullName, FileAttributes.Normal)
                                File.Delete()
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    For Each recDirectory As DirectoryInfo In directory.GetDirectories
                        returnValue = CleanTempStorage(recDirectory)
                    Next
                    directory.Delete()
                Catch ex As Exception
                End Try
            End If
        End If
        Return returnValue
    End Function


    Public Shared Function CleanTempStorage() As Boolean
        Dim directory As New DirectoryInfo(GetTempStoragePath(False))
        CleanTempStorage(directory)
    End Function


    Public Shared Function GetValidFolderNameFromString(input As String) As String
        For Each c As Char In Path.GetInvalidFileNameChars()
            input = input.Replace(c, "_"c)
        Next
        Return input
    End Function


    Public Shared Function CopyResourceToTempFolder(resourceName As String, resourceData As Byte(), itemIdentifier As String) As String
        Dim fileName As String = String.Empty
        If Not String.IsNullOrEmpty(itemIdentifier) Then
            Dim qTemp As String = Path.Combine(Path.Combine(Path.GetTempPath(), FOLDER_NAME), itemIdentifier)
            fileName = Path.Combine(qTemp, GetValidFolderNameFromString(resourceName))
            Dim qTempItemdDirectory As New DirectoryInfo(qTemp)
            If Not qTempItemdDirectory.Exists Then
                qTempItemdDirectory.Create()
            End If
            If Not File.Exists(fileName) Then
                FileHelper.MakeFileFromByteArray(fileName, resourceData)
            End If
        End If
        Return fileName
    End Function

End Class
