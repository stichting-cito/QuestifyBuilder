Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ResourceEventArgs
    Inherits EventArgs

    Public Property Resource() As ResourceDto

    Public Sub New(ByVal resource As ResourceDto)
        Me.Resource = resource
    End Sub

    Public Sub New()

    End Sub

End Class

