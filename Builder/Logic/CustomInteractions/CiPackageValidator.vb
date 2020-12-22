Imports System.IO
Imports System.IO.Compression
Imports System.Linq
Imports Cito.Tester.Common
Imports Newtonsoft.Json

Namespace CustomInteractions

    Public Class CiPackageValidator : Inherits PackageValidator

        Const jsonManifestPath As String = "ref/json"
        Const jsonMetadata As String = "metadata.json"

        Public Sub New(path As String)
            MyBase.New(path)
        End Sub

        Public Overrides Function TryValidate(ByRef errorMessages As List(Of String)) As Boolean
            Dim metaData As MetadataRoot = Nothing
            Return TryValidate(errorMessages, metaData)
        End Function

        Public Overrides Function TryValidate(ByRef errorMessages As List(Of String), ByRef metaData As MetadataRoot) As Boolean
            errorMessages = Validate(errorMessages, metaData, True)
            Return errorMessages.Count = 0
        End Function

        Public Function Validate(errorMessages As List(Of String), ByRef metaData As MetadataRoot, ByVal jsonManifestRequired As Boolean) As List(Of String)
            Try
                Using ciPackageReader As New StreamReader(Path)
                    Using archive As ZipArchive = New ZipArchive(ciPackageReader.BaseStream)
                        If jsonManifestRequired Then
                            ValidateJsonManifest(archive, errorMessages)
                        End If

                        metaData = GetMetaDataFromArchive(archive, errorMessages)
                    End Using
                End Using
            Catch ex As Exception
                errorMessages.Add($"Error while trying to validate custom interaction resource: {ex.Message}")
            End Try
            Return errorMessages
        End Function

        Private Sub ValidateJsonManifest(archive As ZipArchive, errorMessages As List(Of String))

            Dim jsonEntryManifest = archive.Entries.FirstOrDefault(Function(e) e.FullName.StartsWith(jsonManifestPath + "/") And Not String.IsNullOrEmpty(e.Name))

            If jsonEntryManifest IsNot Nothing Then
                ValidateReferences(archive, jsonEntryManifest, errorMessages)
            Else
                errorMessages.Add(String.Format(My.Resources.MissingJsonManifest, jsonManifestPath))
            End If
        End Sub

        Public Function FileIsPci(ByRef errorMessages As List(Of String)) As Boolean
            Return FileIsPci(errorMessages, Nothing)
        End Function

        Public Function FileIsPci(ByRef errorMessages As List(Of String), ByRef metadata As MetadataRoot) As Boolean
            errorMessages = Validate(errorMessages, metadata, False)

            Return Not errorMessages.Any() AndAlso metadata IsNot Nothing AndAlso metadata.Modules.Any() AndAlso Not String.IsNullOrEmpty(metadata.TypeIdentifier)
        End Function

        Public Function FileIsCi(ByRef errorMessages As List(Of String)) As Boolean
            Dim metaData As MetadataRoot = Nothing
            Return FileIsCi(errorMessages, metaData)
        End Function

        Public Function FileIsCi(ByRef errorMessages As List(Of String), ByRef metaData As MetadataRoot) As Boolean
            errorMessages = Validate(errorMessages, metaData, True)
            Return metaData IsNot Nothing AndAlso errorMessages.Count = 0
        End Function

        Public Overrides Function GetMetaData() As MetadataRoot
            Dim errorMessages As New List(Of String)()

            Using ciPackageReader As New StreamReader(Path)
                Using archive As ZipArchive = New ZipArchive(ciPackageReader.BaseStream)
                    Return GetMetaDataFromArchive(archive, errorMessages)
                End Using
            End Using
        End Function

        Protected Function GetMetaDataFromArchive(archive As ZipArchive, ByRef errorMessages As List(Of String)) As MetadataRoot
            Dim jsonMetadataEntry = archive.Entries.FirstOrDefault(Function(e) String.Equals(e.Name, jsonMetadata, StringComparison.OrdinalIgnoreCase))
            If jsonMetadataEntry IsNot Nothing Then
                Using jsonFileReader As StreamReader = New StreamReader(jsonMetadataEntry.Open())
                    Dim metaDataContent As String = jsonFileReader.ReadToEnd()
                    Return ReadCustomInteractionMetadata(metaDataContent, errorMessages)
                End Using
            Else
                errorMessages.Add($"Could not find json metadata file.")
                Return Nothing
            End If
        End Function

        Private Sub ValidateReferences(archive As ZipArchive, jsonEntryManifest As ZipArchiveEntry, errorMessages As List(Of String))

            Using jsonFileReader As StreamReader = New StreamReader(jsonEntryManifest.Open())
                Dim jsonFileContents As String = jsonFileReader.ReadToEnd()
                Dim jsonManifestReferences As List(Of String) = PublicationRegExHelper.GetReferencesFromJsonManifest(jsonFileContents)
                For Each referencedFileName As String In jsonManifestReferences
                    Dim referencedFile = archive.GetEntry(referencedFileName)
                    If referencedFile Is Nothing Then
                        errorMessages.Add(String.Format(My.Resources.ReferencedFileNotFoundInJsonManifest, jsonEntryManifest.FullName, referencedFileName, Path))
                    End If
                Next
            End Using

        End Sub

        Friend Shared Function ReadCustomInteractionMetadata(metadataContent As String, ByRef errorMessages As List(Of String)) As MetadataRoot
            Try
                Dim jsonObject = JsonConvert.DeserializeObject(Of MetadataRoot)(metadataContent)
                jsonObject.IsValid(errorMessages)
                Return jsonObject
            Catch ex As Exception
                errorMessages.Add($"Error while reading json metadata file: {ex.Message}")
            End Try
            Return Nothing
        End Function
    End Class
End Namespace