
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class AspectReferenceSerializationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedResult()
        Dim aspect As New AspectReference() With {.MaxScore = 3, .Description = ""}
        Dim result As XElement

        result = MyBase.DoSerialize(Of AspectReference)(aspect)

        Assert.IsNotNull(result, "Serialized result should not be null.")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestDeSerializedResultWithSafeXml()
        Dim input As XElement = <AspectReference xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" maxScore="3">&lt;p xmlns="http://www.w3.org/1999/xhtml"&gt;Test serialization&lt;/p&gt;</AspectReference>
        Dim result As AspectReference

        result = MyBase.Deserialize(Of AspectReference)(input)

        Assert.IsNotNull(result, "Deserialized result should not be null.")
        Assert.IsFalse(String.IsNullOrWhiteSpace(result.Description), "Description should not be empty.")
        Assert.AreEqual("<p xmlns=""http://www.w3.org/1999/xhtml"">Test serialization</p>", result.Description)
        Assert.AreEqual(3, result.MaxScore, "MaxScore should be 3.")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestDeSerializedResultWithUnsafeXml()
        Dim input As XElement = <AspectReference xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" maxScore="3"><p xmlns="http://www.w3.org/1999/xhtml">Test serialization</p></AspectReference>
        Dim result As AspectReference

        result = MyBase.Deserialize(Of AspectReference)(input)

        Assert.IsNotNull(result, "Deserialized result should not be null.")
        Assert.IsNull(result.Description, "Description should be null.")
        Assert.AreEqual(3, result.MaxScore, "MaxScore should be 3.")
    End Sub

End Class
