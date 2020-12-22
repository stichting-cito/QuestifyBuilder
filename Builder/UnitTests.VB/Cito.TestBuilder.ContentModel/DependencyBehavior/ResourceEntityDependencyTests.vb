Imports System.Text
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Cito.TestBuilder.ContentModel.EntityClasses
Imports Cito.TestBuilder.UnitTest.Framework

<TestClass()>
Public Class ResourceEntityDependencyTests

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Use TestInitialize to run code before running each test
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Use TestCleanup to run code after each test has run
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region


    <TestMethod(), Owner("FlBr"), TestCategory("QBContentModel"), WorkItem(9608)>
    Public Sub ProblemRepro_AddDependenResourceToResourceEntity()
        'Arrange
        Dim entity As ResourceEntity = New ItemResourceEntity()
        Dim genericRs As New GenericResourceEntity() With {.Name = "TestDependency"}

        'Act
        Dim depResource As New DependentResourceEntity() With
            {
                .Resource = entity,
                .DependentResource = genericRs
            }      'This is responsible for adding 1

        entity.DependentResourceCollection.Add(depResource) 'Api wise this seems like the correct call
        'Assert
        Assert.AreEqual(2, entity.DependentResourceCollection.Count) 'THis is the problem,.. the result is 2
        'Since this is generated code by LLBLGen,.. we have no influence so we need to be aware of this.
        Assert.AreSame(genericRs, entity.DependentResourceCollection(0).DependentResource)
        Assert.AreSame(genericRs, entity.DependentResourceCollection(1).DependentResource)
        Assert.AreEqual(1, genericRs.ReferencedResourceCollection.Count) 'The dependent resource is aware of it's user.
        Assert.AreSame(entity, genericRs.ReferencedResourceCollection(0).Resource)
    End Sub

    <TestMethod(), Owner("FlBr"), TestCategory("QBContentModel"), WorkItem(9608)>
    Public Sub AddDependenResourceToResourceEntity()
        'Arrange
        Dim entity As ResourceEntity = New ItemResourceEntity()
        Dim genericRs As New GenericResourceEntity() With {.Name = "TestDependency"}

        'Act
        Dim depResource As New DependentResourceEntity() With
            {
                .Resource = entity,
                .DependentResource = genericRs
            }
        'Assert
        Assert.AreEqual(1, entity.DependentResourceCollection.Count)
        Assert.AreEqual(1, genericRs.ReferencedResourceCollection.Count) 'The dependent resource is aware of it's user.
        Assert.AreSame(entity, genericRs.ReferencedResourceCollection(0).Resource)
    End Sub

    <TestMethod(), Owner("FlBr"), TestCategory("QBContentModel"), WorkItem(9608)>
    Public Sub AddDependenResourceToResourceEntityAlternative()
        'Arrange
        Dim entity As ResourceEntity = New ItemResourceEntity()
        Dim genericRs As New GenericResourceEntity() With {.Name = "TestDependency"}
        'Act
        entity.DependentResourceCollection.AddNew().DependentResource = genericRs
        'Assert
        Assert.AreEqual(1, entity.DependentResourceCollection.Count)
        Assert.AreEqual(1, genericRs.ReferencedResourceCollection.Count) 'The dependent resource is aware of it's user.
        Assert.AreSame(entity, genericRs.ReferencedResourceCollection(0).Resource)
    End Sub

End Class
