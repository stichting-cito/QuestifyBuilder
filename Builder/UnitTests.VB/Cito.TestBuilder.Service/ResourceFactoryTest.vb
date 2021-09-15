
Imports FakeItEasy
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Direct
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

'''<summary>
'''This is a test class for ResourceFactory and is intended
'''to contain all ResourceFactory Unit Tests
'''</summary>
<TestClass()>
Public Class ResourceFactoryTest

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

    <TestInitialize()>
    Public Sub MyTestInitialize()
        PermissionFactory.Instantiate(A.Fake(Of IPermissionService))
    End Sub

    <TestCleanup()>
    Public Sub MyTestCleanup()
        ResourceFactory.Destroy()
        PermissionFactory.Destroy()
    End Sub

    '''<summary>
    '''A test for Destroy()
    '''</summary>
    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))>
    Public Sub ResourceFactory_DestroyTest()
        'Arrange
        ResourceFactory.Instantiate(New ResourceService())

        'Act
        ResourceFactory.Destroy()
        Dim auth As IResourceService = ResourceFactory.Instance

        'Assert
        'Expects Exception
    End Sub

    '''<summary>
    '''A test for Instance()
    '''</summary>
    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))>
    Public Sub ResourceFactory_InstanceTest()
        'Arrange
        Dim val As IResourceService = DirectCast(New ResourceService, IResourceService)

        'Act
        ResourceFactory.Instantiate(val)

        'Assert
        Assert.AreEqual(Of IResourceService)(val, ResourceFactory.Instance, "ResourceFactory.Instance was not set correctly.")

        ResourceFactory.Destroy()
        Dim auth As IResourceService = ResourceFactory.Instance
        Assert.Fail("ServiceException expected")
    End Sub

    '''<summary>
    '''A test for Instantiate(ByVal IResourceService)
    '''</summary>
    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))>
    Public Sub ResourceFactory_InstantiateTest()
        'Arrange
        Dim expected As IResourceService = DirectCast(New ResourceService, IResourceService)

        'Act
        Dim actual As IResourceService = ResourceFactory.Instantiate(expected)

        'Assert
        Assert.AreEqual(Of IResourceService)(expected, actual, "ResourceFactory.Instantiate did not return the expected value.")

        ResourceFactory.Instantiate(Nothing)
        Assert.Fail("ServiceException expected")
    End Sub

End Class
