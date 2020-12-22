
Imports FluentAssertions
Imports Questify.Builder.Security

<TestClass>
Public Class PasswordHashTests

    <TestMethod()>
    Public Sub HashPasswordReturnsSameResult()
        Dim plainTextPassword As String = "SomePa$$W0rd"

        Dim hashedPassword As String = PasswordHashing.CreateHash(plainTextPassword)

        PasswordHashing.Verify(plainTextPassword, hashedPassword).Should().BeTrue()
    End Sub

    <TestMethod>
    Public Sub HashedPasswordContainsPrefix()
        Dim plainTextPassword As String = "SomePa$$W0rd"

        Dim hashedPassword As String = PasswordHashing.CreateHash(plainTextPassword)

        hashedPassword.StartsWith(PasswordHashing.Prefix).Should().BeTrue()
    End Sub

    <TestMethod>
    Public Sub SmallPasswordCanBeHashedAndVerified()
        Dim plainTextPassword As String = "12"

        Dim hashedPassword As String = PasswordHashing.CreateHash(plainTextPassword)

        PasswordHashing.Verify(plainTextPassword, hashedPassword).Should().BeTrue()
    End Sub

End Class
