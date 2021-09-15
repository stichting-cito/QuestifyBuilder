Imports System.Collections.Concurrent
Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Model.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.BaseClasses
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30.loose
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType

Namespace QTI.Requests.QTI30

    Public Class PublicationRequest
        Inherits PublicationRequestBase
        Implements IDisposable

        Private _validationFunction As ValidationEventHandler

        Public Sub New(ByVal sourcePackage As FileInfo, ByVal targetPackageFileSystemInfo As FileInfo, validationFunction As ValidationEventHandler)
            MyBase.New(sourcePackage, targetPackageFileSystemInfo)
            _validationFunction = validationFunction

            InitialiseManifest()
            InitialiseLists()
            Settings = New XmlReaderSettings
            If _validationFunction IsNot Nothing Then
                AddHandler Settings.ValidationEventHandler, _validationFunction
            End If

        End Sub

        Public Property Manifest() As ManifestType
        Public Property Resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))
        Public Property FilesPerEntity As ConcurrentDictionary(Of String, List(Of String))
        Public Property Settings As XmlReaderSettings
        Public Property Tests As Dictionary(Of String, TestDocumentSet)
        Public Property ListOfItems As ConcurrentDictionary(Of String, PublishedItem)
        Public Property GeneratedCssContent As New ConcurrentBag(Of String)
        Public Property FileTypeDictionary As ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
        Public Property ResourceTypeDictionary As ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType)
        Public Property ResourceMimeTypeDictionary As ConcurrentDictionary(Of String, String)
        Public Property ResourcesRetrieved As ConcurrentBag(Of String)
        Public Property ValidationLock As New Object

        Protected Overridable Sub InitialiseLists()
            _FilesPerEntity = New ConcurrentDictionary(Of String, List(Of String))
            _Tests = New Dictionary(Of String, TestDocumentSet)
            _ListOfItems = New ConcurrentDictionary(Of String, PublishedItem)
            _FileTypeDictionary = GetFileTypesDictionary()
            _ResourceTypeDictionary = GetResourceTypeDictionary()
            _ResourceMimeTypeDictionary = New ConcurrentDictionary(Of String, String)
            _ResourcesRetrieved = New ConcurrentBag(Of String)
        End Sub

        Protected Overridable Sub InitialiseManifest()
            _Resources = New ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))
            _Manifest = New ManifestType
            _Manifest.identifier = $"MANIFEST-{Guid.NewGuid.ToString}"

            _Manifest.metadata = New ManifestMetadataType
            _Manifest.metadata.schema = ManifestMetadataTypeSchema.QTIPackage
            _Manifest.metadata.schemaversion = ManifestMetadataTypeSchemaversion.Item300

            _Manifest.metadata.lom = New LOMType

            _Manifest.resources = New ResourcesType
        End Sub

        Protected Function GetFileTypesDictionary() As ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
            Dim returnValue As New ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
            For Each fileTypeEnum As PackageCreatorConstants.FileDirectoryType In [Enum].GetValues(GetType(PackageCreatorConstants.FileDirectoryType))
                returnValue.TryAdd(fileTypeEnum, GetFileDirectoryName(fileTypeEnum))
            Next
            Return returnValue
        End Function

        Protected Function GetResourceTypeDictionary() As ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType)
            Dim returnValue As New ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType)
            For Each manifestResourceType As PackageCreator.QTIManifestResourceType In [Enum].GetValues(GetType(PackageCreator.QTIManifestResourceType))
                returnValue.TryAdd(manifestResourceType, GetManifestResourceType(manifestResourceType))
            Next
            Return returnValue
        End Function

        Public Overridable Function GetFileDirectoryName(fileType As PackageCreatorConstants.FileDirectoryType) As String
            Return fileType.ToString
        End Function

        Protected Overridable Function GetManifestResourceType(manifestResourceType As PackageCreator.QTIManifestResourceType) As ResourceTypeType
            Select Case manifestResourceType
                Case PackageCreator.QTIManifestResourceType.imsqti_item
                    Return ResourceTypeType.imsqti_item_xmlv3p0
                Case PackageCreator.QTIManifestResourceType.imsqti_test
                    Return ResourceTypeType.imsqti_test_xmlv3p0
                Case PackageCreator.QTIManifestResourceType.associatedcontent
                    Return ResourceTypeType.associatedcontentlearningapplicationresource
                Case PackageCreator.QTIManifestResourceType.sharedstimulus
                    Return ResourceTypeType.imsqti_stimulus_xmlv3p0
                Case PackageCreator.QTIManifestResourceType.webcontent,
                     PackageCreator.QTIManifestResourceType.adaptive_driver,
                     PackageCreator.QTIManifestResourceType.adaptive_module
                    Return ResourceTypeType.webcontent
                Case Else
                    Return Nothing
            End Select
        End Function

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    If Settings IsNot Nothing AndAlso _validationFunction IsNot Nothing Then
                        Try
                            RemoveHandler Settings.ValidationEventHandler, _validationFunction
                        Catch ex As Exception
                        End Try
                    End If
                    Manifest = Nothing
                    Resources = Nothing
                    FilesPerEntity = Nothing
                    Settings = Nothing
                    Tests = Nothing
                    ListOfItems = Nothing
                    FileTypeDictionary = Nothing
                    ResourceTypeDictionary = Nothing
                    ResourcesRetrieved = Nothing
                End If
            End If
            disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub

    End Class
End Namespace