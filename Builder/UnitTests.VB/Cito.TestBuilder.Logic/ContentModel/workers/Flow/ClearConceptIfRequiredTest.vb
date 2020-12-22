
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.IO
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ClearConceptIfRequiredTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SolutionThatIsSync_ConceptShouldNotBeCleared()
        Dim solution = inSyncSample.To(Of Solution)()
        Dim concept = sharedConceptFinding.To(Of ConceptFinding)() : solution.ConceptFindings.Add(concept)
        Dim scorePrm = New IntegerScoringParameter() With {.FindingOverride = "sharedIntegerFinding", .ControllerId = "integerScore"}.AddSubParameters("1", "2", "3", "4", "5")

        Dim inputs As New Dictionary(Of String, Object) From {{"Solution", solution}, {"ScoringParameters", New ScoringParameter() {scorePrm}}}
        WriteSolution("Arrange", solution)

        WorkflowInvoker.Invoke(New ClearConceptIfRequired(), inputs)

        Assert.AreEqual(1, solution.ConceptFindings.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SolutionOutOfSyncSample_ConceptShouldNotBeCleared()
        Dim solution = OutOfSyncSample.To(Of Solution)()
        Dim concept = sharedConceptFinding.To(Of ConceptFinding)() : solution.ConceptFindings.Add(concept)
        Dim scorePrm = New IntegerScoringParameter() With {.FindingOverride = "sharedIntegerFinding", .ControllerId = "integerScore"}.AddSubParameters("1", "2", "3", "4", "5")

        Dim inputs As New Dictionary(Of String, Object) From {{"Solution", solution}, {"ScoringParameters", New ScoringParameter() {scorePrm}}}
        WriteSolution("Arrange", solution)

        WorkflowInvoker.Invoke(New ClearConceptIfRequired(), inputs)

        Assert.AreEqual(0, solution.ConceptFindings.Count)
    End Sub


    ReadOnly inSyncSample As XElement = <solution>
                                            <keyFindings>
                                                <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                    <keyFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <keyValue domain="integerScore" occur="1">
                                                            <integerValue>
                                                                <typedValue>42</typedValue>
                                                            </integerValue>
                                                        </keyValue>
                                                    </keyFact>
                                                    <keyFactSet>
                                                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <keyValue domain="integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>6</typedValue>
                                                                </integerValue>
                                                            </keyValue>
                                                        </keyFact>
                                                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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
                                                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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
                                                </keyFinding>
                                            </keyFindings>
                                            <aspectReferences/>
                                        </solution>


    ReadOnly OutOfSyncSample As XElement = <solution>
                                               <keyFindings>
                                                   <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                       <keyFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>42</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>

                                                       <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>6</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>
                                                       <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>14</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>

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

                                                   </keyFinding>
                                               </keyFindings>
                                           </solution>

    ReadOnly sharedConceptFinding As XElement = <conceptFinding id="sharedIntegerFinding" scoringMethod="None">

                                                    <conceptFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                        <conceptValue domain="5-integerScore" occur="1">
                                                            <integerValue>
                                                                <typedValue>42</typedValue>
                                                            </integerValue>
                                                        </conceptValue>
                                                    </conceptFact>

                                                    <conceptFactSet>
                                                        <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="1-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>6</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="2-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>14</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <concept value="1" code="1"/>
                                                        </concepts>
                                                    </conceptFactSet>

                                                    <conceptFactSet>
                                                        <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="1-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>14</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="2-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>6</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <concept value="1" code="1"/>
                                                        </concepts>
                                                    </conceptFactSet>

                                                    <conceptFactSet>
                                                        <conceptFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="3-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>3</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <conceptFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="4-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>7</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                    </conceptFactSet>

                                                    <conceptFactSet>
                                                        <conceptFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="3-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>7</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <conceptFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="4-integerScore" occur="1">
                                                                <integerValue>
                                                                    <typedValue>3</typedValue>
                                                                </integerValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                    </conceptFactSet>
                                                </conceptFinding>


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
