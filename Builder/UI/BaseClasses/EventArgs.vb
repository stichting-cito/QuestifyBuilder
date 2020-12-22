

Imports Questify.Builder.Logic.Service.Model.Entities

Public Class EntityActionEventArgs
    Inherits EventArgs

    Private ReadOnly _selectedEntity As ResourceDto

    Public ReadOnly Property SelectedEntity() As ResourceDto
        Get
            Return Me._selectedEntity
        End Get
    End Property

    Public Sub New(ByVal selectedEntity As ResourceDto)
        Me._SelectedEntity = selectedEntity
    End Sub

End Class

Public Class ResourceIdActionEventArgs
    Inherits EventArgs
    Private ReadOnly _resourceId As String

    Public ReadOnly Property ResourceId() As String
        Get
            Return _resourceId
        End Get
    End Property


    Public Sub New(ByVal resourceId As String)
        Me._resourceId = resourceId
    End Sub
End Class

