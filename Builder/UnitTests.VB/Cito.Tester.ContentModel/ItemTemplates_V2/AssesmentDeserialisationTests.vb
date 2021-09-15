
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class AssesmentDeserialisationTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeserializeAssesmentTest()
        'Arrange

        'Act
        Dim res As AssessmentItem = DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(AssessmentItem)), AssessmentItem)
        
        'Assert
        Assert.AreEqual(1, res.Parameters.Count)
        Assert.AreEqual(1, res.Parameters.Count)
        Assert.AreEqual(1, res.Parameters(0).InnerParameters.Count)
        Assert.IsInstanceOfType(res.Parameters(0).InnerParameters(0), GetType(XHtmlParameter))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <WorkItem(9658)>
    Public Sub DeserializeAssesmentTestShouldNotContainDesignerSettings()
        'Arrange

        'Act
        Dim res As AssessmentItem = DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(AssessmentItem)), AssessmentItem)
       
        'Assert
        Assert.IsInstanceOfType(res.Parameters(0).InnerParameters(0), GetType(XHtmlParameter))
        Dim p As XHtmlParameter = DirectCast(res.Parameters(0).InnerParameters(0), XHtmlParameter)
        Assert.IsFalse(p.Value.StartsWith("<designersetting"))
        'It's true that inlineelement still contains designersettings,...
    End Sub


    Private data As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="itmCssHtml" title="itmCssHtml" layoutTemplateSrc="ilt.CssHtml">
                                   <solution>
                                       <keyFindings/>
                                       <aspectReferences/>
                                   </solution>
                                   <parameters>
                                       <parameterSet id="invoer">
                                           <xhtmlparameter name="itemGeneral">
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
                                                                   <cito:designersetting key="sortkey">0<cito:listvalues/></cito:designersetting>InlineElement_20121018_7_57_21_843_0.gif</cito:resourceparameter>
                                                           </cito:parameterSet>
                                                       </cito:parameters>
                                                   </cito:InlineElement>Dit bewaar ik <br id="c1-id-10"/>2<br id="c1-id-11"/>3</p>
                                           </xhtmlparameter>
                                       </parameterSet>
                                   </parameters>
                               </assessmentItem>

End Class
