Imports System.Collections.Concurrent
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Xml.Schema
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.ChainHandlers
Imports Questify.Builder.Logic.QTI.ChainHandlers.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.Logic.QTI.Facade.QTI22
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Interfaces.QTI22
Imports Questify.Builder.Logic.QTI.PackageCreators.BaseClasses
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.Requests.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final
Imports Questify.Builder.Logic.Service.Classes
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI22_Final.ResourceType

Namespace QTI.PackageCreators.QTI22

    Public Class QTI22PackageCreator
        Inherits PackageCreatorBase(Of QTI22PublicationRequest)

        Private Shared ReadOnly Lock As New Object
        Private Shared ReadOnly LockDependencies As New Object
        Private _listOfItems As ConcurrentDictionary(Of String, List(Of KeyValuePair(Of String, Tuple(Of Double, Integer))))

        Private ReadOnly _encryptionHandlerTypeName As String
        Private _itemPreviewServiceLocation As String = String.Empty
        Private _replacedIds As ConcurrentDictionary(Of String, String)
        Private _facade As QTI22PackageCreatorFacade
        Public ReadOnly LockCss As New Object
        Private _s As PluginHandlerConfigElement

        Public Sub New(configOfHandler As PluginHandlerConfigCollection)
            MyBase.New(configOfHandler)
            If configOfHandler IsNot Nothing Then
                For Each configElement As PluginHandlerConfigElement In configOfHandler
                    Select Case configElement.Key
                        Case "EncryptionHandlerTypeName"
                            _encryptionHandlerTypeName = configElement.Value
                        Case "ItemPreviewServiceLocation"
                            _itemPreviewServiceLocation = configElement.Value
                    End Select
                Next
            End If

        End Sub

        Protected Sub New()

        End Sub

        Public TEMP_XSD_FOLDER As String = Path.Combine(TempWorkingDirectory.FullName, "XsdZipFiles")
        Public RelativePathToResources As String = "..\"
        Public AdditionalItemCssStyles As String = String.Empty
        Public CurrentAssessmentTest As GeneralAssessmentTest
        Public CurrentAssessmentPackage As TestPackage

        Public Property TimeStamper As ITimeStamp

        Public Property TestPackageResourceGuid As String

        Sub New(s As PluginHandlerConfigElement)
            _s = s
        End Sub

        Private ReadOnly Property EncryptionHandlerTypeName As String
            Get
                Return _encryptionHandlerTypeName
            End Get
        End Property

        Private ReadOnly Property CurrentTestIdentifier As String
            Get
                Return CurrentAssessmentTest.Identifier.Replace(Chr(32), "_"c).Replace(".", "_")
            End Get
        End Property

        Public ReadOnly Property ListOfItems As ConcurrentDictionary(Of String, List(Of KeyValuePair(Of String, Tuple(Of Double, Integer))))
            Get
                If _listOfItems Is Nothing Then _listOfItems = New ConcurrentDictionary(Of String, List(Of KeyValuePair(Of String, Tuple(Of Double, Integer))))
                Return _listOfItems
            End Get
        End Property

        Public ReadOnly Property ReplacedIds As ConcurrentDictionary(Of String, String)
            Get
                If _replacedIds Is Nothing Then _replacedIds = New ConcurrentDictionary(Of String, String)
                Return _replacedIds
            End Get
        End Property

        Public Property TypeOfPackage As PackageCreatorConstants.PackageType

        Public Enum QTIManifestResourceType
            <DescriptionAttribute("imsqti_test_xmlv2p2")>
            imsqti_test
            <DescriptionAttribute("imsqti_item_xmlv2p2")>
            imsqti_item
            <DescriptionAttribute("associatedcontent/xmlv1p0/learning-application-resource")>
            associatedcontent
            <DescriptionAttribute("controlfile/xmlv1p0")>
            controlfile
            <DescriptionAttribute("webcontent")>
            webcontent
            <DescriptionAttribute("ade-theme")>
            theme
            <DescriptionAttribute("imsqti_module")>
            adaptive_module
            <DescriptionAttribute("imsqti_driver")>
            adaptive_driver
        End Enum

        Public Overridable Function Create(bw As BackgroundWorker, bankId As Integer, testNames As IList(Of String), testPackageNames As IList(Of String), ByVal targetPackageFileSystemInfo As FileInfo, validate As Boolean, testToPublish As String, isPreview As Boolean) As Boolean
            Dim returnValue = False
            Using newPublicationRequest = GetPublicationRequest(Nothing, targetPackageFileSystemInfo)
                Dim testCollection As ResourceEntryCollection = LoadPackage(bankId, testNames, testPackageNames)
                returnValue = Create(bw, testCollection, newPublicationRequest, validate, testToPublish, isPreview)
            End Using
            Return returnValue
        End Function

        Protected Function Create(bw As BackgroundWorker, testCollection As ResourceEntryCollection, ByVal newPublicationRequest As QTI22PublicationRequest, validate As Boolean, testToPublish As String, isPreview As Boolean) As Boolean
            Dim testPackage As TestPackage = Nothing
            Dim listOfAssessmentTest As New List(Of AssessmentTest2)
            Dim listOfTestRef As New List(Of TestReference)
            For Each resourceEntry As ResourceEntry In testCollection
                If resourceEntry.Type = "TestPackageResourceEntity" Then
                    testPackage = GetTestPackage(resourceEntry)
                    Dim testRefCollection As ReadOnlyCollection(Of TestReference) = testPackage.GetAllTestReferencesInTestPackage()
                    For Each testRef As TestReference In testRefCollection
                        If String.IsNullOrEmpty(testToPublish) OrElse testRef.Title = testToPublish Then
                            Dim test As AssessmentTest2 = GetTestByCode(testRef.Title)
                            listOfTestRef.Add(testRef)
                            listOfAssessmentTest.Add(test)
                        End If
                    Next
                Else
                    Dim test As AssessmentTest2 = GetAssessmentTest(resourceEntry)
                    listOfAssessmentTest.Add(test)
                End If
            Next
            Return Create(bw, testPackage, listOfAssessmentTest, listOfTestRef, newPublicationRequest, validate, testToPublish, isPreview)
        End Function

        Protected Function Create(bw As BackgroundWorker, testPackage As TestPackage, listOfAssessmentTest As List(Of AssessmentTest2), listOfTestRef As List(Of TestReference), ByVal newPublicationRequest As QTI22PublicationRequest, validate As Boolean, testToPublish As String, isPreview As Boolean) As Boolean
            If isPreview Then
                TypeOfPackage = PackageCreatorConstants.PackageType.TestPreview
            ElseIf testPackage IsNot Nothing Then
                TypeOfPackage = PackageCreatorConstants.PackageType.TestPackagePublication
                CurrentAssessmentPackage = testPackage
            End If
            Dim returnValue As Boolean
            Dim sessionContext As ISessionContext = New SessionContext()
            AddHandler sessionContext.ResourceNeeded, AddressOf ResourceNeeded
            Try
                Using New SessionContextProvider(sessionContext)
                    Initialise()
                    RelativePathToResources = "..\"
                    _listOfItems = New ConcurrentDictionary(Of String, List(Of KeyValuePair(Of String, Tuple(Of Double, Integer))))
                    _facade = New QTI22PackageCreatorFacade
                    _facade.TestCreationChain.Add(New ProgressHandler(Of QTI22PublicationRequest)(DetermineNrOfProgressSteps(listOfAssessmentTest, Not listOfTestRef.Count = 0), AddressOf HandleProgress))

                    ProcessTests(listOfAssessmentTest, listOfTestRef)
                    If testPackage IsNot Nothing Then
                        Dim itemMetaDataCollection As MetaDataCollection = ResourceMan.GetResourceMetaData(testPackage.Identifier)
                        TestPackageResourceGuid = GetResourceGuid(itemMetaDataCollection)
                    End If

                    _facade.SaveTestAndManifestCreationChain.Add(GetSaveTestAndManifestHandler(testPackage, TestPackageResourceGuid))

                    If validate Then
                        SetupFacadeForXsdValidationChain()
                    End If

                    SetupFacadeForPackaging(isPreview)

                    returnValue = _facade.CreatePackage(bw, newPublicationRequest) = ChainHandlerResult.RequestHandled
                    If returnValue Then
                        returnValue = Errors.Count = 0
                    End If
                    For Each handlerEx In _facade.Chain.HandlerExceptions
                        Dim publicationError = New PublicationError
                        publicationError.Message = handlerEx.Message
                        publicationError.EntityProcessed = handlerEx.Message
                        publicationError.ExceptionType = "General Exception"
                        If handlerEx.InnerException IsNot Nothing Then
                            publicationError.ExceptionType = handlerEx.InnerException.GetType.ToString
                            If Not String.IsNullOrEmpty(handlerEx.InnerException.Message) Then
                                publicationError.Message = handlerEx.InnerException.Message
                            End If
                        End If
                        If handlerEx.Message.Contains("object:") AndAlso handlerEx.Message.Contains("message:") Then
                            Dim messages = handlerEx.Message.Replace("object:", String.Empty).Replace("message:", "|").Split("|"c)
                            If messages.Count = 2 Then
                                publicationError.EntityProcessed = messages(0).Trim
                                publicationError.Message = messages(1).Trim
                            End If
                        End If
                        Errors.Add(publicationError)
                    Next
                End Using
            Catch ex As Exception
                Dim publicationError = New PublicationError
                publicationError.Message = ex.Message
                publicationError.ExceptionType = ex.GetType.ToString
                publicationError.EntityProcessed = ex.Message
                returnValue = False
            Finally
                RemoveHandler sessionContext.ResourceNeeded, AddressOf ResourceNeeded
            End Try
            Me.Dispose()
            Return returnValue
        End Function

        Public Function CreateAndPostPackageForItemPreview(assessmentItem As AssessmentItem, postUrl As String, exportFile As FileInfo, target As String) As PublicationResult
            Dim idOrg = assessmentItem.Identifier
            Dim titleOrg = assessmentItem.Title
            Try
                If String.IsNullOrEmpty(assessmentItem.Identifier) Then assessmentItem.Identifier = Guid.NewGuid.ToString()
                If String.IsNullOrEmpty(assessmentItem.Title) Then assessmentItem.Title = Guid.NewGuid.ToString()
                If Not String.IsNullOrEmpty(postUrl) Then
                    _itemPreviewServiceLocation = postUrl
                End If
                Dim result As New PublicationResult
                Initialise()
                RelativePathToResources = "..\"
                Dim sessionContext As ISessionContext = New SessionContext()
                AddHandler sessionContext.ResourceNeeded, AddressOf ResourceNeeded
                Try
                    Using New SessionContextProvider(sessionContext)
                        If assessmentItem IsNot Nothing AndAlso Not String.IsNullOrEmpty(_itemPreviewServiceLocation) Then
                            If Not CachedItems.ContainsKey(assessmentItem.Identifier) Then
                                CachedItems.TryAdd(assessmentItem.Identifier, assessmentItem)
                            End If
                            SetExtraStyles()
                            _listOfItems = New ConcurrentDictionary(Of String, List(Of KeyValuePair(Of String, Tuple(Of Double, Integer))))
                            _facade = New QTI22PackageCreatorFacade
                            Dim success = False
                            Dim fakeItemRef As New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .Title = assessmentItem.Identifier, .SourceName = assessmentItem.Identifier}
                            Dim fakeSection As New TestSection2 With {.Identifier = Guid.NewGuid.ToString, .Title = "Section"}
                            Dim fakeTestPart As New TestPart2 With {.Identifier = Guid.NewGuid.ToString, .Title = "TestPart"}
                            Dim fakeTest As New AssessmentTest2 With {.Identifier = "Test_Preview", .Title = "Test Preview"}
                            fakeTest.IncludedViews.Add(target)
                            fakeSection.Components.Add(fakeItemRef)
                            fakeTestPart.Sections.Add(fakeSection)
                            fakeTest.TestParts.Add(fakeTestPart)
                            ProcessTests(New AssessmentTest2() {fakeTest}.ToList, New List(Of TestReference))

                            _facade.SaveTestAndManifestCreationChain.Add(GetSaveTestAndManifestHandler(Nothing, Guid.NewGuid.ToString))
                            Using newPublicationRequest = GetPublicationRequest(Nothing, exportFile)
                                CopySchemaFiles(GetXsdFolders(newPublicationRequest)(PackageCreatorConstants.FileDirectoryType.controlxsds.ToString))
                                SetupFacadeForPackaging(False)
                                success = _facade.CreatePackage(Nothing, newPublicationRequest) = ChainHandlerResult.RequestHandled
                                If success Then
#If DEBUG Then
                                    For Each errorMessage In Errors
                                        Console.WriteLine("ERROR: {0}", errorMessage.Message)
                                    Next
#End If
                                    Dim postPackageHelper As New PostPackageHelper
                                    Dim urlParameterName As String = GetUrlParameterName()
                                    Dim url = postPackageHelper.PostPackage(_itemPreviewServiceLocation, exportFile.FullName, urlParameterName, False)
                                    If postPackageHelper.Errors.Count > 0 Then
                                        For Each postError In postPackageHelper.Errors
                                            Errors.Add(New PublicationError(postError, "PostPackageError", "package"))
                                        Next
                                    End If
                                    If url IsNot Nothing Then
                                        result.PublicationLocation = url.ToString
                                    End If
                                ElseIf _facade IsNot Nothing AndAlso _facade.Chain.HandlerExceptions IsNot Nothing AndAlso _facade.Chain.HandlerExceptions.Count > 0 Then
                                    AddErrors(_facade.Chain.HandlerExceptions)
                                End If
                            End Using
                        Else
                            Debug.Assert(Not String.IsNullOrEmpty(_itemPreviewServiceLocation), "Missing ItemPreview Location")
                            Debug.Assert(assessmentItem IsNot Nothing, "assessmentItem should not be nothing")
                        End If
                    End Using
                Catch ex As Exception
                    Errors.Add(CreateGeneralError(ex))
                Finally
                    RemoveHandler sessionContext.ResourceNeeded, AddressOf ResourceNeeded
                End Try
                result.ErrorMessage = GetErrorMessage()
                result.Succeeded = result.PublicationLocation IsNot Nothing AndAlso String.IsNullOrEmpty(result.ErrorMessage)
                Return result
            Finally
                If assessmentItem.Identifier <> idOrg Then assessmentItem.Identifier = idOrg
                If assessmentItem.Title <> titleOrg Then assessmentItem.Identifier = titleOrg
                Dispose()
            End Try
        End Function

        Private Sub ProcessTests(testList As List(Of AssessmentTest2), listOfTestRefs As List(Of TestReference))
            For Each testRef As TestReference In listOfTestRefs
                _facade.SetupTestChain.Add(GetTestReferenceHandler(testRef))
            Next
            For Each test As AssessmentTest2 In testList
                CurrentAssessmentTest = CheckForAssessmentTest(test)
                _facade.SetupTestChain.Add(GetAssessmentInitialiseTestHandler(test, CurrentAssessmentTest))
                StartPickingItems(CurrentAssessmentTest)
            Next
            For Each item In _listOfItems.Keys
                _facade.SaveItemAndResourcesChain.Add(New ProgressHandler(Of QTI22PublicationRequest)(String.Format(My.Resources.ProcessingItem, item), AddressOf HandleProgress))
                _facade.SaveItemAndResourcesChain.Add(GetCreateAssessmentItemAndResourcesHandler(item))
            Next
        End Sub

        Protected Overridable Sub SetExtraStyles()
        End Sub

        Protected Function DetermineNrOfProgressSteps(ByVal testList As List(Of AssessmentTest2), isTestPackage As Boolean) As Integer
            Dim numberOfSteps As Integer = 3
            Dim listOfUniqueItems As New List(Of String)
            For Each test As AssessmentTest2 In testList
                AddReferenceToList(listOfUniqueItems, test)
                numberOfSteps += (test.GetAllItemReferencesInTest.Count)
            Next
            numberOfSteps += listOfUniqueItems.Count
            If isTestPackage Then
                numberOfSteps += testList.Count
            End If
            Return numberOfSteps
        End Function

        Protected Overrides Sub Dispose(disposing As Boolean)
            MyBase.Dispose(disposing)
        End Sub

        Protected Function GetErrorMessage() As String
            Dim sb As New StringBuilder(String.Empty)
            For Each e In Errors
                sb.AppendLine(String.Format(My.Resources.ErrorOccurred, e.ExceptionType, e.EntityProcessed, e.Message))
            Next
            Return sb.ToString
        End Function

        Protected Overrides Sub NewItemReference(itemReference As ItemReferenceViewBase, testIdentifier As String)
            Dim childIndex As Integer = GetChildIndex(itemReference)
            If Not _listOfItems.ContainsKey(itemReference.SourceName) Then
                _listOfItems.TryAdd(itemReference.SourceName, New KeyValuePair(Of String, Tuple(Of Double, Integer))() {(New KeyValuePair(Of String, Tuple(Of Double, Integer))(testIdentifier, New Tuple(Of Double, Integer)(itemReference.Weight, childIndex)))}.ToList)
            Else
                _listOfItems(itemReference.SourceName).Add(New KeyValuePair(Of String, Tuple(Of Double, Integer))(testIdentifier, New Tuple(Of Double, Integer)(itemReference.Weight, childIndex)))
            End If
            _facade.TestCreationChain.Add(New ProgressHandler(Of QTI22PublicationRequest)(String.Format(My.Resources.ProcessingItem, itemReference.Title), AddressOf HandleProgress))
            _facade.TestCreationChain.Add(GetAssessmentItemHandler(CType(itemReference, GeneralItemReference), TestIdentifierToCompare(testIdentifier)))
        End Sub

        Private Function GetChildIndex(itemReference As ItemReferenceViewBase) As Integer
            Dim result As Integer = 0
            Dim idx As Integer = 0
            If itemReference.Parent IsNot Nothing AndAlso TypeOf itemReference.Parent Is GeneralTestSection Then
                DirectCast(itemReference.Parent, GeneralTestSection).Components.ForEach(Sub(c)
                                                                                            If c.Identifier.Equals(itemReference.Identifier, StringComparison.InvariantCultureIgnoreCase) Then
                                                                                                result = idx
                                                                                            End If
                                                                                            idx += 1
                                                                                        End Sub)
            End If
            Return result
        End Function

        Protected Overrides Sub NewSection(sender As Object, e As SectionChangeEventArgs)
            _facade.SetupTestChain.Add(GetAssessmentSectionHandler(CType(e.Section, GeneralTestSection), CurrentTestIdentifier, TempWorkingDirectory.FullName))
        End Sub

        Protected Overrides Sub NewTestPart(sender As Object, e As TestPartChangeEventArgs)
            _facade.SetupTestChain.Add(GetAssessmentTestPartHandler(CType(e.TestPart, GeneralTestPart), CurrentTestIdentifier))
        End Sub

        Private Sub AddReferenceToList(ByRef list As List(Of String), test As AssessmentTest2)
            For Each itemRef As ItemReference2 In test.GetAllItemReferencesInTest
                If Not list.Contains(itemRef.SourceName) Then
                    list.Add(itemRef.SourceName)
                End If
            Next
        End Sub

        Private Sub SetupFacadeForPackaging(ByVal isForPreview As Boolean)
            With _facade
                .PackagingChain.Clear()
                .PackagingChain.Add(New ProgressHandler(Of QTI22PublicationRequest)(My.Resources.PublishingPackageToSpecifiedLocation, AddressOf HandleProgress))
                .PackagingChain.Add(GetPackageZipFileCreatorHandler())

                If Not isForPreview AndAlso Not String.IsNullOrEmpty(EncryptionHandlerTypeName) Then
                    Dim handler = DirectCast(Activator.CreateInstance(Type.GetType(EncryptionHandlerTypeName, True)), IChainhandler(Of QTI22PublicationRequest))
                    Debug.Assert(handler IsNot Nothing, String.Format(CultureInfo.InvariantCulture, "{0} could not create instance of type {1}. Check configuration file.", Me.GetType().FullName, EncryptionHandlerTypeName))
                    If handler IsNot Nothing Then
                        .PackagingChain.Add(New ProgressHandler(Of QTI22PublicationRequest)(0, AddressOf HandleProgress))
                        .PackagingChain.Add(New ProgressHandler(Of QTI22PublicationRequest)(My.Resources.BusyEncryptingDocument, AddressOf HandleProgress))
                        .PackagingChain.Add(handler)
                    End If
                End If
            End With
        End Sub

        Protected Overridable Sub SetupFacadeForXsdValidationChain()
            With _facade
                .SetupXsdValidationChain.Clear()
                .SetupXsdValidationChain.Add(New QTI22_SetupXsdHandler(Me))
            End With
        End Sub

        Public Shared Function ObjectToAny(objectToConvert As Object, ns As Serialization.XmlSerializerNamespaces) As XmlElement()
            Return New XmlElement() {ChainHandlerHelper.ObjectToAny(objectToConvert, ns)}
        End Function

        Public Shared Function ObjectsAttributeToAny(objectToConvert As Object, ns As Serialization.XmlSerializerNamespaces) As XmlAttribute()
            Dim attributes As New List(Of XmlAttribute)
            Dim elements = ObjectToAny(objectToConvert, ns)
            For Each element In elements
                For Each attr In element.Attributes
                    If TypeOf attr Is XmlAttribute AndAlso Not DirectCast(attr, XmlAttribute).Name.StartsWith("xmlns") Then
                        Debug.Assert(Not DirectCast(attr, XmlAttribute).Name.Contains("href"), "Href is not allowed to be added again")
                        attributes.Add(DirectCast(attr, XmlAttribute))
                    End If
                Next
            Next
            Return attributes.ToArray
        End Function

        Public Shared Sub AddResourceToManifest(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), resource As ResourceType, files() As FileType)
            Dim resourceDictionary As Dictionary(Of ResourceType, List(Of String))
            Dim newResourceType As ResourceType
            SyncLock Lock
                If Not resources.ContainsKey(resource.identifier) Then
                    resourceDictionary = New Dictionary(Of ResourceType, List(Of String))
                    newResourceType = resource
                    resourceDictionary.Add(newResourceType, New List(Of String))
                    resources.TryAdd(resource.identifier, resourceDictionary)
                Else
                    resourceDictionary = resources.Item(resource.identifier)
                    newResourceType = resourceDictionary.Keys(0)
                End If
                If files IsNot Nothing Then
                    Dim fileList = files.ToList
                    If newResourceType.file IsNot Nothing Then
                        newResourceType.file.ToList.ForEach(Sub(file)
                                                                If fileList.Where(Function(newFile) newFile.href = file.href).Count = 0 Then
                                                                    fileList.Add(file)
                                                                End If
                                                            End Sub)
                    End If
                    newResourceType.file = fileList.ToArray
                End If
            End SyncLock
        End Sub

        Public Overridable Sub SaveGeneratedCss(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), css As String, fileName As String, cssDirectoryName As String, cssResourceType As String)
            ChainHandlerHelper.SaveFile(css, Path.Combine(TempWorkingDirectory.FullName, Path.Combine(cssDirectoryName, fileName)))
            Dim fileList As New List(Of FileType)
            Dim newFile As New FileType
            Dim href As String = String.Concat(cssDirectoryName, "/", Path.GetFileName(fileName))
            newFile.href = href
            fileList.Add(newFile)

            If resources IsNot Nothing Then
                Dim resourceType As New ResourceType
                resourceType.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(fileName, PackageCreatorConstants.TypeOfResource.resource)
                resourceType.type = cssResourceType
                AddResourceToManifest(resources, resourceType, fileList.ToArray)
            End If
        End Sub

        Protected Overridable Function GetNewResourceType() As ResourceType
            Return New ResourceType
        End Function

        Public Overridable Function CheckForAssessmentTest(test As AssessmentTest2) As GeneralAssessmentTest
            If AssessmentTestv2Factory.ContainsView(test, GenericTestModelPlugin.PLUGIN_NAME) Then
                Return AssessmentTestv2Factory.CreateView(Of GeneralAssessmentTest)(test)
            Else
                Throw New TesterException(My.Resources.NoGenericTestFound)
            End If
        End Function

        Public Overridable Sub SaveGeneratedCss(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), css As String, cssDirectoryName As String, cssResourceType As String)
            SaveGeneratedCss(resources, css, PackageCreatorConstants.GENERATED_CSS, cssDirectoryName, cssResourceType)
        End Sub

        Public Shared Function GetResourceType(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), name As String) As ResourceType
            If resources.ContainsKey(name) Then
                Dim resourceDictionary As Dictionary(Of ResourceType, List(Of String))
                resourceDictionary = resources.Item(name)
                Return resourceDictionary.Keys(0)
            End If
            Return Nothing
        End Function

        Public Shared Sub AddResourceToManifest(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), resource As ResourceType)
            AddResourceToManifest(resources, resource, Nothing)
        End Sub

        Public Shared Function AddDependentResourceToManifest(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), referenceId As String, dependencyId As String) As Boolean
            Dim depencencyList As New List(Of String)
            depencencyList.Add(dependencyId)
            Return AddDependentResourceToManifest(resources, referenceId, depencencyList)
        End Function

        Private Shared Function FindVersion(ByVal metaData As MetaData) As Boolean
            Return String.Compare(metaData.Name, "Version", True) = 0
        End Function

        Private Shared Function FindGuid(ByVal metaData As MetaData) As Boolean
            Return String.Compare(metaData.Name, "OriginalResourceId", True) = 0
        End Function

        Public Shared Function GetResourceVersion(itemMetaDataCollection As MetaDataCollection) As Nullable(Of Integer)
            Dim returnValue As Nullable(Of Integer) = Nothing
            Dim metaData As MetaData = itemMetaDataCollection.Find(AddressOf FindVersion)
            If metaData IsNot Nothing Then
                If metaData.Value.Contains(".") Then
                    Dim arVersion = metaData.Value.Split("."c)
                    If arVersion.Count = 2 Then
                        Dim major = 0
                        Dim minor = 0
                        Integer.TryParse(arVersion(0), major)
                        Integer.TryParse(arVersion(1), minor)
                        returnValue = (major * 10000) + minor
                    End If
                End If
            End If
            Return returnValue
        End Function

        Public Shared Function GetResourceGuid(itemMetaDataCollection As MetaDataCollection) As String
            Dim returnValue As String = Nothing
            Dim metaData As MetaData = itemMetaDataCollection.Find(AddressOf FindGuid)
            If metaData IsNot Nothing AndAlso Not String.IsNullOrEmpty(metaData.Value) Then
                returnValue = metaData.Value
            End If
            Return returnValue
        End Function

        Public Shared Function AddDependentResourceToManifest(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), referenceId As String, dependencyIdList As List(Of String)) As Boolean
            Dim returnValue As Boolean = True
            SyncLock LockDependencies
                If resources.ContainsKey(referenceId) Then
                    Dim resourceDictionary As Dictionary(Of ResourceType, List(Of String))
                    resourceDictionary = resources.Item(referenceId)
                    Dim dependencyList As List(Of String) = resourceDictionary.Item(resourceDictionary.Keys(0))
                    For Each dependencyId As String In dependencyIdList
                        If Not dependencyList.Contains(dependencyId) Then
                            dependencyList.Add(dependencyId)
                        End If
                    Next
                Else
                    returnValue = False
                End If
            End SyncLock
            Return returnValue
        End Function

        Public Function GetScoreConverterFromScoringParameters(ByVal item As AssessmentItem) As IScoringConverterQTI22
            Dim scoringPrms = item.Parameters.DeepFetchInlineScoringParameters()
            If scoringPrms.Any(Function(p) TypeOf (p) Is ScoringParameter) Then
                Return GetCombinedScoreConverter(item.Identifier, scoringPrms)
            End If
            Return Nothing
        End Function

        Protected Overridable Function GetAssessmentInitialiseTestHandler(test As AssessmentTest2, qtiTest As GeneralAssessmentTest) As QTI22_AssessmentInitialiseTestHandler
            Return New QTI22_AssessmentInitialiseTestHandler(test, qtiTest, Me)
        End Function

        Public Overridable Function GetUrlParameterName() As String
            Return "contentPackage"
        End Function

        Public Overridable Function GetCombinedScoreConverter(itemIdentifier As String, scoringParameters As HashSet(Of ScoringParameter)) As IScoringConverterQTI22
            Return DirectCast(New QTI22CombinedScoringConverter(scoringParameters), IScoringConverterQTI22)
        End Function

        Public Overridable Function ShouldGetUrlFromHtml() As Boolean
            Return False
        End Function

        Public Overridable Function GetXsdHelper() As XsdHelper
            Return New XsdHelper
        End Function

        Protected Overridable Function GetAssessmentItemHandler(qtiitem As GeneralItemReference, testIdentifier As String) As QTI22_AssessmentItemHandler
            Return New QTI22_AssessmentItemHandler(qtiitem, testIdentifier, Me)
        End Function

        Protected Overridable Function GetCreateAssessmentItemAndResourcesHandler(itemCode As String) As QTI22_CreateAssessmentItemAndResourcesHandler
            Return New QTI22_CreateAssessmentItemAndResourcesHandler(itemCode, ListOfItems(itemCode).[Select](Function(i) TestIdentifierToCompare(i.Key)).ToList(), Me)
        End Function

        Private Function TestIdentifierToCompare(testIdentifierInput As String) As String
            Return testIdentifierInput.Replace(Chr(32), "_"c).Replace(".", "_")
        End Function

        Protected Overridable Function GetAssessmentSectionHandler(qtiTestSection As GeneralTestSection, testname As String, tempFolder As String) As QTI22_AssessmentSectionHandler
            Return New QTI22_AssessmentSectionHandler(qtiTestSection, testname, tempFolder, Me)
        End Function

        Protected Overridable Function GetAssessmentTestPartHandler(qtiTestPart As GeneralTestPart, testname As String) As QTI22_AssessmentTestPartHandler
            Return New QTI22_AssessmentTestPartHandler(qtiTestPart, testname, Me)
        End Function

        Protected Overridable Function GetPackageZipFileCreatorHandler() As QTI22_PackageZipFileCreatorHandler
            Return New QTI22_PackageZipFileCreatorHandler(Me)
        End Function

        Protected Overridable Function GetSaveTestAndManifestHandler(testPackage As TestPackage, testPackageGuid As String) As QTI22_SaveTestAndManifestHandler
            Return New QTI22_SaveTestAndManifestHandler(testPackage, testPackageGuid, Me)
        End Function

        Protected Overridable Function GetTestReferenceHandler(testRef As TestReference) As QTI22_TestReferenceHandler
            Return New QTI22_TestReferenceHandler(testRef)
        End Function

        Protected Overridable Function GetPublicationRequest(sourcePackage As FileInfo, targetPackageFileSystemInfo As FileInfo) As QTI22PublicationRequest
            Return New QTI22PublicationRequest(sourcePackage, targetPackageFileSystemInfo, AddressOf ValidationEventHandler)
        End Function

        Public Overridable Function GetXsdFolders(request As QTI22PublicationRequest) As Dictionary(Of String, String)
            Dim xsdFolders As New Dictionary(Of String, String)
            xsdFolders.Add("controlxsds", Path.Combine(TempWorkingDirectory.ToString(), request.FileTypeDictionary(PackageCreatorConstants.FileDirectoryType.controlxsds)))
            xsdFolders.Add("tempxsds", TEMP_XSD_FOLDER)
            Return xsdFolders
        End Function

        Public Overridable Sub CopySchemaFiles(xsdFolder As String)
            CopySchemas(My.Resources.schema_qti22, xsdFolder)
        End Sub

        Public Overridable Function GetAssessmentTestViewType() As String
            Return GenericTestModelPlugin.PLUGIN_NAME
        End Function

        Protected Sub CopySchemas(schemasZip As Byte(), xsdFolder As String)
            If Not New DirectoryInfo(xsdFolder).Exists Then
                Using memoryStream As New MemoryStream(schemasZip, 0, schemasZip.Count)
                    memoryStream.Position = 0
                    Using streamReader As New StreamReader(memoryStream)
                        ChainHandlerHelper.ExtractZipToDirectory(streamReader, xsdFolder)
                    End Using
                End Using
            End If
        End Sub

        Public Sub ValidationEventHandler(ByVal sender As Object, ByVal e As ValidationEventArgs)
            Dim reader = TryCast(sender, XmlReader)
            Dim fileToCheck = String.Empty
            If reader IsNot Nothing Then
                fileToCheck = Path.GetFileName(reader.BaseURI)
            End If
            Dim itemToValidate = Path.GetFileNameWithoutExtension(fileToCheck).Replace("ITM_", String.Empty).Replace("TST", String.Empty)
            Dim validationError = New PublicationError(String.Format(My.Resources.XsdValidationFile, itemToValidate, fileToCheck, e.Message), My.Resources.ValidationError, itemToValidate)
            Select Case e.Severity
                Case XmlSeverityType.Error
                    Errors.Add(validationError)
                Case XmlSeverityType.Warning
                    Warnings.Add(validationError)
            End Select
        End Sub

    End Class
End Namespace