Imports System.Activities
Imports Questify.Builder.Logic.CustomInteractions.Flow
Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Public NotInheritable Class InlineCustomInteraction
        Private Sub New()
        End Sub
        Public Shared Sub AddParameters(customInteractionName As String, bankId As Integer, parameterSetCollection As ParameterSetCollection, solution As Solution, inlineParameterSetCollection As ParameterSetCollection, findingOverride As String, _
            scoringLabel As String)
            WorkflowInvoker.Invoke(New AddTo(), New Dictionary(Of String, Object)() From {
                {"BankId", bankId},
                {"ResourceName", customInteractionName},
                {"parameters", parameterSetCollection},
                {"ItemSolution", solution},
                {"inlineParameters", inlineParameterSetCollection},
                {"findingOverride", findingOverride},
                {"scoringLabel", scoringLabel}
            })
        End Sub

        Public Shared Sub Remove(parameterSetCollection As ParameterSetCollection, inlineParameterSetCollection As ParameterSetCollection, solution As Solution)
            WorkflowInvoker.Invoke(New RemoveFrom(), New Dictionary(Of String, Object)() From {
                {"parameters", parameterSetCollection}, _
                {"inlineParameters", inlineParameterSetCollection},
                {"itemSolution", solution}
            })
        End Sub
    End Class
End Namespace
