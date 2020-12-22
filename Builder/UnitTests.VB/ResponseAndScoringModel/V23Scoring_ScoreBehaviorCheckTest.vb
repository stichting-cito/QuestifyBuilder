
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

<TestClass>
<ScoringMethod(ScoringFactory.Methods.V23_Scoring)>
Public Class V23Scoring_ScoreBehaviorCheckTest
    Inherits ScoringTestBase

    <TestMethod, TestCategory("ResponseAndScoringModel")>
    <Description("This test proofes that v2.3 scoring treats a factset when partial correctly answerd that the outcome is 1. Polytomous scoring.")>
    Public Sub ScoreFactSets_ANotB_or_NotAB_With_AB_Expects_1()
        Dim solution = toSolution(_2factSetsAB)
        Dim response = toResponse(incorrectResponse_2factSetsAB)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod, TestCategory("ResponseAndScoringModel")>
    <Description("This test proofes that v2.3 scoring treats a factset when  correctly answerd that the outcome is 2. Polytomous scoring.")>
    Public Sub ScoreFactSets_ANotB_or_NotAB_With_ANotB_Expects_2()
        Dim solution = toSolution(_2factSetsAB)
        Dim response = toResponse(response_2factSetsANotB)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(2, result)
    End Sub


    ReadOnly _2factSetsAB As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
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

    Private incorrectResponse_2factSetsAB As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
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
                                                                </responseFinding>
                                                            </responseFindings>
                                                            <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                        </Response>

    Private response_2factSetsANotB As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
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
                                                          </responseFinding>
                                                      </responseFindings>
                                                      <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                  </Response>


End Class
