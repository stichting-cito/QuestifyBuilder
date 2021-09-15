Imports System.Collections.Concurrent
Imports System.IO
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Requests.BaseClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Model.ContentModel.HelperClasses

Namespace QTI.PackageCreators.BaseClasses

    Public MustInherit Class PackageCreatorBase(Of T As PublicationRequestBase)
        Implements IDisposable, IPackageCreator

        Private _itemSelector As ItemSelectorManager
        Private _itemCounter As Integer = 0
        Private _stepCounter As Integer = 0
        Private _contextIdentifier As Nullable(Of Integer)
        Private _customProperties As EntityCollection

        Public ResourceMan As ResourceManagerBase
        Public ReadOnly Property CustomProperties As EntityCollection
            Get
                If _customProperties Is Nothing Then
                    Dim databaseResourceManager As DataBaseResourceManager = DirectCast(ResourceMan, DataBaseResourceManager)
                    _customProperties = databaseResourceManager.BankCustomProperties
                End If
                Return _customProperties
            End Get
        End Property

        Public AllDependentResourceNames As IList(Of String)
        Public TempWorkingDirectory As DirectoryInfo
        Public Warnings As BlockingCollection(Of PublicationError)
        Public Errors As BlockingCollection(Of PublicationError)
        Public CachedItems As ConcurrentDictionary(Of String, AssessmentItem)
        Public CachedTests As ConcurrentDictionary(Of String, AssessmentTest2)

        Protected MustOverride Sub NewTestPart(sender As Object, e As TestPartChangeEventArgs)
        Protected MustOverride Sub NewSection(sender As Object, e As SectionChangeEventArgs)
        Protected MustOverride Sub NewItemReference(itemReference As ItemReferenceViewBase, testIdentifier As String)

        Protected Sub StartPickingItems(test As AssessmentTestViewBase)
            _itemSelector = New ItemSelectorManager(test)

            AddHandler _itemSelector.ResourceNeeded, AddressOf ResourceNeeded
            AddHandler _itemSelector.SectionChangeEvent, AddressOf NewSection
            AddHandler _itemSelector.TestPartChangeEvent, AddressOf NewTestPart

            Dim endOfTest As Boolean = False
            Dim itemIndex As Integer = 0

            While Not endOfTest
                TestSessionContext.CurrentItem = Nothing
                Dim itemReference As ItemReferenceViewBase = _itemSelector.PickNewItem(_itemCounter)
                NewItemReference(itemReference, test.Identifier)
                Dim item As AssessmentItem = GetItemByCode(itemReference.SourceName)


                TestSessionContext.CurrentItem = item
                TestSessionContext.CurrentItemIndex = itemIndex
                endOfTest = _itemSelector.IsLastItemInTest
                itemIndex += 1
            End While

            RemoveHandler _itemSelector.ResourceNeeded, AddressOf ResourceNeeded
            RemoveHandler _itemSelector.SectionChangeEvent, AddressOf NewSection
            RemoveHandler _itemSelector.TestPartChangeEvent, AddressOf NewTestPart
        End Sub

        Public Function GetMaxItemDurationFromItemParameters(itemCode As String) As Integer
            Dim item As AssessmentItem = GetItemByCode(itemCode)
            If item IsNot Nothing Then Return GetMaxItemDurationFromItemParameters(item.Parameters)
            Return 0
        End Function

        Public Function GetMaxItemDurationFromItemParameters(ByVal parameters As ParameterSetCollection) As Integer
            Const prmName As String = "maxItemDuration"
            Dim param As IntegerParameter = Nothing

            For x As Integer = 0 To parameters.Count - 1
                If Not parameters(x).GetParameterByName(prmName, False) Is Nothing Then
                    param = DirectCast(parameters(x).GetParameterByName(prmName, False), IntegerParameter)

                    Exit For
                End If
            Next

            Return If(param Is Nothing, 0, param.Value)
        End Function

        Protected Function LoadPackage(bankId As Integer, testNames As IList(Of String), testPackageNames As IList(Of String)) As ResourceEntryCollection
            ResourceMan = New DataBaseResourceManager(bankId, True)
            Dim returnValue = New ResourceEntryCollection()

            For Each testName In testNames
                returnValue.Add(ResourceMan.GetResourceEntry(testName))
            Next

            For Each testPackageName In testPackageNames
                returnValue.Add(ResourceMan.GetResourceEntry(testPackageName))
            Next

            Return returnValue
        End Function

        Public Shared Function CleanCache(directory As DirectoryInfo) As Boolean
            Dim returnValue As Boolean = True

            If directory IsNot Nothing AndAlso directory.Exists Then
                Dim attempt As Integer = 0

                Do
                    Try
                        directory.Delete(True)

                        Exit Do
                    Catch ex As IO.IOException When attempt < 3
                        Threading.Thread.Sleep(250)
                    Catch ex As Exception
                        Return False
                    End Try

                    attempt += 1
                Loop
            End If

            If Not returnValue Then
                For Each recDirectory As DirectoryInfo In directory.GetDirectories
                    returnValue = CleanCache(recDirectory)
                Next
            End If

            Return returnValue
        End Function

        Protected Sub HandleProgress(message As String, stepCount As Nullable(Of Integer))
            If stepCount.HasValue Then
                _stepCounter = 1
                RaiseEvent StartProgress(Me, New StartEventArgs(stepCount.Value))
            Else
                RaiseEvent Progress(Me, New ProgressEventArgs(message, _stepCounter))
                _stepCounter = _stepCounter + 1
            End If
        End Sub

        Protected Function GetAssessmentTest(testEntry As ResourceEntry) As AssessmentTest2
            Dim request = New ResourceRequestDTO With {.WithCustomProperties = True}
            Dim testResource As BinaryResource = ResourceMan.GetResource(testEntry.Name, AddressOf StreamConverters.ConvertStreamToString, request)

            Return DirectCast(SerializeHelper.XmlDeserializeFromString(DirectCast(testResource.ResourceObject, String), GetType(AssessmentTest2)), AssessmentTest2)
        End Function

        Protected Function GetTestPackage(testEntry As ResourceEntry) As TestPackage
            Dim request = New ResourceRequestDTO With {.WithCustomProperties = True}
            Dim testPackageResource As BinaryResource = ResourceMan.GetResource(testEntry.Name, AddressOf StreamConverters.ConvertStreamToString, request)
            Return DirectCast(SerializeHelper.XmlDeserializeFromString(DirectCast(testPackageResource.ResourceObject, String), GetType(TestPackage)), TestPackage)
        End Function

        Protected Function CreateGeneralError(ex As Exception) As PublicationError
            Return New PublicationError(ex.Message, ex.GetType.ToString, ex.Message)
        End Function

        Protected Sub AddErrors(errorList As BlockingCollection(Of ChainHandlerException))
            For Each handlerEx In errorList
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
                    Dim messages = handlerEx.Message.Replace("object:", "|").Replace("message:", "|")

                    If messages.Count = 2 Then
                        publicationError.EntityProcessed = messages(0)
                        publicationError.Message = messages(1)
                    End If

                    Errors.Add(publicationError)
                End If
            Next
        End Sub

        Public Event Progress(ByVal sender As Object, ByVal e As Cito.Tester.Common.ProgressEventArgs)
        Public Event packageCreated(ByVal sender As Object, ByVal e As EventArgs)
        Public Event StartProgress(ByVal sender As Object, ByVal e As Cito.Tester.Common.StartEventArgs)

        Protected Sub Initialise()
            AllDependentResourceNames = New List(Of String)
            CachedItems = New ConcurrentDictionary(Of String, AssessmentItem)
            CachedTests = New ConcurrentDictionary(Of String, AssessmentTest2)
            Errors = New BlockingCollection(Of PublicationError)()
            Warnings = New BlockingCollection(Of PublicationError)()
            TempWorkingDirectory = New DirectoryInfo(Path.Combine(TempStorageHelper.GetTempStoragePath, Guid.NewGuid.ToString))

            If Not Directory.Exists(TempWorkingDirectory.FullName) Then
                IO.Directory.CreateDirectory(TempWorkingDirectory.FullName)
            Else
                Throw New Exception("Temp folder cannot be cleaned!")
            End If
        End Sub

        Private Function SetupItem(ByVal item As AssessmentItem, assessmentTestViewType As String) As String
            Dim itemLayoutTemplateEventArgs As New ResourceNeededEventArgs(item.LayoutTemplateSourceName, New ResourceProcessingFunction(AddressOf StreamConverters.ConvertStreamToString))

            ResourceNeeded(Nothing, itemLayoutTemplateEventArgs)

            itemLayoutTemplateEventArgs.GetResource(Of String)()
            Dim parser As New ItemLayoutAdapter(item.LayoutTemplateSourceName, item.Parameters, AddressOf ResourceNeeded)
            _contextIdentifier = 1
            Dim parsedDocument As XHtmlDocument = parser.ParseTemplate(assessmentTestViewType, False, _contextIdentifier)

            Return parsedDocument.OuterXml
        End Function

        Public Function GetItemByCode(ByVal itemCode As String) As AssessmentItem
            Dim itemResourceEventArgs As New ResourceNeededEventArgs(itemCode, AddressOf StreamConverters.ConvertStreamToByteArray)

            Dim assessmentItem As AssessmentItem = Nothing
            If Not CachedItems.ContainsKey(itemCode) Then
                ResourceNeeded(Nothing, itemResourceEventArgs)
                If itemResourceEventArgs.BinaryResource IsNot Nothing Then
                    assessmentItem = DirectCast(Cito.Tester.Common.SerializeHelper.XmlDeserializeFromByteArray(DirectCast(itemResourceEventArgs.BinaryResource.ResourceObject, Byte()), GetType(AssessmentItem), True), AssessmentItem)
                    CachedItems.TryAdd(itemCode, assessmentItem)
                End If
            Else
                assessmentItem = CachedItems(itemCode)
            End If
            Return assessmentItem
        End Function

        Public Function GetTestByCode(ByVal testIdentifier As String) As AssessmentTest2
            Dim testResourceEventArgs As New ResourceNeededEventArgs(testIdentifier, GetType(AssessmentTest2))

            Dim assessmentTest As AssessmentTest2 = Nothing
            If Not CachedTests.ContainsKey(testIdentifier) Then
                ResourceNeeded(Nothing, testResourceEventArgs)

                If testResourceEventArgs.BinaryResource IsNot Nothing Then
                    assessmentTest = DirectCast(testResourceEventArgs.BinaryResource.ResourceObject, AssessmentTest2)
                    CachedTests.TryAdd(testIdentifier, assessmentTest)
                End If
            Else
                assessmentTest = CachedTests(testIdentifier)
            End If
            Return assessmentTest
        End Function

        Public Function GetAspectByCode(ByVal code As String) As Aspect Implements IPackageCreator.GetAspectByCode
            If Not AspectHelper.IsDefaultResourceAspect(code) Then
                Dim aspectResourceEventArgs As New ResourceNeededEventArgs(code, GetType(Aspect))
                ResourceNeeded(Nothing, aspectResourceEventArgs)
                Return DirectCast(aspectResourceEventArgs.BinaryResource.ResourceObject, Aspect)
            Else
                Return New Aspect() With {.Title = code, .Identifier = code}
            End If
        End Function

        Public Function ParseTemplate(ByVal itemCode As String, assessmentTestViewType As String, item As AssessmentItem) As String
            Dim curItem = item
            If curItem Is Nothing Then
                curItem = GetItemByCode(itemCode)
            End If
            Dim templateString As String = SetupItem(curItem, assessmentTestViewType)

            Return templateString
        End Function

        Public Function GetMetadata(itemcode As String) As MetaDataCollection
            Dim itemMetaDataCollection As MetaDataCollection
            If TypeOf ResourceMan Is DataBaseResourceManager Then
                DirectCast(ResourceMan, DataBaseResourceManager).IncludeMetaData = Questify.Builder.Logic.ResourceManager.MetaDataType.Publishable
            End If

            itemMetaDataCollection = ResourceMan.GetResourceMetaData(itemcode)
            Dim dependentResourceCollection As DependentResourceCollection = ResourceMan.GetDependentResourcesForResource(itemcode)
            AddDependentResources(dependentResourceCollection)
            Return itemMetaDataCollection
        End Function

        Private Sub AddDependentResources(ByVal dependentResourceCollection As DependentResourceCollection)
            For Each dependentResource As DependentResource In dependentResourceCollection
                If Not (AllDependentResourceNames.Contains(dependentResource.Name)) Then
                    AllDependentResourceNames.Add(dependentResource.Name)
                    AddDependentResources(ResourceMan.GetDependentResourcesForResource(dependentResource.Name))
                End If
            Next
        End Sub

        Public Sub DisposeResourceManager()
            If ResourceMan IsNot Nothing AndAlso ResourceMan.GetType() = GetType(IDisposable) Then
                DirectCast(ResourceMan, IDisposable).Dispose()
            End If

        End Sub

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    DisposeResourceManager()
                    RemoveHandler TestSessionContext.ResourceNeeded, AddressOf ResourceNeeded
                End If
            End If
            Me.disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Sub New()
            Initialise()
        End Sub

        Public Sub New(ByVal handlerConfig As PluginHandlerConfigCollection)
            Me.New()
        End Sub

        Public Sub ResourceNeeded(sender As Object, e As ResourceNeededEventArgs) Implements IPackageCreator.ResourceNeeded
            Dim _resource As BinaryResource = Nothing
            Dim request = New ResourceRequestDTO With {.WithCustomProperties = True}
            If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
                If e.TypedResourceType IsNot Nothing Then
                    Debug.Assert(e.TypedResourceType IsNot GetType(AssessmentItem), "Assessment item should not be read using TypedResourceType because of whitespace handling!")

                    _resource = ResourceMan.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
                Else
                    _resource = ResourceMan.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
                End If

                e.BinaryResource = _resource
            End If

            If (e.Command And ResourceNeededCommand.MetaData) = ResourceNeededCommand.MetaData Then
                Dim fetchedMetaData As MetaDataCollection = ResourceMan.GetResourceMetaData(e.ResourceName)
                e.Metadata.AddRange(fetchedMetaData)
            End If
        End Sub

        Public Overridable Function GetAssessmentTestViewType() As String Implements IPackageCreator.GetAssessmentTestViewType
            Return GenericTestModelPlugin.PLUGIN_NAME
        End Function
    End Class
End Namespace