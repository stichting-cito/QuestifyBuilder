
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class InlineChoiceScoringManipulatorTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SetKeyB_GetDisplayValue_B()
        Dim itm = itemInlineChoice.To(Of AssessmentItem)()
        Dim scoringParams = itm.Parameters.DeepFetchInlineScoringParameters()

        Dim scoringParameter = DirectCast(
            scoringParams.First(Function(scoringPrm) scoringPrm.InlineId.StartsWith("I00000001")),
            InlineChoiceScoringParameter)

        scoringParameter.GetScoreManipulator(itm.Solution).SetKey("B")

        Dim scoringMap = New ScoringMap(New ScoringParameter() {scoringParameter}, itm.Solution).GetMap().First()

        Dim conceptManipulator = scoringMap.GetConceptManipulator(itm.Solution)
        Dim keyValue = conceptManipulator.GetDisplayValueForConceptId("B")

        Assert.AreEqual(3, scoringMap.Count(), "It is a choice so 3 scoringMapKeys")
        Assert.AreEqual("200", keyValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SetKeyB_GetDisplayValue_A1()
        Dim itm = itemInlineChoice.To(Of AssessmentItem)()
        Dim scoringParams = itm.Parameters.DeepFetchInlineScoringParameters()

        Dim scoringParameter = DirectCast(
            scoringParams.First(Function(scoringPrm) scoringPrm.InlineId.StartsWith("I00000001")),
            InlineChoiceScoringParameter)

        scoringParameter.GetScoreManipulator(itm.Solution).SetKey("B")

        Dim scoringMap = New ScoringMap(New ScoringParameter() {scoringParameter}, itm.Solution).GetMap().First()

        Dim conceptManipulator = scoringMap.GetConceptManipulator(itm.Solution)
        Dim keyValue = conceptManipulator.GetDisplayValueForConceptId("A[1]")


        Assert.AreEqual(3, scoringMap.Count(), "It is a choice so 3 scoringMapKeys")
        Assert.AreEqual("100", keyValue)
    End Sub


    Private ReadOnly itemInlineChoice As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="1002" title="1002" layoutTemplateSrc="ilt.inline.choice">
                                                        <solution>
                                                        </solution>
                                                        <parameters>
                                                            <parameterSet id="entireItem">
                                                                <xhtmlparameter name="itemInlineInput">
                                                                    <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">
          Het getal <cito:InlineElement id="I00000001-1b56-4c53-880d-2d54d060fba8" layoutTemplateSourceName="tmp.inline.choice" xmlns:cito="http://www.cito.nl/citotester">
                                                                            <cito:parameters>
                                                                                <cito:parameterSet id="entireItem">
                                                                                    <cito:plaintextparameter name="controlType">choice</cito:plaintextparameter>
                                                                                    <cito:plaintextparameter name="inlineChoiceId">I00000001-1b56-4c53-880d-2d54d060fba8</cito:plaintextparameter>
                                                                                    <cito:plaintextparameter name="inlineChoiceLabel">Getal</cito:plaintextparameter>
                                                                                    <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="Getal" ControllerId="inlineChoiceController" findingOverride="Opgave" minChoices="0" maxChoices="1">
                                                                                        <cito:subparameterset id="A">
                                                                                            <cito:plaintextparameter name="icOption">100</cito:plaintextparameter>
                                                                                        </cito:subparameterset>
                                                                                        <cito:subparameterset id="B">
                                                                                            <cito:plaintextparameter name="icOption">200</cito:plaintextparameter>
                                                                                        </cito:subparameterset>
                                                                                        <cito:subparameterset id="C">
                                                                                            <cito:plaintextparameter name="icOption">300</cito:plaintextparameter>
                                                                                        </cito:subparameterset>
                                                                                        <cito:definition id="">
                                                                                            <cito:plaintextparameter name="icOption"/>
                                                                                        </cito:definition>
                                                                                    </cito:inlineChoiceScoringparameter>
                                                                                </cito:parameterSet>
                                                                            </cito:parameters>
                                                                        </cito:InlineElement>  is in het Romeinse systeem: <cito:InlineElement id="I11111111-43d7-49e6-ab4a-fb967ecba6cc" layoutTemplateSourceName="tmp.inline.choice" xmlns:cito="http://www.cito.nl/citotester">
                                                                            <cito:parameters>
                                                                                <cito:parameterSet id="entireItem">
                                                                                    <cito:plaintextparameter name="controlType">choice</cito:plaintextparameter>
                                                                                    <cito:plaintextparameter name="inlineChoiceId">I11111111-43d7-49e6-ab4a-fb967ecba6cc</cito:plaintextparameter>
                                                                                    <cito:plaintextparameter name="inlineChoiceLabel">Roman</cito:plaintextparameter>
                                                                                    <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="Roman" ControllerId="inlineChoiceController" findingOverride="Opgave" minChoices="0" maxChoices="1">
                                                                                        <cito:subparameterset id="A">
                                                                                            <cito:plaintextparameter name="icOption">C</cito:plaintextparameter>
                                                                                        </cito:subparameterset>
                                                                                        <cito:subparameterset id="B">
                                                                                            <cito:plaintextparameter name="icOption">D</cito:plaintextparameter>
                                                                                        </cito:subparameterset>
                                                                                        <cito:subparameterset id="C">
                                                                                            <cito:plaintextparameter name="icOption">CC</cito:plaintextparameter>
                                                                                        </cito:subparameterset>
                                                                                        <cito:subparameterset id="D">
                                                                                            <cito:plaintextparameter name="icOption">DD</cito:plaintextparameter>
                                                                                        </cito:subparameterset>
                                                                                        <cito:definition id="">
                                                                                            <cito:plaintextparameter name="icOption"/>
                                                                                        </cito:definition>
                                                                                    </cito:inlineChoiceScoringparameter>
                                                                                </cito:parameterSet>
                                                                            </cito:parameters>
                                                                        </cito:InlineElement>
                                                                    </p>
                                                                </xhtmlparameter>
                                                            </parameterSet>
                                                        </parameters>
                                                    </assessmentItem>



End Class
