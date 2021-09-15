
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
        'http://msdn.microsoft.com/en-us/library/aa347875.aspx
        'Enum value '200175' is invalid for type TestBuilderPermissionAccess and cannot be serialized. 
        'Ensure that the necessary enum values are present and are marked with 
        'EnumMemberAttribute attribute if the type has DataContractAttribute attribute.
        
        'Arrange
        Dim val As TestBuilderPermissionAccess = DirectCast(200175, TestBuilderPermissionAccess)
        Dim dataSerializer As New DataContractSerializer(GetType(TestBuilderPermissionAccess))
        Dim m As New MemoryStream
       
        'Act
        dataSerializer.WriteObject(m, val) 'Will this cause an exception?
       
        'Assert
        m.Close()
        Assert.IsTrue(True) 'The problem was an exception
    End Sub

    <TestMethod> <TestCategory("WCF")>
    Public Sub TestBuilderPermissionAccess_AnyTaskIssue()
        'Enum value '200175' is invalid for type TestBuilderPermissionAccess and cannot be serialized. 
        'Ensure that the necessary enum values are present and are marked with 
        'EnumMemberAttribute attribute if the type has DataContractAttribute attribute.
      
        'Arrange
        Dim val As TestBuilderPermissionAccess = TestBuilderPermissionAccess.FullAccess
        Dim dataSerializer As New DataContractSerializer(GetType(TestBuilderPermissionAccess))
        Dim m As New MemoryStream
       
        'Act
        dataSerializer.WriteObject(m, val) 'Will this cause an exception?
       
        'Assert
        m.Close()
        Assert.IsTrue(True) 'The problem was an exception
    End Sub

    <TestMethod> <TestCategory("WCF")>
    Public Sub TestBuilderPermissionAccess_3567Issue()
        'http://msdn.microsoft.com/en-us/library/aa347875.aspx
        'Enum value '200175' is invalid for type TestBuilderPermissionAccess and cannot be serialized. 
        'Ensure that the necessary enum values are present and are marked with 
        'EnumMemberAttribute attribute if the type has DataContractAttribute attribute.
       
        'Arrange
        Dim val As TestBuilderPermissionAccess = DirectCast(3567, TestBuilderPermissionAccess)
        Dim dataSerializer As New DataContractSerializer(GetType(TestBuilderPermissionAccess))
        Dim m As New MemoryStream
       
        'Act
        dataSerializer.WriteObject(m, val) 'Will this cause an exception?
        
        'Assert
        m.Close()
        Assert.IsTrue(True) 'The problem was an exception
    End Sub

    <TestMethod> <TestCategory("WCF")>
    Public Sub TestEnum_Issue()
        'http://msdn.microsoft.com/en-us/library/aa347875.aspx
        'Enum value '200175' is invalid for type TestBuilderPermissionAccess and cannot be serialized. 
        'Ensure that the necessary enum values are present and are marked with 
        'EnumMemberAttribute attribute if the type has DataContractAttribute attribute.
       
        'Arrange
        Dim val As TestEnum = DirectCast(5, TestEnum)
        Dim dataSerializer As New DataContractSerializer(GetType(TestEnum))
        Dim m As New MemoryStream
     
        'Act
        dataSerializer.WriteObject(m, val) 'Will this cause an exception?
      
        'Assert
        m.Close()
        Assert.IsTrue(True) 'The problem was an exception
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