
Imports System.Xml
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class WordHtmlSplitterTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SplitWords()
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>aap noot</p></body>.ToString())
        Dim splicer As New WordHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/text()"), 100)

        Dim result = splicer.Split()

        Assert.AreEqual(2, result.Count())

        Dim docExpected As New XDocument
        docExpected.Add(XElement.Parse("<body><p><span>aap</span> <span>noot</span></p></body>"))
        Dim docResult As New XDocument
        docResult.Add(XElement.Parse(doc.OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(docExpected, docResult))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SplitWords2()
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>aap noot mies teun</p></body>.ToString())
        Dim splicer As New WordHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 0, doc.SelectSingleNode("/body/p/text()"), 100)

        Dim result = splicer.Split()

        Assert.AreEqual(4, result.Count())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SplitWordsInP_Withoffsets()
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>Start hier met selecteren.</p></body>.ToString())
        Dim splicer As New WordHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 6, doc.SelectSingleNode("/body/p/text()"), 15)

        Dim result = splicer.Split()

        Assert.AreEqual(2, result.Count())

        Dim docExpected As New XDocument
        docExpected.Add(XElement.Parse("<body><p>Start <span>hier</span> <span>met</span> selecteren.</p></body>"))
        Dim docResult As New XDocument
        docResult.Add(XElement.Parse(doc.OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(docExpected, docResult))

    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SplitWordsInP_Withoffsets2()
        Dim doc As New XmlDocument : doc.LoadXml(<body><p>Dit is een alinea. Er zijn velen die er op lijken.</p></body>.ToString())
        Dim splicer As New WordHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 6, doc.SelectSingleNode("/body/p/text()"), 40)

        Dim result = splicer.Split()

        Assert.AreEqual(7, result.Count())

        Dim docExpected As New XDocument
        docExpected.Add(XElement.Parse("<body><p>Dit is <span>een</span> <span>alinea</span>. <span>Er</span> <span>zijn</span> <span>velen</span> <span>die</span> <span>er</span> op lijken.</p></body>"))
        Dim docResult As New XDocument
        docResult.Add(XElement.Parse(doc.OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(docExpected, docResult))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Sub SplitInToWords_WithEscapedHtml()

        Dim doc As New XmlDocument : doc.LoadXml(<body><p>In het jaar 2015 is V<%= "&" %>D een warenhuis dat in problemen is geraakt.</p></body>.ToString())
        Dim splicer As New WordHtmlSplitter(doc.SelectSingleNode("/body/p/text()"), 6, doc.SelectSingleNode("/body/p/text()"), 41)

        Dim result = splicer.Split()

        Assert.AreEqual(8, result.Count())

        Dim docExpected As New XDocument
        docExpected.Add(XElement.Parse("<body><p>In het <span>jaar</span> <span>2015</span> <span>is</span> <span>V</span>&amp;<span>D</span> <span>een</span> <span>warenhuis</span> <span>dat</span> in problemen is geraakt.</p></body>"))
        Dim docResult As New XDocument
        docResult.Add(XElement.Parse(doc.OuterXml))
        Assert.IsTrue(UnitTestHelper.AreSame(docExpected, docResult))
    End Sub

End Class
