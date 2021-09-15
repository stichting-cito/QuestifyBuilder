
Imports Questify.Builder.Logic.HtmlHelpers
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports System.Xml
Imports System.Linq
Imports Questify.Builder.UI

''' <summary>
''' Extracts information from HTML tests.
''' </summary>
<TestClass()>
Public Class HtmlResourceExtractorTest

    Private inline As XElement = <p>
                                     <cito:InlineElement id="4c6a8c28-ca8b-4092-940a-bc0ab67fd1e5" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                         <cito:parameters>
                                             <cito:parameterSet id="entireItem">
                                                 <cito:resourceparameter name="source">Charlie Chaplin.gif</cito:resourceparameter>
                                                 <cito:integerparameter name="width"/>
                                                 <cito:integerparameter name="height"/>
                                                 <cito:booleanparameter name="showPopup">False</cito:booleanparameter>
                                                 <cito:resourceparameter name="largeImage"/>
                                                 <cito:integerparameter name="largeWidth"/>
                                                 <cito:integerparameter name="largeHeight"/>
                                             </cito:parameterSet>
                                         </cito:parameters>
                                     </cito:InlineElement>
                                 </p>



    Private html As XElement = <p>
                                   <p id="c1-id-6" xmlns="http://www.w3.org/1999/xhtml">Een stukje tekst in de eerste paragraaf.</p>
                                   <p id="c1-id-7" xmlns="http://www.w3.org/1999/xhtml">Een stukje tekst voor het logo van cito <img id="oldImage" src="resource://package/oldImage.gif" alt=""/>En een stukje tekst na het logo.</p>
                                   <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml"> </p>
                                   <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tot slot nog een inline ding: </p>
                                   <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml"> <img id="5ab293ce-585b-491d-a569-6347aba3fb02" src="resource://package:1/placeholderimage_320_240_video.mp4.jpg" width="320" height="240" isinlineelement="true" alt=""/></p>
                               </p>

    Public Function ToXmlElement(xml As String) As XmlElement
        Dim frag As XmlDocumentFragment = New XmlDocument().CreateDocumentFragment()

        frag.InnerXml = xml

        Return TryCast(frag.FirstChild, XmlElement)
    End Function

    <TestMethod(), TestCategory("UILogic")>
    Public Sub TestIfResourcesArePresent()
        'Arrange
        Dim namespaceManager As XmlNamespaceManager = XHtmlParameterExtensions.GetNamespaceManager()
        Dim inlineConverter As New HtmlInlineConverter(Nothing, namespaceManager, Nothing, Nothing)
        Dim xmlElement As Xml.XmlElement = ToXmlElement(inline.ToString)

        Dim inlineElementList As List(Of InlineElement) = inlineConverter.GetInlineElementsFromXmlElement(xmlElement)
        Dim resources As List(Of String) = HtmlResourceExtractor.GetAllResourcesInHtml(inlineElementList, html.ToString()).ToList()
        
        'Act
        Dim b1 As Boolean = resources.Contains("Charlie Chaplin.gif")
        Dim b2 As Boolean = resources.Contains("InlineImageLayoutTemplate")

        'Assert
        Assert.IsTrue(b1)
        Assert.IsTrue(b2)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub TestIfResourceIsNOTPresent()
        'Arrange
        Dim namespaceManager As XmlNamespaceManager = XHtmlParameterExtensions.GetNamespaceManager()
        Dim inlineConverter As New HtmlInlineConverter(Nothing, namespaceManager, Nothing, Nothing)

        Dim xmlElement As Xml.XmlElement = ToXmlElement(inline.ToString)
        Dim inlineElementList As List(Of InlineElement) = inlineConverter.GetInlineElementsFromXmlElement(xmlElement)
        Dim resources As List(Of String) = HtmlResourceExtractor.GetAllResourcesInHtml(inlineElementList, html.ToString()).ToList()
      
        'Act
        Dim b1 As Boolean = resources.Contains("ThisResourceIsNotPresent")
       
        'Assert
        Assert.IsFalse(b1)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub TestThatCertainItemsAreExtractedFromHtml()
        'Arrange

        'Act
        Dim lstInlineElements As New List(Of String)(HtmlResourceExtractor.GetImageResources(html.ToString()))
        
        'Assert
        Assert.AreEqual(1, lstInlineElements.Count)

        Assert.AreEqual("oldImage.gif", lstInlineElements(0))
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub TestThatCertainItemsAreExtractedFromXml()
        'Arrange
        Dim namespaceManager As XmlNamespaceManager = XHtmlParameterExtensions.GetNamespaceManager()
        Dim inlineConverter As New HtmlInlineConverter(Nothing, namespaceManager, Nothing, Nothing)

        Dim xmlElement As Xml.XmlElement = ToXmlElement(inline.ToString)
        Dim inlineElementList As List(Of InlineElement) = inlineConverter.GetInlineElementsFromXmlElement(xmlElement)
        Dim resources As List(Of String) = HtmlResourceExtractor.GetAllResourcesInHtml(inlineElementList, html.ToString()).ToList
      
        'Act
        Dim lstInlineElements As New List(Of String)

        For Each inline As InlineElement In inlineElementList
            lstInlineElements.AddRange(inline.GetResourcesFromResourceParameter())
        Next
       
        'Assert
        Assert.AreEqual(1, lstInlineElements.Count)
        Assert.AreEqual(2, resources.Count - lstInlineElements.Count)
        Assert.IsTrue(resources.Contains("InlineImageLayoutTemplate"))
        Assert.IsTrue(lstInlineElements.Contains("Charlie Chaplin.gif"))
        Assert.IsTrue(resources.Contains("oldImage.gif"))
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub LoadWithMiltipleRoots_NoExceptionThrown()
        'Arrange
        Dim html As String = "<p>row 1</p>" +
                             "<p>row 2</p>"
        Dim inlineElementList As New List(Of InlineElement)

        'The html loaded is only what is present in the body, therefore it lacks a root
        'This should NOT cause any problems.
      
        'Act
        HtmlResourceExtractor.GetAllResourcesInHtml(inlineElementList, html)
        
        'Assert
        'Should not throw an exception
    End Sub

End Class
