Imports System.Runtime.CompilerServices
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel
    Public Module SolutionExtension

        <Extension>
        Public Sub FixRemovedScoringParameters(solution As Solution, prms As IEnumerable(Of ScoringParameter))
            Dim inputs As New Dictionary(Of String, Object) From {{"Solution", solution}, {"ScoringParameters", prms}}
            WorkflowInvoker.Invoke(New SolutionCleaner(), inputs)
        End Sub

        <Extension>
        Public Sub SortSolution(solution As Solution, prms As IEnumerable(Of ScoringParameter))
            Dim inlineIds As List(Of String) = prms.Where(Function(p) Not String.IsNullOrEmpty(p.InlineId)).Select(Function(p) p.InlineId).ToList()

            solution.Findings.Sort(Function(findingA, findingB)
                                       Dim findingAindex = inlineIds.IndexOf(findingA.Id)
                                       Dim findingBindex = inlineIds.IndexOf(findingB.Id)
                                       Return findingAindex.CompareTo(findingBindex)
                                   End Function)

            For Each finding In solution.Findings
                finding.Facts.Sort(Function(factA, factB)
                                       Dim factAindex = inlineIds.IndexOf(If(factA.Values.Any(), factA.Values.First().Domain, String.Empty))
                                       Dim factBindex = inlineIds.IndexOf(If(factB.Values.Any(), factB.Values.First().Domain, String.Empty))
                                       Return factAindex.CompareTo(factBindex)
                                   End Function)
            Next
        End Sub

        <Extension>
        Public Sub ClearConceptFinding(solution As Solution)
            Solution.ConceptFindings.Clear()
        End Sub

        <Extension>
        Public Function GetControlId(baseValue As BaseFactValue, controlId As String) As String

            If baseValue.Domain.Contains(controlId) Then
                Return baseValue.Domain.Substring(baseValue.Domain.IndexOf(controlId), controlId.Length)
            End If

            Return String.Empty
        End Function

        <Extension>
        Public Function EqualsById(baseValue As BaseFact, id As String) As Boolean
            Return DefaultStringOperations.FactIdEquals(baseValue.Id, id)
        End Function

    End Module
End Namespace
