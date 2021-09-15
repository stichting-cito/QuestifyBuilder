
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

<TestClass()>
Public Class Html_RefsCito_2_C1CitoTests
    Inherits baseHtmlConverterTest

    <TestMethod(), TestCategory("UILogic")>
    Public Sub ConvertFromCitoNodesToC1Nodes()
        'Arrange
        Dim CitoFormat = <p>this is a reference <span id="ref8d8d8f6c-9391-45bc-865b-366b8f1c9485"
                                                    contenteditable="false"
                                                    cito:type="reference"
                                                    cito:reftype="Element"
                                                    cito:description="Element 1"
                                                    cito:value="1"
                                                    xmlns:cito="http://www.cito.nl/citotester"
                                                    xmlns="http://www.w3.org/1999/xhtml">__1__</span></p>.ToString()
        Dim converter As New HtmlConverter_CitoRefToC1Ref()

        'Act
        Dim result = converter.ConvertHtml(CitoFormat)

        'Assert
        Assert.IsFalse(ContainsRootElement(result))
        Assert.IsFalse(ContainsCitoRefAttributtes(result))
        Assert.IsTrue(ContainsC1RefAttributtes(result))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub ConvertFromCitoNodesToC1Nodes_MultipleNodes()
        'Arrange
        Dim CitoFormat = <p>this is a reference <span id="ref8d8d8f6c-9391-45bc-865b-366b8f1c9485"
                                                    contenteditable="false"
                                                    cito:type="reference"
                                                    cito:reftype="Element"
                                                    cito:description="Element 1"
                                                    cito:value="1"
                                                    xmlns:cito="http://www.cito.nl/citotester"
                                                    xmlns="http://www.w3.org/1999/xhtml">__1__</span></p>.ToString()
        Dim converter As New HtmlConverter_CitoRefToC1Ref()

        'Act
        Dim result = converter.ConvertHtml("<p>...</p>" + CitoFormat)

        'Assert
        Assert.IsFalse(ContainsRootElement(result))
        Assert.IsFalse(ContainsCitoRefAttributtes(result))
        Assert.IsTrue(ContainsC1RefAttributtes(result))
    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(9787)>
    Public Sub ConvertUninterestingSpan_resultShouldBeUnalterd()
        'Arrange
        Dim span = <p>this is a reference <span id="id1" xmlns="http://www.w3.org/1999/xhtml">abc</span><span id="id1" xmlns="http://www.w3.org/1999/xhtml">def</span></p>.ToString()
        Dim converter As New HtmlConverter_CitoRefToC1Ref()

        'Act
        Dim result = converter.ConvertHtml(span)

        'Assert
        Assert.AreEqual(span.Length, result.Length)
    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(9787)>
    Public Sub ConvertUninterestingSpanSeperatedBySpaces_resultShouldBeUnalterd()
        'Arrange
        Dim span = "<p>this is a reference <span id=""id1"" xmlns=""http://www.w3.org/1999/xhtml"">abc</span> <span id=""id1"" xmlns=""http://www.w3.org/1999/xhtml"">def</span></p>"
        Dim converter As New HtmlConverter_CitoRefToC1Ref()

        'Act
        Dim result = converter.ConvertHtml(span)

        'Assert
        Assert.AreEqual(span.Length, result.Length)
    End Sub

End Class
