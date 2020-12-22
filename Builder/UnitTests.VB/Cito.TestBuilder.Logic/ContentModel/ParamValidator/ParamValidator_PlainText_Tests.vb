
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_PlainText_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                               </PlainTextParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired()
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>
                                   text
                               </PlainTextParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired_noValue()
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>

                               </PlainTextParameter>)

        Dim result = prm.IsValid()

        Assert.IsFalse(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub Regex_noValue()
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="validationRegEx">\w+</designersetting>
                               </PlainTextParameter>)

        Dim result = prm.IsValid()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Regex_WrongValue()
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea"><designersetting key="validationRegEx">\s+</designersetting>123</PlainTextParameter>)

        Dim result = prm.IsValid()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Regex_OKValue()
        Dim prm = GetTextParam(<PlainTextParameter name="itemQuestionArea">
                                   <designersetting key="validationRegEx">\w+</designersetting>abc
                               </PlainTextParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    Private Function GetTextParam(x As XElement) As PlainTextParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of PlainTextParameter)(x)
        Return ret
    End Function

End Class