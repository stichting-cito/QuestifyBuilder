Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Janus.Windows.GridEX

Public Class UserGrid

    <Description("This event will be raised when a new user is selected in the grid"), Category("Usergrid events")> _
    Public Event UserSelectionChanged As EventHandler(Of EventArgs(Of EntityClasses.UserEntity))

    <Description("This event will be raised when when double clicking on a gridrow (user)"), Category("Usergrid events")> _
    Public Event RowDoubleClick As EventHandler(Of EventArgs(Of EntityClasses.UserEntity))

    Protected Sub OnUserSelectionChanged(ByVal e As EventArgs(Of EntityClasses.UserEntity))
        RaiseEvent UserSelectionChanged(Me, e)
    End Sub

    Protected Sub OnRowDoubleClick(ByVal e As EventArgs(Of EntityClasses.UserEntity))
        RaiseEvent RowDoubleClick(Me, e)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), _
    Browsable(False), Bindable(False)> _
    Public Property DataSource() As EntityCollection
        Get
            Dim data As EntityCollection = Nothing
            If UserGridControl.DataSource IsNot Nothing AndAlso TypeOf UserGridControl.DataSource Is EntityCollection Then
                data = DirectCast(UsersBindingSource.DataSource, EntityCollection)
            End If
            Return data
        End Get
        Set(ByVal value As EntityCollection)
            UsersBindingSource.DataSource = value
        End Set
    End Property

    <Browsable(False), Bindable(False)> _
    Public Property SelectedUser() As EntityClasses.UserEntity
        Get
            Dim row As GridEXRow = UserGridControl.GetRow()
            If row IsNot Nothing Then
                Return DirectCast(row.DataRow, EntityClasses.UserEntity)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As EntityClasses.UserEntity)
            If value IsNot Nothing Then
                For Each row As GridEXRow In UserGridControl.GetRows()
                    If DirectCast(row.DataRow, EntityClasses.UserEntity).Id = value.Id Then
                        UserGridControl.MoveTo(row)
                        Exit For
                    End If
                Next
            End If
        End Set
    End Property

    Public Overrides Sub Refresh()
        UserGridControl.Refetch()
        MyBase.Refresh()
    End Sub

    Private Sub UserGridControl_ApplyingFilter(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UserGridControl.ApplyingFilter

        For i As Integer = 0 To UserGridControl.FilterRow.Cells.Count - 1

            If UserGridControl.FilterRow.Cells(i).Text.Length > 0 Then

                If Not UserGridControl.FilterRow.Cells(i).Text.EndsWith("*") Then
                    UserGridControl.FilterRow.Cells(i).Value = UserGridControl.FilterRow.Cells(i).Text & "*"
                End If
            End If

        Next

    End Sub

    Private Sub UserGridControl_FilterApplied(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserGridControl.FilterApplied

    End Sub

    Private Sub UserGridControl_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserGridControl.SelectionChanged
        If Me.SelectedUser IsNot Nothing Then
            OnUserSelectionChanged(New EventArgs(Of EntityClasses.UserEntity)(Me.SelectedUser))
        End If
    End Sub

    Private Sub UserGridControl_SortKeysChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserGridControl.SortKeysChanged
        Dim sortKey As GridEXSortKey = UserGridControl.Tables(0).SortKeys(0)
        UserGridControl.Tables(0).SortKeys.Clear()
        UserGridControl.Tables(0).SortKeys.Add("IsActive", SortOrder.Descending)
        UserGridControl.Tables(0).SortKeys.Add(sortKey)
    End Sub

    Private Sub UserGridControl_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles UserGridControl.RowDoubleClick
        If Me.SelectedUser IsNot Nothing Then
            OnRowDoubleClick(New EventArgs(Of EntityClasses.UserEntity)(Me.SelectedUser))
        End If
    End Sub
End Class
