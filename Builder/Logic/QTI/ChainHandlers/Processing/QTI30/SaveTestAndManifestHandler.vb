Imports System.Collections.Concurrent
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Model.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class SaveTestAndManifestHandler
        Inherits ChainHandlerBase


        Private _manifestName As String = String.Empty
        Private ReadOnly _testPackage As TestPackage
        Private ReadOnly _testPackageGuid As String
        Private _settings As XmlReaderSettings
        Private ReadOnly _packageType As PackageCreatorConstants.PackageType
        Protected FolderDirectory As ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
        Protected ResourceTypes As ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType)
        Public TimeStamper As ITimeStamp



        Public Sub New(testPackage As TestPackage, testPackageGuid As String, packageCreator As PackageCreator)
            MyBase.New(packageCreator)
            TimeStamper = packageCreator.TimeStamper
            _testPackageGuid = testPackageGuid
            _testPackage = testPackage
            _packageType = packageCreator.TypeOfPackage
            If testPackage IsNot Nothing AndAlso testPackage.Identifier IsNot Nothing Then LastHandledObject = $"testpackage {testPackage.Identifier}"
        End Sub


        Public Overrides Function ProcessRequest(requestData As PublicationRequest) As ChainHandlerResult
            _settings = requestData.Settings
            FolderDirectory = requestData.FileTypeDictionary
            ResourceTypes = requestData.ResourceTypeDictionary
            For Each testDocumentSet As TestDocumentSet In requestData.Tests.Values
                SaveTestDocuments(testDocumentSet, requestData.FilesPerEntity, testDocumentSet.Test.Identifier.Replace(Chr(32), "_"c))
                Dim qtiTest As GeneralAssessmentTest = CheckForAssessmentTest(testDocumentSet.Test)
                AddTestResourcesToManifest(requestData, testDocumentSet)
                _manifestName = $"{_manifestName}-{testDocumentSet.Test.Identifier}"
            Next

            _manifestName = _manifestName.Trim("-"c)
            If _testPackage IsNot Nothing Then
                _manifestName = _testPackage.Identifier
            End If
            AddManifestMetaData(requestData, _testPackage, _packageType, _manifestName, _testPackageGuid)
            BeforeManifestSave(requestData)
            SaveManifest(requestData.Resources, requestData.Manifest, requestData.FilesPerEntity, requestData.GeneratedCssContent)

            Return ChainHandlerResult.RequestHandled
        End Function

        Protected Overridable Sub BeforeManifestSave(requestData As PublicationRequest)
        End Sub

        Protected Overridable Function CheckForAssessmentTest(test As AssessmentTest2) As GeneralAssessmentTest
            If AssessmentTestv2Factory.ContainsView(test, GenericTestModelPlugin.PLUGIN_NAME) Then
                Return AssessmentTestv2Factory.CreateView(Of GeneralAssessmentTest)(test)
            Else
                Throw New ArgumentNullException("Test doesn't contain a General assessmenttest view.")
            End If
        End Function

        Protected Overridable Sub SaveTestDocuments(testDocumentSet As TestDocumentSet, ByRef filesPerEntity As ConcurrentDictionary(Of String, List(Of String)), testname As String)
            Dim fileNameTest As String = $"{testname}.xml"
            ChainHandlerHelper.SaveDocument(testDocumentSet.AssessmentTestType, Path.Combine(Path.Combine(PackageCreator.TempWorkingDirectory.FullName, FolderDirectory(PackageCreatorConstants.FileDirectoryType.items)), fileNameTest), testname, filesPerEntity, Nothing)
            Dim files = filesPerEntity(testname)
            Validate(files)
        End Sub

        Protected Overridable Sub SaveManifest(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), manifest As ManifestType, ByRef filesPerEntity As ConcurrentDictionary(Of String, List(Of String)), css As ConcurrentBag(Of String))
            If css.Count <> 0 Then
                Dim cssContent = String.Join(vbNewLine, css.ToArray)
                PackageCreator.SaveGeneratedCss(resources, cssContent, FolderDirectory(PackageCreatorConstants.FileDirectoryType.css), ResourceTypes(PackageCreator.QTIManifestResourceType.webcontent))
            End If
            Dim resourceTypeList As New List(Of ResourceType)
            Dim resourceKeys = resources.Keys.ToList
            resourceKeys.Sort()
            For Each resourceKey In resourceKeys
                Dim resourceTypeDictionary = resources(resourceKey)
                For Each resourceType As ResourceType In resourceTypeDictionary.Keys
                    Dim dependencyList As New List(Of DependencyType)
                    For Each dependentResource As String In resourceTypeDictionary.Item(resourceType)
                        Dim dependencyType As New DependencyType
                        dependencyType.identifierref = dependentResource
                        dependencyList.Add(dependencyType)
                    Next
                    resourceType.dependency = dependencyList.ToArray
                    resourceTypeList.Add(resourceType)
                Next
            Next
            If manifest.organizations Is Nothing Then
                manifest.organizations = New OrganizationsType
            End If
            If manifest.resources Is Nothing Then
                manifest.resources = New ResourcesType
            End If
            If manifest.metadata Is Nothing Then
                manifest.metadata = New ManifestMetadataType
            End If
            manifest.resources.resource = resourceTypeList.ToArray
            Dim manifestDocument = GetManifestDocument(manifest)
            GetNamespaceHelper().UpdateNameSpaces(manifestDocument, True)
            Dim fileName As String = Path.Combine(PackageCreator.TempWorkingDirectory.FullName, $"{PackageCreatorConstants.IMSMANIFEST}.xml")
            ChainHandlerHelper.SaveDocument(manifestDocument, fileName, PackageCreatorConstants.IMSMANIFEST, filesPerEntity)
            Validate({fileName}.ToList)
        End Sub

        Protected Overridable Function GetManifestDocument(manifest As ManifestType) As XmlDocument
            Dim ns = New Serialization.XmlSerializerNamespaces()
            ns.Add("meta", "http://www.imsglobal.org/xsd/imsqti_metadata_v3p0")
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
            ns.Add("imsmd", "http://ltsc.ieee.org/xsd/LOM")
            Dim serializedManifest As String = Cito.Tester.Common.SerializeHelper.XmlSerializeToString(manifest, False, ns, False, Text.Encoding.UTF8)
            Dim manifestDocument As New XmlDocument
            manifestDocument.LoadXml(serializedManifest)
            Return manifestDocument
        End Function

        Protected Overridable Function GetXsdHelper() As XsdHelper
            Return New XsdHelper
        End Function

        Protected Overridable Function GetNamespaceHelper() As NamespaceHelper
            Return New QTI30NamespaceHelper
        End Function

        Protected Overridable Sub AddTestResourcesToManifest(requestData As PublicationRequest, testDocumentSet As TestDocumentSet)
            Dim testname = testDocumentSet.Test.Identifier.Replace(Chr(32), "_"c)
            Dim files = GetTestFiles(testname)
            If files.Any() Then
                Dim resourceType As ResourceType = PackageCreator.GetResourceType(requestData.Resources, ChainHandlerHelper.GetIdentifierFromResourceId(testname, PackageCreatorConstants.TypeOfResource.test))
                AddPropertiesToResourceType(requestData, testDocumentSet, resourceType, testname)
                PackageCreator.AddResourceToManifest(requestData.Resources, resourceType, files.ToArray)
            End If
        End Sub

        Protected Overridable Sub AddPropertiesToResourceType(requestData As PublicationRequest, testDocumentSet As TestDocumentSet, ByRef resourceType As ResourceType, testName As String)
            Dim fileNameTest As String = $"{testName}.xml"
            Dim hrefTest As String = $"{FolderDirectory(PackageCreatorConstants.FileDirectoryType.items)}/{fileNameTest}"
            resourceType.href = hrefTest
        End Sub

        Protected Overridable Function GetTestFiles(testname As String) As IEnumerable(Of FileType)
            Dim fileNameTest As String = $"{testname}.xml"
            Dim hrefTest As String = $"{FolderDirectory(PackageCreatorConstants.FileDirectoryType.items)}/{fileNameTest}"
            Dim files As New List(Of FileType)
            Dim testFile As New FileType
            testFile.href = hrefTest
            files.Add(testFile)
            Return files
        End Function

        Protected Overridable Sub AddManifestMetaData(request As PublicationRequest, testPackage As TestPackage, packageType As PackageCreatorConstants.PackageType, manifestName As String, testPackageGuid As String)
        End Sub

        Protected Overridable Function GetXsdResourceType() As ResourceType
            Return New ResourceType
        End Function

        Protected Sub Validate(files As List(Of String))
            For Each fileName As String In files
                Using reader As XmlReader = XmlReader.Create(fileName, _settings)
                    While reader.Read()
                    End While
                    If PackageCreator.Errors.Count > 0 Then
                        Throw New XmlException("XSD validation failed.")
                    End If
                End Using
            Next
        End Sub
    End Class
End Namespace