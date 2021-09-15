
Imports FakeItEasy
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Direct
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

'''<summary>
'''This is a test class for Cito.BankFactory and is intended
'''to contain all Cito.BankFactory Unit Tests
'''</summary>
<TestClass()> _
Public Class BankFactoryTest

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

    <TestInitialize()> _
    Public Sub MyTestInitialize()
        PermissionFactory.Instantiate(A.Fake(Of IPermissionService))
        SecurityFactory.Instantiate(A.Fake(Of ISecurityService))
    End Sub

    <TestCleanup()> _
    Public Sub MyTestCleanup()
        BankFactory.Destroy()
        PermissionFactory.Destroy()
        SecurityFactory.Destroy()
    End Sub

    '''<summary>
    '''A test for Destroy()
    '''</summary>
    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))> _
    Public Sub BankFactory_DestroyTest()
        'Arrange
        'Claiming ownership since fixed
        BankFactory.Instantiate(New BankService())

        'Act
        BankFactory.Destroy()
        Dim auth As IBankService = BankFactory.Instance
        
        'Assert
        'Expects Exception
    End Sub

    '''<summary>
    '''A test for Instance()
    '''</summary>
    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))> _
    Public Sub BankFactory_InstanceTest()
        'Arrange
        Dim val As IBankService = DirectCast(New BankService, IBankService)

        'Act
        BankFactory.Instantiate(val)

        'Assert
        Assert.AreEqual(Of IBankService)(val, BankFactory.Instance, "Cito.BankFactory.Instance was not set correctly.")

        BankFactory.Destroy()
        Dim auth As IBankService = BankFactory.Instance
        Assert.Fail("ServiceException expected")
    End Sub

    '''<summary>
    '''A test for Instantiate(ByVal IBankService)
    '''</summary>
    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))> _
    Public Sub BankFactory_InstantiateTest()
        'Arrange
        Dim expected As IBankService = DirectCast(New BankService, IBankService)

        'Act
        Dim actual As IBankService = BankFactory.Instantiate(expected)

        'Assert
        Assert.AreEqual(Of IBankService)(expected, actual, "Cito.BankFactory.Instantiate did not return the expected value.")

        BankFactory.Instantiate(Nothing)
        Assert.Fail("ServiceException expected")
    End Sub

End Class
