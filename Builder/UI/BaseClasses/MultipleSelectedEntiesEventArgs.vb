Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class MultipleSelectedEntiesEventArgs
    Inherits EventArgs

    Private _SelectedEntities As ObjectModel.ReadOnlyCollection(Of IEntity2)

    Public ReadOnly Property SelectedEntities() As ObjectModel.ReadOnlyCollection(Of IEntity2)
        Get
            Return Me._SelectedEntities
        End Get
    End Property

    Public Sub New(ByVal selectedEntities As ObjectModel.ReadOnlyCollection(Of IEntity2))
        Me._SelectedEntities = selectedEntities
    End Sub
End Class
