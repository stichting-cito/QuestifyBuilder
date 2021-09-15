
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_PlainText_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        'Arrange
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                               </PlainTextParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired()
        'Arrange
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>
                                   text
                               </PlainTextParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired_noValue()
        'Arrange
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>

                               </PlainTextParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub Regex_noValue()
        'Arrange
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="validationRegEx">\w+</designersetting>
                               </PlainTextParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Regex_WrongValue()
        'Arrange
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea"><designersetting key="validationRegEx">\s+</designersetting>123</PlainTextParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Regex_OKValue()
        'Arrange
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="validationRegEx">\w+</designersetting>abc
                               </PlainTextParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    Private Function GetTextParam(x As XElement) As PlainTextParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of PlainTextParameter)(x)
        Return ret
    End Function

End Class