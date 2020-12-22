
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass>
Public Class AttributeReferencesTests
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeDefaultAttributeReference()
        Dim a As New AttributeReference
        Dim result As XElement

        result = MyBase.DoSerialize(Of AttributeReference)(a)

        Assert.AreEqual("<attributereference xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" whattocopy=""Value"" />", result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeDefaultAttributeReference()
        Dim x = <attributereference/>

        Dim result = MyBase.Deserialize(Of AttributeReference)(x)

        Assert.IsNotNull(result, "A result was expected")
        Assert.IsNull(result.Name, "A Name was not expected at this time")
        Assert.IsNull(result.Value, "value was not expected")
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeAttributeReferenceWithNameAndValue()
        Dim attr As New AttributeReference With {.Name = "test", .Value = "123"}
        Dim result As XElement

        result = MyBase.DoSerialize(Of AttributeReference)(attr)

        Assert.AreEqual(4, result.Attributes.Count())
        Assert.AreEqual("test", result.Attributes.Single(Function(a) a.Name = "name").Value)
        Assert.AreEqual("Value", result.Attribute("whattocopy").Value)
        Assert.AreEqual("123", result.Value)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeAttributeReferenceWithNameAndValue()
        Dim x = <attributereference name="test">123</attributereference>
        Dim result As AttributeReference

        result = MyBase.Deserialize(Of AttributeReference)(x)

        Assert.AreEqual("test", result.Name)
        Assert.AreEqual("123", result.Value)
    End Sub

End Class
