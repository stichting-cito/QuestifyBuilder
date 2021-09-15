
Imports System.Linq
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class AddMissingAnswerCatagoryToMCTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetSolutionWithMissingParamForD_ExpectsFactD_ToBeCreated()
        'Arrange     
        Dim solution = solutionMissingParam.To(Of Solution)()
        Dim sp = ChoiceScoringParameterABCD()

        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringMapKey", combinedScoringMap}, {"Solution", solution}}
        WriteToDebug(solution, "Arrange")

        'Act
        WorkflowInvoker.Invoke(New AddMissingAnswerCatagoryToMC(), inputs)

        'Assert
        WriteToDebug(solution, "Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(expectedResult.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SolutionWithoutMissingInput_EqualsOutcome()
        'Arrange     
        Dim solution = expectedResult.To(Of Solution)()
        Dim sp = ChoiceScoringParameterABCD()

        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringMapKey", combinedScoringMap}, {"Solution", solution}}
        WriteToDebug(solution, "Arrange")

        'Act
        WorkflowInvoker.Invoke(New AddMissingAnswerCatagoryToMC(), inputs)

        'Assert
        WriteToDebug(solution, "Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(expectedResult.ToString(), solution.DoSerialize().ToString()))
    End Sub

#Region "Data"
    ReadOnly solutionMissingParam As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                    <keyFindings>
                                                        <keyFinding id="Integratie" scoringMethod="None">
                                                            <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="Controller" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>A</typedValue>
                                                                    </stringValue>
                                                                </keyValue>
                                                            </keyFact>
                                                        </keyFinding>
                                                    </keyFindings>
                                                    <conceptFindings>
                                                        <conceptFinding id="Integratie" scoringMethod="None">
                                                            <conceptFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="A-Controller" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>A</typedValue>
                                                                    </stringValue>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <conceptFact id="B[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="Controller" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>B</typedValue>
                                                                    </stringValue>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <conceptFact id="C[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="Controller" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>C</typedValue>
                                                                    </stringValue>
                                                                </conceptValue>
                                                            </conceptFact>
                                                        </conceptFinding>
                                                    </conceptFindings>
                                                    <aspectReferences/>
                                                </solution>

    ReadOnly expectedResult As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                              <keyFindings>
                                                  <keyFinding id="Integratie" scoringMethod="None">
                                                      <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <keyValue domain="Controller" occur="1">
                                                              <stringValue>
                                                                  <typedValue>A</typedValue>
                                                              </stringValue>
                                                          </keyValue>
                                                      </keyFact>
                                                  </keyFinding>
                                              </keyFindings>
                                              <conceptFindings>
                                                  <conceptFinding id="Integratie" scoringMethod="None">
                                                      <conceptFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <conceptValue domain="A-Controller" occur="1">
                                                              <stringValue>
                                                                  <typedValue>A</typedValue>
                                                              </stringValue>
                                                          </conceptValue>
                                                      </conceptFact>
                                                      <conceptFact id="B[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <conceptValue domain="Controller" occur="1">
                                                              <stringValue>
                                                                  <typedValue>B</typedValue>
                                                              </stringValue>
                                                          </conceptValue>
                                                      </conceptFact>
                                                      <conceptFact id="C[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <conceptValue domain="Controller" occur="1">
                                                              <stringValue>
                                                                  <typedValue>C</typedValue>
                                                              </stringValue>
                                                          </conceptValue>
                                                      </conceptFact>
                                                      <conceptFact id="D[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <conceptValue domain="Controller" occur="1">
                                                              <stringValue>
                                                                  <typedValue>D</typedValue>
                                                              </stringValue>
                                                          </conceptValue>
                                                      </conceptFact>
                                                  </conceptFinding>
                                              </conceptFindings>
                                              <aspectReferences/>
                                          </solution>
#End Region

    Private Function ChoiceScoringParameterABCD() As ChoiceScoringParameter
        Return New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Controller", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
    End Function

End Class
