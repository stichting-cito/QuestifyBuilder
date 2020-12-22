
Imports System.Xml.Linq

<TestClass>
Public Class BugTests
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_Test()
        Dim solution = toSolution(sol1)
        Dim response = toResponse(resp1)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(1, result, "Score should be 1")
    End Sub

    Private sol1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                   <keyFindings>
                                       <keyFinding id="mc" scoringMethod="Dichotomous">
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

    Private resp1 As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                    <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                    <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization"><responseFinding xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" id="mc">
                                        <responseFact id="mc" xmlns="http://Cito.Tester.Server/xml/serialization">
                                            <responseValue domain="mc" occur="1">
                                                <stringValue>
                                                    <typedValue>D</typedValue>
                                                </stringValue>
                                            </responseValue>
                                        </responseFact>
                                        </responseFinding>
                                    </responseFindings>
                                    <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                </Response>

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub ScoreSolution_Dichotoom()
        Dim solution = ToSolution(sol2)
        Dim response = ToResponse(resp2)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(0, result, "Score should be 0")
    End Sub

    Public sol2 As XElement = <solution>
                                  <keyFindings>
                                      <keyFinding id="mc" scoringMethod="Dichotomous">
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

    Public resp2 As XElement = <Response active="true" sessionId="6638e887-f41e-42cd-a1d8-a96da908739c" id="00103" responseNr="4" translatedScore="1" rawScore="1" navigatedToIndex="4">
                                   <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                   <ApplicationId xmlns="http://Cito.Tester.Server/xml/serialization">LAMARK</ApplicationId>
                                   <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                       <responseFinding id="mc">
                                           <responseFact id="mc">
                                               <responseValue domain="mc" occur="1">
                                                   <stringValue>
                                                       <typedValue>X</typedValue>
                                                   </stringValue>
                                               </responseValue>
                                           </responseFact>
                                       </responseFinding>
                                   </responseFindings>
                                   <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">3</ItemIndexInTest>
                               </Response>

End Class
