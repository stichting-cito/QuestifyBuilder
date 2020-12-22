Imports Questify.Builder.Security
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class CustomPropertyGrid

    Public Overrides ReadOnly Property AllowMoveResources As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property AllowSynchronize() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AddNew, TestBuilderPermissionTarget.CustomBankPropertyEntity, bankId)
    End Function

    Public Overrides Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowDelete AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.CustomBankPropertyEntity, bankId)
    End Function

    Public Overrides Function EditIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.CustomBankPropertyEntity, bankId)
    End Function

    Public Overrides Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowShowProperties AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ViewProperties, TestBuilderPermissionTarget.CustomBankPropertyEntity, bankId)
    End Function

    Public Overrides Sub AddNew()
        OnAddNewItem(New EventArgs)
    End Sub

    Public Overrides Sub Delete()
        If GridControl.SelectedItems.Count > 0 Then
            Dim selectedRows As New List(Of GridEXRow)

            Dim selectedEntitiesCollection As New List(Of ResourceDto)

            For Each row As GridEXSelectedItem In GridControl.SelectedItems
                If row.RowType = RowType.Record Then
                    selectedRows.Add(row.GetRow())
                    selectedEntitiesCollection.Add(DirectCast(row.GetRow().DataRow, ResourceDto))
                End If
            Next

            If selectedRows.Count > 0 Then
                Dim beforeEventArgs As New DeletingRowsEventArgs(GridControl.SelectedItems)

                OnDeletingRows(beforeEventArgs)
                If Not beforeEventArgs.Cancel Then
                    Dim currentCursor As Cursor = Cursor.Current
                    Cursor.Current = Cursors.WaitCursor
                    Dim args As New RowsDeletedEventArgs(selectedEntitiesCollection)
                    OnRowsDeleted(args)

                    For Each row As GridEXRow In selectedRows
                        If Not args.RowsFailedToDelete Is Nothing AndAlso args.RowsFailedToDelete.Contains(DirectCast(row.DataRow, ResourceDto)) Then
                            Continue For
                        End If
                        row.Delete()
                    Next
                    Cursor.Current = currentCursor
                End If
            End If
        Else
            MessageBox.Show(My.Resources.GridBase_DeleteNoEntitySelected_Text, My.Resources.GridBase_DeleteNoEntitySelected_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

End Class
