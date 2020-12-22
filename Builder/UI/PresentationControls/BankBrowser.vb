Imports System.ComponentModel
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Security
Imports Questify.Builder.Security.TestBuilderPermissionAccess

Public Class BankBrowser


    Private _idBankInvolvedInPendingBankAction As Integer = -1
    Private _pendingBankAction As BankAction = BankAction.None
    Private _beforeBankActionTreeRows As New Dictionary(Of Integer, GridEXRow)

    Private _refreshPending As Boolean
    Private _previousSearchText As String
    Private _previousSearchStartIndex As Integer



    <Description("Datasource where the bankbrowser binds to"), Bindable(True), Category("Data")> _
    <AttributeProvider(GetType(IListSource))> _
    Public Property DataSource() As Object
        Get
            Return BankTreeControl.DataSource
        End Get
        Set(ByVal value As Object)

            _refreshPending = False

            Try
                BankTreeControl.DataSource = value

                For Each row As GridEXRow In BankTreeControl.GetRows()
                    row.Expanded = True
                Next

            Catch ex As Exception
            End Try
        End Set
    End Property

    <Description("Datamember inside the datasource what has to be used"), Bindable(True), Category("Data")> _
    <Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(Drawing.Design.UITypeEditor))> _
    Public Property DataMember() As String
        Get
            Return BankTreeControl.DataMember
        End Get
        Set(ByVal value As String)
            BankTreeControl.DataMember = value
        End Set
    End Property

    <Description("The currently selected bank of this browser"), Bindable(False), Category("BankBrowser Specific")> _
    Public Property SelectedBank() As BankDto
        Get
            If BankTreeControl.SelectedItems.Count > 0 Then
                Return CType(BankTreeControl.GetRow().DataRow, BankDto)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As BankDto)
            Dim row As GridEXRow = BankTreeControl.GetRow(value)
            If Not (row Is Nothing) Then
                BankTreeControl.MoveTo(row)
            End If
        End Set
    End Property

    <Description("Allow the user to filter the tree"), Bindable(True), Category("BankBrowser Specific")> _
    Public Property EnableFiltering() As Boolean
        Get
            Return (BankTreeControl.FilterMode = FilterMode.Automatic)
        End Get
        Set(ByVal value As Boolean)
            If value Then
                BankTreeControl.FilterMode = FilterMode.Manual
            Else
                BankTreeControl.FilterMode = FilterMode.None
            End If
        End Set
    End Property



    Public Enum BankAction
        None
        Add
        Update
        Delete
        Clear
        Refresh
    End Enum

    <Description("This event will be raised when the user selects a bank in the bankbrowser"), Category("Bankbrowser events")> _
    Public Event BankSelected As EventHandler(Of BankSelectedEventArgs)

    <Description("This event will be raised when the user adds a new bank in the bankbrowser"), Category("Bankbrowser events")> _
    Public Event AddNewBank As EventHandler(Of AddBankEventArgs)

    <Description("This event will be raised when the user asks for the properties of a selected bank in the bankbrowser"), Category("Bankbrowser events")> _
    Public Event ShowBankProperties As EventHandler(Of BankSelectedEventArgs)

    <Description("This event will be raised when the user deletes a selected bank in the bankbrowser"), Category("Bankbrowser events")> _
    Public Event DeleteBank As EventHandler(Of BankSelectedEventArgs)

    <Description("This event will be raised when the user clears a selected bank in the bankbrowser"), Category("Bankbrowser events")> _
    Public Event ClearBank As EventHandler(Of BankSelectedEventArgs)

    <Description("This event will be raised when the user clears and deletes a selected bank (and subbanks) in the bankbrowser"), Category("Bankbrowser events")> _
    Public Event ClearAndDeleteBankHierarchical As EventHandler(Of BankSelectedEventArgs)

    <Description("This event will be raised when the user requests to refresh the content of the bankbrowser"), Category("Bankbrowser events")> _
    Public Event RefreshBanks As EventHandler(Of BankSelectedEventArgs)

    Protected Overridable Sub OnBankSelected(ByVal e As BankSelectedEventArgs)
        BankContextMenu.Items("AddRootbankTODOToolStripMenuItem").Enabled = PermissionFactory.Instance.TryUserIsPermittedTo(AddNew, TestBuilderPermissionTarget.BankEntity, 0)
        BankContextMenu.Items("AddBankToolStripMenuItem").Enabled = PermissionFactory.Instance.TryUserIsPermittedTo(AddNew, TestBuilderPermissionTarget.BankEntity, e.SelectedBank.Id)
        BankContextMenu.Items("DeleteToolStripMenuItem").Enabled = PermissionFactory.Instance.TryUserIsPermittedTo(Delete, TestBuilderPermissionTarget.BankEntity, e.SelectedBank.Id)
        BankContextMenu.Items("ClearAndDeleteToolStripMenuItem").Enabled = PermissionFactory.Instance.TryUserIsPermittedTo(Delete, TestBuilderPermissionTarget.BankEntity, e.SelectedBank.Id)
        BankContextMenu.Items("ClearToolStripMenuItem").Enabled = _
            PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ClearBank, e.SelectedBank.Id, 0) _
            OrElse PermissionFactory.Instance.TryUserIsPermittedTo(Delete, TestBuilderPermissionTarget.BankEntity, e.SelectedBank.Id)
        BankContextMenu.Items("PropertiesToolStripMenuItem").Enabled = PermissionFactory.Instance.TryUserIsPermittedTo(ViewProperties, TestBuilderPermissionTarget.BankEntity, e.SelectedBank.Id)

        RaiseEvent BankSelected(Me, e)
    End Sub

    Protected Overridable Sub OnAddNewBank(ByVal e As AddBankEventArgs)
        RaiseEvent AddNewBank(Me, e)
    End Sub

    Protected Overridable Sub OnShowBankProperties(ByVal e As BankSelectedEventArgs)
        RaiseEvent ShowBankProperties(Me, e)
    End Sub

    Protected Overridable Sub OnDeleteBank(ByVal e As BankSelectedEventArgs)
        RaiseEvent DeleteBank(Me, e)
    End Sub

    Protected Overridable Sub OnClearBank(ByVal e As BankSelectedEventArgs)
        RaiseEvent ClearBank(Me, e)
    End Sub

    Protected Overridable Sub OnClearAndDeleteBank(ByVal e As BankSelectedEventArgs)
        RaiseEvent ClearAndDeleteBankHierarchical(Me, e)
    End Sub

    Protected Overridable Sub OnRefreshBanks(ByVal e As BankSelectedEventArgs)
        RaiseEvent RefreshBanks(Me, e)
    End Sub



    Public Sub StoreBankTreeState(ByVal priorToBankAction As BankAction, ByVal idOfBankToRestoreAsCurrent As Integer)

        _pendingBankAction = priorToBankAction
        _beforeBankActionTreeRows.Clear()

        Dim bankRow As GridEXRow
        Dim bankInstance As BankDto

        Dim bankGridExRow As GridEXRow = BankTreeControl.GetRow()

        If idOfBankToRestoreAsCurrent > -1 Then
            _idBankInvolvedInPendingBankAction = idOfBankToRestoreAsCurrent
        Else
            If priorToBankAction = BankAction.Delete AndAlso bankGridExRow IsNot Nothing Then
                bankGridExRow = bankGridExRow.Parent
            End If

            If bankGridExRow IsNot Nothing Then

                bankInstance = DirectCast(bankGridExRow.DataRow, BankDto)
                If bankInstance IsNot Nothing Then
                    _idBankInvolvedInPendingBankAction = bankInstance.Id
                End If
            End If
        End If

        For Each bankRow In BankTreeControl.GetDataRows()
            Try
                bankInstance = DirectCast(bankRow.DataRow, BankDto)
                _beforeBankActionTreeRows.Add(bankInstance.Id, bankRow)
            Catch ex As SD.LLBLGen.Pro.ORMSupportClasses.ORMEntityIsDeletedException
            End Try
        Next
    End Sub


    Public Sub RestoreBankTreeState()

        Select Case _pendingBankAction
            Case BankAction.Add, BankAction.Update, BankAction.Delete, BankAction.Refresh
                If _pendingBankAction <> BankAction.Refresh Then
                    BankTreeControl.Refetch()
                End If

                For Each bankRow As GridEXRow In BankTreeControl.GetDataRows()
                    Try
                        Dim bankInstance As BankDto = DirectCast(bankRow.DataRow, BankDto)
                        If _beforeBankActionTreeRows.ContainsKey(bankInstance.Id) Then
                            bankRow.Expanded = _beforeBankActionTreeRows(bankInstance.Id).Expanded
                        End If

                        If bankInstance.Id = _idBankInvolvedInPendingBankAction Then
                            If _pendingBankAction = BankAction.Add Then
                                bankRow.Expanded = True
                            End If
                            BankTreeControl.MoveTo(bankRow)
                        End If
                    Catch ex As SD.LLBLGen.Pro.ORMSupportClasses.ORMEntityIsDeletedException
                    End Try
                Next
        End Select

        _pendingBankAction = BankAction.None
        _beforeBankActionTreeRows.Clear()
        _idBankInvolvedInPendingBankAction = -1
    End Sub




    Private Sub BankTreeControl_RowCollapsed(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles BankTreeControl.RowCollapsed
        BankTreeControl.Update()
    End Sub

    Private Sub bankTreeControl_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankTreeControl.SelectionChanged
        If BankTreeControl.SelectedItems.Count > 0 Then
            Dim selectedBank As BankDto = DirectCast(BankTreeControl.GetRow.DataRow, BankDto)

            Try
                Dim id = selectedBank.Id
            Catch ex As SD.LLBLGen.Pro.ORMSupportClasses.ORMEntityIsDeletedException
                Exit Sub
            End Try

            If Not (selectedBank Is Nothing) Then
                OnBankSelected(New BankSelectedEventArgs(selectedBank))
            End If
        End If
    End Sub

    Private Sub bankTreeControl_ApplyingFilter(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BankTreeControl.ApplyingFilter

        Dim filterRow As GridEXRow = BankTreeControl.FilterRow
        Dim filterColumn As GridEXColumn = BankTreeControl.RootTable.Columns("Name")
        If Not filterRow.Cells(filterColumn).Value Is Nothing Then
            Dim filterString As String = filterRow.Cells(filterColumn).Value.ToString()
            If filterString.Length > 0 Then
                BankTreeControl.RootTable.ApplyFilter(New GridEXFilterCondition(filterColumn, ConditionOperator.Contains, filterString))
            End If
        Else
            BankTreeControl.RootTable.ApplyFilter(Nothing)
        End If
    End Sub

    Private Sub BankTreeControl_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankTreeControl.DoubleClick
        If BankTreeControl.CurrentRow IsNot Nothing Then
            BankTreeControl.CurrentRow.Expanded = Not BankTreeControl.CurrentRow.Expanded
        End If
    End Sub

    Private Sub ExpandRow(ByVal row As GridEXRow)
        If row IsNot Nothing Then

            row.Expanded = True

            For Each childrow As GridEXRow In row.GetChildRows()
                ExpandRow(childrow)
            Next
        End If
    End Sub

    Private Sub BankTreeControl_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles BankTreeControl.PreviewKeyDown
        Dim currentRow As GridEXRow = BankTreeControl.GetRow()

        If currentRow IsNot Nothing Then
            Dim ForcedUpdate As Boolean = True
            BankTreeControl.SuspendLayout()

            If e.KeyCode = Keys.Multiply Then
                ExpandRow(currentRow)
            ElseIf e.KeyCode = Keys.Add Then
                currentRow.Expanded = True
            ElseIf e.KeyCode = Keys.Subtract Then
                currentRow.Expanded = False
            Else
                ForcedUpdate = False
            End If

            If ForcedUpdate Then
                BankTreeControl.Update()
            End If
            BankTreeControl.ResumeLayout()
        End If
    End Sub

    Private Sub AddBankToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddBankToolStripMenuItem.Click
        If Me.SelectedBank IsNot Nothing Then
            OnAddNewBank(New AddBankEventArgs(Me.SelectedBank))
        End If
    End Sub

    Private Sub AddRootbankTODOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRootbankTODOToolStripMenuItem.Click
        OnAddNewBank(New AddBankEventArgs(Nothing))
    End Sub

    Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertiesToolStripMenuItem.Click
        OnShowBankProperties(New BankSelectedEventArgs(Me.SelectedBank))
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        OnDeleteBank(New BankSelectedEventArgs(Me.SelectedBank))
    End Sub

    Private Sub ClearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem.Click
        OnClearBank(New BankSelectedEventArgs(Me.SelectedBank))
    End Sub



    Public Sub StartBanksRefresh()
        If Not _refreshPending Then
            _refreshPending = True

            OnRefreshBanks(New BankSelectedEventArgs(SelectedBank))

            While _refreshPending
                Application.DoEvents()
            End While

            If Not String.IsNullOrEmpty(BankSearchTextBox.Text) Then
                If Not BankSearchTextBox.Text.Equals(_previousSearchText) Then
                    _previousSearchStartIndex = -1
                End If

                _previousSearchText = BankSearchTextBox.Text

                If _previousSearchStartIndex >= BankTreeControl.RowCount Then
                    _previousSearchStartIndex = -1
                End If

                If BankTreeControl.Find(BankTreeControl.RootTable.Columns(0), ConditionOperator.BeginsWith, BankSearchTextBox.Text, _previousSearchStartIndex, 1) Then
                    _previousSearchStartIndex = BankTreeControl.Row
                    BankTreeControl.Focus()
                Else
                    _previousSearchStartIndex = -1
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripButtonRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresh.Click
        StartBanksRefresh()
    End Sub

    Private Sub BankSearchTextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles BankSearchTextBox.KeyPress
        If e.KeyChar = Chr(System.Windows.Forms.Keys.Enter) Then
            StartBanksRefresh()
        End If
    End Sub

    Private Sub ClearAndDeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearAndDeleteToolStripMenuItem.Click
        OnClearAndDeleteBank(New BankSelectedEventArgs(Me.SelectedBank))
    End Sub



End Class