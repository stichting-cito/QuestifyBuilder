
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Direct
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

'''<summary>
'''This is a test class for AuthorizationFactory and is intended
'''to contain all AuthorizationFactory Unit Tests
'''</summary>
<TestClass()>
Public Class AuthorizationFactoryTest

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

    <TestCleanup()>
    Public Sub MyTestCleanup()
        AuthorizationFactory.Destroy()
    End Sub

    '''<summary>
    '''A test for Destroy()
    '''</summary>
    <TestMethod()>
    <ExpectedException(GetType(ServiceException), "Unexpected exception")>
    Public Sub AuthorizationFactory_DestroyTest()
        'Arrange
        AuthorizationFactory.Instantiate(New AuthorizationService())

        'Act
        AuthorizationFactory.Destroy()
        Dim auth As IAuthorizationService = AuthorizationFactory.Instance
        
        'Assert
        'Expects Exception
    End Sub

    '''<summary>
    '''A test for Instance()
    '''</summary>
    <TestMethod()>
    <ExpectedException(GetType(ServiceException), "Unexpected exception")>
    Public Sub AuthorizationFactory_InstanceTest()
        'Arrange
        Dim val As IAuthorizationService = DirectCast(New AuthorizationService, IAuthorizationService)

        'Act
        AuthorizationFactory.Instantiate(val)

        'Assert
        Assert.AreEqual(Of IAuthorizationService)(val, AuthorizationFactory.Instance, "AuthorizationFactory.Instance was not set correctly.")

        AuthorizationFactory.Destroy()
        Dim auth As IAuthorizationService = AuthorizationFactory.Instance
        Assert.Fail("ServiceException expected")
    End Sub

    '''<summary>
    '''A test for Instantiate(ByVal IAuthorizationService)
    '''</summary>
    <TestMethod()>
    <ExpectedException(GetType(ServiceException), "Unexpected exception")>
    Public Sub AuthorizationFactory_InstantiateTest()
        'Arrange
        Dim expected As IAuthorizationService = DirectCast(New AuthorizationService, IAuthorizationService)

        'Act
        Dim actual As IAuthorizationService = AuthorizationFactory.Instantiate(expected)

        'Assert
        Assert.AreEqual(Of IAuthorizationService)(expected, actual, "AuthorizationFactory.Instantiate did not return the expected value.")

        AuthorizationFactory.Instantiate(Nothing)
        Assert.Fail("ServiceException expected")
    End Sub

End Class

