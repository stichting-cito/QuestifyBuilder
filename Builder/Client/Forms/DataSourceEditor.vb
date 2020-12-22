Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Text
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Commanding
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class DataSourceEditor


    Private _dataSourceResourceEntity As DataSourceResourceEntity
    Private _dataSourceSettings As New List(Of DataSourceSettings)
    Private _forceClose As Boolean
    Private _originalHash As Byte()
    Private _resourceManager As DataBaseResourceManager
    Private _errors As List(Of String) = New List(Of String)()
    Private ReadOnly _windowFacade As IWindowFacade = New WindowFacade()



    Public Sub New(ByVal dataSourceResource As DataSourceResourceEntity)
        MyClass.New()
        If dataSourceResource Is Nothing Then
            Throw New ArgumentNullException("dataSourceResource")
        End If

        _dataSourceResourceEntity = dataSourceResource
        _resourceManager = New DataBaseResourceManager(_dataSourceResourceEntity.BankId)
    End Sub

    Private Sub New()
        InitializeComponent()

        AddHandler ResourceIdentifierEditorInstance.ChangeCode, AddressOf ResourceIdentifierEditorInstance_ChangeCode
    End Sub



    Protected ReadOnly Property IsDirty() As Boolean
        Get
            Return (ResourceDataIsDirty OrElse MetaDataControl.IsDirty() OrElse _dataSourceResourceEntity.HasChangesInTopology OrElse ResourceCustomProperties1.RemovedEntities.Count > 0)
        End Get
    End Property

    Protected ReadOnly Property ResourceDataIsDirty() As Boolean
        Get
            Dim currentEntityHash As Byte() = If(_dataSourceSettings(0) IsNot Nothing, _dataSourceSettings(0).GetMD5Hash(), Nothing)
            Return Not ArrayHelper.CompareByteArray(_originalHash, currentEntityHash)
        End Get
    End Property



    Public ReadOnly Property DataSourceResourceEntity As DataSourceResourceEntity
        Get
            Return _dataSourceResourceEntity
        End Get
    End Property



    Private Sub ResourceIdentifierEditorInstance_ChangeCode(ByVal sender As Object, ByVal e As EventArgs)
        If PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ChangeItemCode, _dataSourceResourceEntity.BankId, 0) Then
            If Not Me.IsDirty Then
                Dim referencedResources As EntityCollection = ResourceFactory.Instance.GetReferencesForResource(_dataSourceResourceEntity)

                Dim dlg As New ChangeDataSourceCodeDialog(_dataSourceResourceEntity.Name, referencedResources.Count)
                AddHandler dlg.ValidateNewCodeName, AddressOf ChangeCodeDialog_ValidateNewCodeName
                Dim result As DialogResult = dlg.ShowDialog()

                If result = DialogResult.OK Then
                    _dataSourceResourceEntity.Name = dlg.NewCodeName
                    If ValidateItem(True) Then
                        Try
                            If Me.Save() Then
                            End If
                        Catch ex As ORMException
                            Throw New AppLogicException(My.Resources.ItemEditor_AnErrorOccurredWhileUpdatingTheCodeNameInTests, ex)
                        End Try
                    End If
                End If
            Else
                MessageBox.Show(String.Format(My.Resources.ItemEditor_OnlyPossibleToChangeTheResourceCodeWhenNoOtherChanges, My.Resources.Item), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show(My.Resources.ItemEditor_NoSufficientRightsToPerformThisAction, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub ChangeCodeDialog_ValidateNewCodeName(ByVal sender As Object, ByVal e As ValidateNewCodeNameEventArgs)
        e.Valid = Not ResourceFactory.Instance.ResourceExists(_dataSourceResourceEntity.BankId, e.NewCodeName, True)
        If Not e.Valid Then
            MessageBox.Show(My.Resources.ItemEditor_ItemCodeNameAlreadyExistsInBankHierarchy, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub BindEditorControls(ByVal resetModelHash As Boolean)
        If resetModelHash Then
            _originalHash = _dataSourceSettings(0).GetMD5Hash()
        End If

        ResourceIdentifierEditorInstance.ResourceEntity = _dataSourceResourceEntity
        ResourceIdentifierEditorInstance.ToggleCodeField(_dataSourceResourceEntity.IsNew)
        MetaDataControl.ResourceEntity = _dataSourceResourceEntity
        ResourceCustomProperties1.ResourceEntity = _dataSourceResourceEntity

        DataSourceSettingsEditorInstance.Initialize(IDataSourceSettingsDesignerFactory.DesignerMode.Config, _dataSourceSettings, _resourceManager)
        DataSourcePreviewInstance.Initialize(_dataSourceSettings, _resourceManager)

        If DataSourceSettingsEditorInstance.EmbeddedEditors.Count = 1 Then
            CreateMenusForEditor(DataSourceSettingsEditorInstance.EmbeddedEditors(0))
        Else
            SelectionToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub CreateMenusForEditor(ByVal configEditor As DataSourceUIControlBase)

        Dim relatedPane = Me.TabPage3

        SelectionToolStripMenuItem.Tag = relatedPane

        For Each command As ICommand In configEditor.Commands
            If DataSourceEditorToolStrip.Items(command.Name) IsNot Nothing Then
                DataSourceEditorToolStrip.Items.RemoveByKey(command.Name)
            End If
            Dim toolStripItem As New ToolStripButton(command.Name, Nothing, Nothing, command.Name)
            toolStripItem.Tag = relatedPane
            DataSourceEditorToolStrip.Items.Add(toolStripItem)
            If configEditor.Parameters.ContainsKey(command.Name) Then
                CommandManager1.Bind(command, configEditor.Parameters(command.Name), toolStripItem)
            Else
                CommandManager1.Bind(command, toolStripItem)
            End If

            Dim mnuCommand As String = $"mnu_{command.Name}"
            If SelectionToolStripMenuItem.DropDownItems(mnuCommand) IsNot Nothing Then
                SelectionToolStripMenuItem.DropDownItems.RemoveByKey(mnuCommand)
            End If
            Dim menuItem As New ToolStripButton(command.Name, Nothing, Nothing, mnuCommand)
            menuItem.Tag = relatedPane
            SelectionToolStripMenuItem.DropDownItems.Add(menuItem)
            CommandManager1.Bind(command, menuItem)
        Next
        SelectionToolStripMenuItem.Visible = SelectionToolStripMenuItem.HasDropDownItems
    End Sub

    Private Sub BindTitleBar(ByVal rebind As Boolean)
        Dim titleBarBinding As Binding = Me.DataBindings("text")

        If rebind AndAlso titleBarBinding IsNot Nothing Then
            Me.DataBindings.Remove(titleBarBinding)
            titleBarBinding = Nothing
        End If

        If titleBarBinding Is Nothing Then
            titleBarBinding = New Binding("text", _dataSourceResourceEntity, "name")
            AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
            Me.DataBindings.Add(titleBarBinding)
        End If
    End Sub

    Private Function CopyCurrent(Of T As {ResourceEntity, New})(ByVal sourceEntity As T, ByVal newCode As String) As T
        Dim newResourceEntity As T = New T

        With newResourceEntity
            .Fields = sourceEntity.Fields.CloneAsDirty
            .ResourceId = Guid.NewGuid
            .Name = newCode
            .IsNew = True

            .Bank = sourceEntity.Bank

            .Version = "0.1"
            .OriginalName = sourceEntity.Name
            .OriginalVersion = sourceEntity.Version

            For Each dependency As DependentResourceEntity In sourceEntity.DependentResourceCollection
                Dim tempDependency As DependentResourceEntity
                tempDependency = .DependentResourceCollection.AddNew()

                With tempDependency
                    .Resource = newResourceEntity
                    .DependentResource = dependency.DependentResource
                End With
            Next

            For Each customProperty As CustomBankPropertyValueEntity In sourceEntity.CustomBankPropertyValueCollection
                If TypeOf customProperty Is FreeValueCustomBankPropertyValueEntity Then
                    Dim srcProperty As FreeValueCustomBankPropertyValueEntity = DirectCast(customProperty, FreeValueCustomBankPropertyValueEntity)
                    Dim dstProperty As New FreeValueCustomBankPropertyValueEntity(.ResourceId, srcProperty.CustomBankPropertyId)

                    dstProperty.Value = srcProperty.Value

                    .CustomBankPropertyValueCollection.Add(dstProperty)

                ElseIf TypeOf customProperty Is ListCustomBankPropertyValueEntity Then
                    Dim srcProperty As ListCustomBankPropertyValueEntity = DirectCast(customProperty, ListCustomBankPropertyValueEntity)
                    Dim dstProperty As New ListCustomBankPropertyValueEntity(.ResourceId, srcProperty.CustomBankPropertyId)
                    Dim selectedValue As ListCustomBankPropertySelectedValueEntity

                    For Each selectedValue In srcProperty.ListCustomBankPropertySelectedValueCollection
                        Dim newSelectedValue As New ListCustomBankPropertySelectedValueEntity(
                         .ResourceId, customProperty.CustomBankPropertyId, selectedValue.ListValueBankCustomPropertyId)

                        dstProperty.ListCustomBankPropertySelectedValueCollection.Add(newSelectedValue)
                    Next

                    .CustomBankPropertyValueCollection.Add(dstProperty)
                End If
            Next
        End With

        Return newResourceEntity
    End Function

    Private Sub DataSourceEditor_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed

        _dataSourceSettings = Nothing
        _dataSourceResourceEntity = Nothing
        _resourceManager.Dispose()
        _resourceManager = Nothing
    End Sub

    Private Sub DataSourceEditor_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If (Not _forceClose) Then
            EnsuresFormUpdateAfterEditing()

            If Me.IsDirty Then
                If DataSourceSettingsEditorInstance.ValidateAllEditors() Then
                    If Not Me.Save(True) Then
                        Dim message = DataSourceSettingsEditorInstance.GetErrorMessage
                        If Not String.IsNullOrEmpty(message) Then
                            MessageBox.Show(message, My.Resources.DataSourceEditor_DialogTitle)
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = False
                    End If
                Else
                    If MessageBox.Show(My.Resources.DataSourceEditor_ValidationErrorsOnExit, My.Resources.DataSourceEditor_DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        e.Cancel = True
                    End If
                End If
            Else
                e.Cancel = False
            End If
        End If
    End Sub

    Private Sub DataSourceEditor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If _resourceManager Is Nothing Then _resourceManager = New DataBaseResourceManager(_dataSourceResourceEntity.BankId)

            If Not _dataSourceResourceEntity.IsNew Then
                _dataSourceResourceEntity = ResourceFactory.Instance.GetDataSource(_dataSourceResourceEntity)
            End If

            _dataSourceSettings.Add(Parsers.ParseItemDataSourceSettingsFromResourceEntity(_dataSourceResourceEntity))

            If SaveAsToolStripButton.Enabled Then
                SaveAsToolStripButton.Enabled = Not _dataSourceResourceEntity.IsNew
                SaveAsToolStripMenuItem.Enabled = Not _dataSourceResourceEntity.IsNew
            End If

            BindTitleBar(False)
            BindEditorControls(True)

            HideUnusedTabs()
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.ValidateParametersWhenOpeningEditorThisEditorCannotBeOpened, Environment.NewLine, FormatErrorMessage(ex)), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            _forceClose = True
            Me.Close()
        End Try
    End Sub

    Private Function FormatErrorMessage(ByVal exception As Exception) As String
        Dim result As StringBuilder = New StringBuilder()

        result.Append(exception.Message)

        If (exception.InnerException IsNot Nothing) Then
            result.Append(vbNewLine)
            result.Append(FormatErrorMessage(exception.InnerException))
        End If

        Return result.ToString()
    End Function

    Private Sub HideUnusedTabs()
        If _dataSourceSettings.Count = 0 Then
            Throw New ArgumentException("Datasource collection may never be empty!")
        End If

        Dim datasource = _dataSourceSettings(0).CreateGetDataSource()
        Dim ds = TryCast(datasource, DataSource(Of ResourceRef, ItemDataSourceConfig))

        If ds Is Nothing Then Return

        If Not GetType(IDataSourceReporting(Of ItemDataSourceConfig)).IsAssignableFrom(ds.GetType()) Then
            TabControl2.TabPages.Remove(TabPage5)
        End If

        If ds.ShowPreviewControl = False Then
            TabControl2.TabPages.Remove(TabPage4)
        End If

    End Sub

    Private Sub DataSourceSettingsEditorInstance_DependentResourceAdded(ByVal sender As Object, ByVal e As ResourceNameEventArgs) Handles DataSourceSettingsEditorInstance.DependentResourceAdded
        Dim resourceId As Guid
        If Guid.TryParse(e.ResourceName, resourceId) Then
            DependencyManagement.AddDependentResourceToResource(_dataSourceResourceEntity, resourceId)
        Else
            Dim dependentResourceToAdd As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(_resourceManager.BankId, e.ResourceName, New ResourceRequestDTO())
            DependencyManagement.AddDependentResourceToResource(_dataSourceResourceEntity, dependentResourceToAdd)
        End If
    End Sub

    Private Sub DataSourceSettingsEditorInstance_DependentResourceRemoved(ByVal sender As Object, ByVal e As ResourceNameEventArgs) Handles DataSourceSettingsEditorInstance.DependentResourceRemoved
        DependencyManagement.RemoveDependentResourceFromResource(_dataSourceResourceEntity, e.ResourceName)
    End Sub

    Private Function PostSaveEditedEntity(Of T)(ByVal resource As ResourceEntity) As Boolean
        Dim result As Boolean

        _dataSourceResourceEntity = ResourceFactory.Instance.GetDataSource(CType(resource, DataSourceResourceEntity))

        _dataSourceSettings.Clear()
        _dataSourceSettings.Add(Parsers.ParseItemDataSourceSettingsFromResourceEntity(_dataSourceResourceEntity))

        If PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.ItemEntity, _dataSourceResourceEntity.BankId) Then
            Me.SaveAsToolStripButton.Enabled = True
        End If

        BindEditorControls(True)
        BindTitleBar(True)
        result = True

        Return result
    End Function

    Private Function PreSaveEditedEntity(Of T)(ByVal editedEntity As T, ByVal resource As ResourceEntity) As Boolean
        Dim result As Boolean = True


        Return result
    End Function

    Private Function Save() As Boolean
        Dim result As Boolean = False

        EnsuresFormUpdateAfterEditing()

        If PreSaveEditedEntity(Of DataSourceSettings)(_dataSourceSettings(0), _dataSourceResourceEntity) Then
            If SaveEditedEntity(_dataSourceSettings(0), _dataSourceResourceEntity) Then
                If PostSaveEditedEntity(Of DataSourceSettings)(_dataSourceResourceEntity) Then
                    result = True
                End If
            End If
        End If

        Return result
    End Function

    Private Function Save(ByVal prompt As Boolean) As Boolean
        Dim result As Boolean

        EnsuresFormUpdateAfterEditing()

        If Not DataSourceSettingsEditorInstance.ValidateAllEditors() Then
            result = False
        Else
            If prompt Then
                If Me.IsDirty AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.TestEntity, _dataSourceResourceEntity.BankId) Then
                    Select Case MessageBox.Show(My.Resources.Editor_SaveChangesQuestionMessage, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        Case DialogResult.Cancel
                            result = False
                        Case DialogResult.No
                            result = True
                        Case DialogResult.Yes
                            result = ValidateItem(True) AndAlso Save()
                    End Select
                Else : result = True
                End If
            Else
                result = ValidateItem(True) AndAlso Save()
            End If
        End If

        Return result
    End Function

    Private Function IsNewResourceValid(ByVal bankId As Integer, ByVal resourceName As String) As Boolean
        Return Not String.IsNullOrEmpty(resourceName) AndAlso ResourceFactory.Instance.GetResourceByNameWithOption(bankId, resourceName, New ResourceRequestDTO()) Is Nothing
    End Function

    Private Function SaveAs() As Boolean
        Dim resultValue As Boolean = False
        Dim orgDataSourceResourceEntity As DataSourceResourceEntity
        Dim isValidResourceName As Boolean = False
        Dim newCode As InputBoxResult = Nothing

        While (Not isValidResourceName OrElse Not resultValue)
            newCode = InputBox.Show(My.Resources.ItemEditor_PleaseEnterNewItemCode, False, My.Resources.ItemEditor_SaveAsText, String.Format(My.Resources.CopyOf0, _dataSourceResourceEntity.Name), AddressOf ValidationHelper.IsValidResourceCode)

            If (newCode.ReturnCode = DialogResult.OK) Then
                Try
                    isValidResourceName = IsNewResourceValid(_dataSourceResourceEntity.BankId, newCode.Text)

                    If (isValidResourceName) Then
                        orgDataSourceResourceEntity = _dataSourceResourceEntity
                        _dataSourceResourceEntity = CopyCurrent(_dataSourceResourceEntity, newCode.Text)

                        With _dataSourceSettings
                        End With

                        resultValue = Me.Save()

                        If resultValue Then
                            _dataSourceResourceEntity = ResourceFactory.Instance.GetDataSource(_dataSourceResourceEntity)

                            _dataSourceSettings.Clear()
                            _dataSourceSettings.Add(Parsers.ParseItemDataSourceSettingsFromResourceEntity(_dataSourceResourceEntity))
                        Else
                            Dim message = DataSourceSettingsEditorInstance.GetErrorMessage
                            If Not String.IsNullOrEmpty(message) Then
                                MessageBox.Show(message, My.Resources.DataSourceEditor_DialogTitle)
                            End If
                            _dataSourceResourceEntity = orgDataSourceResourceEntity
                            BindEditorControls(True)
                        End If
                    Else
                        MessageBox.Show(My.Resources.TestEditor_TestCodeNameAlreadyExistsInBankHierarchy, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Application.ProductName)
                    Exit While
                End Try
            Else
                MessageBox.Show(My.Resources.ItemEditor_SaveAsCancelled, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit While
            End If
        End While

        Return resultValue
    End Function

    Private Sub SaveAsToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripButton.Click, SaveAsToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()

        SaveAs()
    End Sub

    Private Sub SaveCloseToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveCloseToolStripButton.Click
        SaveAndClose()
    End Sub

    Private Function SaveEditedEntity(ByVal editedEntity As DataSourceSettings, ByVal dataSourceResource As DataSourceResourceEntity) As Boolean
        Dim entityUpdatedResult As Boolean = False
        dataSourceResource.SetDataSource(editedEntity)
        If Me.ResourceCustomProperties1.RemovedEntities.Count > 0 Then
            BankFactory.Instance.DeleteCustomPropertyValues(ResourceCustomProperties1.RemovedEntities)
        End If

        If dataSourceResource.RequiresMajorVersionIncrement() Then
            If Not IncrementVersion(dataSourceResource) Then
                StatusTextLabel.Text = "Failed"
                Return False
            End If
        End If

        Dim result As String = ResourceFactory.Instance.UpdateDataSourceResource(dataSourceResource)
        If String.IsNullOrEmpty(result) Then
            StatusTextLabel.Text = "Saved"
            entityUpdatedResult = True
        Else
            MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            StatusTextLabel.Text = "Failed"
            entityUpdatedResult = False
        End If

        Return entityUpdatedResult
    End Function

    Private Function IncrementVersion(ByVal entity As ResourceEntity) As Boolean
        Return _windowFacade.OpenMajorVersionDialog(entity)
    End Function

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripButton.Click
        If Not Me.Save(False) Then
            Dim message = DataSourceSettingsEditorInstance.GetErrorMessage
            If Not String.IsNullOrEmpty(message) Then
                MessageBox.Show(message, My.Resources.DataSourceEditor_DialogTitle)
            End If
        End If
    End Sub

    Private Sub TitleToFormTitleFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        Dim resourceType As String

        If _dataSourceResourceEntity.IsTemplate Then
            resourceType = My.Resources.TestTemplate
        Else
            resourceType = My.Resources.DataSource
        End If
        e.Value = String.Format(My.Resources.EditorFor01, resourceType, e.Value)
    End Sub

    Private Sub EnsuresFormUpdateAfterEditing()
        For Each c As Control In Me.Controls
            c.Focus()
        Next
    End Sub


    Private Sub CanDataSourcePreviewExecuted(sender As Object, e As CancelEventArgs) Handles DataSourcePreviewInstance.CanDataSourcePreviewExecuted
        e.Cancel = Not DataSourceSettingsEditorInstance.ValidateAllEditors()
        If (e.Cancel) Then
            TabControl2.SelectedTab = Me.TabPage3
        End If

    End Sub

    Private Sub DataSourcePreviewInstance_DataSourcePreviewExecuted(ByVal sender As Object,
                                                                    ByVal e As DataSourcePreviewExecutedEventArgs) Handles DataSourcePreviewInstance.DataSourcePreviewExecuted
        Dim dataSources As New Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))
        dataSources.Add(e.Settings, e.ResultItemList)

        Me.DataSourceReportsInstance.Initialize(dataSources, _resourceManager)
    End Sub

    Private Sub SaveCloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveCloseToolStripMenuItem.Click
        SaveAndClose()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub SaveAndClose()
        If Me.IsDirty Then
            If Me.Save(False) Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Function ValidateItem(ByVal showMessageBox As Boolean) As Boolean
        Dim validatingFailed As Boolean = False
        Dim errorMessageBuilder As New StringBuilder()
        errorMessageBuilder.AppendFormat(My.Resources.ItemEditor_ValidateItem_ValidationErrors, Environment.NewLine)

        _dataSourceResourceEntity.ValidateEntity()

        Dim dataErrorInfo As IDataErrorInfo = DirectCast(_dataSourceResourceEntity, IDataErrorInfo)
        If Not String.IsNullOrEmpty(dataErrorInfo.Error) Then
            errorMessageBuilder.Append(dataErrorInfo.Error)
            validatingFailed = True
        End If

        If validatingFailed Then
            If showMessageBox Then
                MessageBox.Show(errorMessageBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return False
        Else
            Return True
        End If

    End Function
End Class