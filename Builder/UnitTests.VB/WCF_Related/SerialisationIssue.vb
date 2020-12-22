
Imports System.IO
Imports Questify.Builder.Security
Imports System.Runtime.Serialization
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass()> Public Class SerialisationIssue

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod> <TestCategory("WCF")>
    Public Sub TestBuilderPermissionAccess_200175Issue()

        Dim val As TestBuilderPermissionAccess = DirectCast(200175, TestBuilderPermissionAccess)
        Dim dataSerializer As New DataContractSerializer(GetType(TestBuilderPermissionAccess))
        Dim m As New MemoryStream

        dataSerializer.WriteObject(m, val)

        m.Close()
        Assert.IsTrue(True)
    End Sub

    <TestMethod> <TestCategory("WCF")>
    Public Sub TestBuilderPermissionAccess_AnyTaskIssue()

        Dim val As TestBuilderPermissionAccess = TestBuilderPermissionAccess.FullAccess
        Dim dataSerializer As New DataContractSerializer(GetType(TestBuilderPermissionAccess))
        Dim m As New MemoryStream

        dataSerializer.WriteObject(m, val)

        m.Close()
        Assert.IsTrue(True)
    End Sub

    <TestMethod> <TestCategory("WCF")>
    Public Sub TestBuilderPermissionAccess_3567Issue()

        Dim val As TestBuilderPermissionAccess = DirectCast(3567, TestBuilderPermissionAccess)
        Dim dataSerializer As New DataContractSerializer(GetType(TestBuilderPermissionAccess))
        Dim m As New MemoryStream

        dataSerializer.WriteObject(m, val)

        m.Close()
        Assert.IsTrue(True)
    End Sub

    <TestMethod> <TestCategory("WCF")>
    Public Sub TestEnum_Issue()

        Dim val As TestEnum = DirectCast(5, TestEnum)
        Dim dataSerializer As New DataContractSerializer(GetType(TestEnum))
        Dim m As New MemoryStream

        dataSerializer.WriteObject(m, val)

        m.Close()
        Assert.IsTrue(True)
    End Sub

    <Flags>
    Public Enum TestEnum
        None = 0
        a = 1
        b = 2
        c = 4
        d = 8
    End Enum

End Class