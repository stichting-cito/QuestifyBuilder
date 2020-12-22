
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class MoveSingleFactsFromFactSetsToFindingTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestThatASingleIntegerValueIsMoved()
        Dim solution = _testSolution1.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.ControllerId = "Some_InlineId", .FindingOverride = "TestFindingId"}.AddSubParameters("A", "B", "C", "D")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})

        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).KeyFactsets(0).Facts}, {"FindingToMoveTo", solution.Findings(0)}, {"FactIdsToScoringParameter", result}, {"ScoringMap", New ScoringMap(New ScoringParameter() {sp}, solution).GetMap()}}

        WorkflowInvoker.Invoke(New MoveSingleFactsFromFactSetsToFinding(), inputs)

        solution.WriteToDebug("Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(solution.DoSerialize().ToString(), _result1.ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestThatADoubleIntegerValueIsNotMoved()
        Dim solution = _testSolution2.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.ControllerId = "Some_InlineId", .FindingOverride = "TestFindingId"}.AddSubParameters("A", "B", "C", "D")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})

        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).KeyFactsets(0).Facts}, {"FindingToMoveTo", solution.Findings(0)}, {"FactIdsToScoringParameter", result}, {"ScoringMap", New ScoringMap(New ScoringParameter() {sp}, solution).GetMap()}}

        WorkflowInvoker.Invoke(New MoveSingleFactsFromFactSetsToFinding(), inputs)

        solution.WriteToDebug("Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(solution.DoSerialize().ToString(), _testSolution2.ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestThatAMcValueIsMoved()
        Dim solution = _testSolution3.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.ControllerId = "Some_InlineId", .FindingOverride = "TestFindingId", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})

        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).KeyFactsets(0).Facts}, {"FindingToMoveTo", solution.Findings(0)}, {"FactIdsToScoringParameter", result}, {"ScoringMap", New ScoringMap(New ScoringParameter() {sp}, solution).GetMap()}}

        WorkflowInvoker.Invoke(New MoveSingleFactsFromFactSetsToFinding(), inputs)

        solution.WriteToDebug("Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(solution.DoSerialize().ToString(), _result3.ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestThatSingleFactInFactSetIsRemovedWhenItAlsoExistInAGroup()
        Dim solution = _testSolution4.To(Of Solution)()
        Dim sp1 = New TimeScoringParameter() With {.InlineId = "I8026e8ea-9736-42a8-958a-93a2b2e8e596", .ControllerId = "gapController", .FindingOverride = "gapController"}.AddSubParameters("1")
        Dim sp2 = New TimeScoringParameter() With {.InlineId = "I83acb713-3769-49e6-96ab-1b1c09748cf7", .ControllerId = "gapController", .FindingOverride = "gapController"}.AddSubParameters("1")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp1, sp2}}})

        Dim inputs As New Dictionary(Of String, Object) From {
            {"BaseFacts", solution.Findings(0).KeyFactsets(1).Facts},
            {"FindingToMoveTo", solution.Findings(0)},
            {"FactIdsToScoringParameter", result},
            {"ScoringMap", New ScoringMap(New ScoringParameter() {sp1, sp2}, solution).GetMap()}
        }

        WorkflowInvoker.Invoke(New MoveSingleFactsFromFactSetsToFinding(), inputs)

        solution.WriteToDebug("Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(_result4.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestThatCatchAllValueConceptFactInFactSetIsMovedToFinding()
        Dim solution = _testSolution5.To(Of Solution)()

        Dim sp = New DecimalScoringParameter() With {.ControllerId = "gapController",
                                                     .FindingOverride = "gapController",
                                                     .InlineId = "I9594b772-0cbc-4e67-a957-bf03ea76559f"}.AddSubParameters("1")

        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})

        Dim inputs As New Dictionary(Of String, Object) From {
            {"BaseFacts", solution.ConceptFindings(0).KeyFactsets(0).Facts},
            {"FindingToMoveTo", solution.ConceptFindings(0)},
            {"FactIdsToScoringParameter", result},
            {"ScoringMap", New ScoringMap(New ScoringParameter() {sp}, solution).GetMap()}
        }

        WorkflowInvoker.Invoke(New MoveSingleFactsFromFactSetsToFinding(), inputs)

        solution.WriteToDebug("Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(_result5.ToString(), solution.DoSerialize().ToString()))
    End Sub


    ReadOnly _testSolution1 As XElement = <solution>
                                              <keyFindings>
                                                  <keyFinding id="shared_finding" scoringMethod="None">

                                                      <keyFactSet>
                                                          <keyFact id="A-Some_InlineId" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="Some_InlineId" occur="1">
                                                                  <integerValue>
                                                                      <typedValue>42</typedValue>
                                                                  </integerValue>
                                                              </keyValue>
                                                          </keyFact>
                                                      </keyFactSet>

                                                  </keyFinding>
                                              </keyFindings>
                                          </solution>

    ReadOnly _result1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                        <keyFindings>
                                            <keyFinding id="shared_finding" scoringMethod="None">
                                                <keyFact id="A-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="Some_InlineId" occur="1">
                                                        <integerValue>
                                                            <typedValue>42</typedValue>
                                                        </integerValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFactSet/>
                                            </keyFinding>
                                        </keyFindings>
                                        <aspectReferences/>
                                    </solution>

    ReadOnly _testSolution2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                              <keyFindings>
                                                  <keyFinding id="shared_finding" scoringMethod="None">
                                                      <keyFactSet>
                                                          <keyFact id="A-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="Some_InlineId" occur="1">
                                                                  <integerValue>
                                                                      <typedValue>42</typedValue>
                                                                  </integerValue>
                                                              </keyValue>
                                                          </keyFact>
                                                          <keyFact id="B-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="Some_InlineId" occur="1">
                                                                  <integerValue>
                                                                      <typedValue>42</typedValue>
                                                                  </integerValue>
                                                              </keyValue>
                                                          </keyFact>
                                                      </keyFactSet>
                                                  </keyFinding>
                                              </keyFindings>
                                              <aspectReferences/>
                                          </solution>


    ReadOnly _testSolution3 As XElement = <solution>
                                              <keyFindings>
                                                  <keyFinding id="shared_finding" scoringMethod="None">
                                                      <keyFact id="A-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <keyValue domain="Some_InlineId" occur="1">
                                                              <stringValue>
                                                                  <typedValue>Original</typedValue>
                                                              </stringValue>
                                                          </keyValue>
                                                      </keyFact>
                                                      <keyFactSet>
                                                          <keyFact id="A-Some_InlineId" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="Some_InlineId" occur="1">
                                                                  <stringValue>
                                                                      <typedValue>InSet</typedValue>
                                                                  </stringValue>
                                                              </keyValue>
                                                          </keyFact>
                                                      </keyFactSet>
                                                  </keyFinding>
                                              </keyFindings>
                                          </solution>

    ReadOnly _result3 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                        <keyFindings>
                                            <keyFinding id="shared_finding" scoringMethod="None">
                                                <keyFact id="A-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="Some_InlineId" occur="1">
                                                        <stringValue>
                                                            <typedValue>Original</typedValue>
                                                        </stringValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFactSet/>
                                            </keyFinding>
                                        </keyFindings>
                                        <aspectReferences/>
                                    </solution>

    ReadOnly _testSolution4 As XElement = <solution>
                                              <keyFindings>
                                                  <keyFinding id="gapController" scoringMethod="Dichotomous">
                                                      <keyFactSet>
                                                          <keyFact id="1-I83acb713-3769-49e6-96ab-1b1c09748cf7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="I83acb713-3769-49e6-96ab-1b1c09748cf7" occur="1">
                                                                  <stringValue>
                                                                      <typedValue>12:45</typedValue>
                                                                  </stringValue>
                                                              </keyValue>
                                                          </keyFact>
                                                          <keyFact id="1-I8026e8ea-9736-42a8-958a-93a2b2e8e596" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="I8026e8ea-9736-42a8-958a-93a2b2e8e596" occur="1">
                                                                  <stringValue>
                                                                      <typedValue>11:00</typedValue>
                                                                  </stringValue>
                                                              </keyValue>
                                                          </keyFact>
                                                      </keyFactSet>
                                                      <keyFactSet>
                                                          <keyFact id="1-I8026e8ea-9736-42a8-958a-93a2b2e8e596" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="I8026e8ea-9736-42a8-958a-93a2b2e8e596" occur="1">
                                                                  <stringValue>
                                                                      <typedValue>11:56</typedValue>
                                                                  </stringValue>
                                                              </keyValue>
                                                          </keyFact>
                                                      </keyFactSet>
                                                  </keyFinding>
                                              </keyFindings>
                                              <aspectReferences/>
                                          </solution>

    ReadOnly _result4 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                        <keyFindings>
                                            <keyFinding id="gapController" scoringMethod="Dichotomous">
                                                <keyFactSet>
                                                    <keyFact id="1-I83acb713-3769-49e6-96ab-1b1c09748cf7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <keyValue domain="I83acb713-3769-49e6-96ab-1b1c09748cf7" occur="1">
                                                            <stringValue>
                                                                <typedValue>12:45</typedValue>
                                                            </stringValue>
                                                        </keyValue>
                                                    </keyFact>
                                                    <keyFact id="1-I8026e8ea-9736-42a8-958a-93a2b2e8e596" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <keyValue domain="I8026e8ea-9736-42a8-958a-93a2b2e8e596" occur="1">
                                                            <stringValue>
                                                                <typedValue>11:00</typedValue>
                                                            </stringValue>
                                                        </keyValue>
                                                    </keyFact>
                                                </keyFactSet>
                                                <keyFactSet/>
                                            </keyFinding>
                                        </keyFindings>
                                        <aspectReferences/>
                                    </solution>

    ReadOnly _testSolution5 As XElement = <solution>
                                              <keyFindings/>
                                              <conceptFindings>
                                                  <conceptFinding id="gapController" scoringMethod="None">
                                                      <conceptFactSet>
                                                          <conceptFact id="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <conceptValue domain="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                                                  <catchAllValue/>
                                                              </conceptValue>
                                                          </conceptFact>
                                                          <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                      </conceptFactSet>
                                                  </conceptFinding>
                                              </conceptFindings>
                                              <aspectReferences/>
                                          </solution>

    ReadOnly _result5 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                        <keyFindings/>
                                        <conceptFindings>
                                            <conceptFinding id="gapController" scoringMethod="None">
                                                <conceptFact id="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="1[*]-I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                                        <catchAllValue/>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFactSet/>
                                            </conceptFinding>
                                        </conceptFindings>
                                        <aspectReferences/>
                                    </solution>


End Class
