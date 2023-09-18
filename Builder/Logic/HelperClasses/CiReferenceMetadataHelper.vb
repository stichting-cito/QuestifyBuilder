Imports System.IO
Imports System.IO.Compression
Imports System.Linq
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Questify.Builder.Logic.CustomInteractions

Namespace HelperClasses

    Public NotInheritable Class CiReferenceMetadataHelper
        Private Sub New()

        End Sub

        Public Shared Function GetReferencesFromJsonManifest(jsonManifestZipEntry As ZipArchiveEntry) As List(Of String)
            Using jsonFileReader = New StreamReader(jsonManifestZipEntry.Open())
                Dim jsonManifestContents = jsonFileReader.ReadToEnd()
                Return GetReferencesFromJsonManifest(jsonManifestContents)
            End Using
        End Function

        Public Shared Function GetReferencesFromJsonManifest(jsonManifestContents As String) As List(Of String)
            Return GetMetadataFromString(jsonManifestContents)?.GetAllReferencedFiles()
        End Function

        Public Shared Function AddCiNameToReferencesInJsonManifest(jsonManifestContents As String, ciNameToAddToReference As String) As String
            Dim metadata = GetMetadataFromString(jsonManifestContents)
            AddCiNameToReferences(metadata.Styles, ciNameToAddToReference)
            AddCiNameToReferences(metadata.Medias, ciNameToAddToReference)
            AddCiNameToReferences(metadata.Scripts, ciNameToAddToReference)

            Return GetStringFromReferenceMetadata(metadata)
        End Function

        Private Shared Sub AddCiNameToReferences(ByRef references As String(), ciNameToAddToReference As String)
            references = references.Select(Function(r) AddCiNameToReference(r, ciNameToAddToReference)).ToArray
        End Sub

        Public Shared Function AddCiNameToReference(reference As String, ciNameToAddToReference As String) As String
            Return String.Concat(Left(reference, 4), ciNameToAddToReference, "/", Right(reference, Len(reference) - 4))
        End Function

        Private Shared Function GetMetadataFromString(jsonManifestContents As String) As ReferenceMetadata
            Try
                Return JsonConvert.DeserializeObject(Of ReferenceMetadata)(jsonManifestContents)
            Catch
                Return Nothing
            End Try
        End Function

        Private Shared Function GetStringFromReferenceMetadata(metadata As ReferenceMetadata) As String
            Return JsonConvert.SerializeObject(metadata)
        End Function

    End Class
End Namespace