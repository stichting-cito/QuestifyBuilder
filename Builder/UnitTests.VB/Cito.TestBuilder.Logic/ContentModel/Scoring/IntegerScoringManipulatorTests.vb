
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq
Imports System.Xml.Serialization
Imports System.Xml.Linq
Imports System.Diagnostics
Imports System.IO
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class IntegerScoringManipulatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ControllerIdSet_ShouldBeSetAsPostFix()
        Dim param = New IntegerScoringParameter() With {.ControllerId = "Gap"}

        Dim result = DirectCast(param.GetScoreManipulator(New Solution), MultiTypeScoringManipulator).Manipulator.FactIdPostFix

        Assert.AreEqual("-Gap", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub InlineIdSet_ShouldBeSetAsPostFix()
        Dim param = New IntegerScoringParameter() With {.InlineId = "Inline"}

        Dim result = DirectCast(param.GetScoreManipulator(New Solution), MultiTypeScoringManipulator).Manipulator.FactIdPostFix

        Assert.AreEqual("-Inline", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ControllerIdSet_and_InlineIdSet_Inline_ShouldBeSetAsPostFix()
        Dim param = New IntegerScoringParameter() With {.InlineId = "Inline", .ControllerId = "Controller"}

        Dim result = DirectCast(param.GetScoreManipulator(New Solution), MultiTypeScoringManipulator).Manipulator.FactIdPostFix

        Assert.AreEqual("-Inline", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetKey_A_ShouldContainA_AndValue()
        Dim param = New IntegerScoringParameter() With {.ControllerId = "Gap"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = New KeyFinding()
        Dim manipulator As IGapScoringManipulator(Of MultiType) = New MultiTypeScoringManipulator(New KeyManipulator(key), param)

        manipulator.SetKey("A", New GapValue(Of MultiType)(5, GapComparisonType.GreaterThan))
        manipulator.SetKey("B", 6)

        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual(1, res("A").Count())
        Assert.AreEqual(GapComparisonType.GreaterThan, res("A").First().Comparison)
        Assert.AreEqual(5, res("A").First().Value.IntegerValue)
        Assert.AreEqual("5", res("A").First().Value.ToString)

        Assert.IsTrue(res.ContainsKey("B"))
        Assert.AreEqual(1, res("B").Count())
        Assert.AreEqual(GapComparisonType.Equals, res("B").First().Comparison)
        Assert.AreEqual(6, res("B").First().Value.IntegerValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestMultipleIntegerScoringParameters()
        Dim paramA = New IntegerScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline1"}
        paramA.Value = New ParameterSetCollection
        paramA.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim paramB = New IntegerScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline2"}
        paramB.Value = New ParameterSetCollection
        paramB.Value.Add(New ParameterCollection() With {.Id = "C"})
        paramB.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim solution As New Solution()

        Dim manipulatorA = paramA.GetScoreManipulator(solution)
        Dim manipulatorB = paramB.GetScoreManipulator(solution)

        manipulatorA.SetKey("A", 1)
        manipulatorB.SetKey("C", New GapValue(Of MultiType)(3, GapComparisonType.GreaterThan))
        manipulatorB.SetKey("D", New GapValue(Of MultiType)(4, 5))

        Dim keyStatusA = manipulatorA.GetKeyStatus()
        Dim keyStatusB = manipulatorB.GetKeyStatus()

        WriteSolution("assert", solution)
        Assert.AreEqual(1, keyStatusA.Count)
        Assert.AreEqual(1, keyStatusA("A").First().Value.IntegerValue)
        Assert.AreEqual("inline1", solution.Findings(0).Facts(0).Values(0).Domain)
        Assert.AreEqual(GapComparisonType.Equals, keyStatusA("A").First().Comparison)
        Assert.AreEqual(2, keyStatusB.Count)
        Assert.AreEqual(3, keyStatusB("C").First().Value.IntegerValue)
        Assert.AreEqual(GapComparisonType.GreaterThan, keyStatusB("C").First().Comparison)
        Assert.AreEqual("inline2", solution.Findings(0).Facts(1).Values(0).Domain)
        Assert.AreEqual(4, keyStatusB("D").First().Value.IntegerValue)
        Assert.AreEqual(5, keyStatusB("D").First().Value2.IntegerValue)
        Assert.AreEqual("inline2", solution.Findings(0).Facts(2).Values(0).Domain)
        Assert.AreEqual(GapComparisonType.Range, keyStatusB("D").First().Comparison)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestSetValueMultipleTimes()
        Dim paramA = New IntegerScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline1"}
        paramA.Value = New ParameterSetCollection
        paramA.Value.Add(New ParameterCollection() With {.Id = "A"})
        Dim solution As New Solution()
        Dim manipulatorA = paramA.GetScoreManipulator(solution)

        manipulatorA.SetKey("A", 1)
        manipulatorA.ReplaceKeyValueAt("A", 2, 0)
        manipulatorA.ReplaceKeyValueAt("A", 100, 0)
        Dim keyStatusA = manipulatorA.GetKeyStatus()

        Assert.AreEqual(1, keyStatusA.Values.Count)
        Assert.AreEqual(100, keyStatusA.Values(0).First().Value.IntegerValue)
        Assert.AreEqual(1, keyStatusA.Values(0).Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestSolutionSorting_SeparateFindings()
        Dim paramA = New IntegerScoringParameter() With {.ControllerId = "Gap", .InlineId = "inline1"}
        paramA.Value = New ParameterSetCollection
        paramA.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim paramB = New IntegerScoringParameter() With {.ControllerId = "Gap", .InlineId = "inline2"}
        paramB.Value = New ParameterSetCollection
        paramB.Value.Add(New ParameterCollection() With {.Id = "B"})

        Dim paramC = New DecimalScoringParameter() With {.ControllerId = "Gap", .InlineId = "inline3"}
        paramC.Value = New ParameterSetCollection
        paramC.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim parameters = New List(Of ScoringParameter)(New ScoringParameter() {paramA, paramB, paramC})

        Dim solution As New Solution()

        Dim manipulatorA = paramA.GetScoreManipulator(solution)
        Dim manipulatorC = paramC.GetScoreManipulator(solution)
        Dim manipulatorB = paramB.GetScoreManipulator(solution)

        manipulatorA.SetKey("A", 1)
        manipulatorC.SetKey("C", 3)
        manipulatorB.SetKey("B", 2)
        solution.SortSolution(parameters)
        Dim keyValuesString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        Assert.AreEqual("1|2|3", keyValuesString)

        keyValuesString = New ScoringDisplayValueCalculator(parameters, solution).GetScoreDisplayValue()
        Assert.AreEqual("1|2|3", keyValuesString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestSolutionSorting_SharedFinding()
        Dim paramA = New IntegerScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline1"}.AddSubParameters("A")
        Dim paramB = New IntegerScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline2"}.AddSubParameters("B")
        Dim paramC = New DecimalScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline3"}.AddSubParameters("C")

        Dim parameters = New List(Of ScoringParameter)(New ScoringParameter() {paramA, paramB, paramC})

        Dim solution As New Solution()

        Dim manipulatorA = paramA.GetScoreManipulator(solution)
        Dim manipulatorB = paramB.GetScoreManipulator(solution)
        Dim manipulatorC = paramC.GetScoreManipulator(solution)

        manipulatorA.SetKey("A", 1)
        manipulatorC.SetKey("C", 3)
        manipulatorB.SetKey("B", 2)
        solution.SortSolution(parameters)
        Dim keyValuesString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        Assert.AreEqual("1&2&3", keyValuesString)

        keyValuesString = New ScoringDisplayValueCalculator(parameters, solution).GetScoreDisplayValue()
        Assert.AreEqual("1&2&3", keyValuesString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestScoringParameterRemoval_SharedFinding()
        Dim paramA = New IntegerScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline1"}.AddSubParameters("A")
        Dim paramCD = New IntegerScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline2"}.AddSubParameters("C", "D")
        Dim paramE = New DecimalScoringParameter() With {.ControllerId = "Gap", .FindingOverride = "shared_finding", .InlineId = "inline3"}.AddSubParameters("E")

        Dim solution As New Solution()

        Dim manipulatorA = paramA.GetScoreManipulator(solution)
        Dim manipulatorCD = paramCD.GetScoreManipulator(solution)
        Dim manipulatorE = paramE.GetScoreManipulator(solution)

        manipulatorA.SetKey("A", 1)
        manipulatorCD.SetKey("C", New GapValue(Of MultiType)(3, GapComparisonType.GreaterThan))
        manipulatorCD.SetKey("D", New GapValue(Of MultiType)(4, 5))
        manipulatorE.SetKey("E", 7.5D)
        Dim solutionBeforeFix = DeepCloneSolution(solution)

        solution.FixRemovedScoringParameters(New ScoringParameter() {paramA, paramE})
        Dim solutionAfterFix = DeepCloneSolution(solution)

        WriteSolution("assert", solution)
        Assert.AreEqual(1, solutionBeforeFix.Findings.Count)
        Assert.AreEqual(4, solutionBeforeFix.Findings(0).Facts.Count, "Expected 4 facts")
        Assert.AreEqual(1, solutionBeforeFix.Findings(0).Facts(0).Values.Count)

        Assert.AreEqual(1, solutionAfterFix.Findings.Count)
        Assert.AreEqual(2, solutionAfterFix.Findings(0).Facts.Count, "Expected 2 facts")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TestScoringParameterRemoval_SeparatedFindings()
        Dim paramA = New IntegerScoringParameter() With {.ControllerId = "Gap", .InlineId = "inline1"}.AddSubParameters("A")
        Dim paramCD = New IntegerScoringParameter() With {.ControllerId = "Gap", .InlineId = "inline2"}.AddSubParameters("C", "D")
        Dim paramE = New DecimalScoringParameter() With {.ControllerId = "Gap", .InlineId = "inline3"}.AddSubParameters("E")

        Dim solution As New Solution()

        Dim manipulatorA = paramA.GetScoreManipulator(solution)
        Dim manipulatorCD = paramCD.GetScoreManipulator(solution)
        Dim manipulatorE = paramE.GetScoreManipulator(solution)

        manipulatorA.SetKey("A", 1)
        manipulatorCD.SetKey("C", New GapValue(Of MultiType)(3, GapComparisonType.GreaterThan))
        manipulatorCD.SetKey("D", New GapValue(Of MultiType)(4, 5))
        manipulatorE.SetKey("E", 7.5D)
        Dim solutionState1 = DeepCloneSolution(solution)
        solution.FixRemovedScoringParameters(New ScoringParameter() {paramA, paramE})
        Dim solutionState2 = DeepCloneSolution(solution)

        WriteSolution("assert", solution)
        Assert.AreEqual(3, solutionState1.Findings.Count)
        Assert.AreEqual(1, solutionState1.Findings(0).Facts.Count)
        Assert.AreEqual(2, solutionState1.Findings(1).Facts.Count)
        Assert.AreEqual(1, solutionState1.Findings(2).Facts.Count)

        Assert.AreEqual(2, solutionState2.Findings.Count)
        Assert.AreEqual(1, solutionState2.Findings(0).Facts.Count)
        Assert.AreEqual(1, solutionState2.Findings(1).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub UnsetKeys()
        Dim solution = Deserialize(Of Solution)(someIntegerData)
        WriteSolution("Arrange", solution)
        Dim intParam = New IntegerScoringParameter() With {.ControllerId = "Controller"}
        intParam.Value = New ParameterSetCollection() : intParam.Value.Add(New ParameterCollection() With {.Id = "A"})
        Dim manipulator = intParam.GetScoreManipulator(solution)

        manipulator.RemoveKey("A")

        WriteSolution("Assert", solution)
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual(0, solution.Findings(0).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub UnsetKeyInFactSet()
        Dim solution = Deserialize(Of Solution)(someIntegerDataWithFactSets)
        WriteSolution("Arrange", solution)

        Dim intParam = New IntegerScoringParameter() With {.ControllerId = "integerScore"}
        intParam.Value = New ParameterSetCollection() : intParam.Value.Add(New ParameterCollection() With {.Id = "1"}) : intParam.Value.Add(New ParameterCollection() With {.Id = "B"})
        Dim manipulator = intParam.GetScoreManipulator(solution)

        manipulator.SetFactSetTarget(1)

        manipulator.RemoveKey("B")
        Dim result = manipulator.GetKeyStatus("B")

        WriteSolution("Assert", solution)
        Assert.IsFalse(result(0).Value.IntegerValue.HasValue)
        Assert.AreEqual(String.Empty, result(0).Value.ToString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValueForKey()
        Dim solution = sampleData.To(Of Solution)()
        Dim intParam = New IntegerScoringParameter() With {.ControllerId = "Controller"}.AddSubParameters("A")
        Dim manipulator = intParam.GetScoreManipulator(solution)

        Dim result = manipulator.GetDisplayValueForKey("A")

        Dim expected As String = "1#2#3"
        Assert.AreEqual(expected, result)
    End Sub


    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

    Function DeepCloneSolution(s As Solution) As Solution
        Dim a As New XmlSerializer(GetType(Solution))
        Dim stream As New MemoryStream()
        a.Serialize(stream, s)
        stream.Position = 0
        Return DirectCast(a.Deserialize(stream), Solution)
    End Function

    Function DoSerialize(Of T)(obj As T) As XElement
        Dim s = New XmlSerializer(GetType(T))
        Dim ret As XElement = Nothing
        Using m As New StringWriter()
            s.Serialize(m, obj)
            ret = XElement.Parse(m.ToString())
        End Using
        Return ret
    End Function

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Private someIntegerData As XElement = <solution>
                                              <keyFindings>
                                                  <keyFinding id="Controller" scoringMethod="None">
                                                      <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <keyValue domain="Controller" occur="1">
                                                              <integerValue>
                                                                  <typedValue>1</typedValue>
                                                              </integerValue>
                                                          </keyValue>
                                                      </keyFact>
                                                  </keyFinding>
                                              </keyFindings>
                                              <aspectReferences/>
                                          </solution>

    Private someIntegerDataWithFactSets As XElement = <solution>
                                                          <keyFindings>
                                                              <keyFinding id="integerScore" scoringMethod="Dichotomous">
                                                                  <keyFactSet>
                                                                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="B-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>14</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                  </keyFactSet>
                                                                  <keyFactSet>
                                                                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>14</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="B-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                  </keyFactSet>
                                                                  <keyFactSet>
                                                                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>3</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                  </keyFactSet>
                                                                  <keyFactSet>
                                                                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                      <keyFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>3</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                  </keyFactSet>
                                                                  <keyFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="integerScore" occur="1">
                                                                          <integerValue>
                                                                              <typedValue>1</typedValue>
                                                                          </integerValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                              </keyFinding>
                                                          </keyFindings>
                                                          <aspectReferences/>
                                                      </solution>

    ReadOnly sampleData As XElement = <solution>
                                          <keyFindings>
                                              <keyFinding id="Controller" scoringMethod="None">
                                                  <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                      <keyValue domain="Controller" occur="1">
                                                          <integerValue>
                                                              <typedValue>1</typedValue>
                                                          </integerValue>
                                                          <integerValue>
                                                              <typedValue>2</typedValue>
                                                          </integerValue>
                                                          <integerValue>
                                                              <typedValue>3</typedValue>
                                                          </integerValue>
                                                      </keyValue>
                                                  </keyFact>
                                              </keyFinding>
                                          </keyFindings>
                                          <aspectReferences/>
                                      </solution>

End Class
