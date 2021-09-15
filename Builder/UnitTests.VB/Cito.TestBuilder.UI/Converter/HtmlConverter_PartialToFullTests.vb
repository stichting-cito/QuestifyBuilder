
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

<TestClass()>
Public Class HtmlConverter_PartialToFullTests

    <TestMethod(), TestCategory("UILogic"), WorkItem(9787)>
    Public Sub PreserveWhitespaceWhenConverting()
        'Arrange
        Dim namespaceManager = New XmlNamespaceManager(New NameTable())
        namespaceManager.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        namespaceManager.AddNamespace("cito", "http://www.cito.nl/citotester")

        Dim converter As New HtmlConverter_PartialToFull(New Dictionary(Of String, String), Nothing, Nothing, namespaceManager)
        
        'Act
        Dim result = converter.ConvertHtml("<span>a</span> <span>b</span>")
        
        'Assert
        Assert.IsTrue(result.Contains("</span> <span"))
    End Sub

End Class
