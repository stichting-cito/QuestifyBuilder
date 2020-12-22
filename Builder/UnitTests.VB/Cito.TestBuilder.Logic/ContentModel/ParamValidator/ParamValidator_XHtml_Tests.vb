
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_XHtml_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                               </XHtmlParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_NoValue_NotValid()
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                               </XHtmlParameter>)

        Dim result = prm.IsValid()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_NoActualValue_NotValid()
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <p></p>
                               </XHtmlParameter>)

        Dim result = prm.IsValid()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_NoTextButImg_Valid()
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <img src="picture.jpg"/>
                               </XHtmlParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_Text_Valid()
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <p>some text</p>
                               </XHtmlParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_Text2_Valid()
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   some text
                               </XHtmlParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RequiredValue_Hidden_NoValue_Valid()
        Dim prm = GetTextParam(<XHtmlParameter name="a">
                                   <designersetting key="required">True</designersetting>
                                   <designersetting key="visible">false</designersetting>
                               </XHtmlParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    Private Function GetTextParam(x As XElement) As XHtmlParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of XHtmlParameter)(x)
        Return ret
    End Function

End Class