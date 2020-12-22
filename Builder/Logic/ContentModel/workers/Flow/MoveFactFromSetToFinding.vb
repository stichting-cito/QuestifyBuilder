Imports System.Activities

Namespace ContentModel.workers.Flow

    Public NotInheritable Class MoveFactFromSetToFinding
        Inherits CodeActivity

        Property Text() As InArgument(Of String)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim text As String
            text = context.GetValue(Me.Text)
        End Sub
    End Class
End Namespace