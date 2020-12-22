
Imports FakeItEasy
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Direct
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

<TestClass()> _
Public Class BankFactoryTest

    Private testContextInstance As TestContext

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

    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))> _
    Public Sub BankFactory_DestroyTest()
        BankFactory.Instantiate(New BankService())

        BankFactory.Destroy()
        Dim auth As IBankService = BankFactory.Instance

    End Sub

    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))> _
    Public Sub BankFactory_InstanceTest()
        Dim val As IBankService = DirectCast(New BankService, IBankService)

        BankFactory.Instantiate(val)

        Assert.AreEqual(Of IBankService)(val, BankFactory.Instance, "Cito.BankFactory.Instance was not set correctly.")

        BankFactory.Destroy()
        Dim auth As IBankService = BankFactory.Instance
        Assert.Fail("ServiceException expected")
    End Sub

    <TestMethod(), TestCategory("Services")>
    <ExpectedException(GetType(ServiceException))> _
    Public Sub BankFactory_InstantiateTest()
        Dim expected As IBankService = DirectCast(New BankService, IBankService)

        Dim actual As IBankService = BankFactory.Instantiate(expected)

        Assert.AreEqual(Of IBankService)(expected, actual, "Cito.BankFactory.Instantiate did not return the expected value.")

        BankFactory.Instantiate(Nothing)
        Assert.Fail("ServiceException expected")
    End Sub

End Class
