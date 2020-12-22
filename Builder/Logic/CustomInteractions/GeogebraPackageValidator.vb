Imports System.IO
Imports System.IO.Compression
Imports System.Linq
Imports System.Xml.Linq

Namespace CustomInteractions

    Public Class GeogebraPackageValidator : Inherits PackageValidator

        Const XmlMetadata As String = "geogebra.xml"
        Const GgbAttributeLabel As String = "label"
        Const GgbAttributeKey As String = "exp"
        Const GgbElementIndicator As String = "geo"

        Public Sub New(path As String)
            MyBase.New(path)
        End Sub

        Public Overrides Function TryValidate(ByRef errorMessages As List(Of String)) As Boolean
            Return True
        End Function

        Public Overrides Function GetMetaData() As MetadataRoot
            Dim errorMessages As New List(Of String)()

            Using ggbPackageReader As New StreamReader(Path)
                Using archive As ZipArchive = New ZipArchive(ggbPackageReader.BaseStream)

                    Dim xmlMetadataEntry = archive.Entries.FirstOrDefault(Function(e) String.Equals(e.Name, xmlMetadata, StringComparison.OrdinalIgnoreCase))

                    If xmlMetadataEntry IsNot Nothing Then
                        Using xmlFileReader As StreamReader = New StreamReader(xmlMetadataEntry.Open())
                            Dim metaDataContent As String = xmlFileReader.ReadToEnd()
                            Dim xDoc As XDocument = XDocument.Parse(metaDataContent)
                            Return ReadCustomInteractionMetadata(xDoc.Descendants("expression").Where(Function(n) n.Attributes.Any(Function(a) a.Name.ToString.Equals(ggbAttributeLabel) AndAlso a.Value.StartsWith(ggbElementIndicator))), errorMessages)
                        End Using
                    End If

                End Using
            End Using

            Return Nothing
        End Function

        Friend Shared Function ReadCustomInteractionMetadata(nodes As IEnumerable(Of XElement), ByRef errorMessages As List(Of String)) As MetadataRoot
            Try
                If nodes IsNot Nothing AndAlso nodes.Count > 0 Then
                    Dim metaData As New MetadataRoot()
                    Dim scores As New List(Of MetadataScoring)

                    For Each node In nodes
                        If node.Attributes(ggbAttributeLabel) IsNot Nothing AndAlso Not String.IsNullOrEmpty(node.Attributes(ggbAttributeLabel).First.Value) AndAlso node.Attributes(ggbAttributeKey) IsNot Nothing AndAlso Not String.IsNullOrEmpty(node.Attributes(ggbAttributeKey).First.Value) Then
                            scores.Add(New MetadataScoring() With {.GeogebraScoring = New GeogebraScoring() With {.Label = node.Attributes(ggbAttributeLabel).First.Value, .CorrectResponse = node.Attributes(ggbAttributeKey).First.Value}})
                        End If
                    Next

                    metaData.Scoring = scores
                    metaData.IsValid(errorMessages)
                    Return metaData
                End If
            Catch ex As Exception
                errorMessages.Add($"Error while reading xml metadata file: {ex.Message}")
            End Try
            Return Nothing
        End Function

    End Class

End Namespace