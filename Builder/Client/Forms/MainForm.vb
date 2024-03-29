Imports Cinch
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Security
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Configuration
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports CustomClasses
Imports Questify.Builder.Logic
Imports Cito.Tester.ContentModel.Datasources
Imports Janus.Windows.GridEX
Imports Questify.Builder.Client.My
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.ResourceManager
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Questify.Builder.UI.PublicationService
Imports System.Text
Imports System.Threading
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Logic.ContentModel
Imports ItemEditorConstants = Questify.Builder.UI.Wpf.Presentation.ItemEditor.Constants
Imports Questify.Builder.Logic.Service.InvalidateCache.Helper
Imports Questify.Builder.Logic.CustomProperties
Imports System.Globalization
Imports System.Windows.Threading
Imports Questify.Builder.Logic.Service.Exceptions
Imports Enums
Imports NLog
Imports Questify.Builder.Client.My.Resources
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Logic.Service.Model.Entities.Custom
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.LlblGen.Proxy.HelperFunctions
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports ReflectionHelper = Questify.Builder.Logic.Service.HelperFunctions.ReflectionHelper
Imports SecurityException = Questify.Builder.Security.SecurityException
Imports LogHelper = Questify.Builder.Logic.Service.Logging.LogHelper
Imports Questify.Builder.Logic.Service.Logging
Imports Xceed.Wpf.Toolkit.PropertyGrid.Editors
Imports Caliburn.Micro
Imports ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto

Public Class MainForm

    Private _performanceMonitor As Stopwatch
    Private _timer As DispatcherTimer

    Private ReadOnly DEBUG_TOKEN As Boolean

    Private WithEvents _statusBarListener As StatusBarListener = StatusBarListener.MainInstance
    Private WithEvents _actionCommands As ActionCommand = ActionCommand.Instance
    Private _preSelectedItemsInGrid As List(Of Guid)
    Private ReadOnly _windowFacade As IWindowFacade = New WindowFacade()
    Private _titleOfActiveWindow As String
    Private _cachedBankStructure As IEnumerable(Of BankDto)

    Private ReadOnly _statusStripToolTip As New ToolTip
    Private _formClosing As Boolean = False
    Private _tabPages As List(Of TabPage) = New List(Of TabPage)
    Private _currentUser As UserEntity

    Private Enum ShowWindowCommands As Integer
        Restore = 9
    End Enum
    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As ShowWindowCommands) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowText(hWnd As IntPtr, lpString As StringBuilder, nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowThreadProcessId(ByVal windowHandle As IntPtr, ByRef processId As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    End Function

    Public Const WM_HOTKEY As Integer = &H312
    Private Const CTRL_SHIFT_C_HOTKEY_ID As Integer = 1

    Enum KeyModifier
        NONE = 0
        ALT = &H1
        CTRL = &H2
        SHIFT = &H4
        WinKey = &H8
    End Enum


    Public Sub New()

        InitializeComponent()

        For Each tabPage As TabPage In MainTabControl.TabPages
            _tabPages.Add(tabPage)
        Next
        Mediator.Instance.RegisterHandler(Of EventArgs(Of Guid))(ItemEditorConstants.MoveNextItemMessage,
                                                               Sub(e) ItmEdtr_Move(e.Value, True))
        Mediator.Instance.RegisterHandler(Of EventArgs(Of Guid))(ItemEditorConstants.MovePreviousItemMessage,
                                                                       Sub(e) ItmEdtr_Move(e.Value, False))

        Mediator.Instance.RegisterHandler(Of EventArgs(Of IPropertyEntity))(ItemEditorConstants.RefreshGridAndSelectResource,
                                                                                  Sub(e)
                                                                                      If Me.InvokeRequired Then
                                                                                          Me.Invoke(Sub() Editor_RefreshGridAndSelectResource(e.Value))
                                                                                      Else
                                                                                          Editor_RefreshGridAndSelectResource(e.Value)
                                                                                      End If
                                                                                  End Sub)
        Mediator.Instance.RegisterHandler(Of EventArgs(Of IPropertyEntity))(ItemEditorConstants.UpdateGridAndSelectResource,
                                                                                  Sub(e)
                                                                                      If Me.InvokeRequired Then
                                                                                          Me.Invoke(Sub() Editor_UpdateGridAndSelectResource(e.Value))
                                                                                      Else
                                                                                          Editor_UpdateGridAndSelectResource(e.Value)
                                                                                      End If

                                                                                  End Sub
                                                                            )

        Mediator.Instance.RegisterHandler(Of EventArgs(Of Guid))(ItemEditorConstants.SelectResourceInGrid, Sub(e)
                                                                                                               SelectResourceInGridWithinTab(TabPageItems, e.Value)
                                                                                                           End Sub)

#If DEBUG Or TEST Or DEV Then
        DEBUG_TOKEN = True
#Else
        DEBUG_TOKEN = False
#End If

        If _currentUser Is Nothing Then
            _currentUser = New UserEntity(CType(Thread.CurrentPrincipal.Identity, TestBuilderIdentity).UserId)
            _currentUser = AuthorizationFactory.Instance.GetUserWithRoles(_currentUser)
        End If
    End Sub


    Private Sub ToolStripEmptySpace_MouseHover(sender As Object, e As EventArgs) Handles ToolStripEmptySpace.MouseHover
        If (Not String.IsNullOrEmpty(ToolStripEmptySpace.Text)) Then
            Dim loc As New Point(MousePosition.X, MousePosition.Y - 20)
            loc = MainStatusStrip.PointToClient(loc)
            _statusStripToolTip.Show(ToolStripEmptySpace.Text, MainStatusStrip, loc)
        End If
    End Sub

    Private Sub ToolStripEmtpySpace_MouseLeave(sender As Object, e As EventArgs) Handles ToolStripEmptySpace.MouseLeave
        _statusStripToolTip.Hide(MainStatusStrip)
    End Sub

    Private Sub WaitTimer_Tick(sender As Object, e As EventArgs)
        CheckMaintenanceWindow()
    End Sub

    Private Sub CheckMaintenanceWindow()
        Dim loginPermissionEndTime As DateTime?
        Try
            loginPermissionEndTime = AuthorizationFactory.Instance.GetMaintenanceWindow()
        Catch ex As SqlException
        End Try

        If loginPermissionEndTime.HasValue Then
            SetBackColor(If(loginPermissionEndTime.HasValue, loginPermissionEndTime.Value > DateTime.Now, False))
        Else
            Me.Invoke(Sub()
                          Me.ToolStripEmptySpace.BackColor = Me.ToolStripEmptySpace2.BackColor
                      End Sub)
        End If

        Me.Invoke(Sub()
                      Me.ToolStripEmptySpace.Text = If(loginPermissionEndTime.HasValue, String.Format(Resources.MainForm_MaintenanceWindowMessage, loginPermissionEndTime.Value.ToString("d MMMM yyyy H:mm")), String.Empty)
                  End Sub)
    End Sub

    Private Sub SetBackColor(loginPermitted As Boolean)
        Dim backColorToSet As Color = If(loginPermitted, Color.SandyBrown, Color.Tomato)
        Me.Invoke(Sub()
                      Me.ToolStripEmptySpace.BackColor = backColorToSet
                  End Sub)
    End Sub



    Private Sub GridBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles BackGroundDataWorkerPool.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)
        Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
#If DEBUG Then
        _performanceMonitor = New Stopwatch
        _performanceMonitor.Start()
#End If

        worker.ReportProgress(0)
        Dim bank As BankDto = Nothing
        If task.InputParameter IsNot Nothing Then
            bank = TryCast(GetInputParameter("BankEntity", task.InputParameter), BankDto)
        End If

        Try
            Select Case task.WorkerTask
                Case TaskType.GetItemsForBank
                    If ItemGrid.IsGridInFilterMode Then
                        Try
                            Dim filterArgs As FastSearchInitiatedEventArgs = ItemGrid.FilterArgsForFastSearch
                            task.Result = DtoFactory.Item.GetItemsListWithSearchOptions(bank.Id, filterArgs.SearchKeywords, filterArgs.IncludeSubbanks, filterArgs.SearchInBankProperties, filterArgs.SearchInItemText, filterArgs.TestContextResourceId, filterArgs.MaxRecordsToReturn)
                        Catch ex As Exception
                            task.Result = ex
                        End Try
                    Else
                        task.Result = DtoFactory.Item.GetResourcesForBank(bank.Id)
                    End If

                Case TaskType.GetMediaForBank
                    task.Result = DtoFactory.Generic.GetResourcesForBank(bank.Id)
                Case TaskType.GetTestsForBank
                    task.Result = DtoFactory.Test.GetResourcesForBank(bank.Id)
                Case TaskType.GetTestPackageForBank
                    task.Result = DtoFactory.TestPackage.GetResourcesForBank(bank.Id)
                Case TaskType.GetTestTemplatesForBank
                    task.Result = DtoFactory.TestTemplate.GetResourcesForBank(bank.Id)
                Case TaskType.GetItemLayoutTemplatesForBank
                    task.Result = DtoFactory.ItemLayoutTemplate.GetResourcesForBank(bank.Id)
                Case TaskType.GetControlTemplatesForBank
                    task.Result = DtoFactory.ControlTemplate.GetResourcesForBank(bank.Id)
                Case TaskType.GetBanks
                    Dim banks = DtoFactory.Bank.All()
                    _cachedBankStructure = banks.Flattened
                    task.Result = banks
                Case TaskType.GetBankStartPageInfo
                    task.Result = BankFactory.Instance.GetBankStatistics(bank.Id, User.Name)
                Case TaskType.GetAspectsForBank
                    task.Result = DtoFactory.Aspect.GetResourcesForBank(bank.Id)
                Case TaskType.GetDataSourcesForBank
                    task.Result = DtoFactory.Datasource.GetResourcesForBank(bank.Id)
                Case TaskType.GetDataSourceTemplatesForBank
                    task.Result = DtoFactory.DatasourceTemplate.GetResourcesForBank(bank.Id)
                Case TaskType.GetCustomPropertiesForBank
                    task.Result = DtoFactory.CustomBankResourceProperty.GetResourcesForBank(bank.Id)
            End Select
            e.Result = task
        Catch securityException As SecurityException
        End Try

        If worker.CancellationPending Then e.Cancel = True
    End Sub

    Private Sub BackGroundDataWorkerPool_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackGroundDataWorkerPool.ProgressChanged
        If Not InvokeRequired Then
            Cursor = Cursors.WaitCursor
        End If
    End Sub

    Private Function GetInputParameter(key As String, inputParameter As Object) As Object
        If inputParameter IsNot Nothing AndAlso inputParameter.GetType() Is GetType(Dictionary(Of String, Object)) Then
            Dim params = DirectCast(inputParameter, Dictionary(Of String, Object))
            If params.ContainsKey(key) Then
                Return params(key)
            End If
        End If
        Return inputParameter
    End Function

    Private Sub ShowFastSearchErrorMessage(ByVal excMessage As String)
        MessageBox.Show($"{My.Resources.MainForm_SyntaxErrorSearchString}{Environment.NewLine}{excMessage}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub FillGridBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles BackGroundDataWorkerPool.RunWorkerCompleted

        If e.Error IsNot Nothing Then
            MessageBox.Show(My.Resources.AnErrorOccurred & vbCrLf & vbCrLf & e.Error.Message)
        End If

        If e.Error IsNot Nothing OrElse e.Cancelled OrElse e.Result Is Nothing Then
            If Not InvokeRequired Then
                Cursor = Cursors.Default
            End If
            Return
        End If

        Dim task As BackgroundWorkerTask = DirectCast(e.Result, BackgroundWorkerTask)
        Dim selectionId = GetInputParameter("Selection", task.InputParameter)
        Dim selectEntityResourceId As Guid
        Dim selectedEntity As ResourceDto = Nothing
        If selectionId IsNot Nothing AndAlso Guid.TryParse(selectionId.ToString, selectEntityResourceId) AndAlso TypeOf task.Result Is IEnumerable(Of ResourceDto) Then
            selectedEntity = DirectCast(task.Result, IEnumerable(Of ResourceDto)).ToList.FirstOrDefault(Function(resource) resource.ResourceId = selectEntityResourceId)
        End If

        Select Case task.WorkerTask
            Case TaskType.GetItemsForBank

                If TypeOf task.Result Is Exception Then
                    NLog.LogManager.GetCurrentClassLogger().Log(LogLevel.Warn, "MainForm resulted in a exception")

                    Dim generalExc As Exception = DirectCast(task.Result, Exception)
                    If Not generalExc.InnerException Is Nothing AndAlso TypeOf generalExc.InnerException Is SqlException Then
                        Dim errorNumber As Integer = DirectCast(generalExc.InnerException, SqlException).Number
                        If errorNumber = -2 Then
                            MessageBox.Show(My.Resources.MainForm_FastSearchTimeOut, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            ShowFastSearchErrorMessage(DirectCast(task.Result, Exception).Message)
                        End If
                    Else
                        ShowFastSearchErrorMessage(DirectCast(task.Result, Exception).Message)
                    End If
                Else
                    If task.Result IsNot Nothing Then
                        ItemGrid.DataSource = task.Result
                    End If
                    If MainBankBrowser.SelectedBank IsNot Nothing Then
                        ItemGrid.InitializeSearchBar(MainBankBrowser.SelectedBank.Id)
                    End If

                    If ItemGrid.IsGridInFilterMode Then
                        Dim data As ResourceDto() = DirectCast(task.Result, ResourceDto())

                        StatusBarListener.MainInstance.PublishMessage(ItemGrid, String.Format(My.Resources.StatusBar_ItemsFound, data.Count))
                    End If
                    Dim selectedResourceId As Guid
                    If selectionId IsNot Nothing AndAlso Guid.TryParse(selectionId.ToString, selectedResourceId) Then
                        ItemGrid.SelectedEntity = selectedEntity
                    Else
                        If Not (_preSelectedItemsInGrid Is Nothing) Then
                            ItemGrid.GridControl.Select()
                            ItemGrid.GridControl.SelectedItems.Clear()

                            For Each selectedGuid As Guid In _preSelectedItemsInGrid
                                For Each row As GridEXRow In ItemGrid.GridControl.GetRows()
                                    If row.RowType = RowType.GroupHeader Then
                                        For Each childrow As GridEXRow In row.GetChildRows()
                                            If childrow.DataRow IsNot Nothing Then
                                                Dim entity = CType(childrow.DataRow, ItemResourceDto)
                                                If entity.ResourceId = selectedGuid Then
                                                    ItemGrid.GridControl.SelectedItems.Add(childrow.Position)
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                    Exit For
                                Next
                            Next

                            _preSelectedItemsInGrid = Nothing
                        End If
                    End If
                End If

            Case TaskType.GetMediaForBank
                MediaGrid.DataSource = task.Result
                MediaGrid.SelectedEntity = selectedEntity
            Case TaskType.GetTestPackageForBank
                TestPackageGrid.DataSource = task.Result
                TestPackageGrid.SelectedEntity = selectedEntity
            Case TaskType.GetTestsForBank
                TestGrid.DataSource = task.Result
                TestGrid.SelectedEntity = selectedEntity
            Case TaskType.GetTestTemplatesForBank
                TestTemplateGrid.DataSource = task.Result
                TestTemplateGrid.SelectedEntity = selectedEntity
            Case TaskType.GetControlTemplatesForBank
                ControlTemplateGrid.DataSource = task.Result
                ControlTemplateGrid.SelectedEntity = selectedEntity
            Case TaskType.GetItemLayoutTemplatesForBank
                ItemLayoutTemplateGrid.DataSource = task.Result
                ItemLayoutTemplateGrid.SelectedEntity = selectedEntity
            Case TaskType.GetAspectsForBank
                AspectGrid.DataSource = task.Result
                AspectGrid.SelectedEntity = selectedEntity
            Case TaskType.GetDataSourcesForBank
                DataSourceGrid.DataSource = task.Result
                DataSourceGrid.SelectedEntity = selectedEntity
            Case TaskType.GetDataSourceTemplatesForBank
                DataSourceTemplateGrid.DataSource = task.Result
                DataSourceTemplateGrid.SelectedEntity = selectedEntity
            Case TaskType.GetBanks
                MainBankBrowser.DataSource = task.Result
                MainBankBrowser.RestoreBankTreeState()
                Dim titleOfActiveWindow As String = Me.Text
                If Not titleOfActiveWindow Is Nothing Then
                    UpdateBreadcrumbPath(titleOfActiveWindow.ToString)
                End If
            Case TaskType.GetBankStartPageInfo
                BankStartPage.DataSource = DirectCast(task.Result, BankStatistics)
            Case TaskType.GetCustomPropertiesForBank
                CustomPropertyGrid.DataSource = task.Result
                CustomPropertyGrid.SelectedEntity = selectedEntity
        End Select

        If task.InputParameter IsNot Nothing Then
            Dim tab = TryCast(GetInputParameter("TabForResourceSelection", task.InputParameter), TabPage)
            Dim resourceId = GetInputParameter("SelectedResourceId", task.InputParameter)
            If tab IsNot Nothing AndAlso TypeOf resourceId Is Guid Then
                SelectResourceInGridWithinTab(tab, DirectCast(resourceId, Guid))
            End If
        End If

#If DEBUG Then
        _performanceMonitor.Stop()
        Console.WriteLine($"task {task.WorkerTask.ToString} took {_performanceMonitor.Elapsed.Seconds.ToString} seconds")
        _performanceMonitor.Reset()
#End If

        If Not InvokeRequired Then
            Cursor = Cursors.Default
        End If
    End Sub



    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        MyApplication.MainFormLoadingComplete = True
        UpdateBreadcrumbPath(Me.Text)
    End Sub

    Private Function GetBankPathOfEntity(entityBankId As Integer) As String
        Dim bank = MainBankBrowser.SelectedBank
        If bank Is Nothing Then
            Return String.Empty
        Else
            Dim bankId = entityBankId
            If entityBankId = 0 Then
                bankId = bank.Id
            End If
            Dim bankInfo As New StringBuilder()
            Dim inTree As Boolean = False
            If _cachedBankStructure IsNot Nothing Then
                Dim targetbank = _cachedBankStructure.FirstOrDefault(Function(b) b.Id.Equals(bankId))
                If targetbank IsNot Nothing Then
                    bank = targetbank
                    inTree = True
                    bankInfo.Append(bank.Name)
                End If

                While bank.ParentBankId.HasValue
                    bank = _cachedBankStructure.FirstOrDefault(Function(b) b.Id = bank.ParentBankId.Value)
                    If bank IsNot Nothing AndAlso bank.Id = bankId Then inTree = True
                    If bank IsNot Nothing AndAlso inTree Then
                        If bankInfo.Length = 0 Then
                            bankInfo.Append(bank.Name)
                        Else
                            bankInfo.Insert(0, String.Concat(bank.Name, " -> "))
                        End If
                    End If
                End While
            End If
            Return bankInfo.ToString()
        End If
    End Function

    Private Function GetFullBankPath() As String
        Return GetBankPathOfEntity(Nothing)
    End Function

    Protected Overrides Sub WndProc(ByRef m As Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            If CTRL_SHIFT_C_HOTKEY_ID = m.WParam.ToInt32 Then
                Clipboard.SetText(BreadcrumbBankPartToolStripStatusLabel.Text & BreadcrumbStripStatusLabel.Text)
            End If
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub SetToolStripToolTip()
        BreadcrumbBankPartToolStripStatusLabel.ToolTipText = My.Resources.MainForm_CopyToClipboard
        BreadcrumbStripStatusLabel.ToolTipText = My.Resources.MainForm_CopyToClipboard
    End Sub


    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TempStorageHelper.CleanTempStorage()

        RegisterHotKey(Me.Handle, CTRL_SHIFT_C_HOTKEY_ID, (KeyModifier.CTRL Or KeyModifier.SHIFT), Keys.C)

        SetToolStripToolTip()
        SetBackgroundColor()

        AddHandler BankStartPage.OpenItem, AddressOf OpenItemFromStartPage

        BackGroundDataWorkerPool.StatusbarListener = _statusBarListener

        Me.MainBankBrowser.StoreBankTreeState(BankBrowser.BankAction.Refresh, TestBuilderClientSettings.SelectedBankId)
        BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")

        SetTitleBar()

        SelectStoredTab()

        UsernameStatusLabel.Text = String.Format(UsernameStatusLabel.Text, User.Name)

        Dim requiredAccess As TestBuilderPermissionAccess

        requiredAccess = TestBuilderPermissionAccess.AddNew Or TestBuilderPermissionAccess.Edit Or TestBuilderPermissionAccess.Delete

        If PermissionFactory.Instance.TryUserIsPermittedToNamedTask(requiredAccess, TestBuilderPermissionTarget.UserEntity, TestBuilderPermissionNamedTask.None, 0, 0) AndAlso
         PermissionFactory.Instance.TryUserIsPermittedToNamedTask(requiredAccess, TestBuilderPermissionTarget.UserApplicationRoleEntity, TestBuilderPermissionNamedTask.None, 0, 0) AndAlso
         PermissionFactory.Instance.TryUserIsPermittedToNamedTask(requiredAccess, TestBuilderPermissionTarget.UserBankRoleEntity, TestBuilderPermissionNamedTask.None, 0, 0) Then

            UserAccountToolStripMenuItem.Visible = True
        Else
            UserAccountToolStripMenuItem.Visible = False
        End If

        AnnouncementControlToolStripMenuItem.Visible = PermissionFactory.Instance.TryUserIsPermittedToNamedTask(requiredAccess, TestBuilderPermissionTarget.UserApplicationRoleEntity, TestBuilderPermissionNamedTask.None, 0, 0)

        TestBuilderAsyncProtocolContextManager.Initialize()

        CheckMaintenanceWindow()
        _timer = New DispatcherTimer(New TimeSpan(1, 0, 0), DispatcherPriority.Background, Sub() WaitTimer_Tick(Nothing, Nothing), Dispatcher.CurrentDispatcher)
    End Sub

    Private Sub SetBackgroundColor()
        Dim colorName As String = ConfigurationManager.AppSettings("QB_BackgroundColor")
        Dim knownColor As KnownColor

        If Not String.IsNullOrEmpty(colorName) AndAlso [Enum].TryParse(Of KnownColor)(colorName, knownColor) Then
            Me.BackColor = Color.FromName(colorName)
        End If
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        _formClosing = True

        RemoveHandler BankStartPage.OpenItem, AddressOf OpenItemFromStartPage

        If Me.MainBankBrowser.SelectedBank IsNot Nothing Then
            TestBuilderClientSettings.SelectedBankId = Me.MainBankBrowser.SelectedBank.Id
        End If

        BackGroundDataWorkerPool.StopAllTasks()

        Try
            My.Settings.Save()
            _currentUser.UserSettings = QbSettingsParser.GettingsSettingsString(BankFactory.Instance.GetListOfBankIds())
            _currentUser.IsDirty = True
            AuthorizationFactory.Instance.UpdateUser(_currentUser)
        Catch
        End Try

        TestBuilderAsyncProtocolContextManager.Destroy()
        TempStorageHelper.CleanTempStorage()

        UnregisterHotKey(Me.Handle, CTRL_SHIFT_C_HOTKEY_ID)
    End Sub

    Private Sub MainBankBrowser_BankSelected(ByVal sender As Object, ByVal e As BankSelectedEventArgs) Handles MainBankBrowser.BankSelected
        DisposeGrid()
        If _formClosing Then Return

        If (BankFactory.Instance.BankExists(e.SelectedBank.Id)) Then
            LogHelper.TrackEvent(EventsToTrack.SwitchToBank, New Dictionary(Of String, String) From {{"BankId", e.SelectedBank.Id.ToString()}})

            Dim makeExportEntryVisible As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.Any, e.SelectedBank.Id)
            Dim makeImportEntryVisible As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Import, TestBuilderPermissionTarget.Any, e.SelectedBank.Id)
            Dim makeHarmonizationVisible As Boolean = ShowHarmonisationOptions(e.SelectedBank.Id, MainTabControl?.SelectedTab?.Tag?.ToString())

            ExportToolStripButton.Visible = makeExportEntryVisible
            ExportToolStripMenuItem.Visible = makeExportEntryVisible
            ExportToolStripButton.Enabled = makeExportEntryVisible
            ExportToolStripMenuItem.Enabled = makeExportEntryVisible

            ImportToolStripButton.Visible = makeImportEntryVisible
            ImportToolStripMenuItem.Visible = makeImportEntryVisible
            ImportToolStripButton.Enabled = makeImportEntryVisible
            ImportToolStripMenuItem.Enabled = makeImportEntryVisible

            HarmonizeDependentItemsToolStripButton.Visible = makeHarmonizationVisible
            HarmonizeDependentItemsToolStripMenuItem.Visible = makeHarmonizationVisible
            HarmonizeDependentItemsToolStripButton.Enabled = makeHarmonizationVisible
            HarmonizeDependentItemsToolStripMenuItem.Enabled = makeHarmonizationVisible

            If MainTabControl.SelectedTab.Tag.ToString.ToLower = "start" Then
                ApplyUserPermissionsToMenuNew(e.SelectedBank.Id)
            End If

            If ExportToolStripMenuItem.Visible = False AndAlso ImportToolStripButton.Visible = False AndAlso
PublishToolStripButton.Visible = False AndAlso PreviewToolStripButton.Visible = False Then
                ToolStripSeparatorFile2.Visible = False
            Else
                ToolStripSeparatorFile2.Visible = True
            End If

            ApplyUserPermissionsToTabs(e.SelectedBank.Id)
            ApplyUserPermissionsToToolStripSplitNew(e.SelectedBank.Id)
            If _actionCommands.CurrentActionCommands IsNot Nothing Then
                _actionCommands.CurrentActionCommands.InitializeActionCommands(e.SelectedBank.Id)
            End If

            If MainTabControl.SelectedTab IsNot Nothing AndAlso MainTabControl.SelectedTab.Tag.ToString = "Items" AndAlso ItemGrid.SearchToolbarVisibility = True Then
                ItemGrid.ResetSearchBar()
            End If
            FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
            Dim titleOfActiveWindow As String = Me.Text
            If Not titleOfActiveWindow Is Nothing Then
                UpdateBreadcrumbPath(titleOfActiveWindow)
            End If
        Else
            MessageBox.Show(My.Resources.BankDeleted)
            MainBankBrowser_RefreshBanks(Nothing, Nothing)
        End If
    End Sub

    Private Sub MainTabControl_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles MainTabControl.Selecting
        DisposeGrid()
    End Sub

    Private Sub MainTabControl_TabIndexChanged(sender As Object, e As EventArgs) Handles MainTabControl.SelectedIndexChanged
        _actionCommands.SetNoContext()

        ItemGrid.HideFieldChooser()
        TestGrid.HideFieldChooser()
        TestPackageGrid.HideFieldChooser()
        TestTemplateGrid.HideFieldChooser()
        ItemLayoutTemplateGrid.HideFieldChooser()
        MediaGrid.HideFieldChooser()
        ControlTemplateGrid.HideFieldChooser()

        If MainTabControl.SelectedTab IsNot Nothing Then
            TestBuilderClientSettings.SelectedTabKey = Me.MainTabControl.SelectedTab.Tag.ToString()

            If Not MainTabControl.SelectedTab.Tag.ToString.ToLower = "start" Then
                For Each toolstrip As ToolStripMenuItem In DirectCast(DirectCast(MainMenu.Items("FileToolStripMenuItem"), ToolStripMenuItem).DropDownItems("NewToolStripMenuItem"), ToolStripMenuItem).DropDownItems
                    toolstrip.Enabled = False
                    toolstrip.Visible = False
                Next
            Else
                If MainBankBrowser.SelectedBank IsNot Nothing Then
                    ApplyUserPermissionsToMenuNew(MainBankBrowser.SelectedBank.Id)
                End If
            End If

            If ExportToolStripMenuItem.Visible = False AndAlso ImportToolStripButton.Visible = False AndAlso
PublishToolStripButton.Visible = False AndAlso PreviewToolStripButton.Visible = False Then
                ToolStripSeparatorFile2.Visible = False
            Else
                ToolStripSeparatorFile2.Visible = True
            End If
            With MainTabControl
                For Each obj As Object In MainTabControl.SelectedTab.Controls
                    If TypeOf obj Is IActionCommands Then
                        Dim idSelectedBank As Integer = 0
                        If MainBankBrowser.SelectedBank IsNot Nothing Then
                            idSelectedBank = MainBankBrowser.SelectedBank.Id
                        End If
                        DirectCast(obj, IActionCommands).InitializeActionCommands(idSelectedBank)
                        Exit For
                    End If
                Next
            End With

            FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
        End If
    End Sub

    Private Shared Function TabKeyToPermissionTarget(ByVal tabKey As String) As TestBuilderPermissionTarget
        Select Case tabKey
            Case "Start"
                Return TestBuilderPermissionTarget.BankEntity
            Case "Items", "ItemsCore"
                Return TestBuilderPermissionTarget.ItemEntity
            Case "ItemLayoutTemplates"
                Return TestBuilderPermissionTarget.ItemLayoutTemplateEntity
            Case "Tests"
                Return TestBuilderPermissionTarget.TestEntity
            Case "TestTemplates"
                Return TestBuilderPermissionTarget.TestTemplateEntity
            Case "ControlTemplates"
                Return TestBuilderPermissionTarget.ControlTemplateEntity
            Case "Media"
                Return TestBuilderPermissionTarget.MediaEntity
            Case "Aspects"
                Return TestBuilderPermissionTarget.AspectEntity
            Case "DataSources"
                Return TestBuilderPermissionTarget.DataSourceEntity
            Case "DataSourceTemplates"
                Return TestBuilderPermissionTarget.DataSourceTemplateEntity
            Case "TestPackages"
                Return TestBuilderPermissionTarget.TestPackageEntity
            Case "CustomProperties"
                Return TestBuilderPermissionTarget.CustomBankPropertyEntity
        End Select

        Return TestBuilderPermissionTarget.None
    End Function

    Private Sub ApplyUserPermissionsToTabs(ByVal selectedBankId As Integer)
        For Each tabPage As TabPage In _tabPages
            Dim permissionTarget As TestBuilderPermissionTarget = TabKeyToPermissionTarget(tabPage.Tag.ToString)
            Dim requiredAccess As TestBuilderPermissionAccess = TestBuilderPermissionAccess.List
            If permissionTarget = TestBuilderPermissionTarget.BankEntity Then
                requiredAccess = TestBuilderPermissionAccess.Refer
            End If
            If PermissionFactory.Instance.TryUserIsPermittedTo(requiredAccess, permissionTarget, selectedBankId) Then
                If Not MainTabControl.TabPages.Contains(tabPage) Then
                    MainTabControl.TabPages.Add(tabPage)
                End If
            ElseIf MainTabControl.TabPages.Contains(tabPage) Then
                MainTabControl.TabPages.Remove(tabPage)
            End If
        Next

        SelectStoredTab()
    End Sub

    Private Sub SelectStoredTab()

        If Not String.IsNullOrEmpty(TestBuilderClientSettings.SelectedTabKey) Then
            Dim selected As TabPage = (From t In _tabPages
                                       Let tab = CType(t, TabPage)
                                       Where tab.Tag.ToString = TestBuilderClientSettings.SelectedTabKey
                                       Select tab).FirstOrDefault()

            If selected IsNot Nothing AndAlso MainTabControl.TabPages.Contains(selected) Then
                MainTabControl.SelectedTab = selected
            Else
                MainTabControl.TabPages(0).Select()
            End If
        End If

        Application.DoEvents()
    End Sub

    Private Sub ApplyUserPermissionsToToolStripSplitNew(ByVal selectedBankId As Integer)
        Dim enableNewToolStripButton As Boolean = False
        For Each toolstrip As ToolStripMenuItem In NewToolStripSplitButton.DropDownItems
            Dim permissionTarget As TestBuilderPermissionTarget = TabKeyToPermissionTarget(toolstrip.Tag.ToString)
            Dim RequiredAccess As TestBuilderPermissionAccess = TestBuilderPermissionAccess.AddNew

            Dim enableToolStripButton As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(RequiredAccess, permissionTarget, selectedBankId)
            If enableToolStripButton Then enableNewToolStripButton = True
            toolstrip.Enabled = enableToolStripButton
            toolstrip.Visible = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.List, permissionTarget, selectedBankId)
        Next
        NewToolStripSplitButton.Enabled = enableNewToolStripButton
    End Sub

    Private Sub ApplyUserPermissionsToMenuNew(ByVal selectedBankId As Integer)
        Dim enableNewMenuButton As Boolean = False
        For Each toolstrip As ToolStripMenuItem In DirectCast(DirectCast(MainMenu.Items("FileToolStripMenuItem"), ToolStripMenuItem).DropDownItems("NewToolStripMenuItem"), ToolStripMenuItem).DropDownItems
            Dim permissionTarget As TestBuilderPermissionTarget = TabKeyToPermissionTarget(toolstrip.Tag.ToString)
            Dim RequiredAccess As TestBuilderPermissionAccess = TestBuilderPermissionAccess.AddNew

            Dim enableToolStripButton As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(RequiredAccess, permissionTarget, selectedBankId)
            If enableToolStripButton Then enableNewMenuButton = True
            toolstrip.Enabled = enableToolStripButton
            toolstrip.Visible = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.List, permissionTarget, selectedBankId)
        Next
        NewToolStripMenuItem.Enabled = enableNewMenuButton
    End Sub

    Private Sub FillGridOfCurrentTabPage(ByVal currentTab As TabPage,
        Optional selection As IPropertyEntity = Nothing,
        Optional selectedResourceId As Nullable(Of Guid) = Nothing)
        If currentTab IsNot Nothing And MainBankBrowser.SelectedBank IsNot Nothing Then

            If currentTab.Tag.ToString = "ItemsCore" Then
                Return
            End If

            Dim task As TaskType
            Dim bank As BankDto = MainBankBrowser.SelectedBank

            Dim inputParameterDictionary = New Dictionary(Of String, Object)
            inputParameterDictionary.Add("BankEntity", bank)

            If selection IsNot Nothing Then
                InvalidateCacheHelper.ClearCacheForBank(bank.Id)
                inputParameterDictionary.Add("Selection", selection.Id)
            End If
            If selectedResourceId IsNot Nothing Then
                inputParameterDictionary.Add("TabForResourceSelection", currentTab)
                inputParameterDictionary.Add("SelectedResourceId", selectedResourceId.Value)
            End If

            Select Case currentTab.Tag.ToString
                Case "CustomProperties"
                    ExportToolStripButton.Enabled = False
                    ExportToolStripMenuItem.Enabled = False
                    ExportToolStripButton.Visible = False
                    ExportToolStripMenuItem.Visible = False
                    ImportToolStripButton.Visible = False
                    ImportToolStripMenuItem.Visible = False
                    ImportToolStripButton.Enabled = False
                    ImportToolStripMenuItem.Enabled = False
                Case Else
                    Dim makeExportEntryVisible As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.Any, bank.Id)
                    Dim makeImportEntryVisible As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Import, TestBuilderPermissionTarget.Any, bank.Id)

                    If Not makeImportEntryVisible AndAlso currentTab.Tag.ToString.ToLower.StartsWith("items") Then
                        If PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ImportItemsWithWordTemplate, bank.Id, 0) OrElse PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ImportItemsWithExcelTemplate, bank.Id, 0) OrElse PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ImportItemsWithAccessTemplate, bank.Id, 0) Then
                            makeImportEntryVisible = True
                        End If
                    End If

                    ExportToolStripButton.Enabled = makeExportEntryVisible
                    ExportToolStripMenuItem.Enabled = makeExportEntryVisible
                    ExportToolStripButton.Visible = makeExportEntryVisible
                    ExportToolStripMenuItem.Visible = makeExportEntryVisible
                    ImportToolStripButton.Visible = makeImportEntryVisible
                    ImportToolStripMenuItem.Visible = makeImportEntryVisible
                    ImportToolStripButton.Enabled = makeImportEntryVisible
                    ImportToolStripMenuItem.Enabled = makeImportEntryVisible
            End Select

            Dim makeHarmonizationVisible As Boolean = ShowHarmonisationOptions(bank.Id, currentTab.Tag.ToString)
            HarmonizeDependentItemsToolStripButton.Visible = makeHarmonizationVisible
            HarmonizeDependentItemsToolStripMenuItem.Visible = makeHarmonizationVisible
            HarmonizeDependentItemsToolStripButton.Enabled = makeHarmonizationVisible
            HarmonizeDependentItemsToolStripMenuItem.Enabled = makeHarmonizationVisible

            Select Case currentTab.Tag.ToString
                Case "Start"
                    task = TaskType.GetBankStartPageInfo
                    bank = MainBankBrowser.SelectedBank
                    BankStartPage.BankId = MainBankBrowser.SelectedBank.Id
                    BankStartPage.BankInfo = GetFullBankPath()
                Case "Items"
                    task = TaskType.GetItemsForBank
                Case "Tests"
                    task = TaskType.GetTestsForBank
                Case "TestPackages"
                    task = TaskType.GetTestPackageForBank
                Case "TestTemplates"
                    task = TaskType.GetTestTemplatesForBank
                Case "ItemLayoutTemplates"
                    task = TaskType.GetItemLayoutTemplatesForBank
                Case "ControlTemplates"
                    task = TaskType.GetControlTemplatesForBank
                Case "Media"
                    task = TaskType.GetMediaForBank
                Case "Aspects"
                    task = TaskType.GetAspectsForBank
                Case "DataSources"
                    task = TaskType.GetDataSourcesForBank
                Case "DataSourceTemplates"
                    task = TaskType.GetDataSourceTemplatesForBank
                Case "CustomProperties"
                    task = TaskType.GetCustomPropertiesForBank
                Case Else
                    Exit Sub
            End Select

            If bank Is Nothing Then
                If MessageBox.Show(String.Format(My.Resources.MainForm_BankDoesNotExist, MainBankBrowser.SelectedBank.Name), My.Resources.MainForm_BankDoesNotExistTitle, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")
                End If
            Else
                BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(task, inputParameterDictionary), "FillGrid")
            End If
        End If
    End Sub

    Private Sub DisposeGrid()
        If MainTabControl IsNot Nothing AndAlso MainTabControl.SelectedTab IsNot Nothing Then
            Select Case MainTabControl.SelectedTab.Tag.ToString
                Case "Start"
                    BankStartPage.DataSource = Nothing
                Case "Items"
                    ItemGrid.DisposeGrid()
                Case "Tests"
                    TestGrid.DisposeGrid()
                Case "TestPackages"
                    TestPackageGrid.DisposeGrid()
                Case "TestTemplates"
                    TestTemplateGrid.DisposeGrid()
                Case "ItemLayoutTemplates"
                    ItemLayoutTemplateGrid.DisposeGrid()
                Case "ControlTemplates"
                    ControlTemplateGrid.DisposeGrid()
                Case "Media"
                    MediaGrid.DisposeGrid()
                Case "Aspects"
                    AspectGrid.DisposeGrid()
                Case "DataSources"
                    DataSourceGrid.DisposeGrid()
                Case "DataSourceTemplates"
                    DataSourceTemplateGrid.DisposeGrid()
                Case "CustomProperties"
                    CustomPropertyGrid.DisposeGrid()
            End Select
        End If
    End Sub

    Private Sub SelectResourceInGridWithinTab(tab As TabPage, resourceId As Guid)
        Dim grid As GridBase = FindGridControl(tab.Controls)
        If grid IsNot Nothing AndAlso grid.DataSource IsNot Nothing AndAlso TypeOf grid.DataSource Is IList(Of ResourceDto) Then
            For Each e In DirectCast(grid.DataSource, IList(Of ResourceDto))
                If e.ResourceId = resourceId Then
                    grid.SelectedEntity = e
                End If
            Next
        End If
    End Sub

    Private Function FindGridControl(controls As Control.ControlCollection) As GridBase
        If controls IsNot Nothing Then
            For Each control As Control In controls
                If TypeOf control Is GridBase Then
                    Return DirectCast(control, GridBase)
                End If
                Dim grid As GridBase = FindGridControl(control.Controls)
                If grid IsNot Nothing Then
                    Return grid
                End If
            Next
        End If
        Return Nothing
    End Function

    Private Sub SetTitleBar()
        Me.Text = My.Application.Info.Title
        If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("QB_TitlebarAddition")) Then
            Me.Text += "   " & ConfigurationManager.AppSettings("QB_TitlebarAddition")
        End If
    End Sub

    Public Function GetOwnedFormsOfType(ByVal type As Type) As IEnumerable(Of Form)
        Return (From form In Me.OwnedForms Where form.GetType Is type).ToList()
    End Function

    Private Function CanRemoveResource(ByVal e As DeletingRowsEventArgs, ByVal friendlyType As String) As Boolean
        Dim resourcesWithWarnAction As New Dictionary(Of String, ActionEntity)
        Dim resourcesWithProhibitAction As New Dictionary(Of String, ActionEntity)

        For Each item As GridEXSelectedItem In e.ToBeDeleted
            Select Case item.RowType
                Case RowType.Record
                    Dim resource As ResourceEntity = TryCast(item.GetRow.DataRow, ResourceEntity)
                    If resource IsNot Nothing AndAlso resource.State IsNot Nothing AndAlso resource.State.StateActionCollection.Count > 0 Then
                        Dim resultIndex As List(Of Integer) = resource.State.StateActionCollection.FindMatches(StateActionFields.Target = "resourceediting")

                        If resultIndex.Count = 1 Then
                            Dim stateaction As StateActionEntity = resource.State.StateActionCollection(resultIndex(0))

                            Select Case stateaction.Action.Name.ToLower
                                Case "warn"
                                    resourcesWithWarnAction.Add(resource.Name, stateaction.Action)
                                Case "prohibit"
                                    resourcesWithProhibitAction.Add(resource.Name, stateaction.Action)
                            End Select
                        End If
                    End If
            End Select
        Next
        Return ShowDeletionMessage(resourcesWithWarnAction, resourcesWithProhibitAction, friendlyType)
    End Function

    Private Function ShowDeletionMessage(resourcesWithWarnAction As Dictionary(Of String, ActionEntity),
                                         resourcesWithProhibitAction As Dictionary(Of String, ActionEntity),
                                         friendlyType As String) As Boolean
        Dim message As String = String.Empty
        Dim summerytext As New StringBuilder

        If resourcesWithProhibitAction.Count > 0 Then
            summerytext.AppendLine(String.Format(My.Resources.FollowingResourcesPreventDeleting, resourcesWithProhibitAction.Count, friendlyType))
            For Each resName As String In resourcesWithProhibitAction.Keys
                Dim action As ActionEntity = resourcesWithProhibitAction(resName)
                summerytext.AppendLine($"{resName} -> {action.StateActionCollection(0).State.Title}")
            Next
            summerytext.AppendLine()
        End If

        If resourcesWithWarnAction.Count > 0 Then
            summerytext.AppendLine(String.Format(My.Resources.FollowingResourcesResultInAWarning, resourcesWithWarnAction.Count, friendlyType))

            For Each resName As String In resourcesWithWarnAction.Keys
                Dim action As ActionEntity = resourcesWithWarnAction(resName)
                summerytext.AppendLine($"{resName} -> {action.StateActionCollection(0).State.Title}")
            Next

        End If

        If resourcesWithProhibitAction.Count > 0 Then
            message =
                $"{summerytext.ToString}{ControlChars.CrLf}{String.Format(My.Resources.NotAbleToDelete, friendlyType)}"
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        ElseIf resourcesWithWarnAction.Count > 0 Then
            message = $"{summerytext.ToString}{ControlChars.CrLf}{String.Format(My.Resources.SureToDelete)}"
            If MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        Else
            message = String.Format(My.Resources.SureToDelete)
            If MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Private Function ShowHarmonisationOptions(bankId As Integer, tabKey As String) As Boolean
        Select Case tabKey.ToLower
            Case "items", "itemscore"
                Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ImportRawData, TestBuilderPermissionTarget.ItemEntity, bankId)
            Case "itemlayouttemplates"
                Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ImportRawData, TestBuilderPermissionTarget.ItemLayoutTemplateEntity, bankId)
        End Select
        Return False
    End Function




    Private Sub ActionContextChanged(ByVal sender As Object, ByVal e As ContextChangedEventArgs) Handles _actionCommands.ContextChanged
        If e.ActionCommands Is Nothing OrElse e.BankId = 0 Then
            NewToolStripSplitButton.Visible = True
            NewToolStripMenuItem.Visible = True
            ToolStripSeparatorFile2.Visible = True
            ToolStripSeparatorFile1.Visible = True
            RefreshToolStripMenuItem.Visible = True
            ToolStripSeparator4.Visible = True
            ToolStripSeparator5.Visible = False

            DeleteToolStripButton.Visible = False
            DeleteToolStripMenuItem.Visible = False

            EditToolStripButton.Visible = False
            EditFileToolStripMenuItem.Visible = False

            PublishToolStripButton.Visible = False
            PublishToolStripMenuItem.Visible = False

            PreviewToolStripMenuItem.Visible = False
            PreviewToolStripButton.Visible = False

            MultiEditToolStripMenuItem.Visible = False
            ToolStripSeparator1.Visible = False

            PropertyToolStripButton.Visible = False
            ProperiesToolStripMenuItem.Visible = False

            SelectAllToolStripMenuItem.Visible = False

            ToolStripSeparator2.Visible = False
            ToolStripSeparator3.Visible = False

            ProperiesToolStripMenuItem.Visible = False
            SearchToolStripButton.Visible = False
            SearchToolStripMenuItem.Visible = False

            ReportsToolStripButton.Visible = False
            ReportsToolStripMenuItem.Visible = False

            MoveResourceToolStripMenuItem.Visible = False
        Else
            NewToolStripSplitButton.Visible = True
            NewToolStripSplitButton.Enabled = e.ActionCommands.AddNewIsPermitted(e.BankId)
            NewToolStripMenuItem.Visible = NewToolStripSplitButton.Visible
            NewToolStripMenuItem.Enabled = NewToolStripSplitButton.Enabled

            EditToolStripButton.Visible = e.ActionCommands.AllowEdit
            EditToolStripButton.Enabled = e.ActionCommands.EditIsPermitted(e.BankId)
            EditFileToolStripMenuItem.Visible = EditToolStripButton.Visible
            EditFileToolStripMenuItem.Enabled = EditToolStripButton.Enabled

            DeleteToolStripButton.Visible = e.ActionCommands.AllowDelete
            DeleteToolStripButton.Enabled = e.ActionCommands.DeleteIsPermitted(e.BankId)
            DeleteToolStripMenuItem.Visible = DeleteToolStripButton.Visible
            DeleteToolStripMenuItem.Enabled = DeleteToolStripButton.Enabled

            RefreshToolStripMenuItem.Visible = e.ActionCommands.AllowSynchronize
            RefreshToolStripMenuItem.Enabled = e.ActionCommands.SynchronizeIsPermitted(e.BankId)
            RefreshToolStripButton.Visible = e.ActionCommands.AllowSynchronize
            RefreshToolStripButton.Enabled = e.ActionCommands.SynchronizeIsPermitted(e.BankId)

            PublishToolStripButton.Visible = e.ActionCommands.AllowPublish
            PublishToolStripButton.Enabled = e.ActionCommands.PublishIsPermitted(e.BankId)
            PublishToolStripMenuItem.Visible = e.ActionCommands.AllowPublish
            PublishToolStripMenuItem.Enabled = e.ActionCommands.PublishIsPermitted(e.BankId)

            PreviewToolStripMenuItem.Visible = e.ActionCommands.AllowPreview
            PreviewToolStripMenuItem.Enabled = e.ActionCommands.PreviewIsPermitted(e.BankId)
            PreviewToolStripButton.Visible = e.ActionCommands.AllowPreview
            PreviewToolStripButton.Enabled = e.ActionCommands.PreviewIsPermitted(e.BankId)

            PropertyToolStripButton.Visible = e.ActionCommands.AllowShowProperties
            PropertyToolStripButton.Enabled = e.ActionCommands.ShowPropertiesIsPermitted(e.BankId)
            ProperiesToolStripMenuItem.Visible = e.ActionCommands.AllowShowProperties
            ProperiesToolStripMenuItem.Enabled = e.ActionCommands.ShowPropertiesIsPermitted(e.BankId)

            MultiEditToolStripMenuItem.Visible = e.ActionCommands.AllowExecute
            MultiEditToolStripMenuItem.Enabled = e.ActionCommands.MultiEditIsPermitted(e.BankId)
            ToolStripSeparator1.Visible = e.ActionCommands.AllowExecute

            EditFileToolStripMenuItem.Visible = e.ActionCommands.EditIsPermitted(e.BankId)
            EditToolStripButton.Visible = e.ActionCommands.EditIsPermitted(e.BankId)

            DeleteToolStripMenuItem.Visible = e.ActionCommands.DeleteIsPermitted(e.BankId)
            DeleteToolStripButton.Visible = e.ActionCommands.DeleteIsPermitted(e.BankId)

            RefreshToolStripMenuItem.Visible = e.ActionCommands.SynchronizeIsPermitted(e.BankId)
            RefreshToolStripButton.Visible = e.ActionCommands.SynchronizeIsPermitted(e.BankId)

            ProperiesToolStripMenuItem.Visible = e.ActionCommands.ShowPropertiesIsPermitted(e.BankId)
            PropertyToolStripButton.Visible = e.ActionCommands.ShowPropertiesIsPermitted(e.BankId)

            SelectAllToolStripMenuItem.Visible = e.ActionCommands.AllowSelectAll
            ToolStripSeparator2.Visible = e.ActionCommands.AllowSelectAll
            ToolStripSeparator3.Visible = e.ActionCommands.AllowSelectAll

            ReportsToolStripButton.Enabled = e.ActionCommands.AllowReports
            ReportsToolStripMenuItem.Enabled = e.ActionCommands.AllowReports
            ToolStripSeparator5.Visible = e.ActionCommands.AllowReports
            ReportsToolStripButton.Visible = e.ActionCommands.AllowReports
            ReportsToolStripMenuItem.Visible = e.ActionCommands.AllowReports

            ToolStripSeparatorFile1.Visible = True

            SearchToolStripButton.Visible = e.ActionCommands.AllowFastSearch
            SearchToolStripMenuItem.Visible = e.ActionCommands.AllowFastSearch

            MoveResourceToolStripMenuItem.Visible = e.ActionCommands.AllowMoveResources
            MoveResourceToolStripMenuItem.Enabled = e.ActionCommands.MoveResourceIsPermitted(e.BankId)
        End If
    End Sub




    Private Sub NewToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click
        If _actionCommands.CurrentActionCommands IsNot Nothing Then
            _actionCommands.CurrentActionCommands.AddNew()
        End If
    End Sub

    Private Sub NewToolStripSplitButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripSplitButton.ButtonClick
        If _actionCommands.CurrentActionCommands IsNot Nothing AndAlso _actionCommands.CurrentActionCommands.AllowAddNew Then
            _actionCommands.CurrentActionCommands.AddNew()
        Else
            NewToolStripSplitButton.ShowDropDown()
        End If

    End Sub

    Private Sub PublishToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PublishToolStripButton.Click
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)

            If grid.SelectedEntities.Count > 0 Then
                _actionCommands.CurrentActionCommands.Publish()
            ElseIf TypeOf grid Is TestGrid Then
                MessageBox.Show(My.Resources.MainForm_SelectTestToPublish)
            ElseIf TypeOf grid Is TestPackageGrid Then
                MessageBox.Show(My.Resources.MainForm_SelectTestPackageToPublish)
            End If
        Else
            If _actionCommands.CurrentActionCommands IsNot Nothing Then
                _actionCommands.CurrentActionCommands.Publish()
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        End If
    End Sub

    Private Sub SynchonizeToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RefreshToolStripMenuItem.Click, RefreshToolStripButton.Click
        InvalidateCacheHelper.ClearCacheForBank(MainBankBrowser.SelectedBank.Id)

        If _actionCommands.CurrentActionCommands IsNot Nothing Then
            _actionCommands.CurrentActionCommands.Synchronize()
        Else
            Me.Cursor = Cursors.WaitCursor
            BankStartPage.Refresh()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub PropertyToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PropertyToolStripButton.Click, ProperiesToolStripMenuItem.Click
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)

            If grid.SelectedEntities.Count > 0 Then
                _actionCommands.CurrentActionCommands.ShowProperties()
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else
            MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
        End If
    End Sub




    Private Sub StatusBarMessagePublished(ByVal sender As Object, ByVal e As StatusBarMessagePublishedEventArgs) Handles _statusBarListener.StatusBarMessagePublished
        If e Is Nothing Then
            Throw New ArgumentNullException("e")
        End If

        ToolStripStatusBarMessage.Text = e.Message
        ToolStripStatusBarMessage.AccessibleDescription = e.Message
    End Sub

    Private Sub UsernameStatusLabel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UsernameStatusLabel.Click
        Dim user As New UserEntity(DirectCast(My.User.CurrentPrincipal.Identity(), TestBuilderIdentity).UserId)
        Dim userProperty As New UserPropertyDialog(user)
        userProperty.ShowDialog()
    End Sub




    Private Sub MainBankbrowser_AddNewBank(ByVal sender As Object, ByVal e As AddBankEventArgs) Handles MainBankBrowser.AddNewBank
        Dim addBankDialog As New AddBank(e.ParentBank)
        Dim result As DialogResult = addBankDialog.ShowDialog()

        If result = DialogResult.OK Then
            MainBankBrowser.StoreBankTreeState(BankBrowser.BankAction.Add, addBankDialog.NewBankId)
            BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")
        End If
    End Sub

    Private Sub MainBankBrowser_ShowBankProperties(ByVal sender As Object, ByVal e As BankSelectedEventArgs) Handles MainBankBrowser.ShowBankProperties
        Dim bank = BankFactory.Instance.GetBankWithOptions(e.SelectedBank.Id, True, False)
        Using propertyDialog As New BankPropertyDialog(bank)
            propertyDialog.Owner = Me

            Dim result As DialogResult = propertyDialog.ShowDialog()

            If result = DialogResult.OK Or (propertyDialog.ApplyButtonInvoked And result = DialogResult.Cancel) Then
                MainBankBrowser.StoreBankTreeState(BankBrowser.BankAction.Update, -1)
                BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")
            End If
        End Using
    End Sub

    Private Sub MainBankBrowser_DeleteBank(ByVal sender As Object, ByVal e As BankSelectedEventArgs) Handles MainBankBrowser.DeleteBank
        Try
            If MessageBox.Show(String.Format(My.Resources.MainForm_DoYouReallyWantToDeleteBankAndAllOfItsResources, e.SelectedBank.Name), Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Dim bank = BankFactory.Instance.GetBank(e.SelectedBank.Id)
                Dim bankDeletionResult As String = BankFactory.Instance.DeleteBank(bank)
                If String.IsNullOrEmpty(bankDeletionResult) Then
                    MainBankBrowser.StoreBankTreeState(BankBrowser.BankAction.Delete, -1)
                    BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")
                Else
                    MessageBox.Show(bankDeletionResult, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        Catch ex As Exception
            Throw New UIException(My.Resources.ErrorDeletingEntity, ex)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub MainBankBrowser_ClearBank(ByVal sender As Object, ByVal e As BankSelectedEventArgs) Handles MainBankBrowser.ClearBank
        Try
            If MessageBox.Show(My.Resources.DoYouReallyWantToClearAllTheResourcesInTheBank, Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                If BankFactory.Instance.ClearBank(e.SelectedBank.Id) Then
                    MainBankBrowser.StoreBankTreeState(BankBrowser.BankAction.Update, -1)
                    If MainTabControl.SelectedTab.Tag.ToString = "Start" Then
                        BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")
                    Else
                        BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetItemsForBank, e.SelectedBank), "BankHierarchy")
                    End If
                Else
                    MessageBox.Show(My.Resources.TheBankCouldnTBeCleared, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        Catch ex As Exception
            Throw New UIException(My.Resources.ErrorDeletingEntity, ex)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub MainBankBrowser_ClearAndDeleteBankHierarchical(ByVal sender As Object, ByVal e As BankSelectedEventArgs) Handles MainBankBrowser.ClearAndDeleteBankHierarchical
        Try
            If MessageBox.Show(My.Resources.AboutToClearAndDeleteResourcesInTheBankAndSubbanks, Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Dim bankDeletionResult As String = BankFactory.Instance.ClearAndDeleteBankHierarchical(e.SelectedBank.Id)
                If String.IsNullOrEmpty(bankDeletionResult) Then
                    MainBankBrowser.StoreBankTreeState(BankBrowser.BankAction.Delete, -1)
                    BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")
                Else
                    MessageBox.Show(bankDeletionResult, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        Catch ex As Exception
            Throw New UIException(My.Resources.ErrorDeletingEntity, ex)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub MainBankBrowser_RefreshBanks(ByVal sender As Object, ByVal e As BankSelectedEventArgs) Handles MainBankBrowser.RefreshBanks
        MainBankBrowser.StoreBankTreeState(BankBrowser.BankAction.Refresh, -1)
        BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetBanks), "BankHierarchy")
    End Sub


    Private Sub MultiEditToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MultiEditToolStripMenuItem.Click, ItemGrid.MultiEdit
        Dim isOneBankSelected As Boolean = True
        Dim bankId As Integer = 0
        For Each itemResourceEntity In ItemGrid.SelectedEntities
            If bankId = 0 Then
                bankId = itemResourceEntity.BankId
            Else
                If bankId <> itemResourceEntity.BankId Then
                    isOneBankSelected = False
                    Exit For
                End If
            End If
        Next

        If isOneBankSelected Then
            _preSelectedItemsInGrid = New List(Of Guid)

            For Each selectedRow As GridEXSelectedItem In ItemGrid.GridControl.SelectedItems
                If selectedRow.GetRow().DataRow IsNot Nothing AndAlso TypeOf selectedRow.GetRow().DataRow Is ItemResourceDto Then
                    _preSelectedItemsInGrid.Add(CType(selectedRow.GetRow().DataRow, ItemResourceDto).ResourceId)
                End If
            Next

            Try
                Dim newMultiSelectItemEditWizardForm As New MultiSelectItemEditWizardForm(MainBankBrowser.SelectedBank.Id, ItemGrid.SelectedEntities)

                If newMultiSelectItemEditWizardForm.ShowDialog(Me) = DialogResult.OK Then
                    FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
                End If
                newMultiSelectItemEditWizardForm = Nothing
            Catch ex As Exception
                ShowWizardExceptionHandler(Me, GetType(MultiSelectItemEditWizardForm), ex, MainBankBrowser.SelectedBank.Id)
            End Try

        Else
            MessageBox.Show(My.Resources.MultiSelectItemEditWizardMultipleBanksSelected, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim optionsDialog As New OptionsDialog

        If optionsDialog.ShowDialog() = DialogResult.OK Then
            ExitToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim aboutDialog As New AboutDialog

        aboutDialog.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub UserAccountToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UserAccountToolStripMenuItem.Click
        Dim authDialog As New AuthorizationManagementDialog()

        authDialog.ShowDialog()
    End Sub

    Private Sub AnnouncementToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AnnouncementControlToolStripMenuItem.Click
        _windowFacade.OpenAnnouncementDialog()
    End Sub

    Private Sub NewItemToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewItemToolStripMenuItem.Click
        ItemGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub NewSelectionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewSelectionToolStripMenuItem.Click
        DataSourceGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub NewTestToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewTestToolStripMenuItem.Click
        TestGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub NewTestPackageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTestPackageToolStripMenuItem.Click, TestPackagesToolStripMenuItem.Click
        TestPackageGrid_AddNewTestPackage(Nothing, Nothing)
    End Sub

    Private Sub NewTestTemplateToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewTestTemplateToolStripMenuItem.Click
        TestTemplateGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub NewAspectToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewAspectToolStripMenuItem.Click
        AspectGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub SelectionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectionToolStripMenuItem.Click
        DataSourceGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub NewMediaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewMediaToolStripMenuItem.Click
        MediaGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub ItemToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ItemToolStripMenuItem.Click
        ItemGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub TestToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TestToolStripMenuItem.Click
        TestGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub TestTemplateToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TestTemplateToolStripMenuItem.Click
        TestTemplateGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub MediaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MediaToolStripMenuItem.Click
        MediaGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub AspectsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AspectsToolStripMenuItem.Click
        AspectGrid_AddNewItem(Nothing, Nothing)
    End Sub

    Private Sub CustomPropertyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomPropertyMenuItem.Click
        CustomPropertyGrid_AddNewProperty(sender, e)
    End Sub

    Private Sub SourceTextTemplateToolStripMenuItem_Clik(sender As Object, e As EventArgs) Handles SourceTextTemplateToolStripMenuItem.Click, NewSourceTextTemplateToolStripMenuItem.Click
        _windowFacade.CreateNewSourceText(MainBankBrowser.SelectedBank.Id, True)
    End Sub

    Private Sub ImportResourcesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImportToolStripMenuItem.Click, ImportToolStripButton.Click
        Try
            Using importWiz As New ImportFormWizard()
                importWiz.BankId = MainBankBrowser.SelectedBank.Id
                importWiz.ShowDialog(Me)
                If importWiz.DialogResult <> DialogResult.Cancel OrElse importWiz.ChangesMade Then
                    InvalidateCacheHelper.ClearCacheForBank(MainBankBrowser.SelectedBank.Id)
                    FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
                End If
            End Using
        Catch ex As Exception
            ShowWizardExceptionHandler(Me, GetType(ImportFormWizard), ex, MainBankBrowser.SelectedBank.Id)
        End Try
    End Sub

    Private Sub PublishToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PublishToolStripMenuItem.Click
        PublishToolStripButton_Click(sender, e)
    End Sub

    Private Sub ExportResourcesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExportToolStripButton.Click, ExportToolStripMenuItem.Click, TestGrid.Export, TestPackageGrid.Export, TestTemplateGrid.Export, ItemGrid.Export, MediaGrid.Export, ItemLayoutTemplateGrid.Export, ControlTemplateGrid.Export, AspectGrid.Export, DataSourceGrid.Export
        Try
            Using exportWiz As New ExportFormWizard
                Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)
                If TypeOf ctl Is GridBase Then
                    Dim grid As GridBase = DirectCast(ctl, GridBase)
                    If grid.SelectedEntities.Count > 0 Then
                        exportWiz.BankId = MainBankBrowser.SelectedBank.Id
                        exportWiz.BankBreadCrumb = GetFullBankPath()
                        exportWiz.CurrentTabTitle = MainTabControl.SelectedTab.Text
                        exportWiz.SelectedEntities = ResourceFactory.Instance.GetResourcesByIdsWithOption(grid.SelectedEntities.Select(Function(r) r.ResourceId).ToList, DetermineResourceTypeFactory(grid.SelectedEntities.First()), New ResourceRequestDTO()).OfType(Of ResourceEntity).ToList
                        exportWiz.ShowDialog(Me)
                    Else
                        MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
                    End If
                ElseIf TypeOf ctl Is BankStartPageControl Then
                    exportWiz.BankId = MainBankBrowser.SelectedBank.Id
                    exportWiz.SelectedEntities = Nothing

                    exportWiz.BankBreadCrumb = GetFullBankPath()
                    exportWiz.CurrentTabTitle = MainTabControl.SelectedTab.Text

                    exportWiz.ShowDialog(Me)
                Else
                    MessageBox.Show(My.Resources.MainForm_SelectResourcesToExport)
                End If
            End Using
        Catch ex As Exception
            ShowWizardExceptionHandler(Me, GetType(ExportFormWizard), ex, MainBankBrowser.SelectedBank.Id)
        End Try
    End Sub

    Private Function DetermineResourceTypeFactory(resource As ResourceDto) As IEntityFactory2
        Select Case resource.GetType()
            Case GetType(ItemResourceDto)
                Return New ItemResourceEntityFactory()
            Case GetType(GenericResourceDto)
                Return New GenericResourceEntityFactory()
            Case GetType(AssessmentTestResourceDto)
                Return New AssessmentTestResourceEntityFactory()
            Case GetType(TestPackageResourceDto)
                Return New TestPackageResourceEntityFactory()
            Case GetType(ItemLayoutTemplateResourceDto)
                Return New ItemLayoutTemplateResourceEntityFactory()
            Case GetType(ControlTemplateResourceDto)
                Return New ControlTemplateResourceEntityFactory()
            Case GetType(AspectResourceDto)
                Return New AspectResourceEntityFactory()
            Case GetType(DataSourceResourceDto)
                Return New DataSourceResourceEntityFactory()
            Case Else
                Return New ResourceEntityFactory()
        End Select
    End Function

    Private Sub OpenReports_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ReportsToolStripMenuItem.Click, ReportsToolStripButton.Click, TestGrid.ShowReport, TestTemplateGrid.ShowReport, ItemGrid.ShowReport, MediaGrid.ShowReport, ItemLayoutTemplateGrid.ShowReport, ControlTemplateGrid.ShowReport, AspectGrid.ShowReport, DataSourceGrid.ShowReport, TestPackageGrid.ShowReport
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)

            If grid.SelectedEntities.Count > 0 Then
                OpenReportWizard()
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else
            OpenReportWizard()
        End If
    End Sub

    Private Sub OpenReportWizard()
        If ReportsAvailable() Then
            Try
                Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)
                Dim bankId As Integer = MainBankBrowser.SelectedBank.Id
                Dim gridBase = TryCast(ctl, GridBase)
                If gridBase IsNot Nothing Then
                    Using reportWizard As New ReportFormWizard(DirectCast(ctl, GridBase), bankId, MainTabControl.SelectedTab.Text, GetFullBankPath())
                        reportWizard.ShowDialog(Me)
                    End Using
                Else
                    Using reportWizard As New ReportFormWizard(bankId, MainTabControl.SelectedTab.Text, GetFullBankPath())
                        reportWizard.ShowDialog(Me)
                    End Using
                End If
            Catch ex As Exception
                ShowWizardExceptionHandler(Me, GetType(ReportFormWizard), ex, MainBankBrowser.SelectedBank.Id)
            End Try
        Else
            MessageBox.Show(My.Resources.NoReportsAvailable, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function ReportsAvailable() As Boolean
        Dim returnValue As Boolean = False
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)
        Dim gridBase As GridBase = TryCast(ctl, GridBase)
        If (gridBase IsNot Nothing) Then
            returnValue = ConfigPluginHelper.AtLeastOneHandlerAvailable("reportHandlers", gridBase.SelectedEntities)
        Else
            returnValue = ConfigPluginHelper.AtLeastOneHandlerAvailable("reportHandlers", New List(Of ResourceDto))
        End If
        Return returnValue
    End Function

    Private Sub SyncDependantItems_Click(sender As Object, e As EventArgs) Handles HarmonizeDependentItemsToolStripButton.Click, HarmonizeDependentItemsToolStripMenuItem.Click, ItemLayoutTemplateGrid.HarmonizeDependantItems, ItemGrid.HarmonizeDependantItems
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)
            If grid.SelectedEntities.Count > 0 Then
                Dim synchronizeItems As New RefreshItemLayouts(grid.SelectedEntities, My.Resources.HarmonizationDialogTitle)
                synchronizeItems.Execute(Nothing)
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else
            MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
        End If
    End Sub

    Private Sub UpdateBreadcrumbPath(titleOfActiveWindow As String)
        Dim fullBankPath As String = GetFullBankPath()
        BreadcrumbBankPartToolStripStatusLabel.Text = fullBankPath
        BreadcrumbBankPartToolStripStatusLabel.AccessibleDescription = BreadcrumbBankPartToolStripStatusLabel.Text
        BreadcrumbStripStatusLabel.Text = "-> " & titleOfActiveWindow.ToString()
        BreadcrumbStripStatusLabel.AccessibleDescription = BreadcrumbStripStatusLabel.Text
    End Sub

    Private Sub MoveResources(sender As Object, e As EventArgs) Handles MoveResourceToolStripMenuItem.Click,
                                                                        ItemGrid.MoveResources,
                                                                        ItemLayoutTemplateGrid.MoveResources,
                                                                        AspectGrid.MoveResources,
                                                                        CustomPropertyGrid.MoveResources,
                                                                        TestGrid.MoveResources,
                                                                        TestPackageGrid.MoveResources,
                                                                        TestTemplateGrid.MoveResources,
                                                                        MediaGrid.MoveResources,
                                                                        ControlTemplateGrid.MoveResources

        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)

            If grid.SelectedEntities.Count > 0 Then
                Dim listOfBanks As List(Of Integer)
                listOfBanks = grid.SelectedEntities.Select(Function(x) x.BankId).Distinct().ToList()
                If listOfBanks.Count > 1 Then
                    MessageBox.Show(My.Resources.ResourcesToMoveMustBeInSameBank)
                Else
                    Dim resourceSToMove() As Guid
                    resourceSToMove = grid.SelectedEntities.Select(Function(x) x.ResourceId).ToArray()
                    If _windowFacade.ShowResourceMoverWizard(listOfBanks(0), resourceSToMove) Then
                        FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
                    End If
                End If
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else
            MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
        End If

    End Sub

    Private Sub PreviewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PreviewToolStripMenuItem.Click, PreviewToolStripButton.Click
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is TestPackageGrid Then
            Dim grid As TestPackageGrid = DirectCast(ctl, TestPackageGrid)

            If grid.SelectedEntities.Count > 0 Then
                TestPackageGrid_PreviewEntity(Nothing, Nothing)
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        ElseIf TypeOf ctl Is TestGrid Then
            Dim grid As TestGrid = DirectCast(ctl, TestGrid)

            If grid.SelectedEntities.Count > 0 Then
                If TestsContainItems(grid.SelectedEntities.OfType(Of AssessmentTestResourceDto)) Then TestGrid_PreviewEntity(Nothing, Nothing)
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else

            MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
        End If
    End Sub

    Private Function TestsContainItems(tests As IEnumerable(Of AssessmentTestResourceDto)) As Boolean
        For Each testEntity In tests
            Dim containsItems = (From dependency In DtoFactory.Test.GetDependencies(testEntity.ResourceId)
                                 Where TypeOf dependency Is ItemResourceDto).Count > 0
            If Not containsItems Then
                MessageBox.Show(My.Resources.TestDoesNotContainAnyItemsPleaseAddItemsToTheTestBeforePublishing, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub EditFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditFileToolStripMenuItem.Click, EditToolStripButton.Click
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)

            If grid.SelectedEntities.Count > 0 Then
                _actionCommands.CurrentActionCommands.Edit()
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else
            MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteToolStripMenuItem.Click, DeleteToolStripButton.Click
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)

            If grid.SelectedEntities.Count > 0 Then
                _actionCommands.CurrentActionCommands.Delete()
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else
            MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        _actionCommands.CurrentActionCommands.SelectAll()
    End Sub

    Private Sub SearchToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchToolStripButton.Click
        _actionCommands.CurrentActionCommands.ToggleFastSearchBar()
        ToggleSeachBarMenuLayout(SearchToolStripButton.Checked)
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchToolStripMenuItem.Click
        _actionCommands.CurrentActionCommands.ToggleFastSearchBar()
        ToggleSeachBarMenuLayout(SearchToolStripMenuItem.Checked)
    End Sub

    Private Sub ToggleSeachBarMenuLayout(ByVal value As Boolean)
        SearchToolStripMenuItem.Checked = value
        SearchToolStripButton.Checked = value
        If value Then
            SearchToolStripButton.Font = New Font(SearchToolStripButton.Font.FontFamily, SearchToolStripButton.Font.Size, FontStyle.Bold)
        Else
            SearchToolStripButton.Font = New Font(SearchToolStripButton.Font.FontFamily, SearchToolStripButton.Font.Size, FontStyle.Regular)
        End If
    End Sub

    Private Sub CreateProposalFromTemplate(ByVal selectedTemplateEntity As AssessmentTestResourceEntity, ByVal sectionsWithDataSources As Dictionary(Of TestSection2, DataSourceSettings), ByVal dbResourceManager As DataBaseResourceManager)
        If sectionsWithDataSources.Count > 0 Then
            Dim dialog As New RefreshDataSourcesDialog(sectionsWithDataSources.Values, dbResourceManager, True)
            dialog.StartPosition = FormStartPosition.CenterParent
            If dialog.ShowDialog(Me) = DialogResult.OK Then
                Try
                    Me.Cursor = Cursors.WaitCursor
                    Dim numberOfProposals As Integer = dialog.NumberOfProposalsWanted

                    Dim proposalCreator As ICreateProposal = New ProposalCreator
                    Using generatedTests As EntityCollection(Of AssessmentTestResourceEntity) = proposalCreator.CreateProposalsFromDataSourceList(selectedTemplateEntity, numberOfProposals, dbResourceManager)
                        If generatedTests IsNot Nothing Then
                            If generatedTests.Count > 0 Then
                                For Each test As AssessmentTestResourceEntity In generatedTests
                                    Dim testEditorForm As New TestEditor_v2(test, True, True)
                                    AddHandler testEditorForm.FormClosed, AddressOf Editor_FormClosed
                                    testEditorForm.Show(Me)

                                    Application.DoEvents()
                                Next

                            End If
                        Else
                        End If
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, My.Resources.ErrorThrown, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Finally
                    Me.Cursor = Cursors.Default
                End Try
            End If
        Else
            MessageBox.Show(My.Resources.MainForm_TestTemplateGrid_GenerateResources_NoDataSourceSections, My.Resources.MainForm_TestTemplateGrid_GenerateResources_DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub



    Private Sub GridBase_SynchronizeItems(ByVal sender As Object, ByVal e As EventArgs) Handles ItemGrid.SynchronizeItems, TestGrid.SynchronizeItems, ItemLayoutTemplateGrid.SynchronizeItems, MediaGrid.SynchronizeItems, ControlTemplateGrid.SynchronizeItems, AspectGrid.SynchronizeItems, DataSourceGrid.SynchronizeItems, DataSourceTemplateGrid.SynchronizeItems
        FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
    End Sub

    Private Sub GridBase_PropertiesDialogRequested(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles MediaGrid.PropertiesDialogRequested, TestGrid.PropertiesDialogRequested, ItemGrid.PropertiesDialogRequested, ItemLayoutTemplateGrid.PropertiesDialogRequested, ControlTemplateGrid.PropertiesDialogRequested, TestTemplateGrid.PropertiesDialogRequested, AspectGrid.PropertiesDialogRequested, DataSourceGrid.PropertiesDialogRequested, DataSourceTemplateGrid.PropertiesDialogRequested, TestPackageGrid.PropertiesDialogRequested, CustomPropertyGrid.PropertiesDialogRequested
        If e.SelectedEntity IsNot Nothing Then
            Dim propertyEntity As IPropertyEntity
            If TypeOf e.SelectedEntity Is CustomBankPropertyResourceDto Then
                propertyEntity = BankFactory.Instance.GetCustomBankProperty(e.SelectedEntity.ResourceId)
            Else
                propertyEntity = ResourceFactory.Instance.GetResourceByIdWithOption(e.SelectedEntity.ResourceId, DetermineResourceTypeFactory(e.SelectedEntity), New ResourceRequestDTO())
            End If
            If FormHelper.OpenResourcePropertyDialog(propertyEntity, 0) Then
                FillGridOfCurrentTabPage(MainTabControl.SelectedTab, propertyEntity, propertyEntity.Id)
            End If
        End If

    End Sub

    Private Sub GridBase_RowsDeleted(ByVal sender As Object, ByVal e As RowsDeletedEventArgs) Handles ItemGrid.RowsDeleted, ControlTemplateGrid.RowsDeleted, ItemLayoutTemplateGrid.RowsDeleted, TestGrid.RowsDeleted, MediaGrid.RowsDeleted, TestTemplateGrid.RowsDeleted, AspectGrid.RowsDeleted, DataSourceGrid.RowsDeleted, DataSourceTemplateGrid.RowsDeleted, TestPackageGrid.RowsDeleted
        Dim itemData As IList(Of ResourceDto) = DirectCast(e.DataSource, IList(Of ResourceDto))
        If Not itemData.Any() Then
            Return
        End If

        Dim request = New ResourceRequestDTO With {.WithCustomProperties = True}
        Dim resourceToDelete = ResourceFactory.Instance.GetResourcesByIdsWithOption(itemData.Select(Function(i) i.ResourceId).ToList, DetermineResourceTypeFactory(itemData.First()), request)


        Dim rowsFailedToDelete As New EntityCollection
        If (e.RowsFailedToDelete IsNot Nothing) Then rowsFailedToDelete.AddRange(e.RowsFailedToDelete.OfType(Of EntityBase2))

        Dim result As String = ResourceFactory.Instance.DeleteResources(resourceToDelete, rowsFailedToDelete)
        If Not String.IsNullOrEmpty(result) Then
            MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If rowsFailedToDelete IsNot Nothing Then
            e.RowsFailedToDelete = rowsFailedToDelete.OfType(Of ResourceEntity).Select(Function(i) i.ConvertResourceEntityToResourceDto(False)).ToList
        End If

    End Sub

    Private Sub CustomPropertyGrid_RowsDeleted(ByVal sender As Object, ByVal e As RowsDeletedEventArgs) Handles CustomPropertyGrid.RowsDeleted
        Dim customPropertiesToDeletelist = BankFactory.Instance.GetCustomBankProperties(DirectCast(e.DataSource, IList(Of ResourceDto)).Select(Function(cp) cp.ResourceId).ToList)
        Dim customPropertiesToDelete As New EntityCollection
        Dim treeStructuresToRemoveCutOffScoresFromTests As New Dictionary(Of Guid, Integer)
        For Each cp In customPropertiesToDeletelist
            customPropertiesToDelete.Add(cp)
            If TypeOf (cp) Is TreeStructureCustomBankPropertyEntity Then treeStructuresToRemoveCutOffScoresFromTests.Add(cp.CustomBankPropertyId, cp.BankId)
        Next
        Dim removedTreeStructureParts As New List(Of Guid)
        removedTreeStructureParts.AddRange(BankFactory.Instance.GetTreeStructurePartCustomBankPropertiesByCustomBankPropertyIds(treeStructuresToRemoveCutOffScoresFromTests.Keys.ToList()).Select(Function(tp) CType(tp, TreeStructurePartCustomBankPropertyEntity).Code))

        Dim resultString As String = BankFactory.Instance.DeleteCustomPropertiesForced(customPropertiesToDelete)
        If String.IsNullOrEmpty(resultString) Then RemoveCutOffScoresForRemovedTreeStructureCustomProperties(treeStructuresToRemoveCutOffScoresFromTests.Values.OrderBy(Function(b) b).FirstOrDefault(), removedTreeStructureParts)
        FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
    End Sub

    Private Sub RemoveCutOffScoresForRemovedTreeStructureCustomProperties(bankId As Integer, treeStructurePartCustomProperties As List(Of Guid))
        If treeStructurePartCustomProperties IsNot Nothing AndAlso treeStructurePartCustomProperties.Count > 0 Then
            AssessmentTestCutOffScoringHelper.UpdateCutOffScoreConditionsInTests(bankId, Nothing, treeStructurePartCustomProperties)
        End If
    End Sub

    Private Sub CustomPropertyGrid_DeletingRows(ByVal sender As Object, ByVal e As DeletingRowsEventArgs) Handles CustomPropertyGrid.DeletingRows
        If Not CanRemoveProperty(e) Then
            e.Cancel = True
        End If
    End Sub

    Private Function CanRemoveProperty(ByVal e As DeletingRowsEventArgs) As Boolean
        Dim connectedToResources As New Dictionary(Of CustomBankPropertyEntity, List(Of IPropertyEntity))

        For Each selectedItem As GridEXSelectedItem In e.ToBeDeleted
            If selectedItem.RowType = RowType.Record AndAlso TypeOf selectedItem.GetRow().DataRow Is CustomBankPropertyResourceDto Then
                Dim custombankPropertyResource = DirectCast(selectedItem.GetRow().DataRow, CustomBankPropertyResourceDto)
                Dim propertyEntities As New List(Of IPropertyEntity)()
                Dim customBankPropertyEntity = BankFactory.Instance.GetCustomBankProperty(custombankPropertyResource.ResourceId)

                BankFactory.Instance.GetReferencesForCustomBankProperty(customBankPropertyEntity)

                Dim request = New ResourceRequestDTO()
                request.WithCustomProperties = True

                For Each res In ResourceFactory.Instance.GetResourcesByIdsWithOption(customBankPropertyEntity.CustomBankPropertyValueCollection.Select(Function(c) c.ResourceId).ToList(), request)
                    If res IsNot Nothing AndAlso HasValue(CType(res, ResourceEntity).CustomBankPropertyValueCollection.First(Function(i) i.CustomBankPropertyId = customBankPropertyEntity.CustomBankPropertyId)) AndAlso
                        (TypeOf res Is ItemResourceEntity OrElse TypeOf res Is GenericResourceEntity OrElse TypeOf res Is AssessmentTestResourceEntity) Then
                        propertyEntities.Add(CType(res, IPropertyEntity))
                    End If
                Next

                If propertyEntities.Count > 0 Then
                    connectedToResources.Add(customBankPropertyEntity, propertyEntities)
                End If
            End If
        Next

        If connectedToResources.Count > 0 Then
            If New RemoveCustomPropertyWithConnectedResourcesDialog(My.Resources.MainForm_CustomPropertyIsConnectedToItem, FormatMessage(connectedToResources)).ShowDialog() = DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        Else
            Dim result As DialogResult = MessageBox.Show(Me, My.Resources.MainForm_DeleteCustomProperty, String.Empty, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If result = DialogResult.No OrElse result = DialogResult.Cancel Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Private Function HasValue(customBankPropertyValueEntity As CustomBankPropertyValueEntity) As Boolean
        If (TypeOf customBankPropertyValueEntity Is FreeValueCustomBankPropertyValueEntity) Then
            Return Not String.IsNullOrEmpty(CType(customBankPropertyValueEntity, FreeValueCustomBankPropertyValueEntity).Value)
        ElseIf (TypeOf customBankPropertyValueEntity Is ListCustomBankPropertyValueEntity) Then
            Return CType(customBankPropertyValueEntity, ListCustomBankPropertyValueEntity).ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.Count > 0
        ElseIf (TypeOf customBankPropertyValueEntity Is ConceptStructureCustomBankPropertyValueEntity) Then
            Return CType(customBankPropertyValueEntity, ConceptStructureCustomBankPropertyValueEntity).ConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart.Count > 0
        ElseIf (TypeOf customBankPropertyValueEntity Is TreeStructureCustomBankPropertyValueEntity) Then
            Return CType(customBankPropertyValueEntity, TreeStructureCustomBankPropertyValueEntity).TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart.Count > 0
        ElseIf (TypeOf customBankPropertyValueEntity Is RichTextValueCustomBankPropertyValueEntity) Then
            Return Not String.IsNullOrEmpty(CType(customBankPropertyValueEntity, RichTextValueCustomBankPropertyValueEntity).Value)
        Else
            Return False
        End If
    End Function

    Private Function FormatMessage(ByVal propertyEntities As Dictionary(Of CustomBankPropertyEntity, List(Of IPropertyEntity))) As String
        Dim builder As New StringBuilder()

        For Each kvp As KeyValuePair(Of CustomBankPropertyEntity, List(Of IPropertyEntity)) In propertyEntities
            builder.Append(FormatMessage(kvp.Value))
        Next

        Return builder.ToString()
    End Function

    Private Function FormatMessage(ByVal referencedResourceEntities As List(Of IPropertyEntity)) As String
        Dim builder As New StringBuilder()
        For Each referencedResourceEntity As ResourceEntity In referencedResourceEntities
            builder.Append("- ")
            builder.Append(GetBankPathOfEntity(referencedResourceEntity.Bank.Id))
            builder.Append(" -> ")
            builder.Append(referencedResourceEntity.Name)
            builder.Append(RemoveCustomPropertyWithConnectedResourcesDialog.SplitCharacter)
        Next

        Return builder.ToString()

    End Function

    Private Sub GridBase_DeletingRows(ByVal sender As Object, ByVal e As DeletingRowsEventArgs) Handles ItemGrid.DeletingRows, TestGrid.DeletingRows, ControlTemplateGrid.DeletingRows, ItemLayoutTemplateGrid.DeletingRows, MediaGrid.DeletingRows, TestTemplateGrid.DeletingRows, AspectGrid.DeletingRows, DataSourceGrid.DeletingRows, DataSourceTemplateGrid.DeletingRows, TestPackageGrid.DeletingRows
        Dim friendlyType As String = String.Empty
        Select Case sender.GetType().FullName
            Case ItemGrid.GetType().FullName
                friendlyType = "items"
            Case TestGrid.GetType().FullName
                friendlyType = "tests"
            Case TestPackageGrid.GetType().FullName
                friendlyType = "testPackages"
            Case ControlTemplateGrid.GetType().FullName
                friendlyType = "control-templates"
            Case ItemLayoutTemplateGrid.GetType().FullName
                friendlyType = "item-layout-templates"
            Case MediaGrid.GetType().FullName
                friendlyType = "media objects"
            Case TestTemplateGrid.GetType().FullName
                friendlyType = "test-templates"
            Case AspectGrid.GetType().FullName
                friendlyType = "aspects"
            Case DataSourceGrid.GetType().FullName
                friendlyType = "datasources"
            Case DataSourceTemplateGrid.GetType().FullName
                friendlyType = "datasource-templates"
            Case Else
                Throw New NotImplementedException($"cannot find friendly name for '{sender.GetType().FullName}'")
        End Select

        If Not CanRemoveResource(e, friendlyType) Then
            e.Cancel = True
        End If

    End Sub

    Private Sub Editor_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
        If TypeOf sender Is Form Then
            RemoveHandler CType(sender, Form).FormClosed, AddressOf Editor_FormClosed

            If TypeOf sender Is AspectEditor Then
                RemoveHandler CType(sender, AspectEditor).AspectChanged, AddressOf RefreshGrid
            ElseIf TypeOf sender Is CustomPropertyEditor Then
                RemoveHandler CType(sender, CustomPropertyEditor).OnRefresh, AddressOf RefreshGrid
                RemoveHandler CType(sender, CustomPropertyEditor).OnCustomPropertyAdded, AddressOf CustomPropertyEditor_OnCustomPropertyAdded
            ElseIf TypeOf sender Is TestEditor_v2 Then
                RemoveHandler CType(sender, TestEditor_v2).PreviewTest, AddressOf Editor_Preview
            End If
        End If
    End Sub

    Private Sub Editor_UpdateGridAndSelectResource(ByVal propertyEntity As IPropertyEntity)
        If propertyEntity IsNot Nothing Then
            Dim grid As GridBase = FindGridControl(MainTabControl.SelectedTab.Controls)
            If grid IsNot Nothing AndAlso TypeOf grid Is UI.ItemGrid Then
                Dim request = New ItemResourceRequestDTO() With {.WithCustomProperties = True, .WithDependencies = True}
                Dim items = DtoFactory.Item.GetItemsByCode(New List(Of String) From {DirectCast(propertyEntity, ItemResourceEntity).Name}, MainBankBrowser.SelectedBank.Id, request)
                If items IsNot Nothing AndAlso items.Count() > 0 Then
                    Debug.Assert(items.Count() = 1, "Not expected. Expected only 1 item.")
                    Dim item = items(0)
                    Dim culture = Thread.CurrentThread.CurrentUICulture
                    If culture Is Nothing Then culture = CultureInfo.DefaultThreadCurrentUICulture
                    Dim enumValue = DirectCast([Enum].Parse(GetType(ItemTypeEnum), item.ItemTypeFromItemLayoutTemplate), ItemTypeEnum)
                    item.ItemTypeFromItemLayoutTemplateString = ResourceEnumConverter.ConvertToString(enumValue, culture)

                    UpdateItemInGrid(grid, item)
                End If
            Else
                Editor_RefreshGridAndSelectResource(propertyEntity)
            End If
        End If
    End Sub

    Private Sub UpdateItemInGrid(grid As GridBase, item As ItemResourceDto)
        If (grid IsNot Nothing AndAlso grid.DataSource IsNot Nothing AndAlso TypeOf grid.DataSource Is IEnumerable(Of ResourceDto)) Then
            Dim itemDataSource = DirectCast(grid.DataSource, IEnumerable(Of ResourceDto)).FirstOrDefault(Function(i) i.ResourceId = item.ResourceId)
            If (itemDataSource IsNot Nothing) Then
                itemDataSource.CustomPropertyDisplayValues = DtoFactory.Item.Get(item.BankId, item.Name).CustomPropertyDisplayValues
            End If
        End If
        UpdateRowInList(item, grid)
    End Sub

    Private Sub Editor_RefreshGridAndSelectResource(ByVal propertyEntity As IPropertyEntity)
        FillGridOfCurrentTabPage(MainTabControl.SelectedTab, propertyEntity)
    End Sub

    Private Sub UpdateRowInList(item As ItemResourceDto, grid As GridBase)
        Dim row = FindRow(grid, item.ResourceId)

        If row IsNot Nothing Then
            row.BeginEdit()
            For Each cell As GridEXCell In row.Cells
                If cell.Column.BoundMode = ColumnBoundMode.Bound Then
                    cell.Value = ReflectionHelper.GetPropertyValueString(item, cell.Column.DataMember)
                    If cell.Value Is Nothing Then cell.Value = ReflectionHelper.GetPropertyValueDecimal(item, cell.Column.DataMember)
                ElseIf cell.Column.BoundMode = ColumnBoundMode.UnboundFetch Then
                    grid.AddCustomPropertyValues(item, cell)
                End If
            Next
            row.EndEdit()
        End If
    End Sub

    Private Function FindRow(grid As GridBase, id As Guid) As GridEXRow
        Dim row As GridEXRow
        FindRowById(grid.GridControl.SelectedItems.OfType(Of GridEXSelectedItem).Where(Function(r As GridEXSelectedItem) r.RowType = RowType.Record).Select(Function(r As GridEXSelectedItem) r.GetRow).ToArray, id, row)
        If row Is Nothing Then
            FindRowById(grid.GridControl.GetRows.ToArray, id, row)
        End If
        Return row
    End Function

    Private Sub FindRowById(rows As GridEXRow(), resourceId As Guid, ByRef foundRow As GridEXRow)
        If foundRow IsNot Nothing Then Return
        For Each row In rows
            If IsRowWithThisResourceId(row, resourceId) Then
                foundRow = row
                Return
            End If
            Dim childs = row.GetChildRows
            If childs IsNot Nothing Then
                For Each child In childs
                    FindRowById(childs, resourceId, foundRow)
                Next
            End If
        Next
    End Sub

    Private Function IsRowWithThisResourceId(row As GridEXRow, id As Guid) As Boolean
        Return row.RowType = RowType.Record AndAlso row.DataRow IsNot Nothing AndAlso TypeOf row.DataRow Is ResourceDto AndAlso
                    DirectCast(row.DataRow, ResourceDto).ResourceId = id
    End Function

    Private Sub RefreshGrid(ByVal sender As Object, ByVal e As Questify.Builder.UI.RefreshEventArgs)
        If MainTabControl.SelectedTab.Tag.ToString.Equals("Aspects") AndAlso TypeOf sender Is AspectEditor _
 OrElse Not TypeOf sender Is AspectEditor Then
            FillGridOfCurrentTabPage(MainTabControl.SelectedTab, CType(e.SelectedResource, IPropertyEntity))
        End If
    End Sub

    Private Sub Editor_Preview(ByVal sender As Object, ByVal e As EntityActionEventArgs)
        _actionCommands.CurrentActionCommands.Preview(e.SelectedEntity)
    End Sub



    Private Sub MediaGrid_AddNewItem(ByVal sender As Object, ByVal e As EventArgs) Handles MediaGrid.AddNewItem
        Try
            Using newMediaWizard As New AddGenericResourceWizardForm(MainBankBrowser.SelectedBank.Id)
                If newMediaWizard.ShowDialog(Me) = DialogResult.OK Then
                    If newMediaWizard.AddedGenericResources.Count > 0 Then
                        FillGridOfCurrentTabPage(MainTabControl.SelectedTab, selection:=CType(newMediaWizard.AddedGenericResources(0), IPropertyEntity))
                    Else
                        FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
                        If Not newMediaWizard.AddedTemplateResource Is Nothing Then
                            Dim newGenericResourceEntity = New GenericResourceDto With {.ResourceId = newMediaWizard.AddedTemplateResource.ResourceId,
                                                            .BankId = newMediaWizard.AddedTemplateResource.BankId,
                                                            .Name = newMediaWizard.AddedTemplateResource.Name,
                                                           .MediaType = newMediaWizard.AddedTemplateResource.MediaType,
                                                            .IsTemplate = newMediaWizard.AddedTemplateResource.IsTemplate}
                            OpenMediaItem(newGenericResourceEntity)
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            ShowWizardExceptionHandler(Me, GetType(AddGenericResourceWizardForm), ex, MainBankBrowser.SelectedBank.Id)
        End Try
    End Sub

    Private Function ResourceEntityExists(ByVal resourceEntity As ResourceDto, taskType As TaskType) As Boolean
        If resourceEntity Is Nothing Then
            Throw New ArgumentNullException("resourceEntity")
        End If
        If ResourceFactory.Instance.ResourceExists(resourceEntity.BankId, resourceEntity.Name, False, DetermineResourceTypeFactory(resourceEntity)) Then
            Return True
        ElseIf MessageBox.Show(String.Format(My.Resources.MainForm_EntityDoesNotExist, resourceEntity.Name, "Bank"), My.Resources.MainForm_EntityDoesNotExistTitle, MessageBoxButtons.YesNo) = DialogResult.Yes Then
            BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(taskType, resourceEntity.BankId), "FillGrid")
            Return False
        End If
    End Function

    Private Sub MediaGrid_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles MediaGrid.EntityDblClick
        If e.SelectedEntity IsNot Nothing Then
            Dim resourceEntity As GenericResourceDto = DirectCast(e.SelectedEntity, GenericResourceDto)
            OpenMediaItem(resourceEntity)
        End If
    End Sub

    Private Sub OpenMediaItem(resourceEntity As GenericResourceDto)
        If ResourceEntityExists(resourceEntity, TaskType.GetMediaForBank) Then
            Dim openSourceTextEditor As Boolean = ((resourceEntity.MediaType = "application/xhtml+xml" OrElse resourceEntity.MediaType = "text/plain") AndAlso
                           (resourceEntity.IsTemplate OrElse (ModifierKeys <> Keys.Alt))
                          )

            If openSourceTextEditor Then
                _windowFacade.OpenSourceTextEditorById(resourceEntity.ResourceId)
            Else
                If resourceEntity Is Nothing Then

                End If
                Dim editForm As New GenericResourceEditor(resourceEntity.ResourceId)
                AddHandler editForm.FormClosed, AddressOf MediaEditor_FormClosed
                AddHandler editForm.MetaData.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                          OpenResourcePropertyDialog(resourceEntity, e1.Value)
                                                                                      End Sub

                editForm.Show(Me)
            End If
        End If
    End Sub

    Friend Sub MediaEditor_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim gre As GenericResourceEditor = DirectCast(sender, GenericResourceEditor)

        If gre.UserSavedTheResource Then
            BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetMediaForBank, MainBankBrowser.SelectedBank), "FillGrid")
        End If
    End Sub



    Private Sub ItemLayoutTemplateGrid_ToggleVisibilityClick(ByVal sender As Object, ByVal e As EventArgs) Handles ItemLayoutTemplateGrid.TogglingResourceVisibility
        Dim ctl As Control = MainTabControl.SelectedTab.Controls(0)

        If TypeOf ctl Is GridBase Then
            Dim grid As GridBase = DirectCast(ctl, GridBase)

            If grid.SelectedEntities.Count > 0 Then
                ToggleVisibilityOfSelectedResources(grid.SelectedEntities)
            Else
                MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
            End If
        Else
            MessageBox.Show(My.Resources.MainForm_NoResourcesSelected)
        End If
    End Sub

    Private Sub ToggleVisibilityOfSelectedResources(selectedEntities As IList(Of ResourceDto))
        Dim currentBankId As Integer = MainBankBrowser.SelectedBank.Id


        For Each entity As ItemLayoutTemplateResourceDto In selectedEntities
            Dim makeIltVisible As Nullable(Of Boolean)

            If entity.VisibleInPicker Then
                makeIltVisible = False
            Else
                If entity.SetToInvisibleAtBankIds.Any(Function(x) x.Equals(currentBankId)) Then
                    makeIltVisible = True
                End If
            End If

            If makeIltVisible.HasValue Then
                ResourceFactory.Instance.UpdateResourceVisibility(entity.ResourceId, currentBankId, makeIltVisible.Value)
            End If
        Next

        ClearCacheForBankPath()
        FillGridOfCurrentTabPage(MainTabControl.SelectedTab)
    End Sub

    Private Sub ClearCacheForBankPath()
        Dim bankIds As New List(Of Integer)
        Dim bank = MainBankBrowser.SelectedBank

        bankIds.Add(bank.Id)

        If _cachedBankStructure IsNot Nothing Then
            While bank.ParentBankId.HasValue
                bank = _cachedBankStructure.FirstOrDefault(Function(b) b.Id = bank.ParentBankId.Value)
                If bank IsNot Nothing Then
                    bankIds.Add(bank.Id)
                End If
            End While
        End If

        For Each id As Integer In bankIds
            InvalidateCacheHelper.ClearCacheForBank(id)
        Next
    End Sub




    Private Sub TestPackageGrid_AddNewTestPackage(ByVal sender As Object, ByVal e As EventArgs) Handles TestPackageGrid.AddNewTestPackage, TestPackageGrid.AddNewItem
        CreateAndOpenTestPackage()
    End Sub

    Private Sub CreateAndOpenTestPackage()
        Dim newTestPackageEntity As New TestPackageResourceEntity(Guid.NewGuid())
        newTestPackageEntity.BankId = MainBankBrowser.SelectedBank.Id
        newTestPackageEntity.Name = My.Resources.NewTestPackage
        newTestPackageEntity.Version = "0.1"
        newTestPackageEntity.ResourceData = New ResourceDataEntity()

        Dim testPackageEditorForm As New TestPackageEditor(newTestPackageEntity)
        AddHandler testPackageEditorForm.FormClosed, AddressOf Editor_FormClosed
        testPackageEditorForm.Show(Me)
    End Sub

    Private Sub TestPackageGrid_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles TestPackageGrid.EntityDblClick
        Dim testPackageEditorForm As Form = Nothing

        If e.SelectedEntity IsNot Nothing Then
            Dim resourceEntity = DirectCast(e.SelectedEntity, TestPackageResourceDto)

            If ResourceEntityExists(resourceEntity, TaskType.GetTestPackageForBank) Then
                Dim forms As IEnumerable(Of Form) = Me.GetOwnedFormsOfType(GetType(TestPackageEditor))

                For Each form As TestPackageEditor In forms
                    If form.TestPackageResourceEntity.ResourceId = resourceEntity.ResourceId Then
                        testPackageEditorForm = form
                    End If
                Next

                If testPackageEditorForm Is Nothing Then
                    Dim testPackageResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(resourceEntity.ResourceId, DetermineResourceTypeFactory(resourceEntity), New ResourceRequestDTO()), TestPackageResourceEntity)
                    testPackageEditorForm = New TestPackageEditor(testPackageResourceEntity)
                    AddHandler testPackageEditorForm.FormClosed, AddressOf Editor_FormClosed
                    AddHandler DirectCast(testPackageEditorForm, TestPackageEditor).MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                                                                 FormHelper.OpenResourcePropertyDialog(testPackageResourceEntity, e1.Value)
                                                                                                                                             End Sub
                    testPackageEditorForm.Show(Me)
                Else
                    Dim handle = testPackageEditorForm.Handle
                    ShowWindow(handle, ShowWindowCommands.Restore)
                End If
            End If
        End If
    End Sub


    Private Sub TestGrid_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles TestGrid.EntityDblClick
        Dim testEditorForm As Form = Nothing

        If e.SelectedEntity IsNot Nothing Then
            Dim resource = DirectCast(e.SelectedEntity, AssessmentTestResourceDto)
            Dim assessmentTestResourceEntity = CType(ResourceFactory.Instance.GetResourceByIdWithOption(resource.ResourceId, DetermineResourceTypeFactory(resource), New ResourceRequestDTO()), AssessmentTestResourceEntity)

            If ResourceEntityExists(resource, TaskType.GetTestsForBank) AndAlso assessmentTestResourceEntity IsNot Nothing Then
                Dim forms As IEnumerable(Of Form) = Me.GetOwnedFormsOfType(GetType(TestEditor_v2))

                For Each form As TestEditor_v2 In forms
                    If form.TestResourceEntity.ResourceId = assessmentTestResourceEntity.ResourceId Then
                        testEditorForm = form
                    End If
                Next

                If testEditorForm Is Nothing Then
                    testEditorForm = New TestEditor_v2(assessmentTestResourceEntity, True, True)
                    AddHandler testEditorForm.FormClosed, AddressOf Editor_FormClosed
                    AddHandler DirectCast(testEditorForm, TestEditor_v2).PreviewTest, AddressOf Editor_Preview
                    AddHandler DirectCast(testEditorForm, TestEditor_v2).MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                                                      FormHelper.OpenResourcePropertyDialog(assessmentTestResourceEntity, e1.Value)
                                                                                                                                  End Sub
                    testEditorForm.Show(Me)
                Else
                    Dim handle = testEditorForm.Handle
                    ShowWindow(handle, ShowWindowCommands.Restore)
                End If
            End If
        End If
    End Sub

    Private Sub TestGrid_AddNewItem(ByVal sender As Object, ByVal e As EventArgs) Handles TestGrid.AddNewItem
        Dim dialog As New SelectTestTemplateResourceDialog(MainBankBrowser.SelectedBank.Id)
        dialog.ShowDialog()
        If dialog.DialogResult = DialogResult.OK Then
            If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.ResourceId) Then
                Dim request = New ResourceRequestDTO With {.WithDependencies = True}
                Dim templateTestEntity As AssessmentTestResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(dialog.SelectedEntity.ResourceId, DetermineResourceTypeFactory(dialog.SelectedEntity), request), AssessmentTestResourceEntity)
                If templateTestEntity.IsTemplate Then
                    Using dbResourceManager As New DataBaseResourceManager(MainBankBrowser.SelectedBank.Id)
                        Dim sectionsWithDataSources As Dictionary(Of TestSection2, DataSourceSettings) = templateTestEntity.GetAssessmentTest.GetDataSourceSettingsForSectionsInTest(dbResourceManager.BankId)
                        If sectionsWithDataSources.Count > 0 Then
                            CreateProposalFromTemplate(templateTestEntity, sectionsWithDataSources, dbResourceManager)
                        Else
                            CreateAndOpenTestBasedOnTemplate(templateTestEntity)
                        End If
                    End Using

                Else
                    Throw New LogicFatalException("It's only allowed to create a test proposal of a test template. The supplied test isn't a template.")
                End If
            Else
                MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

    End Sub

    Private Sub CreateAndOpenTestBasedOnTemplate(ByVal templateTestEntity As AssessmentTestResourceEntity)

        Dim newTestEntity As New AssessmentTestResourceEntity(Guid.NewGuid())
        newTestEntity.BankId = MainBankBrowser.SelectedBank.Id
        newTestEntity.Name = My.Resources.NewTestDefaultName
        newTestEntity.Version = "0.1"
        newTestEntity.ResourceData = New ResourceDataEntity()
        newTestEntity.IsTemplate = False

        Dim newAssessmentTest = templateTestEntity.GetAssessmentTest
        newAssessmentTest.Identifier = My.Resources.NewTestDefaultName
        newAssessmentTest.Title = String.Empty

        newTestEntity.SetAssessmentTest(SerializeHelper.XmlSerializableClone(newAssessmentTest))
        For Each depResource As DependentResourceEntity In templateTestEntity.DependentResourceCollection
            DependencyManagement.AddDependentResourceToResource(newTestEntity, depResource.DependentResource)
        Next

        Dim testEditorForm As New TestEditor_v2(newTestEntity, False, True)
        AddHandler testEditorForm.FormClosed, AddressOf Editor_FormClosed
        AddHandler testEditorForm.PreviewTest, AddressOf Editor_Preview
        AddHandler testEditorForm.MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub()
                                                                                               FormHelper.OpenResourcePropertyDialog(newTestEntity)
                                                                                           End Sub

        testEditorForm.Show(Me)
    End Sub

    Private Function OpenResourcePropertyDialog(ByVal resource As ResourceDto, Optional ByVal defaultTab As Integer = 0) As Boolean
        Dim returnValue = False
        Dim llblGenResourceEntity = ResourceFactory.Instance.GetResourceByIdWithOption(resource.ResourceId, DetermineResourceTypeFactory(resource), New ResourceRequestDTO())
        If llblGenResourceEntity Is Nothing Then
            Return returnValue
        Else
            returnValue = _windowFacade.OpenResourcePropertyDialog(llblGenResourceEntity.ResourceId, llblGenResourceEntity.GetType(), defaultTab)
        End If
        Return returnValue
    End Function

    Private Sub TestGrid_PublishEntity(ByVal sender As Object, ByVal e As EventArgs) Handles TestGrid.PublishEntity
        If TestGrid.SelectedEntities IsNot Nothing Then
            Try
                Dim publishDialog As New PublicationFormWizard(Me.MainBankBrowser.SelectedBank.Id,
                                                               TestGrid.SelectedEntities,
                                                               MainTabControl.SelectedTab.Text,
                                                               GetFullBankPath())
                publishDialog.ShowDialog(Me)
            Catch ex As Exception
                ShowWizardExceptionHandler(Me, GetType(PublicationFormWizard), ex, MainBankBrowser.SelectedBank.Id)
            End Try
        Else
            MessageBox.Show(My.Resources.MainForm_SelectTestToPublish, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub TestPackageGrid_PublishEntity(ByVal sender As Object, ByVal e As EventArgs) Handles TestPackageGrid.PublishEntity
        If TestPackageGrid.SelectedEntities IsNot Nothing Then
            Try
                Dim publishDialog As New PublicationFormWizard(Me.MainBankBrowser.SelectedBank.Id,
                                                               TestPackageGrid.SelectedEntities,
                                                               MainTabControl.SelectedTab.Text,
                                                               GetFullBankPath())
                publishDialog.ShowDialog(Me)
            Catch ex As Exception
                ShowWizardExceptionHandler(Me, GetType(PublicationFormWizard), ex, MainBankBrowser.SelectedBank.Id)
            End Try
        Else
            MessageBox.Show(My.Resources.MainForm_SelectTestToPublish, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub TestGrid_PreviewEntity(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles TestGrid.PreviewEntity
        Dim testResourceEntity As AssessmentTestResourceEntity = Nothing
        If TestGrid.SelectedEntity IsNot Nothing Then
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim test As AssessmentTestResourceDto
            If (e Is Nothing) Then
                test = DirectCast(TestGrid.SelectedEntity, AssessmentTestResourceDto)
            Else
                test = DirectCast(e.SelectedEntity, AssessmentTestResourceDto)
            End If
            If Not TestsContainItems(New AssessmentTestResourceDto() {test}.ToList) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            testResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(test.ResourceId, DetermineResourceTypeFactory(test), New ResourceRequestDTO()), AssessmentTestResourceEntity)

            If testResourceEntity.ResourceData Is Nothing Then
                Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testResourceEntity)
                testResourceEntity.ResourceData = data
            End If

            Dim factoryResult As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(testResourceEntity.ResourceData.BinData, True)
            Dim selectedTestPreviewHandler As TestPreviewHandlerIdentifier

            Using publicationClient = New PublicationServiceClient()
                Dim testPreviewers = publicationClient.GetAvailableTestPreviewHandlers(testResourceEntity.BankId, New String() {testResourceEntity.Name}, Nothing)
                If (testPreviewers.Count > 1) Then
                    Using selectTestPreviewDialog As New SelectTestPreviewDialog(factoryResult, testPreviewers)
                        If selectTestPreviewDialog.ShowDialog(Me) = DialogResult.OK Then
                            selectedTestPreviewHandler = selectTestPreviewDialog.SelectedTestPreviewHandler
                        Else
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End Using
                ElseIf (testPreviewers.Count = 1) Then
                    selectedTestPreviewHandler = testPreviewers.First()
                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show(My.Resources.MainForm_TestCannotBePreviewed, My.Resources.MainForm_TestCannotBePreviewed_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using
            Dim testPreviewLauncher As TestPreviewLauncher = New TestPreviewLauncher(testResourceEntity.BankId, testResourceEntity.Name, selectedTestPreviewHandler, False)
            FormHelper.ShowPreviewTestProgressDialog(testPreviewLauncher, Me.Cursor)
        End If
    End Sub

    Private Sub TestPackageGrid_PreviewEntity(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles TestPackageGrid.PreviewEntity
        If TestPackageGrid.SelectedEntity IsNot Nothing Then
            Me.Cursor = Cursors.WaitCursor
            Dim testPackageResourceEntity As TestPackageResourceEntity = Nothing
            If (e Is Nothing) Then
                testPackageResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(TestPackageGrid.SelectedEntity.ResourceId, DetermineResourceTypeFactory(TestPackageGrid.SelectedEntity), New ResourceRequestDTO()), TestPackageResourceEntity)
            Else
                testPackageResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(e.SelectedEntity.ResourceId, DetermineResourceTypeFactory(e.SelectedEntity), New ResourceRequestDTO()), TestPackageResourceEntity)
            End If

            If testPackageResourceEntity.ResourceData Is Nothing Then
                Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testPackageResourceEntity)
                testPackageResourceEntity.ResourceData = data
            End If

            Dim factoryResult As TestPackage = TestPackageFactory.ReturnTestPackageModelFromByteArray(testPackageResourceEntity.ResourceData.BinData)
            Dim selectedTestPreviewHandler As TestPreviewHandlerIdentifier
            Dim testId As String = String.Empty
            Dim testRefCollection As ReadOnlyCollection(Of TestReference) = factoryResult.GetAllTestReferencesInTestPackage()
            Dim testNames = testRefCollection.Select(Function(t) t.SourceName)

            Using publicationClient = New PublicationServiceClient()
                Dim testPreviewers As TestPreviewHandlerIdentifier() = publicationClient.GetAvailableTestPreviewHandlers(testPackageResourceEntity.BankId, testNames.ToArray, New String() {testPackageResourceEntity.Name})
                If testRefCollection.Count = 0 Then
                    MessageBox.Show(My.Resources.MainForm_TestPackageCannotBePreviewed, My.Resources.MainForm_TestCannotBePreviewed_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                If testPreviewers.Count = 0 Then
                    MessageBox.Show(My.Resources.MainForm_NoPreviewerAvailable, My.Resources.MainForm_TestCannotBePreviewed_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                ElseIf (testPreviewers.Count > 1) OrElse (testRefCollection.Count > 1) Then
                    Using selectTestPreviewDialog As New SelectTestPreviewDialog(factoryResult, testPackageResourceEntity.BankId, testPreviewers)
                        If selectTestPreviewDialog.ShowDialog(Me) = DialogResult.OK Then
                            selectedTestPreviewHandler = selectTestPreviewDialog.SelectedTestPreviewHandler
                            testId = selectTestPreviewDialog.SelectedTestId
                        Else
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End Using
                Else
                    selectedTestPreviewHandler = testPreviewers.First()
                    testId = testRefCollection.First.Title
                End If
            End Using
            Dim testPreviewLauncher As TestPreviewLauncher = New TestPreviewLauncher(testPackageResourceEntity.BankId, testId, selectedTestPreviewHandler, True)
            FormHelper.ShowPreviewTestProgressDialog(testPreviewLauncher, Me.Cursor)
        End If
    End Sub



    Private Sub ItemGrid_AddNewItem(ByVal sender As Object, ByVal e As EventArgs) Handles ItemGrid.AddNewItem
        If _windowFacade.ItemsToBeOpen.Count = FormHelper.MAX_OPEN_ITEMS Then
            MessageBox.Show(String.Format(My.Resources.MainForm_MaxOpenItemsReachedNewItem, FormHelper.MAX_OPEN_ITEMS), My.Resources.MainForm_MaxOpenItemsReachedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim dialogSelectItemLayoutTemplate As New SelectItemLayoutTemplateResourceDialog(MainBankBrowser.SelectedBank.Id, New List(Of ItemTypeEnum)(New ItemTypeEnum() {ItemTypeEnum.Error, ItemTypeEnum.Inline}), True)
            Select Case dialogSelectItemLayoutTemplate.ShowDialog(Me)
                Case DialogResult.OK
                    If dialogSelectItemLayoutTemplate.SelectedEntity IsNot Nothing Then
                        If Not dialogSelectItemLayoutTemplate.EntitiesProhibitedToSelect.Contains(dialogSelectItemLayoutTemplate.SelectedEntity.ResourceId) Then
                            Dim layoutId = dialogSelectItemLayoutTemplate.SelectedEntity.ResourceId
                            Dim bankId = MainBankBrowser.SelectedBank.Id

                            _windowFacade.CreateNewItem(layoutId, bankId, True, True, New Guid())
                        Else
                            MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If
                Case DialogResult.Cancel
            End Select
        End If
    End Sub

    Private Sub OpenItemFromStartPage(ByVal sender As Object, ByVal e As EntityActionEventArgs)
        FormHelper.OpenItem(DirectCast(e.SelectedEntity, ItemResourceDto), CanMoveBack, CanMoveNext)
    End Sub

    Private Sub ItemGrid_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles ItemGrid.EntityDblClick
        FormHelper.OpenItem(DirectCast(e.SelectedEntity, ItemResourceDto), CanMoveBack, CanMoveNext)
    End Sub

    Private Sub ItemGrid_EditItemInSecondWindow(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles ItemGrid.EditItemInSecondWindow
        If _windowFacade.ItemsToBeOpen.Count = 0 Then
            MessageBox.Show(My.Resources.MainForm_NoOpenIItems, My.Resources.MainForm_NoOpenIItemsTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf _windowFacade.ItemsToBeOpen.Count = FormHelper.MAX_OPEN_ITEMS Then
            MessageBox.Show(String.Format(My.Resources.MainForm_MaxOpenItemsReached, FormHelper.MAX_OPEN_ITEMS), My.Resources.MainForm_MaxOpenItemsReachedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            OpenSecondItem(DirectCast(e.SelectedEntity, ItemResourceDto))
        End If
    End Sub

    Private Sub OpenSecondItem(ByVal entityToOpen As ItemResourceDto)
        If entityToOpen IsNot Nothing Then
            If ResourceEntityExists(entityToOpen, TaskType.GetItemsForBank) And FormHelper.AddToOpenItems(entityToOpen.ResourceId) Then
                _windowFacade.OpenSecondItemEditorById(entityToOpen.ResourceId, CanMoveBack, CanMoveNext)
            End If
        End If
    End Sub

    Private Sub ItemGrid_SearchBarVisibilityToggled(ByVal sender As Object, ByVal e As EventArgs) Handles ItemGrid.SearchBarVisibilityToggled
        SearchToolStripButton.Checked = ItemGrid.SearchToolbarVisibility
        SearchToolStripMenuItem.Checked = ItemGrid.SearchToolbarVisibility
    End Sub

    Private ReadOnly Property CanMoveNext As Boolean
        Get
            Return ItemsTabIsSelected() AndAlso (ItemGrid.GridControl.Row + 1 < ItemGrid.GridControl.RowCount)
        End Get
    End Property

    Private ReadOnly Property CanMoveBack As Boolean
        Get
            Return ItemsTabIsSelected() AndAlso (ItemGrid.GridControl.Row > 1)
        End Get
    End Property

    Private Function ItemsTabIsSelected() As Boolean
        Return MainTabControl.SelectedTab.Tag.ToString.StartsWith("Items")
    End Function


    Private Sub ItmEdtr_Move(currentResourceId As Guid, moveToNext As Boolean)
        If CanMoveNext AndAlso moveToNext Then
            ItemGrid.GridControl.MoveNext()
        ElseIf CanMoveBack AndAlso Not moveToNext Then
            ItemGrid.GridControl.MovePrevious()
        End If

        If FormHelper.AddToOpenItems(ItemGrid.SelectedEntity.ResourceId) Then
            _windowFacade.ItemsToBeOpen.Remove(currentResourceId)
            _windowFacade.OpenItemEditorById(ItemGrid.SelectedEntity.ResourceId, CanMoveBack, CanMoveNext, currentResourceId)
        End If
    End Sub




    Private Sub AspectGrid_AddNewItem(ByVal sender As Object, ByVal e As EventArgs) Handles AspectGrid.AddNewItem

        Dim resource As New AspectResourceEntity(Guid.NewGuid())
        resource.BankId = MainBankBrowser.SelectedBank.Id
        resource.ResourceData = New ResourceDataEntity()

        Dim aspectEditorForm As New AspectEditor(resource)
        AddHandler aspectEditorForm.AspectChanged, AddressOf RefreshGrid
        AddHandler aspectEditorForm.AspectResourceMetaData.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                        FormHelper.OpenResourcePropertyDialog(resource, e1.Value)
                                                                                                    End Sub
        aspectEditorForm.Show(Me)
    End Sub

    Private Sub AspectGrid_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles AspectGrid.EntityDblClick
        Dim aspectEditorForm As AspectEditor = Nothing

        If e.SelectedEntity IsNot Nothing Then
            Dim resourceEntity = DirectCast(e.SelectedEntity, AspectResourceDto)

            If ResourceEntityExists(resourceEntity, TaskType.GetAspectsForBank) Then
                Dim forms As IEnumerable(Of Form) = Me.GetOwnedFormsOfType(GetType(AspectEditor))

                For Each form As AspectEditor In forms
                    If form.Resource.ResourceId = resourceEntity.ResourceId Then
                        aspectEditorForm = form
                    End If
                Next

                If aspectEditorForm Is Nothing Then
                    Dim llblGenAspectResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(resourceEntity.ResourceId, DetermineResourceTypeFactory(resourceEntity), New ResourceRequestDTO()), AspectResourceEntity)
                    aspectEditorForm = New AspectEditor(llblGenAspectResourceEntity)
                    AddHandler aspectEditorForm.AspectChanged, AddressOf RefreshGrid
                    AddHandler aspectEditorForm.AspectResourceMetaData.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                                    OpenResourcePropertyDialog(resourceEntity, e1.Value)
                                                                                                                End Sub

                    aspectEditorForm.Show(Me)
                Else
                    aspectEditorForm.Focus()
                End If
            End If
        End If
    End Sub



    Private Sub TestTemplateGrid_AddNewItem(ByVal sender As Object, ByVal e As EventArgs) Handles TestTemplateGrid.AddNewItem
        Dim newTestEntity As New AssessmentTestResourceEntity(Guid.NewGuid())
        newTestEntity.BankId = MainBankBrowser.SelectedBank.Id
        newTestEntity.Name = My.Resources.NewTestTemplateDefaultName
        newTestEntity.Title = My.Resources.NewTestTemplateDefaultName
        newTestEntity.Version = "0.1"
        newTestEntity.ResourceData = New ResourceDataEntity()
        newTestEntity.IsTemplate = True

        Dim testEditorForm As New TestEditor_v2(newTestEntity, False, False)
        AddHandler testEditorForm.FormClosed, AddressOf Editor_FormClosed
        AddHandler testEditorForm.MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                               FormHelper.OpenResourcePropertyDialog(newTestEntity, e1.Value)
                                                                                           End Sub

        testEditorForm.Show(Me)
    End Sub

    Private Sub TestTemplateGrid_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles TestTemplateGrid.EntityDblClick
        Dim testEditorForm As Form = Nothing

        If e.SelectedEntity IsNot Nothing Then
            Dim resourceEntity As AssessmentTestResourceDto = DirectCast(e.SelectedEntity, AssessmentTestResourceDto)

            If ResourceEntityExists(resourceEntity, TaskType.GetTestTemplatesForBank) Then
                Dim forms As IEnumerable(Of Form) = Me.GetOwnedFormsOfType(GetType(TestEditor_v2))

                For Each form As TestEditor_v2 In forms
                    If form.TestResourceEntity.ResourceId = resourceEntity.ResourceId Then
                        testEditorForm = form
                    End If
                Next

                If testEditorForm Is Nothing Then
                    Dim request = New ResourceRequestDTO With {.WithDependencies = True}
                    Dim assessmentTestResource = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(resourceEntity.ResourceId, DetermineResourceTypeFactory(resourceEntity), request), AssessmentTestResourceEntity)
                    testEditorForm = New TestEditor_v2(assessmentTestResource, False, False)
                    AddHandler testEditorForm.FormClosed, AddressOf Editor_FormClosed
                    AddHandler DirectCast(testEditorForm, TestEditor_v2).MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                                                      OpenResourcePropertyDialog(resourceEntity, e1.Value)
                                                                                                                                  End Sub
                    testEditorForm.Show(Me)
                Else
                    testEditorForm.Focus()
                End If
            End If
        End If
    End Sub



    Private Sub DataSourceGrid_AddNewItem(ByVal sender As Object, ByVal e As EventArgs) Handles DataSourceGrid.AddNewItem
        Dim dialog As New SelectDataSourceResourceDialog(MainBankBrowser.SelectedBank.Id, True, Nothing)
        dialog.StartPosition = FormStartPosition.CenterParent
        dialog.ShowDialog()

        If dialog.DialogResult = DialogResult.OK Then
            Dim newDataSourceEntity As New DataSourceResourceEntity(Guid.NewGuid())

            With newDataSourceEntity
                .BankId = MainBankBrowser.SelectedBank.Id
                .Name = My.Resources.newString + dialog.SelectedEntity.Name.ToLowerInvariant()
                .Title = My.Resources.newString + dialog.SelectedEntity.Name.ToLowerInvariant()
                .Version = "0.1"
                .DataSourceType = dialog.SelectedEntity.DataSourceType
                .ResourceData = New ResourceDataEntity()
                .IsTemplate = False

                .ResourceData.BinData = DirectCast(ResourceFactory.Instance.GetResourceDataByResourceId(dialog.SelectedEntity.ResourceId).BinData.Clone(), Byte())

                Dim request = New ResourceRequestDTO With {.WithDependencies = True}
                Dim fullTemplateEntity As DataSourceResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(dialog.SelectedEntity.ResourceId, DetermineResourceTypeFactory(dialog.SelectedEntity), request), DataSourceResourceEntity)

                For Each depResource As DependentResourceEntity In fullTemplateEntity.DependentResourceCollection
                    Dim clonedDepResource As DependentResourceEntity = .DependentResourceCollection.AddNew()
                    clonedDepResource.DependentResource = depResource.DependentResource
                Next
            End With

            Dim dataSourceEditorForm As New DataSourceEditor(newDataSourceEntity)
            AddHandler dataSourceEditorForm.FormClosed, AddressOf Editor_FormClosed
            AddHandler dataSourceEditorForm.MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                         FormHelper.OpenResourcePropertyDialog(newDataSourceEntity, e1.Value)
                                                                                                     End Sub
            dataSourceEditorForm.Show(Me)
        End If
    End Sub

    Private Sub DataSourceGrid_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles DataSourceGrid.EntityDblClick
        Dim dataSourceEditorForm As Form = Nothing

        If e.SelectedEntity IsNot Nothing Then
            Dim resourceEntity As DataSourceResourceDto = DirectCast(e.SelectedEntity, DataSourceResourceDto)

            If ResourceEntityExists(resourceEntity, TaskType.GetDataSourcesForBank) Then
                Dim forms As IEnumerable(Of Form) = Me.GetOwnedFormsOfType(GetType(DataSourceEditor))

                For Each form As DataSourceEditor In forms
                    If form.DataSourceResourceEntity.ResourceId = resourceEntity.ResourceId Then
                        dataSourceEditorForm = form
                    End If
                Next

                If dataSourceEditorForm Is Nothing Then
                    Dim llblGenDatasourceResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(resourceEntity.ResourceId, DetermineResourceTypeFactory(resourceEntity), New ResourceRequestDTO()), DataSourceResourceEntity)
                    dataSourceEditorForm = New DataSourceEditor(llblGenDatasourceResourceEntity)
                    AddHandler dataSourceEditorForm.FormClosed, AddressOf Editor_FormClosed
                    AddHandler DirectCast(dataSourceEditorForm, DataSourceEditor).MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                                                               OpenResourcePropertyDialog(resourceEntity, e1.Value)
                                                                                                                                           End Sub
                    dataSourceEditorForm.Show(Me)
                Else
                    dataSourceEditorForm.Focus()
                End If
            End If
        End If
    End Sub



    Private Sub CustomPropertyGrid_AddNewProperty(ByVal sender As Object, ByVal e As EventArgs) Handles CustomPropertyGrid.AddNewItem
        Using selectPropertyTypeDialog As New SelectCustomPropertyTypeDialog()
            Dim result As DialogResult = selectPropertyTypeDialog.ShowDialog(Me)
            If result = vbCancel Then
                Exit Sub
            End If

            Dim selectedType As CustomBankPropertyType = selectPropertyTypeDialog.SelectedType
            Dim newEntity As CustomBankPropertyEntity = CreateCustomPropertyBasedOnType(selectedType)
            newEntity.Version = "1.0"
            newEntity.BankId = MainBankBrowser.SelectedBank.Id
            Dim editor As New CustomPropertyEditor(newEntity)

            AddHandler editor.FormClosed, AddressOf Editor_FormClosed
            AddHandler editor.OnCustomPropertyAdded, AddressOf CustomPropertyEditor_OnCustomPropertyAdded
            AddHandler editor.OnRefresh, AddressOf RefreshGrid

            editor.Show(Me)
        End Using
    End Sub

    Private Sub CustomPropertyEditor_OnCustomPropertyAdded(sender1 As Object, e1 As SelectedCustomPropertyEventArgs)
        RefreshGrid(sender1, New Questify.Builder.UI.RefreshEventArgs(e1.SelectedCustomProperty, True))
    End Sub

    Private Sub CustomPropertyGrid_EntityDoubleClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles CustomPropertyGrid.EntityDblClick
        Dim editor As CustomPropertyEditor = Nothing

        If e.SelectedEntity IsNot Nothing Then
            Dim entityToEdit As CustomBankPropertyEntity = BankFactory.Instance.GetCustomBankProperty(e.SelectedEntity.ResourceId)
            Dim forms As IEnumerable(Of Form) = GetOwnedFormsOfType(GetType(CustomPropertyEditor))

            For Each form As CustomPropertyEditor In forms
                If form.CustomBankPropertyEntity.CustomBankPropertyId.Equals(entityToEdit.CustomBankPropertyId) Then
                    editor = form
                    Exit For
                End If
            Next

            If editor Is Nothing Then
                editor = New CustomPropertyEditor(entityToEdit)

                editor.Show(Me)
            Else
                editor.Focus()
            End If

            AddHandler editor.FormClosed, AddressOf Editor_FormClosed
            AddHandler editor.OnRefresh, AddressOf RefreshGrid
        End If

    End Sub

    Private Shared Function CreateCustomPropertyBasedOnType(ByVal customPropertyType As CustomBankPropertyType) As CustomBankPropertyEntity

        Dim newEntity As CustomBankPropertyEntity = Nothing
        Select Case customPropertyType
            Case CustomBankPropertyType.Concept
                newEntity = New ConceptStructureCustomBankPropertyEntity()
                With newEntity
                    .Name = My.Resources.CustomPropertiesNewConceptStructure
                    .Title = My.Resources.CustomPropertiesNewConceptStructure
                    .ApplicableToMask = CType(ResourceTypeEnum.ItemResource, Integer)
                    .Publishable = True
                End With
            Case CustomBankPropertyType.FreeValue
                newEntity = New FreeValueCustomBankPropertyEntity()
                With newEntity
                    .Name = My.Resources.CustomPropertiesNewFree
                    .Title = My.Resources.CustomPropertiesNewFree
                    .ApplicableToMask = 0
                End With
            Case CustomBankPropertyType.FreeValueRichText
                newEntity = New RichTextValueCustomBankPropertyEntity()
                With newEntity
                    .Name = My.Resources.CustomPropertiesNewFreeRichText
                    .Title = My.Resources.CustomPropertiesNewFreeRichText
                    .ApplicableToMask = 0
                End With
            Case CustomBankPropertyType.ListMultipleSelect
                newEntity = New ListCustomBankPropertyEntity()
                With CType(newEntity, ListCustomBankPropertyEntity)
                    .Name = My.Resources.CustomPropertiesNewMulti
                    .Title = My.Resources.CustomPropertiesNewMulti
                    .MultipleSelect = True
                    .ApplicableToMask = 0
                End With
            Case CustomBankPropertyType.ListSingleSelect
                newEntity = New ListCustomBankPropertyEntity()
                With newEntity
                    .Name = My.Resources.CustomPropertiesNewSingle
                    .Title = My.Resources.CustomPropertiesNewSingle
                    .ApplicableToMask = 0
                End With
            Case CustomBankPropertyType.Tree
                newEntity = New TreeStructureCustomBankPropertyEntity()
                With newEntity
                    .Name = My.Resources.CustomPropertiesNewTreeStructure
                    .Title = My.Resources.CustomPropertiesNewTreeStructure
                    .ApplicableToMask = CType(ResourceTypeEnum.ItemResource, Integer)
                End With
            Case Else
                Throw New ArgumentException("Unsupported type: " & customPropertyType.GetType().Name)
        End Select

        With newEntity
            .CustomBankPropertyId = Guid.NewGuid()
        End With

        Return newEntity
    End Function

End Class
