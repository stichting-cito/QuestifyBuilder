
Imports System.Xml
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class SentenceSplitterTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SplitInSentenceInP()
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>Dit is een alinea. Er zijn velen die er op lijken.</p></body>.ToString())
        Dim splicer As New SentenceHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/text()"), 19)

        Dim result = splicer.Split()

        Assert.AreEqual(1, result.Count())

        Dim docExpected As New XDocument
        docExpected.Add(XElement.Parse("<body><p><span>Dit is een alinea.</span> Er zijn velen die er op lijken.</p></body>"))
        Dim docResult As New XDocument
        docResult.Add(XElement.Parse(doc.OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(docExpected, docResult))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SplitInSentenceInP_WithMarkup()
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>Dit is een <b>alinea. Er</b> zijn velen die er op lijken.</p></body>.ToString())
        Dim splicer As New SentenceHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/text()"), 19)

        Dim result = splicer.Split()

        Assert.AreEqual(0, result.Count())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub ExampleWithSelectionOverParagraph()
        Dim doc As New XmlDocument : doc.LoadXml(data.ToString())
        Dim splicer As New SentenceHtmlSplitter(doc.SelectSingleNode("/body/p[@id='c1-id-9']/text()"), 6, doc.SelectSingleNode("/body/p[@id='c1-id-10']/text()"), 11)

        Dim result = splicer.Split()

        Assert.AreEqual(1, result.Count())

        Dim docExpected As New XDocument
        docExpected.Add(XElement.Parse("<p id=""c1-id-9"" >Start <span>hier met selecteren.</span></p>"))
        Dim docResult As New XDocument
        docResult.Add(XElement.Parse(doc.SelectSingleNode("/body/p[@id='c1-id-9']").OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(docExpected, docResult))

        docExpected = New XDocument()
        docExpected.Add(XElement.Parse("<p id=""c1-id-10"">Dit is een alinea. Er zijn velen die er op lijken. Maar dit is de alinea die je wil selecteren. Niet selecteren betekent dat je faalt. Selecteer en je zult het redden.</p>"))
        docResult = New XDocument()
        docResult.Add(XElement.Parse(doc.SelectSingleNode("/body/p[@id='c1-id-10']").OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(docExpected, docResult))
    End Sub

    Private data As XElement = <body>
                                   <p id="c1-id-8">begin</p>
                                   <p id="c1-id-9">Start hier met selecteren.</p>
                                   <p id="c1-id-10">Dit is een alinea. Er zijn velen die er op lijken. Maar dit is de alinea die je wil selecteren. Niet selecteren betekent dat je faalt. Selecteer en je zult het redden.</p>
                                   <p id="c1-id-11">Einde van selecteren.</p>
                                   <p id="c1-id-12">eind</p>
                               </body>

End Class
