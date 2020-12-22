Imports System.Activities
Imports Questify.Builder.Logic.CustomInteractions.Flow
Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Public NotInheritable Class CustomInteraction
        Private Sub New()
        End Sub
        Public Shared Sub AddParameters(customInteractionName As String, bankId As Integer, parameterSetCollection As ParameterSetCollection, solution As Solution)
            WorkflowInvoker.Invoke(New AddTo(), New Dictionary(Of String, Object)() From { _
                {"BankId", bankId}, _
                {"ResourceName", customInteractionName}, _
                {"parameters", parameterSetCollection}, _
                {"ItemSolution", solution} _
            })
        End Sub

        Public Shared Sub Remove(parameterSetCollection As ParameterSetCollection, solution As Solution)
            WorkflowInvoker.Invoke(New RemoveFrom(), New Dictionary(Of String, Object)() From { _
                {"parameters", parameterSetCollection}, _
                {"itemSolution", solution} _
            })
        End Sub

    End Class
End Namespace
