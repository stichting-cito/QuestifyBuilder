
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Direct
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

<TestClass()>
Public Class AuthorizationFactoryTest

    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestCleanup()>
    Public Sub MyTestCleanup()
        AuthorizationFactory.Destroy()
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ServiceException), "Unexpected exception")>
    Public Sub AuthorizationFactory_DestroyTest()
        AuthorizationFactory.Instantiate(New AuthorizationService())

        AuthorizationFactory.Destroy()
        Dim auth As IAuthorizationService = AuthorizationFactory.Instance

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ServiceException), "Unexpected exception")>
    Public Sub AuthorizationFactory_InstanceTest()
        Dim val As IAuthorizationService = DirectCast(New AuthorizationService, IAuthorizationService)

        AuthorizationFactory.Instantiate(val)

        Assert.AreEqual(Of IAuthorizationService)(val, AuthorizationFactory.Instance, "AuthorizationFactory.Instance was not set correctly.")

        AuthorizationFactory.Destroy()
        Dim auth As IAuthorizationService = AuthorizationFactory.Instance
        Assert.Fail("ServiceException expected")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ServiceException), "Unexpected exception")>
    Public Sub AuthorizationFactory_InstantiateTest()
        Dim expected As IAuthorizationService = DirectCast(New AuthorizationService, IAuthorizationService)

        Dim actual As IAuthorizationService = AuthorizationFactory.Instantiate(expected)

        Assert.AreEqual(Of IAuthorizationService)(expected, actual, "AuthorizationFactory.Instantiate did not return the expected value.")

        AuthorizationFactory.Instantiate(Nothing)
        Assert.Fail("ServiceException expected")
    End Sub

End Class

