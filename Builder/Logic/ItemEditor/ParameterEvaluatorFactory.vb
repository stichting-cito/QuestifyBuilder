Imports Questify.Builder.Logic.Annotations
Imports Cito.Tester.ContentModel

Namespace ItemEditor
    Public NotInheritable Class ParameterEvaluatorFactory
        Private Sub New()
        End Sub
        Public Shared Function Create(<NotNull> parameter As ParameterBase) As IParameterEvaluator
            If TypeOf parameter Is CollectionParameter Then
                Return New CollectionParameterEvaluator(DirectCast(parameter, CollectionParameter))
            End If

            Return New ParameterEvaluator(parameter)

        End Function
    End Class
End Namespace
