Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class ResourceMetaDataManifest


    Private _resourceReferences As ResourceManifestMetaDataEntryReferenceCollection
    Private _customPropertyDefinitions As ResourceManifestMetadataDefinitionCollection



    Public ReadOnly Property ResourceReferences() As ResourceManifestMetaDataEntryReferenceCollection
        Get
            Return Me._resourceReferences
        End Get
    End Property

    <XmlArrayItem("singleValueDefinition", GetType(ResourceManifestMetadataSingleValueDefinition))> _
    <XmlArrayItem("multiValueDefinition", GetType(ResourceManifestMetadataMultiValueDefinition))> _
    <XmlArrayItem("conceptStructureDefinition", GetType(ResourceManifestMetaDataConceptStructureDefinition))> _
    <XmlArrayItem("treeStructureDefinition", GetType(ResourceManifestMetaDataTreeStructureDefinition))> _
    Public ReadOnly Property CustomPropertyDefinitions() As ResourceManifestMetadataDefinitionCollection
        Get
            Return _customPropertyDefinitions
        End Get
    End Property



    Public Shared Function Load(manifestStream As Stream) As ResourceMetaDataManifest
        Dim manifest As ResourceMetaDataManifest

        If manifestStream Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_ResourceManifest_Load_StreamNotSet)
        End If

        manifest = DirectCast(SerializeHelper.XmlDeserializeFromStream(manifestStream, GetType(ResourceMetaDataManifest)), ResourceMetaDataManifest)
        manifestStream.Close()
        Return manifest
    End Function

    Public Shared Function Load(manifestUri As Uri) As ResourceMetaDataManifest
        Dim resourceManager As New ManifestResourceManager(Nothing, Nothing, Nothing, Nothing)
        Return Load(resourceManager, manifestUri, True)
    End Function

    Public Shared Function Load(resourceManager As ManifestResourceManager, manifestUri As Uri) As ResourceMetaDataManifest
        Return Load(resourceManager, manifestUri, False)
    End Function

    Private Shared Function Load(resourceManager As ManifestResourceManager, manifestUri As Uri, disposeResourceManager As Boolean) As ResourceMetaDataManifest
        Dim manifestStream As StreamResource = Nothing
        Dim ManifestMetaData As ResourceMetaDataManifest

        Try
            manifestStream = resourceManager.GetStream(manifestUri, True)
            ManifestMetaData = Load(manifestStream.ResourceObject)

        Catch notFound As FileNotFoundException
            ManifestMetaData = New ResourceMetaDataManifest
        Catch ex As Exception
            Throw New ResourceException("Error while loading resource manifest metadata.", ex)
        Finally
            If manifestStream IsNot Nothing Then
                manifestStream.CloseStream()
            End If

            If disposeResourceManager Then
                resourceManager.Dispose()
            End If
        End Try

        Return ManifestMetaData
    End Function

    Public Shared Sub Save(manifestUri As Uri, manifestMetaData As ResourceMetaDataManifest, resourceManager As ManifestResourceManager)
        ReflectionHelper.CheckIsNotNothing(resourceManager, "Resource manager")

        Dim manifestStream As StreamResource

        Dim temp As String = SerializeHelper.XmlSerializeToString(manifestMetaData)
        Dim bytes() As Byte = UnicodeEncoding.Unicode.GetBytes(temp)
        Dim Stream As New MemoryStream(bytes)

        manifestStream = New StreamResource(GetFilename(manifestUri), "manifestMetaData", False, Stream, Nothing)
        resourceManager.PutResource(manifestStream)

    End Sub

    Private Shared Function GetFilename(uri As Uri) As String
        Return uri.Segments.GetValue(uri.Segments.Length - 1).ToString
    End Function



    Public Sub New()
        _resourceReferences = New ResourceManifestMetaDataEntryReferenceCollection
        _customPropertyDefinitions = New ResourceManifestMetadataDefinitionCollection
    End Sub


End Class
