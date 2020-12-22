Imports System.ComponentModel

Imports Cito.Tester.ContentModel
Imports Janus.Windows.GridEX
Imports System.Text

Public Class TestPackageHierarchyControl


    Private _testPackageModel As TestPackage
    Private _lastExpandedRowState As List(Of ExpandedState)
    Private _testPackageIsNew As Boolean
    Private _testPackageComponentDeleteEnabled As Boolean


    <Description("This event will be raised when the user selects a component in the testhierarcy"), Category("TestHierarchyControl events")> _
    Public Event TestPackageComponentSelected As EventHandler(Of TestPackageComponentSelectedEventArgs)

    Protected Sub OnTestPackageComponentSelected(ByVal e As TestPackageComponentSelectedEventArgs)
        RaiseEvent TestPackageComponentSelected(Me, e)
    End Sub

    <Description("This event will be raised when the control needs additional resource, that can be handled by an external resourcemanager"), Category("TestHierarchyControl events")> _
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Protected Sub OnResourceNeeded(ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub

    <Description("This event will be raised when the user selects a row, but it is not applied."), Category("TestHierarchyControl events")> _
    Public Event TestPackageComponentSelectedChanging As EventHandler(Of CurrentTestPackageComponentSelectedChangingEventArgs)

    Protected Sub OnTestPackageComponentSelectedChanging(ByVal e As CurrentTestPackageComponentSelectedChangingEventArgs)
        RaiseEvent TestPackageComponentSelectedChanging(Me, e)
    End Sub

    <Description("This event will be raised when the user drops selected test from the test selector dialog"), Category("TestHierarchyControl events")> _
    Public Event DragDropTest As EventHandler(Of SelectedTestCollectionEventArgs)

    <Description("This event will be raised when the user selects a component in the testhierarcy"), Category("TestHierarchyControl events")> _
    Public Event TestPackageComponentDoubleClicked As EventHandler(Of TestPackageComponentSelectedEventArgs)

    Protected Sub OnTestPackageComponentDoubleClicked(ByVal e As TestPackageComponentSelectedEventArgs)
        RaiseEvent TestPackageComponentDoubleClicked(Me, e)
    End Sub



    Public Class AddTestToTestPackageEventArgs
        Inherits EventArgs

        Private _parentTestset As TestSet
        Private _baseTest As TestReference
        Private _append As Boolean
        Private _insertBefore As Boolean

        Public Sub New(ByVal parentTestset As TestSet, ByVal baseTest As TestReference, ByVal append As Boolean, ByVal insertBefore As Boolean)
            _parentTestset = parentTestset
            _baseTest = baseTest
            _append = append
            _insertBefore = insertBefore
        End Sub

        Public ReadOnly Property ParentTestset() As TestSet
            Get
                Return _parentTestset
            End Get
        End Property


        Public ReadOnly Property BaseTest() As TestReference
            Get
                Return _baseTest
            End Get
        End Property

        Public ReadOnly Property Append() As Boolean
            Get
                Return _append
            End Get
        End Property

        Public ReadOnly Property InsertBefore() As Boolean
            Get
                Return _insertBefore
            End Get
        End Property

    End Class

    <Description("This event will be raised when the user selects 'add testset' in the context menu"), Category("TestHierarchyControl events")> _
    Public Event AddTestsetMenuClick As EventHandler

    <Description("This event will be raised when the user selects 'add test', 'insert before'or 'insert after' in the context menu"), Category("TestHierarchyControl events")> _
    Public Event AddTestToTestPackage As EventHandler(Of AddTestToTestPackageEventArgs)

    <Description("This event will be raised when the user selects 'delete test component' in the context menu"), Category("TestHierarchyControl events")>
    Public Event DeleteTestPackageComponentMenuClick As EventHandler


    Protected Sub OnAddTestSetMenuClick(ByVal e As EventArgs)
        RaiseEvent AddTestsetMenuClick(Me, e)
    End Sub


    Protected Sub OnAddTestPackageMenuClick(ByVal e As AddTestToTestPackageEventArgs)
        RaiseEvent AddTestToTestPackage(Me, e)
    End Sub


    Protected Sub OnDeleteTestPackageComponentMenuClick(ByVal e As EventArgs)
        RaiseEvent DeleteTestPackageComponentMenuClick(Me, e)
    End Sub




    Public Sub New()

        InitializeComponent()

        _lastExpandedRowState = New List(Of ExpandedState)
    End Sub




    Public Property TestPackage() As TestPackage
        Get
            Return _testPackageModel
        End Get
        Set(ByVal value As TestPackage)
            If value IsNot Nothing Then
                _testPackageModel = value
                GridBindingSource.DataSource = _testPackageModel
                Me.RefetchDataSource(False)

                TestPackageHierarchyGrid.ExpandRecords()
            End If
        End Set
    End Property

    Public ReadOnly Property SelectedComponents() As List(Of TestPackageNode)
        Get
            Dim selectedEntities As New List(Of TestPackageNode)

            For Each aSelectedItem As Janus.Windows.GridEX.GridEXSelectedItem In TestPackageHierarchyGrid.SelectedItems
                If aSelectedItem.Position >= 0 Then
                    Dim isValidatingEntityBase As TestPackageNode = TryCast(aSelectedItem.GetRow.DataRow, TestPackageNode)

                    If isValidatingEntityBase IsNot Nothing Then
                        selectedEntities.Add(isValidatingEntityBase)
                    End If
                End If
            Next

            Return selectedEntities
        End Get
    End Property

    Public ReadOnly Property PositionToInsertItem As Integer
        Get
            Dim returnValue As Integer = 0
            Dim selectedRow As GridEXRow = Me.TestPackageHierarchyGrid.SelectedItems(0).GetRow()
            For Each item As GridEXSelectedItem In TestPackageHierarchyGrid.SelectedItems
                Select Case selectedRow.DataRow.GetType().FullName
                    Case GetType(TestReference).FullName
                        returnValue = GetRelativePositionOfRow(TestPackageHierarchyGrid.SelectedItems(0).GetRow().Position)
                        Exit For
                End Select
            Next
            Return returnValue
        End Get
    End Property

    Public ReadOnly Property IsSelectionATestReference As Boolean
        Get
            Return TypeOf Me.TestPackageHierarchyGrid.SelectedItems(0).GetRow().DataRow Is TestReference
        End Get
    End Property

    Public ReadOnly Property TestSetContext() As TestSet
        Get
            Dim contextToReturn As TestSet = Nothing

            If Me.SelectedComponent IsNot Nothing Then
                If TypeOf Me.SelectedComponent Is TestSet Then
                    contextToReturn = CType(Me.SelectedComponent, TestSet)
                ElseIf TypeOf Me.SelectedComponent Is TestReference Then
                    Dim parentNode As TestPackageNode = Me.GetParentComponentOf(Me.SelectedComponent)
                    Debug.Assert(TypeOf parentNode Is TestSet, "parent for testref is expected to be a TestSet")
                    contextToReturn = CType(parentNode, TestSet)
                End If
            End If
            Return contextToReturn
        End Get
    End Property

    Public Function GetSelectedIndexes() As List(Of Integer)
        Dim list As New List(Of Integer)

        For Each item As GridEXSelectedItem In TestPackageHierarchyGrid.SelectedItems
            list.Add(GetRelativePositionOfRow(item.GetRow().Position))
        Next

        Return list
    End Function

    Public Sub SetSelection(ByVal countOfRowsToSelect As Integer)

        Dim currentIndex As Integer = 0

        If Not IsSelectionATestReference Then
            currentIndex = TestPackageHierarchyGrid.GetRow(Me.SelectedComponent).Position + 1
        Else
            If (TestPackageHierarchyGrid.SelectedItems.Count > 0) Then
                Dim row As GridEXRow = TestPackageHierarchyGrid.SelectedItems(0).GetRow()
                currentIndex = row.Position
            End If
        End If

        RemoveHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged

        AddSelections(currentIndex, countOfRowsToSelect)
        AddHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged

    End Sub


    Public Property SelectedComponent() As TestPackageNode
        Get
            If Not TestPackageHierarchyGrid.SelectedItems.Count = 0 AndAlso TestPackageHierarchyGrid.SelectedItems(0).Position >= 0 Then
                Dim selectedRow As Janus.Windows.GridEX.GridEXRow = TestPackageHierarchyGrid.SelectedItems(0).GetRow
                Return DirectCast(selectedRow.DataRow, TestPackageNode)
            End If

            Return Nothing
        End Get
        Set(ByVal value As TestPackageNode)
            Dim gridRow As Janus.Windows.GridEX.GridEXRow = TestPackageHierarchyGrid.GetRow(value)
            If gridRow IsNot Nothing Then
                TestPackageHierarchyGrid.MoveTo(gridRow)
            End If
        End Set
    End Property

    Public Property TestPackageIsNew() As Boolean
        Get
            Return _testPackageIsNew
        End Get
        Set(ByVal value As Boolean)
            _testPackageIsNew = value
        End Set
    End Property




    Private Sub TestPackageHierarchyGrid_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles TestPackageHierarchyGrid.FormattingRow
        If e.Row.DataRow IsNot Nothing Then
            If TypeOf e.Row.DataRow Is ItemReference2 Then
                e.Row.RowStyle = New Janus.Windows.GridEX.GridEXFormatStyle
                e.Row.RowStyle.BackColor = Color.FromArgb(186, 157, 129)
                e.Row.RowStyle.ForeColor = Color.Black
            ElseIf TypeOf e.Row.DataRow Is TestSection2 Then
                e.Row.RowStyle = New Janus.Windows.GridEX.GridEXFormatStyle
                e.Row.RowStyle.BackColor = Color.FromArgb(129, 186, 157)
                e.Row.RowStyle.ForeColor = Color.Black
            End If

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
                End If
            End If
        End If
    End Sub

    Private Sub TestPackageHierarchyGrid_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles TestPackageHierarchyGrid.LoadingRow

        If e.Row.RowType = RowType.Record Then
            ToggleRowState(e.Row)
        End If
    End Sub

    Private Sub TestHierarchyGrid_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestPackageHierarchyGrid.SelectionChanged
        Dim selectedTests As New List(Of TestPackageNode)
        ValidateSelection(selectedTests)

        OnTestPackageComponentSelected(New TestPackageComponentSelectedEventArgs(selectedTests))

    End Sub

    Private Sub TestPackageHierarchyGrid_DoubleClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestPackageHierarchyGrid.DoubleClick
        Dim selectedTests As New List(Of TestPackageNode)
        ValidateSelection(selectedTests)

        OnTestPackageComponentDoubleClicked(New TestPackageComponentSelectedEventArgs(selectedTests))
    End Sub


    Private Sub ValidateSelection(ByRef selectedTests As List(Of TestPackageNode))
        Dim removeLastSelection As Boolean = False
        Dim initialSelectedType As Type = Nothing

        RemoveHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
        For i As Integer = TestPackageHierarchyGrid.SelectedItems.Count - 1 To 0 Step -1
            Dim SelectedItem As Janus.Windows.GridEX.GridEXSelectedItem = TestPackageHierarchyGrid.SelectedItems(i)

            Dim dataRow As Object = SelectedItem.GetRow().DataRow
            If (dataRow Is Nothing) Then Continue For

            If (i = TestPackageHierarchyGrid.SelectedItems.Count - 1) Then
                initialSelectedType = dataRow.GetType()
                selectedTests.Add(DirectCast(SelectedItem.GetRow.DataRow, TestPackageNode))
                Continue For
            End If


            If Not (dataRow.GetType().FullName = initialSelectedType.FullName) AndAlso TestPackageHierarchyGrid.SelectedItems.Count > 1 Then
                removeLastSelection = True
                Continue For
            End If

            selectedTests.Add(DirectCast(SelectedItem.GetRow.DataRow, TestPackageNode))
        Next

        If (removeLastSelection) Then
            TestPackageHierarchyGrid.SelectedItems.Remove(TestPackageHierarchyGrid.SelectedItems(TestPackageHierarchyGrid.SelectedItems.Count - 1))
        End If

        AddHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
    End Sub

    Public Function TryFindTestSet(ByRef foundTestset As TestSet, ByRef currentRelativePostition As Integer) As Boolean
        If (Me.TestPackageHierarchyGrid.SelectedItems.Count < 1) Then
            Return False
        End If
        Dim selectedRow As GridEXRow = Me.TestPackageHierarchyGrid.SelectedItems(0).GetRow()

        If TypeOf selectedRow.DataRow Is ItemReference2 Then
            foundTestset = DirectCast(selectedRow.Parent.DataRow, TestSet)
            currentRelativePostition = GetRelativePositionOfRow(selectedRow.Position)
        ElseIf TypeOf selectedRow.DataRow Is TestSection2 Then
            foundTestset = DirectCast(selectedRow.DataRow, TestSet)
            currentRelativePostition = 0
        Else
            Return False
        End If

        Return True
    End Function

    Private Sub TestHierarchyGrid_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TestPackageHierarchyGrid.KeyUp
        If e.KeyCode = System.Windows.Forms.Keys.Delete Then
            If _testPackageComponentDeleteEnabled Then
                OnDeleteTestPackageComponentMenuClick(New EventArgs)
            End If
        End If
    End Sub

    Private Sub TestHierarchyGrid_CurrentCellChanging(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles TestPackageHierarchyGrid.CurrentCellChanging
        If TestPackageHierarchyGrid.SelectedItems.Count = 1 AndAlso TestPackageHierarchyGrid.SelectedItems(0).Position >= 0 Then
            Dim selectedRow As Janus.Windows.GridEX.GridEXRow = TestPackageHierarchyGrid.SelectedItems(0).GetRow

            If Not e.Row.Equals(selectedRow) Then
                Dim args As New CurrentTestPackageComponentSelectedChangingEventArgs
                OnTestPackageComponentSelectedChanging(args)

                e.Cancel = args.Cancel
            End If
        End If
    End Sub


    Private Sub MoveTestPackageComponentUpInParentCollection(ByVal parentCollection As IList, ByVal component As TestPackageNode, ByVal refetchData As Boolean)
        Dim indexOfComponent As Integer = parentCollection.IndexOf(component)

        If indexOfComponent > 0 Then
            parentCollection.Remove(component)

            parentCollection.Insert(indexOfComponent - 1, component)
            If refetchData Then
                RefetchDataSource(False)
                Me.SelectedComponent = component
            End If
        End If
    End Sub


    Private Sub MoveTestPackageComponentUpInParentCollection(ByVal parentCollection As IList, ByVal component As TestPackageNode)
        MoveTestPackageComponentUpInParentCollection(parentCollection, component, True)
    End Sub

    Private Sub MoveTestPackageComponentDownInParentCollection(ByVal parentCollection As IList, ByVal component As TestPackageNode, ByVal refetchData As Boolean)
        Dim indexOfComponent As Integer = parentCollection.IndexOf(component)

        If indexOfComponent < parentCollection.Count - 1 Then
            parentCollection.Remove(component)

            parentCollection.Insert(indexOfComponent + 1, component)

            If refetchData Then
                RefetchDataSource(False)
                Me.SelectedComponent = component
            End If
        End If
    End Sub


    Private Sub MoveTestComponentDownInParentCollection(ByVal parentCollection As IList, ByVal component As TestPackageNode)
        MoveTestPackageComponentDownInParentCollection(parentCollection, component, True)
    End Sub

    Private Sub SaveExpandedRowState()

        _lastExpandedRowState.Clear()

        SaveExpandedRowStateLoopThoughRowCollection(TestPackageHierarchyGrid.GetRows())

    End Sub


    Private Sub SaveExpandedRowStateLoopThoughRowCollection(ByVal rows() As GridEXRow)
        For Each row As GridEXRow In rows

            If row.Expanded AndAlso row.DataRow IsNot Nothing AndAlso Not TypeOf row.DataRow Is TestPackage Then
                Dim component As TestPackageNode = DirectCast(row.DataRow, TestPackageNode)
                _lastExpandedRowState.Add(New ExpandedState(component.GetType(), component.Identifier))
            End If

            If row.Children > 0 Then
                SaveExpandedRowStateLoopThoughRowCollection(row.GetChildRecords())
            End If
        Next
    End Sub

    Private Sub ApplyExpandedRowState()
        If TestPackageHierarchyGrid.RowCount > 0 Then
            Dim rootRow As GridEXRow = TestPackageHierarchyGrid.GetRow()
            rootRow.Expanded = True

            For Each stateRow As ExpandedState In _lastExpandedRowState
                ApplyExpandedRowStateLoopThroughRowCollection(stateRow, rootRow.GetChildRecords())
            Next
        End If
    End Sub

    Private Function ApplyExpandedRowStateLoopThroughRowCollection(ByVal stateToApply As ExpandedState, ByVal rows() As GridEXRow) As Boolean
        Dim returnValue As Boolean = False

        For Each row As GridEXRow In rows
            If TypeOf row.DataRow Is TestPackageNode Then
                Dim testPackageNodeOfDataRow As TestPackageNode = DirectCast(row.DataRow, TestPackageNode)
                If Type.Equals(testPackageNodeOfDataRow.GetType(), stateToApply.RowType) AndAlso testPackageNodeOfDataRow.Identifier.Equals(stateToApply.Identifier) Then
                    row.Expanded = True
                    returnValue = True
                    Exit For
                End If
            End If

            If row.Children > 0 Then
                returnValue = ApplyExpandedRowStateLoopThroughRowCollection(stateToApply, row.GetChildRecords())
                If returnValue Then Exit For
            End If
        Next
        Return returnValue
    End Function

    Private Sub SetOrderLockStateOnRow(row As GridEXRow, locked As Boolean)
        Dim testSet As TestSet = Nothing
        If TypeOf row.DataRow Is TestSet Then
            testSet = DirectCast(row.DataRow, TestSet)
        ElseIf row.DataRow.GetType() = GetType(TestReference) Then
            testSet = DirectCast(row.Parent.DataRow, TestSet)
        Else
        End If
        If testSet IsNot Nothing Then testSet.LockedOrder = locked

        For Each childrow As GridEXRow In row.GetChildRows()
            If TypeOf childrow.DataRow Is TestReference Then
                DirectCast(childrow.DataRow, TestReference).LockedOrder = locked
                ToggleRowState(childrow)
            End If
        Next
    End Sub

    Private Sub ToggleRowState(row As GridEXRow)
        If Not row.DataRow Is Nothing Then
            Dim locked As Boolean = False
            If TypeOf row.DataRow Is TestSet Then
                locked = DirectCast(row.DataRow, TestSet).LockedOrder
            ElseIf TypeOf row.DataRow Is TestReference Then
                locked = DirectCast(row.DataRow, TestReference).LockedOrder
            ElseIf TypeOf row.DataRow Is TestPackage Then
                locked = DirectCast(row.DataRow, TestPackage).LockedOrder
            Else
                Throw New NotSupportedException(String.Format("Unsupported test node to lock/unlock: '{0}'", row.DataRow.GetType().FullName))
            End If

            If locked Then
                row.Cells("Title").Image = My.Resources.linked_icon
            Else
                row.Cells("Title").Image = Nothing
            End If

        End If
    End Sub


    Public Sub RefreshDataSource()
        TestPackageHierarchyGrid.Refresh()
    End Sub

    Public Sub RefetchDataSource(ByVal keepPosition As Boolean)
        Dim selectedRow As Janus.Windows.GridEX.GridEXRow = Nothing
        If Not TestPackageHierarchyGrid.GetRow() Is Nothing Then
            selectedRow = TestPackageHierarchyGrid.GetRow()
        End If

        SaveExpandedRowState()

        TestPackageHierarchyGrid.Refetch()

        ApplyExpandedRowState()

        If selectedRow IsNot Nothing AndAlso keepPosition Then
            TestPackageHierarchyGrid.MoveTo(selectedRow)
        End If
    End Sub

    Public Function GetParentComponentOf(ByVal component As TestPackageNode) As TestPackageNode
        Dim gridRow As Janus.Windows.GridEX.GridEXRow = TestPackageHierarchyGrid.GetRow(component)
        If gridRow IsNot Nothing Then
            Return DirectCast(gridRow.Parent.DataRow, TestPackageNode)
        End If

        Return Nothing
    End Function

    Public Sub LockOrderOfTestsInTestSet(locked As Boolean)

        If TestPackageHierarchyGrid.SelectedItems.Count = 1 Then
            Dim row As GridEXRow = TestPackageHierarchyGrid.SelectedItems(0).GetRow()

            SetOrderLockStateOnRow(row, locked)

            ToggleRowState(row)
        Else
        End If
    End Sub

    Public Sub DisableAllTestComponentContextMenu()
        AddTestToolStripMenuItem.Visible = False

        AddTestsetToolStripMenuItem.Visible = False
        DeleteTestsetComponentToolStripMenuItem.Visible = False
        ContextMenuToolStripSeparator.Visible = False
        MoveTestDownMenuItem.Visible = False
        MoveTestUpMenuItem.Visible = False
        MoveTestSetDownMenuItem.Visible = False
        MoveTestSetUpMenuItem.Visible = False
        InsertTestBeforeToolStripMenuItem.Visible = False
        _testPackageComponentDeleteEnabled = False
    End Sub


    Public Sub DetermineTestComponentsContextMenuEnableState(ByVal userTestPackageDesignPermission As Boolean,
                    Optional ByVal selectedComps As List(Of TestPackageNode) = Nothing)
        Me.DisableAllTestComponentContextMenu()

        If (selectedComps Is Nothing) Then selectedComps = SelectedComponents

        If selectedComps.Count > 0 Then

            Dim singleItemSelected As Boolean = (selectedComps.Count = 1)
            Dim firstSelectedComponent As TestPackageNode = selectedComps(0)

            If TypeOf firstSelectedComponent Is TestPackage Then
                Dim testPackage As TestPackage = DirectCast(firstSelectedComponent, TestPackage)
                AddTestsetToolStripMenuItem.Visible = True
                AddTestsetToolStripMenuItem.Enabled = userTestPackageDesignPermission
            ElseIf TypeOf firstSelectedComponent Is TestSet Then
                Dim testPart As TestSet = DirectCast(firstSelectedComponent, TestSet)
                Dim bEnabled = singleItemSelected AndAlso userTestPackageDesignPermission

                AddTestToolStripMenuItem.Visible = True
                AddTestToolStripMenuItem.Enabled = True

                ContextMenuToolStripSeparator.Visible = True

                DeleteTestsetComponentToolStripMenuItem.Visible = True
                DeleteTestsetComponentToolStripMenuItem.Enabled = bEnabled

                MoveTestSetDownMenuItem.Visible = singleItemSelected
                MoveTestSetDownMenuItem.Enabled = bEnabled

                MoveTestSetUpMenuItem.Visible = singleItemSelected
                MoveTestSetUpMenuItem.Enabled = bEnabled

                _testPackageComponentDeleteEnabled = userTestPackageDesignPermission
            ElseIf TypeOf firstSelectedComponent Is TestReference Then
                Dim testReference As TestReference = DirectCast(firstSelectedComponent, TestReference)

                DeleteTestsetComponentToolStripMenuItem.Visible = True
                DeleteTestsetComponentToolStripMenuItem.Enabled = True

                MoveTestDownMenuItem.Visible = True
                MoveTestDownMenuItem.Enabled = Not testReference.LockedOrder AndAlso singleItemSelected

                MoveTestUpMenuItem.Visible = True
                MoveTestUpMenuItem.Enabled = Not testReference.LockedOrder AndAlso singleItemSelected

                InsertTestBeforeToolStripMenuItem.Visible = singleItemSelected
                InsertTestBeforeToolStripMenuItem.Enabled = True

                _testPackageComponentDeleteEnabled = True
            Else
                Throw New NotSupportedException(String.Format("'{0}' not supported", firstSelectedComponent.GetType().FullName))
            End If

        End If
    End Sub


    Public Sub MoveCurrentComponentDownInSection()
        Dim ListOfSelectedComponents As New List(Of TestPackageNode)

        If Me.SelectedComponents.Count > 0 Then
            Dim parentList As Collections.IList = DirectCast(DirectCast(DirectCast(Me.SelectedComponents(0), TestPackageComponent).Parent, TestSet).Components, Collections.IList)

            For i As Integer = parentList.Count To 1 Step -1
                If Me.SelectedComponents.IndexOf(DirectCast(parentList(i - 1), TestPackageNode)) >= 0 Then
                    If i = parentList.Count Then
                        Exit Sub
                    End If
                    ListOfSelectedComponents.Add(DirectCast(parentList(i - 1), TestPackageNode))
                End If
            Next
            Dim currentComponent As TestPackageComponent = Nothing

            For Each currentComponent In ListOfSelectedComponents
                MoveTestComponentDownInParentCollection(DirectCast(DirectCast(currentComponent.Parent, TestSet).Components, Collections.IList), currentComponent)
            Next

            RefetchDataSource(False)
            Me.SelectedComponent = currentComponent
        End If
    End Sub



    Public Sub MoveCurrentComponentUpInSection()
        Dim ListOfSelectedComponents As New List(Of TestPackageNode)

        If Me.SelectedComponents.Count > 0 Then
            Dim parentList As Collections.IList = DirectCast(DirectCast(DirectCast(Me.SelectedComponents(0), TestPackageComponent).Parent, TestSet).Components, Collections.IList)

            For i As Integer = 1 To parentList.Count
                If Me.SelectedComponents.IndexOf(DirectCast(parentList(i - 1), TestPackageNode)) >= 0 Then
                    If i = 1 Then
                        Exit Sub
                    End If
                    ListOfSelectedComponents.Add(DirectCast(parentList(i - 1), TestPackageNode))
                End If
            Next
            Dim currentComponent As TestPackageComponent = Nothing

            For Each currentComponent In ListOfSelectedComponents
                MoveTestPackageComponentUpInParentCollection(DirectCast(DirectCast(currentComponent.Parent, TestSet).Components, Collections.IList), currentComponent, False)
            Next

            RefetchDataSource(False)
            Me.SelectedComponent = currentComponent
        End If
    End Sub

    Public Sub MoveCurrentTestsetUpInTestPackage()
        Dim selectedSection As TestSet = DirectCast(Me.SelectedComponent, TestSet)
        MoveTestPackageComponentUpInParentCollection(DirectCast(DirectCast(selectedSection.Parent, TestPackage).TestSets, Collections.IList), selectedSection)
    End Sub

    Public Sub MoveCurrentTestsetDownInTestPackage()
        Dim selectedTestset As TestSet = DirectCast(Me.SelectedComponent, TestSet)
        MoveTestPackageComponentDownInParentCollection(DirectCast(DirectCast(selectedTestset.Parent, TestPackage).TestSets, Collections.IList), selectedTestset)
    End Sub

    Private Sub MoveTestPackageComponentDownInParentCollection(ByVal parentCollection As IList, ByVal component As TestPackageNode)
        MoveTestPackageComponentDownInParentCollection(parentCollection, component, True)
    End Sub




    Private Sub AddTestsetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTestsetToolStripMenuItem.Click
        OnAddTestSetMenuClick(New EventArgs)
    End Sub

    Private Sub AddTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTestToolStripMenuItem.Click, InsertTestBeforeToolStripMenuItem.Click
        Dim AddTestPackageArgs As AddTestToTestPackageEventArgs

        If sender Is InsertTestBeforeToolStripMenuItem Then
            Dim baseTest As TestReference = CType(Me.SelectedComponent, TestReference)
            Dim parentTestSet As TestSet = CType(Me.GetParentComponentOf(Me.SelectedComponent), TestSet)

            AddTestPackageArgs = New AddTestToTestPackageEventArgs(parentTestSet, baseTest, False, (sender Is InsertTestBeforeToolStripMenuItem))
        Else
            AddTestPackageArgs = New AddTestToTestPackageEventArgs(CType(Me.SelectedComponent, TestSet), Nothing, True, False)
        End If
        TestPackageHierarchyGrid.AllowDrop = True

        OnAddTestPackageMenuClick(AddTestPackageArgs)
    End Sub

    Private Sub DeleteTestComponentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteTestsetComponentToolStripMenuItem.Click
        OnDeleteTestPackageComponentMenuClick(New EventArgs)
    End Sub


    Private Sub MoveTestUpMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveTestUpMenuItem.Click
        MoveCurrentComponentUpInSection()
    End Sub

    Private Sub MoveTestDownMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveTestDownMenuItem.Click
        MoveCurrentComponentDownInSection()
    End Sub

    Private Sub MoveTestSetUpMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveTestSetUpMenuItem.Click
        MoveCurrentTestsetUpInTestPackage()
    End Sub

    Private Sub MoveTestPartDownMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveTestSetDownMenuItem.Click
        MoveCurrentTestsetDownInTestPackage()
    End Sub




    Private Sub TestPackageHierarchyGrid_RowDrag(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowDragEventArgs) Handles TestPackageHierarchyGrid.RowDrag

        Dim dataRow As Object = TestPackageHierarchyGrid.GetRow().DataRow

        If (TypeOf dataRow Is TestReference AndAlso Not DirectCast(dataRow, TestReference).LockedOrder) OrElse TypeOf dataRow Is TestSet Then
            TestPackageHierarchyGrid.AllowDrop = True
            TestPackageHierarchyGrid.DoDragDrop(dataRow, DragDropEffects.Move)
        End If
    End Sub


    Private Sub TestPackageHierarchyGrid_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TestPackageHierarchyGrid.DragOver
        If e.Data.GetDataPresent(GetType(TestReference)) Then
            Dim rowDrop As Integer = TestPackageHierarchyGrid.RowPositionFromPoint()
            If rowDrop >= 0 Then
                Dim rowDragOver As Janus.Windows.GridEX.GridEXRow = TestPackageHierarchyGrid.GetRow(rowDrop)
                If TypeOf rowDragOver.DataRow Is TestSet OrElse TypeOf rowDragOver.DataRow Is TestReference Then
                    e.Effect = DragDropEffects.Move
                Else
                    e.Effect = DragDropEffects.None
                End If
            End If
        ElseIf e.Data.GetDataPresent(GetType(TestSet)) Then
            Dim rowDrop As Integer = TestPackageHierarchyGrid.RowPositionFromPoint()
            If rowDrop >= 0 Then
                Dim rowDragOver As Janus.Windows.GridEX.GridEXRow = TestPackageHierarchyGrid.GetRow(rowDrop)
                If TypeOf rowDragOver.DataRow Is TestSet OrElse TypeOf rowDragOver.DataRow Is TestPackage Then
                    e.Effect = DragDropEffects.Move
                Else
                    e.Effect = DragDropEffects.None
                End If
            End If
        ElseIf e.Data.GetDataPresent(GetType(GridEXSelectedItemCollection)) Then
            Dim rowDrop As Integer = TestPackageHierarchyGrid.RowPositionFromPoint()
            If rowDrop >= 0 Then
                Dim rowDragOver As Janus.Windows.GridEX.GridEXRow = TestPackageHierarchyGrid.GetRow(rowDrop)
                If TypeOf rowDragOver.DataRow Is TestSet OrElse TypeOf rowDragOver.DataRow Is TestReference Then
                    e.Effect = DragDropEffects.Move
                Else
                    e.Effect = DragDropEffects.None
                End If
            End If
        End If
    End Sub

    Private Sub TestPackageHierarchyGrid_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TestPackageHierarchyGrid.DragDrop
        Dim indexOfDroppedRow As Integer = 0
        Dim draggedComponent As TestPackageComponent = Nothing

        If e.Data.GetDataPresent(GetType(TestReference)) OrElse e.Data.GetDataPresent(GetType(TestSet)) Then

            If e.Data.GetDataPresent(GetType(TestReference)) Then
                draggedComponent = DirectCast(e.Data.GetData(GetType(TestReference)), TestPackageComponent)
            ElseIf e.Data.GetDataPresent(GetType(TestSection2)) Then
                draggedComponent = DirectCast(e.Data.GetData(GetType(TestSet)), TestPackageComponent)
            End If

            indexOfDroppedRow = TestPackageHierarchyGrid.RowPositionFromPoint()

            Dim selColl As New List(Of TestPackageNode)
            If indexOfDroppedRow >= 0 Then

                TestPackageHierarchyGrid.SelectedItems.Sort()

                For i As Integer = 0 To TestPackageHierarchyGrid.SelectedItems.Count - 1
                    Dim dataRow As Object = TestPackageHierarchyGrid.GetRow(DirectCast(TestPackageHierarchyGrid.SelectedItems(i), GridEXSelectedItem).Position).DataRow
                    Dim dragComponent As TestPackageComponent = DirectCast(dataRow, TestPackageComponent)
                    selColl.Add(dragComponent)
                Next

                Dim dataRowDrop As Object = TestPackageHierarchyGrid.GetRow(indexOfDroppedRow).DataRow
                For Each dragComponent As TestPackageComponent In selColl
                    If TypeOf dragComponent Is TestReference Then
                        MoveTestToRowWhenDropped(DirectCast(dragComponent, TestReference), dataRowDrop)
                    ElseIf TypeOf dragComponent Is TestSet Then
                        MoveTestSetToRowWhenDropped(DirectCast(dragComponent, TestSet), dataRowDrop)
                    Else
                        Throw New NotSupportedException()
                    End If
                Next
            End If

            If selColl.Count > 0 Then

                RemoveHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
                RefetchDataSource(False)

                AddSelections(TestPackageHierarchyGrid.GetRow(selColl(0)).Position, selColl.Count)
                AddHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
            End If

        ElseIf e.Data.GetDataPresent(GetType(GridEXSelectedItemCollection)) Then

            Dim selectedItemColl As GridEXSelectedItemCollection = DirectCast(e.Data.GetData(GetType(GridEXSelectedItemCollection)), GridEXSelectedItemCollection)
            selectedItemColl.Sort()
            Dim index As Integer = GetPositionOfDroppedRow()
            Dim relIndex As Integer = GetRelativePositionOfRow(index)

            Dim addToTestSet As TestSet
            If TypeOf TestPackageHierarchyGrid.GetRow(index).DataRow Is TestSet Then
                addToTestSet = DirectCast(TestPackageHierarchyGrid.GetRow(index).DataRow, TestSet)
                relIndex = 0
                index += 1
            Else
                addToTestSet = DirectCast(TestPackageHierarchyGrid.GetRow(index).Parent.DataRow, TestSet)
            End If
            RaiseEvent DragDropTest(Me, New SelectedTestCollectionEventArgs(selectedItemColl, relIndex, addToTestSet))

            RemoveHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
            RefetchDataSource(False)

            AddSelections(index, selectedItemColl.Count)
            AddHandler TestPackageHierarchyGrid.SelectionChanged, AddressOf TestHierarchyGrid_SelectionChanged
        End If
    End Sub

    Private Sub AddSelections(ByVal currentRow As Integer, ByVal selectedRowCount As Integer)
        If Not TestPackageHierarchyGrid.SelectedItems.Count = 0 AndAlso Not TestPackageHierarchyGrid.SelectedItems(0).GetRow().Expanded Then
            TestPackageHierarchyGrid.SelectedItems(0).GetRow().Expanded = True
        End If
        TestPackageHierarchyGrid.SelectedItems.Clear()
        TestPackageHierarchyGrid.Row = currentRow
        For i As Integer = 0 To selectedRowCount - 1
            TestPackageHierarchyGrid.SelectedItems.Add(currentRow + i)
        Next
    End Sub

    Private Function GetPositionOfDroppedRow() As Integer

        Dim mousePosition As Point = GridEX.MousePosition
        Dim translatedPoint As Point = TestPackageHierarchyGrid.PointToClient(mousePosition)
        Dim indexOfDroppedRow As Integer = TestPackageHierarchyGrid.RowPositionFromPoint(translatedPoint.X, translatedPoint.Y)

        Return indexOfDroppedRow

    End Function

    Private Function GetRelativePositionOfRow(ByVal indexOfRow As Integer) As Integer

        Dim isTest As Boolean = TestPackageHierarchyGrid.GetRow(indexOfRow).DataRow.GetType() Is GetType(TestReference)
        Dim isTestSet As Boolean = TestPackageHierarchyGrid.GetRow(indexOfRow).DataRow.GetType() Is GetType(TestSet)

        If (isTestSet) Then
            Return 0
        End If

        If (indexOfRow <= 0 OrElse Not isTest) Then
            Return indexOfRow
        End If

        Dim indexOfParent As Integer = TestPackageHierarchyGrid.GetRow(indexOfRow).Parent.Position
        Return (indexOfRow - indexOfParent) - 1
    End Function

    Private Sub MoveTestToRowWhenDropped(ByVal draggedComponent As TestReference, ByVal dataRowDrop As Object)
        If Not draggedComponent.Equals(dataRowDrop) Then
            DirectCast(draggedComponent.Parent, TestSet).Components.Remove(draggedComponent)

            If TypeOf dataRowDrop Is TestSet Then
                Dim droppedParentSection As TestSet = DirectCast(dataRowDrop, TestSet)
                droppedParentSection.Components.Add(draggedComponent)

            ElseIf TypeOf dataRowDrop Is TestReference Then
                Dim droppedTestReference As TestReference = DirectCast(dataRowDrop, TestReference)
                Dim droppedParentSection As TestSet = DirectCast(droppedTestReference.Parent, TestSet)
                Dim droppedTestReferenceIndexInTestset As Integer = droppedParentSection.Components.IndexOf(droppedTestReference)
                droppedParentSection.Components.Insert(droppedTestReferenceIndexInTestset, draggedComponent)
            End If
        End If
    End Sub

    Private Shared Sub MoveTestSetToRowWhenDropped(ByVal draggedComponent As TestPackageComponent, ByVal dataRowDrop As Object)
        If Not draggedComponent.Equals(dataRowDrop) Then
            If TypeOf draggedComponent.Parent Is TestSet Then
                DirectCast(draggedComponent.Parent, TestSet).Components.Remove(draggedComponent)
            Else
                Throw New NotSupportedException()
            End If

            If TypeOf dataRowDrop Is TestSet Then
                Dim droppedParentSection As TestSet = DirectCast(dataRowDrop, TestSet)
                droppedParentSection.Components.Add(draggedComponent)
            End If
        End If
    End Sub



    Private Class ExpandedState

        Private _rowType As Type
        Private _identifier As String

        Public ReadOnly Property RowType() As Type
            Get
                Return _rowType
            End Get
        End Property

        Public ReadOnly Property Identifier() As String
            Get
                Return _identifier
            End Get
        End Property

        Public Sub New(ByVal rowType As Type, ByVal identifier As String)
            _rowType = rowType
            _identifier = identifier
        End Sub

    End Class


    Public Sub ExportToExcel()
        Try
            Dim GridEXExporter As New Janus.Windows.GridEX.Export.GridEXExporter
            GridEXExporter.GridEX = Me.TestPackageHierarchyGrid
            GridEXExporter.IncludeExcelProcessingInstruction = True
            GridEXExporter.IncludeFormatStyle = False
            GridEXExporter.IncludeHeaders = False
            GridEXExporter.IncludeChildTables = True
            GridEXExporter.IncludeCollapsedRows = True
            If IO.Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) Then
                SaveExcelFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            End If
            Dim result As DialogResult = SaveExcelFileDialog.ShowDialog()
            Dim fileName As String = String.Empty
            If result = DialogResult.OK Then
                fileName = SaveExcelFileDialog.FileName
                Dim stream As System.IO.FileStream = New System.IO.FileStream(fileName, System.IO.FileMode.Create)
                GridEXExporter.Export(stream)
                stream.Flush()
                stream.Close()
                If MessageBox.Show(My.Resources.WouldYouLikeToOpenTheFileInExcelNow, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Try
                        System.Diagnostics.Process.Start(fileName)
                    Catch ex As Exception
                        MessageBox.Show(My.Resources.ErrorThrown + ex.ToString())
                    End Try
                End If
            End If
            GridEXExporter.Dispose()
        Catch ioException As System.IO.IOException
            MessageBox.Show(My.Resources.ErrorWhileExportingToExcelFileMightBeInUse, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
