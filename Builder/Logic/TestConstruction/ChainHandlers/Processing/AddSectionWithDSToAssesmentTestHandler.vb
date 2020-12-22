Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel

Imports Cito.Tester.ContentModel.Datasources
Imports Cito.Tester.Common
Namespace TestConstruction.ChainHandlers.Processing
    ''' <summary>
    ''' This is a handler responsible for adding a new section combined with a datasource.
    ''' </summary>
    Public Class AddSectionWithDSToAssesmentTestHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)

#Region "Fields"
        Private _comparer As IEqualityComparer(Of ResourceRef) = New ResourceRefIdentityEqualityComparer
        Private _bankContext As ContentModel.EntityClasses.BankEntity
        Private _assessmentTest As Tester.ContentModel.AssessmentTest2
        Private _ParentSection As Tester.ContentModel.TestSection2
        Private _insertAtPosition As Integer
        Private _testEntity As AssessmentTestResourceEntity
        Private _resourceManager As DataBaseResourceManager
        Private _datasourceToAdd As Questify.Builder.Model.ContentModel.EntityClasses.DataSourceResourceEntity
        Private _NameOfDatasourceToAdd As String
#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AddSectionWithDSToAssesmentTestHandler" /> class. This class will add a new section
        ''' with a link to the data source (normal or inclusion group). It will add all items to it.
        ''' </summary>
        ''' <param name="resourceManager">The resource manager.</param>
        ''' <param name="datasourceToAdd">The datasource to add.</param>
        ''' <param name="assessmentTest">The assessment test.</param>
        ''' <param name="parentSection">The parent section.</param>
        ''' <param name="insertAtPosition">The insert at position.</param>
        ''' <param name="testEntity">The test entity.</param>
        Sub New(resourceManager As DataBaseResourceManager,
                assessmentTest As AssessmentTest2,
                parentSection As TestSection2,
                insertAtPosition As Integer,
                testEntity As AssessmentTestResourceEntity)
            ' TODO: Complete member initialization 
            _resourceManager = resourceManager
            _bankContext = resourceManager.BankContext
            _assessmentTest = assessmentTest
            _ParentSection = parentSection
            _insertAtPosition = insertAtPosition
            _testEntity = testEntity
        End Sub

#End Region


        Public Overrides Function ProcessRequest(requestData As TestConstructionRequest) As ChainHandlerResult

            If (_datasourceToAdd Is Nothing) Then
                Return ChainHandlerResult.RequestHandled
            End If

            'What are we trying to do here? By action of user of otherwise we need to create a section with all the items
            'from an inclusion group. 
            'ParentSection
            '   +
            '   |
            '   +---> CreatedSection(this is an inclusionGroup)
            '           +
            '           |
            '           +-Item
            '           |
            '           +-Item
            '           |
            '           +-Item

            '--[Create the new Section]--
            Dim createdSection As CreatedTestNodeAndViews(Of TestSection2, TestSectionViewBase) = AssessmentTestv2Factory.CreateTestSectionAndViews(_assessmentTest.IncludedViews)
            Dim selectedComponent As AssessmentTestNode = _ParentSection '_addToSection is the Section that is

            '--[Add Created Section To ParentSection]
            _ParentSection.Components.Add(createdSection.TestNode)
            'set default properties
            createdSection.TestNode.Identifier = Guid.NewGuid.ToString()
            '   set Name
            createdSection.TestNode.Title = _datasourceToAdd.Name
            '   WHAT DOES THIS DO?
            For Each testSectionView As TestSectionViewBase In createdSection.Views
                AssessmentTestv2Factory.SetDefaultTestSectionViewSettings(testSectionView)
            Next

            '--[Set DataSource (inclusionGroup) to CreatedSection and add all items from InclusionGroup]
            createdSection.TestNode.ItemDataSource = _datasourceToAdd.Name 'No Need to remove
            'Add Data Source
            DependencyManagement.AddDependentResourceToResource(_testEntity, _datasourceToAdd)
            'Now just add all items that are NOT conflicting in any way.
            Dim toAdd As New List(Of ResourceRef)
            Dim want2Add As New List(Of ResourceRef)
            want2Add.AddRange(ItemHelpers.GetItemsFromDataSource(_resourceManager, _datasourceToAdd))

            'Filter out any exclusion groups
            Dim group As Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))
            Dim itemsNot2Use As List(Of ResourceRef)
            group = ItemHelpers.GetItemsPerGroup(_resourceManager, New String() {ItemHelpers.Exclusion})

            Dim _AllNotConflictingItems As IEnumerable(Of ResourceRef) = SetOperations.Union(requestData.Items, requestData.ItemContext, _comparer)
            Dim _AllItems As IEnumerable(Of ResourceRef) = SetOperations.Union(_AllNotConflictingItems, want2Add, _comparer)

            For Each exl As DataSourceSettings In group.Keys
                'all items in exclusion group
                itemsNot2Use = New List(Of ResourceRef)(CType(exl.CreateGetDataSource(), ItemDataSource).Get(_resourceManager))
                Dim tmp As IEnumerable(Of ResourceRef) = SetOperations.Intersect(_AllItems, itemsNot2Use, _comparer)
                'Does the exclusion group intersects the all the items,.. remove unwanted items from "want2Add" to add group. 
                If SetOperations.ContainsAll(tmp, itemsNot2Use, _comparer) Then

                    '--[Remove items that are intersecting, they cause conflicts]
                    'At least one item should be present in the want2Add collection.
                    For Each _2rm As ResourceRef In itemsNot2Use
                        'remove possible item.
                        Dim indx As Integer = -1
                        'find item 2 remove
                        For value As Integer = 0 To want2Add.Count - 1
                            If _comparer.Equals(want2Add(value), _2rm) Then
                                indx = value 'store index
                            End If
                        Next
                        'remove the found item.
                        If indx <> -1 Then want2Add.RemoveAt(indx)
                    Next

                    'Refresh the list
                    _AllItems = SetOperations.Union(_AllNotConflictingItems, want2Add, _comparer)
                End If

                Dim remove As IEnumerable(Of ResourceRef)
                remove = SetOperations.Intersect(want2Add, itemsNot2Use, _comparer)
                itemsNot2Use.AddRange(remove)
            Next
            '--[Filter out items NOT to use]
            'toAdd.AddRange(SetOperations.Difference(itemsNot2Use, want2Add, _comparer))
            toAdd.AddRange(want2Add)

            '--[AddItems]

            Dim add_Handler As New AddToAssessmentTestHandler(_bankContext, _assessmentTest, createdSection.TestNode, 0)

            Dim result As ChainHandlerResult

            Dim request As New TestConstructionRequest(TestConstructionRequest.RequestTypeEnum.Add,
                                                                            toAdd,
                                                                            New ResourceRef() {})

            result = add_Handler.ProcessRequest(request)

            If (result = ChainHandlerResult.RequestHandled) Then
                '--[AddDependency]
                Dim add_dep As New AddDependenciesToAssessmentTest(_testEntity)
                Dim result2 As ChainHandlerResult
                result2 = add_dep.ProcessRequest(request)
                Return result2
            Else
                Debug.Assert(False, "adding items to test failed.")
                Return result
            End If

        End Function

        Private Sub TryFindDataSource()
            Dim dataSource As DataSourceResourceEntity
            'After this handles successfully this handler is enabled to add new
            'Sections with datasources.
            dataSource = TryCast( _
                ResourceFactory.Instance.GetResourceByName(_bankContext, _NameOfDatasourceToAdd), 
                DataSourceResourceEntity)

            Debug.Assert(Not dataSource Is Nothing)

            _datasourceToAdd = dataSource
        End Sub

        ''' <summary>
        ''' Gets or sets the data source to add. This handler is dependent on it. If not found it will not process.
        ''' </summary>
        ''' <value>
        ''' The data source to add.
        ''' </value>
        Public Property DatasourceToAdd As String
            Get
                Return _NameOfDatasourceToAdd
            End Get
            Set(value As String)
                Debug.Assert(Not value Is Nothing)
                _NameOfDatasourceToAdd = value
                TryFindDataSource()
            End Set
        End Property


        Private Sub AddDependentResourceToTest(ByVal testResource As AssessmentTestResourceEntity, ByVal resource As ResourceEntity)
            If testResource Is Nothing Then
                Throw New ArgumentNullException("testResource")
            End If

            If resource Is Nothing Then
                Throw New ArgumentNullException("resource")
            End If

            If Not testResource.ContainsDependentResource(resource) Then
                DependencyManagement.AddDependentResourceToResource(testResource, resource)
            End If
        End Sub



    End Class
End Namespace