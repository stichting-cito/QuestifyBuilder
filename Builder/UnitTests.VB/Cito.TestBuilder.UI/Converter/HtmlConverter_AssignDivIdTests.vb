
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

<TestClass()>
Public Class HtmlConverter_AssignDivIdTests
    Inherits baseHtmlConverterTest

    <TestMethod(), TestCategory("UILogic"), WorkItem(9382)>
    Public Sub AddIdsToDivsTest()
        Dim CitoFormat = <div><div xmlns="http://www.w3.org/1999/xhtml">Fred heeft 13 kippen. </div>
                             <div xmlns="http://www.w3.org/1999/xhtml">Hij koopt nog 8 kippen. </div>
                             <div xmlns="http://www.w3.org/1999/xhtml">Hoeveel kippen heeft hij dan in totaal? </div>
                         </div>.ToString()
        Dim converter As New HtmlConverter_AssignDivId()

        Dim result = converter.ConvertHtml(CitoFormat)

        Assert.AreEqual(3, converter.IdCounter)
        Assert.IsTrue(result.Contains("id=""div0"""))
        Assert.IsTrue(result.Contains("id=""div1"""))
        Assert.IsTrue(result.Contains("id=""div2"""))
    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(9382)>
    Public Sub DoNotAddIdsToDivsTest()
        Dim CitoFormat = <div><div xmlns="http://www.w3.org/1999/xhtml" id="a">Fred heeft 13 kippen. </div>
                             <div xmlns="http://www.w3.org/1999/xhtml" id="b">Hij koopt nog 8 kippen. </div>
                             <div xmlns="http://www.w3.org/1999/xhtml" id="c">Hoeveel kippen heeft hij dan in totaal? </div>
                         </div>.ToString()
        Dim converter As New HtmlConverter_AssignDivId()

        Dim result = converter.ConvertHtml(CitoFormat)

        Assert.AreEqual(0, converter.IdCounter)
    End Sub


    <TestMethod(), TestCategory("UILogic"), WorkItem(9382)>
    Public Sub AddIdsToDivsTest_UseFactory()
        Dim CitoFormat = <div><div xmlns="http://www.w3.org/1999/xhtml">Fred heeft 13 kippen. </div>
                             <div xmlns="http://www.w3.org/1999/xhtml">Hij koopt nog 8 kippen. </div>
                             <div xmlns="http://www.w3.org/1999/xhtml">Hoeveel kippen heeft hij dan in totaal? </div>
                         </div>.ToString()
        Dim converter As IHtmlConverter = New C1HtmlConverter().FromCitoFormatOldItem()

        Dim result = converter.ConvertHtml(CitoFormat)

        Assert.IsTrue(result.Contains("id=""div0"""))
        Assert.IsTrue(result.Contains("id=""div1"""))
        Assert.IsTrue(result.Contains("id=""div2"""))
    End Sub

End Class
