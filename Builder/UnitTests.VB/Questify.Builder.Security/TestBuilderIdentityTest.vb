
Imports Questify.Builder.Security

<TestClass()> _
Public Class TestBuilderIdentityTest

    Private _testBuilderIdentity As TestBuilderIdentity
    Private _clientName As String
    Private _userId As Integer
    Private _userName As String
    Private _type As String

    <TestInitialize()> _
    Public Sub MyTestInitialize()
        _userId = 2
        _userName = "userName"
        _type = "type"
        _testBuilderIdentity = New TestBuilderIdentity(_userId, _userName, _type)
    End Sub

    '''<summary>
    '''A test for IsAuthenticated()
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderIdentity_IsAuthenticatedTest()
        Assert.IsTrue(_testBuilderIdentity.IsAuthenticated, "Cito.TestBuilder.Security.TestBuilderIdentity.IsAuthenticated was not set correctly.")
        Assert.IsFalse((New TestBuilderIdentity).IsAuthenticated, "Cito.TestBuilder.Security.TestBuilderIdentity.IsAuthenticated was not set correctly.")
    End Sub

    '''<summary>
    '''A test for Name()
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderIdentity_NameTest()
        Dim val As String = "Haha"
        _testBuilderIdentity.UserName = val
        Assert.AreEqual(Of String)(val, _testBuilderIdentity.Name, "Cito.TestBuilder.Security.TestBuilderIdentity.Name was not set correctly.")
    End Sub

    '''<summary>
    '''A test for New()
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderIdentity_ConstructorTest()
        Dim target As New TestBuilderIdentity()
    End Sub

    '''<summary>
    '''A test for New(ByVal Integer, ByVal String, ByVal Integer, ByVal String, ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderIdentity_ConstructorTest1()
        Assert.AreEqual(Of Integer)(_userId, _testBuilderIdentity.UserId, "UserId is not the same.")
        Assert.AreEqual(Of String)(_userName, _testBuilderIdentity.UserName, "UserName is not the same.")
        Assert.AreEqual(Of String)(_type, _testBuilderIdentity.AuthenticationType, "Type is not the same.")
    End Sub

    '''<summary>
    '''A test for New(ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderIdentity_ConstructorTest2()
        Dim type As String = "type"
        Dim target As TestBuilderIdentity = New TestBuilderIdentity(type)

        Assert.AreEqual(Of String)(type, target.AuthenticationType, "Type is not the same.")
    End Sub

    '''<summary>
    '''A test for UserId()
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderIdentity_UserIdTest()
        Dim val As Integer = 100
        _testBuilderIdentity.UserId = val

        Assert.AreEqual(val, _testBuilderIdentity.UserId, "Cito.TestBuilder.Security.TestBuilderIdentity.UserId was not set correctly.")
    End Sub

    '''<summary>
    '''A test for UserName()
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderIdentity_UserNameTest()
        Dim val As String = "Martijn"
        _testBuilderIdentity.UserName = val

        Assert.AreEqual(val, _testBuilderIdentity.UserName, "Cito.TestBuilder.Security.TestBuilderIdentity.UserName was not set correctly.")
    End Sub

End Class
