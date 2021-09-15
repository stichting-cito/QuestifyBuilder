
Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports System.Drawing.Design
Imports System.IO
Imports System.Linq
Imports System.Windows.Input
Imports Enums
Imports Questify.Builder.Configuration
Imports Janus.Windows.GridEX
Imports Janus.Windows.GridEX.Export
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class GridBase
    Inherits ActionCommandSupportBaseControl

#Region " Fields "
    Const CustombankPropertyColumn As String = "CUSTOMPROPERTYCOLUMN"
    Private ReadOnly _entitiesProhibitedToSelect As New List(Of Guid)
    Private _showDisabledRowsAsGray As Boolean = False
    Private _dataResourceResetting As Boolean = False
    Private _grayedOutItemsSelected As Boolean
    Private _gridContentContextMenuDisabled As Boolean = False
    Private _multipleRecordsSelecting As Boolean = False
    Private _restoringGridSettings As Boolean = False
    Private _settingDataSource As Boolean = False
    Private _listOfBanks As List(Of Integer) = Nothing
    Private _allGroupsCollapsed As Boolean = False
#End Region

#Region " Properties "

    Public Overridable Property ShouldDoSelectionCheck As Boolean = False

    ''' <summary>
    ''' Determines whether the grid collapses all header rows except first
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable ReadOnly Property AutoCollapseHeaderRowsExceptFirst As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overridable ReadOnly Property SettingDataSource As Boolean
        Get
            Return _settingDataSource
        End Get
    End Property
#End Region

#Region " Public Events "

    ''' <summary>
    ''' This event will be raised when the user selects an component in the list
    ''' </summary>
    ''' <remarks></remarks>
    <Description("This event will be raised when the user selects an component in the list"), Category("GridBase events")>
    Public Event EntitySelected As EventHandler(Of EntityActionEventArgs)

    ''' <summary>
    ''' This event will be raised when the user selects one component in the list
    ''' </summary>
    ''' <remarks></remarks>
    <Description("This event will be raised when the user selects more than one component in the list"), Category("GridBase events")>
    Public Event EntitiesSelected As EventHandler(Of EventArgs)

    ''' <summary>
    ''' This event will be raised when a component in the list is 'double clicked'
    ''' </summary>
    ''' <remarks></remarks>
    <Description("This event will be raised when a component in the list is 'double clicked'"), Category("GridBase events")>
    Public Event EntityDblClick As EventHandler(Of EntityActionEventArgs)

    ''' <summary>
    ''' This event will be raised when in the context menu of component the option 'open in second window' is clicked
    ''' </summary>
    ''' <remarks></remarks>
    <Description("This event will be raised when a component in the list is 'double clicked'"), Category("GridBase events")>
    Public Event EditItemInSecondWindow As EventHandler(Of EntityActionEventArgs)

    ''' <summary>
    ''' This event will be raised when a component in the list is 'double clicked'
    ''' </summary>
    ''' <remarks></remarks>
    <Description("This event will be raised when a component in the list is 'double clicked' but does not contain a entity"), Category("GridBase events")>
    Public Event ResourceIdDblClick As EventHandler(Of ResourceIdActionEventArgs)

    ''' <summary>
    ''' This event will be raised when rows in the list are about to be deleted.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("This event will be raised when rows in the list are about to be deleted."), Category("GridBase events")>
    Public Event DeletingRows As EventHandler(Of DeletingRowsEventArgs)

    ''' <summary>
    ''' This event will be raised when rows in the list are deleted.
    ''' </summary>
    <Description("This event will be raised when rows in the list are deleted."), Category("GridBase events")>
    Public Event RowsDeleted As EventHandler(Of RowsDeletedEventArgs)

    ''' <summary>
    ''' This event will be raised when the 'AddNewItem' option in the toolbar is clicked
    ''' </summary>
    <Description("This event will be raised when the 'AddNewItem' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event AddNewItem As EventHandler(Of EventArgs)

    <Description("This event will be raised when the 'AddNewTestPackage' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event AddNewTestPackage As EventHandler(Of EventArgs)

    ''' <summary>
    ''' This event will be raised when the 'SaveAsItem' option in the toolbar is clicked
    ''' </summary>
    <Description("This event will be raised when the 'SaveAsItem' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event SaveAsItem As EventHandler(Of EventArgs)

    ''' <summary>
    ''' This event will be raised when the 'Synchronize' option in the toolbar is clicked
    ''' </summary>
    <Description("This event will be raised when the 'Synchronize' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event SynchronizeItems As EventHandler(Of EventArgs)

    <Description("This event will be raised when the 'SelectAll' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event SelectAll As EventHandler(Of EventArgs)

    <Description("This event will be raised when the 'Export' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event Export As EventHandler(Of EventArgs)

    Public Event SelectedRowChanged As EventHandler(Of EventArgs)

    ''' <summary>
    ''' This event will be raised when the 'Publish' option in the toolbar is clicked
    ''' </summary>
    <Description("This event will be raised when the 'Publish' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event PublishEntity As EventHandler(Of EventArgs)

    <Description("This event will be raised when the 'Reports' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event ShowReport As EventHandler(Of EventArgs)

    ''' <summary>
    ''' This event will be raised when the 'properties' option in the toolbar is clicked or selected in the context menu
    ''' </summary>
    <Description("This event will be raised when the 'Property' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event PropertiesDialogRequested As EventHandler(Of EntityActionEventArgs)

    ''' <summary>
    ''' Occurs when [item parameters requested].
    ''' </summary>
    <Description("This event will be raised when the 'Show item parameters' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event ItemParametersRequested As EventHandler(Of MultipleSelectedEntiesEventArgs)

    ''' <summary>
    ''' Occurs when [Multi-edit].
    ''' </summary>
    <Description("This event will be raised when the 'Multi Edit' option in the contectmenu is clicked"), Category("GridBase events")>
    Public Event MultiEdit As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Occurs when [preview entity].
    ''' </summary>
    <Description("This event will be raised when the 'Preview' option in the contectmenu is clicked"), Category("GridBase events")>
    Public Event PreviewEntity As EventHandler(Of EntityActionEventArgs)

    ''' <summary>
    ''' Occurs when 'Harmonize items' context menu entry is clicked.
    ''' </summary>
    <Description("This event will be raised when the 'Harmonize items' option in the context menu is clicked"), Category("GridBase events")>
    Public Event HarmonizeDependantItems As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Occurs when 'Select for moving' context menu entry is clicked.
    ''' </summary>
    <Description("This event will be raised when the 'Move resource to here...' option in the context menu is clicked"), Category("GridBase events")>
    Public Event MoveResources As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Occurs when 'Toggle Resource Visibility' context menu entry is clicked.
    ''' </summary>
    <Description("This event will be raised when the 'Toggle Resource Visibility' option in the context menu is clicked"), Category("GridBase events")>
    Public Event TogglingResourceVisibility As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Raises the <see cref="E:SynchonizeItems" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnSynchronizeItems(e As EventArgs)
        RaiseEvent SynchronizeItems(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:SelectAll" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnSelectAll(e As EventArgs)
        RaiseEvent SelectAll(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:Export" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnExport(e As EventArgs)
        RaiseEvent Export(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:ShowReport" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnShowReport(e As EventArgs)
        RaiseEvent ShowReport(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:Publish" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnPublishEntity(e As EventArgs)
        RaiseEvent PublishEntity(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:AddNewItem" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnAddNewItem(e As EventArgs)
        RaiseEvent AddNewItem(Me, e)
    End Sub

    Public Sub OnSaveAsItem(e As EventArgs)
        RaiseEvent SaveAsItem(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:AddNewTestPackage" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnAddNewTestPackage(e As EventArgs)
        RaiseEvent AddNewTestPackage(Me, e)
    End Sub
    ''' <summary>
    ''' Raises the <see cref="E:DeletingRows" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="Questify.Builder.UI.DeletingRowsEventArgs" /> instance containing the event data.</param>
    Public Sub OnDeletingRows(e As DeletingRowsEventArgs)
        RaiseEvent DeletingRows(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:RowsDeleted" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="Questify.Builder.UI.RowsDeletedEventArgs" /> instance containing the event data.</param>
    Public Sub OnRowsDeleted(e As RowsDeletedEventArgs)
        RaiseEvent RowsDeleted(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the ComponentSelected event
    ''' </summary>
    ''' <param name="e">The e.</param>
    Public Overridable Sub OnComponentSelected(e As EntityActionEventArgs)
        RaiseEvent EntitySelected(Me, e)
        RaiseContextChanged(Me, e)
        RaiseEvent EntitiesSelected(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the ComponentDblClick event
    ''' </summary>
    ''' <param name="e">The e.</param>
    Public Overridable Sub OnComponentDblClick(e As EntityActionEventArgs)
        RaiseEvent EntityDblClick(Me, e)
        RaiseContextChanged(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="EditItemInSecondWindow" /> event
    ''' </summary>
    ''' <param name="e">The e.</param>
    Public Overridable Sub OnEditSecondItem(e As EntityActionEventArgs)
        RaiseEvent EditItemInSecondWindow(Me, e)
        RaiseContextChanged(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:ComponentDblClickCode" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="Questify.Builder.UI.ResourceIdActionEventArgs" /> instance containing the event data.</param>
    Public Overridable Sub OnComponentDblClickCode(e As ResourceIdActionEventArgs)
        RaiseEvent ResourceIdDblClick(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:ShowProperty" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Overridable Sub OnPropertiesDialogRequested(e As EntityActionEventArgs)
        RaiseEvent PropertiesDialogRequested(Me, e)
    End Sub


    ''' <summary>
    ''' Raises the <see cref="E:ItemParametersRequested" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="Questify.Builder.UI.EntityActionEventArgs" /> instance containing the event data.</param>
    Public Overridable Sub OnItemParametersRequested(e As MultipleSelectedEntiesEventArgs)
        RaiseEvent ItemParametersRequested(Me, e)
    End Sub
    ''' <summary>
    ''' Raises the <see cref="E:Preview" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Overridable Sub OnPreviewEntity(e As EntityActionEventArgs)
        RaiseEvent PreviewEntity(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:HarmonizeDependantItems" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Public Overridable Sub OnHarmonizeDependantItems(e As EventArgs)
        RaiseEvent HarmonizeDependantItems(Me, e)
    End Sub

    Public Overridable Sub OnMoveResource(e As EventArgs)
        RaiseEvent MoveResources(Me, e)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:MultiEdit" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Overridable Sub OnMultiEdit(e As EventArgs)
        RaiseEvent MultiEdit(Me, e)
    End Sub

    ''' <summary>
    ''' Called when [selection changed].
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnSelectionChanged(sender As Object, e As EventArgs)
        RaiseEvent SelectedRowChanged(sender, e)
    End Sub

    ''' <summary>
    ''' Called when toggling the visibility of the resource.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnTogglingResourceVisibility(sender As Object, e As EventArgs)
        RaiseEvent TogglingResourceVisibility(sender, e)
    End Sub
    
    Event FormattingRow As EventHandler(Of RowFormattingEventArgs)

    ''' <summary>
    ''' Raises the <see cref="E:FormattingRow" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="RowFormattingEventArgs"/> instance containing the event data.</param>
    Private Sub OnFormattingRow(e As RowFormattingEventArgs)
        RaiseEvent FormattingRow(Me, e)
        If Me.ShouldDoSelectionCheck AndAlso e.Resource IsNot Nothing AndAlso Not e.Resource.IsSelectable Then
            e.Disabled = True
            If Not _entitiesProhibitedToSelect.Contains(e.Resource.resourceId) Then
                _entitiesProhibitedToSelect.Add(e.Resource.resourceId)
            End If
        End If
    End Sub


#End Region

#Region " Event Passing "

    ''' <summary>
    ''' Handles the ApplyingFilter event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    Private Sub GridControl_ApplyingFilter(sender As Object, e As CancelEventArgs) Handles GridControl.ApplyingFilter
        For i As Integer = 0 To GridControl.FilterRow.Cells.Count - 1
            If GridControl.FilterRow.Cells(i).Text.Length > 0 Then

                If Not GridControl.FilterRow.Cells(i).Text.EndsWith("*") AndAlso GridControl.FilterRow.Cells(i).Column.EditType <> EditType.NoEdit Then
                    GridControl.Row = GridEX.filterRowPosition
                    GridControl.SetValue(i, GridControl.FilterRow.Cells(i).Text & "*")
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Disposes the grid.
    ''' </summary>
    ''' <returns></returns>
    Public Function DisposeGrid() As Boolean
        Try
            StoreGridSettings(SaveSettingType.All, True)
            DataSource = Nothing

            If (_listOfBanks IsNot Nothing) Then
                _listOfBanks.Clear()
                _listOfBanks = Nothing
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Handles the FormattingRow event of the GridControl control
    ''' Counts the childrows.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Janus.Windows.GridEX.RowLoadEventArgs" /> instance containing the event data.</param>
    Private Sub GridControl_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles GridControl.FormattingRow
        If e.Row.RowType = RowType.GroupHeader Then
            'e.Row.GroupCaption += " (" + e.Row.GetRecordCount(e.Row.Table).ToString() + ")"
            e.Row.GroupCaption = FormatGroupHeader(e.Row.GroupCaption, e.Row.GetRecordCount(e.Row.Table))
        End If
        If e.Row.RowType = RowType.Record AndAlso e.Row.DataRow IsNot Nothing AndAlso TypeOf e.Row.DataRow Is ResourceDto Then
            Dim eArgs As New RowFormattingEventArgs()

            eArgs.Resource = DirectCast(e.Row.DataRow, ResourceDto)
            OnFormattingRow(eArgs)

            If (_showDisabledRowsAsGray AndAlso eArgs.Disabled = True) OrElse Not eArgs.Resource.VisibleInPicker Then
                Dim rowStyle As New GridEXFormatStyle
                rowStyle.ForeColor = Color.Gray
                e.Row.RowStyle = rowStyle
            End If
        End If
    End Sub

    ''' <summary>
    ''' Formats the caption of the group header. When a grouping column consists of multiple columns, the caption will be also. The groupHeaderCaption consists of the names of 
    ''' the columns separated by ' - '.
    ''' </summary>
    ''' <param name="groupHeaderCaption"></param>
    ''' <param name="groupRowCount">nr of items in the group</param>
    Private Function FormatGroupHeader(groupHeaderCaption As String, groupRowCount As Integer) As String
        Dim separatedColumnCaptions() As String = groupHeaderCaption.Split(New String() {"-"}, StringSplitOptions.None)

        ' If separatedColumnCaptions.Length > 1 then it is assumed that we're processing a bank group row, we then remove the bank id from the caption by skipping the first element 
        'when constructing the new caption.
        If (separatedColumnCaptions.Length > 1) Then
            groupHeaderCaption = String.Join("-", separatedColumnCaptions, 1, separatedColumnCaptions.Length - 1).TrimStart()
        End If

        ' Include the row count in the group caption.
        Return $"{groupHeaderCaption} ({groupRowCount})"
    End Function

    ''' <summary>
    ''' Handles the RowDoubleClick event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Janus.Windows.GridEX.RowActionEventArgs" /> instance containing the event data.</param>
    Private Sub GridControl_RowDoubleClick(sender As Object, e As RowActionEventArgs) Handles GridControl.RowDoubleClick
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If e.Row.RowType <> RowType.Record Then
            Me.Cursor = System.Windows.Forms.Cursors.Default
        ElseIf TypeOf e.Row.DataRow Is ResourceDto Then
            Dim involvedComponent As ResourceDto = CType(e.Row.DataRow, ResourceDto)
            Me.OnComponentDblClick(New EntityActionEventArgs(involvedComponent))
        ElseIf e.Row.Cells("ResourceId") IsNot Nothing Then
            Dim code As String = e.Row.Cells("ResourceId").Value.ToString
            Me.OnComponentDblClickCode(New ResourceIdActionEventArgs(code))
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub


    ''' <summary>
    ''' Handles the KeyDown event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
    Private Sub GridControl_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridControl.KeyDown
        If e.KeyCode = Keys.Enter Then
            If e.Modifiers = Keys.Alt Then
                EditSecondItem()
            Else
                If GridControl.CurrentRow IsNot Nothing Then
                    If GridControl.Row <> GridEX.filterRowPosition AndAlso GridControl.CurrentRow.DataRow IsNot Nothing Then
                        Dim involvedComponent As ResourceDto = DirectCast(GridControl.CurrentRow.DataRow, ResourceDto)
                        Me.OnComponentDblClick(New EntityActionEventArgs(involvedComponent))
                    End If
                Else
                    MessageBox.Show(My.Resources.NoRecordSelectedToEdit, My.Resources.GridBase_EditNoEntitySelected_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the MouseClick event of the GridControl
    ''' </summary>
    ''' <param name="sender">source of the event</param>
    ''' <param name="e">The e</param>
    Private Sub GridBase_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GridControl.MouseClick
        If (Keyboard.Modifiers And System.Windows.Input.ModifierKeys.Alt) > 0 Then
            EditSecondMenuItem_Click(sender, e)
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyUp event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
    Private Sub GridControl_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridControl.KeyUp
        If e.KeyCode = Keys.Delete AndAlso AllowDelete AndAlso GridControl.Row <> GridEX.filterRowPosition AndAlso
            GridControl.CurrentRow IsNot Nothing AndAlso GridControl.CurrentRow.DataRow IsNot Nothing Then

            Me.Delete()
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the ColumnMoved event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ColumnActionEventArgs"/> instance containing the event data.</param>
    Private Sub GridControl_ColumnMoved(sender As Object, e As ColumnActionEventArgs) Handles GridControl.ColumnMoved
        StoreGridSettings(SaveSettingType.Columns, False)
        StoreGridColumnsOrderSettings(SaveSettingType.ColumnPosition)
    End Sub

    ''' <summary>
    ''' Handles the ColumnVisibleChanged event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ColumnActionEventArgs"/> instance containing the event data.</param>
    Private Sub GridControl_ColumnVisibleChanged(sender As Object, e As ColumnActionEventArgs) Handles GridControl.ColumnVisibleChanged
        If Not _restoringGridSettings Then
            StoreGridSettings(SaveSettingType.Columns, False)
        End If
    End Sub

    ''' <summary>
    ''' Handles the SelectionChanged event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub GridControl_SelectionChanged(sender As Object, e As EventArgs) Handles GridControl.SelectionChanged
        If Not _multipleRecordsSelecting Then
            ' update statusbar
            StatusBarListener.MainInstance.PublishMessage(Me, $"{GridControl.SelectedItems.Count} {My.Resources.StatusBar_RowsSelected}")
            OnComponentSelected(New EntityActionEventArgs(Me.SelectedEntity))

            _grayedOutItemsSelected = False 'Reset disallow adding flag

            For i = 0 To DirectCast(sender, GridEX).SelectedItems.Count - 1
                Dim row = DirectCast(sender, GridEX).SelectedItems(i)
                If row.Table IsNot Nothing Then
                    Dim dataRow = row.GetRow()

                    If dataRow.RowStyle IsNot Nothing AndAlso dataRow.RowStyle.ForeColor.Name = "Gray" Then 'grayed out items may not be inserted.
                        'Don't allow dragging
                        _grayedOutItemsSelected = True
                        Exit For
                    End If
                End If
            Next
            If Not _dataResourceResetting Then
                StoreGridSettings(SaveSettingType.RowPosition, False)
            End If
            OnSelectionChanged(sender, e)
        End If
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether [greyed out items selected].
    ''' </summary>
    ''' <value>
    '''     <c>true</c> if [greyed out items selected]; otherwise, <c>false</c>.
    ''' </value>
    Public ReadOnly Property GreyedOutItemsSelected() As Boolean
        Get
            Return _grayedOutItemsSelected
        End Get
    End Property

#End Region

#Region " Public Properties "

    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return _entitiesProhibitedToSelect
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether custom property columns are visible.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [custom property columns visible]; otherwise, <c>false</c>.
    ''' </value>
    <Description("Indicates whether custom property columns are visible"), Bindable(False), Category("GridBase Specific")>
    Public Property CustomPropertyColumnsVisible As Boolean = False

    ''' <summary>
    ''' Gets or sets the custom property filer.
    ''' </summary>
    ''' <value>The custom property filer.</value>
    <Description("Filter custom properties"), DefaultValue(ResourceTypeEnum.None), Bindable(False), Category("GridBase Specific")>
    Public Property CustomPropertyFilter As ResourceTypeEnum = ResourceTypeEnum.None

    ''' <summary>
    ''' Gets or sets the selected entity in the list.
    ''' </summary>
    <Description("The currently selected component in this list"), Bindable(False), Category("GridBase Specific")>
    Public Property SelectedEntity As ResourceDto
        Get
            If GridControl.SelectedItems.Count > 0 Then
                If TypeOf GridControl.GetRow().DataRow Is ResourceDto Then
                    Return CType(GridControl.GetRow().DataRow, ResourceDto)
                End If
            End If

            Return Nothing
        End Get
        Set
            If Value IsNot Nothing Then
                Dim row As GridEXRow = GridControl.GetRow(Value)
                If Not (row Is Nothing) Then
                    GridControl.MoveTo(row)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Allow the user to filter the list
    ''' </summary>
    ''' <value></value>
    <Description("Allow the user to filter the list"), Bindable(True), Category("GridBase Specific")>
    Public Property EnableFiltering As Boolean
        Get
            Return (GridControl.FilterMode = FilterMode.Automatic)
        End Get
        Set
            If Value Then
                GridControl.FilterMode = FilterMode.Manual
            Else
                GridControl.FilterMode = FilterMode.None
            End If
        End Set
    End Property

    ''' <summary>
    ''' Datasource where the grid binds to.
    ''' </summary>
    ''' <value>The data source.</value>
    <Description("Datasource where the grid binds to"), Bindable(True), Category("Data")>
    <AttributeProvider(GetType(IListSource))>
    Public Property DataSource As Object
        Get
            Return GridBindingSource.DataSource
        End Get
        Set
            _dataResourceResetting = True
            GridControl.ScrollBars = ScrollBars.Automatic
            _entitiesProhibitedToSelect.Clear()

            If Value Is Nothing Then
                ' save location before datasource is resetted
                GridControl.HorizontalScrollPosition = 0
            End If

            Try 'Bug #16050, somehow GridBindingSource gets disposed.
                _settingDataSource = True
                Dim list = TryCast(Value, IList)
                If (list IsNot Nothing AndAlso list.Count > 0 AndAlso TypeOf list(0) Is ResourceDto) Then 'seems to be needed to be able to delete locally
                    GridBindingSource.DataSource = Nothing 'TFS #43036. fixes odd behaviour that when an existing datasource is overwritten directly that data is changed upong assigning it
                    GridBindingSource.DataSource = list.OfType(Of ResourceDto)
                Else
                    GridBindingSource.DataSource = Value
                End If

            Catch ex As ObjectDisposedException
                ' Should not have come here, but since we did, we will silently ignore...
            End Try

            If Value IsNot Nothing AndAlso Not Value.GetType.FullName = "System.RuntimeType" Then
                If CustomPropertyColumnsVisible Then
                    ' Add custom properties to grid 
                    AddCustomBankPropertyColumns()
                End If
                _settingDataSource = False
                _listOfBanks = BankFactory.Instance.GetListOfBankIds()
                RestoreGridSettings()
                If Not GridControl.ColumnAutoResize AndAlso Value IsNot Nothing Then
                    '30-5-2008 - changed - users feedback
                    AutoSizeGrid(False)
                End If
                For Each column As GridEXColumn In GridControl.RootTable.Columns
                    If Not column.AllowRemove = InheritableBoolean.True Then
                        column.AllowRemove = InheritableBoolean.True
                    End If
                Next
            End If
            _dataResourceResetting = False
        End Set
    End Property

    Private Sub CollapseHeaderRowsExceptFirst()
        Dim rowsToCollapse As New List(Of GridEXRow)
        Dim rowToExpand As GridEXRow = Nothing
        'First: determine which row to collapse/expand
        For x = 0 To GridControl.RowCount - 1
            Dim row = GridControl.GetRow(x)
            If row.RowType = RowType.GroupHeader AndAlso row.Group.CustomGroup IsNot Nothing AndAlso row.Group.CustomGroup.Key = "BankIdBankName" Then
                If rowToExpand Is Nothing Then
                    rowToExpand = row
                Else
                    rowsToCollapse.Add(row)
                End If
            End If
        Next

        'Collapse all rows
        For Each row As GridEXRow In rowsToCollapse.Where(Function(r) r.Expanded = True)
            row.Expanded = False
        Next

        'Except the first row
        If rowToExpand IsNot Nothing Then
            rowToExpand.Expanded = True
        End If
    End Sub

    Private ReadOnly Property GridKey As String
        Get
            Dim key = Name
            If Parent IsNot Nothing Then
                key = String.Concat(Parent.Name, key)
            End If
            Return key
        End Get
    End Property

    Private ReadOnly Property UserSettingsAvailable As Boolean
        Get
            Dim settings = UserSettings.GetUserBankSettingsForGrid(ActionCommand.Instance.CurrentBankId, GridKey)
            Return settings.ColumnSettings IsNot Nothing AndAlso settings.ColumnSettings.Count = GridControl.RootTable.Columns.Count
        End Get
    End Property

    ''' <summary>
    ''' Restores the selected item in grid.
    ''' </summary>
    Public Sub RestoreSelectedItemInGrid(guidOfSelectedEntity As String)
        Dim found = False
        If guidOfSelectedEntity <> Guid.Empty.ToString AndAlso Not String.IsNullOrEmpty(guidOfSelectedEntity) Then
            found = RecursiveSearchAndSelect(GridControl.GetRows, guidOfSelectedEntity)
        End If

        If found Then
            Return
        End If

        Dim headerRows = GridControl.GetRows.Where(Function(r) r.RowType = RowType.GroupHeader)
        If headerRows.Any Then
            Dim firstExpandedHeaderRow = headerRows.FirstOrDefault(Function(r) r.Expanded)
            If firstExpandedHeaderRow IsNot Nothing Then
                ' Select the first enabled child of the first expanded header row, if any.
                If firstExpandedHeaderRow.GetChildRows.Any(Function(x) Not (x.RowStyle IsNot Nothing AndAlso x.RowStyle.ForeColor.Name = "Gray")) Then
                    GridControl.MoveTo(firstExpandedHeaderRow.GetChildRows.Where(Function(x) Not (x.RowStyle IsNot Nothing AndAlso x.RowStyle.ForeColor.Name = "Gray")).First)
                End If
            End If
        Else
            ' Select the first row regardless of type, if any. Covers grids that may exist without Header rows.
            GridControl.MoveTo(GridControl.GetRows.FirstOrDefault)
        End If
    End Sub

    ''' <summary>
    ''' Recursive search and select.
    ''' </summary>
    ''' <param name="rows">The rows.</param>
    ''' <param name="searchFor">The search for.</param>
    Private Function RecursiveSearchAndSelect(rows() As GridEXRow, searchFor As String) As Boolean
        For Each row As GridEXRow In rows
            If row Is Nothing Then
                Continue For
            End If
            Dim rowEntity = TryCast(row.DataRow, ResourceDto)
            If rowEntity IsNot Nothing AndAlso rowEntity.resourceId.ToString = searchFor Then
                GridControl.MoveTo(row)
                Return True
            ElseIf row.Children > 0 AndAlso RecursiveSearchAndSelect(row.GetChildRows(), searchFor) Then
                row.Expanded = True
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Is the grid used merely to pick items or is it used to launch all kind of actions on its content.
    ''' </summary>
    <Description("Is the grid used to pick items (True) or to launch various actions on its content (False)"), Category("GridBase Specific")>
    Public Property UseGridAsItemPicker As Boolean

    ''' <summary>
    ''' Datamember inside the datasource what has to be used.
    ''' </summary>
    <Description("Datamember inside the datasource what has to be used"), Bindable(True), Category("Data")>
    <Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property DataMember As String
        Get
            Return GridControl.DataMember
        End Get
        Set
            GridControl.DataMember = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [multi select].
    ''' </summary>
    ''' <value><c>true</c> if [multi select]; otherwise, <c>false</c>.</value>
    <Description("Whether that this grid support multiple selections of entites"), Bindable(True), Category("GridBase Specific"), DefaultValue(False)>
    Public Property MultiSelect As Boolean
        Get
            Return (GridControl.SelectionMode = SelectionMode.MultipleSelection)
        End Get
        Set
            If Value Then
                GridControl.SelectionMode = SelectionMode.MultipleSelection
            Else
                GridControl.SelectionMode = SelectionMode.SingleSelection
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets the selected entities in this grid.
    ''' </summary>
    ''' <value>The selected properties.</value>
    Public ReadOnly Property SelectedEntities As IList(Of ResourceDto)
        Get
            ' ReSharper disable once LocalVariableHidesMember
            Return (From selectedEntity In GridControl.SelectedItems
                    Where DirectCast(selectedEntity, GridEXSelectedItem).GetRow().DataRow IsNot Nothing
                    Select CType(DirectCast(selectedEntity, GridEXSelectedItem).GetRow().DataRow, ResourceDto)).ToList()
        End Get
    End Property

#End Region

#Region " Protected Properties "

    ''' <summary>
    ''' Allows grouping of columns in this grid
    ''' </summary>
    <Description("Allows grouping of columns in this grid"), Bindable(False), Category("GridBase Specific")>
    Protected Overridable ReadOnly Property AllowGrouping() As Boolean
        Get
            Return True
        End Get
    End Property

#End Region

#Region " Grid layout "

    ''' <summary>
    ''' Set layout of grid using Janusys layout XML
    ''' </summary>
    ''' <param name="layoutXML"></param>
    Public Sub SetLayout(layoutXml As String)
        Dim gridEx1DesignTimeLayout = New GridEXLayout
        CType(GridControl, ISupportInitialize).BeginInit()
        SuspendLayout()
        gridEx1DesignTimeLayout.LayoutString = layoutXml
        GridControl.DesignTimeLayout = gridEx1DesignTimeLayout
        ResumeLayout(False)
        CType(GridControl, ISupportInitialize).EndInit()
    End Sub

    Private Sub AutoSizeGrid(storeSettings As Boolean)
        If _settingDataSource OrElse UserSettingsAvailable Then
            Return
        End If

        Dim columWidth = 5
        ' if auto resize is disabled on grid, the columns have the width of the longest text in 
        ' the column plus 5 pixels.
        Dim visibleColumnsWidth As Integer
        Dim visibleColumnsCount As Integer
        GridControl.AutoSizeColumns()
        For Each column As GridEXColumn In GridControl.RootTable.Columns
            column.Width += columWidth
            If column.Visible Then
                visibleColumnsCount += 1
                visibleColumnsWidth += column.Width
            End If
        Next

        If (visibleColumnsCount <= 0) Then
            ' If no visible columns, there is nothing left to do.
            Return
        End If

        If visibleColumnsWidth < GridControl.Width - 100 Then
            For Each column As GridEXColumn In GridControl.RootTable.Columns
                Dim newWidth As Integer = (GridControl.Width \ visibleColumnsCount) - (columWidth \ visibleColumnsCount)
                If newWidth > column.Width Then
                    column.Width = newWidth
                End If
            Next
        End If

        If storeSettings Then
            StoreGridSettings(SaveSettingType.Columns, True)
        End If
    End Sub

#End Region

#Region " Constructors / Destructors "

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        
    End Sub

#End Region

#Region " Implementation for IActionCommands "

    ''' <summary>
    ''' Allows the context menu cell.
    ''' </summary>
    ''' <param name="parentFormName">Name of the parent form.</param><returns></returns>
    Public Overrides Function AllowContextMenuCell(parentFormName As String) As Boolean
        Return True
    End Function

    ''' <summary>
    ''' Allows the context menu header.
    ''' </summary>
    ''' <param name="parentFormName">Name of the parent form.</param><returns></returns>
    Public Overrides Function AllowContextMenuHeader(parentFormName As String) As Boolean
        Return True
    End Function

    ''' <summary>
    ''' Gets a value indicating whether [allow reports].
    ''' </summary>
    Public Overrides ReadOnly Property AllowReports() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [show disabled rows as gray].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [show disabled rows as gray]; otherwise, <c>false</c>.
    ''' </value>
    Public Overridable Property ShowDisabledRowsAsGray As Boolean
        Get
            Return _showDisabledRowsAsGray
        End Get
        Set
            _showDisabledRowsAsGray = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets a value indicating whether [select all].
    ''' </summary>
    ''' <value><c>true</c> if [select all]; otherwise, <c>false</c>.</value>
    Public Overrides ReadOnly Property AllowSelectAll() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property AllowMoveResources() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Deletes the selected.
    ''' </summary>
    Public Overrides Sub Delete()
        If GridControl.SelectedItems.Count = 0 Then
            MessageBox.Show(My.Resources.GridBase_DeleteNoEntitySelected_Text, My.Resources.GridBase_DeleteNoEntitySelected_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' create list of selected rows
        Dim selectedRows As New List(Of GridEXRow)
        Dim resourcesToDelete As New List(Of ResourceDto)

        For Each row As GridEXSelectedItem In GridControl.SelectedItems
            If row.RowType <> RowType.Record Then
                Continue For
            End If

            Dim resource = DirectCast(row.GetRow().DataRow, ResourceDto)

            If DeleteIsPermitted(resource.bankId) Then
                selectedRows.Add(row.GetRow())
                resourcesToDelete.Add(resource)
            End If
        Next

        If selectedRows.Count = 0 Then
            Return
        End If

        Dim beforeEventArgs As New DeletingRowsEventArgs(GridControl.SelectedItems)

        OnDeletingRows(beforeEventArgs)

        If beforeEventArgs.Cancel Then
            Return
        End If

        'show hourglass
        Dim currentCursor As Windows.Forms.Cursor = Windows.Forms.Cursor.Current
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        ' raise event to delete rows on datasource.
        Dim args As New RowsDeletedEventArgs(resourcesToDelete)
        OnRowsDeleted(args)

        ' delete rows locally
        For Each row As GridEXRow In selectedRows
            If args.RowsFailedToDelete IsNot Nothing AndAlso args.RowsFailedToDelete.Any(Function(r) r.resourceId = DirectCast(row.DataRow, ResourceDto).resourceId) Then
                Continue For
            End If
            row.Delete()
        Next
        'reset cursor
        Windows.Forms.Cursor.Current = currentCursor
    End Sub

    ''' <summary>
    ''' Synchronizes this instance.
    ''' </summary>
    Public Overrides Sub Synchronize()
        OnSynchronizeItems(New EventArgs)
    End Sub

    ''' <summary>
    ''' Synchronizes the items that are dependant on the currently selected resources. Only known use so far is synchronizing items and item layout templates.
    ''' </summary>
    Public Overrides Sub Harmonize()
        OnHarmonizeDependantItems(New EventArgs)
    End Sub

    Public Overrides Sub MoveTheResources()
        OnMoveResource(New EventArgs)
    End Sub

    ''' <summary>
    ''' Selects all rows.
    ''' </summary>
    Public Overrides Sub SelectAllRows()
        Me.GridControl.SelectedItems.Clear()
        SelectRows(GridControl.GetRows)
    End Sub

    ''' <summary>
    ''' Selects the rows.
    ''' </summary>
    ''' <param name="rows">The rows.</param>
    Private Sub SelectRows(rows As GridEXRow())
        For Each row As GridEXRow In rows
            If row.Children > 0 Then
                SelectRows(row.GetChildRows)
            Else
                _multipleRecordsSelecting = Not (row.Position = GridControl.RowCount)
                Me.GridControl.SelectedItems.Add(row.Position)
                _multipleRecordsSelecting = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Hides the field chooser.
    ''' </summary>
    Public Sub HideFieldChooser()
        If Me.GridControl IsNot Nothing Then
            Me.GridControl.HideFieldChooser()
        End If
    End Sub

    ''' <summary>
    ''' Edits this instance.
    ''' </summary>
    Public Overrides Sub Edit()
        If GridControl.CurrentRow IsNot Nothing Then
            If GridControl.Row <> GridEX.filterRowPosition AndAlso GridControl.CurrentRow.DataRow IsNot Nothing Then
                Dim involvedComponent As ResourceDto = DirectCast(GridControl.CurrentRow.DataRow, ResourceDto)
                Me.OnComponentDblClick(New EntityActionEventArgs(involvedComponent))
            End If
        Else
            MessageBox.Show(My.Resources.NoRecordSelectedToEdit, My.Resources.GridBase_EditNoEntitySelected_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    ''' <summary>
    ''' Edits this instance in a second window
    ''' </summary>
    Public Sub EditSecondItem()
        If GridControl.CurrentRow IsNot Nothing Then
            If GridControl.Row <> GridEX.filterRowPosition AndAlso GridControl.CurrentRow.DataRow IsNot Nothing Then
                Dim involvedComponent As ResourceDto = DirectCast(GridControl.CurrentRow.DataRow, ResourceDto)
                Me.OnEditSecondItem(New EntityActionEventArgs(involvedComponent))
            End If
        Else
            MessageBox.Show(My.Resources.NoRecordSelectedToEdit, My.Resources.GridBase_EditNoEntitySelected_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    <SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")>
    Public Overrides Sub ExportResources()
        OnExport(New EventArgs)
    End Sub

    ''' <summary>
    ''' Shows the report wizard.
    ''' </summary>
    Public Overrides Sub ShowReportWizard()
        OnShowReport(New EventArgs)
    End Sub

    ''' <summary>
    ''' Shows the property info.
    ''' </summary>
    Public Overrides Sub ShowProperties()
        Dim selected = Me.SelectedEntity
        If selected IsNot Nothing Then
            OnPropertiesDialogRequested(New EntityActionEventArgs(selected))
        End If
    End Sub

    ''' <summary>
    ''' Toggles the visibility of the resource.
    ''' </summary>
    ''' <param name="bankId">The id of the bank to toggle the visibility of the resources for.</param>
    ''' <remarks>Also toggles visibility of the resources for sub banks.</remarks>
    Public Overrides Function ToggleResourceVisibility(bankId As Integer) As Boolean
        Return False
    End Function

#End Region

#Region " Private methods "

    ''' <summary>
    ''' Handles the Click event of the SelectColumnsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub SelectColumnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectColumnsToolStripMenuItem.Click
        Dim form As Form = FindParentForm(Me)
        Debug.Assert(form IsNot Nothing, "Unable to determine form this grid is placed on.")
        GridControl.ShowFieldChooser(form, SelectColumnsToolStripMenuItem.Text)
    End Sub

    ''' <summary>
    ''' Finds the parent form this grid is placed on. This method should be used for getting
    ''' owner form of dialogs shown by this grid control.
    ''' </summary>
    ''' <param name="ctrl">The CTRL.</param><returns></returns>
    Private Function FindParentForm(ctrl As Control) As Form
        If TypeOf ctrl Is Form Then
            Return CType(ctrl, Form)
        ElseIf ctrl.Parent Is Nothing Then
            Return Nothing
        Else
            Return FindParentForm(ctrl.Parent)
        End If
    End Function


    ''' <summary>
    ''' Handles the Click event of the PropertiesToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub PropertiesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PropertiesToolStripMenuItem.Click
        If Me.SelectedEntity IsNot Nothing Then
            OnPropertiesDialogRequested(New EntityActionEventArgs(Me.SelectedEntity))
        End If
    End Sub

    Private Enum SaveSettingType
        ColumnPosition
        RowPosition
        Columns
        Filter
        All
    End Enum

    ''' <summary>
    ''' Preserves the current row location.
    ''' </summary>
    Private Sub StoreGridSettings(type As SaveSettingType, appendOnly As Boolean)
        Dim settings = UserSettings.GetUserBankSettingsForGrid(ActionCommand.Instance.CurrentBankId, GridKey)

        If type = SaveSettingType.RowPosition OrElse type = SaveSettingType.All Then
            If SelectedEntity IsNot Nothing Then
                Try
                    settings.SelectedRowIdentifier = SelectedEntity.resourceId.ToString
                Catch e As ORMEntityIsDeletedException
                    Return
                End Try
            End If

            settings.ScrollBarPosition = GridControl.VerticalScrollPosition
        End If

        If type = SaveSettingType.Columns OrElse type = SaveSettingType.All Then
            SaveColumnSettings(settings, appendOnly)
        End If

        If type = SaveSettingType.Filter OrElse type = SaveSettingType.All Then
            SaveFilterSettings(settings, appendOnly)
        End If

        Dim listOfBanks = BankFactory.Instance.GetListOfBankIds()
        UserSettings.StoreUserBankSettings(listOfBanks)
        listOfBanks = Nothing
    End Sub

    Private Sub SaveFilterSettings(settings As GridSettings, appendOnly As Boolean)
        If GridControl.FilterRow Is Nothing Then
            Return
        End If

        For i = 0 To GridControl.FilterRow.Cells.Count - 1
            ' index is declared here for use in the lambda expresion as using the iteration variable i directly may have unexpected results.
            Dim index = i
            Dim filterText As String = GridControl.FilterRow.Cells(i).Text

            If NewFilterTextDiffersFromSavedColumnFilterText(filterText, GridControl.FilterRow.Cells(index).Column.Key, settings.ColumnSettings) Then
                If (settings.ColumnSettings Is Nothing OrElse Not appendOnly) Then
                    settings.ColumnSettings = New List(Of ColumnSettings)

                    For Each column As GridEXColumn In GridControl.RootTable.Columns
                        If appendOnly AndAlso settings.ColumnSettings.Any(Function(cs) cs.ColumnIdentifier.Equals(column.Key, StringComparison.InvariantCultureIgnoreCase)) Then
                            'ColumnSetting already exists, do not add
                        Else
                            Dim newColumnSetting = GetColumnSetting(column)
                            settings.ColumnSettings.Add(newColumnSetting)
                        End If
                    Next
                End If

                Dim columnSetting = settings.ColumnSettings.FirstOrDefault(Function(c) c.ColumnIdentifier = GridControl.FilterRow.Cells(index).Column.Key)
                columnSetting.Filter = filterText
            End If
        Next
    End Sub

    Private Sub SaveColumnSettings(settings As GridSettings, appendOnly As Boolean)
        If settings.ColumnSettings Is Nothing OrElse Not appendOnly Then
            settings.ColumnSettings = New List(Of ColumnSettings)
        End If

        For Each column As GridEXColumn In GridControl.RootTable.Columns
            If appendOnly AndAlso settings.ColumnSettings.Any(Function(cs) cs.ColumnIdentifier.Equals(column.Key, StringComparison.InvariantCultureIgnoreCase)) Then
                'ColumnSetting already exists, do not add
            Else
                Dim columnSetting As ColumnSettings = GetColumnSetting(column)
                settings.ColumnSettings.Add(columnSetting)
            End If
        Next
    End Sub

    Private Shared Function GetColumnSetting(column As GridEXColumn) As ColumnSettings

        Dim columnSetting As New ColumnSettings(column.Key)
        columnSetting.IsGrouped = column.IsGrouped
        If (column.Group IsNot Nothing) Then
            columnSetting.SortGroupAscending = (column.Group.SortOrder = SortOrder.Ascending)
            columnSetting.GroupPosition = column.Group.Index - 1
        End If
        columnSetting.Visible = column.Visible
        columnSetting.Width = column.Width
        columnSetting.IsSorted = column.IsSorted
        columnSetting.SortAscending = (column.SortOrder = SortOrder.Ascending)
        Return columnSetting
    End Function

    ''' <summary>
    ''' Store the current grid column order into the user-settings file property
    ''' </summary>
    Private Sub StoreGridColumnsOrderSettings(saveSettingType As SaveSettingType)
        'checking operation type.
        If (saveSettingType = SaveSettingType.ColumnPosition) Then
            'get all entities from the user-settings (only return entities where bankId still exists in database)
            If _listOfBanks Is Nothing Then
                _listOfBanks = BankFactory.Instance.GetListOfBankIds()
            End If
            Dim allColumnOrderSettings = StoreSettings(Of GridColumnOrderSettings).GetEntities().Where(Function(e) _listOfBanks.Contains(e.BankId)).ToList()

            If (allColumnOrderSettings IsNot Nothing) Then
                'get the entities for the current Bank Id and Grid Type.
                Dim currentColumnOrderSettingsList = allColumnOrderSettings.Where(Function(x) x.BankId = ActionCommand.Instance.CurrentBankId AndAlso x.GridType = Me.GetType().ToString()).ToList()

                'remove all existing entities for the current Grid-Bank Id from the main list.
                For Each currentColumn As GridColumnOrderSettings In currentColumnOrderSettingsList
                    Dim entity = allColumnOrderSettings.Single(Function(x) x.BankId = ActionCommand.Instance.CurrentBankId AndAlso x.GridType = Me.GetType().ToString() AndAlso x.ColumnKey = currentColumn.ColumnKey)
                    allColumnOrderSettings.Remove(entity)
                Next

                For Each col As GridEXColumn In GridControl.RootTable.Columns
                    Dim newEntity As New GridColumnOrderSettings

                    'if the current grid doesnt exist in the user-settings we will use the default position.
                    If currentColumnOrderSettingsList.Count = 0 Then
                        newEntity.ColumnPosition = col.Index
                    Else
                        'when the current grid exist in the user-settings use the new position.
                        newEntity.ColumnPosition = col.Position
                    End If

                    newEntity.BankId = ActionCommand.Instance.CurrentBankId
                    newEntity.ColumnIndex = col.Index
                    newEntity.GridType = Me.GetType().ToString()
                    newEntity.ColumnKey = col.Key
                    allColumnOrderSettings.Add(newEntity)
                Next
                'save the entities into the user-settings
                StoreSettings(Of GridColumnOrderSettings).StoreEntities(allColumnOrderSettings)
            End If

        End If
    End Sub

    Private Function NewFilterTextDiffersFromSavedColumnFilterText(newFilterText As String, columnKey As String, columnSettingsList As List(Of ColumnSettings)) As Boolean
        Dim colSettings As ColumnSettings = Nothing

        If columnSettingsList IsNot Nothing Then
            colSettings = columnSettingsList.FirstOrDefault(Function(c) c.ColumnIdentifier = columnKey)
        End If

        If String.IsNullOrEmpty(newFilterText) Then
            If colSettings IsNot Nothing Then
                Return Not String.IsNullOrEmpty(colSettings.Filter)
            End If
        Else
            Return colSettings Is Nothing OrElse Not newFilterText.Equals(colSettings.Filter)
        End If

        Return False
    End Function

    ''' <summary>
    ''' Restores the current row location.
    ''' </summary>
    Private Sub RestoreGridSettings()
        Dim settings = UserSettings.GetUserBankSettingsForGrid(ActionCommand.Instance.CurrentBankId, GridKey)
        Dim sortKeys As New List(Of GridEXSortKey)
        _restoringGridSettings = True

        ' Reset the grouping on the grid. (Clear all groups but keep the default grouping.)
        If GridControl.RootTable IsNot Nothing Then
            If GridControl.RootTable.Groups IsNot Nothing AndAlso GridControl.RootTable.Groups.Count > 0 Then
                Dim defaultGroup As GridEXGroup = GridControl.RootTable.Groups(0)
                GridControl.RootTable.Groups.Clear()
                GridControl.RootTable.Groups.Add(defaultGroup)
            End If

            If settings.ColumnSettings IsNot Nothing AndAlso GridControl.RootTable.Columns IsNot Nothing Then
                Dim columnsEqual = settings.ColumnSettings.Count = GridControl.RootTable.Columns.Count
                For Each column As GridEXColumn In GridControl.RootTable.Columns
                    Dim columnSetting = settings.ColumnSettings.FirstOrDefault(Function(c) c.ColumnIdentifier = column.Key)

                    If columnSetting IsNot Nothing Then
                        ' If the number of columns is equal to the number of columns in the setting, set the width of the column.
                        If columnsEqual Then
                            column.Width = columnSetting.Width
                        End If

                        ' Check if the column is sorted.
                        If columnSetting.IsSorted Then
                            ' Add a new sort key for this column.
                            sortKeys.Add(New GridEXSortKey(column, DirectCast(IIf(columnSetting.SortAscending, SortOrder.Ascending, SortOrder.Descending), SortOrder)))
                        End If

                        If column.Caption = "Item id" AndAlso Not ItemIdHelper.UseItemId Then
                            'remove  rather than set invisible so that it won't show in the SelectColumns screen
                            Me.GridControl.RootTable.Columns.Remove(column)
                        Else
                            column.Visible = columnSetting.Visible
                        End If
                    End If
                Next

                ' Check for and display grouped columns 
                Dim groupedColumns = settings.ColumnSettings.Where(Function(cs) cs.IsGrouped).OrderBy(Function(cs) cs.GroupPosition).ToList()
                groupedColumns.ForEach(Sub(cs)
                                           If (GridControl.RootTable.Columns.Item(cs.ColumnIdentifier) IsNot Nothing) Then
                                               GridControl.RootTable.Groups.Add(GridControl.RootTable.Columns.Item(cs.ColumnIdentifier), DirectCast(IIf(cs.SortGroupAscending, SortOrder.Ascending, SortOrder.Descending), SortOrder))
                                               GridControl.GroupByBoxVisible = True
                                           End If
                                       End Sub)

                ' Check if the grid is filtered.
                If settings.IsFiltered Then
                    ' If the filter row is nothing filter mode is turned off, so turn it on.
                    If GridControl.FilterRow Is Nothing Then
                        GridControl.FilterMode = FilterMode.Automatic
                    End If

                    ' Set the row to the filter row.
                    GridControl.Row = GridEX.filterRowPosition

                    ' Set the values for the filter row cells.
                    For i = 0 To GridControl.FilterRow.Cells.Count - 1
                        ' index is declared here for use in the lambda expresion as using the iteration variable i directly may have unexpected results.
                        Dim index = i
                        Dim columnSetting = settings.ColumnSettings.FirstOrDefault(Function(c) c.ColumnIdentifier = GridControl.FilterRow.Cells(index).Column.Key)

                        If Not String.IsNullOrEmpty(columnSetting.Filter) Then
                            GridControl.SetValue(i, columnSetting.Filter)
                        End If
                    Next

                    ' Apply the filter.
                    GridControl.UpdateData()
                Else
                    'Hide the FilterRow if no column is being filtered.
                    If GridControl.FilterRow IsNot Nothing Then
                        GridControl.FilterMode = FilterMode.None
                    End If
                End If
            Else
                AutoSizeGrid(True)
            End If

            ' Apply sorting.
            If sortKeys.Count > 0 Then
                GridControl.RootTable.SortKeys.Clear()

                For Each sortKey As GridEXSortKey In sortKeys
                    GridControl.RootTable.SortKeys.Add(sortKey)
                Next
            End If
        End If

        If AutoCollapseHeaderRowsExceptFirst Then
            CollapseHeaderRowsExceptFirst()
        End If

        RestoreSelectedItemInGrid(settings.SelectedRowIdentifier)
        RestoreGridColumnOrder()
        GridControl.VerticalScrollPosition = settings.ScrollBarPosition
        _restoringGridSettings = False
    End Sub

    ''' <summary>
    ''' Restore Grid columns order
    ''' </summary>
    Private Sub RestoreGridColumnOrder()
        'Check if there are grid column settings for banks that are no longer in the database. 
        'In that case, call StoreGridColumnsOrderSettings to reset the collection
        If _listOfBanks Is Nothing Then
            _listOfBanks = BankFactory.Instance.GetListOfBankIds()
        End If
        If StoreSettings(Of GridColumnOrderSettings).GetEntities().Any(Function(e) Not _listOfBanks.Contains(e.BankId)) Then
            StoreGridColumnsOrderSettings(SaveSettingType.ColumnPosition)
        End If

        'get all the entities from the user-settings for the current bank id and grid type
        Dim gridColumnsOrderList = StoreSettings(Of GridColumnOrderSettings).GetEntities().
            Where(Function(x) x.BankId = ActionCommand.Instance.CurrentBankId AndAlso x.GridType = Me.GetType().ToString()).ToList()

        'if the current grid exist in the user-settings then update the column order grid.
        If (gridColumnsOrderList.Count <> 0) Then
            UpdateGridColumnOrder(gridColumnsOrderList)
        Else
            'if the current grid doesnt exist in the user-settings. then add the current grid information to the user-settings
            StoreGridColumnsOrderSettings(SaveSettingType.ColumnPosition)

            'defence: checking again just in case if an other process has cleared the user-settings
            gridColumnsOrderList = StoreSettings(Of GridColumnOrderSettings).GetEntities().
                Where(Function(x) x.BankId = ActionCommand.Instance.CurrentBankId AndAlso x.GridType = Me.GetType().ToString()).ToList()
            UpdateGridColumnOrder(gridColumnsOrderList)
        End If
    End Sub

    ''' <summary>
    ''' Update the column order on within the current grid.
    ''' </summary>
    ''' <param name="gridColumnsOrderList"></param>
    Private Sub UpdateGridColumnOrder(gridColumnsOrderList As List(Of GridColumnOrderSettings))
        If gridColumnsOrderList IsNot Nothing Then
            For Each gridColumnOrder As GridColumnOrderSettings In gridColumnsOrderList
                'chacking if the saved column exist in the grid.
                'checking if the user-settings column position is in range of the current grid.
                If (GridControl.RootTable.Columns.Contains(gridColumnOrder.ColumnKey) AndAlso GridControl.RootTable.Columns.Count > gridColumnOrder.ColumnPosition) Then
                    'set column position
                    GridControl.RootTable.Columns(gridColumnOrder.ColumnKey).Position = gridColumnOrder.ColumnPosition
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Adds the custom bank property columns.
    ''' </summary>
    Private Sub AddCustomBankPropertyColumns()

        Dim currentBankId As Integer
        currentBankId = ActionCommand.Instance.CurrentBankId

        If currentBankId > 0 Then
            Dim customProperties = DtoFactory.CustomBankProperty.GetCustomBankPropertiesForBranchWithFilter(currentBankId, CustomPropertyFilter.ToString)

            ' Remove existing custom property columns
            Dim columnsToRemove As IEnumerable(Of String) = (From column In GridControl.RootTable.Columns
                                                             Where DirectCast(column, GridEXColumn).Tag IsNot Nothing AndAlso DirectCast(column, GridEXColumn).Tag.ToString = CustombankPropertyColumn
                                                             Select DirectCast(column, GridEXColumn).Key).ToList()

            For Each key As String In columnsToRemove.OrderByDescending(Function(c) c)
                If GridControl.RootTable.Columns.Contains(key) Then
                    GridControl.RootTable.Columns.Remove(key)
                End If
            Next

            ' Add custom property columns for current datasource and bank context
            For Each customPropertyDto In customProperties
                Dim newColumn As New GridEXColumn
                With newColumn
                    .Key = customPropertyDto.customBankPropertyId.ToString
                    .Caption = $"[{customPropertyDto.name}]"
                    .ColumnType = ColumnType.Text
                    .DataMember = customPropertyDto.name
                    .BoundMode = ColumnBoundMode.UnboundFetch
                    .Tag = CustombankPropertyColumn
                End With
                GridControl.RootTable.Columns.Add(newColumn)
            Next
        End If
    End Sub


    ''' <summary>
    ''' Handles the LoadingRow event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Janus.Windows.GridEX.RowLoadEventArgs" /> instance containing the event data.</param>
    Protected Overridable Sub GridControl_LoadingRow(sender As Object, e As RowLoadEventArgs) Handles GridControl.LoadingRow
        If CustomPropertyColumnsVisible AndAlso e.Row.RowType = RowType.Record Then
            Dim resourceDto = TryCast(e.Row.DataRow, ResourceDto)

            For Each cell As GridEXCell In e.Row.Cells
                If cell.Column.BoundMode = ColumnBoundMode.UnboundFetch Then
                    AddCustomPropertyValues(resourceDto, cell)
                End If
            Next
        End If

        'Don() 't have an event that notifies us loading of all rows is completed.
        ' This is somewhat of a workaround, because some double fireing may occur.
        ' Note : recordcount is used because this count works correcly combined with
        '        filtering.
        Dim grid = DirectCast(sender, GridEX)
        If e.Row.Position = grid.RecordCount Then
            AutoSizeGrid(True)
        End If
    End Sub

    Public Sub AddCustomPropertyValues(resourceDto As ResourceDto, cell As GridEXCell)
        If resourceDto IsNot Nothing AndAlso resourceDto.CustomPropertyDisplayValues IsNot Nothing Then
            Dim customBankPropertyId As Guid
            If (Guid.TryParse(cell.Column.Key, customBankPropertyId)) Then
                Dim value = resourceDto.CustomPropertyDisplayValues.Where(Function(c) c.CustomPropertyId = customBankPropertyId).FirstOrDefault
                If value IsNot Nothing Then
                    cell.Value = value.DisplayValue
                ElseIf cell.Value IsNot Nothing Then
                    cell.Value = String.Empty
                End If
            End If
        End If
    End Sub

    Private Sub GroupingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GroupingToolStripMenuItem.Click
        GroupingToolStripMenuItem.Checked = Not GroupingToolStripMenuItem.Checked

        GridControl.GroupByBoxVisible = Me.GroupingToolStripMenuItem.Checked
    End Sub

    Private Sub CollapseGroupsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CollapseGroupsToolStripMenuItem.Click
        If (_allGroupsCollapsed) Then
            ' Expand all groups.
            GridControl.ExpandGroups()

            _allGroupsCollapsed = False
        Else
            ' Collapse all groups.
            GridControl.CollapseGroups()
            ' Open the first group if this is the bank group.
            Dim firstGroup As GridEXRow = GridControl.GetRows().FirstOrDefault()
            If (firstGroup IsNot Nothing AndAlso firstGroup.RowType = RowType.GroupHeader AndAlso
                firstGroup.Group.CustomGroup IsNot Nothing AndAlso firstGroup.Group.CustomGroup.Key = "BankIdBankName") Then
                firstGroup.Expanded = True
            End If

            _allGroupsCollapsed = True
        End If
    End Sub

    Private Sub GridControl_GroupsChanged(sender As Object, e As EventArgs) Handles GridControl.GroupsChanged
        _allGroupsCollapsed = False
    End Sub

    Private Sub GridControl_RowExpanded(sender As Object, e As EventArgs) Handles GridControl.RowExpanded
        _allGroupsCollapsed = False
    End Sub

    Private Sub FilteringToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilteringToolStripMenuItem.Click
        FilteringToolStripMenuItem.Checked = Not FilteringToolStripMenuItem.Checked

        If FilteringToolStripMenuItem.Checked Then
            GridControl.FilterMode = FilterMode.Automatic

            For i = 0 To GridControl.FilterRow.Cells.Count - 1
                If GridControl.FilterRow.Cells(i).Column.EditType <> EditType.TextBox Then
                    GridControl.FilterRow.Cells(i).Column.FilterEditType = FilterEditType.TextBox
                End If
            Next

        Else
            GridControl.FilterMode = FilterMode.None
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyPress event of the GridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs" /> instance containing the event data.</param>
    Private Sub GridControl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridControl.KeyPress
        Const ascValueCtrlA As Integer = 1
        Debug.WriteLine(Asc(e.KeyChar))

        Dim visibleRecords As New List(Of Integer)
        For i = 0 To GridControl.RowCount - 1
            Dim currentRow = GridControl.GetRow(i)
            If currentRow.RowType = RowType.Record AndAlso currentRow.IsVisible Then
                visibleRecords.Add(i)
            End If
        Next

        'make sure that when the last row was already selected the SelectionChanged event will still fire, by clearing current selection
        'while clearing selection do not fire the event
        _multipleRecordsSelecting = True
        Me.GridControl.SelectedItems.Clear()
        _multipleRecordsSelecting = False

        If Asc(e.KeyChar) = ascValueCtrlA AndAlso GridControl.SelectionMode <> SelectionMode.SingleSelection Then
            For Each record In visibleRecords
                _multipleRecordsSelecting = Not (record = visibleRecords.Last) 'only 'false' for last selected row
                GridControl.SelectedItems.Add(record)
                _multipleRecordsSelecting = False
            Next
        End If

    End Sub

#End Region

#Region " Context Menu event handlers "

    ''' <summary>
    ''' Handles the Click event of the EditToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        If EditIsPermitted(SelectedEntity.bankId) Then
            Edit()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the EditSecondToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub EditSecondMenuItem_Click(sender As Object, e As EventArgs) Handles EditSecondMenuItem.Click
        If EditIsPermitted(SelectedEntity.bankId) Then
            EditSecondItem()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the MultiEditToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub MultiEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiEditToolStripMenuItem.Click
        If MultiEditIsPermitted(SelectedEntity.bankId) Then
            OnMultiEdit(New EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ReportsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub ReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.Click
        OnShowReport(New EventArgs)
    End Sub

    ''' <summary>
    ''' Handles the Click event of the DeleteToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Delete()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the PreviewToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub PreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreviewToolStripMenuItem.Click
        If PreviewIsPermitted(SelectedEntity.bankId) Then
            Preview(SelectedEntity)
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the PublishToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub PublishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PublishToolStripMenuItem.Click
        Dim allowedForall = True

        For Each resource As ResourceDto In SelectedEntities
            If Not PublishIsPermitted(resource.bankId) Then
                allowedForall = False
                Exit For
            End If
        Next

        If allowedForall Then
            Publish()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ShowParametersInGridToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click
        ExportResources()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the FastSearchToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub FastSearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FastSearchToolStripMenuItem.Click
        ToggleFastSearchBar()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the SyncDependantItemsToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub SyncDependantItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HarmonizeDependentItemsToolStripMenuItem.Click
        Harmonize()
    End Sub

    Private Sub MoveResourceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoveResourceToolStripMenuItem.Click
        MoveTheResources()
    End Sub


    ''' <summary>
    ''' Sets all items to not visible
    ''' </summary>
    Private Sub SetAllMenuItemsToNotVisible()
        For Each item In ReportsContextMenuStrip.Items
            If (TypeOf item Is ToolStripMenuItem) Then
                DirectCast(item, ToolStripMenuItem).Visible = False
            ElseIf (TypeOf item Is ToolStripSeparator) Then
                DirectCast(item, ToolStripSeparator).Visible = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Makes it possible to switch of the opening of the rightclick contextmenu. Rightclick on header or groupcolumn will work.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property GridContentContextMenuDisabled() As Boolean
        Get
            Return _gridContentContextMenuDisabled
        End Get
        Set
            _gridContentContextMenuDisabled = Value
        End Set
    End Property


    ''' <summary>
    ''' Handles the Opening event of the ContextMenuStrip1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ReportsContextMenuStrip.Opening
        'Start by making all context menu items invisible.
        SetAllMenuItemsToNotVisible()
        If (GridControl.HitTest() = GridArea.ColumnHeader _
         OrElse GridControl.HitTest() = GridArea.GroupByBox _
         OrElse GridControl.HitTest() = GridArea.FilterRow) AndAlso AllowContextMenuHeader(Me.ParentForm.Name) Then

            SelectColumnsToolStripMenuItem.Visible = True
            FilteringToolStripMenuItem.Visible = True
            GroupingToolStripMenuItem.Visible = True
            CollapseGroupsToolStripMenuItem.Visible = GroupingToolStripMenuItem.Checked

            If AllowFastSearch Then
                FastSearchToolStripMenuItem.Visible = True
                FastSearchToolStripMenuItem.Enabled = True
                FilteringToolStripSeparator.Visible = True
            End If
        Else
            If Not (_gridContentContextMenuDisabled) Then
                If GridControl.HitTest() = GridArea.Cell AndAlso AllowContextMenuCell(ParentForm.Name) Then
                    If GridControl.CurrentRow IsNot Nothing AndAlso GridControl.CurrentRow.DataRow IsNot Nothing Then
                        Dim gridContainsSelectedRows As Boolean = GridControl.SelectedItems.Count > 0
                        EditToolStripMenuItem.Visible = gridContainsSelectedRows AndAlso AllowEdit
                        EditToolStripMenuItem.Enabled = EditIsPermitted(ActionCommand.Instance.CurrentBankId)

                        EditSecondMenuItem.Visible = gridContainsSelectedRows AndAlso AllowEdit AndAlso MultiEditIsPermitted(ActionCommand.Instance.CurrentBankId)
                        EditSecondMenuItem.Enabled = EditIsPermitted(ActionCommand.Instance.CurrentBankId)

                        DeleteToolStripMenuItem.Visible = gridContainsSelectedRows AndAlso AllowDelete
                        DeleteToolStripMenuItem.Enabled = DeleteIsPermitted(ActionCommand.Instance.CurrentBankId)

                        Dim harmonizePermitted As Boolean = gridContainsSelectedRows AndAlso AllowHarmonizeDependantItems AndAlso HarmonizeDependantItemsIsPermitted(ActionCommand.Instance.CurrentBankId)
                        HarmonizeDependentItemsToolStripMenuItem.Visible = harmonizePermitted
                        HarmonizeDependentItemsToolStripMenuItem.Enabled = harmonizePermitted

                        Dim showProperties As Boolean = gridContainsSelectedRows AndAlso AllowShowProperties
                        PropertiesToolStripMenuItem.Visible = showProperties
                        PropertiesSeparator.Visible = showProperties
                        PropertiesToolStripMenuItem.Enabled = ShowPropertiesIsPermitted(ActionCommand.Instance.CurrentBankId)

                        PreviewToolStripMenuItem.Visible = gridContainsSelectedRows AndAlso AllowPreview
                        PreviewToolStripMenuItem.Enabled = PreviewIsPermitted(ActionCommand.Instance.CurrentBankId)

                        MultiEditToolStripMenuItem.Visible = MultiEditIsPermitted(ActionCommand.Instance.CurrentBankId)
                        MultiEditToolStripMenuItem.Enabled = MultiEditIsPermitted(ActionCommand.Instance.CurrentBankId)
                        MultiEditToolStripSeparator.Visible = MultiEditIsPermitted(ActionCommand.Instance.CurrentBankId)

                        Dim exportPermitted As Boolean = ExportIsPermitted(ActionCommand.Instance.CurrentBankId)
                        ExportToolStripMenuItem.Visible = exportPermitted
                        ExportToolStripMenuItem.Enabled = exportPermitted
                        ExportSeparator.Visible = (gridContainsSelectedRows AndAlso AllowPreview) OrElse exportPermitted OrElse (gridContainsSelectedRows AndAlso AllowPublish) OrElse
                            ShowReportIsPermitted(ActionCommand.Instance.CurrentBankId)

                        ReportsToolStripMenuItem.Visible = ShowReportIsPermitted(ActionCommand.Instance.CurrentBankId)
                        ReportsToolStripMenuItem.Enabled = ShowReportIsPermitted(ActionCommand.Instance.CurrentBankId)

                        PublishToolStripMenuItem.Visible = gridContainsSelectedRows AndAlso AllowPublish
                        PublishToolStripMenuItem.Enabled = PublishIsPermitted(ActionCommand.Instance.CurrentBankId)

                        MoveResourceToolStripMenuItem.Visible = AllowMoveResources
                        MoveResourceToolStripMenuItem.Enabled = MoveResourcesIsPermitted(ActionCommand.Instance.CurrentBankId)

                        ToggleVisibilityMenuItem.Visible = AllowToggleResourceVisibility
                        ToggleVisibilityMenuItem.Enabled = ToggleResourceVisibilityIsPermitted(ActionCommand.Instance.CurrentBankId)
                        ToggleVisibilitySeparator.Visible = AllowToggleResourceVisibility
                    End If
                Else
                    e.Cancel = True
                End If
            End If
        End If

        If GridControl.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the click event of the ToggleVisibilityMenuItem.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToggleVisibilityMenuItem_Click(sender As Object, e As EventArgs) Handles ToggleVisibilityMenuItem.Click
        OnTogglingResourceVisibility(sender, e)
    End Sub

    ''' <summary>
    ''' Exports to excel.
    ''' </summary>
    Public Overrides Sub ExportToExcel()
        Try
            Using gridExExporter As New GridEXExporter
                gridExExporter.GridEX = GridControl
                gridExExporter.IncludeExcelProcessingInstruction = True
                gridExExporter.IncludeFormatStyle = False
                gridExExporter.IncludeHeaders = True
                gridExExporter.IncludeChildTables = False
                gridExExporter.IncludeCollapsedRows = True
                If Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) Then
                    SaveExcelFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                End If

                Dim result As DialogResult = SaveExcelFileDialog.ShowDialog()
                Dim fileName As String
                If result = DialogResult.OK Then
                    fileName = SaveExcelFileDialog.FileName
                    Dim stream = New FileStream(fileName, FileMode.Create)
                    gridExExporter.Export(stream)
                    stream.Flush()
                    stream.Close()
                    If MessageBox.Show(My.Resources.WouldYouLikeToOpenTheFileInExcelNow, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Try
                            Process.Start(fileName)
                        Catch noExcelInstalledException As Win32Exception
                            MessageBox.Show(My.Resources.ThereIsNoApplicationAssociatedToThisFileExcelIsProbablyNotInstalled, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            MessageBox.Show(My.Resources.ErrorThrown + ex.ToString())
                        End Try
                    End If
                End If
            End Using
        Catch ioException As IOException
            MessageBox.Show(My.Resources.ErrorWhileExportingToExcelFileMightBeInUse, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
#End Region

    Private Sub GridControl_SortKeysChanged(sender As Object, e As EventArgs) Handles GridControl.SortKeysChanged
        StoreGridSettings(SaveSettingType.Columns, False)
    End Sub

    Private Sub GridControl_FilterApplied(sender As Object, e As EventArgs) Handles GridControl.FilterApplied
        StoreGridSettings(SaveSettingType.Filter, False)
    End Sub

    Private Sub GridControl_GroupsChanged(sender As Object, e As GroupsChangedEventArgs) Handles GridControl.GroupsChanged
        StoreGridSettings(SaveSettingType.Columns, False)
    End Sub
End Class
