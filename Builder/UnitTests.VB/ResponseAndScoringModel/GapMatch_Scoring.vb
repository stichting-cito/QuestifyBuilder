
Imports System.Xml.Linq

<TestClass>
Public Class GapMatch_Scoring
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Score_MaxSolutionSolution_Test()
        Dim sol = ToSolution(_sol)

        Dim maxScore = sol.GetMaxSolutionTranslatedScore()

        Assert.IsTrue(CType((maxScore = 1), Boolean))
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Score_CorrectResponse_Expects1()
        Dim sol = ToSolution(_sol)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("A", "I23a0653b-b574-4d5e-ad66-e05af1a169da"), New Tuple(Of String, String)("B", "I27b8eca6-ae70-4f7b-bc23-b904b2e9045f"), New Tuple(Of String, String)("C", "I47a1295a-c729-49d5-9da0-bac0799a019e")}
        Dim r = GetResponse(valuesToCreate, "gapMatchController")

        Dim score = sol.ScoreSolution(r)

        Assert.IsTrue(CType((score = 1), Boolean))
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Score_IncompleteResponse_Expects0()
        Dim sol = ToSolution(_sol)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("A", "I23a0653b-b574-4d5e-ad66-e05af1a169da"), New Tuple(Of String, String)("B", "I27b8eca6-ae70-4f7b-bc23-b904b2e9045f")}
        Dim r = GetResponse(valuesToCreate, "gapMatchController")

        Dim score = sol.ScoreSolution(r)

        Assert.IsTrue(CType((score = 0), Boolean))
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Score_IncorrectResponse_Expects0()
        Dim sol = ToSolution(_sol)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("B", "I23a0653b-b574-4d5e-ad66-e05af1a169da"), New Tuple(Of String, String)("B", "I27b8eca6-ae70-4f7b-bc23-b904b2e9045f"), New Tuple(Of String, String)("A", "I47a1295a-c729-49d5-9da0-bac0799a019e")}
        Dim r = GetResponse(valuesToCreate, "gapMatchController")

        Dim score = sol.ScoreSolution(r)

        Assert.IsTrue(CType((score = 0), Boolean))
    End Sub

    Private ReadOnly _sol As XElement = <solution>
                                            <keyFindings>
                                                <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                                                    <keyFact id="I23a0653b-b574-4d5e-ad66-e05af1a169da" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <keyValue domain="I23a0653b-b574-4d5e-ad66-e05af1a169da" occur="1">
                                                            <stringValue>
                                                                <typedValue>A</typedValue>
                                                            </stringValue>
                                                        </keyValue>
                                                    </keyFact>
                                                    <keyFact id="I27b8eca6-ae70-4f7b-bc23-b904b2e9045f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <keyValue domain="I27b8eca6-ae70-4f7b-bc23-b904b2e9045f" occur="1">
                                                            <stringValue>
                                                                <typedValue>B</typedValue>
                                                            </stringValue>
                                                        </keyValue>
                                                    </keyFact>
                                                    <keyFact id="I47a1295a-c729-49d5-9da0-bac0799a019e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <keyValue domain="I47a1295a-c729-49d5-9da0-bac0799a019e" occur="1">
                                                            <stringValue>
                                                                <typedValue>C</typedValue>
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

End Class
