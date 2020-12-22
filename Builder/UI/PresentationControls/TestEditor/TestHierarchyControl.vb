Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Text
Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Janus.Windows.GridEX
Imports Janus.Windows.GridEX.Export


Public Class TestHierarchyControl


    Private _assessmentTestModel As AssessmentTest2
    Private _lastExpandedRowState As List(Of ExpandedState)
    Private _testComponentDeleteEnabled As Boolean



    <Description("This event will be raised when the user selects a component in the testhierarcy"), Category("TestHierarchyControl events")>
    Public Event TestComponentSelected As EventHandler(Of TestComponentSelectedEventArgs)

    Protected Sub OnTestComponentSelected(e As TestComponentSelectedEventArgs)
        RaiseEvent TestComponentSelected(Me, e)
    End Sub

    <Description("This event will be raised when the control needs additional resource, that can be handled by an external resourcemanager"), Category("TestHierarchyControl events")>
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Protected Sub OnResourceNeeded(e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub

    <Description("This event will be raised when the user selects a row, but it is not applied."), Category("TestHierarchyControl events")>
    Public Event TestComponentSelectedChanging As EventHandler(Of CurrentTestComponentSelectedChangingEventArgs)

    Protected Sub OnTestComponentSelectedChanging(e As CurrentTestComponentSelectedChangingEventArgs)
        RaiseEvent TestComponentSelectedChanging(Me, e)
    End Sub

    <Description("This event will be raised when the user drops selected items from the item selector dialog"), Category("TestHierarchyControl events")>
    Public Event DragDropItem As EventHandler(Of SelectedItemCollectionEventArgs)

    <Description("This event will be raised when the user selects a component in the testhierarcy"), Category("TestHierarchyControl events")>
    Public Event TestComponentDoubleClicked As EventHandler(Of TestComponentSelectedEventArgs)

    Protected Sub OnTestComponentDoubleClicked(e As TestComponentSelectedEventArgs)
        RaiseEvent TestComponentDoubleClicked(Me, e)
    End Sub


    Public Class AddAssessmentItemEventArgs
        Inherits EventArgs

        Private ReadOnly _parentSection As TestSection2
        Private ReadOnly _baseItem As ItemReference2
        Private ReadOnly _append As Boolean


        Public Sub New(parentSection As TestSection2, baseItem As ItemReference2, append As Boolean)
            _parentSection = parentSection
            _baseItem = baseItem
            _append = append
        End Sub

        Public ReadOnly Property ParentSection As TestSection2
            Get
                Return _parentSection
            End Get
        End Property

        Public ReadOnly Property BaseItem As ItemReference2
            Get
                Return _baseItem
            End Get
        End Property

        Public ReadOnly Property Append As Boolean
            Get
                Return _append
            End Get
        End Property

    End Class

    Public Class ToggleLockedForEditEventArgs
        Inherits EventArgs

        Private _locked As Boolean

        Public Sub New(locked As Boolean)
            _locked = locked
        End Sub

        Public ReadOnly Property Locked As Boolean
            Get
                Return _locked
            End Get
        End Property
    End Class

    <Description("This event will be raised when the user selects 'add testpart' in the context menu"), Category("TestHierarchyControl events")>
    Public Event AddTestPartMenuClick As EventHandler

    <Description("This event will be raised when the user selects 'add section' in the context menu"), Category("TestHierarchyControl events")>
    Public Event AddSectionMenuClick As EventHandler

    <Description("This event will be raised when the user selects 'add item', 'insert before'or 'insert after' in the context menu"), Category("TestHierarchyControl events")>
    Public Event AddAssessmentItem As EventHandler(Of AddAssessmentItemEventArgs)

    <Description("This event will be raised when the user selects 'add items from code', 'insert before'or 'insert after' in the context menu"), Category("TestHierarchyControl events")>
    Public Event AddItemsFromCode As EventHandler

    <Description("This event will be raised when the user selects 'delete test component' in the context menu"), Category("TestHierarchyControl events")>
    Public Event DeleteTestComponentMenuClick As EventHandler

    <Description("This event will be raised when the user selects 'copy' in the context menu"), Category("TestHierarchyControl events")>
    Public Event CopyTestComponents As EventHandler(Of EventArgs)

    <Description("This event will be raised when the user selects 'Past' in the context menu"), Category("TestHierarchyControl events")>
    Public Event PasteTestComponents As EventHandler(Of EventArgs)

    <Description("This event will be raised when the user selects 'Locked for edit' in the context menu"), Category("TestHierarchyControl events")>
    Public Event ToggleLockedForEdit As EventHandler(Of ToggleLockedForEditEventArgs)

    <Description("This event will be raised when the user selects 'Refresh using Auto item selection' in the context menu"), Category("TestHierarchyControl events")>
    Public Event RefreshComponentsWithDataSource As EventHandler(Of RefreshComponentsWithDataSourceEventArgs)


    Public Class RefreshComponentsWithDataSourceEventArgs
        Inherits EventArgs

        Private ReadOnly _componentsWithDataSources As IList(Of AssessmentTestNode) = New List(Of AssessmentTestNode)

        Public Sub New(selectedComponents As List(Of AssessmentTestNode))
            For Each node As AssessmentTestNode In selectedComponents
                If TypeOf node Is TestSection2 AndAlso Not String.IsNullOrEmpty(DirectCast(node, TestSection2).ItemDataSource) Then
                    _componentsWithDataSources.Add(node)
                End If
            Next
        End Sub

        Public ReadOnly Property ComponentsWithDataSources As IList(Of AssessmentTestNode)
            Get
                Return _componentsWithDataSources
            End Get
        End Property

    End Class

    Protected Sub OnRefreshComponentsWithDataSource(e As RefreshComponentsWithDataSourceEventArgs)
        RaiseEvent RefreshComponentsWithDataSource(Me, e)
    End Sub

    Private Sub RefreshSectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshSectionToolStripMenuItem.Click
        OnRefreshComponentsWithDataSource(New RefreshComponentsWithDataSourceEventArgs(Me.SelectedComponents))
    End Sub

    Protected Sub OnCopyTestComponents(e As EventArgs)
        RaiseEvent CopyTestComponents(Me, e)
    End Sub

    Protected Sub OnPasteTestComponents(e As EventArgs)
        RaiseEvent PasteTestComponents(Me, e)
    End Sub

    Protected Sub OnAddTestPartMenuClick(e As EventArgs)
        RaiseEvent AddTestPartMenuClick(Me, e)
    End Sub

    Protected Sub OnAddSectionMenuClick(e As EventArgs)
        RaiseEvent AddSectionMenuClick(Me, e)
    End Sub

    Protected Sub OnAddItemMenuClick(e As AddAssessmentItemEventArgs)
        RaiseEvent AddAssessmentItem(Me, e)
    End Sub

    Protected Sub OnAddItemsFromCodeMenuClick(e As EventArgs)
        RaiseEvent AddItemsFromCode(Me, e)
    End Sub

    Protected Sub OnDeleteTestComponentMenuClick(e As EventArgs)
        RaiseEvent DeleteTestComponentMenuClick(Me, e)
    End Sub

    Protected Sub OnLockedForEditMenuClick(e As ToggleLockedForEditEventArgs)
        RaiseEvent ToggleLockedForEdit(Me, e)
    End Sub




    Public Sub New()

        InitializeComponent()

        _lastExpandedRowState = New List(Of ExpandedState)
    End Sub



    Public Property AssessmentTest As AssessmentTest2
        Get
            Return _assessmentTestModel
        End Get
        Set
            If value IsNot Nothing Then
                _assessmentTestModel = value
                GridBindingSource.DataSource = _assessmentTestModel
                RefetchDataSource(False, False)

                TestHierarchyGrid.ExpandRecords()
            End If
        End Set
    End Property


    Public ReadOnly Property SelectedComponents As List(Of AssessmentTestNode)
        Get
            Dim selectedEntities As New List(Of AssessmentTestNode)

            For Each aSelectedItem As GridEXSelectedItem In TestHierarchyGrid.SelectedItems
                If aSelectedItem.Position >= 0 Then
                    Dim isValidatingEntityBase = TryCast(aSelectedItem.GetRow.DataRow, AssessmentTestNode)

                    If isValidatingEntityBase IsNot Nothing Then
                        selectedEntities.Add(isValidatingEntityBase)
                    End If
                End If
            Next

            Return selectedEntities
        End Get
    End Property

    Public ReadOnly Property TestSectionContext As TestSection2
        Get
            Dim contextToReturn As TestSection2 = Nothing

            If SelectedComponent IsNot Nothing Then
                If TypeOf SelectedComponent Is TestSection2 Then
                    contextToReturn = CType(SelectedComponent, TestSection2)
                ElseIf TypeOf SelectedComponent Is ItemReference2 Then
                    Dim parentNode As AssessmentTestNode = GetParentComponentOf(SelectedComponent)
                    Debug.Assert(TypeOf parentNode Is TestSection2, "parent for itemref is expected to be a TestSection")
                    contextToReturn = CType(parentNode, TestSection2)
                End If
            End If

            Return contextToReturn
        End Get
    End Property

    Public ReadOnly Property PositionToInsertItem As Integer
        Get
            Dim returnValue = 0
            If TestHierarchyGrid.SelectedItems.Count > 0 Then
                Dim lastIndex = TestHierarchyGrid.SelectedItems.Count - 1
                Dim selectedRow As GridEXRow = TestHierarchyGrid.SelectedItems(lastIndex).GetRow()
                Select Case selectedRow.DataRow.GetType().FullName
                    Case GetType(ItemReference2).FullName
                        returnValue = GetRelativePositionOfRow(selectedRow.Position)
                End Select
            End If
            Return returnValue
        End Get
    End Property

    Public ReadOnly Property LastSelectedItemRefIndex As Integer
        Get
            Dim highestNumber = 0
            If TestHierarchyGrid.SelectedItems.Count > 0 Then
                For i = 0 To TestHierarchyGrid.SelectedItems.Count - 1
                    Dim selectedRow As GridEXRow = TestHierarchyGrid.SelectedItems(i).GetRow()
                    If selectedRow.Position > highestNumber Then
                        highestNumber = selectedRow.Position
                    End If
                Next
            End If
            Return highestNumber
        End Get
    End Property

    Public ReadOnly Property IsSelectionAnItemReference As Boolean
        Get
            Return TypeOf TestHierarchyGrid.SelectedItems(0).GetRow().DataRow Is ItemReference2
        End Get
    End Property

    Public Function GetSelectedIndexes() As List(Of Integer)
        Dim list As New List(Of Integer)

        For Each item As GridEXSelectedItem In TestHierarchyGrid.SelectedItems
            list.Add(GetRelativePositionOfRow(item.GetRow().Position))
        Next

        Return list
    End Function

    Public Sub SetSelection(lastSelectedIndex As Integer, countOfRowsToSelect As Integer)
        Dim currentIndex As Integer = If(IsSelectionAnItemReference, lastSelectedIndex, TestHierarchyGrid.GetRow(SelectedComponent).Position)

        RemoveHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
        AddSelections(currentIndex, countOfRowsToSelect)
        AddHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
    End Sub

    Public Property SelectedComponent As AssessmentTestNode
        Get
            If Not TestHierarchyGrid.SelectedItems.Count = 0 AndAlso TestHierarchyGrid.SelectedItems(0).Position >= 0 Then
                Dim selectedRow As GridEXRow = TestHierarchyGrid.SelectedItems(0).GetRow
                Return DirectCast(selectedRow.DataRow, AssessmentTestNode)
            End If

            Return Nothing
        End Get
        Set
            Dim gridRow As GridEXRow = TestHierarchyGrid.GetRow(value)
            If gridRow IsNot Nothing Then
                TestHierarchyGrid.MoveTo(gridRow)
            End If
        End Set
    End Property

    Public Property TestIsNew As Boolean

    Public Property TestIsTemplate As Boolean

    Public ReadOnly Property AllTestComponentsLockedForEdit As Boolean
        Get
            Dim locked = True
            GetAllTestComponentsLockedForEdit(AssessmentTest.TestParts, locked)

            Return locked
        End Get
    End Property



    Private Sub GetAllTestComponentsLockedForEdit(testComponents As IEnumerable, ByRef locked As Boolean)
        If locked Then
            For Each testComponent As AssessmentTestNode In testComponents
                If Not testComponent.LockedForEdit Then
                    locked = False
                    Exit For
                Else
                    If TypeOf testComponent Is TestPart2 Then
                        GetAllTestComponentsLockedForEdit(DirectCast(testComponent, TestPart2).Sections, locked)
                    ElseIf TypeOf testComponent Is TestSection2 Then
                        GetAllTestComponentsLockedForEdit(DirectCast(testComponent, TestSection2).Components, locked)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub TestHierarchyGrid_LoadingRow(sender As Object, e As RowLoadEventArgs) Handles TestHierarchyGrid.LoadingRow
        If e.Row.RowType = RowType.Record Then
            ToggleRowState(e.Row, False)
        End If
    End Sub

    Private Sub TestHierarchyGrid_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles TestHierarchyGrid.FormattingRow
        If e.Row.DataRow IsNot Nothing Then
            If e.Row.RowType = RowType.Record Then
                If Not TypeOf e.Row.DataRow Is AssessmentTest2 Then
                    Dim indexBuilder As New StringBuilder()
                    Dim row As GridEXRow = e.Row

                    While row.Parent IsNot Nothing
                        If Not TypeOf row.Parent.DataRow Is AssessmentTest2 Then
                            indexBuilder.Insert(0, (row.Parent.RowIndex + 1).ToString + ".")
                        End If
                        row = row.Parent
                    End While
                    indexBuilder.Append((e.Row.RowIndex + 1).ToString())

                    Dim cell As GridEXCell = e.Row.Cells("title")
                    cell.Text = indexBuilder.Append(" - " + cell.Text).ToString()

                    If TypeOf e.Row.DataRow Is ItemReference2 Then
                        Dim formatStyle As New GridEXFormatStyle() With {.ImageHorizontalAlignment = ImageHorizontalAlignment.Near}
                        With DirectCast(e.Row, GridEXRow)
                            .Cells("StatusIconAnchor").FormatStyle = formatStyle
                            .Cells("StatusIconAnchor").Column().Width = If(.Cells("StatusIconAnchor").Column().Visible, 20, 0)
                            .Cells("StatusIconSeeding").FormatStyle = formatStyle
                            .Cells("StatusIconSeeding").Column().Width = If(.Cells("StatusIconSeeding").Column().Visible, 20, 0)
                        End With
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub TestHierarchyGrid_SelectionChanged(sender As Object, e As EventArgs) Handles TestHierarchyGrid.SelectionChanged
        Dim selectedItems As New List(Of AssessmentTestNode)
        ValidateSelection(selectedItems)

        OnTestComponentSelected(New TestComponentSelectedEventArgs(selectedItems))

    End Sub

    Private Sub TestHierarchyGrid_DoubleClicked(sender As Object, e As EventArgs) Handles TestHierarchyGrid.DoubleClick
        Dim selectedItems As New List(Of AssessmentTestNode)
        ValidateSelection(selectedItems)

        OnTestComponentDoubleClicked(New TestComponentSelectedEventArgs(selectedItems))
    End Sub

    Private Sub ValidateSelection(ByRef selectedItems As List(Of AssessmentTestNode))
        Dim selectionOfMixedTypes As Boolean = False
        Dim initialSelectedType As Type = Nothing

        RemoveHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
        For i As Integer = TestHierarchyGrid.SelectedItems.Count - 1 To 0 Step -1
            Dim selectedItem As GridEXSelectedItem = TestHierarchyGrid.SelectedItems(i)

            Dim dataRow As Object = selectedItem.GetRow().DataRow
            If (dataRow Is Nothing) Then
                Continue For
            End If

            If (i = TestHierarchyGrid.SelectedItems.Count - 1) Then
                initialSelectedType = dataRow.GetType()
                selectedItems.Add(DirectCast(selectedItem.GetRow.DataRow, AssessmentTestNode))
                Continue For
            End If


            If Not (dataRow.GetType().FullName = initialSelectedType.FullName) AndAlso TestHierarchyGrid.SelectedItems.Count > 1 Then
                selectionOfMixedTypes = True
                Exit For
            End If

            selectedItems.Add(DirectCast(selectedItem.GetRow.DataRow, AssessmentTestNode))
        Next

        If (selectionOfMixedTypes) Then
            MessageBox.Show(My.Resources.MultipleSelectionIsOnlySupportedForItems)
            TestHierarchyGrid.SelectedItems.Clear()
        End If

        AddHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
    End Sub


    Private Sub TestHierarchyGrid_KeyUp(sender As Object, e As KeyEventArgs) Handles TestHierarchyGrid.KeyUp
        If e.KeyCode = Keys.Delete Then
            If _testComponentDeleteEnabled Then
                OnDeleteTestComponentMenuClick(New EventArgs)
            End If
        End If
    End Sub

    Private Sub TestHierarchyGrid_CurrentCellChanging(sender As Object, e As CurrentCellChangingEventArgs) Handles TestHierarchyGrid.CurrentCellChanging
        If TestHierarchyGrid.SelectedItems.Count = 1 AndAlso TestHierarchyGrid.SelectedItems(0).Position >= 0 Then
            Dim selectedRow As GridEXRow = TestHierarchyGrid.SelectedItems(0).GetRow

            If Not e.Row.Equals(selectedRow) Then
                Dim args As New CurrentTestComponentSelectedChangingEventArgs
                OnTestComponentSelectedChanging(args)

                e.Cancel = args.Cancel
            End If
        End If
    End Sub

    Private Sub MoveTestComponentUpInParentCollection(parentCollection As IList, component As AssessmentTestNode, refetchData As Boolean)
        Dim indexOfComponent As Integer = parentCollection.IndexOf(component)

        If indexOfComponent > 0 Then
            parentCollection.Remove(component)

            parentCollection.Insert(indexOfComponent - 1, component)
            If refetchData Then
                RefetchDataSource(False)
                SelectedComponent = component
            End If
        End If
    End Sub


    Private Sub MoveTestComponentUpInParentCollection(parentCollection As IList, component As AssessmentTestNode)
        MoveTestComponentUpInParentCollection(parentCollection, component, True)
    End Sub

    Private Sub MoveTestComponentDownInParentCollection(parentCollection As IList, component As AssessmentTestNode, refetchData As Boolean)
        Dim indexOfComponent As Integer = parentCollection.IndexOf(component)

        If indexOfComponent < parentCollection.Count - 1 Then
            parentCollection.Remove(component)

            parentCollection.Insert(indexOfComponent + 1, component)

            If refetchData Then
                RefetchDataSource(False)
                SelectedComponent = component
            End If
        End If
    End Sub

    Private Sub MoveTestComponentDownInParentCollection(parentCollection As IList, component As AssessmentTestNode)
        MoveTestComponentDownInParentCollection(parentCollection, component, True)
    End Sub

    Private Sub SaveExpandedRowState()

        _lastExpandedRowState.Clear()

        SaveExpandedRowStateLoopThroughRowCollection(TestHierarchyGrid.GetRows())

    End Sub

    Private Sub SaveExpandedRowStateLoopThroughRowCollection(rows() As GridEXRow)
        For Each row As GridEXRow In rows

            If row.Expanded AndAlso row.DataRow IsNot Nothing AndAlso Not TypeOf row.DataRow Is AssessmentTest2 Then
                Dim component = DirectCast(row.DataRow, AssessmentTestNode)
                _lastExpandedRowState.Add(New ExpandedState(component.GetType(), component.Identifier))
            End If

            If row.Children > 0 Then
                SaveExpandedRowStateLoopThroughRowCollection(row.GetChildRecords())
            End If
        Next
    End Sub

    Private Sub ApplyExpandedRowState()
        If TestHierarchyGrid.RowCount > 0 Then
            Dim rootRow As GridEXRow = TestHierarchyGrid.GetRow()
            rootRow.Expanded = True

            For Each stateRow As ExpandedState In _lastExpandedRowState
                ApplyExpandedRowStateLoopThoughRowCollection(stateRow, rootRow.GetChildRecords())
            Next
        End If
    End Sub

    Private Function ApplyExpandedRowStateLoopThoughRowCollection(stateToApply As ExpandedState, rows() As GridEXRow) As Boolean
        Dim returnValue = False

        For Each row As GridEXRow In rows
            If TypeOf row.DataRow Is AssessmentTestNode Then
                Dim testNodeOfDataRow = DirectCast(row.DataRow, AssessmentTestNode)
                If (testNodeOfDataRow.GetType() Is stateToApply.RowType) AndAlso testNodeOfDataRow.Identifier.Equals(stateToApply.Identifier) Then
                    row.Expanded = True
                    returnValue = True
                    Exit For
                End If
            End If

            If row.Children > 0 Then
                returnValue = ApplyExpandedRowStateLoopThoughRowCollection(stateToApply, row.GetChildRecords())
                If returnValue Then
                    Exit For
                End If
            End If
        Next
        Return returnValue
    End Function

    Private Sub TestContextMenuStrip_Opening(sender As Object, e As CancelEventArgs) Handles TestContextMenuStrip.Opening

        If SelectedComponents.Count = 1 AndAlso Not LockedForEditMenuItem.Checked Then
            Dim clipboardWrapper As TestComponentCollectionClipboardWrapper
            clipboardWrapper = CType(ClipboardHelper.GetData(), TestComponentCollectionClipboardWrapper)

            If clipboardWrapper IsNot Nothing AndAlso clipboardWrapper.Components IsNot Nothing Then
                Dim firstComponentOnClipboard = CType(clipboardWrapper.Components(0), AssessmentTestNode)

                If TypeOf SelectedComponent Is AssessmentTest2 AndAlso TypeOf firstComponentOnClipboard Is TestPart2 Then
                    PasteToolStripMenuItem.Enabled = True
                ElseIf TypeOf SelectedComponent Is TestPart2 AndAlso TypeOf firstComponentOnClipboard Is TestSection2 Then
                    PasteToolStripMenuItem.Enabled = True
                ElseIf TypeOf SelectedComponent Is TestSection2 AndAlso (TypeOf firstComponentOnClipboard Is TestSection2 OrElse TypeOf firstComponentOnClipboard Is ItemReference2) Then
                    PasteToolStripMenuItem.Enabled = True
                Else
                    PasteToolStripMenuItem.Enabled = False
                End If
            Else
                PasteToolStripMenuItem.Enabled = False
            End If
        Else
            PasteToolStripMenuItem.Enabled = False
        End If

        SetRefreshSectionToolStripMenuItemState(LockedForEditMenuItem.Checked)
    End Sub

    Private Sub SetRefreshSectionToolStripMenuItemState(lockedForEdit As Boolean)
        If lockedForEdit Then
            RefreshSectionToolStripMenuItem.Enabled = False
        Else
            If TypeOf SelectedComponent Is TestSection2 _
            AndAlso Not String.IsNullOrEmpty(DirectCast(SelectedComponent, TestSection2).ItemDataSource) Then
                RefreshSectionToolStripMenuItem.Enabled = True
            Else
                RefreshSectionToolStripMenuItem.Enabled = False
            End If
        End If
    End Sub



    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Dim copyEventArgs As New EventArgs
        OnCopyTestComponents(copyEventArgs)
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        Dim pastEventArgs As New EventArgs
        OnPasteTestComponents(pastEventArgs)
    End Sub

    Private Sub LockedForEditMenuItem_Click(sender As Object, e As EventArgs) Handles LockedForEditMenuItem.Click
        LockTestComponentForEdit(LockedForEditMenuItem.Checked)
        OnLockedForEditMenuClick(New ToggleLockedForEditEventArgs(LockedForEditMenuItem.Checked))
    End Sub

    Public Sub LockTestComponentForEdit(locked As Boolean)
        Dim canLockRecursive = False
        Dim lockRecursive = False
        Dim selectedRows As New List(Of GridEXRow)

        For Each selectedItem As GridEXSelectedItem In TestHierarchyGrid.SelectedItems
            Dim row As GridEXRow = selectedItem.GetRow()
            selectedRows.Add(row)
            If row.GetChildRows.Length > 0 Then
                canLockRecursive = True
            End If
        Next

        If canLockRecursive Then
            Dim lockOrUnlock As String

            If locked Then
                lockOrUnlock = My.Resources.LockForEdit
            Else
                lockOrUnlock = My.Resources.UnlockForEdit
            End If

            lockRecursive = (MessageBox.Show(String.Format(My.Resources.LockChildrenForEdit, lockOrUnlock.ToLower), lockOrUnlock, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes)
        End If

        For Each row As GridEXRow In selectedRows
            SetLockStateOnRow(row, locked, lockRecursive)

            ToggleRowState(row, lockRecursive)
        Next

    End Sub

    Private Sub ToggleRowState(row As GridEXRow, recursive As Boolean)
        If row.DataRow IsNot Nothing Then
            Dim locked = False
            If TypeOf row.DataRow Is AssessmentTest2 Then
                locked = DirectCast(row.DataRow, AssessmentTest2).LockedForEdit
            ElseIf TypeOf row.DataRow Is AssessmentTestNode Then
                locked = DirectCast(row.DataRow, AssessmentTestNode).LockedForEdit
            Else
                Throw New NotSupportedException($"Unsupported test node to lock/unlock: '{row.DataRow.GetType().FullName}'")
            End If

            Dim anchorItem As Boolean = False
            Dim seedingItem As Boolean = False
            If TypeOf row.DataRow Is ItemReference2 Then
                With DirectCast(row.DataRow, ItemReference2)
                    anchorItem = .IsAnchorItem
                    seedingItem = (.ItemFunctionalType = ItemFunctionalType.Seeding)

                    Dim formatStyle As New GridEXFormatStyle() With {.ForeColor = Color.FromArgb(0, 0, 0)}
                    If seedingItem Then
                        formatStyle.ForeColor = Color.FromArgb(125, 60, 0)
                    ElseIf .ItemFunctionalType = ItemFunctionalType.Informational Then
                        formatStyle.ForeColor = Color.FromArgb(0, 108, 195)
                    ElseIf .ItemFunctionalType = ItemFunctionalType.System Then
                        formatStyle.ForeColor = Color.FromArgb(117, 117, 117)
                    End If
                    row.Cells("Title").FormatStyle = formatStyle
                    row.Cells("StatusIconAnchor").Image = If(anchorItem, My.Resources.anchor_icon, Nothing)
                    row.Cells("StatusIconSeeding").Image = If(seedingItem, My.Resources.seeding_icon, Nothing)
                End With
            ElseIf TypeOf row.DataRow Is TestSection2 Then
                row.RowStyle = New GridEXFormatStyle() With {.BackColor = Color.FromArgb(230, 195, 221), .ForeColor = Color.FromArgb(0, 0, 0)}
            End If

            If locked Then
                row.Cells("Title").Image = My.Resources.LockedForEditImage
            ElseIf recursive OrElse Not TypeOf row.DataRow Is ItemReference2 Then
                row.Cells("Title").Image = Nothing
            End If
            If Not recursive AndAlso TypeOf row.DataRow Is ItemReference2 Then
                ToggleItemReferenceRowState(row)
            End If
        End If

        If recursive Then
            For Each childrow As GridEXRow In row.GetChildRows()
                ToggleRowState(childrow, recursive)
            Next
        End If

    End Sub

    Private Sub ToggleItemReferenceRowState(row As GridEXRow)
        If TypeOf row.DataRow Is ItemReference2 Then
            Dim oneOfChildrenLocked As Boolean = row.Parent.GetChildRows().Any(Function(r) TypeOf r.DataRow Is ItemReference2 AndAlso DirectCast(r.DataRow, ItemReference2).LockedForEdit)
            Dim oneOfChildrenIsAnchor As Boolean = row.Parent.GetChildRows().Any(Function(r) TypeOf r.DataRow Is ItemReference2 AndAlso DirectCast(r.DataRow, ItemReference2).IsAnchorItem)
            Dim oneOfChildrenIsSeeding As Boolean = row.Parent.GetChildRows().Any(Function(r) TypeOf r.DataRow Is ItemReference2 AndAlso DirectCast(r.DataRow, ItemReference2).ItemFunctionalType = ItemFunctionalType.Seeding)

            row.Parent.GetChildRows().Where(Function(r) TypeOf r.DataRow Is ItemReference2).ToList.ForEach(Sub(r)
                                                                                                               If Not DirectCast(r.DataRow, ItemReference2).LockedForEdit Then r.Cells("Title").Image = If(oneOfChildrenLocked, My.Resources.nowyoudontseeme_16, Nothing)
                                                                                                               r.Cells("StatusIconAnchor").Column().Visible = oneOfChildrenIsAnchor
                                                                                                               r.Cells("StatusIconSeeding").Column().Visible = oneOfChildrenIsSeeding
                                                                                                           End Sub)
        End If
    End Sub

    Private Sub SetLockStateOnRow(row As GridEXRow, locked As Boolean, recursive As Boolean)
        If TypeOf row.DataRow Is AssessmentTest2 Then
            DirectCast(row.DataRow, AssessmentTest2).LockedForEdit = locked
        ElseIf TypeOf row.DataRow Is AssessmentTestNode Then
            DirectCast(row.DataRow, AssessmentTestNode).LockedForEdit = locked
        Else
            Throw New NotSupportedException($"Unsupported test node to lock/unlock: '{row.DataRow.GetType().FullName}'")
        End If

        If recursive Then
            For Each childrow As GridEXRow In row.GetChildRows()
                SetLockStateOnRow(childrow, locked, recursive)
            Next
        End If
    End Sub

    Private Function IsTestComponentLockedForEdit(testComponent As AssessmentTestNode) As Boolean
        Dim lockedForEdit As Boolean = False

        If testComponent IsNot Nothing Then
            If TypeOf testComponent Is AssessmentTest2 Then
                lockedForEdit = DirectCast(testComponent, AssessmentTest2).LockedForEdit
            ElseIf TypeOf testComponent Is AssessmentTestNode Then
                lockedForEdit = DirectCast(testComponent, AssessmentTestNode).LockedForEdit
            End If
        End If

        Return lockedForEdit
    End Function



    Public Sub RefreshDataSource()
    End Sub

    Public Sub RefetchDataSource(keepPosition As Boolean, Optional refetchGrid As Boolean = True)
        Dim selectedRow As GridEXRow = Nothing
        If TestHierarchyGrid.GetRow() IsNot Nothing Then
            selectedRow = TestHierarchyGrid.GetRow()
        End If

        SaveExpandedRowState()

        If refetchGrid Then
            TestHierarchyGrid.Refetch()
        End If

        ApplyExpandedRowState()

        If selectedRow IsNot Nothing AndAlso keepPosition Then
            TestHierarchyGrid.MoveTo(selectedRow)
        End If
    End Sub

    Public Function GetParentComponentOf(component As AssessmentTestNode) As AssessmentTestNode
        Dim gridRow As GridEXRow = TestHierarchyGrid.GetRow(component)
        If gridRow IsNot Nothing Then
            Return DirectCast(gridRow.Parent.DataRow, AssessmentTestNode)
        End If

        Return Nothing
    End Function

    Public Sub DisableAllTestComponentContextMenu()
        AddItemToolStripMenuItem.Visible = False
        AddItemsFromCodesToolStripMenuItem.Visible = False
        Debug.WriteLine("AddSectionToolStripMenuItem.Visible=False")
        AddSectionToolStripMenuItem.Visible = False
        SetDataSourceMenuItem.Visible = False
        AddTestPartToolStripMenuItem.Visible = False
        DeleteTestComponentToolStripMenuItem.Visible = False
        ContextMenuToolStripSeparator.Visible = False
        MoveItemDownMenuItem.Visible = False
        MoveItemUpMenuItem.Visible = False
        MoveTestPartDownMenuItem.Visible = False
        MoveTestPartUpMenuItem.Visible = False
        MoveSectionDownMenuItem.Visible = False
        MoveSectionUpMenuItem.Visible = False
        CopyToolStripMenuItem.Visible = False
        PasteToolStripMenuItem.Visible = False
        RefreshItemDataSourceToolStripMenuItem.Enabled = False
        _testComponentDeleteEnabled = False
    End Sub

    Public Function HasSelectionLockedComponents(components As List(Of AssessmentTestNode)) As Boolean
        Dim returnValue As Boolean = False

        For Each comp As AssessmentTestNode In components
            If comp.LockedForEdit Then
                returnValue = True
                Exit For
            End If
        Next

        Return returnValue
    End Function

    Public Function HasSelectionLockedParents(items As IEnumerable(Of ItemReference2)) As Boolean
        Dim returnValue As Boolean = False

        For Each item In items
            If item.Parent.LockedForEdit Then
                returnValue = True
                Exit For
            End If
        Next

        Return returnValue
    End Function

    Public Sub DetermineTestComponentsContextMenuEnableState(userTestDesignPermission As TestDesignPermission,
           lockedComponentSelected As Nullable(Of Boolean),
           parentComponentIsLocked As Nullable(Of Boolean),
           Optional ByVal selectedComps As List(Of AssessmentTestNode) = Nothing)
        DisableAllTestComponentContextMenu()

        If (selectedComps Is Nothing) Then
            selectedComps = SelectedComponents
        End If

        If selectedComps.Count > 0 Then

            If Not (lockedComponentSelected.HasValue) Then
                lockedComponentSelected = HasSelectionLockedComponents(selectedComps)
            End If

            LockedForEditMenuItem.Enabled = userTestDesignPermission = TestDesignPermission.Advanced
            LockedForEditMenuItem.Checked = lockedComponentSelected.Value

            Dim singleItemSelected As Boolean = (selectedComps.Count = 1)
            Dim firstSelectedComponent As AssessmentTestNode = selectedComps(0)

            If TypeOf firstSelectedComponent Is AssessmentTest2 Then
                AddTestPartToolStripMenuItem.Visible = True
                AddTestPartToolStripMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal
            ElseIf TypeOf firstSelectedComponent Is TestPart2 Then
                Debug.WriteLine("AddSectionToolStripMenuItem.Visible = True")

                AddSectionToolStripMenuItem.Visible = True
                AddSectionToolStripMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                ContextMenuToolStripSeparator.Visible = True

                DeleteTestComponentToolStripMenuItem.Visible = True
                DeleteTestComponentToolStripMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                MoveTestPartDownMenuItem.Visible = singleItemSelected
                MoveTestPartDownMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                MoveTestPartUpMenuItem.Visible = singleItemSelected
                MoveTestPartUpMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                PasteToolStripMenuItem.Visible = True

                _testComponentDeleteEnabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal
            ElseIf TypeOf firstSelectedComponent Is TestSection2 Then
                Dim testSection As TestSection2 = DirectCast(firstSelectedComponent, TestSection2)

                AddItemToolStripMenuItem.Visible = True
                AddItemToolStripMenuItem.Enabled = Not lockedComponentSelected.Value

                AddItemsFromCodesToolStripMenuItem.Visible = True
                AddItemsFromCodesToolStripMenuItem.Enabled = Not lockedComponentSelected.Value

                Debug.WriteLine("AddSectionToolStripMenuItem.Visible = True")

                AddSectionToolStripMenuItem.Visible = True
                AddSectionToolStripMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                SetDataSourceMenuItem.Visible = True
                SetDataSourceMenuItem.Enabled = Not lockedComponentSelected.Value

                ContextMenuToolStripSeparator.Visible = True

                DeleteTestComponentToolStripMenuItem.Visible = True
                DeleteTestComponentToolStripMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                MoveSectionDownMenuItem.Visible = singleItemSelected
                MoveSectionDownMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                MoveSectionUpMenuItem.Visible = singleItemSelected
                MoveSectionUpMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                CopyToolStripMenuItem.Visible = True
                CopyToolStripMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                PasteToolStripMenuItem.Visible = True

                _testComponentDeleteEnabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                RefreshItemDataSourceToolStripMenuItem.Enabled = Not String.IsNullOrEmpty(testSection.ItemDataSource)
            ElseIf TypeOf firstSelectedComponent Is ItemReference2 Then
                AddItemsFromCodesToolStripMenuItem.Visible = True
                AddItemsFromCodesToolStripMenuItem.Enabled = Not parentComponentIsLocked.Value

                DeleteTestComponentToolStripMenuItem.Visible = True
                DeleteTestComponentToolStripMenuItem.Enabled = Not lockedComponentSelected.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal

                MoveItemDownMenuItem.Visible = True
                MoveItemDownMenuItem.Enabled = Not parentComponentIsLocked.Value AndAlso singleItemSelected

                MoveItemUpMenuItem.Visible = True
                MoveItemUpMenuItem.Enabled = Not parentComponentIsLocked.Value AndAlso singleItemSelected

                AddItemToolStripMenuItem.Visible = True
                AddSectionToolStripMenuItem.Visible = True

                AddSectionToolStripMenuItem.Enabled = Not parentComponentIsLocked.Value AndAlso Not userTestDesignPermission = TestDesignPermission.Minimal
                SetDataSourceMenuItem.Visible = True

                SetDataSourceMenuItem.Enabled = Not parentComponentIsLocked.Value
                AddItemToolStripMenuItem.Enabled = Not parentComponentIsLocked.Value

                CopyToolStripMenuItem.Visible = True
                CopyToolStripMenuItem.Enabled = True

                PasteToolStripMenuItem.Visible = True

                _testComponentDeleteEnabled = True
            Else
                Throw New NotSupportedException($"'{firstSelectedComponent.GetType().FullName}' not supported")
            End If

        End If
    End Sub

    Public Sub MoveCurrentComponentDownInSection()
        Dim ListOfSelectedComponents As New List(Of AssessmentTestNode)

        If SelectedComponents.Count > 0 Then
            Dim parentList = DirectCast(DirectCast(DirectCast(Me.SelectedComponents(0), TestComponent2).Parent, TestSection2).Components, IList)

            For i As Integer = parentList.Count To 1 Step -1
                If SelectedComponents.IndexOf(DirectCast(parentList(i - 1), AssessmentTestNode)) >= 0 Then
                    If i = parentList.Count Then
                        Exit Sub
                    End If
                    ListOfSelectedComponents.Add(DirectCast(parentList(i - 1), AssessmentTestNode))
                End If
            Next
            Dim currentComponent As TestComponent2 = Nothing

            For Each currentComponent In ListOfSelectedComponents
                MoveTestComponentDownInParentCollection(DirectCast(DirectCast(currentComponent.Parent, TestSection2).Components, IList), currentComponent, False)
            Next

            RefetchDataSource(False)
            SelectedComponent = currentComponent
        End If
    End Sub

    Public Sub MoveCurrentComponentUpInSection()
        Dim listOfSelectedComponents As New List(Of AssessmentTestNode)

        If SelectedComponents.Count > 0 Then
            Dim parentList = DirectCast(DirectCast(DirectCast(Me.SelectedComponents(0), TestComponent2).Parent, TestSection2).Components, IList)

            For i = 1 To parentList.Count
                If SelectedComponents.IndexOf(DirectCast(parentList(i - 1), AssessmentTestNode)) >= 0 Then
                    If i = 1 Then
                        Exit Sub
                    End If
                    listOfSelectedComponents.Add(DirectCast(parentList(i - 1), AssessmentTestNode))
                End If
            Next
            Dim currentComponent As TestComponent2 = Nothing

            For Each currentComponent In listOfSelectedComponents
                MoveTestComponentUpInParentCollection(DirectCast(DirectCast(currentComponent.Parent, TestSection2).Components, IList), currentComponent, False)
            Next

            RefetchDataSource(False)
            SelectedComponent = currentComponent
        End If
    End Sub

    Public Sub MoveCurrentSectionUpInSectionOrTestPart()
        Dim selectedSection = DirectCast(Me.SelectedComponent, TestSection2)
        If TypeOf selectedSection.Parent Is TestPart2 Then
            MoveTestComponentUpInParentCollection(DirectCast(DirectCast(selectedSection.Parent, TestPart2).Sections, IList), selectedSection)
        ElseIf TypeOf selectedSection.Parent Is TestSection2 Then
            MoveTestComponentUpInParentCollection(DirectCast(DirectCast(selectedSection.Parent, TestSection2).Components, IList), selectedSection)
        End If
    End Sub

    Public Sub MoveCurrentSectionDownInSectionOrTestPart()
        Dim selectedSection = DirectCast(Me.SelectedComponent, TestSection2)
        If TypeOf selectedSection.Parent Is TestPart2 Then
            MoveTestComponentDownInParentCollection(DirectCast(DirectCast(selectedSection.Parent, TestPart2).Sections, IList), selectedSection)
        ElseIf TypeOf selectedSection.Parent Is TestSection2 Then
            MoveTestComponentDownInParentCollection(DirectCast(DirectCast(selectedSection.Parent, TestSection2).Components, IList), selectedSection)
        End If
    End Sub

    Public Sub MoveCurrentTestPartUpInTest()
        Dim selectedTestPart = DirectCast(Me.SelectedComponent, TestPart2)
        MoveTestComponentUpInParentCollection(DirectCast(Me.AssessmentTest.TestParts, IList), selectedTestPart)
    End Sub

    Public Sub MoveCurrentTestPartDownInTest()
        Dim selectedTestPart = DirectCast(Me.SelectedComponent, TestPart2)
        MoveTestComponentDownInParentCollection(DirectCast(Me.AssessmentTest.TestParts, IList), selectedTestPart)
    End Sub





    Private Sub AddTestPartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddTestPartToolStripMenuItem.Click
        OnAddTestPartMenuClick(New EventArgs)
    End Sub

    Private Sub AddSectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddSectionToolStripMenuItem.Click
        OnAddSectionMenuClick(New EventArgs)
    End Sub

    Private Sub AddItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddItemToolStripMenuItem.Click
        Dim addItemArgs As AddAssessmentItemEventArgs

        If TypeOf SelectedComponent Is ItemReference2 Then
            Dim baseItem = CType(Me.SelectedComponent, ItemReference2)
            Dim parentSection = CType(Me.GetParentComponentOf(Me.SelectedComponent), TestSection2)

            addItemArgs = New AddAssessmentItemEventArgs(parentSection, baseItem, False)
        Else
            addItemArgs = New AddAssessmentItemEventArgs(CType(Me.SelectedComponent, TestSection2), Nothing, True)
        End If
        TestHierarchyGrid.AllowDrop = True

        OnAddItemMenuClick(addItemArgs)
    End Sub

    Private Sub DeleteTestComponentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteTestComponentToolStripMenuItem.Click
        OnDeleteTestComponentMenuClick(New EventArgs)
    End Sub

    Private Sub MoveItemUpMenuItem_Click(sender As Object, e As EventArgs) Handles MoveItemUpMenuItem.Click
        MoveCurrentComponentUpInSection()
    End Sub

    Private Sub MoveItemDownMenuItem_Click(sender As Object, e As EventArgs) Handles MoveItemDownMenuItem.Click
        MoveCurrentComponentDownInSection()
    End Sub

    Private Sub MoveTestsectionUpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoveSectionUpMenuItem.Click
        MoveCurrentSectionUpInSectionOrTestPart()
    End Sub

    Private Sub MoveTestsectionDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoveSectionDownMenuItem.Click
        MoveCurrentSectionDownInSectionOrTestPart()
    End Sub

    Private Sub MoveTestPartUpMenuItem_Click(sender As Object, e As EventArgs) Handles MoveTestPartUpMenuItem.Click
        MoveCurrentTestPartUpInTest()
    End Sub

    Private Sub MoveTestPartDownMenuItem_Click(sender As Object, e As EventArgs) Handles MoveTestPartDownMenuItem.Click
        MoveCurrentTestPartDownInTest()
    End Sub

    Private Sub AddItemsFromCodesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddItemsFromCodesToolStripMenuItem.Click
        OnAddItemsFromCodeMenuClick(New EventArgs)
    End Sub

    Private Sub RefreshItemDataSourceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshItemDataSourceToolStripMenuItem.Click
        OnRefreshComponentsWithDataSource(New RefreshComponentsWithDataSourceEventArgs(Me.SelectedComponents))
    End Sub



    Private Sub TestHierarchyGrid_RowDrag(sender As Object, e As RowDragEventArgs) Handles TestHierarchyGrid.RowDrag

        Dim dataRow As Object = TestHierarchyGrid.GetRow().DataRow

        TestHierarchyGrid.AllowDrop = True
        TestHierarchyGrid.DoDragDrop(dataRow, DragDropEffects.Move)
    End Sub

    Private Sub TestHierarchyGrid_DragOver(sender As Object, e As DragEventArgs) Handles TestHierarchyGrid.DragOver
        e.Effect = DragDropEffects.None
        If e.Data.GetDataPresent(GetType(ItemReference2)) OrElse e.Data.GetDataPresent(GetType(GridEXSelectedItemCollection)) Then
            Dim rowDrop As Integer = TestHierarchyGrid.RowPositionFromPoint()
            If rowDrop >= 0 Then
                Dim rowDragOver As GridEXRow = TestHierarchyGrid.GetRow(rowDrop)
                If TypeOf rowDragOver.DataRow Is TestSection2 OrElse TypeOf rowDragOver.DataRow Is ItemReference2 Then
                    e.Effect = DragDropEffects.Move
                Else
                    e.Effect = DragDropEffects.None
                End If
            End If
        ElseIf e.Data.GetDataPresent(GetType(TestSection2)) Then
            Dim rowDrop As Integer = TestHierarchyGrid.RowPositionFromPoint()
            If rowDrop >= 0 Then
                Dim rowDragOver As GridEXRow = TestHierarchyGrid.GetRow(rowDrop)
                If TypeOf rowDragOver.DataRow Is TestSection2 OrElse TypeOf rowDragOver.DataRow Is TestPart2 Then
                    e.Effect = DragDropEffects.Move
                Else
                    e.Effect = DragDropEffects.None
                End If
            End If
        ElseIf e.Data.GetDataPresent(GetType(TestPart2)) Then
            Dim rowDrop As Integer = TestHierarchyGrid.RowPositionFromPoint()
            If rowDrop >= 0 Then
                Dim rowDragOver As GridEXRow = TestHierarchyGrid.GetRow(rowDrop)
                If TypeOf rowDragOver.DataRow Is AssessmentTest2 Then
                    e.Effect = DragDropEffects.Move
                Else
                    e.Effect = DragDropEffects.None
                End If
            End If
        End If
    End Sub

    Private Sub TestHierarchyGrid_DragDrop(sender As Object, e As DragEventArgs) Handles TestHierarchyGrid.DragDrop
        Dim indexOfDroppedRow As Integer = 0

        indexOfDroppedRow = TestHierarchyGrid.RowPositionFromPoint()
        If e.Data.GetDataPresent(GetType(TestPart2)) Then

            Dim draggedTestPart = DirectCast(e.Data.GetData(GetType(TestPart2)), TestPart2)

            AssessmentTest.TestParts.Remove(draggedTestPart)
            AssessmentTest.TestParts.Add(draggedTestPart)

            RemoveHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
            RefetchDataSource(False)
            AddHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged

        ElseIf e.Data.GetDataPresent(GetType(ItemReference2)) OrElse
            e.Data.GetDataPresent(GetType(TestSection2)) Then

            Dim selColl As New List(Of AssessmentTestNode)
            If indexOfDroppedRow >= 0 Then

                TestHierarchyGrid.SelectedItems.Sort()

                For i = 0 To TestHierarchyGrid.SelectedItems.Count - 1
                    Dim dataRow As Object = TestHierarchyGrid.GetRow(DirectCast(TestHierarchyGrid.SelectedItems(i), GridEXSelectedItem).Position).DataRow
                    Dim dragComponent = DirectCast(dataRow, TestComponent2)
                    selColl.Add(dragComponent)
                Next

                Dim dataRowDrop As Object = TestHierarchyGrid.GetRow(indexOfDroppedRow).DataRow
                For Each dragComponent As TestComponent2 In selColl
                    If TypeOf dragComponent Is ItemReference2 Then
                        MoveItemToRowWhenDropped(DirectCast(dragComponent, ItemReference2), dataRowDrop)
                    ElseIf TypeOf dragComponent Is TestSection2 Then
                        MoveSectionToRowWhenDropped(DirectCast(dragComponent, TestSection2), dataRowDrop)
                    Else
                        Throw New NotSupportedException()
                    End If
                Next
            End If

            If selColl.Count > 0 Then
                RemoveHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
                RefetchDataSource(False)

                AddSelections(TestHierarchyGrid.GetRow(selColl(0)).Position, selColl.Count)
                AddHandler TestHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
            End If

        ElseIf e.Data.GetDataPresent(GetType(GridEXSelectedItemCollection)) Then

            Dim index As Integer = GetPositionOfDroppedRow(e)
            Dim relIndex As Integer = GetRelativePositionOfRow(index)

            Dim addToSection As TestSection2
            If TypeOf TestHierarchyGrid.GetRow(index).DataRow Is TestSection2 Then
                addToSection = DirectCast(TestHierarchyGrid.GetRow(index).DataRow, TestSection2)
                relIndex = 0
                index += 1
            Else
                addToSection = DirectCast(TestHierarchyGrid.GetRow(index).Parent.DataRow, TestSection2)
            End If

            Dim selectedItemColl = DirectCast(e.Data.GetData(GetType(GridEXSelectedItemCollection)), GridEXSelectedItemCollection)
            selectedItemColl.Sort()
            RaiseEvent DragDropItem(Me, New SelectedItemCollectionEventArgs(selectedItemColl, relIndex, index, addToSection))
        End If
    End Sub

    Private Sub AddSelections(currentRow As Integer, selectedRowCount As Integer)
        With Me.TestHierarchyGrid
            If .SelectedItems.Count > 0 AndAlso Not .SelectedItems(0).GetRow().Expanded Then
                .SelectedItems(0).GetRow().Expanded = True
            End If

            If .SelectedItems.Count > 0 Then
                .SelectedItems.Clear()
            End If

            .Row = Math.Min(.RowCount - 1, currentRow + 1)

            For rowIndex As Integer = Math.Min(.RowCount - 1, currentRow + 2) To Math.Min(.RowCount - 1, currentRow + selectedRowCount)
                .SelectedItems.Add(rowIndex)
            Next
        End With

        Me.OnTestComponentSelected(New TestComponentSelectedEventArgs(Me.SelectedComponents))
    End Sub

    Private Function GetPositionOfDroppedRow(e As DragEventArgs) As Integer
        Dim mousePosition As Point = New Point(e.X, e.Y)
        Dim translatedPoint As Point = TestHierarchyGrid.PointToClient(mousePosition)
        Dim indexOfDroppedRow As Integer = TestHierarchyGrid.RowPositionFromPoint(translatedPoint.X, translatedPoint.Y)

        Debug.Assert(indexOfDroppedRow >= 0, "Should Not be less than 0")
        Debug.Assert(indexOfDroppedRow <= TestHierarchyGrid.RowCount, "Should Not be more than nr of rows.")

        Return indexOfDroppedRow

    End Function

    Private Function GetRelativePositionOfRow(indexOfRow As Integer) As Integer

        Dim isItem As Boolean = TestHierarchyGrid.GetRow(indexOfRow).DataRow.GetType() Is GetType(ItemReference2)
        Dim isSection As Boolean = TestHierarchyGrid.GetRow(indexOfRow).DataRow.GetType() Is GetType(TestSection2)

        If (isSection) Then
            Return 0
        End If

        If (indexOfRow <= 0 OrElse Not isItem) Then
            Return indexOfRow
        End If

        Dim indexOfParent As Integer = TestHierarchyGrid.GetRow(indexOfRow).Parent.Position
        Return (indexOfRow - indexOfParent)
    End Function

    Private Sub MoveItemToRowWhenDropped(draggedComponent As ItemReference2, dataRowDrop As Object)
        If Not draggedComponent.Equals(dataRowDrop) Then
            DirectCast(draggedComponent.Parent, TestSection2).Components.Remove(draggedComponent)

            If TypeOf dataRowDrop Is TestSection2 Then
                Dim droppedParentSection = DirectCast(dataRowDrop, TestSection2)
                droppedParentSection.Components.Add(draggedComponent)

            ElseIf TypeOf dataRowDrop Is ItemReference2 Then
                Dim droppedItemReference = DirectCast(dataRowDrop, ItemReference2)
                Dim droppedParentSection = DirectCast(droppedItemReference.Parent, TestSection2)
                Dim droppedItemReferenceIndexInSection As Integer = droppedParentSection.Components.IndexOf(droppedItemReference)
                droppedParentSection.Components.Insert(droppedItemReferenceIndexInSection, draggedComponent)
            End If
        End If
    End Sub

    Private Shared Sub MoveSectionToRowWhenDropped(draggedComponent As TestComponent2, dataRowDrop As Object)
        If Not draggedComponent.Equals(dataRowDrop) Then
            If TypeOf draggedComponent.Parent Is TestSection2 Then
                DirectCast(draggedComponent.Parent, TestSection2).Components.Remove(draggedComponent)
            ElseIf TypeOf draggedComponent.Parent Is TestPart2 Then
                DirectCast(draggedComponent.Parent, TestPart2).Sections.Remove(DirectCast(draggedComponent, TestSection2))
            Else
                Throw New NotSupportedException()
            End If

            If TypeOf dataRowDrop Is TestSection2 Then
                Dim droppedParentSection = DirectCast(dataRowDrop, TestSection2)
                droppedParentSection.Components.Add(draggedComponent)
            ElseIf TypeOf dataRowDrop Is TestPart2 Then
                Dim droppedParentTestPart = DirectCast(dataRowDrop, TestPart2)
                droppedParentTestPart.Sections.Add(DirectCast(draggedComponent, TestSection2))
            End If
        End If
    End Sub



    Private Class ExpandedState
        Public ReadOnly Property RowType As Type

        Public ReadOnly Property Identifier As String

        Public Sub New(rowType As Type, identifier As String)
            Me.RowType = rowType
            Me.Identifier = identifier
        End Sub
    End Class


    Public Sub ExportToExcel()
        Try
            Dim gridExExporter As New GridEXExporter
            gridExExporter.GridEX = TestHierarchyGrid
            gridExExporter.IncludeExcelProcessingInstruction = True
            gridExExporter.IncludeFormatStyle = False
            gridExExporter.IncludeHeaders = False
            gridExExporter.IncludeChildTables = True
            gridExExporter.IncludeCollapsedRows = True
            If Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) Then
                SaveExcelFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            End If
            Dim result As DialogResult = SaveExcelFileDialog.ShowDialog()
            Dim fileName As String = String.Empty
            If result = DialogResult.OK Then
                fileName = SaveExcelFileDialog.FileName
                Dim stream = New FileStream(fileName, FileMode.Create)
                gridExExporter.Export(stream)
                stream.Flush()
                stream.Close()
                If MessageBox.Show(My.Resources.WouldYouLikeToOpenTheFileInExcelNow, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Try
                        Process.Start(fileName)
                    Catch ex As Exception
                        MessageBox.Show(My.Resources.ErrorThrown + ex.ToString())
                    End Try
                End If
            End If
            gridExExporter.Dispose()
        Catch ioException As IOException
            MessageBox.Show(My.Resources.ErrorWhileExportingToExcelFileMightBeInUse, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class
