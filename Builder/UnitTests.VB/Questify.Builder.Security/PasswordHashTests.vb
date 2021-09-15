
Imports FluentAssertions
Imports Questify.Builder.Security

<TestClass>
Public Class PasswordHashTests

    <TestMethod()>
    Public Sub HashPasswordReturnsSameResult()
        'Arrange
        Dim plainTextPassword As String = "SomePa$$W0rd"
        
        'Act
        Dim hashedPassword As String = PasswordHashing.CreateHash(plainTextPassword)

        'Assert
        PasswordHashing.Verify(plainTextPassword, hashedPassword).Should().BeTrue()
    End Sub

    <TestMethod>
    Public Sub HashedPasswordContainsPrefix()
        'Arrange
        'Encrypted passwords stored with prefix so it can be determined that they are encrypted
        'If password does not start with prefix it is not converted from plain text yet
        Dim plainTextPassword As String = "SomePa$$W0rd"

        'Act
        Dim hashedPassword As String = PasswordHashing.CreateHash(plainTextPassword)

        'Assert
        hashedPassword.StartsWith(PasswordHashing.Prefix).Should().BeTrue()
    End Sub

    <TestMethod>
    Public Sub SmallPasswordCanBeHashedAndVerified()
        'Arrange
        Dim plainTextPassword As String = "12"

        'Act
        Dim hashedPassword As String = PasswordHashing.CreateHash(plainTextPassword)

        'Assert
        PasswordHashing.Verify(plainTextPassword, hashedPassword).Should().BeTrue()
    End Sub

End Class
