Imports System.ComponentModel
Imports Questify.Builder.Security
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic.Service.HelperFunctions

Public Class ItemGrid

    Private _gridInFilterMode As Boolean
    Private _filterArgs As FastSearchInitiatedEventArgs

    <Description("This event will be raised when the search bar visibility is toggled by the user."), Category("Itemgrid specific events")>
    Public Event SearchBarVisibilityToggled As EventHandler

    Protected Sub OnSearchBarVisibilityToggled(ByVal e As EventArgs)
        RaiseEvent SearchBarVisibilityToggled(Me, e)
    End Sub

    Public Overrides ReadOnly Property AllowAddNew() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides Function AllowContextMenuCell(ByVal parentFormName As String) As Boolean
        If parentFormName = "SelectItemResourceDialog" Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Overrides Function AllowContextMenuHeader(ByVal parentFormName As String) As Boolean
        Return True
    End Function

    Public Overrides Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AddNew, TestBuilderPermissionTarget.ItemEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowEdit() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function MultiEditIsPermitted(ByVal bankId As Integer) As Boolean
        Return ExecuteIsPermitted(bankId)
    End Function

    Public Overrides Function EditIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowEdit AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.ItemEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowExecute() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function ExecuteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowExecute AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.ItemEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowShowProperties() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides ReadOnly Property AllowharmonizeDependantItems As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function HarmonizeDependantItemsIsPermitted(bankid As Integer) As Boolean
        Return AllowharmonizeDependantItems AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ImportRawData, TestBuilderPermissionTarget.ItemEntity, bankid)
    End Function

    Public Overrides Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowShowProperties AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ViewProperties, TestBuilderPermissionTarget.ItemEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowDelete() As Boolean = Not UseGridAsItemPicker

    Public Overrides Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowDelete AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.ItemEntity, bankId)
    End Function

    Public Overrides Sub AddNew()
        OnAddNewItem(New EventArgs)
    End Sub

    Public Overrides ReadOnly Property AllowSynchronize() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property


    Public Overrides Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowSynchronize
    End Function

    Public Overrides ReadOnly Property AllowMoveResources As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property AllowFastSearch() As Boolean
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property FilterArgsForFastSearch() As FastSearchInitiatedEventArgs
        Get
            Return _filterArgs
        End Get
    End Property

    Public ReadOnly Property IsGridInFilterMode() As Boolean
        Get
            Return _gridInFilterMode
        End Get
    End Property

    Protected Overrides ReadOnly Property AutoCollapseHeaderRowsExceptFirst As Boolean
        Get
            Return Not IsGridInFilterMode
        End Get
    End Property

    Public Property SearchToolbarVisibility() As Boolean
        Get
            Return ItemSearchControl.Visible
        End Get
        Set(ByVal value As Boolean)
            ItemSearchControl.Visible = value

            OnSearchBarVisibilityToggled(New EventArgs())

            If Not ItemSearchControl.Visible AndAlso _gridInFilterMode = True Then
                _gridInFilterMode = False

                OnSynchronizeItems(New EventArgs)
            Else
                ItemSearchControl.SearchForTextBox.Focus()
            End If

            ItemSearchControl.Reset()
        End Set
    End Property


    Public Sub InitializeSearchBar(ByVal bankId As Integer)
        If Not _gridInFilterMode Then
            ItemSearchControl.Initialize(bankId)
        End If
    End Sub

    Public Overrides Sub ToggleFastSearchBar()
        Me.SearchToolbarVisibility = Not Me.SearchToolbarVisibility
    End Sub

    Public Sub ResetSearchBar()
        _gridInFilterMode = False
        _filterArgs = Nothing

        ItemSearchControl.Reset()
    End Sub

    Private Sub ItemSearchControl_FastSearchInitiated(ByVal sender As Object, ByVal e As FastSearchInitiatedEventArgs) Handles ItemSearchControl.FastSearchInitiated
        _gridInFilterMode = True
        _filterArgs = e

        OnSynchronizeItems(New EventArgs)
    End Sub

    Private Sub ItemSearchControl_ClearedSearchStatement(ByVal sender As Object, ByVal e As EventArgs) Handles ItemSearchControl.ClearedSearchStatement
        _gridInFilterMode = False
        _filterArgs = Nothing

        OnSynchronizeItems(New EventArgs)
    End Sub

    Public Overrides Function ExportIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.ItemEntity, bankId)
    End Function

    Private Sub ItemGrid_SearchBarVisibilityToggled(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SearchBarVisibilityToggled
        FastSearchToolStripMenuItem.Checked = Me.SearchToolbarVisibility
    End Sub

    Private Sub GridControl_RowDrag(ByVal sender As Object, ByVal e As RowDragEventArgs) Handles GridControl.RowDrag
        If GreyedOutItemsSelected Then
            Exit Sub
        End If

        If Not (GridControl.SelectedItems Is Nothing) Then
            GridControl.DoDragDrop(GridControl.SelectedItems, DragDropEffects.Move)
        End If
    End Sub

    Public Sub New()

        InitializeComponent()

        If Not ItemIdHelper.UseItemId Then
            Dim column = Me.GridControl.RootTable.Columns.Item("ItemId")
            Me.GridControl.RootTable.Columns.Remove(column)
        End If
    End Sub
End Class
