
Imports Questify.Builder.Security
Imports Questify.Builder.Security.ActiveDirectory

<TestClass()>
Public Class SecurityFactoryTest

    <TestCleanup()>
    Public Sub MyTestCleanup()
        SecurityFactory.Destroy()
    End Sub


    <TestMethod()>
    Public Sub SecurityFactory_AuthenticationProviderTest()
        Assert.IsNotNull(SecurityFactory.AuthenticationProvider, "SecurityFactory.AuthenticationProvider was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub SecurityFactory_InstantiateTest()
        Dim expected As ISecurityService = New SecurityService()

        Dim actual As ISecurityService = SecurityFactory.Instantiate(expected)

        Assert.AreEqual(Of ISecurityService)(expected, actual, "SecurityFactory.Instantiate did not return the expected value.")
    End Sub

    <TestMethod()> _
    Public Sub SecurityFactory_IsinstantiatedTest()
        SecurityFactory.Instantiate(New SecurityService())
        Assert.IsTrue(SecurityFactory.Isinstantiated, "SecurityFactory.IsInstantiated was not set correctly.")

        SecurityFactory.Destroy()
        Assert.IsFalse(SecurityFactory.Isinstantiated, "SecurityFactory.IsInstantiated was not set correctly.")
    End Sub

End Class
