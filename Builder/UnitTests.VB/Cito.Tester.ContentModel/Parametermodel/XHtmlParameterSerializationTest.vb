
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class XHtmlParameterSerializationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedResult()
        'Arrange
        Dim p = New XhtmlResourceParameter() With {.Name = "img1"}
        p.DesignerSettings.Add(New DesignerSetting() With {.Key = "k1", .Value = "v1"})
        p.DesignerSettings.Add(New DesignerSetting() With {.Key = "k2", .Value = "v2"})

        p.Value = ""
        Dim result As XElement
       
        'Act
        result = MyBase.DoSerialize(Of XhtmlResourceParameter)(p)
      
        'Assert
        Assert.IsNotNull(result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestDeSerializedResult()
        'Arrange
        Dim result As XHtmlParameter
        Dim input As XElement = <XHtmlParameter name="text">
                                    <designersetting key="label">aaa</designersetting>
                                    <designersetting key="description"></designersetting>
                                    <designersetting key="required">false</designersetting>
                                    <designersetting key="validationRegEx"></designersetting>
                                    <designersetting key="validationRegExMessage"></designersetting>
                                    <designersetting key="group"></designersetting>
                                    <designersetting key="defaultvalue"></designersetting>
                                    <designersetting key="visible"></designersetting>
                                    <designersetting key="sourcecontentparameter"></designersetting>
                                </XHtmlParameter>
        
        'Act
        result = MyBase.Deserialize(Of XHtmlParameter)(input)
        
        'Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(0, result.DesignerSettings.Count) 'DesignerSettings should not be serialized.
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestDeSerializedWithContentResult()
        'Arrange
        Dim result As XHtmlParameter
        Dim input As XElement = <XHtmlParameter name="text">
                                    <designersetting key="label">aaa</designersetting>
                                    <designersetting key="description"></designersetting>
                                    <designersetting key="required">false</designersetting>
                                    <designersetting key="validationRegEx"></designersetting>
                                    <designersetting key="validationRegExMessage"></designersetting>
                                    <designersetting key="group"></designersetting>
                                    <designersetting key="defaultvalue"></designersetting>
                                    <designersetting key="visible"></designersetting>
                                    <designersetting key="sourcecontentparameter"></designersetting>
                                    CONTENT
                                </XHtmlParameter>
        
        'Act
        result = MyBase.Deserialize(Of XHtmlParameter)(input)
        
        'Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(0, result.DesignerSettings.Count) 'DesignerSettings should not be serialized.
    End Sub

End Class
