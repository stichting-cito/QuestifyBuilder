
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class RowsDeletedEventArgs
    Inherits EventArgs

    Public Property RowsFailedToDelete As IList(Of ResourceDto)

    Public ReadOnly Property DataSource As Object

    Public Sub New(dataSource As Object)
        Me.DataSource = dataSource
    End Sub
End Class