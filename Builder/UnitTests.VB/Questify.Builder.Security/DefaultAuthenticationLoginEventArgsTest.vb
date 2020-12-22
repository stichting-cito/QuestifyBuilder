
Imports Questify.Builder.Security

<TestClass()> _
Public Class AuthenticationLoginEventArgsTest

    Private _target As New AuthenticationLoginEventArgs()

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_CancelTest()

        _target.Cancel = True

        Assert.IsTrue(_target.Cancel, "AuthenticationLoginEventArgs.Cancel was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_ClientTest()
        Dim val As String = "Client X"

        _target.Client = val

        Assert.AreEqual(Of String)(val, _target.Client, "AuthenticationLoginEventArgs.Client was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_PasswordTest()
        Dim val As String = "Welkom01"

        _target.Password = val

        Assert.AreEqual(Of String)(val, _target.Password, "AuthenticationLoginEventArgs.Password was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_UsernameTest()
        Dim val As String = "User X"

        _target.Username = val

        Assert.AreEqual(val, _target.Username, "AuthenticationLoginEventArgs.Username was not set correctly.")
    End Sub

End Class
