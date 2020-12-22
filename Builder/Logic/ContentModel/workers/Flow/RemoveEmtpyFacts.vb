Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveEmptyFacts
        Inherits CodeActivity

        Property Facts As InArgument(Of IList(Of BaseFact))

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim facts = context.GetValue(Me.Facts)

            For Each fact As BaseFact In facts.ToList()
                If (isEmpty(fact)) Then facts.Remove(fact)
            Next

        End Sub

        Private Function isEmpty(fact As BaseFact) As Boolean
            Return Not fact.Values.Any()
        End Function

    End Class
End Namespace