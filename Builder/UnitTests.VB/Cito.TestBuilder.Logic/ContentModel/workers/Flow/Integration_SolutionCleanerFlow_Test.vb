
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports System.Activities
Imports Questify.Builder.Logic
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Integration_SolutionCleanerFlow_Test

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestOneInteractionRemoved_SolutionIsCleaned()
        Dim solution = _testSolution1.To(Of Solution)()
        Dim sp = New DecimalScoringParameter() With {.ControllerId = "gapController",
                                                     .FindingOverride = "gapController",
                                                     .InlineId = "I9594b772-0cbc-4e67-a957-bf03ea76559f"}.AddSubParameters("1")

        Dim inputs As New Dictionary(Of String, Object) From {{"Solution", solution}, {"ScoringParameters", New ScoringParameter() {sp}}}

        WorkflowInvoker.Invoke(New SolutionCleaner(), inputs)

        solution.WriteToDebug("Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(_result1.ToString(), solution.DoSerialize().ToString()))
    End Sub

    ReadOnly _testSolution1 As XElement =
          <solution>
              <keyFindings>
                  <keyFinding id="gapController" scoringMethod="Dichotomous">
                      <keyFactSet>
                          <keyFact id="1-I7cea3261-917d-478e-8906-7e17733b57bb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <keyValue domain="I7cea3261-917d-478e-8906-7e17733b57bb" occur="1">
                                  <decimalValue>
                                      <typedValue>2.2</typedValue>
                                  </decimalValue>
                              </keyValue>
                          </keyFact>
                          <keyFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <keyValue domain="I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                  <decimalValue>
                                      <typedValue>1.1</typedValue>
                                  </decimalValue>
                              </keyValue>
                          </keyFact>
                      </keyFactSet>
                      <keyFactSet>
                          <keyFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <keyValue domain="I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                  <decimalValue>
                                      <typedValue>2.2</typedValue>
                                  </decimalValue>
                              </keyValue>
                          </keyFact>
                          <keyFact id="1-I7cea3261-917d-478e-8906-7e17733b57bb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <keyValue domain="I7cea3261-917d-478e-8906-7e17733b57bb" occur="1">
                                  <decimalValue>
                                      <typedValue>1.1</typedValue>
                                  </decimalValue>
                              </keyValue>
                          </keyFact>
                      </keyFactSet>
                  </keyFinding>
              </keyFindings>
              <conceptFindings>
                  <conceptFinding id="gapController" scoringMethod="None">
                      <conceptFactSet>
                          <conceptFact id="1-I7cea3261-917d-478e-8906-7e17733b57bb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <conceptValue domain="1-I7cea3261-917d-478e-8906-7e17733b57bb" occur="1">
                                  <decimalValue>
                                      <typedValue>2.2</typedValue>
                                  </decimalValue>
                              </conceptValue>
                          </conceptFact>
                          <conceptFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <conceptValue domain="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                  <decimalValue>
                                      <typedValue>1.1</typedValue>
                                  </decimalValue>
                              </conceptValue>
                          </conceptFact>
                          <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                      </conceptFactSet>
                      <conceptFactSet>
                          <conceptFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <conceptValue domain="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                  <decimalValue>
                                      <typedValue>2.2</typedValue>
                                  </decimalValue>
                              </conceptValue>
                          </conceptFact>
                          <conceptFact id="1-I7cea3261-917d-478e-8906-7e17733b57bb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <conceptValue domain="1-I7cea3261-917d-478e-8906-7e17733b57bb" occur="1">
                                  <decimalValue>
                                      <typedValue>1.1</typedValue>
                                  </decimalValue>
                              </conceptValue>
                          </conceptFact>
                          <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                      </conceptFactSet>
                      <conceptFactSet>
                          <conceptFact id="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <conceptValue domain="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                  <catchAllValue/>
                              </conceptValue>
                          </conceptFact>
                          <conceptFact id="1[*]-I7cea3261-917d-478e-8906-7e17733b57bb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                              <conceptValue domain="1[*]-I7cea3261-917d-478e-8906-7e17733b57bb" occur="1">
                                  <catchAllValue/>
                              </conceptValue>
                          </conceptFact>
                          <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                      </conceptFactSet>
                  </conceptFinding>
              </conceptFindings>
              <aspectReferences/>
              <ItemScoreTranslationTable>
                  <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                  <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
              </ItemScoreTranslationTable>
          </solution>

    ReadOnly _result1 As XElement =
          <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
              <keyFindings>
                  <keyFinding id="gapController" scoringMethod="Dichotomous">
                      <keyFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                              <decimalValue>
                                  <typedValue>1.1</typedValue>
                              </decimalValue>
                              <decimalValue>
                                  <typedValue>2.2</typedValue>
                              </decimalValue>
                          </keyValue>
                      </keyFact>
                  </keyFinding>
              </keyFindings>
              <conceptFindings>
                  <conceptFinding id="gapController" scoringMethod="None">
                      <conceptFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <conceptValue domain="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                              <decimalValue>
                                  <typedValue>1.1</typedValue>
                              </decimalValue>
                              <decimalValue>
                                  <typedValue>2.2</typedValue>
                              </decimalValue>
                          </conceptValue>
                      </conceptFact>
                      <conceptFact id="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <conceptValue domain="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                              <catchAllValue/>
                          </conceptValue>
                      </conceptFact>
                  </conceptFinding>
              </conceptFindings>
              <aspectReferences/>
              <ItemScoreTranslationTable>
                  <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                  <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
              </ItemScoreTranslationTable>
          </solution>

End Class
