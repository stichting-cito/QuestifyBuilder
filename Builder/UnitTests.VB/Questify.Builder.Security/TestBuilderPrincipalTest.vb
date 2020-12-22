'ELMO
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Questify.Builder.Security

<TestClass()>
Public Class TestBuilderPrincipalTest

    <TestMethod()>
    Public Sub TestBuilderPrincipal_GetAuthPrincipalTest()
        'Arrange
        Dim identity As New TestBuilderIdentity()
        Dim actual As TestBuilderPrincipal = Cito.TestBuilder.Security.TestBuilderPrincipal.GetAuthPrincipal(identity)

        Assert.IsNull(actual, "Cito.TestBuilder.Security.TestBuilderPrincipal.GetAuthPrincipal did not return the expected value.")

        identity.UserId = 1 ' Identity is now authenticated

        'Act
        actual = Cito.TestBuilder.Security.TestBuilderPrincipal.GetAuthPrincipal(identity)

        'Assert
        Assert.AreEqual(Of TestBuilderIdentity)(identity, DirectCast(actual.Identity, TestBuilderIdentity), "TestBuilderPrincipal.GetAuthPrincipal did not return the expected value.")
    End Sub

    <TestMethod()>
    Public Sub TestBuilderPrincipal_ConstructorTest()
        'Arrange
        Dim identity As New TestBuilderIdentity()

        'Act
        Dim target As TestBuilderPrincipal = New TestBuilderPrincipal(identity)

        'Assert
        Assert.AreEqual(Of TestBuilderIdentity)(identity, DirectCast(target.Identity, TestBuilderIdentity), "Constructor did not set the identity correctly.")
    End Sub

End Class
