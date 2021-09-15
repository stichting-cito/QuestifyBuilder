
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class RemoveUnusedFactsTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TryToRemoveFactsFromSolutionWithoutAny()
        'Arrange
        Dim solution = solution_ValidFacts.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.InlineId = "Some_InlineId", .FindingOverride = "TestFindingId"}.AddSubParameters("A", "B", "C", "D")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})
        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).Facts}, {"FactIdsToScoringParameter", result}}

        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(4, solution.Findings(0).Facts.Count)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TryToRemoveFactsFromSolutionWithOnlyOnValidFacts_OtherPostfix()
        'Arrange
        Dim solution = solution_NoValidFacts.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.InlineId = "Some_InlineId", .FindingOverride = "TestFindingId"}.AddSubParameters("A", "B", "C", "D")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})
        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).Facts}, {"FactIdsToScoringParameter", result}}

        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(0, solution.Findings(0).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TryToRemoveFactsFromSolutionWithOnlyonValidFacts_OtherPrefixes()
        'Arrange
        Dim solution = solution_NonValidFacts2.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.InlineId = "Some_InlineId", .FindingOverride = "TestFindingId"}.AddSubParameters("A", "B", "C", "D")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})
        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).Facts}, {"FactIdsToScoringParameter", result}}

        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(0, solution.Findings(0).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveFactsFromSolution_HalfOfFactsShouldBeRemoved()
        'Arrange
        Dim solution = solution_HalfValidFacts.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.InlineId = "Some_InlineId", .FindingOverride = "TestFindingId"}.AddSubParameters("A", "B", "C", "D")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})
        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).Facts}, {"FactIdsToScoringParameter", result}}

        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(2, solution.Findings(0).Facts.Count)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveFactsFromSolution_FactsWithAnswerCategoryFactId()
        'Arrange
        Dim solution = solution_WithAnswerCategoryFactId.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.InlineId = "Some_InlineId", .FindingOverride = "TestFindingId"}.AddSubParameters("A")
        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}})
        Dim inputs As New Dictionary(Of String, Object) From {{"BaseFacts", solution.Findings(0).Facts}, {"FactIdsToScoringParameter", result}}

        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFacts(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(2, solution.Findings(0).Facts.Count)
    End Sub

#Region "Data"
    ReadOnly solution_ValidFacts As XElement = <solution>
                                                   <keyFindings>
                                                       <keyFinding id="TestFindingId" scoringMethod="Dichotomous">
                                                           <keyFact id="A-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                           <keyFact id="B-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                           <keyFact id="C-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                           <keyFact id="D-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                       </keyFinding>
                                                   </keyFindings>
                                               </solution>

    ReadOnly solution_NoValidFacts As XElement = <solution>
                                                     <keyFindings>
                                                         <keyFinding id="TestFindingId" scoringMethod="Dichotomous">
                                                             <keyFact id="A-SomeOther_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                             <keyFact id="B-SomeOther_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                             <keyFact id="C-SomeOther_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                             <keyFact id="D-SomeOther_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                         </keyFinding>
                                                     </keyFindings>
                                                 </solution>

    ReadOnly solution_NonValidFacts2 As XElement = <solution>
                                                       <keyFindings>
                                                           <keyFinding id="TestFindingId" scoringMethod="Dichotomous">
                                                               <keyFact id="W-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                               <keyFact id="X-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                               <keyFact id="Y-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                               <keyFact id="Z-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                           </keyFinding>
                                                       </keyFindings>
                                                   </solution>

    ReadOnly solution_HalfValidFacts As XElement = <solution>
                                                       <keyFindings>
                                                           <keyFinding id="TestFindingId" scoringMethod="Dichotomous">
                                                               <keyFact id="A-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                               <keyFact id="B-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                               <keyFact id="1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                               <keyFact id="2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                           </keyFinding>
                                                       </keyFindings>
                                                   </solution>

    ReadOnly solution_WithAnswerCategoryFactId As XElement = <solution>
                                                                 <keyFindings>
                                                                     <keyFinding id="TestFindingId" scoringMethod="Dichotomous">
                                                                         <keyFact id="A-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                         <keyFact id="A[1]-Some_InlineId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                     </keyFinding>
                                                                 </keyFindings>
                                                             </solution>
#End Region

End Class