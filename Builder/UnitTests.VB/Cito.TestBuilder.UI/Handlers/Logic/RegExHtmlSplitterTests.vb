
Imports System.Xml
Imports System.Linq
Imports System.Xml.Linq
Imports System.Text.RegularExpressions
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class RegExHtmlSplitterTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    <Description("Example selection over paragraphs")>
    Sub FlattenTests_withExample1()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(example1.ToString())
        Dim startNode = doc.SelectSingleNode("/body/p[@id='c1-id-9']/text()")
        Dim endNode = doc.SelectSingleNode("/body/p[@id='c1-id-10']/text()")

        Dim splicer As New RegExHtmlSplitter(New Regex("\w+"), startNode, 6, endNode, 11)
        
        'Act
        Dim result = splicer.FlattenNodes()
        
        'Assert
        Assert.AreEqual(4, result.Count) 'Textnode, parent P and textnode
        Assert.IsInstanceOfType(result(0), GetType(XmlText))
        Assert.AreEqual("p", result(1).LocalName)
        Assert.AreEqual("c1-id-9", result(1).Attributes("id").Value)
        Assert.AreEqual("p", result(2).LocalName)
        Assert.AreEqual("c1-id-10", result(2).Attributes("id").Value)
        Assert.IsInstanceOfType(result(3), GetType(XmlText))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    <Description("Example selection over paragraphs")>
    Sub GetStringMap_withExample1()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(example1.ToString())
        Dim startNode = doc.SelectSingleNode("/body/p[@id='c1-id-9']/text()")
        Dim endNode = doc.SelectSingleNode("/body/p[@id='c1-id-12']/text()")

        Dim splicer As New RegExHtmlSplitter(New Regex("\w+"), startNode, 0, endNode, 4)
        
        'Act
        Dim result = splicer.GetStringMap()
        
        'Assert
        Assert.AreEqual(4, result.Count) 'Textnode, parent P and textnode
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    <Description("start selection withing strong part")>
    Sub FlattenTests_withExample2()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(example2.ToString())
        Dim startNode = doc.SelectSingleNode("//strong[@id='c1-id-13']/text()")
        Dim endNode = doc.SelectSingleNode("/p[@id='c1-id-10']/text()")

        Dim splicer As New RegExHtmlSplitter(New Regex("\w+"), startNode, 3, endNode, 45)
        
        'Act
        Dim result = splicer.FlattenNodes()
        
        'Assert
        Assert.AreEqual(3, result.Count) 'Textnode, parent P and textnode
        Assert.IsInstanceOfType(result(0), GetType(XmlText))
        Assert.AreEqual("strong", result(1).LocalName)
        Assert.IsInstanceOfType(result(2), GetType(XmlText))
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    <Description("start selection withing strong part, that is not start.")>
    Sub FlattenTests_withExample3()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(example3.ToString())
        Dim startNode = doc.SelectSingleNode("//strong[@id='c1-id-13']/text()")
        Dim endNode = doc.SelectSingleNode("/p[@id='c1-id-10']/text()[2]")

        Dim splicer As New RegExHtmlSplitter(New Regex("\w+"), startNode, 3, endNode, 45)
        
        'Act
        Dim result = splicer.FlattenNodes()
        
        'Assert
        Assert.AreEqual(3, result.Count) 'Textnode, parent P and textnode
        Assert.IsInstanceOfType(result(0), GetType(XmlText))
        Assert.AreEqual("strong", result(1).LocalName)
        Assert.IsInstanceOfType(result(2), GetType(XmlText))
        Assert.IsTrue(result(2).InnerText.Contains("alinea. Er zijn velen die er op lijken. Maar"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SingleWordInP_ResultsWordInSpan()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>woord</p></body>.ToString())
        Dim splicer As New RegExHtmlSplitter(New Regex("\w+"), doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/text()"), 5)

        'Act
        Dim result = splicer.Split()

        'Assert
        Assert.AreEqual(1, result.Count())
        Assert.AreEqual(<span>woord</span>.ToString(), result(0).OuterXml)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub DoubleWordInP_ResultsWordInSpan()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>woord*waard</p></body>.ToString())
        Dim splicer As New RegExHtmlSplitter(New Regex("\w+"), doc.SelectSingleNode("/body/p/text()"), 2, doc.SelectSingleNode("/body/p/text()"), 9)

        'Act
        Dim result = splicer.Split()

        'Assert
        Assert.AreEqual(2, result.Count())
        Assert.AreEqual(<span>ord</span>.ToString(), result(0).OuterXml)
        Assert.AreEqual(<span>waa</span>.ToString(), result(1).OuterXml)
        Dim xDoc As New XDocument()
        xDoc.Add(<body><p>wo<span>ord</span>*<span>waa</span>rd</p></body>)
        Dim xDocResult As New XDocument()
        xdocResult.Add(XElement.Parse(doc.OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(xDoc, xDocResult))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub Match2WordsWithStyle()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>woord <b>waard</b></p></body>.ToString())
        Dim splicer As New RegExHtmlSplitter(New Regex("\w+"), doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/b/text()"), 5)

        'Act
        Dim result = splicer.Split()

        'Assert
        Assert.AreEqual(2, result.Count())
        Assert.AreEqual(<span>woord</span>.ToString(), result(0).OuterXml)
        Assert.AreEqual(<span>waard</span>.ToString(), result(1).OuterXml)
        Dim xDoc As New XDocument()
        xDoc.Add(<body><p><span>woord</span><b><span>waard</span></b></p></body>)
        Dim xDocResult As New XDocument()
        xdocResult.Add(XElement.Parse(doc.OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(xDoc, xDocResult))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub MatchWithEncodedHtml()
        'Arrange
        Dim doc As New XmlDocument : doc.LoadXml(example4.ToString())
        Dim splicer As New RegExHtmlSplitter(New Regex(".+"), doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/text()"), 35)

        'Act
        Dim result = splicer.Split()

        'Assert
        Assert.AreEqual(1, result.Count())
        Assert.AreEqual("<span>De firma V&amp;D heeft in het jaar 2015</span>", result(0).OuterXml)
    End Sub

    Private example1 As XElement = <body>
                                       <p id="c1-id-8">begin</p>
                                       <p id="c1-id-9">Start hier met selecteren.</p>
                                       <p id="c1-id-10">Dit is een alinea. Er zijn velen die er op lijken. Maar dit is de alinea die je wil selecteren. Niet selecteren betekent dat je faalt. Selecteer en je zult het redden.</p>
                                       <p id="c1-id-11">Einde van selecteren.</p>
                                       <p id="c1-id-12">eind</p>
                                   </body>

    Private example2 As XElement = <p id="c1-id-10">
                                       <strong id="c1-id-13">Dit is een </strong>
                                       alinea. Er zijn velen die er op lijken. Maar dit is de alinea die je wil selecteren. Niet selecteren betekent dat je faalt. Selecteer en je zult het redden.</p>

    Private example3 As XElement = <p id="c1-id-10">Dit is 
                                        <strong id="c1-id-13">een </strong>
                                        alinea. Er zijn velen die er op lijken. Maar dit is de alinea die je wil selecteren. Niet selecteren betekent dat je faalt. Selecteer en je zult het redden.</p>

    Private example4 As XElement = <body><p id="c1-id-12">De firma V<%= "&" %>D heeft in het jaar 2015 zware problemen ondervonden.</p></body>

End Class