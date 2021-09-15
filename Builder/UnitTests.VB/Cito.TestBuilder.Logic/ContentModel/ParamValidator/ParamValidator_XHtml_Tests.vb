
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_XHtml_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        'Arrange
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                               </XHtmlParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_NoValue_NotValid()
        'Arrange
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                               </XHtmlParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_NoActualValue_NotValid()
        'Arrange
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <p></p>
                               </XHtmlParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_NoTextButImg_Valid()
        'Arrange
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <img src="picture.jpg"/>
                               </XHtmlParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_Text_Valid()
        'Arrange
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <p>some text</p>
                               </XHtmlParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_Text2_Valid()
        'Arrange
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   some text
                               </XHtmlParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_Hidden_NoValue_Valid()
        'Arrange
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <designersetting key="visible">false</designersetting>
                               </XHtmlParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    Private Function GetTextParam(x As XElement) As XHtmlParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of XHtmlParameter)(x)
        Return ret
    End Function

End Class