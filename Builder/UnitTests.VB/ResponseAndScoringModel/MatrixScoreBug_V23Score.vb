
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring
Imports FluentAssertions

<TestClass>
<ScoringMethod(ScoringFactory.Methods.V23_Scoring)>
Public Class MatrixScoreBug_V23Score
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWithCorrectResponse_ExpectsScore1()
        GetScoreSolution(RecordedSolution,CorrectResponse).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub Score_WithFactSets_WithCorrectResponse_ExpectsScore1()
        Dim expected as Integer = 1
        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            expected = 0
        End If
        GetScoreSolution(_recordedSolutionWithFactSets,_correctResponseWithFactSets).Should().Be(expected)        
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_FirstAnswerWrong_ExpectsScore0()
        GetScoreSolution(RecordedSolution,IncorrectResponseFirstAnswerWrong).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_SecondAnswerWrong_ExpectsScore0()
        GetScoreSolution(RecordedSolution,IncorrectResponseSecondAnswerWrong).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreWith__InCorrectResponse_ThirdAnswerWrong_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseThirdAnswerWrong).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_FirstTwoAnswersWrong_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseFirstTwoAnswersWrong).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_LastTwoAnswersWrong_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseLastTwoAnswersWrong).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_FirstAndLastAnswerWrong_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseFirstAndLastAnswerWrong).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_FirstAnswerMissing_LastTwoCorrect_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseFirstAnswerMissingLastTwoCorrect).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_AllAnswersMissing_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseLastAnswerMissingAllAnswersMissing).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_FirstIncorrect_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseLastAnswerMissingFirstIncorrect).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_FirstTwoCorrect_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseLastAnswerMissingFirstTwoCorrect).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> 
    Public Sub ScoreWith__InCorrectResponse_LastAnswerMissing_SecondIncorrect_ExpectsScore0()
        GetScoreSolution(RecordedSolution, IncorrectResponseLastAnswerMissingSecondIncorrect).Should().Be(0)
    End Sub

    'A is the correct answer on all questions.
    Public Shared ReadOnly RecordedSolution As XElement = <solution>
                                               <keyFindings>
                                                   <keyFinding id="matrix" scoringMethod="Dichotomous">
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

    'A is the correct answer on all questions.
    Private ReadOnly _recordedSolutionWithFactSets As XElement = <solution>
                                                            <keyFindings>
                                                                <keyFinding id="matrix" scoringMethod="Dichotomous">
                                                                    <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="matrix4" occur="1">
                                                                            <stringValue>
                                                                                <typedValue>A</typedValue>
                                                                            </stringValue>
                                                                        </keyValue>
                                                                    </keyFact>
                                                                    <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="matrix3" occur="1">
                                                                            <stringValue>
                                                                                <typedValue>A</typedValue>
                                                                            </stringValue>
                                                                        </keyValue>
                                                                    </keyFact>
                                                                    <keyFactSet>
                                                                        <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="matrix1" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>A</typedValue>
                                                                                </stringValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                        <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="matrix2" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>A</typedValue>
                                                                                </stringValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFactSet>
                                                                    <keyFactSet>
                                                                        <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="matrix1" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>B</typedValue>
                                                                                </stringValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                        <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="matrix2" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>B</typedValue>
                                                                                </stringValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFactSet>
                                                                </keyFinding>
                                                            </keyFindings>
                                                            <aspectReferences/>
                                                            <ItemScoreTranslationTable>
                                                                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                            </ItemScoreTranslationTable>
                                                        </solution>

    'A is the (correct) answer on all questions.
    Public Shared ReadOnly CorrectResponse As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                              <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                              <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                  <responseFinding id="matrix">
                                                      <responseFact id="matrix">
                                                          <responseValue domain="domainX" occur="1">
                                                              <stringValue>
                                                                  <typedValue>A</typedValue>
                                                              </stringValue>
                                                          </responseValue>
                                                          <responseValue domain="domainY" occur="1">
                                                              <stringValue>
                                                                  <typedValue>A</typedValue>
                                                              </stringValue>
                                                          </responseValue>
                                                      </responseFact>
                                                      <responseFact id="matrix">
                                                          <responseValue domain="domainX" occur="1">
                                                              <stringValue>
                                                                  <typedValue>A</typedValue>
                                                              </stringValue>
                                                          </responseValue>
                                                          <responseValue domain="domainY" occur="1">
                                                              <stringValue>
                                                                  <typedValue>B</typedValue>
                                                              </stringValue>
                                                          </responseValue>
                                                      </responseFact>
                                                      <responseFact id="matrix">
                                                          <responseValue domain="domainX" occur="1">
                                                              <stringValue>
                                                                  <typedValue>A</typedValue>
                                                              </stringValue>
                                                          </responseValue>
                                                          <responseValue domain="domainY" occur="1">
                                                              <stringValue>
                                                                  <typedValue>C</typedValue>
                                                              </stringValue>
                                                          </responseValue>
                                                      </responseFact>
                                                  </responseFinding>
                                              </responseFindings>
                                              <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                          </Response>

    'A is the (correct) answer on all questions.
    Private ReadOnly _correctResponseWithFactSets As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                           <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                           <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <responseFinding id="matrix">
                                                                   <responseFact id="1-matrix">
                                                                       <responseValue domain="matrix1" occur="1">
                                                                           <stringValue>
                                                                               <typedValue>A</typedValue>
                                                                           </stringValue>
                                                                       </responseValue>
                                                                   </responseFact>
                                                                   <responseFact id="2-matrix">
                                                                       <responseValue domain="matrix2" occur="1">
                                                                           <stringValue>
                                                                               <typedValue>A</typedValue>
                                                                           </stringValue>
                                                                       </responseValue>
                                                                   </responseFact>
                                                                   <responseFact id="3-matrix">
                                                                       <responseValue domain="matrix3" occur="1">
                                                                           <stringValue>
                                                                               <typedValue>A</typedValue>
                                                                           </stringValue>
                                                                       </responseValue>
                                                                   </responseFact>
                                                                   <responseFact id="4-matrix">
                                                                       <responseValue domain="matrix4" occur="1">
                                                                           <stringValue>
                                                                               <typedValue>A</typedValue>
                                                                           </stringValue>
                                                                       </responseValue>
                                                                   </responseFact>
                                                               </responseFinding>
                                                           </responseFindings>
                                                           <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                       </Response>

    'C is the answer on the first question, A on the second and A on the last question.
    Public Shared ReadOnly IncorrectResponseFirstAnswerWrong As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                 <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                 <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <responseFinding id="matrix">
                                                                         <responseFact id="matrix">
                                                                             <responseValue domain="domainX" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>C</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                             <responseValue domain="domainY" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>A</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                         </responseFact>
                                                                         <responseFact id="matrix">
                                                                             <responseValue domain="domainX" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>A</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                             <responseValue domain="domainY" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>B</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                         </responseFact>
                                                                         <responseFact id="matrix">
                                                                             <responseValue domain="domainX" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>A</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                             <responseValue domain="domainY" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>C</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                         </responseFact>
                                                                     </responseFinding>
                                                                 </responseFindings>
                                                                 <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                             </Response>

    'A is the answer on the first question, B on the second and A on the last question.
    Public Shared ReadOnly IncorrectResponseSecondAnswerWrong As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                  <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                  <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <responseFinding id="matrix">
                                                                          <responseFact id="matrix">
                                                                              <responseValue domain="domainX" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>A</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                              <responseValue domain="domainY" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>A</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                          </responseFact>
                                                                          <responseFact id="matrix">
                                                                              <responseValue domain="domainX" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>B</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                              <responseValue domain="domainY" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>B</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                          </responseFact>
                                                                          <responseFact id="matrix">
                                                                              <responseValue domain="domainX" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>A</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                              <responseValue domain="domainY" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>C</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                          </responseFact>
                                                                      </responseFinding>
                                                                  </responseFindings>
                                                                  <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                              </Response>

    'A is the answer on the first question, A on the second and C on the last question.
    Public Shared ReadOnly IncorrectResponseThirdAnswerWrong As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                 <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                 <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <responseFinding id="matrix">
                                                                         <responseFact id="matrix">
                                                                             <responseValue domain="domainX" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>A</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                             <responseValue domain="domainY" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>A</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                         </responseFact>
                                                                         <responseFact id="matrix">
                                                                             <responseValue domain="domainX" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>A</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                             <responseValue domain="domainY" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>B</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                         </responseFact>
                                                                         <responseFact id="matrix">
                                                                             <responseValue domain="domainX" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>C</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                             <responseValue domain="domainY" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>C</typedValue>
                                                                                 </stringValue>
                                                                             </responseValue>
                                                                         </responseFact>
                                                                     </responseFinding>
                                                                 </responseFindings>
                                                                 <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                             </Response>

    'Matrix of 3 by 3 where C is the answer on the first question, B on the second and A on the last question.
    Public Shared ReadOnly IncorrectResponseFirstTwoAnswersWrong As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                     <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                     <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <responseFinding id="matrix">
                                                                             <responseFact id="matrix">
                                                                                 <responseValue domain="domainX" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>C</typedValue>
                                                                                     </stringValue>
                                                                                 </responseValue>
                                                                                 <responseValue domain="domainY" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>A</typedValue>
                                                                                     </stringValue>
                                                                                 </responseValue>
                                                                             </responseFact>
                                                                             <responseFact id="matrix">
                                                                                 <responseValue domain="domainX" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>B</typedValue>
                                                                                     </stringValue>
                                                                                 </responseValue>
                                                                                 <responseValue domain="domainY" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>B</typedValue>
                                                                                     </stringValue>
                                                                                 </responseValue>
                                                                             </responseFact>
                                                                             <responseFact id="matrix">
                                                                                 <responseValue domain="domainX" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>A</typedValue>
                                                                                     </stringValue>
                                                                                 </responseValue>
                                                                                 <responseValue domain="domainY" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>C</typedValue>
                                                                                     </stringValue>
                                                                                 </responseValue>
                                                                             </responseFact>
                                                                         </responseFinding>
                                                                     </responseFindings>
                                                                     <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                 </Response>

    'Matrix of 3 by 3 where A is the answer on the first question, C on the second and B on the last question.
    Public Shared ReadOnly IncorrectResponseLastTwoAnswersWrong As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                    <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                    <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <responseFinding id="matrix">
                                                                            <responseFact id="matrix">
                                                                                <responseValue domain="domainX" occur="1">
                                                                                    <stringValue>
                                                                                        <typedValue>A</typedValue>
                                                                                    </stringValue>
                                                                                </responseValue>
                                                                                <responseValue domain="domainY" occur="1">
                                                                                    <stringValue>
                                                                                        <typedValue>A</typedValue>
                                                                                    </stringValue>
                                                                                </responseValue>
                                                                            </responseFact>
                                                                            <responseFact id="matrix">
                                                                                <responseValue domain="domainX" occur="1">
                                                                                    <stringValue>
                                                                                        <typedValue>C</typedValue>
                                                                                    </stringValue>
                                                                                </responseValue>
                                                                                <responseValue domain="domainY" occur="1">
                                                                                    <stringValue>
                                                                                        <typedValue>B</typedValue>
                                                                                    </stringValue>
                                                                                </responseValue>
                                                                            </responseFact>
                                                                            <responseFact id="matrix">
                                                                                <responseValue domain="domainX" occur="1">
                                                                                    <stringValue>
                                                                                        <typedValue>B</typedValue>
                                                                                    </stringValue>
                                                                                </responseValue>
                                                                                <responseValue domain="domainY" occur="1">
                                                                                    <stringValue>
                                                                                        <typedValue>C</typedValue>
                                                                                    </stringValue>
                                                                                </responseValue>
                                                                            </responseFact>
                                                                        </responseFinding>
                                                                    </responseFindings>
                                                                    <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                </Response>

    'Matrix of 3 by 3 where C is the answer on the first question, A on the second and B on the last question.
    Public Shared ReadOnly IncorrectResponseFirstAndLastAnswerWrong As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                        <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                        <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <responseFinding id="matrix">
                                                                                <responseFact id="matrix">
                                                                                    <responseValue domain="domainX" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>C</typedValue>
                                                                                        </stringValue>
                                                                                    </responseValue>
                                                                                    <responseValue domain="domainY" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>A</typedValue>
                                                                                        </stringValue>
                                                                                    </responseValue>
                                                                                </responseFact>
                                                                                <responseFact id="matrix">
                                                                                    <responseValue domain="domainX" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>A</typedValue>
                                                                                        </stringValue>
                                                                                    </responseValue>
                                                                                    <responseValue domain="domainY" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>B</typedValue>
                                                                                        </stringValue>
                                                                                    </responseValue>
                                                                                </responseFact>
                                                                                <responseFact id="matrix">
                                                                                    <responseValue domain="domainX" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>B</typedValue>
                                                                                        </stringValue>
                                                                                    </responseValue>
                                                                                    <responseValue domain="domainY" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>C</typedValue>
                                                                                        </stringValue>
                                                                                    </responseValue>
                                                                                </responseFact>
                                                                            </responseFinding>
                                                                        </responseFindings>
                                                                        <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                    </Response>

    'The answer on the first question is missing, A is the answer on the second question and A on the last.
    Public Shared ReadOnly IncorrectResponseFirstAnswerMissingLastTwoCorrect As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                                  <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                  <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <responseFinding id="matrix">
                                                                                          <responseFact id="matrix">
                                                                                              <responseValue domain="domainX" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>A</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                              <responseValue domain="domainY" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>B</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                          </responseFact>
                                                                                          <responseFact id="matrix">
                                                                                              <responseValue domain="domainX" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>A</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                              <responseValue domain="domainY" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>C</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                          </responseFact>
                                                                                      </responseFinding>
                                                                                  </responseFindings>
                                                                                  <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                              </Response>

    'The answer on the last question is missing, A is the answer on the second question and A on the first.
    Public Shared ReadOnly IncorrectResponseLastAnswerMissingFirstTwoCorrect As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                                  <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                  <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <responseFinding id="matrix">
                                                                                          <responseFact id="matrix">
                                                                                              <responseValue domain="domainX" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>A</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                              <responseValue domain="domainY" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>A</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                          </responseFact>
                                                                                          <responseFact id="matrix">
                                                                                              <responseValue domain="domainX" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>A</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                              <responseValue domain="domainY" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>B</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                          </responseFact>
                                                                                      </responseFinding>
                                                                                  </responseFindings>
                                                                                  <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                              </Response>

    'The answer on the last question is missing, A is the answer on the second question and B on the first.
    Public Shared ReadOnly IncorrectResponseLastAnswerMissingFirstIncorrect As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                                 <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                 <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                     <responseFinding id="matrix">
                                                                                         <responseFact id="matrix">
                                                                                             <responseValue domain="domainX" occur="1">
                                                                                                 <stringValue>
                                                                                                     <typedValue>B</typedValue>
                                                                                                 </stringValue>
                                                                                             </responseValue>
                                                                                             <responseValue domain="domainY" occur="1">
                                                                                                 <stringValue>
                                                                                                     <typedValue>A</typedValue>
                                                                                                 </stringValue>
                                                                                             </responseValue>
                                                                                         </responseFact>
                                                                                         <responseFact id="matrix">
                                                                                             <responseValue domain="domainX" occur="1">
                                                                                                 <stringValue>
                                                                                                     <typedValue>A</typedValue>
                                                                                                 </stringValue>
                                                                                             </responseValue>
                                                                                             <responseValue domain="domainY" occur="1">
                                                                                                 <stringValue>
                                                                                                     <typedValue>B</typedValue>
                                                                                                 </stringValue>
                                                                                             </responseValue>
                                                                                         </responseFact>
                                                                                     </responseFinding>
                                                                                 </responseFindings>
                                                                                 <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                             </Response>

    'The answer on the last question is missing, B is the answer on the second question and A on the first.
    Public Shared ReadOnly IncorrectResponseLastAnswerMissingSecondIncorrect As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                                  <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                  <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <responseFinding id="matrix">
                                                                                          <responseFact id="matrix">
                                                                                              <responseValue domain="domainX" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>A</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                              <responseValue domain="domainY" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>A</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                          </responseFact>
                                                                                          <responseFact id="matrix">
                                                                                              <responseValue domain="domainX" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>B</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                              <responseValue domain="domainY" occur="1">
                                                                                                  <stringValue>
                                                                                                      <typedValue>B</typedValue>
                                                                                                  </stringValue>
                                                                                              </responseValue>
                                                                                          </responseFact>
                                                                                      </responseFinding>
                                                                                  </responseFindings>
                                                                                  <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                              </Response>

    'No answer at all.
    Public Shared ReadOnly IncorrectResponseLastAnswerMissingAllAnswersMissing As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                                    <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                    <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <responseFinding id="matrix">
                                                                                        </responseFinding>
                                                                                    </responseFindings>
                                                                                    <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                                </Response>

End Class
