Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Diagnostics.CodeAnalysis
Imports System.ComponentModel
Imports System.Text
Imports Questify.Builder.Logic.ContentModel
Imports SecurityException = System.Security.SecurityException
Imports Questify.Builder.Logic.CustomProperties
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.UI

Public Class CustomPropertyEditor

    Public Event OnCustomPropertyAdded As EventHandler(Of SelectedCustomPropertyEventArgs)
    Public Event OnRefresh As EventHandler(Of Questify.Builder.UI.RefreshEventArgs)

    Private _editCustomPropertiesPermitted As Boolean
    Private ReadOnly _customBankPropertyEntity As CustomBankPropertyEntity
    Private _structureUserControl As IEditableCollectionControl
    Private _valueUserControl As IEditableCollectionControl
    Private _richTextUserControl As RichTextValueDetailsUserControl

    Public Sub New(customBankPropertyEntity As CustomBankPropertyEntity)
        InitializeComponent()
        _customBankPropertyEntity = customBankPropertyEntity
        SetToolBarButtons()
    End Sub

    Public ReadOnly Property CustomBankPropertyEntity As CustomBankPropertyEntity
        Get
            Return _customBankPropertyEntity
        End Get
    End Property

    Private Sub BindControls()
        GeneralUserControl.Initialize(_customBankPropertyEntity, False)

        If TypeOf _customBankPropertyEntity Is ConceptStructureCustomBankPropertyEntity Then
            TabPageValues.Controls.Remove(CType(_valueUserControl, Control))
            TabPageStructure.Controls.Remove(CType(_structureUserControl, Control))

            _valueUserControl = New ConceptValueDetailsUserControl()
            _structureUserControl = New ConceptStructureUserControl()

            Dim isConceptPropertyConnectedToItem As Boolean = IsConceptStructureCustomBankPropertyConnectedToItem(CType(_customBankPropertyEntity, ConceptStructureCustomBankPropertyEntity))

            _valueUserControl.Initialize(_customBankPropertyEntity, isConceptPropertyConnectedToItem)
            _structureUserControl.Initialize(_customBankPropertyEntity, isConceptPropertyConnectedToItem)

            MenuDelete.Enabled = Not isConceptPropertyConnectedToItem
            MenuAdd.Enabled = Not isConceptPropertyConnectedToItem

            TabPageValues.Controls.Add(CType(_valueUserControl, Control))
            TabPageStructure.Controls.Add(CType(_structureUserControl, Control))

            CType(_valueUserControl, Control).Dock = DockStyle.Fill
            CType(_structureUserControl, Control).Dock = DockStyle.Fill
        ElseIf TypeOf _customBankPropertyEntity Is TreeStructureCustomBankPropertyEntity Then
            TabPageValues.Controls.Remove(CType(_valueUserControl, Control))
            TabPageStructure.Controls.Remove(CType(_structureUserControl, Control))

            _valueUserControl = New TreeValueDetailsUserControl()
            _structureUserControl = New TreeStructureUserControl()

            Dim isTreePropertyConnectedToItem As Boolean = IsTreeStructureCustomBankPropertyConnectedToItem(CType(_customBankPropertyEntity, TreeStructureCustomBankPropertyEntity))

            _valueUserControl.Initialize(_customBankPropertyEntity, False)
            _structureUserControl.Initialize(_customBankPropertyEntity, False)

            TabPageValues.Controls.Add(CType(_valueUserControl, Control))
            TabPageStructure.Controls.Add(CType(_structureUserControl, Control))

            CType(_valueUserControl, Control).Dock = DockStyle.Fill
            CType(_structureUserControl, Control).Dock = DockStyle.Fill
        ElseIf TypeOf _customBankPropertyEntity Is ListCustomBankPropertyEntity Then
            TabControlMain.TabPages.Remove(TabPageStructure)

            TabPageValues.Controls.Remove(CType(_valueUserControl, Control))
            TabPageStructure.Controls.Remove(CType(_structureUserControl, Control))

            _valueUserControl = New ListValueDetailsUserControl()
            _valueUserControl.Initialize(CType(_customBankPropertyEntity, ListCustomBankPropertyEntity), False)

            TabPageValues.Controls.Add(CType(_valueUserControl, Control))

            CType(_valueUserControl, Control).Dock = DockStyle.Fill
        Else
            TabControlMain.TabPages.Remove(TabPageStructure)
            TabControlMain.TabPages.Remove(TabPageValues)
        End If
    End Sub


    Private Function IsConceptStructureCustomBankPropertyConnectedToItem(ByVal conceptStructureCustomBankPropertyEntity As ConceptStructureCustomBankPropertyEntity) As Boolean
        If Not conceptStructureCustomBankPropertyEntity.IsNew AndAlso conceptStructureCustomBankPropertyEntity.CustomBankPropertyValueCollection.Count = 0 Then
            BankFactory.Instance.GetReferencesForCustomBankProperty(conceptStructureCustomBankPropertyEntity)
        End If

        Return conceptStructureCustomBankPropertyEntity.CustomBankPropertyValueCollection.Count > 0
    End Function


    Private Function IsTreeStructureCustomBankPropertyConnectedToItem(ByVal treeStructureCustomBankPropertyEntity As TreeStructureCustomBankPropertyEntity) As Boolean
        If Not treeStructureCustomBankPropertyEntity.IsNew AndAlso treeStructureCustomBankPropertyEntity.CustomBankPropertyValueCollection.Count = 0 Then
            BankFactory.Instance.GetReferencesForCustomBankProperty(treeStructureCustomBankPropertyEntity)
        End If

        Return treeStructureCustomBankPropertyEntity.CustomBankPropertyValueCollection.Count > 0
    End Function

    Private Sub CustomPropertyEditor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim isValid As Boolean = ValidateChildren()

        If IsDirty() Then
            If ValidateChildren() Then
                Dim result As DialogResult = MessageBox.Show(My.Resources.ConceptEditor_SaveChanges, String.Empty, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If result = DialogResult.Yes Then
                    If isValid Then
                        If Not Save() Then
                            e.Cancel = True
                        End If
                    Else
                        e.Cancel = True
                    End If
                ElseIf result = DialogResult.No Then
                    If Not _customBankPropertyEntity.IsNew Then
                        RaiseEvent OnRefresh(Me, New Questify.Builder.UI.RefreshEventArgs(_customBankPropertyEntity, False))
                    End If
                Else
                    e.Cancel = True
                End If
            Else
                Dim title = String.Format(My.Resources.CustomPropertyEditor_TitleBar, _customBankPropertyEntity.Name)
                If MessageBox.Show(My.Resources.CustomPropertyEditor_ValidationErrorsOnExit, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub


    Private Function IsDirty() As Boolean
        If _customBankPropertyEntity.HasChangesInTopology() Then
            Return True
        End If

        If _valueUserControl IsNot Nothing AndAlso _valueUserControl.RemovedEntities.Count > 0 Then
            Return True
        ElseIf _structureUserControl IsNot Nothing AndAlso _structureUserControl.RemovedEntities.Count > 0 Then
            Return True
        ElseIf _richTextUserControl IsNot Nothing AndAlso _richTextUserControl.IsDirty() Then
            Return True
        End If

        Return False
    End Function

    Private Sub BindCustomPropertyNameToTitleOfForm()
        Dim titleBarBinding As New Binding("Text", _customBankPropertyEntity, "name")
        AddHandler titleBarBinding.Format, AddressOf TitleBarBindingFormat

        DataBindings.Clear()
        DataBindings.Add(titleBarBinding)
    End Sub

    Private Sub TitleBarBindingFormat(sender1 As Object, e1 As ConvertEventArgs)
        e1.Value = String.Format(My.Resources.CustomPropertyEditor_TitleBar, e1.Value)
    End Sub

    Private Sub CustomPropertyEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        BindCustomPropertyNameToTitleOfForm()

        _editCustomPropertiesPermitted = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit Or TestBuilderPermissionAccess.AddNew Or TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.CustomBankPropertyEntity, _customBankPropertyEntity.BankId)
        If Not _editCustomPropertiesPermitted Then
            MessageBox.Show(My.Resources.CustomPropertyEditor_NotAllowed)
            Close()
        End If

        BindControls()
    End Sub

    Private Sub TabControlMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControlMain.SelectedIndexChanged
        UpdateGrid()
        SetToolBarButtons()
    End Sub

    Private Sub UpdateGrid()
        If IsDirty() Then
            Dim isConnectedToItem As Boolean

            If TypeOf _customBankPropertyEntity Is ConceptStructureCustomBankPropertyEntity Then
                isConnectedToItem = IsConceptStructureCustomBankPropertyConnectedToItem(CType(_customBankPropertyEntity, ConceptStructureCustomBankPropertyEntity))
            ElseIf TypeOf _customBankPropertyEntity Is TreeStructureCustomBankPropertyEntity Then
                isConnectedToItem = IsTreeStructureCustomBankPropertyConnectedToItem(CType(_customBankPropertyEntity, TreeStructureCustomBankPropertyEntity))
            End If

            Select Case TabControlMain.SelectedTab.Tag.ToString
                Case "Values"
                    _valueUserControl.Initialize(_customBankPropertyEntity, isConnectedToItem)
                Case "Structure"
                    _structureUserControl.Initialize(_customBankPropertyEntity, isConnectedToItem)
            End Select
        End If
    End Sub

    Private Sub SetToolBarButtons()
        Select Case TabControlMain.SelectedTab.Tag.ToString
            Case "Values"
                If _valueUserControl IsNot Nothing Then
                    MenuDelete.Visible = True
                    MenuAdd.Visible = True
                ElseIf _richTextUserControl IsNot Nothing Then
                    MenuDelete.Visible = False
                    MenuAdd.Visible = False
                End If
            Case "Structure"
                MenuDelete.Visible = False
                MenuAdd.Visible = False

                If TypeOf _structureUserControl Is ConceptStructureUserControl OrElse TypeOf _structureUserControl Is TreeStructureUserControl Then
                    ActiveControl = CType(_structureUserControl, Control)
                End If
            Case Else
                MenuDelete.Visible = False
                MenuAdd.Visible = False
        End Select

        If TypeOf _customBankPropertyEntity Is ConceptStructureCustomBankPropertyEntity AndAlso IsConceptStructureCustomBankPropertyConnectedToItem(CType(_customBankPropertyEntity, ConceptStructureCustomBankPropertyEntity)) Then
            MenuDelete.Enabled = False
            MenuAdd.Enabled = False
        End If
    End Sub


    <SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Private Function Save() As Boolean
        Dim resultOfDeletedConceptStructures As Boolean = True
        Dim resultOfDeletedListValueDetails As Boolean = True
        Dim resultOfAddAndUpdate As Boolean

        _customBankPropertyEntity.ValidateEntity()

        If IsDirty() Then
            Try
                resultOfAddAndUpdate = ValidateCustomProperty(True)
                Dim isNew As Boolean = _customBankPropertyEntity.IsNew

                If (resultOfAddAndUpdate = True) Then
                    If _editCustomPropertiesPermitted Then
                        Dim dirtyTreeStructureParts As New List(Of TreeStructurePartCustomBankPropertyEntity)
                        Dim removedTreeStructureParts As New List(Of Guid)
                        If Not isNew AndAlso _valueUserControl IsNot Nothing Then
                            dirtyTreeStructureParts = _customBankPropertyEntity.GetUpdatesInTestStructureParts()
                            removedTreeStructureParts = _valueUserControl.RemovedEntities.OfType(Of TreeStructurePartCustomBankPropertyEntity).Select(Function(t) t.Code).ToList()
                        End If

                        Dim resourcesToUpdateDisplayValueFor = GetResourcesToUpdateDisplayValueFor()

                        BankFactory.Instance.UpdateCustomProperty(_customBankPropertyEntity)

                        resultOfDeletedConceptStructures = HandleDeletedObjects(_structureUserControl)
                        resultOfDeletedListValueDetails = HandleDeletedObjects(_valueUserControl)

                        If resourcesToUpdateDisplayValueFor IsNot Nothing Then
                            UpdateDisplayValues(resourcesToUpdateDisplayValueFor)
                        End If

                        AssessmentTestCutOffScoringHelper.UpdateCutOffScoreConditionsInTests(_customBankPropertyEntity.BankId, dirtyTreeStructureParts, removedTreeStructureParts)
                    End If

                    If isNew Then
                        RaiseEvent OnCustomPropertyAdded(Me, New SelectedCustomPropertyEventArgs(_customBankPropertyEntity))
                    Else
                        RaiseEvent OnRefresh(Me, New Questify.Builder.UI.RefreshEventArgs(_customBankPropertyEntity, False))
                    End If
                Else
                    resultOfAddAndUpdate = False
                End If
            Catch ex As SecurityException
                MessageBox.Show(ex.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                resultOfAddAndUpdate = False
            Catch ex As Exception
                MessageBox.Show(String.Format(My.Resources.ErrorSavingBankInformation, Environment.NewLine, ex.Message), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                resultOfAddAndUpdate = False
            End Try
        Else
            resultOfAddAndUpdate = ValidateCustomProperty(True)
        End If

        Return resultOfAddAndUpdate AndAlso resultOfDeletedListValueDetails AndAlso resultOfDeletedConceptStructures
    End Function

    Private Function GetResourcesToUpdateDisplayValueFor() As Dictionary(Of Guid, Guid)
        Dim updateResourceCustomProperties As Dictionary(Of Guid, Guid) = Nothing

        If TypeOf _customBankPropertyEntity Is ListCustomBankPropertyEntity Then
            Dim dirtyListValues = DirectCast(_customBankPropertyEntity, ListCustomBankPropertyEntity).ListValueCustomBankPropertyCollection.DirtyEntities
            If dirtyListValues IsNot Nothing AndAlso dirtyListValues.Any(Function(de) de.IsDirty AndAlso Not de.IsNew) Then
                Dim currentValues = BankFactory.Instance.GetListValueCustomBankProperties(dirtyListValues.Select(Function(d) d.ListValueBankCustomPropertyId).ToList())

                Dim updateValues = From dv In dirtyListValues
                                   Join cv In currentValues
                                   On dv.ListValueBankCustomPropertyId Equals DirectCast(cv, ListValueCustomBankPropertyEntity).ListValueBankCustomPropertyId
                                   Where Not dv.Name.Equals(DirectCast(cv, ListValueCustomBankPropertyEntity).Name)
                                   Select dv.ListValueBankCustomPropertyId

                If updateValues IsNot Nothing Then
                    For Each uv In updateValues
                        For Each ir As ListCustomBankPropertySelectedValueEntity In BankFactory.Instance.GetListCustomBankPropertyValueReferences(uv)
                            If updateResourceCustomProperties Is Nothing Then updateResourceCustomProperties = New Dictionary(Of Guid, Guid)
                            If Not updateResourceCustomProperties.ContainsKey(ir.ResourceId) Then
                                updateResourceCustomProperties.Add(ir.ResourceId, ir.CustomBankPropertyId)
                            End If
                        Next
                    Next
                End If
            End If
            For Each rv In _valueUserControl.RemovedEntities.OfType(Of ListValueCustomBankPropertyEntity).Select(Function(t) t.ListValueBankCustomPropertyId)
                For Each ir As ListCustomBankPropertySelectedValueEntity In BankFactory.Instance.GetListCustomBankPropertyValueReferences(rv)
                    If updateResourceCustomProperties Is Nothing Then updateResourceCustomProperties = New Dictionary(Of Guid, Guid)
                    If Not updateResourceCustomProperties.ContainsKey(ir.ResourceId) Then
                        updateResourceCustomProperties.Add(ir.ResourceId, ir.CustomBankPropertyId)
                    End If
                Next
            Next
        ElseIf TypeOf _customBankPropertyEntity Is TreeStructureCustomBankPropertyEntity Then
            Dim dirtyListValues = DirectCast(_customBankPropertyEntity, TreeStructureCustomBankPropertyEntity).TreeStructurePartCustomBankPropertyCollection.DirtyEntities
            If dirtyListValues IsNot Nothing AndAlso dirtyListValues.Any(Function(de) de.IsDirty AndAlso Not de.IsNew) Then
                Dim currentValues = BankFactory.Instance.GetTreeStructurePartCustomBankProperties(dirtyListValues.Select(Function(d) d.TreeStructurePartCustomBankPropertyId).ToList(), False)

                Dim updateValues = From dv In dirtyListValues
                                   Join cv In currentValues
                                   On dv.TreeStructurePartCustomBankPropertyId Equals DirectCast(cv, TreeStructurePartCustomBankPropertyEntity).TreeStructurePartCustomBankPropertyId
                                   Where Not dv.Name.Equals(DirectCast(cv, TreeStructurePartCustomBankPropertyEntity).Name)
                                   Select dv.TreeStructurePartCustomBankPropertyId

                If updateValues IsNot Nothing Then
                    For Each uv In updateValues
                        For Each ir As TreeStructureCustomBankPropertySelectedPartEntity In BankFactory.Instance.GetTreeStructureCustomBankPropertyValueReferences(uv)
                            If updateResourceCustomProperties Is Nothing Then updateResourceCustomProperties = New Dictionary(Of Guid, Guid)
                            If Not updateResourceCustomProperties.ContainsKey(ir.ResourceId) Then
                                updateResourceCustomProperties.Add(ir.ResourceId, ir.CustomBankPropertyId)
                            End If
                        Next
                    Next
                End If
            End If
            For Each rv In _valueUserControl.RemovedEntities.OfType(Of TreeStructurePartCustomBankPropertyEntity).Select(Function(t) t.TreeStructurePartCustomBankPropertyId)
                For Each ir As TreeStructureCustomBankPropertySelectedPartEntity In BankFactory.Instance.GetTreeStructureCustomBankPropertyValueReferences(rv)
                    If updateResourceCustomProperties Is Nothing Then updateResourceCustomProperties = New Dictionary(Of Guid, Guid)
                    If Not updateResourceCustomProperties.ContainsKey(ir.ResourceId) Then
                        updateResourceCustomProperties.Add(ir.ResourceId, ir.CustomBankPropertyId)
                    End If
                Next
            Next
        End If

        Return updateResourceCustomProperties
    End Function

    Private Function UpdateDisplayValues(resourcesToUpdateDisplayValueFor As Dictionary(Of Guid, Guid)) As Boolean
        If resourcesToUpdateDisplayValueFor IsNot Nothing Then
            Dim updatedCustomPropertiesValues As New EntityCollection
            Dim knownCustomProperties As List(Of CustomBankPropertyDto) = Nothing

            Dim entitiesToProgress = BankFactory.Instance.GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(New List(Of Guid) From {_customBankPropertyEntity.CustomBankPropertyId}, resourcesToUpdateDisplayValueFor.Keys.ToList, False)
            For Each value As CustomBankPropertyValueEntity In entitiesToProgress
                If knownCustomProperties Is Nothing Then
                    knownCustomProperties = New List(Of CustomBankPropertyDto)
                    knownCustomProperties.AddRange(DtoFactory.CustomBankProperty.GetMulti(New List(Of Guid) From {_customBankPropertyEntity.CustomBankPropertyId}))
                End If
                value.SetCustomPropertyDisplayValue(_customBankPropertyEntity, knownCustomProperties)
                updatedCustomPropertiesValues.Add(value)
            Next

            BankFactory.Instance.UpdateCustomPropertyValues(updatedCustomPropertiesValues)
            updatedCustomPropertiesValues = Nothing
        End If
    End Function


    Private Function HandleDeletedObjects(ByVal control As IEditableCollectionControl) As Boolean
        If control IsNot Nothing Then

            If control.RemovedEntities.Count > 0 Then
                Dim resultString As String = BankFactory.Instance.DeleteCustomProperties(control.RemovedEntities)

                If resultString <> String.Empty Then
                    Dim msgResult As DialogResult
                    If control.RemovedEntitiesAllConfirmed Then
                        msgResult = DialogResult.OK
                    Else
                        msgResult = MessageBox.Show(My.Resources.WarningDeleteCustomProperties, My.Resources.WarningDeleteCustomPropertiesTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
                    End If

                    If msgResult = DialogResult.Cancel Then
                        control.UndoRemoveEntities()
                        Return False
                    End If

                    resultString = BankFactory.Instance.DeleteCustomPropertiesForced(control.RemovedEntities)
                    If resultString <> String.Empty Then
                        MessageBox.Show(String.Format(My.Resources.ErrorSavingBankInformation, Environment.NewLine, resultString))
                    End If
                End If

                control.RemovedEntities.Clear()
            End If
        End If

        Return True
    End Function

    Private Function ValidateCustomProperty(ByVal showMessageBox As Boolean) As Boolean
        Dim validatingFailed As Boolean = False
        Dim errorMessageBuilder As New StringBuilder()
        errorMessageBuilder.AppendFormat(My.Resources.CustomPropertyEditor_ValidationErrors, Environment.NewLine)

        Dim dataErrorInfo As IDataErrorInfo = DirectCast(_customBankPropertyEntity, IDataErrorInfo)
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

    Private Sub CustomPropertyEditor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If DataBindings IsNot Nothing AndAlso DataBindings.Count > 0 Then
            RemoveHandler DataBindings(0).Format, AddressOf TitleBarBindingFormat
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuSave.Click
        If ValidateChildren() Then
            If Save() Then
                _valueUserControl.Saved()
            End If
        End If
    End Sub

    Private Sub SaveAndCloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuSaveAndClose.Click
        If ValidateChildren() Then
            If Save() Then
                Close()
            End If
        End If
    End Sub

    Private Sub AddValueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuAdd.Click
        If String.IsNullOrEmpty(_valueUserControl.ErrorMessage) Then
            _valueUserControl.AddItem()
        Else
            MessageBox.Show(My.Resources.FixErrorsFirst, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DeleteValueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuDelete.Click
        _valueUserControl.RemoveItem()
    End Sub
End Class