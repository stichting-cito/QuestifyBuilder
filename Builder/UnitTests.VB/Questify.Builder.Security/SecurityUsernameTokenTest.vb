
Imports Microsoft.Web.Services3.Security.Tokens
Imports Questify.Builder.Security

<TestClass()> _
Public Class SecurityUsernameTokenTest
    <TestMethod()> _
    Public Sub SecurityUsernameToken_PolicyTest()
        Dim currentPolicy As String = SecurityUsernameToken.Policy

        Dim val As String = "My Policy"
        SecurityUsernameToken.Policy = val

        Assert.AreEqual(Of String)(val, SecurityUsernameToken.Policy, "Cito.TestBuilder.Security.SecurityUsernameToken.Policy was not set correctly.")

        SecurityUsernameToken.Policy = currentPolicy
    End Sub

    <TestMethod()> _
    Public Sub SecurityUsernameToken_UsernameTokenTest()
        Dim currentToken As UsernameToken = SecurityUsernameToken.UsernameToken
        Dim val As New UsernameToken("user", "pass")
        SecurityUsernameToken.UsernameToken = val

        Assert.AreEqual(Of UsernameToken)(val, SecurityUsernameToken.UsernameToken, "Cito.TestBuilder.Security.SecurityUsernameToken.UsernameToken was not set correctly.")

        SecurityUsernameToken.UsernameToken = currentToken
    End Sub

End Class
