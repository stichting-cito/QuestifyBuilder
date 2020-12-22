Imports System.ComponentModel
Imports System.Linq

Public Class SelectColumns

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property SelectedColumns() As IList(Of String)
        Get
            Return If(TypeOf SelectedColumnsBindingSource.DataSource.DataSource Is SelectColumnsOptionsValidator, _
                        DirectCast(Me.SelectedColumnsBindingSource.DataSource.DataSource, SelectColumnsOptionsValidator).SelectedColumns.ToList(), _
                        New List(Of String)())
        End Get
        Set(values As IList(Of String))
            Me.AvailableColumnsListView.SuspendLayout()
            Me.SelectedColumnsListBox.SuspendLayout()

            Me.AvailableColumnsListView.Items.Clear()

            For Each item As String In Me._AvailableColumnsBindingSource.List
                If Me.AvailableColumnsListView.Items(item) Is Nothing Then
                    Me.AvailableColumnsListView.Items.Add(item).Name = item
                End If
            Next

            Dim tmpList = New List(Of String)(Me.SelectedColumnsBindingSource.List.Count)
            tmpList.AddRange(Me.SelectedColumnsBindingSource.List)
            For Each columnItem As String In tmpList
                Me.SelectedColumnsBindingSource.List.Remove(columnItem)
                If Me.AvailableColumnsListView.Items(columnItem) Is Nothing Then
                    Me.AvailableColumnsListView.Items.Add(columnItem).Name = columnItem
                End If
            Next

            For Each columnName As String In values
                Dim columnItem As ListViewItem = Me.AvailableColumnsListView.Items(columnName)
                If columnItem IsNot Nothing Then
                    Me.AvailableColumnsListView.Items.Remove(columnItem)
                    Me.AvailableColumnsBindingSource.Remove(columnItem.Text)
                    Me.SelectedColumnsBindingSource.Add(columnItem.Text)
                End If
            Next

            Me.AvailableColumnsListView.ResumeLayout()
            Me.SelectedColumnsListBox.ResumeLayout()
        End Set
    End Property


    Private Sub MoveItem(src As BindingSource, moveIndex As Integer)
        If src.Current IsNot Nothing Then
            Dim index As Integer = src.IndexOf(src.Current)
            Dim swap As Object = src.Item(index)
            src.RemoveAt(index)
            src.Insert(index + moveIndex, swap)

            SelectedColumnsListBox.SelectedItems.Clear()
            SelectedColumnsListBox.SelectedItem = index + moveIndex
        End If
    End Sub

    Private Sub SelectColumns_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.AvailableColumnsListView.Focus()
        InitializeAvailableListViewControl()
    End Sub

    Private Sub SelectColumnButton_Click(sender As Object, e As EventArgs) Handles SelectColumnButton.Click
        AvailableColumnsListView.Sorting = SortOrder.None
        Dim columnsToMove As List(Of String) = New List(Of String)

        Dim itemToSelect As ListViewItem = GetItemToSelect()

        For Each columnName As ListViewItem In AvailableColumnsListView.SelectedItems
            columnsToMove.Add(columnName.Text)
            AvailableColumnsListView.Items.Remove(columnName)
        Next
        For i As Integer = 0 To columnsToMove.Count - 1
            AvailableColumnsBindingSource.Remove(columnsToMove.Item(i))
            SelectedColumnsBindingSource.Add(columnsToMove.Item(i))
        Next

        SelectedColumnsListBox.SelectedItems.Clear()
        SelectedColumnsListBox.SelectedIndex = (SelectedColumnsListBox.Items.Count - 1)

        If SelectedColumnsListBox.Items.Count = 1 Then
            SelectedColumnsListBox_SelectedIndexChanged(Nothing, Nothing)
        End If

        If Me.ErrorProvider.GetError(SelectedColumnsListBox) IsNot Nothing Then
            Me.ErrorProvider.SetError(SelectedColumnsListBox, String.Empty)
        End If

        AvailableColumnsListView.Focus()
        If (AvailableColumnsListView.Items.Count > 0 AndAlso Not itemToSelect.Equals(New ListViewItem)) Then
            AvailableColumnsListView.Items(AvailableColumnsListView.Items.IndexOf(itemToSelect)).Selected = True
            AvailableColumnsListView.Select()
        End If

    End Sub

    Private Function GetItemToSelect() As ListViewItem
        If (AvailableColumnsListView.Items.Count <> AvailableColumnsListView.SelectedItems.Count) Then
            If (AvailableColumnsListView.Items.Count = 1) Then Return AvailableColumnsListView.Items(0)

            Dim selectedColumns As IEnumerable(Of ListViewItem) = AvailableColumnsListView.SelectedItems.Cast(Of ListViewItem)()
            Dim availableColumns As IEnumerable(Of ListViewItem) = AvailableColumnsListView.Items.Cast(Of ListViewItem)()
            Dim index As Integer = availableColumns.Except(selectedColumns).Min(Function(c) c.Index)

            If (index >= AvailableColumnsListView.Items.Count) Then
                index = AvailableColumnsListView.SelectedItems(0).Index - 1
            End If
            Return AvailableColumnsListView.Items(index)
        End If
        Return New ListViewItem
    End Function

    Private Sub DeselectColumnButton_Click(sender As Object, e As EventArgs) Handles DeSelectColumnButton.Click
        If (SelectedColumnsListBox.SelectedItems.Count > 0) Then
            AvailableColumnsListView.Sorting = SortOrder.None
            Dim columnsToMove As List(Of String) = New List(Of String)

            For Each columnName As String In SelectedColumnsListBox.SelectedItems
                columnsToMove.Add(columnName)
            Next

            For i As Integer = 0 To columnsToMove.Count - 1
                SelectedColumnsBindingSource.Remove(columnsToMove.Item(i))
                AvailableColumnsListView.Items.Add(columnsToMove.Item(i)).Name = columnsToMove.Item(i)
                AvailableColumnsBindingSource.Add(columnsToMove.Item(i))
            Next

            AvailableColumnsListView.SelectedItems.Clear()
            If (columnsToMove.Any()) Then
                Dim itemToSelect As ListViewItem = AvailableColumnsListView.FindItemWithText(columnsToMove.Last())
                AvailableColumnsListView.Items(itemToSelect.Index).Selected = True
                AvailableColumnsListView.Select()
            End If
            SelectedColumnsListBox.Focus()
        End If
    End Sub

    Private Sub MoveUpButton_Click(sender As Object, e As EventArgs) Handles MoveUpButton.Click
        Dim selectedColumnIndex As Integer = SelectedColumnsListBox.SelectedIndex
        MoveItem(CType(SelectedColumnsListBox.DataSource, BindingSource), -1)
        If SelectedColumnsListBox.Items(selectedColumnIndex - 1) IsNot Nothing Then
            SelectedColumnsListBox.SelectedIndex = selectedColumnIndex - 1
        End If
    End Sub

    Private Sub MoveDownButton_Click(sender As Object, e As EventArgs) Handles MoveDownButton.Click
        Dim selectedColumnIndex As Integer = SelectedColumnsListBox.SelectedIndex
        MoveItem(CType(SelectedColumnsListBox.DataSource, BindingSource), 1)
        If SelectedColumnsListBox.Items(selectedColumnIndex + 1) IsNot Nothing Then
            SelectedColumnsListBox.SelectedIndex = selectedColumnIndex + 1
        End If
    End Sub

    Private Sub SelectedColumnsListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectedColumnsListBox.SelectedIndexChanged
        MoveUpButton.Enabled = Not (SelectedColumnsListBox.SelectedIndex = 0) AndAlso SelectedColumnsListBox.SelectedItems.Count = 1
        MoveDownButton.Enabled = Not (SelectedColumnsListBox.SelectedIndex = (SelectedColumnsListBox.Items.Count - 1)) AndAlso SelectedColumnsListBox.SelectedItems.Count = 1
    End Sub

    Public Sub SelectColumns_Validating(sender As Object, e As CancelEventArgs) Handles Me.Validating

        If sender Is Nothing _
                OrElse sender.ParentForm.ActiveControl Is Nothing _
                OrElse sender.ParentForm.ActiveControl.CausesValidation Then
            Dim datasource As SelectColumnsOptionsValidator = DirectCast(OptionsValidatorBindingSource.Current, SelectColumnsOptionsValidator)
            datasource.NumberOfSelectedColumns = SelectedColumnsListBox.Items.Count

            Dim errorMessage As String = datasource.Item("SelectedColumns")
            If Not String.IsNullOrEmpty(errorMessage) Then
                ErrorProvider.SetError(SelectedColumnsListBox, errorMessage)

                e.Cancel = True
            End If

            If Not e.Cancel Then
                Me.OnValidated(e)
            End If
        End If
    End Sub

    Private Sub AvailableColumnsListView_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles AvailableColumnsListView.ColumnClick
        If AvailableColumnsListView.Sorting = SortOrder.Ascending Then
            AvailableColumnsListView.Sorting = SortOrder.Descending
        Else
            AvailableColumnsListView.Sorting = SortOrder.Ascending
        End If

        AvailableColumnsListView.Sort()
    End Sub

    Private Sub InitializeAvailableListViewControl()
        For Each column As String In Me.SelectedColumns
            If Me.AvailableColumnsBindingSource.Contains(column) Then
                Me.AvailableColumnsBindingSource.Remove(column)

                If Not Me.SelectedColumnsBindingSource.Contains(column) Then
                    Me.SelectedColumnsBindingSource.Add(column)
                End If
            End If
        Next

        Me.SelectedColumnsListBox.SelectedItems.Clear()
        Me.SelectedColumnsListBox.SelectedIndex = (Me.SelectedColumnsListBox.Items.Count - 1)

        AvailableColumnsListView.View = View.Details
        AvailableColumnsListView.Columns.Add(My.Resources.ResourceManager.GetString("Sort"), AvailableColumnsListView.Width, HorizontalAlignment.Center)

        Me.AvailableColumnsListView.Items.Clear()
        For Each item As String In _AvailableColumnsBindingSource.List
            If Me.AvailableColumnsListView.Items(item) Is Nothing Then
                Me.AvailableColumnsListView.Items.Add(item).Name = item
            End If
        Next

        If AvailableColumnsListView.Items.Count > 0 Then
            AvailableColumnsListView.Items(0).Selected = True
            AvailableColumnsListView.Select()
        End If

    End Sub


End Class
