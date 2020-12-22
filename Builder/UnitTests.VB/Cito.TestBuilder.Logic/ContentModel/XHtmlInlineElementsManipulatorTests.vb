
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class XHtmlInlineElementsManipulatorTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub ExtractInlineElementsFromXhtmlParameter_ShouldHave2()
        Dim xHtmlParam As XHtmlParameter
        xHtmlParam = DirectCast(SerializeHelper.XmlDeserializeFromString(_xhtmlParam.ToString(), GetType(XHtmlParameter)), XHtmlParameter)

        Dim result = xHtmlParam.GetInlineElements()

        Assert.AreEqual(2, result.Values.Count)
        Assert.IsNotNull(result("3b2b1940-0510-48d5-a4b8-91ce41e50d77"))
        Assert.IsNotNull(result("3b2b1940-0220-48d5-a4b8-91ce41e50d77"))
        Assert.IsFalse(result.ContainsKey("This key is not present"))
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub ReplaceInlineElement_ShouldHave2()
        Dim xHtmlParam As XHtmlParameter
        xHtmlParam = DirectCast(SerializeHelper.XmlDeserializeFromString(_xhtmlParam.ToString(), GetType(XHtmlParameter)), XHtmlParameter)
        Dim manipulator As New XHtmlInlineElementsManipulator(xHtmlParam)
        Dim ReplacementInline As New InlineElement() With {.Identifier = "Replaced"}

        manipulator.ReplaceInlineElement("3b2b1940-0220-48d5-a4b8-91ce41e50d77", ReplacementInline)

        Dim result = xHtmlParam.GetInlineElements()
        Assert.AreEqual(2, result.Values.Count)
        Assert.IsNotNull(result("Replaced"))
        Assert.IsNotNull(result("3b2b1940-0510-48d5-a4b8-91ce41e50d77"))
        Assert.IsFalse(result.ContainsKey("This key is not present"))
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ReplaceInlineElement_NameSpaceShouldHaveSpecificFormat()
        Dim xHtmlParam As XHtmlParameter
        xHtmlParam = DirectCast(SerializeHelper.XmlDeserializeFromString(_xhtmlParam2.ToString(), GetType(XHtmlParameter)), XHtmlParameter)
        Dim manipulator As New XHtmlInlineElementsManipulator(xHtmlParam)
        Dim ReplacementInline As New InlineElement() With {.Identifier = "Replaced"}
        manipulator.ReplaceInlineElement("ToReplace", ReplacementInline)
        Assert.IsTrue(xHtmlParam.Value.Contains("<cito:InlineElement "))
    End Sub

    Private _xhtmlParam As XElement = <XHtmlParameter name="itemGeneral">
                                          <designersetting key="label">Algemeen tekstveld<listvalues/></designersetting>
                                          <designersetting key="description">Geef hier tekst en/of afbeelding in zoals die in het rechter deel onder de antwoorden weergegeven dient te worden.<listvalues/></designersetting>
                                          <designersetting key="group">1 Algemeen tekstveld<listvalues/></designersetting>
                                          <designersetting key="required">True<listvalues/></designersetting>
                                          <p id="c1-id-9" xmlns="http://www.w3.org/1999/xhtml">
                                              <cito:InlineElement id="3b2b1940-0510-48d5-a4b8-91ce41e50d77" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                  <cito:parameters>
                                                      <cito:parameterSet id="entireItem">
                                                          <cito:resourceparameter name="source">
                                                              <cito:designersetting key="label">Afbeelding bestand<cito:listvalues/></cito:designersetting>
                                                              <cito:designersetting key="description">Selecteer de afbeelding<cito:listvalues/></cito:designersetting>
                                                              <cito:designersetting key="required">true<cito:listvalues/></cito:designersetting>
                                                              <cito:designersetting key="filter">image/pjpeg|image/jpeg|image/png|image/gif<cito:listvalues/></cito:designersetting>
                                                              <cito:designersetting key="group">Afbeelding<cito:listvalues/></cito:designersetting>
                                                              <cito:designersetting key="sortkey">0<cito:listvalues/></cito:designersetting>1001.gif</cito:resourceparameter>
                                                      </cito:parameterSet>
                                                  </cito:parameters>
                                              </cito:InlineElement>
                                              <cito:InlineElement id="3b2b1940-0220-48d5-a4b8-91ce41e50d77" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                  <cito:parameters>
                                                      <cito:parameterSet id="entireItem">
                                                          <cito:resourceparameter name="source">
                                                              <cito:designersetting key="sortkey">0<cito:listvalues/></cito:designersetting>1002.gif</cito:resourceparameter>
                                                      </cito:parameterSet>
                                                  </cito:parameters>
                                              </cito:InlineElement>Dit bewaar ik <br id="c1-id-10"/>2<br id="c1-id-11"/>3</p>
                                      </XHtmlParameter>

    Private _xhtmlParam2 As XElement = <XHtmlParameter name="itemGeneral">
                                           <p id="c1-id-9" xmlns="http://www.w3.org/1999/xhtml">
                                               <cito:InlineElement id="ToReplace" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                   <cito:parameters>
                                                       <cito:parameterSet id="entireItem">
                                                           <cito:resourceparameter name="source"/>
                                                       </cito:parameterSet>
                                                   </cito:parameters>
                                               </cito:InlineElement>
                                           </p>
                                       </XHtmlParameter>

End Class
