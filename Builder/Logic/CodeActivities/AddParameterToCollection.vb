Imports System.Activities
Imports Cito.Tester.ContentModel

Namespace CodeActivities

    Public NotInheritable Class AddParameterToCollection
        Inherits CodeActivity
        Public Property ParameterCollection As InArgument(Of ParameterCollection)

        Public Property ParameterToAdd As InArgument(Of ParameterBase)

        Protected Overrides Sub Execute(context As CodeActivityContext)
            Dim parCollection As ParameterCollection = context.GetValue(ParameterCollection)
            Dim toAdd as ParameterBase = context.GetValue(ParameterToAdd)

            parCollection.InnerParameters.Add(toAdd)
        End Sub
    End Class
End Namespace
