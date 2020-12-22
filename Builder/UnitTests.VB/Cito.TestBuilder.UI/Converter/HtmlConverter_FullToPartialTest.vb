
Imports System.Xml.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

<TestClass()>
Public Class HtmlConverter_FullToPartialTest
    Inherits baseHtmlConverterTest

    <TestMethod(), TestCategory("UILogic")>
    Public Sub ExtractSimpleStringFromHtml()
        Dim converter As New HtmlConverter_FullToPartial()

        Dim tst As String = converter.ConvertHtml(<body xmlns="http://www.w3.org/1999/xhtml">data</body>.ToString)

        Assert.AreEqual("data", tst)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub ExtractSimpleStringWithLinefeedFromHtml()
        Dim converter As New HtmlConverter_FullToPartial()

        Dim tst As String = converter.ConvertHtml("<body xmlns=""http://www.w3.org/1999/xhtml"">" + "ln1" + Environment.NewLine + "ln2" + "</body>")

        Assert.AreEqual("ln1" + Environment.NewLine + "ln2", tst)
    End Sub

    Private simpleBody As XElement = <body xmlns="http://www.w3.org/1999/xhtml">data</body>

    <TestMethod(), TestCategory("UILogic"), WorkItem(9787)>
    Public Sub PreserveWhitespaceWhenExtracting()
        Dim converter As New HtmlConverter_FullToPartial()

        Dim tst As String = converter.ConvertHtml("<body xmlns=""http://www.w3.org/1999/xhtml""><span>a</span> <span>b</span></body>")

        Assert.IsTrue(tst.Contains("</span> <span"))
    End Sub

End Class
