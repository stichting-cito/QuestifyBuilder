
Imports System.Xml.Linq

<TestClass()>
Public Class GraphicalGapMatch_Cat_Scoring
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore_MR_Dich()
        Dim solution = toSolution(mr_Finding_Dich)

        Dim result = solution.MaxSolutionRawScore

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_Dichotomous()
        Dim solution = toSolution(mr_Finding_Dich)

        Dim r = toResponse(responseMR)

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_Polytomous()
        Dim solution = toSolution(mr_Finding_OneFactPoly)

        Dim r = toResponse(responseMR)

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_ResponseWithKeyValueAndConstructionPolytomous()
        Dim solution = toSolution(mr_Finding_TwoFactsPoly)

        Dim r = toResponse(responseWithKeyValueAndConstruction)

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    Private mr_Finding_Dich As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                              <keyFindings>
                                                  <keyFinding id="ggm" scoringMethod="Dichotomous">
                                                      <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <keyValue domain="domainA" occur="1">
                                                              <stringValue>
                                                                  <typedValue>B</typedValue>
                                                              </stringValue>
                                                          </keyValue>
                                                          <keyValue domain="domainA" occur="1">
                                                              <stringValue>
                                                                  <typedValue>C</typedValue>
                                                              </stringValue>
                                                          </keyValue>
                                                      </keyFact>
                                                  </keyFinding>
                                              </keyFindings>
                                              <aspectReferences/>
                                          </solution>

    Private mr_Finding_OneFactPoly As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                     <keyFindings>
                                                         <keyFinding id="ggm" scoringMethod="Polytomous">
                                                             <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <keyValue domain="domainA" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>B</typedValue>
                                                                     </stringValue>
                                                                 </keyValue>
                                                                 <keyValue domain="domainA" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>C</typedValue>
                                                                     </stringValue>
                                                                 </keyValue>
                                                             </keyFact>
                                                         </keyFinding>
                                                     </keyFindings>
                                                     <aspectReferences/>
                                                 </solution>


    Private mr_Finding_TwoFactsPoly As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                      <keyFindings>
                                                          <keyFinding id="ggm" scoringMethod="Polytomous">
                                                              <keyFactSet>
                                                                  <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="domainA" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>B</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                      <keyValue domain="domainA" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>C</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                                  <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="domainB" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>A</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                      <keyValue domain="domainB" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>D</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                              </keyFactSet>
                                                              <keyFactSet>
                                                                  <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="domainA" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>D</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                      <keyValue domain="domainA" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>C</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                                  <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="domainB" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>C</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                      <keyValue domain="domainB" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>A</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                              </keyFactSet>
                                                          </keyFinding>
                                                      </keyFindings>
                                                      <aspectReferences/>
                                                  </solution>

    Private responseMR As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                         <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                         <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                             <responseFinding id="ggm">
                                                 <responseFact id="A">
                                                     <responseValue domain="domainA" occur="1">
                                                         <stringValue>
                                                             <typedValue>B</typedValue>
                                                         </stringValue>
                                                     </responseValue>
                                                     <responseValue domain="domainA" occur="1">
                                                         <stringValue>
                                                             <typedValue>C</typedValue>
                                                         </stringValue>
                                                     </responseValue>
                                                 </responseFact>
                                             </responseFinding>
                                             <responseFinding id="audioController1"/>
                                         </responseFindings>
                                         <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                     </Response>

    Private responseWithKeyValueAndConstruction As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                                  <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                  <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <responseFinding id="ggm">
                                                                          <responseFact id="A">
                                                                              <responseValue domain="domainA" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>D</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                              <responseValue domain="domainA" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>C</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                          </responseFact>
                                                                          <responseFact id="B">
                                                                              <responseValue domain="domainB" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>A</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                              <responseValue domain="domainB" occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>C</typedValue>
                                                                                  </stringValue>
                                                                              </responseValue>
                                                                          </responseFact>
                                                                      </responseFinding>
                                                                      <responseFinding id="audioController1"/>
                                                                  </responseFindings>
                                                                  <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                              </Response>

End Class
