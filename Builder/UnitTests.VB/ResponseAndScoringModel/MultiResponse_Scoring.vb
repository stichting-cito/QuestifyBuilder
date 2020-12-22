
Imports System.Xml.Linq
Imports FluentAssertions

<TestClass()>
Public Class MultiResponse_Scoring
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_Correct()
        GetScoreSolution(_mrFindingDichotomous2, _responseMr2).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_Incorrect()
        GetScoreSolution(_mrFindingDichotomous2, _responseMrIncorrect2).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_Partially()
        GetScoreSolution(_mrFindingDichotomous2, _responseMrPartially).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_KeyFactSets_FirstOption_Correct()
        GetScoreSolution(_mrFindingDichotomous3, _responseMr2).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_KeyFactSets_FirstOption_Correct2()
        GetScoreSolution(_mrFindingDichotomous4, _responseMr4).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_KeyFactSets_SecondOption_Correct()
        GetScoreSolution(_mrFindingDichotomous3, _responseMr3).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_KeyFactSets_FirstOption_Correct_OtherWrong()
        GetScoreSolution(_mrFindingDichotomous3, _responseMrIncorrect2).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_KeyFactSets_FirstOption_Incorrect_OtherCorrect()
        GetScoreSolution(_mrFindingDichotomous3, _responseMrIncorrect4).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Dichotomous_KeyFactSets_AllOptions_OtherCorrect()
        GetScoreSolution(_mrFindingDichotomous3, _responseMrIncorrect3).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_Correct()
        GetScoreSolution(_mrFindingPolytomous2, _responseMr2).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_Incorrect()
        GetScoreSolution(_mrFindingPolytomous2, _responseMrIncorrect5).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_Partially()
        GetScoreSolution(_mrFindingPolytomous2, _responseMrPartially).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_KeyFactSets_FirstOption_Correct()
        GetScoreSolution(_mrFindingPolytomous3, _responseMr2).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_KeyFactSets_SecondOption_Correct()
        GetScoreSolution(_mrFindingPolytomous3, _responseMr3).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_KeyFactSets_FirstOption_Correct_OtherWrong()
        GetScoreSolution(_mrFindingPolytomous3, _responseMrIncorrect2).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_KeyFactSets_FirstOption_Incorrect_OtherCorrect()
        GetScoreSolution(_mrFindingPolytomous3, _responseMrIncorrect4).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MR_Polytomous_KeyFactSets_AllOptions_OtherCorrect()
        GetScoreSolution(_mrFindingPolytomous3, _responseMrIncorrect3).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckMoreOptionsThanCorrectAnswers_Dichotomous_ShouldBe0()
        GetScoreSolution(MrFindingDichotomous, ResponseMrAllOptionsChecked).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckAllOptionsAllOptionsAreCorrect_Dichotomous_ShouldBe1()
        GetScoreSolution(MrFindingAllCorrectDichotomous, ResponseMrAllOptionsChecked).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckMoreOptionsThanCorrectAnswers_Polytomous_ShouldBe2()
        GetScoreSolution(MrFindingPolytomous, ResponseMrAllOptionsChecked).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckAllOptionsAllOptionsAreCorrect_Polytomous_ShouldBe4()
        GetScoreSolution(MrFindingAllCorrectPolytomous, ResponseMrAllOptionsChecked).Should().Be(4)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckMoreOptionsThanCorrectAnswers_Booleans_Dichotomous_ShouldBe0()
        GetScoreSolution(_mrFindingDichotomous2, _responseMrBooleansAllOptionsChecked).Should().Be(0)
    End Sub

    Public Shared ReadOnly MrFindingDichotomous As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                  <keyFindings>
                                                                      <keyFinding id="mc" scoringMethod="Dichotomous">
                                                                          <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="mc" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>B</typedValue>
                                                                                  </stringValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                          <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="mc" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>C</typedValue>
                                                                                  </stringValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFinding>
                                                                  </keyFindings>
                                                                  <aspectReferences/>
                                                              </solution>

    Public Shared ReadOnly MrFindingAllCorrectDichotomous As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                            <keyFindings>
                                                                                <keyFinding id="mc" scoringMethod="Dichotomous">
                                                                                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <keyValue domain="mc" occur="1">
                                                                                            <stringValue>
                                                                                                <typedValue>A</typedValue>
                                                                                            </stringValue>
                                                                                        </keyValue>
                                                                                    </keyFact>
                                                                                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <keyValue domain="mc" occur="1">
                                                                                            <stringValue>
                                                                                                <typedValue>B</typedValue>
                                                                                            </stringValue>
                                                                                        </keyValue>
                                                                                    </keyFact>
                                                                                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <keyValue domain="mc" occur="1">
                                                                                            <stringValue>
                                                                                                <typedValue>C</typedValue>
                                                                                            </stringValue>
                                                                                        </keyValue>
                                                                                    </keyFact>
                                                                                    <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <keyValue domain="mc" occur="1">
                                                                                            <stringValue>
                                                                                                <typedValue>D</typedValue>
                                                                                            </stringValue>
                                                                                        </keyValue>
                                                                                    </keyFact>
                                                                                </keyFinding>
                                                                            </keyFindings>
                                                                            <aspectReferences/>
                                                                        </solution>

    Public Shared ReadOnly MrFindingPolytomous As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                 <keyFindings>
                                                                     <keyFinding id="mc" scoringMethod="Polytomous">
                                                                         <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="mc" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>B</typedValue>
                                                                                 </stringValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                         <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="mc" occur="1">
                                                                                 <stringValue>
                                                                                     <typedValue>C</typedValue>
                                                                                 </stringValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                     </keyFinding>
                                                                 </keyFindings>
                                                                 <aspectReferences/>
                                                             </solution>

    Public Shared ReadOnly MrFindingAllCorrectPolytomous As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                           <keyFindings>
                                                                               <keyFinding id="mc" scoringMethod="Polytomous">
                                                                                   <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                       <keyValue domain="mc" occur="1">
                                                                                           <stringValue>
                                                                                               <typedValue>A</typedValue>
                                                                                           </stringValue>
                                                                                       </keyValue>
                                                                                   </keyFact>
                                                                                   <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                       <keyValue domain="mc" occur="1">
                                                                                           <stringValue>
                                                                                               <typedValue>B</typedValue>
                                                                                           </stringValue>
                                                                                       </keyValue>
                                                                                   </keyFact>
                                                                                   <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                       <keyValue domain="mc" occur="1">
                                                                                           <stringValue>
                                                                                               <typedValue>C</typedValue>
                                                                                           </stringValue>
                                                                                       </keyValue>
                                                                                   </keyFact>
                                                                                   <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                       <keyValue domain="mc" occur="1">
                                                                                           <stringValue>
                                                                                               <typedValue>D</typedValue>
                                                                                           </stringValue>
                                                                                       </keyValue>
                                                                                   </keyFact>
                                                                               </keyFinding>
                                                                           </keyFindings>
                                                                           <aspectReferences/>
                                                                       </solution>

    Public Shared ReadOnly ResponseMrAllOptionsChecked As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                         <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                         <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <responseFinding id="mc">
                                                                                 <responseFact id="mc">
                                                                                     <responseValue domain="mc" occur="1">
                                                                                         <stringValue>
                                                                                             <typedValue>A</typedValue>
                                                                                         </stringValue>
                                                                                     </responseValue>
                                                                                 </responseFact>
                                                                                 <responseFact id="mc">
                                                                                     <responseValue domain="mc" occur="1">
                                                                                         <stringValue>
                                                                                             <typedValue>B</typedValue>
                                                                                         </stringValue>
                                                                                     </responseValue>
                                                                                 </responseFact>
                                                                                 <responseFact id="mc">
                                                                                     <responseValue domain="mc" occur="1">
                                                                                         <stringValue>
                                                                                             <typedValue>C</typedValue>
                                                                                         </stringValue>
                                                                                     </responseValue>
                                                                                 </responseFact>
                                                                                 <responseFact id="mc">
                                                                                     <responseValue domain="mc" occur="1">
                                                                                         <stringValue>
                                                                                             <typedValue>D</typedValue>
                                                                                         </stringValue>
                                                                                     </responseValue>
                                                                                 </responseFact>
                                                                             </responseFinding>
                                                                             <responseFinding id="audioController1"/>
                                                                         </responseFindings>
                                                                         <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                     </Response>

    Private ReadOnly _mrFindingDichotomous2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                              <keyFindings>
                                                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                                                      <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="C-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>true</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="D-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>false</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="A-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>true</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="B-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>false</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                  </keyFinding>
                                                              </keyFindings>
                                                              <aspectReferences/>
                                                          </solution>

    Private ReadOnly _mrFindingDichotomous3 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                              <keyFindings>
                                                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                                                      <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="C-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>true</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="D-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>false</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFactSet>
                                                                          <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="A-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>true</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="B-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>false</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFactSet>
                                                                      <keyFactSet>
                                                                          <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="A-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>false</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="B-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>true</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFactSet>
                                                                  </keyFinding>
                                                              </keyFindings>
                                                              <aspectReferences/>
                                                          </solution>

    Private ReadOnly _mrFindingDichotomous4 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                              <keyFindings>
                                                                  <keyFinding id="mc" scoringMethod="Dichotomous">
                                                                      <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="C-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>true</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="D-mc" occur="1">
                                                                              <booleanValue>
                                                                                  <typedValue>false</typedValue>
                                                                              </booleanValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFactSet>
                                                                          <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="A-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>true</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="B-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>true</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFactSet>
                                                                      <keyFactSet>
                                                                          <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="A-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>false</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="B-mc" occur="1">
                                                                                  <booleanValue>
                                                                                      <typedValue>true</typedValue>
                                                                                  </booleanValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFactSet>
                                                                  </keyFinding>
                                                              </keyFindings>
                                                              <aspectReferences/>
                                                          </solution>

    Private ReadOnly _mrFindingPolytomous2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                             <keyFindings>
                                                                 <keyFinding id="mc" scoringMethod="Polytomous">
                                                                     <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="C-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="D-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="A-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="B-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                 </keyFinding>
                                                             </keyFindings>
                                                             <aspectReferences/>
                                                         </solution>

    Private ReadOnly _mrFindingPolytomous3 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                             <keyFindings>
                                                                 <keyFinding id="mc" scoringMethod="Polytomous">
                                                                     <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="C-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="D-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFactSet>
                                                                         <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="A-mc" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>true</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                         <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="B-mc" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>false</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                     </keyFactSet>
                                                                     <keyFactSet>
                                                                         <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="A-mc" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>false</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                         <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="B-mc" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>true</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                     </keyFactSet>
                                                                 </keyFinding>
                                                             </keyFindings>
                                                             <aspectReferences/>
                                                         </solution>

    Private ReadOnly _responseMrBooleansAllOptionsChecked As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                            <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                            <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                <responseFinding id="mc">
                                                                                    <responseFact id="A-mc">
                                                                                        <responseValue domain="A-mc" occur="1">
                                                                                            <booleanValue>
                                                                                                <typedValue>true</typedValue>
                                                                                            </booleanValue>
                                                                                        </responseValue>
                                                                                    </responseFact>
                                                                                    <responseFact id="B-mc">
                                                                                        <responseValue domain="B-mc" occur="1">
                                                                                            <booleanValue>
                                                                                                <typedValue>true</typedValue>
                                                                                            </booleanValue>
                                                                                        </responseValue>
                                                                                    </responseFact>
                                                                                    <responseFact id="C-mc">
                                                                                        <responseValue domain="C-mc" occur="1">
                                                                                            <booleanValue>
                                                                                                <typedValue>true</typedValue>
                                                                                            </booleanValue>
                                                                                        </responseValue>
                                                                                    </responseFact>
                                                                                    <responseFact id="D-mc">
                                                                                        <responseValue domain="D-mc" occur="1">
                                                                                            <booleanValue>
                                                                                                <typedValue>true</typedValue>
                                                                                            </booleanValue>
                                                                                        </responseValue>
                                                                                    </responseFact>
                                                                                </responseFinding>
                                                                                <responseFinding id="audioController1"/>
                                                                            </responseFindings>
                                                                            <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                                        </Response>

    Private ReadOnly _responseMr2 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                    <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                    <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <responseFinding id="mc">
                                                            <responseFact id="A-mc">
                                                                <responseValue domain="A-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>true</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="B-mc">
                                                                <responseValue domain="B-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="C-mc">
                                                                <responseValue domain="C-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>true</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="D-mc">
                                                                <responseValue domain="D-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                        </responseFinding>
                                                    </responseFindings>
                                                    <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                </Response>

    Private ReadOnly _responseMr3 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                    <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                    <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <responseFinding id="mc">
                                                            <responseFact id="A-mc">
                                                                <responseValue domain="A-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="B-mc">
                                                                <responseValue domain="B-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>true</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="C-mc">
                                                                <responseValue domain="C-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>true</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="D-mc">
                                                                <responseValue domain="D-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                        </responseFinding>
                                                    </responseFindings>
                                                    <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                </Response>

    Private ReadOnly _responseMr4 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                    <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                    <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <responseFinding id="mc">
                                                            <responseFact id="A-mc">
                                                                <responseValue domain="A-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>true</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="B-mc">
                                                                <responseValue domain="B-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>true</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="C-mc">
                                                                <responseValue domain="C-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>true</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                            <responseFact id="D-mc">
                                                                <responseValue domain="D-mc" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </responseValue>
                                                            </responseFact>
                                                        </responseFinding>
                                                    </responseFindings>
                                                    <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                </Response>

    Private ReadOnly _responseMrPartially As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                            <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                            <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <responseFinding id="mc">
                                                                    <responseFact id="A-mc">
                                                                        <responseValue domain="A-mc" occur="1">
                                                                            <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                            </booleanValue>
                                                                        </responseValue>
                                                                    </responseFact>
                                                                    <responseFact id="B-mc">
                                                                        <responseValue domain="B-mc" occur="1">
                                                                            <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                            </booleanValue>
                                                                        </responseValue>
                                                                    </responseFact>
                                                                    <responseFact id="C-mc">
                                                                        <responseValue domain="C-mc" occur="1">
                                                                            <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                            </booleanValue>
                                                                        </responseValue>
                                                                    </responseFact>
                                                                    <responseFact id="D-mc">
                                                                        <responseValue domain="D-mc" occur="1">
                                                                            <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                            </booleanValue>
                                                                        </responseValue>
                                                                    </responseFact>
                                                                </responseFinding>
                                                            </responseFindings>
                                                            <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                        </Response>

    Private ReadOnly _responseMrIncorrect2 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                             <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                             <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <responseFinding id="mc">
                                                                     <responseFact id="A-mc">
                                                                         <responseValue domain="A-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="B-mc">
                                                                         <responseValue domain="B-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="C-mc">
                                                                         <responseValue domain="C-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="D-mc">
                                                                         <responseValue domain="D-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                 </responseFinding>
                                                             </responseFindings>
                                                             <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                         </Response>

    Private ReadOnly _responseMrIncorrect3 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                             <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                             <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <responseFinding id="mc">
                                                                     <responseFact id="A-mc">
                                                                         <responseValue domain="A-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="B-mc">
                                                                         <responseValue domain="B-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="C-mc">
                                                                         <responseValue domain="C-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="D-mc">
                                                                         <responseValue domain="D-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                 </responseFinding>
                                                             </responseFindings>
                                                             <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                         </Response>

    Private ReadOnly _responseMrIncorrect4 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                             <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                             <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <responseFinding id="mc">
                                                                     <responseFact id="A-mc">
                                                                         <responseValue domain="A-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="B-mc">
                                                                         <responseValue domain="B-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="C-mc">
                                                                         <responseValue domain="C-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="D-mc">
                                                                         <responseValue domain="D-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                 </responseFinding>
                                                             </responseFindings>
                                                             <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                         </Response>

    Private ReadOnly _responseMrIncorrect5 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                             <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                             <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <responseFinding id="mc">
                                                                     <responseFact id="A-mc">
                                                                         <responseValue domain="A-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="B-mc">
                                                                         <responseValue domain="B-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="C-mc">
                                                                         <responseValue domain="C-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>false</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                     <responseFact id="D-mc">
                                                                         <responseValue domain="D-mc" occur="1">
                                                                             <booleanValue>
                                                                                 <typedValue>true</typedValue>
                                                                             </booleanValue>
                                                                         </responseValue>
                                                                     </responseFact>
                                                                 </responseFinding>
                                                             </responseFindings>
                                                             <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                         </Response>

End Class
