
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

<TestClass>
Public Class SolutionScoringInfoTests
    Inherits ScoringTestBase



    <TestMethod, TestCategory("ResponseAndScoringModel")>
    Public Sub GetMaxRawScoreFromExample_Expects_1()

        Dim solution = toSolution(NewMRSituation_AB_combined)

        Assert.AreEqual(1, solution.MaxSolutionRawScore)
    End Sub

    <TestMethod, TestCategory("ResponseAndScoringModel")>
    Public Sub GetMaxRawScoreFromExample_NoScoresSetToZero_Expects_2()

        Dim solution = toSolution(NewMRSituation_AB_combined_AllScore1)

        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, solution.MaxSolutionRawScore)
        Else
            Assert.AreEqual(1, solution.MaxSolutionRawScore)
        End If
    End Sub



    ReadOnly NewMRSituation_AB_combined As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                          <keyFindings>
                                                              <keyFinding id="mc" scoringMethod="Polytomous">

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

    ReadOnly NewMRSituation_AB_combined_AllScore1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                    <keyFindings>
                                                                        <keyFinding id="mc" scoringMethod="Polytomous">

                                                                            <keyFactSet>
                                                                                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="A-mc" occur="1">
                                                                                        <booleanValue>
                                                                                            <typedValue>true</typedValue>
                                                                                        </booleanValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                                <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="B-mc" occur="1">
                                                                                        <booleanValue>
                                                                                            <typedValue>false</typedValue>
                                                                                        </booleanValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                            </keyFactSet>

                                                                            <keyFactSet>
                                                                                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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


End Class
