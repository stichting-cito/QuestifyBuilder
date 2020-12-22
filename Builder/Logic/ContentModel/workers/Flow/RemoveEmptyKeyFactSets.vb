Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveEmptyKeyFactSets(Of TFinding As {KeyFinding})
        Inherits CodeActivity

        Property Finding As InArgument(Of TFinding)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim finding = context.GetValue(Me.Finding)

            For Each keySet In finding.KeyFactsets.ToList()

                If (keySet.Facts.Count = 0) Then
                    finding.KeyFactsets.Remove(keySet)
                End If

            Next
        End Sub


    End Class
End Namespace
