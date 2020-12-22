
Imports FakeItEasy
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Direct
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

<TestClass()>
Public Class ResourceFactoryTest

    Private testContextInstance As TestContext

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

    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))>
    Public Sub ResourceFactory_DestroyTest()
        ResourceFactory.Instantiate(New ResourceService())

        ResourceFactory.Destroy()
        Dim auth As IResourceService = ResourceFactory.Instance

    End Sub

    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))>
    Public Sub ResourceFactory_InstanceTest()
        Dim val As IResourceService = DirectCast(New ResourceService, IResourceService)

        ResourceFactory.Instantiate(val)

        Assert.AreEqual(Of IResourceService)(val, ResourceFactory.Instance, "ResourceFactory.Instance was not set correctly.")

        ResourceFactory.Destroy()
        Dim auth As IResourceService = ResourceFactory.Instance
        Assert.Fail("ServiceException expected")
    End Sub

    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))>
    Public Sub ResourceFactory_InstantiateTest()
        Dim expected As IResourceService = DirectCast(New ResourceService, IResourceService)

        Dim actual As IResourceService = ResourceFactory.Instantiate(expected)

        Assert.AreEqual(Of IResourceService)(expected, actual, "ResourceFactory.Instantiate did not return the expected value.")

        ResourceFactory.Instantiate(Nothing)
        Assert.Fail("ServiceException expected")
    End Sub

End Class
