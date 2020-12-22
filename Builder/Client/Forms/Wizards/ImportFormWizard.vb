Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports System.Text
Imports Questify.Builder.Client.BuilderTasksService
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Packaging
Imports Questify.Builder.Security
Imports Questify.Builder.UI
Imports Questify.Builder.Logic.ResourceManager

Public Class ImportFormWizard

    <Flags()>
    Public Enum ResourceConflictResolution
        LeaveCurrentInstance = 1
        LeaveThisAndFollowingInstances = 2
        ReplaceCurrentInstance = 4
        ReplaceThisAndFollowingInstances = 8
    End Enum

    Private WithEvents rm As ResourceManifest
    Private WithEvents _selectedImportHandler As IImportHandler
    Private _currentOptionControl As ImportOptionControlBase
    Private ReadOnly _itemTemplateParameters As New Dictionary(Of String, ParameterSetCollection)
    Private ReadOnly _controlTemplatesAndIltsThatUseThem As New Dictionary(Of String, List(Of Guid))
    Private ReadOnly _iltsWithControlTemplatesAndIds As New Dictionary(Of String, Dictionary(Of String, List(Of String)))
    Private ReadOnly _skippedResources As New List(Of String)
    Private _bankItemLayoutTemplates As IEnumerable(Of ItemLayoutTemplateResourceEntity)
    Private _bankControltemplates As IEnumerable(Of ControlTemplateResourceEntity)
    Private ReadOnly _itemsWithRemovedCustomProperties As New List(Of String)
    Private _existingResourceConflictResolution As ResourceConflictResolution
    Private _manifest As ResourceManifest
    Private ReadOnly _listOfErrors As New List(Of String)
    Private ReadOnly _listOfWarnings As New List(Of String)
    Private _harmonizationIsBusy As Boolean = False
    Private _harmonizationCancelled As Boolean = False
    Private ReadOnly _bankResourceManagerCache As New Dictionary(Of Integer, ResourceManagerHolder)
    Private ReadOnly _bankCache As New Dictionary(Of Integer, BankEntity)
    Private _shouldHarmonize As Boolean = False
    Private _harmonizeResult As BuilderTaskResult

    Public Sub New()
        InitializeComponent()

        Me.InitTabControl()
    End Sub

    Public ReadOnly Property ChangesMade As Boolean
        Get
            Return _currentOptionControl IsNot Nothing AndAlso _currentOptionControl.ChangesMade
        End Get
    End Property

    Private ReadOnly Property BankItemLayoutTemplates As IEnumerable(Of ItemLayoutTemplateResourceEntity)
        Get
            If _bankItemLayoutTemplates Is Nothing Then
                _bankItemLayoutTemplates = ResourceFactory.Instance.GetItemLayoutTemplatesForBank(BankId).OfType(Of ItemLayoutTemplateResourceEntity)()
            End If
            Return _bankItemLayoutTemplates
        End Get
    End Property

    Private ReadOnly Property BankControlTemplates As IEnumerable(Of ControlTemplateResourceEntity)
        Get
            If _bankControltemplates Is Nothing Then
                _bankControltemplates = ResourceFactory.Instance.GetControlTemplatesForBank(BankId).OfType(Of ControlTemplateResourceEntity)().Where(Function(ct) ct.BankId = BankId)
            End If
            Return _bankControltemplates
        End Get
    End Property

    Private ReadOnly Property ShouldHarmonize As Boolean
        Get
            Return _shouldHarmonize
        End Get
    End Property

    Private Sub ImportFormWizard_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        TabControlMain.Dispose()
        If _selectedImportHandler IsNot Nothing AndAlso TypeOf _selectedImportHandler Is IDisposable Then
            RemoveHandler _selectedImportHandler.StartProgress, AddressOf ImportHandler_StartImport
            RemoveHandler _selectedImportHandler.Progress, AddressOf ImportHandler_ImportProgress
            RemoveHandler _selectedImportHandler.ImportHandlerHandleConflict, AddressOf ImportHandler_HandleConflict
            RemoveHandler _selectedImportHandler.ImportHandlerHandleError, AddressOf ImportHandler_HandleError
            RemoveHandler _selectedImportHandler.ImportHandlerHandleWarning, AddressOf ImportHandler_HandleWarning
            DirectCast(_selectedImportHandler, IDisposable).Dispose()
        End If
    End Sub

    Private Sub ImportForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = My.Resources.ImportOfResources

        InitializeHandlersFromConfig()
    End Sub

    Private Sub ChangeImportMethod()
        _selectedImportHandler = DirectCast(ImportMethodCombo.SelectedItem, IImportHandler)

        If _selectedImportHandler IsNot Nothing Then

            OptionsGroupBox.Controls.Clear()

            _currentOptionControl = _selectedImportHandler.GetOptionsUserControl()
            OptionsGroupBox.Controls.Add(_currentOptionControl)
            _currentOptionControl.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub InitializeHandlersFromConfig()
        Dim fullImportAccess = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Import, TestBuilderPermissionTarget.Any, BankId)
        Dim importWithExcel As Boolean

        If Not fullImportAccess Then
            importWithExcel = PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ImportItemsWithExcelTemplate, BankId, 0)
        End If

        For Each ImportHandler As PluginHandlerElement In ConfigPluginHelper.GetListOfPluginHandlersBySectionName("importHandlers")
            Dim handlerInstance = TryCreateInstanceOfImportHandler(ImportHandler)
            If handlerInstance Is Nothing Then
                Continue For
            End If

            Dim isTypeSupported As Boolean = False

            AddHandler handlerInstance.StartProgress, AddressOf ImportHandler_StartImport
            AddHandler handlerInstance.Progress, AddressOf ImportHandler_ImportProgress
            AddHandler handlerInstance.ImportHandlerHandleConflict, AddressOf ImportHandler_HandleConflict
            AddHandler handlerInstance.ImportHandlerHandleError, AddressOf ImportHandler_HandleError
            AddHandler handlerInstance.ImportHandlerHandleWarning, AddressOf ImportHandler_HandleWarning

            If handlerInstance.SupportedResourceTypes.ToLower = "all" Then
                isTypeSupported = True
            ElseIf Not String.IsNullOrEmpty(handlerInstance.SupportedResourceTypes) AndAlso Me.Owner IsNot Nothing AndAlso TypeOf (Me.Owner) Is MainForm Then
                If fullImportAccess OrElse (importWithExcel AndAlso TypeOf handlerInstance Is ExcelImportHandler) Then
                    Dim mainForm As MainForm = DirectCast(Me.Owner, MainForm)
                    For Each supportedType As String In handlerInstance.SupportedResourceTypes.Split("|"c)
                        If supportedType.ToLower = mainForm.MainTabControl.SelectedTab.Tag.ToString.ToLower Then
                            isTypeSupported = True
                        End If
                    Next
                End If
            End If

            If isTypeSupported Then
                ImportMethodCombo.Items.Add(handlerInstance)
            End If
        Next

        If ImportMethodCombo.Items.Count = 1 Then
            ImportMethodCombo.SelectedIndex = 0
        End If
    End Sub

    Private Function TryCreateInstanceOfImportHandler(importHandler As PluginHandlerElement) As IImportHandler
        Try
            Return DirectCast(Activator.CreateInstance(Type.GetType(importHandler.Type, True)), IImportHandler)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub ImportMethodCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ImportMethodCombo.SelectedIndexChanged
        ChangeImportMethod()
    End Sub

    Private Sub ImportHandler_StartImport(ByVal sender As Object, ByVal e As StartEventArgs)
        With ProcessTabContent
            .ProgressMinimumValueDetail = 0
            .ProgressMaximumValueDetail = e.NumberOfResources
            .ProgressValueDetail = 0
        End With
    End Sub

    Private Sub ImportHandler_ImportProgress(ByVal sender As Object, ByVal e As ProgressEventArgs)
        If e.ProgessValue.HasValue Then
            ProcessTabContent.ProgressValueDetail = e.ProgessValue.Value
        Else
            ProcessTabContent.ProgressValueDetail += 1
        End If
        ProcessTabContent.ProcessInfoTextDetail = e.StatusMessage
        Application.DoEvents()
    End Sub

    Private Function OverwriteCurrentResource(ByVal e As ImportHandlerHandleConflictEventArgs) As Boolean
        Dim conflictResolution As ResourceConflictResolution = ResourceConflictResolution.LeaveCurrentInstance

        If e.BankIdExistingResource = e.BankContextId Then
            If _existingResourceConflictResolution <> ResourceConflictResolution.LeaveThisAndFollowingInstances AndAlso
              _existingResourceConflictResolution <> ResourceConflictResolution.ReplaceThisAndFollowingInstances Then

                Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(HandleImportConflictDialog))

                Using handleConflictDialog As New HandleImportConflictDialog(String.Format(resources.GetString("ResourceNameLabelText"), e.BankName, e.ResourceName), ResourceConflictResolution.LeaveCurrentInstance Or ResourceConflictResolution.LeaveThisAndFollowingInstances Or ResourceConflictResolution.ReplaceCurrentInstance Or ResourceConflictResolution.ReplaceThisAndFollowingInstances, My.Resources.HandleImportConflictResourceDialogWindowTitle)
                    handleConflictDialog.ShowDialog(Me)

                    _existingResourceConflictResolution = handleConflictDialog.ChoosenConflictResolution
                End Using
            End If

            conflictResolution = _existingResourceConflictResolution
        End If

        Return (conflictResolution = ResourceConflictResolution.ReplaceCurrentInstance OrElse
            conflictResolution = ResourceConflictResolution.ReplaceThisAndFollowingInstances)
    End Function

    Private Sub ImportHandler_HandleConflict(ByVal sender As Object, ByVal e As ImportHandlerHandleConflictEventArgs)

        e.Cancel = Not OverwriteCurrentResource(e)

        If e.Cancel Then
            _skippedResources.Add(e.ResourceName)
        End If
    End Sub

    Private Sub ImportHandler_HandleError(ByVal sender As Object, ByVal e As ImportExportHandlerHandleErrorEventArgs)
        _listOfErrors.Add(e.ErrorMessage)
    End Sub

    Private Sub ImportHandler_HandleWarning(ByVal sender As Object, ByVal e As ImportExportHandlerHandleWarningEventArgs)
        _listOfWarnings.Add(e.Message)
    End Sub

    Private Sub ForcesValidation()
        For Each c As Control In Me.Controls
            c.Focus()
        Next
    End Sub

    Private Sub ImportWizardForm_WizardBeforeProcess(ByVal sender As Object, ByVal e As WizardProcessEventArgs) Handles Me.WizardBeforeProcess
        If Not _selectedImportHandler.GetOptionsUserControl.Validate(False) Then
            e.Cancel = True
        Else
            If Not String.IsNullOrEmpty(_selectedImportHandler.GetOptionsUserControl.ErrorMessage) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub ImportWizardForm_WizardBeforeTabChange(ByVal sender As Object, ByVal e As WizardBeforeTabChangeEventArgs) Handles Me.WizardBeforeTabChange
        If _currentOptionControl IsNot Nothing Then _currentOptionControl.Focus()

        Select Case e.CurrentTab.Tag.ToString
            Case "WelcomeTab"
                If ImportMethodCombo.Items.Count = 1 Then
                    e.NextTab = TabImportOptions
                End If
            Case "ImportMethodeTab"
                e.Cancel = ImportMethodCombo.SelectedIndex = -1
            Case "ImportOptionsTab"
                ForcesValidation()
                If Not String.IsNullOrEmpty(_selectedImportHandler.GetOptionsUserControl.ErrorMessage) Then
                    e.Cancel = True
                    Return
                End If

                If e.CurrentTab.Tag.ToString = "ImportOptionsTab" Then
                    If _selectedImportHandler.ImportFileIsPackage Then
                        Try
                            If TypeOf (_selectedImportHandler.GetOptionsUserControl) Is PackageImportOptionsControl Then
                                Dim optionsControl As PackageImportOptionsControl = DirectCast(_selectedImportHandler.GetOptionsUserControl, PackageImportOptionsControl)
                                _manifest = LoadManifest(optionsControl.Options.Url)
                                If _manifest Is Nothing Then
                                    e.Cancel = True
                                    MessageBox.Show(String.Format(My.Resources.ImportFailure, vbCrLf),
                                              My.Resources.ImportFailureTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Return
                                End If
                                e.NextTab = OverviewTabPageControl
                            End If

                        Catch resEx As ResourceException
                            If TypeOf (_selectedImportHandler) Is PackageImportHandler Then
                                DirectCast(_selectedImportHandler, PackageImportHandler).HasError = True
                            End If
                            ResultTabContent.ResultText = resEx.Message
                            e.NextTab = ResultTabPageControl

                        Catch ioEx As IOException
                            ResultTabContent.ResultText = ioEx.Message
                            e.NextTab = ResultTabPageControl

                        End Try
                    Else
                        e.NextTab = OverviewTabPageControl
                    End If
                End If
        End Select
    End Sub

    Private Function LoadManifest(ByVal packageUrl As String) As ResourceManifest
        Dim manifest = New ResourceManifest()

        If packageUrl.EndsWith(".exportset", StringComparison.CurrentCultureIgnoreCase) OrElse
             packageUrl.EndsWith(".packageset", StringComparison.CurrentCultureIgnoreCase) Then
            Dim packageSet As PackageSet = PackageSet.LoadFromFile(packageUrl)
            If (packageSet.PackageSetEntryCollection.Count > 0) Then
                Dim firstPackage As PackageSetEntry = packageSet.PackageSetEntryCollection.Item(0)
                Dim firstPackagePath As String = Path.Combine(Path.GetDirectoryName(packageUrl), firstPackage.PackageUri)
                manifest = ResourceManifest.PreLoad(New Uri(Path.Combine(firstPackagePath, "Manifest.xml")))
            End If
        Else
            manifest = ResourceManifest.PreLoad(New Uri(Path.Combine(packageUrl, "Manifest.xml")))
        End If

        Return manifest
    End Function

    Private Sub ImportWizardForm_WizardCompleted(ByVal sender As Object, ByVal e As EventArgs) Handles Me.WizardCompleted
        If String.IsNullOrEmpty(_selectedImportHandler.GetOptionsUserControl.ErrorMessage) AndAlso _listOfErrors.Count = 0 AndAlso Not _harmonizationCancelled Then
            Dim resultText As New StringBuilder
            If _skippedResources.Count > 0 Then
                resultText.AppendLine(My.Resources.TheFollowingResourcesWereSkippedBecauseTheyAlreadyExistedWithinDestinationBankHierarchy)
                resultText.AppendLine("")

                _skippedResources.Sort()
                For Each resourceName As String In _skippedResources
                    resultText.AppendLine(resourceName)
                Next
            End If

            If _itemsWithRemovedCustomProperties.Count > 0 Then
                If Not String.IsNullOrEmpty(resultText.ToString) Then resultText.AppendLine()
                resultText.AppendLine(String.Format(My.Resources.CustomBankPropertiesRemovedFromItem, _itemsWithRemovedCustomProperties.Count))
                resultText.AppendLine("")
                _itemsWithRemovedCustomProperties.Sort()
                For Each resourceName As String In _itemsWithRemovedCustomProperties
                    resultText.AppendLine(resourceName)
                Next
            End If

            If _listOfWarnings.Count > 0 Then
                If Not String.IsNullOrEmpty(resultText.ToString) Then resultText.AppendLine()
                _listOfWarnings.ForEach(Sub(s) resultText.AppendLine(s))
            End If

            If Not String.IsNullOrEmpty(resultText.ToString()) Then
                ResultTabContent.ResultText = resultText.ToString()
            End If
        ElseIf _harmonizationCancelled Then
            ResultTabContent.Task = My.Resources.OperationCancelled
            ResultTabContent.ResultText = My.Resources.HarmonizationCancelledMessage
        Else
            ResultTabContent.Task = My.Resources.OperationUnSuccesfull
            ResultTabContent.TaskDescription = My.Resources.ErrorImporting

            Dim errorMessageForResultTab As New StringBuilder(String.Empty)
            For Each errorMessage As String In _listOfErrors
                errorMessageForResultTab.AppendLine(errorMessage)
            Next
            If Not String.IsNullOrEmpty(_selectedImportHandler.GetOptionsUserControl.ErrorMessage) Then
                errorMessageForResultTab.AppendLine(_selectedImportHandler.GetOptionsUserControl.ErrorMessage)
            End If
            ResultTabContent.ResultText = errorMessageForResultTab.ToString
        End If
    End Sub

    Private Sub ImportWizardForm_WizardDoProcess(ByVal sender As Object, ByVal e As WizardProcessEventArgs) Handles Me.WizardDoProcess
        If TypeOf _selectedImportHandler Is IImportHandlerQuestifyExportFiles Then
            Dim selectedImportHandlerForQuestifyExportFiles = CType(_selectedImportHandler, IImportHandlerQuestifyExportFiles)
            If _manifest.Resources.Any(Function(r) r.Type.Equals("ItemLayoutTemplateResourceEntity", StringComparison.OrdinalIgnoreCase)) OrElse
                _manifest.Resources.Any(Function(r) r.Type.Equals("ControlTemplateResourceEntity", StringComparison.OrdinalIgnoreCase)) Then

                StoreCurrentParameterSetFromItemTemplateForHarmonizationDetermination(Me.BankId, _manifest.Resources)
            End If
            Dim optionsControl As PackageImportOptionsControl = DirectCast(_selectedImportHandler.GetOptionsUserControl, PackageImportOptionsControl)
            If optionsControl.Options.Url.EndsWith(".export", StringComparison.CurrentCultureIgnoreCase) OrElse
optionsControl.Options.Url.EndsWith(".package", StringComparison.CurrentCultureIgnoreCase) Then
                ImportHandler_ImportProgress(Nothing, New ProgressEventArgs(My.Resources.Import_Package))
                Dim packageUri = New Uri($"{optionsControl.Options.Url}/")

                Try
                    Using resManager As New ManifestResourceManager(_manifest, Nothing, packageUri, Guid.NewGuid.ToString())

                        Dim manifestMetaData As ResourceMetaDataManifest = ResourceMetaDataManifest.Load(resManager, New Uri(packageUri, "ManifestMetaData.xml"))

                        resManager.ManifestMetaData = manifestMetaData

                        ImportHandler_ImportProgress(Nothing, New ProgressEventArgs(My.Resources.Import_Package_StartImport))
                        Dim result As Boolean = _selectedImportHandler.Import(resManager, Me.BankId)
                        If Not _listOfErrors.Count = 0 Then
                            e.Cancel = False
                        Else
                            e.Cancel = Not result
                        End If
                        PackageManager.RemovePackageFromCache(packageUri)
                    End Using
                Catch ex As Exception
                    e.Cancel = True
                End Try

            ElseIf optionsControl.Options.Url.EndsWith(".exportset", StringComparison.CurrentCultureIgnoreCase) OrElse
optionsControl.Options.Url.EndsWith(".packageset", StringComparison.CurrentCultureIgnoreCase) Then
                ImportHandler_ImportProgress(Nothing, New ProgressEventArgs(My.Resources.Import_PackageSet))
                Dim packageSet As PackageSet = PackageSet.LoadFromFile(optionsControl.Options.Url)
                Dim rootBank As Integer?

                If optionsControl.Options.ImportToRoot = False Then
                    rootBank = Me.BankId
                End If
                ImportHandler_ImportProgress(Nothing, New ProgressEventArgs(My.Resources.Import_PackageSet_StartImport))
                e.Cancel = Not _selectedImportHandler.Import(packageSet, rootBank)
            End If
            If Not e.Cancel AndAlso ShouldHarmonize Then
                Try
                    ImportHandler_ImportProgress(Nothing, New ProgressEventArgs(My.Resources.ImportHarmonisationDetermineIltsToHarmonize))
                    Dim templatesToHarmonize = GetTemplatesIdsToHarmonize(BankId).ToList
                    Dim itemsToHarmonize = GetItemsFromResources(_manifest.Resources).Where(Function(i) i IsNot Nothing AndAlso Not _skippedResources.Contains(i))
                    Dim shouldHarmonizeItems = itemsToHarmonize IsNot Nothing AndAlso itemsToHarmonize.Count > 0
                    Dim shouldHarmonizeTemplates = templatesToHarmonize IsNot Nothing AndAlso templatesToHarmonize.Count > 0
                    Dim shouldHarmonize As Boolean = shouldHarmonizeItems OrElse shouldHarmonizeTemplates

                    If shouldHarmonize Then
                        _harmonizationIsBusy = True
                        AddHandler selectedImportHandlerForQuestifyExportFiles.HarmonizeCompleted, AddressOf ImportHandler_HarmonizeCompleted
                        SetASyncProcessing(True)
                        SupportsCancelation = True

                        selectedImportHandlerForQuestifyExportFiles.HarmonizeAfterImport(BankId, templatesToHarmonize, itemsToHarmonize)
                    End If
                Catch ex As Exception
                    ImportHandler_HandleError(Me, New ImportExportHandlerHandleErrorEventArgs(ex.Message))
                End Try
            End If
        Else
            _selectedImportHandler.Import(Me.BankId)
        End If

        MainForm.MainBankBrowser.StartBanksRefresh()
    End Sub

    Private Sub ImportHandler_HarmonizeCompleted(result As BuilderTaskResult)
        If (result?.Warnings.Any()) Then
            _listOfWarnings.AddRange(result.Warnings)
        End If

        If (result?.Exceptions.Any()) Then
            _listOfErrors.AddRange(result.Exceptions)
        End If

        If (result?.Errors.Any()) Then
            _listOfErrors.AddRange(result.Errors)
        End If

        Me.SelectTab(ResultTabPageControl)
        MainForm.MainBankBrowser.StartBanksRefresh()

        Dim questifyImportHandler = TryCast(_selectedImportHandler, IImportHandlerQuestifyExportFiles)
        If (questifyImportHandler IsNot Nothing) Then
            RemoveHandler questifyImportHandler.HarmonizeCompleted, AddressOf ImportHandler_HarmonizeCompleted
        End If
    End Sub

    Private Sub ImportFormWizard_WizardCancel(sender As Object, e As WizardCancelEventArgs) Handles MyBase.WizardCancel
        If _harmonizationIsBusy Then
            Dim selectedImportHandlerForQuestifyExportFiles As IImportHandlerQuestifyExportFiles = TryCast(_selectedImportHandler, IImportHandlerQuestifyExportFiles)
            If (selectedImportHandlerForQuestifyExportFiles IsNot Nothing) Then
                selectedImportHandlerForQuestifyExportFiles.CancelHarmonization()
                _harmonizationCancelled = True
                ImportHandler_ImportProgress(Nothing, New ProgressEventArgs(My.Resources.OperationCancelling))
                e.Cancel = False
            End If
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub ImportWizardForm_WizardTabChanged(ByVal sender As Object, ByVal e As WizardTabChangedEventArgs) Handles Me.WizardTabChanged
        Select Case e.CurrentTab.Tag.ToString

            Case "WelcomeTab"
                WelcomeTabContent.Title = My.Resources.Import
                WelcomeTabContent.Description = My.Resources.ThisWizardHelpsYouToImportResources

            Case "OverviewTab"
                OverviewTabContent.Task = My.Resources.CompleteTheWizard
                OverviewTabContent.TaskDescription = My.Resources.VerifyTheChoicesMadeInTheWizard

                If TypeOf (_selectedImportHandler.GetOptionsUserControl) Is PackageImportOptionsControl Then
                    Dim optionsControl As PackageImportOptionsControl = DirectCast(_selectedImportHandler.GetOptionsUserControl, PackageImportOptionsControl)
                    OverviewTabContent.OverviewText = String.Format(My.Resources.ResourceToImportDestinationCurrentBank, New Object() {optionsControl.Options.Url.ToString,
                                              vbCrLf, ImportMethodCombo.Text, "n.a."})
                ElseIf TypeOf (_selectedImportHandler.GetOptionsUserControl) Is ExcelImportOptionsControl Then
                    Dim optionsControl As ExcelImportOptionsControl = DirectCast(_selectedImportHandler.GetOptionsUserControl, ExcelImportOptionsControl)
                    OverviewTabContent.OverviewText = String.Format(My.Resources.ResourceToImportDestinationCurrentBankExcel, optionsControl.Options.ExcelUrl.ToString, vbCrLf, ImportMethodCombo.Text)
                End If
            Case "ProcessTab"
                Me.CancelBtn.DialogResult = DialogResult.None
                ProcessTabContent.Task = My.Resources.PerformingOperation
                ProcessTabContent.TaskDescription = String.Empty
            Case "ResultTab"
                If TypeOf (_selectedImportHandler.GetOptionsUserControl) Is PackageImportOptionsControl Then
                    Dim packageImportHandler As PackageImportHandler = DirectCast(_selectedImportHandler, PackageImportHandler)
                    Dim optionsControl As PackageImportOptionsControl = DirectCast(_selectedImportHandler.GetOptionsUserControl, PackageImportOptionsControl)
                    If (packageImportHandler.HasError) Then
                        ResultTabContent.Task = My.Resources.OperationUnSuccesfull
                        ResultTabContent.TaskDescription = My.Resources.ImportFailureGeneral
                    Else
                        ResultTabContent.Task = My.Resources.OperationSuccesfull
                        ResultTabContent.TaskDescription = String.Format(My.Resources.SuccesfulyImportedToCurrentBank, optionsControl.Options.Url)
                        ResultTabContent.ResultText = String.Empty
                    End If
                ElseIf TypeOf (_selectedImportHandler.GetOptionsUserControl) Is ExcelImportOptionsControl Then
                    Dim excelImportHandler As ExcelImportHandler = DirectCast(_selectedImportHandler, ExcelImportHandler)
                    Dim optionsControl As ExcelImportOptionsControl = DirectCast(_selectedImportHandler.GetOptionsUserControl, ExcelImportOptionsControl)
                    If (excelImportHandler.HasError) Then
                        ResultTabContent.Task = My.Resources.OperationUnSuccesfull
                        ResultTabContent.TaskDescription = My.Resources.ImportFailureGeneral
                    Else
                        ResultTabContent.Task = My.Resources.OperationSuccesfull
                        ResultTabContent.TaskDescription = String.Format(My.Resources.SuccesfulyImportedToCurrentBank, optionsControl.Options.Url)
                    End If
                    ResultTabContent.ResultText = excelImportHandler.ImportResults
                End If
            Case "ImportMethodeTab"
                If ImportMethodCombo.Items.Count <> 1 Then
                    ImportMethodeTabContent.Task = My.Resources.ChooseAImportMethod
                    ImportMethodeTabContent.TaskDescription = String.Empty
                End If
            Case "ImportOptionsTab"
                ImportOptionsTabContent.Task = My.Resources.FillTheImportMethodOptions
                ImportOptionsTabContent.TaskDescription = String.Empty
                _currentOptionControl.Focus()
        End Select
    End Sub

    Private Function GetItemsFromResources(resources As ResourceEntryCollection) As List(Of String)
        Return resources.Select(Function(r)
                                    If r.Type.Equals("ItemResourceEntity", StringComparison.OrdinalIgnoreCase) Then
                                        Return r.Name
                                    End If
                                    Return Nothing
                                End Function).ToList
    End Function

    Private Sub StoreCurrentParameterSetFromItemTemplateForHarmonizationDetermination(bankId As Integer, resources As ResourceEntryCollection)
        Dim iltsToHarmonize = resources.Where(Function(r) r.Type.Equals("ItemLayoutTemplateResourceEntity", StringComparison.OrdinalIgnoreCase)).Select(Function(ilt) ilt.Name).ToList()
        Dim importedControlTemplates = resources.Where(Function(r) r.Type.Equals("ControlTemplateResourceEntity", StringComparison.OrdinalIgnoreCase)).Select(Function(ict) ict.Name).ToList()

        If iltsToHarmonize IsNot Nothing AndAlso iltsToHarmonize.Count > 0 Then
            Dim iltsInBank = ResourceFactory.Instance.GetItemLayoutTemplatesForBankList(bankId, True)
            If iltsInBank.Count > 0 Then _shouldHarmonize = True
            Dim iltsToRemove = iltsInBank.Where(Function(i) i.Value <> bankId).Select(Function(i) i.Key)
            If iltsToRemove IsNot Nothing AndAlso iltsToRemove.Count > 0 Then
                iltsToHarmonize = iltsToHarmonize.Except(iltsToRemove.ToList()).ToList()
            End If
        End If
        If importedControlTemplates IsNot Nothing AndAlso importedControlTemplates.Count > 0 Then
            Dim ctsInBank = ResourceFactory.Instance.GetControlTemplatesForBankList(bankId, True)
            If ctsInBank.Count > 0 Then _shouldHarmonize = True
            Dim ctsToRemove = ctsInBank.Where(Function(i) i.Value <> bankId).Select(Function(i) i.Key)
            If ctsToRemove IsNot Nothing AndAlso ctsToRemove.Count > 0 Then
                importedControlTemplates = importedControlTemplates.Except(ctsToRemove.ToList()).ToList()
            End If
        End If

        For Each bankTemplate In BankControlTemplates
            If importedControlTemplates.Contains(bankTemplate.Name) Then
                For Each referencedResource In bankTemplate.ReferencedResourceCollection
                    If BankItemLayoutTemplates.Select(Function(it) it.ResourceId).Contains(referencedResource.ResourceId) Then
                        Dim refIltName = BankItemLayoutTemplates.FirstOrDefault(Function(b) b.ResourceId.Equals(referencedResource.ResourceId)).Name
                        If Not iltsToHarmonize.Any(Function(ilt) ilt.Equals(refIltName, StringComparison.InvariantCultureIgnoreCase)) Then iltsToHarmonize.Add(refIltName)
                    End If

                Next
            End If
        Next

        Dim ilts = DetermineIltsAndTheControlTemplatesTheyUse(bankId, iltsToHarmonize)
        If ilts IsNot Nothing Then
            Dim iltsToHarmonizeParameters = GetParameterSets(ilts)
            For Each ilt In iltsToHarmonizeParameters.Keys
                If Not _itemTemplateParameters.ContainsKey(ilt) Then _itemTemplateParameters.Add(ilt, iltsToHarmonizeParameters(ilt).Value)
            Next
        End If
    End Sub

    Private Function DetermineIltsAndTheControlTemplatesTheyUse(bankId As Integer, iltNames As List(Of String)) As EntityCollection
        If _controlTemplatesAndIltsThatUseThem.Count = 0 Then
            Dim ilts = ResourceFactory.Instance.GetItemLayoutTemplatesForBankWithListOfNamesFilter(bankId, iltNames)
            DetermineIltsAndTheControlTemplatesTheyUse(ilts)
            Return ilts
        End If
        Return Nothing
    End Function

    Private Sub DetermineIltsAndTheControlTemplatesTheyUse(ilts As EntityCollection)
        If ilts Is Nothing Then
            Return
        End If

        For Each ilt In ilts
            Dim template = TryCast(ilt, ItemLayoutTemplateResourceEntity)
            If template IsNot Nothing Then
                Dim adapter = New ItemLayoutAdapter(template.Name, Nothing, GetResourceNeededHandler(template))
                Dim listOfControlTemplatesAndIds = adapter.GetListOfControlTemplatesAndIds()
                If Not _iltsWithControlTemplatesAndIds.ContainsKey(template.Name) Then _iltsWithControlTemplatesAndIds.Add(template.Name, listOfControlTemplatesAndIds)
                For Each kvp In listOfControlTemplatesAndIds
                    Dim lstOfIltsPerCtrlTemplate As New List(Of Guid)
                    If _controlTemplatesAndIltsThatUseThem.ContainsKey(kvp.Key) Then
                        lstOfIltsPerCtrlTemplate = _controlTemplatesAndIltsThatUseThem(kvp.Key)
                        lstOfIltsPerCtrlTemplate.Add(template.ResourceId)
                        _controlTemplatesAndIltsThatUseThem(kvp.Key) = lstOfIltsPerCtrlTemplate
                    Else
                        lstOfIltsPerCtrlTemplate.Add(template.ResourceId)
                        _controlTemplatesAndIltsThatUseThem.Add(kvp.Key, lstOfIltsPerCtrlTemplate)
                    End If
                Next
            End If
        Next
    End Sub

    Private Function GetParameterSets(bankId As Integer, templates As IEnumerable(Of String)) As Dictionary(Of String, KeyValuePair(Of Guid, ParameterSetCollection))
        Dim itemTemplateParameters As New Dictionary(Of String, KeyValuePair(Of Guid, ParameterSetCollection))
        Dim templateResources = ResourceFactory.Instance.GetResourcesByNamesWithOption(bankId, templates.ToList(), New ResourceRequestDTO())
        For Each ilt In templateResources
            Dim template = TryCast(ilt, ItemLayoutTemplateResourceEntity)
            If template IsNot Nothing Then
                AddParameterSets(itemTemplateParameters, template)
            End If
        Next
        Return itemTemplateParameters
    End Function

    Private Function GetParameterSets(templates As EntityCollection) As Dictionary(Of String, KeyValuePair(Of Guid, ParameterSetCollection))
        Dim itemTemplateParameters As New Dictionary(Of String, KeyValuePair(Of Guid, ParameterSetCollection))
        For Each ilt In templates
            Dim template = TryCast(ilt, ItemLayoutTemplateResourceEntity)
            If template IsNot Nothing Then
                ImportHandler_ImportProgress(Nothing, New ProgressEventArgs(String.Format(My.Resources.ImportHarmonisationDetermineParameterSetForILT, template.Name)))
                AddParameterSets(itemTemplateParameters, template)
            End If
        Next
        Return itemTemplateParameters
    End Function

    Private Sub AddParameterSets(ByRef itemTemplateParameters As Dictionary(Of String, KeyValuePair(Of Guid, ParameterSetCollection)), template As ItemLayoutTemplateResourceEntity)
        Dim adapter = New ItemLayoutAdapter(template.Name, Nothing, GetResourceNeededHandler(template))
        Dim newParameterSets As New ParameterSetCollection

        Try
            newParameterSets = ParameterSetCollection.DeepClone(adapter.CreateParameterSetsFromItemTemplate())
        Catch ex As TesterException
        Catch ex As Exception
            Throw
        End Try

        itemTemplateParameters.Add(template.Name, New KeyValuePair(Of Guid, ParameterSetCollection)(template.ResourceId, newParameterSets))
    End Sub

    Private Function GetTemplatesIdsToHarmonize(bankId As Integer) As IEnumerable(Of Guid)
        Dim newParameterSet = GetParameterSets(bankId, _itemTemplateParameters.Keys.Where(Function(t) Not _skippedResources.Contains(t)))
        Dim result As New List(Of Guid)
        result = (From templateName In newParameterSet.Keys Where Not newParameterSet(templateName).Value.AreEqual(_itemTemplateParameters(templateName)) Select newParameterSet(templateName).Key).ToList()

        If result.Count > 0 Then

            For Each ilt In (From templateName In newParameterSet.Keys Where result.Contains(newParameterSet(templateName).Key) Select templateName)
                For Each coll In newParameterSet(ilt).Value
                    If _itemTemplateParameters.ContainsKey(ilt) AndAlso _itemTemplateParameters(ilt).Any(Function(ps) ps.Id.Equals(coll.Id, StringComparison.InvariantCultureIgnoreCase)) AndAlso Not ParametersAreEqual(_itemTemplateParameters(ilt).Where(Function(ps) ps.Id.Equals(coll.Id, StringComparison.InvariantCultureIgnoreCase)).First, coll) Then
                        Dim ctrlTemplate = _iltsWithControlTemplatesAndIds(ilt).FirstOrDefault(Function(id) id.Value.Any(Function(s) s.Equals(coll.Id, StringComparison.InvariantCultureIgnoreCase)))
                        If Not String.IsNullOrEmpty(ctrlTemplate.Key) Then
                            For Each iltemplate In _controlTemplatesAndIltsThatUseThem(ctrlTemplate.Key)
                                If Not result.Contains(iltemplate) Then result.Add(iltemplate)
                            Next
                        End If
                    End If
                Next
            Next
        End If
        Return result
    End Function

    Private Function GetResourceNeededHandler(template As ItemLayoutTemplateResourceEntity) As EventHandler(Of ResourceNeededEventArgs)
        If template.Bank Is Nothing Then
            If Not _bankCache.ContainsKey(template.BankId) Then
                _bankCache.Add(template.BankId, BankFactory.Instance.GetBankWithOptions(template.BankId, False, True))
            End If
            template.Bank = _bankCache(template.BankId)
        End If
        If Not _bankResourceManagerCache.ContainsKey(template.BankId) Then
            _bankResourceManagerCache.Add(template.BankId, New ResourceManagerHolder(template.Bank))
        End If
        Return _bankResourceManagerCache(template.BankId).ResourceNeeded
    End Function

    Private Function ParametersAreEqual(parameterColl1 As ParameterCollection, parameterColl2 As ParameterCollection) As Boolean
        Dim paramSetColl1 As New ParameterSetCollection()
        paramSetColl1.Add(parameterColl1)
        Dim paramSetColl2 As New ParameterSetCollection()
        paramSetColl2.Add(parameterColl2)
        Return paramSetColl1.AreEqual(paramSetColl2)
    End Function

    Private Sub CustomPropertyRemoved(ByVal sender As Object, ByVal e As ImportCustomBankPropertiesRemovedArgs) Handles _selectedImportHandler.ImportHandlerCustomBankPropertiesRemoved
        If Not _itemsWithRemovedCustomProperties.Contains(e.ResourceName) Then
            _itemsWithRemovedCustomProperties.Add(e.ResourceName)
        End If
    End Sub
End Class
