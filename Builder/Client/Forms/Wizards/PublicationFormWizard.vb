Imports System.Configuration
Imports System.Text
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel
Imports Questify.Builder.Configuration
Imports System.Text.RegularExpressions
Imports Questify.Builder.UI.PublicationService
Imports System.Net
Imports Questify.Builder.Client.ValidationService
Imports System.Threading
Imports System.IO
Imports System.Reflection
Imports Enums
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Packaging
Imports Questify.Builder.UI

Public Class PublicationFormWizard


    Private ReadOnly _selectionToPublish As IPublicationSelection
    Private _publicationHandler As IPublicationHandler
    Private _publicationTaskProgress As PublicationTaskProgress
    Private ReadOnly _exportedFiles As New List(Of String)
    Private _downloadProgressInitialized As Boolean = False

    Private _canPublishTest As Boolean = True
    Private _selectedPublicationHandler As PublicationHandlerIdentifier
    Private _configurationOptions As Dictionary(Of String, String)
    Private ReadOnly _publishedEntities As String = String.Empty
    Private ReadOnly _skippedEntities As String = String.Empty
    Private ReadOnly _bankId As Integer
    Private _validatorIndexDictionary As Dictionary(Of Integer, String)
    Private _processResult As Boolean = False
    Private ReadOnly _currentTabTitle As String
    Private ReadOnly _bankBreadCrumb As String

    Private Const TabWelcome As String = "WelcomeTab"
    Private Const TabPublicationMethod As String = "PublicationMethodeTab"
    Private Const TabPublicationOptions As String = "PublicationOptionsTab"
    Private Const TabOverview As String = "OverviewTab"
    Private Const TabValidation As String = "ValidationTab"

    Private Const TabProcess As String = "ProcessTab"
    Private Const TabResult As String = "ResultTab"
    Private Const TabOverviewControl As String = "OverviewTabPageControl"



    Public Sub New()
        InitializeComponent()

        Me.InitTabControl()
    End Sub

    Public Sub New(ByVal bankId As Integer, ByVal selectedEntities As IEnumerable(Of ResourceDto), currentTabTitle As String, bankBreadCrumb As String)
        _selectionToPublish = New TestPackagesAndTestPublicationSelection(bankId, selectedEntities.ToList)
        _bankId = bankId
        _currentTabTitle = currentTabTitle
        _bankBreadCrumb = bankBreadCrumb

        InitializeComponent()

        Me.InitTabControl()
    End Sub




    Public Overrides ReadOnly Property ASyncProcessing As Boolean
        Get
            Return True
        End Get
    End Property




    Private Sub ChangePublicationMethod()
        _selectedPublicationHandler = DirectCast(PublicationMethodCombo.SelectedItem, PublicationHandlerIdentifier)

        Debug.Assert(_selectedPublicationHandler IsNot Nothing)
        If (_selectedPublicationHandler Is Nothing) Then
            Return
        End If

        Using publicationClient As New PublicationServiceClient()
            Try
                _configurationOptions = publicationClient.GetConfigurationOptions(_selectedPublicationHandler.Type, _bankId, _selectionToPublish.TestNames.ToArray(), _selectionToPublish.TestPackageNames.ToArray())
                publicationClient.Close()
            Catch serviceException As Exception
                publicationClient.Abort()
                MessageBox.Show(String.Format(My.Resources.ServiceNotAvailable, My.Resources.Publication, FormatErrorMessage(serviceException)), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            End Try
        End Using

        ProcessButton.Enabled = False
        NextButton.Enabled = True

        If _selectionToPublish.TestPackageNames IsNot Nothing AndAlso _selectionToPublish.TestPackageNames.Any() Then
            AddTestEntityNamesToConfigOptions(_selectionToPublish.TestPackageNames)
        ElseIf _selectionToPublish.TestNames IsNot Nothing AndAlso _selectionToPublish.TestNames.Any() Then
            AddTestEntityNamesToConfigOptions(_selectionToPublish.TestNames)
        End If

        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        Dim fileVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location)
        _configurationOptions.Add(PublicationHandlerConfigurationOptions.PublicationAssembly,
                                  $"{fileVersion.ProductName} {fileVersion.ProductVersion}")

        ResolveHandler()
        PreparePublicationOptions()

    End Sub

    Private Sub PreparePublicationOptions()
        If _publicationHandler.PublicationOptionsControl IsNot Nothing Then
            Dim tab = Me.TabControlMain.TabPages(TabPublicationOptions)
            tab.Controls.Clear()
            Dim control = _publicationHandler.PublicationOptionsControl
            control.Dock = DockStyle.Fill
            tab.Controls.Add(control)
        End If
    End Sub

    Private Sub ResolveHandler()
        _publicationHandler = IoCHelper.GetInstances(Of IPublicationHandler).FirstOrDefault(Function(i) i.GetType().ToString().Equals(_selectedPublicationHandler.Type))
        _publicationHandler.PublicationSelection = _selectionToPublish
        _publicationHandler.ConfigurationOptions = _configurationOptions
    End Sub


    Private Sub AddTestEntityNamesToConfigOptions(names As IEnumerable(Of String))
        _configurationOptions.Add(PublicationHandlerConfigurationOptions.UserName, Thread.CurrentPrincipal.Identity.Name)
        Dim resources = ResourceFactory.Instance.GetResourcesByNamesWithOption(_bankId, names.ToList(), New ResourceRequestDTO())
        Dim versions = resources.Cast(Of ResourceEntity).
            Where(Function(r) names.Any(Function(n) n.Equals(r.Name, StringComparison.InvariantCultureIgnoreCase)) AndAlso Not String.IsNullOrEmpty(r.Version)).
            Select(Function(r) $"{r.Name}|{r.Version}").ToList()

        If versions.Count > 0 Then _configurationOptions.Add(PublicationHandlerConfigurationOptions.AssessementTestEntityVersion, String.Join(";", versions))
    End Sub

    Private Function ForcesValidation() As Boolean
        For Each c As Control In Me.Controls
            c.Focus()
        Next

        Return _publicationHandler.IsValid()
    End Function

    Private Sub InitializeHandlers()

        If (_selectionToPublish.IsEmpty) Then
            MessageBox.Show(My.Resources.TestPackageDoesNotContainAnytestPleaseAddTetsToTheTestPackagebeforePublishing, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.Close()
        End If

        Using publicationClient As New PublicationServiceClient()

            Try
                Dim handlers As PublicationHandlerIdentifier() = publicationClient.GetAvailablePublicationHandlers(_bankId, _selectionToPublish.TestNames.ToArray(), _selectionToPublish.TestPackageNames.ToArray())
                For Each handler In handlers.Where(Function(x) x.QualifiesForCurrentSelection)
                    PublicationMethodCombo.Items.Add(handler)
                Next

                If PublicationMethodCombo.Items.Count = 1 Then
                    PublicationMethodCombo.SelectedIndex = 0
                ElseIf PublicationMethodCombo.Items.Count = 0 Then
                    Dim notQualifyingHandlers = handlers.Where(Function(x) Not x.QualifiesForCurrentSelection).Select(Function(y) _
                                                                                                                            $"{ _
                                                                                                                            y _
                                                                                                                            .
                                                                                                                            UserFriendlyName _
                                                                                                                            } - { _
                                                                                                                            y _
                                                                                                                            .
                                                                                                                            ReasonForNotQualifyingForCurrentSelection _
                                                                                                                            }").ToArray()

                    If notQualifyingHandlers.Count > 0 Then
                        Dim unavailablePublicationHandlerExplanation =
                                $"{My.Resources.NoPublicationHandlers}{vbCrLf}{vbCrLf}{ _
                                String.Join(vbCrLf, notQualifyingHandlers)}"
                        Dim messageDialog As New MessageDialog(Me.Text, unavailablePublicationHandlerExplanation)
                        Dim messageDialogGraphics As Graphics = messageDialog.CreateGraphics()
                        Dim messageDialogFont As Font = messageDialog.Font
                        Dim maxHandlerMessageLen As Integer = notQualifyingHandlers.Max(Function(s) messageDialogGraphics.MeasureString(s, messageDialogFont).ToSize().Width)

                        messageDialog.Width = maxHandlerMessageLen + 30
                        messageDialog.ShowDialog(Me)
                    Else
                        Dim messageDialog As New MessageDialog(Me.Text, My.Resources.NoPublicationHandlers)
                        messageDialog.ShowDialog(Me)
                    End If
                    Me.Close()
                End If
                publicationClient.Close()
            Catch serviceException As Exception
                publicationClient.Abort()
                MessageBox.Show(String.Format(My.Resources.ServiceNotAvailable, My.Resources.Publication, FormatErrorMessage(serviceException)), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End Try
        End Using

    End Sub

    Private Sub PublicationForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        _selectionToPublish.Initialise()
        Me.Text = _selectionToPublish.Title
        InitializeHandlers()

        If Not _selectionToPublish.ContainsItems Then
            MessageBox.Show(My.Resources.TestDoesNotContainAnyItemsPleaseAddItemsToTheTestBeforePublishing, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If

    End Sub

    Private Sub PublicationHandler_PublicationProgress(ByVal sender As Object, ByVal e As ProgressEventArgs)
        BackgroundWorkerPublication.ReportProgress(0, e)
    End Sub

    Private Sub PublicationHandler_StartPublication(ByVal sender As Object, ByVal e As StartEventArgs)
        BackgroundWorkerPublication.ReportProgress(0, e)
    End Sub

    Friend Class OverallProgressEventArgs
        Public Property ThisAreStartArgs As Boolean
        Public Property Index As Integer
        Public Property TestName As String
    End Class

    Private Sub Validator_Progress(ByVal sender As Object, ByVal e As ProgressEventArgs)
        If e.ProgessValue.HasValue AndAlso e.ProgessValue.Value > 0 Then
            ProgressBarValidation.Value = e.ProgessValue.Value
        Else
            Dim newValue = ProgressBarValidation.Value + 1
            newValue = Math.Max(ProgressBarValidation.Minimum, newValue)
            newValue = Math.Min(newValue, ProgressBarValidation.Maximum)
            ProgressBarValidation.Value = newValue
        End If
        ValidatorProgressLabel.Text = e.StatusMessage
        Application.DoEvents()
    End Sub

    Private Sub Validator_StartProgress(ByVal sender As Object, ByVal e As StartEventArgs)
        ProcessTabContent.ProcessInfoTextDetail = My.Resources.StartPublication

        With ProgressBarValidation
            .Value = 0
            .Minimum = 0
            .Maximum = e.NumberOfResources
        End With
        ValidatorProgressLabel.Text = String.Empty
        Application.DoEvents()
    End Sub

    Private Sub PublicationMethodCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles PublicationMethodCombo.SelectedIndexChanged
        ChangePublicationMethod()
    End Sub

    Private Sub PublicationWizardForm_WizardBeforeTabChange(ByVal sender As Object, ByVal e As WizardBeforeTabChangeEventArgs) Handles Me.WizardBeforeTabChange
        Select Case e.CurrentTab.Tag.ToString
            Case TabWelcome
                Try
                    If Not _selectionToPublish.AtLeastOneHandlerAvailable() Then
                        e.NextTab = TabControlMain.TabPages(TabPublicationMethod)
                    End If
                Catch serviceException As Exception
                    MessageBox.Show(String.Format(My.Resources.ServiceNotAvailable, My.Resources.Validation, FormatErrorMessage(serviceException)), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    e.Cancel = True
                    Me.Close()
                End Try
            Case TabValidation
                e.Cancel = Not _canPublishTest
            Case TabPublicationMethod
                e.Cancel = PublicationMethodCombo.SelectedIndex = -1
                If Not _publicationHandler.ShowPublicationOptions Then
                    e.NextTab = TabControlMain.TabPages(TabOverviewControl)
                End If
            Case TabPublicationOptions
                Dim isValid As Boolean = ForcesValidation()
                e.Cancel = Not isValid

                If (_selectionToPublish.TestPackageNames.Count() = 0 AndAlso _selectionToPublish.TestNames.Count() = 0) Then
                    e.NextTab = GetProcessingTab()
                End If
        End Select
    End Sub

    Private Sub PublicationWizardForm_WizardDoProcess(ByVal sender As Object, ByVal e As WizardProcessEventArgs) Handles Me.WizardDoProcess
        Me.PushButtonState()
        Me.DisableAllButtons()

        _processResult = Publish()
        Me.PopButtonState()
    End Sub

    Private Function FormatErrorMessage(ByVal exception As Exception) As String
        Dim errorBuilder As New StringBuilder(Environment.NewLine + Environment.NewLine + exception.Message)
        Dim inner = exception.InnerException
        While inner IsNot Nothing
            errorBuilder.AppendLine("- " + inner.Message.ToString())
            inner = inner.InnerException
        End While

        Return errorBuilder.ToString()
    End Function

    Private Sub PublicationWizardForm_WizardTabChanged(ByVal sender As Object, ByVal e As WizardTabChangedEventArgs) Handles Me.WizardTabChanged
        Select Case e.CurrentTab.Tag.ToString
            Case TabWelcome
                WelcomeTabContent.Title = My.Resources.Publication
                WelcomeTabContent.Description = _selectionToPublish.WizardDescription

            Case TabValidation
                ResetTableLayoutControl()
                Me.DisableAllButtons()
                ValidationWizardTabContentControl.Task = My.Resources.ValidationTask
                ValidationWizardTabContentControl.TaskDescription = My.Resources.ValidationTaskDesciption
                Application.DoEvents()

                Dim pollingInterval As Integer = 0
                If Not Integer.TryParse(ConfigurationManager.AppSettings("ValidationPollingInterval"), pollingInterval) Then
                    pollingInterval = 2000
                End If
                Using validationClient = New ValidationServiceClient()

                    Try
                        Dim testNames = _selectionToPublish.TestNamesToBeValidated.ToArray()
                        Dim validatorIdentifiers = validationClient.Validate(_bankId, testNames)
                        Dim rowIndex = 0

                        For Each validatorIdentifier In validatorIdentifiers
                            If rowIndex = 0 Then
                                _validatorIndexDictionary = New Dictionary(Of Integer, String)
                            End If
                            _validatorIndexDictionary.Add(rowIndex, validatorIdentifier.UserFriendlyName)
                            InitHandler(rowIndex, validatorIdentifier.UserFriendlyName)
                            rowIndex += 1
                        Next
                        Application.DoEvents()

                        Dim index As Integer = 0

                        Dim validCount As Integer = 0
                        Dim invalidCount As Integer = 0
                        Dim warningCount As Integer = 0
                        Dim total = 0
                        _canPublishTest = True
                        Dim progressDictionary = New Dictionary(Of String, Integer)
                        Dim finishedDictionary = New Dictionary(Of String, Boolean)
                        Dim totalDictionary = New Dictionary(Of String, Integer)
                        Dim taskIndexMapping = New Dictionary(Of String, Integer)
                        Dim taskIndex = 0
                        For Each validatorIdentifier In validatorIdentifiers
                            taskIndexMapping.Add(validatorIdentifier.TaskId, taskIndex)
                            StartValidating(index)
                            Dim progress = validationClient.GetProgress(validatorIdentifier.TaskId)
                            progressDictionary.Add(validatorIdentifier.TaskId, progress.Progress)
                            finishedDictionary.Add(validatorIdentifier.TaskId, False)
                            totalDictionary.Add(validatorIdentifier.TaskId, progress.Total)
                            taskIndex += 1
                        Next

                        While finishedDictionary.Any(Function(t) t.Value = False)
                            For Each validatorIdentifier In validatorIdentifiers
                                If Not finishedDictionary(validatorIdentifier.TaskId) Then
                                    Dim progress = validationClient.GetProgress(validatorIdentifier.TaskId)
                                    finishedDictionary(validatorIdentifier.TaskId) = progress.Finished
                                    If Not progress.Finished Then
                                        totalDictionary(validatorIdentifier.TaskId) = progress.Total
                                        progressDictionary(validatorIdentifier.TaskId) = progress.Progress
                                        Dim newTotal = totalDictionary.Values.Sum
                                        If total <> newTotal Then
                                            total = newTotal
                                            Validator_StartProgress(Me, New StartEventArgs(newTotal))
                                        End If
                                        Validator_Progress(Me, New ProgressEventArgs(progress.ProgressString, progressDictionary.Values.Sum))
                                    Else
                                        Select Case progress.ValidationResult
                                            Case ValidationResult.NotValid
                                                invalidCount += 1
                                            Case ValidationResult.Valid
                                                validCount += 1
                                            Case ValidationResult.Warning
                                                warningCount += 1
                                        End Select
                                        EndValidating(taskIndexMapping(progress.TaskId), progress, validCount, invalidCount, warningCount)
                                        validationClient.FinishValidation(validatorIdentifier.TaskId)
                                        index += 1
                                    End If
                                End If
                            Next
                            Thread.Sleep(pollingInterval)
                        End While


                        ProgressBarValidation.Value = ProgressBarValidation.Maximum
                        _canPublishTest = (invalidCount = 0)
                    Catch serviceException As Exception
                        MessageBox.Show(String.Format(My.Resources.ServiceNotAvailable, My.Resources.Validation, FormatErrorMessage(serviceException)), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Close()
                    End Try
                End Using

                Me.SetButtons()
            Case TabOverview
                OverviewTabContent.Task = My.Resources.CompleteTheWizard
                OverviewTabContent.TaskDescription = My.Resources.VerifyTheChoicesMadeInTheWizard
                Dim messageBuilder As New StringBuilder
                Dim publishEncryptedText As String = My.Resources.No
                For Each testPackageToPublish As String In _selectionToPublish.TestPackageNames
                    messageBuilder.AppendLine(String.Format(My.Resources.TestPackageToPublishPublicationMethod, testPackageToPublish, vbCrLf, PublicationMethodCombo.Text, vbCrLf, publishEncryptedText, vbCrLf))
                Next
                If _selectionToPublish.TestPackageNames.Any() Then
                    messageBuilder.AppendLine(String.Format(My.Resources.TheTestPackageContainTheFollowingTests, vbCrLf))
                End If
                For Each testToPublish As String In _selectionToPublish.TestNames
                    messageBuilder.AppendLine(String.Format(My.Resources.TestToPublishPublicationMethod, testToPublish, vbCrLf, PublicationMethodCombo.Text, vbCrLf, publishEncryptedText, vbCrLf))
                Next
                OverviewTabContent.OverviewText = messageBuilder.ToString

            Case TabProcess

                ProcessTabContent.Task = My.Resources.PerformingOperation
                ProcessTabContent.TaskDescription = String.Empty

                PushButtonState()
                DisableAllButtons()

                BackgroundWorkerPublication.RunWorkerAsync()

            Case TabResult
                ResultTabContent.TaskDescription = String.Empty
                If _processResult Then
                    ResultTabContent.Task = My.Resources.OperationSuccesfull
                    Dim messageBuilder As New StringBuilder
                    If Not String.IsNullOrEmpty(_publishedEntities) Then
                        If _selectionToPublish.TestPackageNames.Any() Then
                            messageBuilder.AppendLine(String.Format(My.Resources.TheFollowingTestPackagesHaveBeenSuccessfullyPublished0, _publishedEntities))
                        Else
                            messageBuilder.AppendLine(String.Format(My.Resources.TheFollowingTestHaveBeenSuccessfullyPublished0, _publishedEntities))
                        End If
                    End If
                    If Not String.IsNullOrEmpty(_skippedEntities) Then
                        messageBuilder.AppendLine(String.Format(My.Resources.TheFollowingTestAreSkipped0, _skippedEntities))
                    End If
                    ResultTabContent.TaskDescription = messageBuilder.ToString
                    Dim sb As New StringBuilder()
                    If _exportedFiles IsNot Nothing AndAlso _exportedFiles.Count > 0 Then
                        Dim linkCount = 0
                        For Each fullname In _exportedFiles
                            If _publicationHandler.ShowFileResultsAsUrl Then
                                ResultTabContent.AddLink(String.Format(My.Resources.PublicationLocationText, Path.GetFileName(fullname)), fullname, linkCount * 16)
                            Else
                                ResultTabContent.AddTextToResultTab(My.Resources.PublicationResultText)
                            End If
                            linkCount += 1
                        Next
                        sb.AppendLine(GetErrorsAndWarnings)
                    Else
                        ResultTabContent.AddTextToResultTab(GetErrorsAndWarnings())
                    End If
                Else
                    ResultTabContent.Task = My.Resources.OperationUnSuccesfull
                    ResultTabContent.AddTextToResultTab(GetErrorsAndWarnings())
                End If
            Case TabPublicationMethod
                If PublicationMethodCombo.Items.Count >= 1 Then
                    If PublicationMethodCombo.SelectedItem Is Nothing Then
                        PublicationMethodCombo.SelectedIndex = 0
                    End If

                    If PublicationMethodCombo.Items.Count = 1 Then
                        If e.PreviousTab.Tag.ToString = TabPublicationOptions Then
                            OnGotoPreviousTab()
                        Else
                            OnGotoNextTab()
                        End If
                    End If
                    PublicationMethodeTabContent.Task = My.Resources.ChooseAPublicationMethod
                    PublicationMethodeTabContent.TaskDescription = String.Empty
                End If
                If (_selectionToPublish.TestPackageNames.Count() = 0 AndAlso _selectionToPublish.TestNames.Count() = 0) Then
                    NextButton.Enabled = False
                    ProcessButton.Enabled = True
                End If


        End Select
    End Sub

    Private Function GetErrorsAndWarnings() As String
        Dim sb As New StringBuilder(String.Empty)

        If _publicationTaskProgress IsNot Nothing Then
            If Not String.IsNullOrEmpty(_publicationTaskProgress.Errors) Then
                sb.AppendLine(My.Resources.Errors)
                sb.AppendLine()
                sb.AppendLine(_publicationTaskProgress.Errors)
                sb.AppendLine()
            End If
            If Not String.IsNullOrEmpty(_publicationTaskProgress.Warnings) Then
                sb.AppendLine(My.Resources.Warnings)
                sb.AppendLine()
                sb.AppendLine(_publicationTaskProgress.Warnings)
            End If
        End If

        Return sb.ToString
    End Function

    Private Sub InitHandler(ByVal index As Integer, ByVal validatorName As String)
        ValidationTableLayoutPanel.RowCount += 1
        ValidationTableLayoutPanel.RowStyles.Insert(index, New RowStyle(SizeType.AutoSize))

        Dim pictureBox As New PictureBox
        pictureBox.Image = My.Resources.NotValidated
        pictureBox.Size = pictureBox.Image.Size
        pictureBox.Dock = DockStyle.Fill

        Dim label As New Label
        label.Text = validatorName
        label.TextAlign = ContentAlignment.MiddleLeft
        label.Dock = DockStyle.Fill
        label.AutoSize = True

        ValidationTableLayoutPanel.Controls.Add(pictureBox, 0, index)
        ValidationTableLayoutPanel.Controls.Add(label, 1, index)
    End Sub

    Private Sub ResetTableLayoutControl()
        If Me.ValidationTableLayoutPanel.RowCount > 1 Then
            Me.ValidationTableLayoutPanel.Controls.Clear()
            Me.ValidationTableLayoutPanel.RowCount = 1
        End If
    End Sub

    Private Sub StartValidating(ByVal index As Integer)
        If TypeOf (ValidationTableLayoutPanel.GetControlFromPosition(0, index)) Is PictureBox Then
            DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(0, index), PictureBox).Image = My.Resources.Wait
        End If
        If TypeOf (ValidationTableLayoutPanel.GetControlFromPosition(1, index)) Is Label Then
            Dim oldFont As Font = DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(1, index), Label).Font
            Dim newFont As New Font(oldFont.FontFamily, oldFont.Size, FontStyle.Bold)
            DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(1, index), Label).Font = newFont
        End If
    End Sub

    Private Sub EndValidating(ByVal index As Integer, progress As ValidationTaskProgress, ByVal validCount As Integer, ByVal invalidCount As Integer, ByVal warningCount As Integer)

        ValidatorProgressLabel.Text = String.Empty

        If TypeOf (ValidationTableLayoutPanel.GetControlFromPosition(0, index)) Is PictureBox Then
            Select Case progress.ValidationResult
                Case ValidationResult.NotValid
                    DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(0, index), PictureBox).Image = My.Resources.NotValid
                Case ValidationResult.Valid
                    DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(0, index), PictureBox).Image = My.Resources.Valid
                Case ValidationResult.Warning
                    DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(0, index), PictureBox).Image = My.Resources.Warning
            End Select
        End If
        If Not String.IsNullOrEmpty(progress.ResultText) Then
            Dim tooltipResult As New ToolTip
            tooltipResult.SetToolTip(DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(0, index), PictureBox), progress.ResultText)
        End If
        If TypeOf (ValidationTableLayoutPanel.GetControlFromPosition(1, index)) Is Label Then
            Dim oldFont As Font = DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(1, index), Label).Font
            Dim newFont As New Font(oldFont.FontFamily, oldFont.Size, FontStyle.Regular)
            DirectCast(ValidationTableLayoutPanel.GetControlFromPosition(1, index), Label).Font = newFont
        End If
        If progress.IsReportAvailable Then
            Dim tooltipreport As New ToolTip
            Dim reportButton As New Button
            reportButton.Image = My.Resources.open_report

            reportButton.Height = reportButton.Image.Height + 10
            reportButton.Width = reportButton.Image.Width + 10

            reportButton.Dock = DockStyle.Fill
            tooltipreport.SetToolTip(reportButton, My.Resources.ShowReport)
            ValidationTableLayoutPanel.Controls.Add(reportButton, 2, index)
            reportButton.Tag = progress.Report
            AddHandler reportButton.Click, AddressOf ReportButton_Click
        End If
        Dim resultText As String = String.Empty
        If warningCount = 0 AndAlso invalidCount = 0 Then
            resultText = My.Resources.ValidationsSucceeded
        Else
            resultText = String.Format(My.Resources.ValidationResult, validCount.ToString, warningCount.ToString, invalidCount.ToString)
        End If
        Me.ValidatorProgressLabel.Text = resultText

    End Sub

    Private Sub ReportButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim filename = Path.ChangeExtension(Path.GetTempFileName(), "txt")
        File.WriteAllText(filename, CStr(DirectCast(sender, Button).Tag))
        Process.Start(filename)
    End Sub

    Private Function Publish() As Boolean
        If (_selectionToPublish.IsEmpty) Then
            Return False
        End If

        Using publicationClient As New PublicationServiceClient
            Try
                Dim pollingInterval As Integer = 0
                If Not Integer.TryParse(ConfigurationManager.AppSettings("PublicationPollingInterval"), pollingInterval) Then
                    pollingInterval = 2000
                End If

                Dim taskId As String = publicationClient.Publicize(
                    _selectedPublicationHandler.Type,
                    _configurationOptions,
                    _bankId,
                    _selectionToPublish.TestNames.ToArray(),
                    _selectionToPublish.TestPackageNames.ToArray(),
                    False,
                    _publicationHandler.FilePath)

                _publicationTaskProgress = publicationClient.GetProgress(taskId)
                Dim lastTotal As Integer = 0
                While Not _publicationTaskProgress.Finished
                    If Not lastTotal = _publicationTaskProgress.Total Then
                        lastTotal = _publicationTaskProgress.Total
                        PublicationHandler_StartPublication(Me, New StartEventArgs(lastTotal))
                    End If
                    PublicationHandler_PublicationProgress(Me, New ProgressEventArgs(
                        $"{_publicationTaskProgress.Progress}/{lastTotal} - {_publicationTaskProgress.ProgressString}", _publicationTaskProgress.Progress))
                    Thread.Sleep(pollingInterval)
                    _publicationTaskProgress = publicationClient.GetProgress(taskId)
                End While

                HandlePublicationResult()

                publicationClient.FinishPublication(taskId)
            Catch serviceException As Exception
                publicationClient.Abort()

                Dim errors As String = FormatErrorMessage(serviceException)
                MessageBox.Show(String.Format(My.Resources.ServiceNotAvailable, My.Resources.Publication, errors), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                _publicationTaskProgress.Errors = errors
                Return False
            End Try
            Return _publicationTaskProgress IsNot Nothing AndAlso _publicationTaskProgress.Succeeded
        End Using
    End Function

    Private Sub HandlePublicationResult()

        Using webClient As WebClient = New WebClient()
            AddHandler webClient.DownloadProgressChanged, AddressOf DownloadProgressCallback

            Dim testNames As New List(Of String)
            testNames.AddRange(_selectionToPublish.TestPackageNames)
            testNames.AddRange(_selectionToPublish.TestNames)

            For Each result In _publicationTaskProgress.PublicationLocations
                Dim packageUri As Uri = PackageManager.GetPackageUri(New Uri(result))
                Dim downloadFromUrl = If(packageUri IsNot Nothing, packageUri.ToString(), result)

                Dim fileName = String.Empty
                If _publicationTaskProgress.PublicationLocations.Length = 1 Then
                    fileName = _publicationHandler.FilePath
                Else
                    fileName = downloadFromUrl.Substring(downloadFromUrl.LastIndexOf("/") + 1)
                    fileName = Regex.Replace(fileName, "[-][A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}\b", Function(match As Match)
                                                                                                               Dim last As Boolean = match.NextMatch().Index = 0
                                                                                                               If last Then
                                                                                                                   Return String.Empty
                                                                                                               Else
                                                                                                                   Return match.Value
                                                                                                               End If
                                                                                                           End Function, RegexOptions.IgnoreCase)
                    Dim testName As String = testNames.FirstOrDefault(Function(n) fileName.ToLower().StartsWith(n.ToLower()))
                    If testName IsNot Nothing Then
                        fileName = If(testName.Length < fileName.Length, String.Concat(Path.GetFileNameWithoutExtension(_publicationHandler.FilePath), fileName.Substring(testName.Length)), _publicationHandler.FilePath)
                    End If
                End If
                Dim downloadPath = New FileInfo(Path.Combine(_publicationHandler.PublicationPath, fileName))
                _downloadProgressInitialized = False
                webClient.DownloadFile(downloadFromUrl, downloadPath.FullName)
                _exportedFiles.Add(downloadPath.ToString)
            Next
        End Using
    End Sub

    Private Sub DownloadProgressCallback(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        If Not _downloadProgressInitialized Then
            _downloadProgressInitialized = True
            PublicationHandler_StartPublication(sender, New StartEventArgs(100))
        End If
        Dim percentage = Convert.ToInt32(Math.Round((e.BytesReceived / e.TotalBytesToReceive) * 100))
        PublicationHandler_PublicationProgress(sender, New ProgressEventArgs(
            $"Downloading... {e.BytesReceived}/{e.TotalBytesToReceive}", percentage))
    End Sub

    Private Sub BackgroundWorkerPublication_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorkerPublication.DoWork
        MultiLanguageController.InitializeUILanguage()

        _processResult = Publish()
    End Sub

    Private Sub BackgroundWorkerPublication_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorkerPublication.ProgressChanged
        If e.UserState IsNot Nothing Then
            If TypeOf e.UserState Is StartEventArgs Then
                With ProcessTabContent
                    .ProgressMinimumValueDetail = 0
                    .ProgressMaximumValueDetail = DirectCast(e.UserState, StartEventArgs).NumberOfResources
                    .ProgressValueDetail = 0
                End With
            ElseIf TypeOf e.UserState Is ProgressEventArgs Then
                Dim pea As ProgressEventArgs = DirectCast(e.UserState, ProgressEventArgs)
                If pea.ProgessValue.HasValue Then
                    ProcessTabContent.ProgressValueDetail = pea.ProgessValue.Value
                Else
                    ProcessTabContent.ProgressValueDetail += 1
                End If

                ProcessTabContent.ProcessInfoTextDetail = pea.StatusMessage
            ElseIf TypeOf e.UserState Is OverallProgressEventArgs Then
                Dim opea As OverallProgressEventArgs = DirectCast(e.UserState, OverallProgressEventArgs)
                If opea.ThisAreStartArgs Then
                    ProcessTabContent.PublishMultiple = True
                    ProcessTabContent.ProcessInfoTextOverall = String.Empty
                    With ProcessTabContent
                        .ProgressMinimumValueOverAll = 0
                        .ProgressMaximumValueOverAll = opea.Index
                        .ProgressValueDetail = 0
                    End With
                Else
                    ProcessTabContent.ProgressValueOverAll = opea.Index
                    ProcessTabContent.ProcessInfoTextOverall = opea.TestName
                End If
            End If
        End If
    End Sub

    Private Sub BackgroundWorkerPublication_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorkerPublication.RunWorkerCompleted
        Me.PopButtonState()
        Me.OnGotoNextTab()
    End Sub

End Class
