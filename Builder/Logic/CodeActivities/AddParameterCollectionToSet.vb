Imports System.Activities
Imports Cito.Tester.ContentModel

Namespace CodeActivities

    Public NotInheritable Class AddParameterCollectionToSet
        Inherits CodeActivity
        Public Property Collection As InArgument(Of ParameterCollection)

        Public Property ToSet As InArgument(Of ParameterSetCollection)

        Protected Overrides Sub Execute(context As CodeActivityContext)
            Dim parsCollection As ParameterCollection = context.GetValue(Of ParameterCollection)(Collection)
            Dim parSetCollection As ParameterSetCollection = context.GetValue(Of ParameterSetCollection)(ToSet)

            parSetCollection.Add(parsCollection)
        End Sub
    End Class
End Namespace
