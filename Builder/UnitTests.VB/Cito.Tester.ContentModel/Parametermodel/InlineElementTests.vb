
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports System.Linq
Imports System.Xml.Linq

<TestClass()>
Public Class InlineElementTests
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeserializeInlineElement()
        'Arrange
     
        'Act
        Dim res As InlineElement = DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(InlineElement)), InlineElement)
      
        'Assert
        Assert.AreEqual(1, res.Parameters.Count)
        Assert.AreEqual(1, res.Parameters.Count)
        Assert.AreEqual(1, res.Parameters(0).InnerParameters.Count)
        Assert.IsInstanceOfType(res.Parameters(0).InnerParameters(0), GetType(ResourceParameter))
        Dim p As ResourceParameter = DirectCast(res.Parameters(0).InnerParameters(0), ResourceParameter)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <WorkItem(9658)>
    Public Sub DeserializeInlineElementShouldNotContainDesignerSettings()
        'Arrange
      
        'Act
        Dim res As InlineElement = DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(InlineElement)), InlineElement)
      
        'Assert
        Assert.IsInstanceOfType(res.Parameters(0).InnerParameters(0), GetType(ResourceParameter))
        Dim p As ResourceParameter = DirectCast(res.Parameters(0).InnerParameters(0), ResourceParameter)
        Assert.IsFalse(p.Value.Contains("designersetting"))
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub DeserializeInlineElement_ExpectsPlainTextParametersToHaveValue()
        'Arrange
        Dim inline = someInlineElementWithFilledParams
      
        'Act
        Dim result = Deserialize(Of InlineElement)(inline)
      
        'Assert
        Assert.AreEqual(1, result.Parameters.Count, "Expects 1 parameter collection")
        Dim prms = result.Parameters(0).InnerParameters
        Assert.AreEqual(4, prms.Count, "Expects 4 parameters")
        Assert.IsInstanceOfType(prms(0), GetType(PlainTextParameter))

        Dim plainTxtPrm = DirectCast(prms(0), PlainTextParameter)
        Assert.IsFalse(String.IsNullOrEmpty(plainTxtPrm.Value), "Serialized with data,.. so it should have data now")
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub DeserializeInlineElement_ExpectsXhtmlParametersofInineChoiceToHaveValue()
        'Arrange
        Dim inline = someInlineElementWithFilledParams
    
        'Act
        Dim result = Deserialize(Of InlineElement)(inline)
     
        'Assert
        Assert.AreEqual(1, result.Parameters.Count, "Expects 1 parameter collection")
        Dim prms = result.Parameters(0).InnerParameters
        Assert.AreEqual(4, prms.Count, "Expects 4 parameters")
        Assert.IsInstanceOfType(prms(3), GetType(InlineChoiceScoringParameter))

        Dim inlineChoice = DirectCast(prms(3), InlineChoiceScoringParameter)
        Assert.AreEqual(3, inlineChoice.Value.Count, "Expects 3 parameters collections")

        Assert.AreEqual(1, inlineChoice.Value(0).InnerParameters.Count, "Expects 1st parameter collection to have 1 parameters")

        Assert.IsInstanceOfType(inlineChoice.Value(0).InnerParameters(0), GetType(XHtmlParameter))

        Dim xhtmlPrm = DirectCast(inlineChoice.Value(0).InnerParameters(0), XHtmlParameter)
        Assert.IsFalse(String.IsNullOrEmpty(xhtmlPrm.Value), "Serialized with data,.. so it should have data now")
    End Sub

    <TestMethod(), TestCategory("ContentModel")>
    Public Sub DeserializeInlineElementWithMathParameter()
        'Arrange
       
        'Act
        Dim res As InlineElement = Deserialize(Of InlineElement)(inlineElementWithMathMLParameter)

        'Assert
        Assert.AreEqual(1, res.Parameters.Count)
        Assert.AreEqual(8, res.Parameters(0).InnerParameters.Count)
        Assert.IsInstanceOfType(res.Parameters(0).InnerParameters(6), GetType(MathMLParameter))
        Dim mathMLPrm As MathMLParameter = DirectCast(res.Parameters(0).InnerParameters(6), MathMLParameter)
        Assert.AreEqual(1, mathMLPrm.Nodes.Count())
    End Sub

    <TestMethod(), TestCategory("ContentModel")>
    Public Sub DeserializeInlineElementWithResourceDimensions()
        'Arrange
       
        'Act
        Dim res As InlineElement = Deserialize(Of InlineElement)(inlineElementDimensions)

        'Assert
        Assert.AreEqual(95, res.Parameters(0).Parameters.OfType(Of ResourceParameter).First.Height)
    End Sub

#Region "DATA"

    Private data As XElement = <cito:InlineElement id="3b2b1940-0510-48d5-a4b8-91ce41e50d77" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
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
                               </cito:InlineElement>

    ReadOnly someInlineElementWithFilledParams As XElement = <cito:InlineElement id="I16746288-1b56-4c53-880d-2d54d060fba8" layoutTemplateSourceName="tmp.inline.choice" xmlns:cito="http://www.cito.nl/citotester">
                                                                 <cito:parameters>
                                                                     <cito:parameterSet id="entireItem">
                                                                         <cito:plaintextparameter name="controlType">choice</cito:plaintextparameter>
                                                                         <cito:plaintextparameter name="inlineChoiceId">I16746288-1b56-4c53-880d-2d54d060fba8</cito:plaintextparameter>
                                                                         <cito:plaintextparameter name="inlineChoiceLabel">Getal</cito:plaintextparameter>
                                                                         <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="Getal" ControllerId="inlineChoiceController" findingOverride="Opgave" minChoices="0" maxChoices="1">
                                                                             <cito:subparameterset id="A">
                                                                                 <cito:xhtmlparameter name="icOption" xmlns="http://www.w3.org/1999/xhtml">
                                                                                     <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">100</p>
                                                                                 </cito:xhtmlparameter>
                                                                             </cito:subparameterset>
                                                                             <cito:subparameterset id="B">
                                                                                 <cito:xhtmlparameter name="icOption" xmlns="http://www.w3.org/1999/xhtml">
                                                                                     <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">200</p>
                                                                                 </cito:xhtmlparameter>
                                                                             </cito:subparameterset>
                                                                             <cito:subparameterset id="C">
                                                                                 <cito:xhtmlparameter name="icOption" xmlns="http://www.w3.org/1999/xhtml">
                                                                                     <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">300</p>
                                                                                 </cito:xhtmlparameter>
                                                                             </cito:subparameterset>
                                                                             <cito:definition id="">
                                                                                 <xhtmlparameter name="icOption" xmlns="http://www.w3.org/1999/xhtml"/>
                                                                             </cito:definition>
                                                                         </cito:inlineChoiceScoringparameter>
                                                                     </cito:parameterSet>
                                                                 </cito:parameters>
                                                             </cito:InlineElement>

    ReadOnly inlineElementWithMathMLParameter As XElement =
        <cito:InlineElement id="I61d37dd0-596b-4cc9-a9a8-ef005f0bb01c" layoutTemplateSourceName="InlineGapFormulaLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
            <cito:parameters>
                <cito:parameterSet id="entireItem">
                    <cito:plaintextparameter name="gapId">I61d37dd0-596b-4cc9-a9a8-ef005f0bb01c</cito:plaintextparameter>
                    <cito:plaintextparameter name="gapLabel">Formula1</cito:plaintextparameter>
                    <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                    <cito:listedparameter name="gapType">Formula</cito:listedparameter>
                    <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                    <cito:booleanparameter name="hasMathMLScoring">True</cito:booleanparameter>
                    <cito:mathMLParameter name="initialMathML">
                        <math xmlns="http://www.w3.org/1998/Math/MathML">
                            <msqrt>
                                <mn>4</mn>
                            </msqrt>
                            <mo>-</mo>
                            <mn>2</mn>
                        </math>
                    </cito:mathMLParameter>
                    <cito:mathScoringParameter name="mathMLScoring" label="Formula1" ControllerId="gapController" findingOverride="gapController">
                        <cito:subparameterset id="1">
                            <cito:booleanparameter name="fictiveMathML">True</cito:booleanparameter>
                        </cito:subparameterset>
                        <cito:definition id="">
                            <cito:booleanparameter name="fictiveMathML"/>
                        </cito:definition>
                    </cito:mathScoringParameter>
                </cito:parameterSet>
            </cito:parameters>
        </cito:InlineElement>

    ReadOnly inlineElementDimensions as XElement = <cito:InlineElement id="Ia16cdf52-7f93-4eab-a19d-568a6ece1334" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                       <cito:parameters>
                                                           <cito:parameterSet id="entireItem">
                                                               <cito:resourceparameter name="source" height="95" width="50" editSize="true">L_Z3_FR_Les_A0147_00107-00113_Bild1.png</cito:resourceparameter>
                                                               <cito:booleanparameter name="useBorder">False</cito:booleanparameter>
                                                           </cito:parameterSet>
                                                       </cito:parameters>
                                                   </cito:InlineElement>

#End Region

End Class
