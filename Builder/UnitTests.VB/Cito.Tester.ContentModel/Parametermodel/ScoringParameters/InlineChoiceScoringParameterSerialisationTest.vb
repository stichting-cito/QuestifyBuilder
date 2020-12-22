
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Xml.Linq
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

<TestClass()>
Public Class InlineChoiceScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        Dim inlineChoiceScoringPrm = New InlineChoiceScoringParameter()
        inlineChoiceScoringPrm.MinChoices = 1
        inlineChoiceScoringPrm.MaxChoices = 2
        inlineChoiceScoringPrm.ControllerId = "inline_1"
        inlineChoiceScoringPrm.FindingOverride = "inline_finding"
        inlineChoiceScoringPrm.BluePrint = New ParameterCollection()
        inlineChoiceScoringPrm.BluePrint.InnerParameters.Add(New PlainTextParameter() With {.Name = "option"})

        Dim subParam1 = New ParameterCollection()
        subParam1.InnerParameters.Add(New PlainTextParameter() With {.Name = "option", .Value = "a"})
        Dim subParam2 = New ParameterCollection()
        subParam2.InnerParameters.Add(New PlainTextParameter() With {.Name = "option", .Value = "b"})
        inlineChoiceScoringPrm.Value = New ParameterSetCollection()
        inlineChoiceScoringPrm.Value.Add(subParam1)
        inlineChoiceScoringPrm.Value.Add(subParam2)

        Dim result = DoSerialize(Of InlineChoiceScoringParameter)(inlineChoiceScoringPrm)

        Assert.AreEqual(<InlineChoiceScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ControllerId="inline_1" findingOverride="inline_finding" minChoices="1" maxChoices="2">
                            <subparameterset id="">
                                <plaintextparameter name="option">a</plaintextparameter>
                            </subparameterset>
                            <subparameterset id="">
                                <plaintextparameter name="option">b</plaintextparameter>
                            </subparameterset>
                            <definition id="">
                                <plaintextparameter name="option"/>
                            </definition>
                        </InlineChoiceScoringParameter>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_Test()
        Dim xmlData = <InlineChoiceScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ControllerId="inline_1" findingOverride="inline_finding" minChoices="1" maxChoices="2">
                          <definition id="">
                              <plaintextparameter name="option"/>
                          </definition>
                          <subparameterset id="1">
                              <plaintextparameter name="option">a</plaintextparameter>
                          </subparameterset>
                          <subparameterset id="2">
                              <plaintextparameter name="option">b</plaintextparameter>
                          </subparameterset>
                      </InlineChoiceScoringParameter>

        Dim result = Deserialize(Of InlineChoiceScoringParameter)(xmlData)

        Assert.AreEqual(result.ControllerId, "inline_1")
        Assert.AreEqual(result.FindingOverride, "inline_finding")
        Assert.AreEqual(result.MinChoices, 1)
        Assert.AreEqual(result.MaxChoices, 2)
        Assert.AreEqual(result.BluePrint.InnerParameters.Count, 1)
        Assert.IsInstanceOfType(result.BluePrint.InnerParameters(0), GetType(PlainTextParameter))
        Assert.AreEqual(result.Value.Count, 2)
        Assert.AreEqual(result.Value(0).InnerParameters.Count, 1)
        Assert.IsInstanceOfType(result.Value(0).InnerParameters(0), GetType(PlainTextParameter))
        Assert.AreEqual(CType(result.Value(0).InnerParameters(0), PlainTextParameter).Value, "a")
        Assert.AreEqual(result.Value(1).InnerParameters.Count, 1)
        Assert.IsInstanceOfType(result.Value(1).InnerParameters(0), GetType(PlainTextParameter))
        Assert.AreEqual(CType(result.Value(1).InnerParameters(0), PlainTextParameter).Value, "b")
    End Sub

    <TestMethod(), TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub DeserializeFilledInlineChoiceScoringParameter_Test()
        Dim xmlData = _inlineChoiceWithOptions

        Dim result = Deserialize(Of InlineChoiceScoringParameter)(xmlData)

        Assert.AreEqual("inlineChoiceController", result.ControllerId)
        Assert.AreEqual("Opgave", result.FindingOverride)
        Assert.AreEqual(0, result.MinChoices)
        Assert.AreEqual(1, result.MaxChoices)
        Assert.AreEqual(result.BluePrint.InnerParameters.Count, 1)
        Assert.IsInstanceOfType(result.BluePrint.InnerParameters(0), GetType(XHtmlParameter))
        Assert.AreEqual(result.Value.Count, 3)
        Assert.AreEqual(result.Value(0).InnerParameters.Count, 1)
        Assert.IsInstanceOfType(result.Value(0).InnerParameters(0), GetType(XHtmlParameter))
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(<p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">100</p>), XDocument.Parse(CType(result.Value(0).InnerParameters(0), XHtmlParameter).Value), Nothing))
        Assert.AreEqual(result.Value(1).InnerParameters.Count, 1)
        Assert.IsInstanceOfType(result.Value(1).InnerParameters(0), GetType(XHtmlParameter))
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(<p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">200</p>), XDocument.Parse(CType(result.Value(1).InnerParameters(0), XHtmlParameter).Value), Nothing))
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    <Description("Validates the XmlElement definition on InnerParameters ")>
    Public Sub CompareWithPreviouslyKnowStateInAssessment()

        Dim a = New AssessmentItem
        a.Parameters.Add(New ParameterCollection())
        a.Parameters(0).InnerParameters.Add(New InlineChoiceScoringParameter())

        Dim result = DoSerialize(a)

        Assert.AreEqual(<assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="" title="" layoutTemplateSrc="">
                            <solution>
                                <keyFindings/>
                                <aspectReferences/>
                            </solution>
                            <parameters>
                                <parameterSet id="">
                                    <inlineChoiceScoringparameter minChoices="0" maxChoices="0"/>
                                </parameterSet>
                            </parameters>
                        </assessmentItem>.ToString(), result.ToString())

    End Sub

    <TestMethod(), TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub DeserializeAssessmentItem_Test()
        Dim xmlData = _assessmentItemWithXhtmlParametersInXhtmlParameters

        Dim result = Deserialize(Of AssessmentItem)(xmlData)

        Dim params As HashSet(Of ScoringParameter) = result.Parameters.DeepFetchInlineScoringParameters()
        Assert.AreEqual(1, params.Count)
        Assert.IsInstanceOfType(params.First(), GetType(InlineChoiceScoringParameter))
        Dim inlineChoiceScoringParameter As InlineChoiceScoringParameter = DirectCast(params.First(), InlineChoiceScoringParameter)
        Assert.AreEqual("inlineChoiceController", inlineChoiceScoringParameter.ControllerId)
        Assert.AreEqual("inlineChoiceController", inlineChoiceScoringParameter.FindingOverride)
        Assert.AreEqual(0, inlineChoiceScoringParameter.MinChoices)
        Assert.AreEqual(1, inlineChoiceScoringParameter.MaxChoices)
        Assert.AreEqual(1, inlineChoiceScoringParameter.BluePrint.InnerParameters.Count)
        Assert.IsInstanceOfType(inlineChoiceScoringParameter.BluePrint.InnerParameters(0), GetType(XHtmlParameter))
        Assert.AreEqual(inlineChoiceScoringParameter.Value.Count, 2)
        Assert.AreEqual(inlineChoiceScoringParameter.Value(0).InnerParameters.Count, 1)
        Assert.IsInstanceOfType(inlineChoiceScoringParameter.Value(0).InnerParameters(0), GetType(XHtmlParameter))

        Dim xhtmlParam1 As XHtmlParameter = DirectCast(inlineChoiceScoringParameter.Value(0).InnerParameters(0), XHtmlParameter)
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(<p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml"><strong id="c1-id-12">bold </strong>option</p>), XDocument.Parse(xhtmlParam1.Value), Nothing))

        Assert.AreEqual(inlineChoiceScoringParameter.Value(1).InnerParameters.Count, 1)
        Assert.IsInstanceOfType(inlineChoiceScoringParameter.Value(1).InnerParameters(0), GetType(XHtmlParameter))

        Dim xhtmlParam2 As XHtmlParameter = DirectCast(inlineChoiceScoringParameter.Value(1).InnerParameters(0), XHtmlParameter)
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(<p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml"><em id="c1-id-12">italic </em>option</p>), XDocument.Parse(xhtmlParam2.Value), Nothing))
    End Sub


    ReadOnly _inlineChoiceWithOptions As XElement =
       <InlineChoiceScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="inlineChoiceScoring" label="Getal" ControllerId="inlineChoiceController" findingOverride="Opgave" minChoices="0" maxChoices="1">
           <subparameterset id="A">
               <xhtmlparameter name="icOption">
                   <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">100</p>
               </xhtmlparameter>
           </subparameterset>
           <subparameterset id="B">
               <xhtmlparameter name="icOption">
                   <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">200</p>
               </xhtmlparameter>
           </subparameterset>
           <subparameterset id="C">
               <xhtmlparameter name="icOption">
                   <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">300</p>
               </xhtmlparameter>
           </subparameterset>
           <definition id="">
               <xhtmlparameter name="icOption"/>
           </definition>
       </InlineChoiceScoringParameter>

    ReadOnly _assessmentItemWithXhtmlParametersInXhtmlParameters As XElement =
        <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="ChoiceInlineRT2" title="ChoiceInlineRT1" layoutTemplateSrc="Cito.Generic.Choice.Inline.SC.RT">
            <solution>
                <keyFindings>
                    <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                        <keyFact id="A-Ib46168e0-861a-4843-a4b4-69240681078c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ib46168e0-861a-4843-a4b4-69240681078c" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>
            <parameters>
                <parameterSet id="styling">
                    <listedparameter name="itemStyle">Default</listedparameter>
                </parameterSet>
                <parameterSet id="entireItem">
                    <listedparameter name="inlineType">choice</listedparameter>
                    <plaintextparameter name="inlineClass"/>
                    <integerparameter name="maxChoices">0</integerparameter>
                    <booleanparameter name="dualColumnLayout">False</booleanparameter>
                    <booleanparameter name="showCalculatorButton"/>
                    <booleanparameter name="displayVerklankingOnTheRight">True</booleanparameter>
                    <collectionparameter name="numberOfAudioContentItems">
                        <definition id="">
                            <resourceparameter name="audiocontent"/>
                            <xhtmlparameter name="description"/>
                        </definition>
                    </collectionparameter>
                    <texttospeechparameter name="verklankingLinks"/>
                    <texttospeechparameter name="verklankingRechts"/>
                    <xhtmlparameter name="leftBody"/>
                    <xhtmlparameter name="leftItemInlineInput"/>
                    <integerparameter name="leftItemInlineInputWidth">0</integerparameter>
                    <xhtmlresourceparameter name="leftSource"/>
                    <resourceparameter name="wordSourceText"/>
                    <integerparameter name="sourceHeight">200</integerparameter>
                    <integerparameter name="sourcePositionTop">0</integerparameter>
                    <xhtmlparameter name="itemBody"/>
                    <xhtmlparameter name="itemQuestion">
                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml"> </p>
                    </xhtmlparameter>
                    <xhtmlparameter name="itemInlineInput">
                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">hallo <cito:InlineElement id="Ib46168e0-861a-4843-a4b4-69240681078c" layoutTemplateSourceName="InlineChoiceLayoutTemplateRT" xmlns:cito="http://www.cito.nl/citotester">
                                <cito:parameters>
                                    <cito:parameterSet id="entireItem">
                                        <cito:plaintextparameter name="inlineChoiceId">Ib46168e0-861a-4843-a4b4-69240681078c</cito:plaintextparameter>
                                        <cito:plaintextparameter name="inlineChoiceLabel">inlinechoice1</cito:plaintextparameter>
                                        <cito:booleanparameter name="noEmptyRow">False</cito:booleanparameter>
                                        <cito:plaintextparameter name="fillOutInfo"/>
                                        <cito:collectionparameter name="choices">
                                            <cito:definition id="">
                                                <cito:plaintextparameter name="choice"/>
                                            </cito:definition>
                                        </cito:collectionparameter>
                                        <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="inlinechoice1" ControllerId="inlineChoiceController" findingOverride="inlineChoiceController" minChoices="0" maxChoices="1">
                                            <cito:subparameterset id="A">
                                                <cito:xhtmlparameter name="icOption">
                                                    <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                                        <strong id="c1-id-12">bold </strong>option</p>
                                                </cito:xhtmlparameter>
                                            </cito:subparameterset>
                                            <cito:subparameterset id="B">
                                                <cito:xhtmlparameter name="icOption">
                                                    <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                                        <em id="c1-id-12">italic </em>option</p>
                                                </cito:xhtmlparameter>
                                            </cito:subparameterset>
                                            <cito:definition id="">
                                                <cito:xhtmlparameter name="icOption"/>
                                            </cito:definition>
                                        </cito:inlineChoiceScoringparameter>
                                    </cito:parameterSet>
                                </cito:parameters>
                            </cito:InlineElement>
                        </p>
                    </xhtmlparameter>
                    <xhtmlparameter name="itemGeneral"/>
                    <collectionparameter name="choices">
                        <definition id="">
                            <plaintextparameter name="choice"/>
                            <integerparameter name="nrOfConnections"/>
                        </definition>
                    </collectionparameter>
                    <booleanparameter name="autoScoring">True</booleanparameter>
                </parameterSet>
            </parameters>
        </assessmentItem>


End Class
