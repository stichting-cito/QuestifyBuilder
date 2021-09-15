
Imports System.Xml.Linq
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class RemoveUnusedFindingsTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveFindings_OneScoringParamWithControllerID()
        'Arrange
        Dim solution = _testSolution.To(Of Solution)()
        Dim sp = New IntegerScoringParameter With {.ControllerId = "Some_ControllerID"}
        Dim inputs As New Dictionary(Of String, Object) From {{"Findings", solution.Findings}, {"ScoringParameters", New ScoringParameter() {sp}}}
        
        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFindings(Of KeyFinding)(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsTrue(solution.Findings.Contains("Some_ControllerID"))
        Assert.IsFalse(solution.Findings.Contains("Some_InlineId"))
        Assert.IsFalse(solution.Findings.Contains("Some_FindingOverride"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveFindings_OneScoringParamWithInlineId()
        'Arrange
        Dim solution = _testSolution.To(Of Solution)()
        Dim sp = New IntegerScoringParameter With {.InlineId = "Some_InlineId"}
        Dim inputs As New Dictionary(Of String, Object) From {{"Findings", solution.Findings}, {"ScoringParameters", New ScoringParameter() {sp}}}
        
        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFindings(Of KeyFinding)(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsFalse(solution.Findings.Contains("Some_ControllerID"))
        Assert.IsTrue(solution.Findings.Contains("Some_InlineId"))
        Assert.IsFalse(solution.Findings.Contains("Some_FindingOverride"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveFindings_OneScoringParamWithFindingOverride()
        'Arrange
        Dim solution = _testSolution.To(Of Solution)()
        Dim sp = New IntegerScoringParameter With {.InlineId = "Some_FindingOverride"}
        Dim inputs As New Dictionary(Of String, Object) From {{"Findings", solution.Findings}, {"ScoringParameters", New ScoringParameter() {sp}}}

        'Act
        WorkflowInvoker.Invoke(New RemoveUnusedFindings(Of KeyFinding)(), inputs)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsFalse(solution.Findings.Contains("Some_ControllerID"))
        Assert.IsFalse(solution.Findings.Contains("Some_InlineId"))
        Assert.IsTrue(solution.Findings.Contains("Some_FindingOverride"))
    End Sub

#Region "Data"
    ReadOnly _testSolution As XElement = <solution>
                                             <keyFindings>
                                                 <keyFinding id="Some_ControllerID" scoringMethod="Dichotomous"/>
                                                 <keyFinding id="Some_InlineId" scoringMethod="Dichotomous"/>
                                                 <keyFinding id="Some_FindingOverride" scoringMethod="Dichotomous"/>
                                             </keyFindings>
                                         </solution>

#End Region

End Class
