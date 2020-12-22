Imports System.ComponentModel
Imports System.Linq
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.UI

Public Class ExportFormWizard

    Private _selectedExportHandler As IExportHandlerBase

    Private _currentOptionControl As ExportOptionControlBase

    Private _selectedEntities As IList(Of ResourceEntity)

    Private _exportProcessExceptionMessage As String

    Private _bankName As String

    Public Property SelectedEntities() As IList(Of ResourceEntity)
        Get
            Return _selectedEntities
        End Get
        Set(ByVal value As IList(Of ResourceEntity))
            _selectedEntities = value
        End Set
    End Property

    Public Overrides ReadOnly Property ASyncProcessing As Boolean
        Get
            Return True
        End Get
    End Property

    Private Property ExportProcessWasCanceled As Boolean

    Private Overloads ReadOnly Property BankName As String
        Get
            If String.IsNullOrEmpty(_bankName) Then
                _bankName = BankFactory.Instance.GetBankName(BankId)
            End If
            Return _bankName
        End Get
    End Property

    Public Sub New()
        InitializeComponent()

        Me.InitTabControl()
    End Sub

    Private Sub PublicationForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = My.Resources.ExportOfResources

        InitializeHandlersFromConfig()
    End Sub

    Private Sub ChangeExportMethod()
        _selectedExportHandler = DirectCast(ExportMethodCombo.SelectedItem, IExportHandlerBase)

        OptionsGroupBox.Controls.Clear()
        _currentOptionControl = _selectedExportHandler.GetOptionsUserControl()

        With _currentOptionControl.ConfigurationOptions
            If Not .ContainsKey("exportBank") Then
                .Add("exportBank", Nothing)
            End If

            If Me.SelectedEntities IsNot Nothing Then
                .Item("exportBank") = False.ToString
            Else
                .Item("exportBank") = True.ToString
            End If
        End With

        OptionsGroupBox.Controls.Add(_currentOptionControl)
        _currentOptionControl.Dock = DockStyle.Fill
    End Sub

    Private Sub InitializeHandlersFromConfig()
        For Each exportHandler As PluginHandlerElement In ConfigPluginHelper.GetListOfPluginHandlersBySectionName("exportHandlers")
            Dim handlerInstance = TryCreateInstanceOfExportHandler(exportHandler)
            If handlerInstance Is Nothing Then
                Continue For
            End If

            AddHandler handlerInstance.StartProgress, AddressOf ExportHandler_StartExport
            AddHandler handlerInstance.Progress, AddressOf ExportHandler_ExportProgress
            ExportMethodCombo.Items.Add(handlerInstance)
        Next

        If ExportMethodCombo.Items.Count = 1 Then
            ExportMethodCombo.SelectedIndex = 0
        ElseIf ExportMethodCombo.Items.Count = 0 Then
            MessageBox.Show(My.Resources.NoExportHandlersAvailable, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If
    End Sub

    Private Function TryCreateInstanceOfExportHandler(exportHandler As PluginHandlerElement) As IExportHandlerBase
        Try
            Return DirectCast(Activator.CreateInstance(Type.GetType(exportHandler.Type, True)), IExportHandlerBase)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub PublicationMethodCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ExportMethodCombo.SelectedIndexChanged
        ChangeExportMethod()
    End Sub


    Private Sub ExportHandler_StartExport(ByVal sender As Object, ByVal e As StartEventArgs)
        BackgroundWorkerExport.ReportProgress(0, e)
    End Sub

    Private Sub ExportHandler_ExportProgress(ByVal sender As Object, ByVal e As ProgressEventArgs)
        BackgroundWorkerExport.ReportProgress(0, e)
    End Sub

    Private Sub ForcesValidation()
        For Each c As Control In Me.Controls
            c.Focus()
        Next
    End Sub

    Private Sub AddResourceEntityToSharedResourceEntryCollection(ByVal targetResourceCollection As ResourceEntryCollection, ByVal resourceIds As List(Of Guid), ByVal nameOfResourceEntityToAdd As String)
        If Not targetResourceCollection.ContainsResource(nameOfResourceEntityToAdd) Then
            Dim request = New ResourceRequestDTO With {.WithDependencies = True}
            Dim resource As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(BankId, nameOfResourceEntityToAdd, request)

            AddResourceEntityToSharedResourceEntryCollection(targetResourceCollection, resourceIds, resource.DependentResourceCollection.Select(Function(res) res.DependentResourceId).ToList())

            Dim resourceToAdd As New ResourceEntry(resource.Name, ResourceVersionConverter.ConvertVersion(resource.Version))
            If Not String.IsNullOrEmpty(resource.OriginalVersion) Then
                resourceToAdd.OriginalName = resource.OriginalName
                resourceToAdd.OriginalVersion = ResourceVersionConverter.ConvertVersion(resource.OriginalVersion)
            End If
            DetermineResourceWithCustomProperties(resource, resourceIds)
            targetResourceCollection.Add(resourceToAdd)
        End If
    End Sub

    Private Sub AddResourceEntityToSharedResourceEntryCollection(ByVal targetResourceCollection As ResourceEntryCollection, ByVal resourceIds As List(Of Guid), ByVal resourceEntitiesToAdd As List(Of Guid))
        Dim request = New ResourceRequestDTO With {.WithDependencies = True}
        Dim resources As EntityCollection = ResourceFactory.Instance.GetResourcesByIdsWithOption(resourceEntitiesToAdd, request)
        For Each resource As ResourceEntity In resources
            If Not targetResourceCollection.ContainsResource(resource.Name) Then
                AddResourceEntityToSharedResourceEntryCollection(targetResourceCollection, resourceIds, resource.DependentResourceCollection.Select(Function(res) res.DependentResourceId).ToList())

                Dim resourceToAdd As New ResourceEntry(resource.Name, ResourceVersionConverter.ConvertVersion(resource.Version))
                If Not String.IsNullOrEmpty(resource.OriginalVersion) Then
                    resourceToAdd.OriginalName = resource.OriginalName
                    resourceToAdd.OriginalVersion = ResourceVersionConverter.ConvertVersion(resource.OriginalVersion)
                End If
                DetermineResourceWithCustomProperties(resource, resourceIds)
                targetResourceCollection.Add(resourceToAdd)
            End If
        Next
    End Sub

    Private Sub DetermineResourceWithCustomProperties(resource As ResourceEntity, resourceIds As List(Of Guid))
        Dim resourceType As String = resource.GetType().ToString().ToLower()
        If resourceType.EndsWith(".itemresourceentity") OrElse resourceType.EndsWith(".assessmenttestresourceentity") OrElse resourceType.EndsWith(".genericresourceentity") Then
            If Not resourceIds.Contains(resource.ResourceId) Then resourceIds.Add(resource.ResourceId)
        End If
    End Sub


    Private Sub BackgroundWorkerExport_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorkerExport.DoWork

        MultiLanguageController.InitializeUILanguage()

        If TypeOf _selectedExportHandler Is IExportHandlerPackage Then
            Using resourceManager As New DataBaseResourceManager(Me.BankId, True)

                If Me.SelectedEntities IsNot Nothing Then
                    Dim resources As New ResourceEntryCollection
                    Dim resourceIds As New List(Of Guid)

                    resourceManager.IncludeMetaData = Logic.ResourceManager.MetaDataType.All

                    ExportHandler_StartExport(Me, New StartEventArgs(Me.SelectedEntities.Count))
                    For Each ent As IEntity2 In Me.SelectedEntities
                        If TypeOf ent Is ResourceEntity Then
                            Dim entityNameToProcess As String = DirectCast(ent, ResourceEntity).Name

                            ExportHandler_ExportProgress(Me, New ProgressEventArgs(String.Format(My.Resources.ExportFormWizard_BusyConstructingListOfDepencies, entityNameToProcess)))

                            If BackgroundWorkerExport.CancellationPending Then
                                Exit For
                            End If

                            AddResourceEntityToSharedResourceEntryCollection(resources, resourceIds, entityNameToProcess)
                        End If
                    Next
                    If BackgroundWorkerExport.CancellationPending Then
                        e.Cancel = True
                    Else
                        DirectCast(_selectedExportHandler, IExportHandlerPackage).ResourceIds = resourceIds
                        e.Cancel = Not DirectCast(_selectedExportHandler, IExportHandlerPackage).Export(DirectCast(sender, BackgroundWorker), resourceManager, resources)
                    End If
                Else
                    e.Cancel = Not DirectCast(_selectedExportHandler, IExportHandlerPackage).Export(DirectCast(sender, BackgroundWorker), resourceManager, Me.BankId)
                End If
            End Using

        End If
    End Sub

    Private Sub BackgroundWorkerExport_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorkerExport.ProgressChanged
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
            End If
        End If
    End Sub

    Private Sub BackgroundWorkerExport_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorkerExport.RunWorkerCompleted
        ExportProcessWasCanceled = e.Cancelled
        If e.Error IsNot Nothing Then
            _exportProcessExceptionMessage = e.Error.Message
            If TypeOf (_selectedExportHandler) Is PackageExportHandler Then
                DirectCast(_selectedExportHandler, PackageExportHandler).HasError = True
            End If
        End If
        Me.OnGotoNextTab()
    End Sub


    Private Sub ExportFormWizard_WizardCancel(sender As Object, e As WizardCancelEventArgs) Handles MyBase.WizardCancel
        If BackgroundWorkerExport.IsBusy Then
            If BackgroundWorkerExport.WorkerSupportsCancellation Then
                BackgroundWorkerExport.CancelAsync()
            End If

            e.Cancel = True
        End If
    End Sub

    Private Sub PublicationWizardForm_WizardBeforeProcess(ByVal sender As Object, ByVal e As WizardProcessEventArgs) Handles Me.WizardBeforeProcess
        If Not _selectedExportHandler.GetOptionsUserControl.Validate(False) Then
            e.Cancel = True
        Else
            If Not String.IsNullOrEmpty(_selectedExportHandler.GetOptionsUserControl.ErrorMessage) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub PublicationWizardForm_WizardBeforeTabChange(ByVal sender As Object, ByVal e As WizardBeforeTabChangeEventArgs) Handles Me.WizardBeforeTabChange
        If _currentOptionControl IsNot Nothing Then _currentOptionControl.Focus()

        Select Case e.CurrentTab.Tag.ToString
            Case "ExportMethodeTab"
                e.Cancel = ExportMethodCombo.SelectedIndex = -1

            Case "ExportOptionsTab"
                ForcesValidation()
                e.Cancel = Not String.IsNullOrEmpty(_selectedExportHandler.GetOptionsUserControl.ErrorMessage)

        End Select
    End Sub

    Private Sub PublicationWizardForm_WizardCompleted(ByVal sender As Object, ByVal e As EventArgs) Handles Me.WizardCompleted
        If ExportProcessWasCanceled Then
            ResultTabContent.Task = My.Resources.ExportTaskOperationCancelled
            ResultTabContent.TaskDescription = My.Resources.ExportTaskDescriptionOperationCancelled

            ResultTabContent.ResultText = My.Resources.ExportResultDescriptionOperationCancelled
        Else
            If Not String.IsNullOrEmpty(_exportProcessExceptionMessage) Then
                ResultTabContent.Task = My.Resources.ExportTaskOperationFailed
                ResultTabContent.TaskDescription = String.Empty
                ResultTabContent.ResultText = _exportProcessExceptionMessage
            Else
                ResultTabContent.ResultText = _selectedExportHandler.GetOptionsUserControl.ErrorMessage
            End If
        End If
    End Sub


    Private Sub PublicationWizardForm_WizardTabChanged(ByVal sender As Object, ByVal e As WizardTabChangedEventArgs) Handles Me.WizardTabChanged

        Me.CancelBtn.DialogResult = DialogResult.None

        Select Case e.CurrentTab.Tag.ToString

            Case "WelcomeTab"
                WelcomeTabContent.Title = My.Resources.ExportTitle
                WelcomeTabContent.Description = My.Resources.ThisWizardHelpsYouToExportTheSelectedResources

            Case "OverviewTab"
                Dim url As String = String.Empty
                Dim isBankExport As Boolean = Me.SelectedEntities Is Nothing
                Dim isSetExport As Boolean = False

                If TypeOf _selectedExportHandler Is IExportHandlerPackage Then
                    url = DirectCast(_currentOptionControl, PackageExportOptionsControl).Options.PackageUrl
                    isSetExport = DirectCast(_currentOptionControl, PackageExportOptionsControl).Options.ExportSubBanks

                    If isBankExport Then
                        If isSetExport Then
                            OverviewTabContent.OverviewText = String.Format(My.Resources.ResourceToPublishDestinationPublicationMethod, Me.BankName, vbCrLf, url, ExportMethodCombo.Text & " (packageset)")
                        Else
                            OverviewTabContent.OverviewText = String.Format(My.Resources.ResourceToPublishDestinationPublicationMethod, Me.BankName, vbCrLf, url, ExportMethodCombo.Text)
                        End If
                    Else
                        OverviewTabContent.OverviewText = String.Format(My.Resources.ResourceToPublishDestinationPublicationMethod, Me.SelectedEntities.Count.ToString(), vbCrLf, url, ExportMethodCombo.Text)
                    End If

                End If


                OverviewTabContent.Task = My.Resources.CompleteTheWizard
                OverviewTabContent.TaskDescription = My.Resources.VerifyTheChoicesMadeInTheWizard

            Case "ProcessTab"
                ProcessTabContent.Task = My.Resources.PerformingOperation
                ProcessTabContent.TaskDescription = String.Empty

                Me.CancelBtn.DialogResult = DialogResult.None

                BackgroundWorkerExport.RunWorkerAsync()

            Case "ResultTab"
                Dim url As String = String.Empty
                Dim exportFailed As Boolean = False
                If TypeOf _selectedExportHandler Is IExportHandlerPackage Then
                    url = DirectCast(_currentOptionControl, PackageExportOptionsControl).Options.PackageUrl
                End If
                If TypeOf (_selectedExportHandler) Is PackageExportHandler Then
                    Dim packageExportHandler As PackageExportHandler = DirectCast(_selectedExportHandler, PackageExportHandler)

                    If (packageExportHandler.HasError) Then
                        ResultTabContent.Task = My.Resources.OperationUnSuccesfull
                        ResultTabContent.TaskDescription = My.Resources.ExportTaskOperationFailed
                        ResultTabContent.ResultText = _exportProcessExceptionMessage
                        exportFailed = True
                    End If
                End If

                If Not exportFailed Then
                    If Me.SelectedEntities IsNot Nothing Then
                        ResultTabContent.ResultText = String.Format(My.Resources.SuccesfulyExportedResourcesTo, Me.SelectedEntities.Count.ToString, url)
                    ElseIf TypeOf _selectedExportHandler Is IExportHandlerPackage Then
                        ResultTabContent.ResultText = String.Format(My.Resources.SuccesfulyExportedResourcesTo, DirectCast(_selectedExportHandler, IExportHandlerPackage).ResourceIds.Count, url)
                    Else
                        ResultTabContent.ResultText = String.Format(My.Resources.SuccesfulyExportedResourcesTo, 0, url)
                    End If

                    ResultTabContent.Task = My.Resources.OperationSuccesfull
                    ResultTabContent.TaskDescription = My.Resources.SuccesfulyExported
                End If

            Case "ExportMethodeTab"
                If ExportMethodCombo.SelectedIndex = -1 Then
                    ExportMethodCombo.SelectedIndex = 0
                End If

                ExportMethodeTabContent.Task = My.Resources.FillTheExportMethodOptions
                ExportMethodeTabContent.TaskDescription = String.Empty

            Case "ExportOptionsTab"
                ExportOptionsTabContent.Task = My.Resources.PleaseChooseThePublicationLocation
                ExportOptionsTabContent.TaskDescription = String.Empty
                _currentOptionControl.Focus()

        End Select
    End Sub
End Class
