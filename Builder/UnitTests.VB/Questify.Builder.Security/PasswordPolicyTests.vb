
Imports FluentAssertions
Imports Questify.Builder.Security

<TestClass()>
Public Class PasswordPolicyTests
    Dim _userName As String = "JohnDoe"
    Dim _policy As PasswordPolicy = New PasswordPolicy

    <TestMethod()>
    Public Sub ValidPassesPolicy()
        'Arrange
        Dim password As String = "Ab0$ifjnvn"

        'Act
        Dim actual = _policy.IsValid(password, _userName)

        'Assert
        actual.Should().BeTrue()
    End Sub

    <TestMethod>
    Public Sub TooShortDoesNotPassPolicy()
        'Arrange
        Dim password As String = "Ab0$"

        'Act
        Dim actual = _policy.IsValid(password, _userName)

        'Assert
        actual.Should().BeFalse()
    End Sub
    
    <TestMethod>
    Public Sub TooSimpleDoesNotPassPolicy()
        'Arrange
        Dim password As String = "Abaaaaaaaaa"

        'Act
        Dim actual = _policy.IsValid(password, _userName)

        'Assert
        actual.Should().BeFalse()
    End Sub

    
    <TestMethod>
    Public Sub ContainsUsernameDoesNotPassPolicy()
        'Arrange
        Dim password As String = "Ab0$" & _userName

        'Act
        Dim actual = _policy.IsValid(password, _userName)

        'Assert
        actual.Should().BeFalse()
    End Sub

End Class
