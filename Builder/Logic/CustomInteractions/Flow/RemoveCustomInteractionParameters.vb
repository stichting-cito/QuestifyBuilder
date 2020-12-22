Imports System.Activities
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel

Namespace CustomInteractions.Flow

    Public NotInheritable Class RemoveCustomInteractionParameters
        Inherits CodeActivity
        Public Property ParameterSetCollection As InArgument(Of ParameterSetCollection)
        Public Property ParameterCollectionName As InArgument(Of String)
        Public Property Solution As InArgument(Of Solution)

        Protected Overrides Sub Execute(context As CodeActivityContext)
            Dim parameters As ParameterSetCollection = context.GetValue(ParameterSetCollection)
            Dim collectionId As String = context.GetValue(ParameterCollectionName)
            Dim sol As Solution = context.GetValue(Solution)

            Dim toRemove = parameters.FirstOrDefault(Function(collection) collection.Id = collectionId)
            If toRemove IsNot Nothing Then
                parameters.Remove(toRemove)

                sol.FixRemovedScoringParameters(parameters.DeepFetchInlineScoringParameters())
            End If
        End Sub
    End Class
End NameSpace