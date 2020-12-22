
Option Infer On

Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

<TestClass()>
Public Class Html_RefsC1_2_CitoTests
    Inherits baseHtmlConverterTest

    <TestMethod(), TestCategory("UILogic")>
    Public Sub ConvertFromC1NodesToCitoNodes()
        Dim C1Format = <p>this is a reference <span id="ref8d8d8f6c-9391-45bc-865b-366b8f1c9485"
                                                  contenteditable="false"
                                                  cito_type="reference"
                                                  cito_reftype="Element"
                                                  cito_description="Element 1"
                                                  cito_value="1"
                                                  xmlns="http://www.w3.org/1999/xhtml">__1__</span></p>.ToString()

        Dim converter As New HtmlConverter_C1RefToCitoRef

        Dim result = converter.ConvertHtml(C1Format)

        Assert.IsFalse(ContainsRootElement(result))
        Assert.IsTrue(ContainsCitoRefAttributtes(result))
        Assert.IsTrue(ContainsXmlNSCito(result))
        Assert.IsFalse(ContainsC1RefAttributtes(result))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub ConvertFromC1NodesToCitoNodes_MultipleNodes()
        Dim C1Format = <p>this is a reference <span id="ref8d8d8f6c-9391-45bc-865b-366b8f1c9485"
                                                  contenteditable="false"
                                                  cito_type="reference"
                                                  cito_reftype="Element"
                                                  cito_description="Element 1"
                                                  cito_value="1"
                                                  xmlns="http://www.w3.org/1999/xhtml">__1__</span></p>.ToString()

        Dim converter As New HtmlConverter_C1RefToCitoRef

        Dim result = converter.ConvertHtml("<p>...</p>" + C1Format)

        Assert.IsFalse(ContainsRootElement(result))
        Assert.IsTrue(ContainsCitoRefAttributtes(result))
        Assert.IsTrue(ContainsXmlNSCito(result))
        Assert.IsFalse(ContainsC1RefAttributtes(result))
    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(9787)>
    Public Sub ConvertUninterestingSpan_resultShouldBeUnalterd()
        Dim span = <p>this is a reference <span id="id1" xmlns="http://www.w3.org/1999/xhtml">abc</span><span id="id1" xmlns="http://www.w3.org/1999/xhtml">def</span></p>.ToString()
        Dim converter As New HtmlConverter_C1RefToCitoRef()

        Dim result = converter.ConvertHtml(span)

        Assert.AreEqual(span.Length, result.Length)
    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(9787)>
    Public Sub ConvertUninterestingSpanSeperatedBySpaces_resultShouldBeUnalterd()
        Dim span = "<p>this is a reference <span id=""id1"" xmlns=""http://www.w3.org/1999/xhtml"">abc</span> <span id=""id1"" xmlns=""http://www.w3.org/1999/xhtml"">def</span></p>"
        Dim converter As New HtmlConverter_C1RefToCitoRef()

        Dim result = converter.ConvertHtml(span)

        Assert.AreEqual(span.Length, result.Length)
    End Sub

End Class
