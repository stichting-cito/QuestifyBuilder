
Imports Questify.Builder.Logic.ResourceManager

<TestClass>
Public Class ResourceVersionConverterTest

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertVersionTest()
        'Arrange
        Const v1 As String = "2.81"

        'Act
        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)

        'Assert
        Assert.AreEqual(20081, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertEmptyVersionTest()
        'Arrange
        Const v1 As String = ""
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)
        
        'Assert
        Assert.AreEqual(1, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertZeroVersionTest()
        'Arrange
        Const v1 As String = "0"
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)
        
        'Assert
        Assert.AreEqual(1, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertHighVersionTest()
        'Arrange
        Const v1 As String = "36"
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)
        
        'Assert
        Assert.AreEqual(36, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertOldVersionTest()
        'Arrange
        Const v1 As String = "2"
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)
        
        'Assert
        Assert.AreEqual(2, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackVersionTest()
        'Arrange
        Const v1 As Integer = 20081
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)
        
        'Assert
        Assert.AreEqual("2.81", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackHighVersionTest()
        'Arrange
        Const v1 As Integer = 2081
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)
        
        'Assert
        Assert.AreEqual("0.2081", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackEdgeVersionTest()
        'Arrange
        Const v1 As Integer = 9999
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)
        
        'Assert
        Assert.AreEqual("0.9999", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackEdge2VersionTest()
        'Arrange
        Const v1 As Integer = 10000
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)
        
        'Assert
        Assert.AreEqual("1.0", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackEmptyVersionTest()
        'Arrange
        Const v1 As Integer = 0
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)
        
        'Assert
        Assert.AreEqual("0.1", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackOldVersionTest()
        'Arrange
        Const v1 As Integer = 4
        
        'Act
        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)
        
        'Assert
        Assert.AreEqual("0.4", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertFromStringVersionTest()
        'Arrange
        Const v1 As string = "788"
        Const v2 As string = "1.2"
        
        'Act
        Dim cv1 As Version = ResourceVersionConverter.FromString(v1)
        Dim cv2 As Version = ResourceVersionConverter.FromString(v2)
        
        'Assert
        Assert.AreEqual(0, cv1.Major)
        Assert.AreEqual(788, cv1.Minor)
        Assert.AreEqual(1, cv2.Major)
        Assert.AreEqual(2, cv2.Minor)
    End Sub
End Class

