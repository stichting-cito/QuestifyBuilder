Imports System.Linq
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringDisplayValueCalculcatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub KeyValueString_Joined()
        Dim solution = New Solution()

        Dim param1 = New ChoiceScoringParameter() With {.ControllerId = "24834555-98fa-45ae-b5c0-245085594ade", .FindingOverride = "mc", .MaxChoices = 1}.AddSubParameters("A", "B")
        Dim param2 = New DecimalScoringParameter() With {.ControllerId = "Gap", .InlineId = "mc", .SupportCasScoring = True}.AddSubParameters("1")

        Dim manipulator1 = param1.GetScoreManipulator(solution)
        Dim manipulator2 = param2.GetScoreManipulator(solution)

        manipulator1.SetKey("A")
        Dim val1 As New GapValue(Of MultiType)(123.45D)
        Dim val2 As New GapValue(Of MultiType)(1D, 100D)
        manipulator2.SetKey("1", val1, val2)

        solution.WriteToDebug("pre")

        Dim keyValueString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        Assert.AreEqual("A&123.45#1-100", keyValueString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MultiResponseDisplayValueString()
        Dim param1 = New MultiChoiceScoringParameter() With {.ControllerId = "mr", .MaxChoices = 6}.AddSubParameters("A", "B", "C", "D", "E", "F")
        Dim solution = New Solution()

        Dim manipulator1 = param1.GetScoreManipulator(solution)
        manipulator1.SetKey("B")
        manipulator1.SetKey("D")

        Dim keyValueString = New ScoringDisplayValueCalculator({param1}, solution).GetScoreDisplayValue()

        Assert.AreEqual("True&True", keyValueString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), WorkItem(27299)>
    Public Sub IntegerDisplayValue_Empty()
        Dim intPrm = New IntegerScoringParameter() With {.ControllerId = "gapController", .InlineId = "I2a613de4-179c-4c94-95f4-21c1f59236dd", .FindingOverride = "IEF", .Name = "integerScoring"}.AddSubParameters("1")
        Dim s = _integerSolution.To(Of Solution)()
        Dim key As KeyValue = CType(s.Findings.Single().Facts.Single.Values.Single(), KeyValue)
        key.Values.Add(Nothing)
        Dim displayCalculator = New ScoringDisplayValueCalculator(New ScoringParameter() {intPrm}, s)

        Dim result = displayCalculator.GetScoreDisplayValue()

        Assert.AreEqual(String.Empty, result)
    End Sub

    ReadOnly _integerSolution As XElement = <solution>
                                                <keyFindings>
                                                    <keyFinding id="IEF" scoringMethod="Dichotomous">
                                                        <keyFact id="1-I2a613de4-179c-4c94-95f4-21c1f59236dd" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <keyValue domain="I2a613de4-179c-4c94-95f4-21c1f59236dd" occur="1"/>
                                                        </keyFact>
                                                    </keyFinding>
                                                </keyFindings>
                                                <aspectReferences/>
                                                <ItemScoreTranslationTable>
                                                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                </ItemScoreTranslationTable>
                                            </solution>


End Class
