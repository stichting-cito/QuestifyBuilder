
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class AspectReferenceSerializationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub TestSerializedResult()
        'Arrange
        Dim aspect As New AspectReference() With {.MaxScore = 3, .Description = ""}
        Dim result As XElement

        'Act
        result = MyBase.DoSerialize(Of AspectReference)(aspect)

        'Assert
        Assert.IsNotNull(result, "Serialized result should not be null.")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub TestDeSerializedResultWithSafeXml()
        'Arrange
        'Input XElement with safe XML
        Dim input As XElement = <AspectReference xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" maxScore="3">&lt;p xmlns="http://www.w3.org/1999/xhtml"&gt;Test serialization&lt;/p&gt;</AspectReference>
        Dim result As AspectReference

        'Act
        result = MyBase.Deserialize(Of AspectReference)(input)

        'Assert
        Assert.IsNotNull(result, "Deserialized result should not be null.")
        'Deserializing safe XML should result in a AspectReference with a description.
        Assert.IsFalse(String.IsNullOrWhiteSpace(result.Description), "Description should not be empty.")
        'Check if the description is correct.
        Assert.AreEqual("<p xmlns=""http://www.w3.org/1999/xhtml"">Test serialization</p>", result.Description)
        Assert.AreEqual(3, result.MaxScore, "MaxScore should be 3.")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub TestDeSerializedResultWithUnsafeXml()
        'Arrange
        'Input XElement with unsafe XML
        Dim input As XElement = <AspectReference xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" maxScore="3"><p xmlns="http://www.w3.org/1999/xhtml">Test serialization</p></AspectReference>
        Dim result As AspectReference

        'Act
        result = MyBase.Deserialize(Of AspectReference)(input)

        'Assert
        Assert.IsNotNull(result, "Deserialized result should not be null.")
        'Deserializing safe xml should result in a AspectReference with a null description.
        Assert.IsNull(result.Description, "Description should be null.")
        'The MaxScore should still be deserialized correctly.
        Assert.AreEqual(3, result.MaxScore, "MaxScore should be 3.")
    End Sub

End Class
