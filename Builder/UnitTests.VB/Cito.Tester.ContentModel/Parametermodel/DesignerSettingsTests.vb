
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass>
Public Class DesignerSettingsTests
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeDefaultDesignerSetting()
        'Arrange
        Dim d As New DesignerSetting
        Dim result As XElement
       
        'Act
        result = MyBase.DoSerialize(Of DesignerSetting)(d)
        
        'Assert
        Assert.AreEqual("<designersetting xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" />", result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeDefaultDesignerSetting()
        'Arrange
        Dim x = <designersetting/>
      
        'Act
        Dim result = MyBase.Deserialize(Of DesignerSetting)(x)
      
        'Assert
        Assert.IsNotNull(result, "A result was expected")
        Assert.IsNull(result.Key, "A Key was not expected at this time")
        Assert.IsNull(result.Ref, "No REF expected")
        Assert.IsNull(result.Value, "value was not expected")
        Assert.IsNotNull(result.ListValue, "The listValue althroug 0 elements should have existe.d")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeDesignersettingWithRef()
        'Arrange
        Dim d As New DesignerSetting With {.Ref = "otherParam"}
        Dim result As XElement
    
        'Act
        result = MyBase.DoSerialize(Of DesignerSetting)(d)
       
        'Assert
        Assert.AreEqual("ref=""otherParam""", result.Attribute("ref").ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeDesignersettingWithRef()
        'Arrange
        Dim x = <designersetting ref="xyz"/>
        Dim result As DesignerSetting
      
        'Act
        result = MyBase.Deserialize(Of DesignerSetting)(x)
       
        'Assert
        Assert.AreEqual("xyz", result.Ref)
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializeDesignersettingWithValue()
        'Arrange
        Dim d As New DesignerSetting With {.Value = "123"}
        Dim result As XElement
      
        'Act
        result = MyBase.DoSerialize(Of DesignerSetting)(d)
      
        'Assert
        Assert.AreEqual("123", result.Value)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeSerializeDesignersettingWithValue()
        'Arrange
        Dim x = <designersetting>abc</designersetting>
        Dim result As DesignerSetting
       
        'Act
        result = MyBase.Deserialize(Of DesignerSetting)(x)
       
        'Assert
        Assert.AreEqual("abc", result.Value)
    End Sub

End Class
