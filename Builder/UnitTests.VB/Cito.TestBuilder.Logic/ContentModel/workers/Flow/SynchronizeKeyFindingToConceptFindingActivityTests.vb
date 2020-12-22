
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.IO
Imports System.Diagnostics
Imports System.Activities
Imports Questify.Builder.Logic.ContentModel.workers.Flow

<TestClass>
Public Class SynchronizeKeyFindingToConceptFindingActivityTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub EnsureCopyWorkingOfActivity()
        Dim solution = Deserialize(Of Solution)(keyFindingToSynchronize)
        Dim scorePrm = GetScoreParam(Of MultiChoiceScoringParameter)("mc_1", "A", "B", "C", "D")
        Dim inputs As New Dictionary(Of String, Object) From {{"Solution", solution}, {"ScoreParameter", scorePrm}}
        WriteSolution("Arrange", solution)

        WorkflowInvoker.Invoke(New SynchronizeKeyFindingToConceptFindingActivity(), inputs)

        WriteSolution("Assert", solution)

        Assert.IsTrue(solution.ConceptFindingsSpecified, "ConceptFindings should be there.")
        Assert.AreEqual(1, solution.ConceptFindings.Count, "Expect single finding")
        Assert.AreEqual(4, solution.ConceptFindings(0).Facts.Count, "Expect four facts")
    End Sub

    Private keyFindingToSynchronize As XElement = <solution>
                                                      <keyFindings>
                                                          <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                              <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                  <keyValue domain="mc_1" occur="1">
                                                                      <stringValue>
                                                                          <typedValue>A</typedValue>
                                                                      </stringValue>
                                                                  </keyValue>
                                                              </keyFact>
                                                              <keyFact id="B-mc_2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                  <keyValue domain="mc_2" occur="1">
                                                                      <stringValue>
                                                                          <typedValue>B</typedValue>
                                                                      </stringValue>
                                                                  </keyValue>
                                                              </keyFact>
                                                              <keyFact id="D-mc_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                  <keyValue domain="mc_3" occur="1">
                                                                      <stringValue>
                                                                          <typedValue>D</typedValue>
                                                                      </stringValue>
                                                                  </keyValue>
                                                              </keyFact>
                                                              <keyFact id="C-mc_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                  <keyValue domain="mc_3" occur="1">
                                                                      <stringValue>
                                                                          <typedValue>C</typedValue>
                                                                      </stringValue>
                                                                  </keyValue>
                                                              </keyFact>
                                                          </keyFinding>
                                                      </keyFindings>
                                                      <conceptFindings>
                                                          <conceptFinding id="Opgave" scoringMethod="None"/>
                                                      </conceptFindings>
                                                      <aspectReferences/>
                                                      <ItemScoreTranslationTable>
                                                          <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                          <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                      </ItemScoreTranslationTable>
                                                  </solution>


    Private Function GetScoreParam(Of T As {ScoringParameter, New})(id As String, ParamArray ids As String()) As T
        Dim param = New T() With {.ControllerId = id, .FindingOverride = "Opgave"}
        param.Value = New ParameterSetCollection

        For Each id In ids
            param.Value.Add(New ParameterCollection() With {.Id = id})
        Next

        Return param
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

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

End Class
