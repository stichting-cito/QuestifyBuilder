
Imports System.Xml
Imports System.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass>
Public Class ParagraphHtmlSplitterTest

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Public Sub GetParagraph()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>dit is een zin.dit is een zin.</p></body>.ToString())
        Dim htmlSplicer As New ParagraphHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/text()"), 30)
        
        'Act
        Dim result = htmlSplicer.Split()
        
        'Assert
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(<span>dit is een zin.dit is een zin.</span>.ToString(), result(0).OuterXml)
    End Sub

End Class
