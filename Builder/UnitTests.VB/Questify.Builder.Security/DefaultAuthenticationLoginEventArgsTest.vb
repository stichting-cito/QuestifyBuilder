
Imports Questify.Builder.Security

<TestClass()> _
Public Class AuthenticationLoginEventArgsTest

    Private _target As New AuthenticationLoginEventArgs()

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_CancelTest()
        'Arrange

        'Act
        _target.Cancel = True

        'Assert
        Assert.IsTrue(_target.Cancel, "AuthenticationLoginEventArgs.Cancel was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_ClientTest()
        'Arrange
        Dim val As String = "Client X"

        'Act
        _target.Client = val

        'Assert
        Assert.AreEqual(Of String)(val, _target.Client, "AuthenticationLoginEventArgs.Client was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_PasswordTest()
        'Arrange
        Dim val As String = "Welkom01"
        
        'Act
        _target.Password = val

        'Assert
        Assert.AreEqual(Of String)(val, _target.Password, "AuthenticationLoginEventArgs.Password was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub AuthenticationLoginEventArgs_UsernameTest()
        'Arrange
        Dim val As String = "User X"

        'Act
        _target.Username = val

        'Assert
        Assert.AreEqual(val, _target.Username, "AuthenticationLoginEventArgs.Username was not set correctly.")
    End Sub

End Class
