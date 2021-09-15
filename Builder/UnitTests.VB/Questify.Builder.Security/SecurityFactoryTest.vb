
Imports Questify.Builder.Security
Imports Questify.Builder.Security.ActiveDirectory

'''<summary>
'''This is a test class for SecurityFactory and is intended
'''to contain all SecurityFactory Unit Tests
'''</summary>
<TestClass()>
Public Class SecurityFactoryTest

    <TestCleanup()>
    Public Sub MyTestCleanup()
        SecurityFactory.Destroy()
    End Sub


    '''<summary>
    '''A test for AuthenticationProvider()
    '''</summary>
    <TestMethod()>
    Public Sub SecurityFactory_AuthenticationProviderTest()
        Assert.IsNotNull(SecurityFactory.AuthenticationProvider, "SecurityFactory.AuthenticationProvider was not set correctly.")
    End Sub

    '''<summary>
    '''A test for Instantiate(ByVal ISecurityService)
    '''</summary>
    <TestMethod()> _
    Public Sub SecurityFactory_InstantiateTest()
        'Arrange
        Dim expected As ISecurityService = New SecurityService()

        'Act
        Dim actual As ISecurityService = SecurityFactory.Instantiate(expected)

        'Assert
        Assert.AreEqual(Of ISecurityService)(expected, actual, "SecurityFactory.Instantiate did not return the expected value.")
    End Sub

    '''<summary>
    '''A test for IsInstantiated()
    '''</summary>
    <TestMethod()> _
    Public Sub SecurityFactory_IsinstantiatedTest()
        SecurityFactory.Instantiate(New SecurityService())
        Assert.IsTrue(SecurityFactory.Isinstantiated, "SecurityFactory.IsInstantiated was not set correctly.")

        SecurityFactory.Destroy()
        Assert.IsFalse(SecurityFactory.Isinstantiated, "SecurityFactory.IsInstantiated was not set correctly.")
    End Sub

End Class
