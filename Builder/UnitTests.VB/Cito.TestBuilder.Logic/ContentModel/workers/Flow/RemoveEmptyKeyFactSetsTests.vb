
Imports System.Xml.Linq
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class RemoveEmptyKeyFactSetsTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Remove_1FactSet()
        Dim solution = _testSolution1EmptySet.To(Of Solution)()
        Dim inputs As New Dictionary(Of String, Object) From {{"Finding", solution.Findings(0)}}

        WorkflowInvoker.Invoke(New RemoveEmptyKeyFactSets(Of KeyFinding)(), inputs)

        solution.WriteToDebug("Assert")
        Dim firsFinding = solution.Findings(0)
        Assert.AreEqual(0, firsFinding.KeyFactsets.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Remove_3FactSet()
        Dim solution = _testSolution3EmptySets.To(Of Solution)()
        Dim inputs As New Dictionary(Of String, Object) From {{"Finding", solution.Findings(0)}}

        WorkflowInvoker.Invoke(New RemoveEmptyKeyFactSets(Of KeyFinding)(), inputs)

        solution.WriteToDebug("Assert")
        Dim firsFinding = solution.Findings(0)
        Assert.AreEqual(0, firsFinding.KeyFactsets.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Remove_2FactSet()
        Dim solution = _testSolution2EmptySets_1NotEmpty.To(Of Solution)()
        Dim inputs As New Dictionary(Of String, Object) From {{"Finding", solution.Findings(0)}}

        WorkflowInvoker.Invoke(New RemoveEmptyKeyFactSets(Of KeyFinding)(), inputs)

        solution.WriteToDebug("Assert")
        Dim firsFinding = solution.Findings(0)
        Assert.AreEqual(1, firsFinding.KeyFactsets.Count)
    End Sub

    ReadOnly _testSolution1EmptySet As XElement = <solution>
                                                      <keyFindings>
                                                          <keyFinding id="shared_finding" scoringMethod="None">
                                                              <keyFactSet/>
                                                          </keyFinding>
                                                      </keyFindings>
                                                  </solution>

    ReadOnly _testSolution3EmptySets As XElement = <solution>
                                                       <keyFindings>
                                                           <keyFinding id="shared_finding" scoringMethod="None">
                                                               <keyFactSet/>
                                                               <keyFactSet/>
                                                               <keyFactSet/>
                                                           </keyFinding>
                                                       </keyFindings>
                                                   </solution>

    ReadOnly _testSolution2EmptySets_1NotEmpty As XElement = <solution>
                                                                 <keyFindings>
                                                                     <keyFinding id="shared_finding" scoringMethod="None">
                                                                         <keyFactSet/>
                                                                         <keyFactSet>
                                                                             <keyFact xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                         </keyFactSet>
                                                                         <keyFactSet/>
                                                                     </keyFinding>
                                                                 </keyFindings>
                                                             </solution>


End Class
