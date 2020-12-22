Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveFindings(Of TFinding As {BaseFinding})
        Inherits CodeActivity

        Property Findings As InArgument(Of ICollection(Of TFinding))

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)

            Dim findings = context.GetValue(Me.Findings)

            For Each finding In findings.ToList()
                findings.Remove(finding)
            Next

        End Sub
    End Class

End Namespace