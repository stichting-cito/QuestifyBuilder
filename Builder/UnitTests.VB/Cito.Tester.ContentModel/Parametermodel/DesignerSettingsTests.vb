
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass>
Public Class DesignerSettingsTests
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeDefaultDesignerSetting()
        Dim d As New DesignerSetting
        Dim result As XElement

        result = MyBase.DoSerialize(Of DesignerSetting)(d)

        Assert.AreEqual("<designersetting xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" />", result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeDefaultDesignerSetting()
        Dim x = <designersetting/>

        Dim result = MyBase.Deserialize(Of DesignerSetting)(x)

        Assert.IsNotNull(result, "A result was expected")
        Assert.IsNull(result.Key, "A Key was not expected at this time")
        Assert.IsNull(result.Ref, "No REF expected")
        Assert.IsNull(result.Value, "value was not expected")
        Assert.IsNotNull(result.ListValue, "The listValue althroug 0 elements should have existe.d")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeDesignersettingWithRef()
        Dim d As New DesignerSetting With {.Ref = "otherParam"}
        Dim result As XElement

        result = MyBase.DoSerialize(Of DesignerSetting)(d)

        Assert.AreEqual("ref=""otherParam""", result.Attribute("ref").ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeDesignersettingWithRef()
        Dim x = <designersetting ref="xyz"/>
        Dim result As DesignerSetting

        result = MyBase.Deserialize(Of DesignerSetting)(x)

        Assert.AreEqual("xyz", result.Ref)
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeDesignersettingWithValue()
        Dim d As New DesignerSetting With {.Value = "123"}
        Dim result As XElement

        result = MyBase.DoSerialize(Of DesignerSetting)(d)

        Assert.AreEqual("123", result.Value)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeDesignersettingWithValue()
        Dim x = <designersetting>abc</designersetting>
        Dim result As DesignerSetting

        result = MyBase.Deserialize(Of DesignerSetting)(x)

        Assert.AreEqual("abc", result.Value)
    End Sub

End Class
