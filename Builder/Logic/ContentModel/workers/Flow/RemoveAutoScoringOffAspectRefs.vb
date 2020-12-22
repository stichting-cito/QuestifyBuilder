Imports System.Activities
Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveAutoScoringOffAspectRefs
        Inherits CodeActivity
        Public Property Solution As InArgument(Of Solution)

        Protected Overrides Sub Execute(context As CodeActivityContext)
            Dim sol As Solution = context.GetValue(Solution)
            Dim toRemove = sol.AspectReferenceSetCollection.Where(Function(ref) ref.Id.Equals("autoScoringOffController", StringComparison.InvariantCultureIgnoreCase)).ToList()

            If toRemove IsNot Nothing Then
                toRemove.ForEach(Sub(r)
                                     sol.AspectReferenceSetCollection.Remove(r)
                                 End Sub)
            End If
        End Sub
    End Class
End Namespace