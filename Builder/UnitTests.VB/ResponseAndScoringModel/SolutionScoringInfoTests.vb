
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

''' <summary>
''' These test exist to discover old behavior.
''' </summary>
''' <remarks></remarks>
<TestClass>
Public Class SolutionScoringInfoTests
    Inherits ScoringTestBase

#Region "New MR tests"

    'Since MR is saving correct and incorrect answers, we want to validate certain fields. 
    'Please note that where false is the correct answer, the score = 0.

    <TestMethod, TestCategory("ResponseAndScoringModel")>
    Public Sub GetMaxRawScoreFromExample_Expects_1()
        'Arrange

        'Act
        Dim solution = toSolution(NewMRSituation_AB_combined)

        'Assert
        Assert.AreEqual(1, solution.MaxSolutionRawScore)
    End Sub

    <TestMethod, TestCategory("ResponseAndScoringModel")>
    Public Sub GetMaxRawScoreFromExample_NoScoresSetToZero_Expects_2()
        'Arrange

        'Act
        Dim solution = toSolution(NewMRSituation_AB_combined_AllScore1)

        'Assert
        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(2, solution.MaxSolutionRawScore)
        Else
            Assert.AreEqual(1, solution.MaxSolutionRawScore)
        End If
    End Sub

#End Region

#Region "Data"

    ' A = true              A = false
    '               or
    ' B = false             B = true      
    ' -------------------------------------------------
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

    ' A = true              A = false
    '               or
    ' B = false             B = true      
    ' -------------------------------------------------
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

#End Region

End Class
