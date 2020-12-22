
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring
Imports FluentAssertions

<TestClass>
<ScoringMethod(ScoringFactory.Methods.V23_Scoring)>
Public Class MatrixScoreBug_V23Score_Polytomous
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWithCorrectResponse_ExpectsScore1()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.CorrectResponse).Should().Be(3)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_FirstAnswerWrong_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseFirstAnswerWrong).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_SecondAnswerWrong_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseSecondAnswerWrong).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_ThirdAnswerWrong_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseThirdAnswerWrong).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_FirstTwoAnswersWrong_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseFirstTwoAnswersWrong).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_LastTwoAnswersWrong_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseLastTwoAnswersWrong).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_FirstAndLastAnswerWrong_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseFirstAndLastAnswerWrong).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_FirstAnswerMissing_LastTwoCorrect_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseFirstAnswerMissingLastTwoCorrect).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_AllAnswersMissing_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseLastAnswerMissingAllAnswersMissing).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_FirstIncorrect_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseLastAnswerMissingFirstIncorrect).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_FirstTwoCorrect_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseLastAnswerMissingFirstTwoCorrect).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_SecondIncorrect_ExpectsScore0()
        GetScoreSolution(_recordedSolution, MatrixScoreBug_V23Score.IncorrectResponseLastAnswerMissingSecondIncorrect).Should().Be(1)
    End Sub

    Private ReadOnly _recordedSolution As XElement = <solution>
                                                         <keyFindings>
                                                             <keyFinding id="matrix" scoringMethod="Polytomous">
                                                                 <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="domainX" occur="3">
                                                                         <stringValue>
                                                                             <typedValue>A</typedValue>
                                                                         </stringValue>
                                                                     </keyValue>
                                                                     <keyValue domain="domainY" occur="1">
                                                                         <stringValue>
                                                                             <typedValue>A</typedValue>
                                                                         </stringValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="domainX" occur="3">
                                                                         <stringValue>
                                                                             <typedValue>A</typedValue>
                                                                         </stringValue>
                                                                     </keyValue>
                                                                     <keyValue domain="domainY" occur="1">
                                                                         <stringValue>
                                                                             <typedValue>B</typedValue>
                                                                         </stringValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="C" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="domainX" occur="3">
                                                                         <stringValue>
                                                                             <typedValue>A</typedValue>
                                                                         </stringValue>
                                                                     </keyValue>
                                                                     <keyValue domain="domainY" occur="1">
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
