Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

Namespace ItemEditor
    Friend NotInheritable Class GroupInfluenceFactory
        Private Sub New()
        End Sub
        Public Shared Function CreateFor(parameter As ParameterBase) As IEnumerable(Of IGroupInfluence)
            Dim list As New List(Of IGroupInfluence)()
            If parameter.IsGroupConditionalEnabled() Then
                list.Add(New GroupConditionalEnabled(parameter))
            End If
            Return list
        End Function
    End Class
End Namespace
