
Imports Questify.Builder.Logic.ResourceManager

<TestClass>
Public Class ResourceVersionConverterTest

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertVersionTest()
        Const v1 As String = "2.81"

        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)

        Assert.AreEqual(20081, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertEmptyVersionTest()
        Const v1 As String = ""

        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)

        Assert.AreEqual(1, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertZeroVersionTest()
        Const v1 As String = "0"

        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)

        Assert.AreEqual(1, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertHighVersionTest()
        Const v1 As String = "36"

        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)

        Assert.AreEqual(36, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertOldVersionTest()
        Const v1 As String = "2"

        Dim cv1 = ResourceVersionConverter.ConvertVersion(v1)

        Assert.AreEqual(2, cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackVersionTest()
        Const v1 As Integer = 20081

        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)

        Assert.AreEqual("2.81", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackHighVersionTest()
        Const v1 As Integer = 2081

        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)

        Assert.AreEqual("0.2081", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackEdgeVersionTest()
        Const v1 As Integer = 9999

        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)

        Assert.AreEqual("0.9999", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackEdge2VersionTest()
        Const v1 As Integer = 10000

        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)

        Assert.AreEqual("1.0", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackEmptyVersionTest()
        Const v1 As Integer = 0

        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)

        Assert.AreEqual("0.1", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertBackOldVersionTest()
        Const v1 As Integer = 4

        Dim cv1 = ResourceVersionConverter.ConvertBackVersion(v1)

        Assert.AreEqual("0.4", cv1)
    End Sub

    <TestMethod, TestCategory("Helpers")> _
    Public Sub ConvertFromStringVersionTest()
        Const v1 As string = "788"
        Const v2 As string = "1.2"

        Dim cv1 As Version = ResourceVersionConverter.FromString(v1)
        Dim cv2 As Version = ResourceVersionConverter.FromString(v2)

        Assert.AreEqual(0, cv1.Major)
        Assert.AreEqual(788, cv1.Minor)
        Assert.AreEqual(1, cv2.Major)
        Assert.AreEqual(2, cv2.Minor)
    End Sub
End Class

