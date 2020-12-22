
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel.ParamValidator

<TestClass()>
Public Class HtmlContentValidatorTests

    Private filledHtml As XElement = <p>tekst</p>
    Private emptyHtml As XElement = <p></p>

    <TestMethod(), TestCategory("UILogic")>
    Public Sub HtmlContainsValue()
        Dim validator As New HtmlContentValidator

        Dim result As Boolean = validator.HtmlContainsValue(filledHtml.ToString())

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub HtmlDoesNotContainsValue()
        Dim validator As New HtmlContentValidator

        Dim result As Boolean = validator.HtmlContainsValue(emptyHtml.ToString())

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub HtmlDoessNotContainValueWith_nbsp()
        Dim validator As New HtmlContentValidator

        Dim result As Boolean = validator.HtmlContainsValue("<p> </p>")

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub HtmlContainsValueWithImg()
        Dim validator As New HtmlContentValidator

        Dim result As Boolean = validator.HtmlContainsValue("<p id=""c1-id-9"" xmlns=""http://www.w3.org/1999/xhtml""> <img id=""25ee4f6b-6998-4c3c-b272-241b1ec21ab7"" src=""resource://package:1/Charlie%20Chaplin.gif"" alt="""" isinlineelement=""true"" /></p>")

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub HtmlContainsValueWithBRandText()
        Dim validator As New HtmlContentValidator

        Dim result As Boolean = validator.HtmlContainsValue("<p id=""c1-id-9"" xmlns=""http://www.w3.org/1999/xhtml""> <br /> test</p>")

        Assert.IsTrue(result)
    End Sub

End Class
