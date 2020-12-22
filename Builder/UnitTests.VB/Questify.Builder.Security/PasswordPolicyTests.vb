
Imports FluentAssertions
Imports Questify.Builder.Security

<TestClass()>
Public Class PasswordPolicyTests
    Dim _userName As String = "JohnDoe"
    Dim _policy As PasswordPolicy = New PasswordPolicy

    <TestMethod()>
    Public Sub ValidPassesPolicy()
        Dim password As String = "Ab0$ifjnvn"

        Dim actual = _policy.IsValid(password, _userName)

        actual.Should().BeTrue()
    End Sub

    <TestMethod>
    Public Sub TooShortDoesNotPassPolicy()
        Dim password As String = "Ab0$"

        Dim actual = _policy.IsValid(password, _userName)

        actual.Should().BeFalse()
    End Sub

    <TestMethod>
    Public Sub TooSimpleDoesNotPassPolicy()
        Dim password As String = "Abaaaaaaaaa"

        Dim actual = _policy.IsValid(password, _userName)

        actual.Should().BeFalse()
    End Sub


    <TestMethod>
    Public Sub ContainsUsernameDoesNotPassPolicy()
        Dim password As String = "Ab0$" & _userName

        Dim actual = _policy.IsValid(password, _userName)

        actual.Should().BeFalse()
    End Sub

End Class
