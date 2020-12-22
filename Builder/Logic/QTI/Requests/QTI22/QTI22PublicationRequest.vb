Imports System.Collections.Concurrent
Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Model.QTI22
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Requests.BaseClasses
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Requests.QTI22

    Public Class QTI22PublicationRequest
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
        Public Property ResourceTypeDictionary As ConcurrentDictionary(Of QTI22PackageCreator.QTIManifestResourceType, String)
        Public Property ResourceMimeTypeDictionary As ConcurrentDictionary(Of String, String)
        Public Property ResourcesRetrieved As ConcurrentBag(Of String)
        Public Property ValidationLock As New Object



        Protected Overridable Sub InitialiseLists()
            _FilesPerEntity = New ConcurrentDictionary(Of String, List(Of String))
            _Tests = New Dictionary(Of String, TestDocumentSet)
            _ListOfItems = New ConcurrentDictionary(Of String, PublishedItem)
            _FileTypeDictionary = GetFileTypesDictionary()
            _ResourceTypeDictionary = GetResourceTypeDictory()
            _ResourceMimeTypeDictionary = New ConcurrentDictionary(Of String, String)
            _ResourcesRetrieved = New ConcurrentBag(Of String)
        End Sub


        Protected Overridable Sub InitialiseManifest()
            _Resources = New ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))
            _Manifest = New ManifestType
            _Manifest.identifier = $"MANIFEST-{Guid.NewGuid.ToString}"

            _Manifest.metadata = New ManifestMetadataType
            _Manifest.metadata.schema = "QTIv2.2 Package"
            _Manifest.metadata.schemaversion = "1.0.0"

            _Manifest.resources = New ResourcesType
        End Sub


        Protected Function GetFileTypesDictionary() As ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
            Dim returnValue As New ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
            For Each fileTypeEnum As PackageCreatorConstants.FileDirectoryType In [Enum].GetValues(GetType(PackageCreatorConstants.FileDirectoryType))
                returnValue.TryAdd(fileTypeEnum, GetFileDirectoryName(fileTypeEnum))
            Next
            Return returnValue
        End Function

        Protected Function GetResourceTypeDictory() As ConcurrentDictionary(Of QTI22PackageCreator.QTIManifestResourceType, String)
            Dim returnValue As New ConcurrentDictionary(Of QTI22PackageCreator.QTIManifestResourceType, String)
            For Each manifestResourceType As QTI22PackageCreator.QTIManifestResourceType In [Enum].GetValues(GetType(QTI22PackageCreator.QTIManifestResourceType))
                returnValue.TryAdd(manifestResourceType, GetManifestResourceType(manifestResourceType))
            Next
            Return returnValue
        End Function


        Public Overridable Function GetFileDirectoryName(fileType As PackageCreatorConstants.FileDirectoryType) As String
            Return fileType.ToString
        End Function

        Protected Overridable Function GetManifestResourceType(manifestResourceType As QTI22PackageCreator.QTIManifestResourceType) As String
            Select Case manifestResourceType
                Case QTI22PackageCreator.QTIManifestResourceType.imsqti_item
                    Return $"{manifestResourceType}_xmlv2p2"
                Case QTI22PackageCreator.QTIManifestResourceType.imsqti_test
                    Return $"{manifestResourceType}_xmlv2p2"
                Case Else
                    Return EnumFunctions.EnumDescription(manifestResourceType)
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