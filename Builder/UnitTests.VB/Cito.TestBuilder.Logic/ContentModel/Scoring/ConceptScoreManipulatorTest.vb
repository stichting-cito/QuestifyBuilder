
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports System.Diagnostics
Imports System.Linq
Imports Cito.Tester.Common
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ConceptScoreManipulatorTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddSingleScore_ToSolutionWithConceptFinding()
        Dim sol = GetSolution(Key_and_ConceptFinding__EmptyConceptScore)
        Dim scoreParam As ScoringParameter = New ChoiceScoringParameter() With {.FindingOverride = "mc", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim scoreManipulator = scoreParam.GetScoreManipulator(sol)
        Dim factIds = scoreManipulator.GetKeysAlreadyManipulated().ToList()
        Dim conceptScoreManipulator = CreateManipulator(scoreParam, sol)

        WriteSolution("Before", sol)

        conceptScoreManipulator.SetScore("SomePart", factIds(0), 1)

        WriteSolution("After", sol)

        Assert.AreEqual(1, getConceptFact("mc", "B", sol).Concepts.Count, "Expected the concepts to contain exactly 1 concept")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddSingleScore_ToSolutionWithoutConceptFinding()
        Dim sol = GetSolution(KeyFact_Only)
        WriteSolution("Arranging", sol)
        Dim scoreParam As ScoringParameter = New ChoiceScoringParameter() With {.FindingOverride = "mc", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim scoreManipulator = scoreParam.GetScoreManipulator(sol)
        Dim factIds = scoreManipulator.GetKeysAlreadyManipulated().ToList()
        Dim conceptScoreManipulator = CreateManipulator(scoreParam, sol)

        WriteSolution("Before", sol)

        conceptScoreManipulator.SetScore("SomePart", factIds(0), 1)

        WriteSolution("After", sol)

        Assert.AreEqual(1, getConceptFact("mc", "B", sol).Concepts.Count, "Expected the concepts to contain exactly 1 concept")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptScore_ToSolutionWithoutConceptFinding_ScoreShouldBe_NULL()
        Dim sol = GetSolution(KeyFact_Only)
        WriteSolution("Arranging", sol)

        Dim scoreParam As ScoringParameter = New ChoiceScoringParameter() With {.FindingOverride = "mc", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        Dim scoreManipulator = scoreParam.GetScoreManipulator(sol)
        Dim factIds = scoreManipulator.GetKeysAlreadyManipulated().ToList()
        Dim conceptScoreManipulator = CreateManipulator(scoreParam, sol)

        WriteSolution("Before", sol)

        Dim result = conceptScoreManipulator.GetScoreForPart("SomePart", scoreManipulator.GetKeysAlreadyManipulated()).ToList()

        WriteSolution("After", sol)

        Assert.AreEqual(1, result.Count())
        Assert.AreEqual(Nothing, result.First())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptScore_GetScoreForPartShouldReturnValuesInFactIdOrder()
        Dim sol = GetSolution(Key_and_ConceptFinding_WithFilledInConceptScore)

        Dim scoreParam As ScoringParameter = New GraphGapMatchScoringParameter() With {.FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C")
        Dim factIds1 = New String() {"A-gapMatchController", "A[*]-gapMatchController"}
        Dim factIds2 = New String() {"A[*]-gapMatchController", "A-gapMatchController"}

        Dim conceptScoreManipulator = CreateManipulator(scoreParam, sol)

        Dim result1 = conceptScoreManipulator.GetScoreForPart("Attribuut 01", factIds1).ToList()
        Dim result2 = conceptScoreManipulator.GetScoreForPart("Attribuut 01", factIds2).ToList()

        Assert.AreEqual(2, result1.Count())
        Assert.AreEqual(10, result1(0))
        Assert.AreEqual(20, result1(1))

        Assert.AreEqual(2, result2.Count())
        Assert.AreEqual(20, result2(0))
        Assert.AreEqual(10, result2(1))
    End Sub

    Function GetStringParam(controllerId As String, ParamArray ids As String()) As StringScoringParameter
        Dim param = New StringScoringParameter() With {.ControllerId = controllerId}
        param.Value = New ParameterSetCollection
        For Each id In ids
            param.Value.Add(New ParameterCollection() With {.Id = id})
        Next
        Return param
    End Function

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

    Function getKeyFact(findingId As String, factId As String, s As Solution) As KeyFact
        Dim finding = s.Findings.Where(Function(f) f.Id = findingId).FirstOrDefault
        If finding IsNot Nothing Then
            Dim fact = finding.Facts.Where(Function(f) f.Id = factId).FirstOrDefault()
            If (fact Is Nothing) Then Return Nothing
            Return DirectCast(fact, KeyFact)
        End If

        Return Nothing
    End Function

    Function getConceptFact(findingId As String, factId As String, s As Solution) As ConceptFact
        Dim finding = s.ConceptFindings.Where(Function(f) f.Id = findingId).FirstOrDefault
        If finding IsNot Nothing Then
            Dim fact = finding.Facts.Where(Function(f) f.Id = factId).FirstOrDefault()
            If (fact Is Nothing) Then Return Nothing
            Return DirectCast(fact, ConceptFact)
        End If
        Return Nothing
    End Function

    Private Function GetSolution(xElement As Xml.Linq.XElement) As Solution
        Return DirectCast(SerializeHelper.XmlDeserializeFromString(xElement.ToString(), GetType(Solution)), Solution)
    End Function

    Private Key_and_ConceptFinding__EmptyConceptScore As XElement = <solution>

                                                                        <keyFindings>
                                                                            <keyFinding id="mc" scoringMethod="Polytomous">
                                                                                <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="mc" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>B</typedValue>
                                                                                        </stringValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                            </keyFinding>
                                                                        </keyFindings>

                                                                        <conceptFindings>
                                                                            <conceptFinding id="mc" scoringMethod="None">
                                                                                <conceptFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <conceptValue domain="B" occur="1">
                                                                                        <stringValue>
                                                                                            <typedValue>B</typedValue>
                                                                                        </stringValue>
                                                                                    </conceptValue>
                                                                                </conceptFact>
                                                                            </conceptFinding>
                                                                        </conceptFindings>

                                                                        <aspectReferences/>
                                                                    </solution>

    Private KeyFact_Only As XElement = <solution>

                                           <keyFindings>
                                               <keyFinding id="mc" scoringMethod="Polytomous">
                                                   <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="mc" occur="1">
                                                           <stringValue>
                                                               <typedValue>B</typedValue>
                                                           </stringValue>
                                                       </keyValue>
                                                   </keyFact>
                                               </keyFinding>
                                           </keyFindings>

                                           <aspectReferences/>
                                       </solution>

    Private Key_and_ConceptFinding_WithFilledInConceptScore As XElement = <solution>
                                                                              <keyFindings>
                                                                                  <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                                                                                      <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                          <keyValue domain="A-gapMatchController" occur="1">
                                                                                              <stringValue>
                                                                                                  <typedValue>B</typedValue>
                                                                                              </stringValue>
                                                                                          </keyValue>
                                                                                      </keyFact>
                                                                                  </keyFinding>
                                                                              </keyFindings>
                                                                              <conceptFindings>
                                                                                  <conceptFinding id="gapMatchController" scoringMethod="None">
                                                                                      <conceptFact id="A[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                          <conceptValue domain="A[*]-gapMatchController" occur="1">
                                                                                              <catchAllValue/>
                                                                                          </conceptValue>
                                                                                          <concepts>
                                                                                              <concept value="20" code="Attribuut 01"/>
                                                                                          </concepts>
                                                                                      </conceptFact>
                                                                                      <conceptFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                          <conceptValue domain="A-gapMatchController" occur="1">
                                                                                              <stringValue>
                                                                                                  <typedValue>B</typedValue>
                                                                                              </stringValue>
                                                                                          </conceptValue>
                                                                                          <concepts>
                                                                                              <concept value="10" code="Attribuut 01"/>
                                                                                          </concepts>
                                                                                      </conceptFact>
                                                                                  </conceptFinding>
                                                                              </conceptFindings>
                                                                              <aspectReferences/>
                                                                          </solution>

    Private Function CreateManipulator(prm As ScoringParameter, solution As Solution) As IConceptScoreManipulator
        Dim map = New ScoringMap(New ScoringParameter() {prm}, solution).GetMap()

        Dim combined = CombinedScoringMapKey.Create(map.First())

        Return combined.GetConceptManipulator(solution)
    End Function

End Class
