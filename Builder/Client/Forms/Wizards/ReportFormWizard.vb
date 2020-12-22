Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Logging
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.UI
Imports LogHelper = Questify.Builder.Logic.Service.Logging.LogHelper

Public Class ReportFormWizard

    Const CONFIGSECTION_REPORTHANDLERS As String = "reportHandlers"
    Const CONFIGSECTION_STARTTABREPORTHANDLERS As String = "startTabReportHandlers"
    Const TABTITLE_START As String = "Start"


    Private ReadOnly _grid As GridBase
    Private _selectedReportHandler As IReportHandler = Nothing
    Private _result As Boolean = False
    Private _resourceManager As DataBaseResourceManager
    Private ReadOnly _currentTabTitle As String
    Private ReadOnly _bankBreadCrumb As String



    Public Sub New(ByVal grid As GridBase, ByVal bankId As Integer, currentTabTitle As String, bankBreadCrum As String)
        InitializeComponent()

        _grid = grid
        Me.BankId = bankId
        _currentTabTitle = currentTabTitle
        _bankBreadCrumb = bankBreadCrum

        Me.InitTabControl()
    End Sub

    Public Sub New(ByVal bankId As Integer, currentTabTitle As String, bankBreadCrum As String)
        InitializeComponent()

        Me.BankId = bankId
        _currentTabTitle = currentTabTitle
        _bankBreadCrumb = bankBreadCrum

        Me.InitTabControl()
    End Sub


    Public Overrides ReadOnly Property ASyncProcessing() As Boolean
        Get
            Return If(_selectedReportHandler?.IsDataGeneratedAsynchronous, False)
        End Get
    End Property

    Private Sub ReportFormWizard_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = False
    End Sub

    Private Async Sub ReportWizardForm_WizardTabChanged(ByVal sender As Object, ByVal e As WizardTabChangedEventArgs) Handles Me.WizardTabChanged
        If _selectedReportHandler IsNot Nothing Then
            _selectedReportHandler.ClearErrors()
        End If

        Select Case e.CurrentTab.Tag.ToString
            Case "WelcomeTab"
                WelcomeTabContent.Title = My.Resources.GenerateReport
                WelcomeTabContent.Description = My.Resources.ThisWizardHelpsYouToGenerateAReport

                Application.DoEvents()

            Case "SelectHandlerTab"
                Me.DescriptionLabel.Text = String.Empty
                Me.SelectReportHandlerWizardTabContentControl.Task = My.Resources.SelectReport
                Me.SelectReportHandlerWizardTabContentControl.TaskDescription = My.Resources.SelectReportDescription

                If Me.SelectReportComboBox.DataSource Is Nothing Then

                    Dim selectedEntities As New List(Of ResourceDto)
                    If Me._grid IsNot Nothing Then selectedEntities = Me._grid.SelectedEntities.ToList()

                    Dim section As String = CONFIGSECTION_REPORTHANDLERS
                    If _currentTabTitle = TABTITLE_START Then section = CONFIGSECTION_STARTTABREPORTHANDLERS

                    Dim reportHandlerDictionary As Dictionary(Of String, IReportValidationBase) = ConfigPluginHelper.GetListOfSupportedHandlersBySectionName(section, selectedEntities, Me.BankId)
                    Me.SelectReportComboBox.ValueMember = "value"
                    Me.SelectReportComboBox.DisplayMember = "key"

                    Me.SelectReportComboBox.DataSource = New BindingSource() With {.RaiseListChangedEvents = False, .DataSource = reportHandlerDictionary}

                    If reportHandlerDictionary.Any() Then
                        Dim selectedText = UserSettings.GetUserWizardSettingsForWizard(Me.Name).GetSettingsForControl(Me.SelectReportComboBox.Name)

                        Me.SelectReportComboBox.SelectedItem = Me.SelectReportComboBox.Items.OfType(Of KeyValuePair(Of String, IReportValidationBase)) _
                                                                                                            .Where(Function(kvp) kvp.Key = selectedText) _
                                                                                                            .FirstOrDefault
                    End If
                End If

                Application.DoEvents()

            Case "InitialiseProgressTab"
                If (_selectedReportHandler.IsInitDataGeneratedAsynchronous) Then
                    PreviousButton.Enabled = False
                    NextButton.Enabled = False

                    Me.ProcessTabContent.ProcessInfoTextDetail = String.Empty
                    InitialiseProgressTabContent.Task = My.Resources.InitialiseProgressTask
                    InitialiseProgressTabContent.TaskDescription = My.Resources.InitialiseProgressDescription

                    Application.DoEvents()
                    AddHandler _selectedReportHandler.StartProgress, AddressOf ReportHandler_InitStartReportProgress
                    AddHandler _selectedReportHandler.Progress, AddressOf ReportHandler_InitReportProgress

                    AddHandler _selectedReportHandler.ReportCompleted, AddressOf ReportHandler_InitDataCompleted
                    _selectedReportHandler.InitialiseData()
                End If
            Case "ExtraOptionsTab"
                ExtraOptionsWizardTabContentControl.Task = _selectedReportHandler.ExtraOptionTask
                ExtraOptionsWizardTabContentControl.TaskDescription = _selectedReportHandler.ExtraOptionTaskDescription

                WrapExtraOptionUIPanel.Controls.Clear()
                Dim userControl As UserControl = _selectedReportHandler.GetExtraOptionsUI
                userControl.Dock = DockStyle.Fill
                WrapExtraOptionUIPanel.Controls.Add(userControl)

                Application.DoEvents()

            Case "SelectExportLocationTab"
                SelectExportPathWizardTabContentControl.Task = My.Resources.SelectReportLocation
                SelectExportPathWizardTabContentControl.TaskDescription = My.Resources.SelectReportLocationDescription

                WrapSelectLocationUIPanel.Controls.Clear()
                Dim userControl As UserControl = _selectedReportHandler.GetExportLocationUI
                userControl.Dock = DockStyle.Fill
                WrapSelectLocationUIPanel.Controls.Add(userControl)

                Application.DoEvents()

            Case "OverviewTab"
                OverviewTabContent.Task = My.Resources.CompleteTheWizard
                OverviewTabContent.TaskDescription = My.Resources.VerifyTheChoicesMadeInTheWizard
                OverviewTabContent.OverviewText = _selectedReportHandler.Overview

            Case "ProcessTab"
                Me.ProcessTabContent.Task = My.Resources.GenerationProgress
                Me.ProcessTabContent.TaskDescription = My.Resources.GenerationProgressDescription

                Me._resourceManager = New DataBaseResourceManager(Me.BankId)

                Application.DoEvents()

                AddHandler Me._selectedReportHandler.StartProgress, AddressOf ReportHandler_CreateStartReportProgress
                AddHandler Me._selectedReportHandler.Progress, AddressOf ReportHandler_CreateReportProgress

                If Me._selectedReportHandler.IsDataGeneratedAsynchronous Then
                    AddHandler Me._selectedReportHandler.ReportCompleted, AddressOf ReportHandler_ReportCompleted
                End If

                Me.DisableAllButtons()

                Dim sw = new Stopwatch()
                sw.Start()

                Try
                    Dim asyncHandler = TryCast(Me._selectedReportHandler, IReportHandlerAsync)
                    If (_selectedReportHandler.IsDataGeneratedAsynchronous AndAlso asyncHandler IsNot Nothing) Then
                        CancelBtn.Enabled = True

                        Dim task = Threading.Tasks.Task.Run(AddressOf asyncHandler.GenerateDataAsync)
                        _result = Await task
                    Else
                        _result = _selectedReportHandler.GenerateData()
                        ResultTabContent.ResultText = _selectedReportHandler.ResultText
                    End If
                    sw.Stop()

                    Dim properties = New Dictionary(Of String, String) From {
                            {"HandlerName", _selectedReportHandler.Name},
                            {"HandlerType", _selectedReportHandler.GetType().ToString()},
                            {"Grid", Me._grid?.Name},
                            {"BankId", BankId.ToString()}
                    }

                    Dim metrics = New Dictionary(Of String, Double) From {
                       {"Duration", sw.Elapsed.TotalSeconds},
                       {"NumberOfResources", If(_selectedReportHandler.Collection?.Count(), 0)}
                    }

                    LogHelper.TrackEvent(EventsToTrack.ReportCreated, properties, metrics)
                Finally
                    If Not Me._selectedReportHandler.IsDataGeneratedAsynchronous Then
                        RemoveHandler Me._selectedReportHandler.StartProgress, AddressOf ReportHandler_CreateStartReportProgress
                        RemoveHandler Me._selectedReportHandler.Progress, AddressOf ReportHandler_CreateReportProgress

                        TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(Me._resourceManager)
                        Me._resourceManager.Dispose()
                        Me._resourceManager = Nothing
                    End If
                End Try

            Case "ResultTab"
                If _result Then

                    If Not String.IsNullOrEmpty(_selectedReportHandler?.ExportedReportLocation) Then
                        ResultTabContent.ResultText = _selectedReportHandler.ResultText

                        Dim linkCount = 1
                        Dim theLocations As String = _selectedReportHandler.ExportedReportLocation
                        For Each theLocation In theLocations.Split(";"c)
                            ResultTabContent.AddLink($"{My.Resources.ClickHereToOpen}: ", theLocation, linkCount * 16)
                            linkCount += 1
                        Next
                    Else
                        ResultTabContent.ResultText = _selectedReportHandler.ResultText
                    End If

                    ResultTabContent.Task = My.Resources.OperationSuccesfull
                Else
                    ResultTabContent.Task = My.Resources.OperationUnSuccesfull
                    ResultTabContent.ResultText = _selectedReportHandler.ResultText
                End If

                ResultTabContent.TaskDescription = String.Empty
        End Select
    End Sub

    Private Sub ReportHandler_ReportCompleted(ByVal sender As Object, ByVal e As ReportCompletedEventArgs)
        _result = e.Result

        RemoveHandler _selectedReportHandler.StartProgress, AddressOf ReportHandler_CreateStartReportProgress
        RemoveHandler _selectedReportHandler.Progress, AddressOf ReportHandler_CreateReportProgress
        RemoveHandler _selectedReportHandler.ReportCompleted, AddressOf ReportHandler_ReportCompleted

        TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
        _resourceManager.Dispose()
        _resourceManager = Nothing

        SelectTab(ResultTabPageControl)
    End Sub

    Private Sub ReportHandler_InitDataCompleted(ByVal sender As Object, ByVal e As ReportCompletedEventArgs)
        _result = e.Result
        RemoveHandler _selectedReportHandler.StartProgress, AddressOf ReportHandler_CreateStartReportProgress
        RemoveHandler _selectedReportHandler.Progress, AddressOf ReportHandler_CreateReportProgress
        RemoveHandler _selectedReportHandler.ReportCompleted, AddressOf ReportHandler_InitDataCompleted
        OnGotoNextTab()
    End Sub

    Private Sub ReportWizardForm_WizardBeforeTabChange(ByVal sender As Object, ByVal e As WizardBeforeTabChangeEventArgs) Handles Me.WizardBeforeTabChange
        If _selectedReportHandler IsNot Nothing AndAlso Not String.IsNullOrEmpty(_selectedReportHandler.ValidationErrors) AndAlso Not e.CurrentTab.Tag.ToString = "ProcessTab" Then
            e.Cancel = True
        Else

            Select Case e.CurrentTab.Tag.ToString
                Case "WelcomeTab"

                Case "SelectHandlerTab"
                    If _selectedReportHandler Is Nothing Then
                        SelectReportErrorProvider.SetError(SelectReportComboBox, My.Resources.PleaseSelectAReport)
                        e.Cancel = True
                    Else
                        If _selectedReportHandler.ShouldUseGridAsInput Then
                            _selectedReportHandler.GridToExport = _grid.GridControl
                        End If

                        _selectedReportHandler.BankId = BankId

                        If Not _selectedReportHandler.ShowInitialiseProgressTab Then
                            e.NextTab = TabControlMain.TabPages(GetNextTabAfterSelectHandler())
                        Else
                            If Not _selectedReportHandler.IsInitDataGeneratedAsynchronous Then
                                _selectedReportHandler.InitialiseData()
                                e.NextTab = TabControlMain.TabPages(GetNextTabAfterSelectHandler())
                            End If
                        End If

                        Dim settings = UserSettings.GetUserWizardSettingsForWizard(Me.Name)
                        settings.AddSettingsForControl(SelectReportComboBox.Name, DirectCast(SelectReportComboBox.SelectedItem, KeyValuePair(Of String, IReportValidationBase)).Key)
                        UserSettings.StoreUserWizardSettings()
                    End If

                Case "InitialiseProgressTab"
                    If Not _selectedReportHandler.ShowExtraOptionsTab Then
                        If Not _selectedReportHandler.ShowSelectLocationTab Then
                            e.NextTab = TabControlMain.TabPages("CreateReportProgressTab")
                        Else
                            e.NextTab = TabControlMain.TabPages("SelectExportLocationTab")
                        End If
                    End If


                Case "ExtraOptionsTab"

                Case "SelectExportLocationTab"
                    If Not _selectedReportHandler.ShowSelectLocationTab And Not _selectedReportHandler.ShowCreateReportProgressTab Then
                        e.NextTab = TabControlMain.TabPages("OverviewTabPageControl")
                    Else
                        e.NextTab = GetProcessingTab()
                    End If

                    If e.NextTab.Tag.ToString = "OverviewTab" AndAlso String.IsNullOrEmpty(_selectedReportHandler.Overview) Then
                        e.NextTab = GetProcessingTab()
                    End If
            End Select
        End If
    End Sub

    Private Function GetNextTabAfterSelectHandler() As String
        If Not _selectedReportHandler.ShowExtraOptionsTab Then
            If Not _selectedReportHandler.ShowSelectLocationTab Then
                Return "CreateReportProgressTab"
            Else
                Return "SelectExportLocationTab"
            End If
        Else
            Return "ExtraOptionsTab"
        End If
    End Function

    Public Sub ReportHandler_InitStartReportProgress(ByVal sender As Object, ByVal e As StartEventArgs)
        InitialiseProgressLabel.Text = My.Resources.StartGenerationReport
        InitialiseProgressBar.Minimum = 0
        InitialiseProgressBar.Maximum = e.NumberOfResources
        InitialiseProgressBar.Value = 0
        Application.DoEvents()
    End Sub

    Public Sub ReportHandler_InitReportProgress(ByVal sender As Object, ByVal e As ProgressEventArgs)
        If InitialiseProgressBar.Value < InitialiseProgressBar.Maximum Then
            If e.ProgessValue.HasValue Then
                Me.InitialiseProgressBar.Value = e.ProgessValue.Value
            Else
                Me.InitialiseProgressBar.Value += 1
            End If
            InitialiseProgressLabel.Text = e.StatusMessage
            InitialiseProgressLabel.Refresh()
            InitialiseProgressBar.Refresh()

            If InitialiseProgressBar.Value = InitialiseProgressBar.Maximum Then
                NextButton.Enabled = True
            End If
        End If
    End Sub

    Public Sub ReportHandler_CreateStartReportProgress(ByVal sender As Object, ByVal e As StartEventArgs)
        If (InvokeRequired) Then
            Invoke(Sub() ReportHandler_CreateStartReportProgress(sender, e))
        Else
            ProcessTabContent.ProgressMinimumValueDetail = 0
            ProcessTabContent.ProgressMaximumValueDetail = e.NumberOfResources + 1
            ProcessTabContent.ProcessInfoTextDetail = My.Resources.StartGenerationReport
            ProcessTabContent.ProgressValueDetail = 0
            Application.DoEvents()
        End If
    End Sub

    Public Sub ReportHandler_CreateReportProgress(ByVal sender As Object, ByVal e As ProgressEventArgs)
        If (InvokeRequired) Then
            Invoke(Sub() ReportHandler_CreateReportProgress(sender, e))
        Else
            If e.ProgessValue.HasValue Then
                ProcessTabContent.ProgressValueDetail = e.ProgessValue.Value
            Else
                ProcessTabContent.ProgressValueDetail += 1
            End If
            ProcessTabContent.ProcessInfoTextDetail = e.StatusMessage
            Application.DoEvents()
        End If
    End Sub

    Private Sub SelectReportComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SelectReportComboBox.SelectedIndexChanged
        _selectedReportHandler = DirectCast(SelectReportComboBox.SelectedValue, IReportHandler)
        DescriptionLabel.Text = _selectedReportHandler.Description
    End Sub

    Private Sub ReportFormWizard_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Text = My.Resources.GenerateReport
    End Sub

    Private Sub ReportFormWizard_WizardCancel(sender As Object, e As WizardCancelEventArgs) Handles Me.WizardCancel
        Dim asyncHanler = TryCast(_selectedReportHandler, IReportHandlerAsync)

        If (asyncHanler IsNot Nothing) Then
            asyncHanler.CancelReportGeneration()
        End If

    End Sub
End Class
