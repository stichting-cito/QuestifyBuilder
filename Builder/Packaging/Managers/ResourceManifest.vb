Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Text
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class ResourceManifest

    Private _Resources As ResourceEntryCollection
    Private _publicationMetaData As PublicationMetaData


    Public Shared Function Load(manifestStream As Stream) As ResourceManifest
        Dim manifest As ResourceManifest

        Try
            If manifestStream Is Nothing Then
                Throw New ResourceManagerException(My.Resources.Error_ResourceManifest_Load_StreamNotSet)
            End If

            manifest = DirectCast(SerializeHelper.XmlDeserializeFromStream(manifestStream, GetType(ResourceManifest)), ResourceManifest)

            Return manifest
        Catch ex As Exception
            Debug.Write(ex.ToString())
        Finally
            manifestStream.Close()
        End Try
    End Function

    Public Shared Function Load(manifestUri As Uri) As ResourceManifest
        Dim resourceManager As New ManifestResourceManager(Nothing, Nothing, Nothing, Nothing)
        Dim manifestStream As StreamResource = Nothing
        Dim manifest As ResourceManifest

        Try
            manifestStream = resourceManager.GetStream(manifestUri, True)
            manifest = Load(manifestStream.ResourceObject)

        Catch ex As Exception
            Throw New ResourceException("Error while loading resource manifest.", ex)
        Finally
            If manifestStream IsNot Nothing Then
                manifestStream.CloseStream()
            End If

            resourceManager.Dispose()
        End Try

        Return manifest
    End Function

    Public Shared Function PreLoad(manifestUri As Uri) As ResourceManifest
        Dim resourceManager As New ManifestResourceManager(Nothing, Nothing, Nothing, Nothing)
        Dim manifestStream As StreamResource
        Dim manifest As ResourceManifest = Nothing

        Try
            manifestStream = resourceManager.GetStream(manifestUri, True)

            If manifestStream IsNot Nothing Then
                manifest = Load(manifestStream.ResourceObject)
            End If

        Catch ex As Exception
            Throw New ResourceException("Error while loading resource manifest.", ex)
        Finally
            resourceManager.Dispose()
        End Try

        Return manifest
    End Function

    <SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")> _
    Public Shared Function Load(manifestPath As String) As ResourceManifest
        Return Load(File.Open(manifestPath, FileMode.Open))
    End Function

    Public Shared Sub Save(manifestUri As Uri, manifest As ResourceManifest, resourceManager As ManifestResourceManager)
        ReflectionHelper.CheckIsNotNothing(resourceManager, "Resource manager")

        Dim manifestStream As StreamResource

        Dim temp As String = SerializeHelper.XmlSerializeToString(manifest)
        Dim bytes() As Byte = UnicodeEncoding.Unicode.GetBytes(temp)
        Dim Stream As New MemoryStream(bytes)

        manifestStream = New StreamResource(GetFilename(manifestUri), "manifest", False, Stream, Nothing)
        resourceManager.PutResource(manifestStream)
    End Sub




    Private Shared Function GetFilename(uri As Uri) As String
        Return uri.Segments.GetValue(uri.Segments.Length - 1).ToString
    End Function

    Private Shared Function getBaseUri(uri As Uri) As Uri
        Dim tempUri As String = uri.ToString
        Dim baseUriString As String = tempUri.Replace(uri.Segments.GetValue(uri.Segments.Length - 1).ToString, "")
        Return New Uri(baseUriString)
    End Function



    Public Sub New()
        Me._Resources = New ResourceEntryCollection
        Me.PublicationMetaData = PublicationMetaData.CreatePublicationMetaData
    End Sub



    Public Property PublicationMetaData() As PublicationMetaData
        Get
            Return _publicationMetaData
        End Get
        Set(value As PublicationMetaData)
            _publicationMetaData = value
        End Set
    End Property

    Public ReadOnly Property Resources() As ResourceEntryCollection
        Get
            Return Me._Resources
        End Get
    End Property



    Public Sub Validate(baseUri As Uri)
        If Me.Resources IsNot Nothing Then

            For Each resource As ResourceEntry In Me.Resources
                Dim resourceUri As New Uri(resource.Uri, UriKind.RelativeOrAbsolute)

                If resourceUri.IsAbsoluteUri Then
                    PackageManager.ValidateResource(baseUri, resourceUri)
                End If

            Next
        End If
    End Sub



End Class