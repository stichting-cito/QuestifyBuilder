Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.Common
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Text
Imports Enums
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces

Public Class MultiSelectItemEditWizardForm


    Private _clonedItemResourceEntity As ItemResourceEntity = Nothing
    Private _selectedEntitiesNew As List(Of ItemResourceEntity)
    Private ReadOnly _selectedEntities As IList(Of ResourceDto)
    Private ReadOnly _skippedItemsBecauseNoKeyPresent As New List(Of String)
    Private _resourceManager As DataBaseResourceManager
    Private _contextIdentifier As Nullable(Of Integer)

    Private Const CONDITIONAL_OR As Char = "|"c
    Private Const CONDITIONAL_NOT As Char = "!"c
    Private Const CONDITIONAL_EMPTY As String = "(EMPTY)"
    Private Const CONDITIONAL_DUMMYVALUE As String = "DUMMYVALUE"
    Private Const INLINE_GAP_PREFIX As String = "inlineGap_"



    Public Sub New()
        InitializeComponent()

        Me.InitTabControl()
    End Sub

    Public Sub New(ByVal bankId As Integer, ByVal selectedEntities As IList(Of ResourceDto))
        Me.New()

        Me.BankId = bankId
        _selectedEntities = selectedEntities
        _resourceManager = New DataBaseResourceManager(bankId)
        _contextIdentifier = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(_resourceManager)

        AddHandler TestSessionContext.ResourceNeeded, AddressOf ItemParameterGrid_ResourceNeeded
    End Sub

    Protected Overrides Sub Finalize()
        RemoveHandler TestSessionContext.ResourceNeeded, AddressOf ItemParameterGrid_ResourceNeeded

        If _resourceManager IsNot Nothing Then
            TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
            _resourceManager.Dispose()
            _resourceManager = Nothing
        End If

        MyBase.Finalize()
    End Sub


    Private Sub MultiSelectItemEditWizardForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim request = New ItemResourceRequestDTO() With {.WithCustomProperties = True, .WithDependencies = True}
        _selectedEntitiesNew = ResourceFactory.Instance.GetItemsByCodes(_selectedEntities.Select(Function(r) r.Name).ToList, Me.BankId, request).ToList
    End Sub

    Private Sub MultiSelectItemEditWizardForm_WizardBeforeTabChange(ByVal sender As Object, ByVal e As WizardBeforeTabChangeEventArgs) Handles Me.WizardBeforeTabChange
        Dim currentCursor As Cursor = Cursor.Current
        Try
            Cursor.Current = Cursors.WaitCursor

            Select Case e.CurrentTab.Tag.ToString
                Case "WelcomeTab"
                    FillParametersGrid()

                    FillCustomPropertiesGrid()

                    FillMetaDataGrid()

                Case "SelectEdit"

                    Dim shouldShowOverwriteTab As Boolean = ShowOverwriteTab()
                    If Not shouldShowOverwriteTab Then
                        e.NextTab = TabControlMain.TabPages("FileSelectionTabPageControl")
                    End If

                    _clonedItemResourceEntity = BinaryCloner.DeepClone(Of ItemResourceEntity)(Me.SelectedEntities(0))

                    If AvailableParametersGrid.SelectedItems.Count > 0 Then
                        TableLayoutPanelEditCP.Visible = False
                        TableLayoutPanelEditMD.Visible = False
                        PanelEditPE.Visible = True

                        FillParameterSetsEditor()

                        CancelBtn.Focus()
                    ElseIf AvailablePropertiesGrid.SelectedItems.Count > 0 Then
                        If shouldShowOverwriteTab Then
                            If AvailablePropertiesGrid.SelectedItems.Count = 1 Then
                                Dim customProperty As CustomBankPropertyEntity = CType(AvailablePropertiesGrid.SelectedItems(0).GetRow().DataRow, CustomBankPropertyEntity)
                                Me.OverwriteExistingCPListMultiValuesCheckbox.Text = String.Format(My.Resources.MultiSelectItemEditWizardOverwriteCheckboxText, customProperty.Name)
                            End If
                        Else
                            TableLayoutPanelEditCP.Visible = True
                            TableLayoutPanelEditMD.Visible = False
                            PanelEditPE.Visible = False

                            FillResourceCustomPropertyEditor()
                        End If

                    ElseIf MetaDataGrid.SelectedItems.Count > 0 Then
                        TableLayoutPanelEditCP.Visible = False
                        TableLayoutPanelEditMD.Visible = True
                        PanelEditPE.Visible = False

                        FillMetaDataEditor()
                    End If

                Case "Overwrite"
                    TableLayoutPanelEditCP.Visible = True
                    TableLayoutPanelEditMD.Visible = False
                    PanelEditPE.Visible = False

                    FillResourceCustomPropertyEditor()

                Case "Edit"
                    If AvailableParametersGrid.SelectedItems.Count > 0 Then
                        Dim editorsErrorResult As String = paramSetsEditor.ValidateParameterEditors()
                        If Not String.IsNullOrEmpty(editorsErrorResult) Then
                            MessageBox.Show(My.Resources.MultiSelectItemEditWizardValidationErrors, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If

                    OverviewTabContent.OverviewText = String.Format(My.Resources.MultiSelectItemEditWizardChanging, Me.SelectedEntities.Count.ToString)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = currentCursor
        End Try
    End Sub

    Private Function ShowOverwriteTab() As Boolean
        For x As Integer = 0 To AvailablePropertiesGrid.SelectedItems.Count - 1
            Dim customProperty As CustomBankPropertyEntity = CType(AvailablePropertiesGrid.SelectedItems(x).GetRow().DataRow, CustomBankPropertyEntity)
            If TypeOf customProperty Is ListCustomBankPropertyEntity AndAlso DirectCast(customProperty, ListCustomBankPropertyEntity).MultipleSelect Then Return True
        Next
        Return False
    End Function

    Private Sub MultiSelectItemEditWizardForm_WizardClose(ByVal sender As Object, ByVal e As WizardCancelEventArgs) Handles Me.WizardClose
        resourceCustomPropsEditor.Dispose()
    End Sub

    Private Sub MultiSelectItemEditWizardForm_WizardCompleted(ByVal sender As Object, ByVal e As EventArgs) Handles Me.WizardCompleted
        Dim strB As New StringBuilder

        strB.AppendLine(String.Format(My.Resources.MultiSelectItemEditWizardCompleted, Me.SelectedEntities.Count - _skippedItemsBecauseNoKeyPresent.Count))

        If _skippedItemsBecauseNoKeyPresent.Count > 0 Then
            strB.AppendLine()
            strB.AppendLine(String.Format(My.Resources.MultiSelectItemEditWizardSkippedItemsBecauseMissingKey, _skippedItemsBecauseNoKeyPresent.Count))

            For Each itemCode As String In _skippedItemsBecauseNoKeyPresent
                strB.AppendLine(itemCode)
            Next
        End If
        Me.ResultTabContent.ResultText = strB.ToString
    End Sub

    Private Sub MultiSelectItemEditWizardForm_WizardDoProcess(ByVal sender As Object, ByVal e As WizardProcessEventArgs) Handles Me.WizardDoProcess
        Dim preEditItemResourceEntity As ItemResourceEntity
        Dim addedResourceDependencies As List(Of DependentResourceEntity) = Nothing
        Dim iltAdapter As ItemLayoutAdapter = Nothing

        ProcessTabContent.ProgressMinimumValueDetail = 0
        ProcessTabContent.ProgressMaximumValueDetail = Me.SelectedEntities.Count
        ProcessTabContent.ProgressValueDetail = 0
        Application.DoEvents()


        If AvailableParametersGrid.SelectedItems.Count > 0 Then
            paramSetsEditor.PreItemSave()

            preEditItemResourceEntity = ResourceFactory.Instance.GetItem(_clonedItemResourceEntity, New ResourceRequestDTO() With {.WithDependencies = True})
            addedResourceDependencies = paramSetsEditor.ResourceEntity.DependentResourceCollection.Where(Function(pser)
                                                                                                             Return (preEditItemResourceEntity.DependentResourceCollection.FirstOrDefault(Function(dr) pser.DependentResource.Name = dr.DependentResource.Name) Is Nothing)
                                                                                                         End Function
                                                                                                        ).Cast(Of DependentResourceEntity).ToList()
        End If

        Dim requestedMajorVersionComment = False
        Dim knownCustomProperties = New List(Of CustomBankPropertyDto)
        knownCustomProperties.AddRange(DtoFactory.CustomBankProperty.GetCustomBankPropertiesForBranchWithFilter(BankId, ResourceTypeEnum.ItemResource.ToString))

        For Each itemResourceEntity As ItemResourceEntity In Me.SelectedEntities

            If itemResourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                itemResourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection()
            End If

            If AvailableParametersGrid.SelectedItems.Count > 0 Then
                Dim dataRow As DataRowView = DirectCast(AvailableParametersGrid.SelectedItems(0).GetRow.DataRow, DataRowView)
                Dim paramColId As String = dataRow.Row.Item("paramColId").ToString
                Dim paramGroup As String = dataRow.Row.Item("group").ToString
                Dim paramLabel As String = dataRow.Row.Item("label").ToString

                Dim paramSetDict As New Dictionary(Of String, Dictionary(Of String, ParameterBase))

                For Each paramCollection As ParameterCollection In paramSetsEditor.ParameterSets
                    If paramCollection.Id = paramColId Then
                        If paramCollection.InnerParameters IsNot Nothing Then
                            For Each parameter As ParameterBase In paramCollection.InnerParameters
                                If (parameter.DesignerSettings.GetSettingValueByKey("group") = paramGroup OrElse paramGroup = "-") AndAlso
 (parameter.DesignerSettings.GetSettingValueByKey("label") = paramLabel OrElse parameter.DesignerSettings.GetSettingValueByKey("itemcountlabel") = paramLabel) Then

                                    If Not paramSetDict.ContainsKey(paramCollection.Id) Then
                                        paramSetDict.Add(paramCollection.Id, New Dictionary(Of String, ParameterBase))
                                    End If
                                    paramSetDict(paramCollection.Id).Add(parameter.Name, parameter)
                                End If
                            Next
                        End If
                    End If
                Next

                Dim assessmentItemNew As AssessmentItem = GetExistingAssessmentItem(itemResourceEntity)
                Dim paramCollIndexer As Integer = 0
                For Each paramCollection As ParameterCollection In assessmentItemNew.Parameters
                    If paramCollection.Id = paramColId Then
                        Dim paramIndexer As Integer = 0
                        For Each parameter As ParameterBase In paramCollection.InnerParameters

                            Dim newParameter As ParameterBase
                            If paramSetDict.ContainsKey(paramCollection.Id) AndAlso paramSetDict(paramCollection.Id).ContainsKey(parameter.Name) Then
                                newParameter = paramSetDict(paramCollection.Id)(parameter.Name)

                                If parameter.Name = newParameter.Name Then
                                    If (TypeOf newParameter Is ResourceParameter) Then
                                        Dim oldVal As ResourceParameter = DirectCast(parameter, ResourceParameter)
                                        Dim newVal As ResourceParameter = DirectCast(newParameter, ResourceParameter)
                                        RemoveResource(itemResourceEntity, assessmentItemNew.Parameters, oldVal.Value)
                                        AddResource(itemResourceEntity, newVal.Value)
                                    End If
                                    assessmentItemNew.Parameters(paramCollIndexer).InnerParameters(paramIndexer) = newParameter

                                    UpdateRedirectedParameters(paramSetsEditor.ParameterSets, paramCollection.Id, newParameter, assessmentItemNew.Parameters)
                                    Exit For
                                End If
                            End If
                            paramIndexer += 1
                        Next
                    End If
                    paramCollIndexer += 1
                Next

                For Each addedDependentResource In addedResourceDependencies
                    AddResource(itemResourceEntity, addedDependentResource.DependentResource.Name)
                Next

                Dim scoringPrms = assessmentItemNew.Parameters.DeepFetchInlineScoringParameters()
                If scoringPrms.Any(Function(p) TypeOf (p) Is ScoringParameter) Then
                    assessmentItemNew.Solution.FixRemovedScoringParameters(scoringPrms)
                End If

                assessmentItemNew.SetScorePropertiesForItem(itemResourceEntity)

                itemResourceEntity.SetAssessmentItem(assessmentItemNew)

            ElseIf AvailablePropertiesGrid.SelectedItems.Count > 0 Then
                Dim changedProp As CustomBankPropertyValueEntity
                Dim customProperty As CustomBankPropertyEntity = CType(AvailablePropertiesGrid.SelectedItems(0).GetRow().DataRow, CustomBankPropertyEntity)
                Dim overwriteExistingValues As Boolean = True
                If TypeOf customProperty Is ListCustomBankPropertyEntity AndAlso DirectCast(customProperty, ListCustomBankPropertyEntity).MultipleSelect Then overwriteExistingValues = Me.OverwriteExistingCPListMultiValuesCheckbox.Checked

                If _clonedItemResourceEntity.CustomBankPropertyValueCollection.Count = 0 Then
                    changedProp = Nothing
                    Select Case customProperty.GetType.ToString
                        Case GetType(FreeValueCustomBankPropertyEntity).ToString
                            Dim freeValue As New FreeValueCustomBankPropertyValueEntity
                            freeValue.Value = String.Empty
                            changedProp = freeValue
                        Case GetType(ListCustomBankPropertyEntity).ToString
                            Dim listValue As New ListCustomBankPropertyValueEntity
                            If itemResourceEntity.CustomBankPropertyValueCollection.Any(Function(cp) cp.CustomBankPropertyId.Equals(customProperty.CustomBankPropertyId)) Then
                                listValue.CustomBankPropertyId = customProperty.CustomBankPropertyId
                            End If
                            If overwriteExistingValues Then listValue.ListCustomBankPropertySelectedValueCollection.Clear()
                            changedProp = listValue
                        Case GetType(RichTextValueCustomBankPropertyEntity).ToString
                            If itemResourceEntity.CustomBankPropertyValueCollection.Any(Function(cp) cp.CustomBankPropertyId.Equals(customProperty.CustomBankPropertyId)) Then
                                Dim richTextValue As New RichTextValueCustomBankPropertyValueEntity
                                richTextValue = CType(itemResourceEntity.CustomBankPropertyValueCollection.First(Function(cp) cp.CustomBankPropertyId.Equals(customProperty.CustomBankPropertyId)), RichTextValueCustomBankPropertyValueEntity)
                                richTextValue.Value = String.Empty
                                changedProp = richTextValue
                            End If
                    End Select
                Else
                    changedProp = _clonedItemResourceEntity.CustomBankPropertyValueCollection(0)
                End If

                If changedProp IsNot Nothing Then
                    Select Case changedProp.GetType.ToString
                        Case GetType(FreeValueCustomBankPropertyValueEntity).ToString
                            Dim freeValue As FreeValueCustomBankPropertyValueEntity = DirectCast(changedProp, FreeValueCustomBankPropertyValueEntity)

                            Dim existing As Boolean = False
                            For Each prop As CustomBankPropertyValueEntity In itemResourceEntity.CustomBankPropertyValueCollection
                                If prop.CustomBankPropertyId = freeValue.CustomBankPropertyId Then
                                    If freeValue.Value <> String.Empty Then
                                        Dim newfreeValue As FreeValueCustomBankPropertyValueEntity = DirectCast(prop, FreeValueCustomBankPropertyValueEntity)
                                        newfreeValue.Value = freeValue.Value
                                        newfreeValue.SetCustomPropertyDisplayValue(customProperty, knownCustomProperties)
                                    Else
                                        itemResourceEntity.CustomBankPropertyValueCollection.Remove(prop)
                                        itemResourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(prop)
                                    End If
                                    existing = True
                                    Exit For
                                End If
                            Next

                            If Not existing Then
                                If freeValue.Value <> String.Empty Then
                                    Dim newfreeValue As New FreeValueCustomBankPropertyValueEntity
                                    newfreeValue.IsNew = True
                                    newfreeValue.ResourceId = itemResourceEntity.ResourceId
                                    newfreeValue.CustomBankPropertyId = freeValue.CustomBankPropertyId
                                    newfreeValue.Value = freeValue.Value
                                    newfreeValue.SetCustomPropertyDisplayValue(customProperty, knownCustomProperties)
                                    itemResourceEntity.CustomBankPropertyValueCollection.Add(newfreeValue)
                                End If
                            End If

                        Case GetType(RichTextValueCustomBankPropertyValueEntity).ToString
                            Dim richTextValue As RichTextValueCustomBankPropertyValueEntity = DirectCast(changedProp, RichTextValueCustomBankPropertyValueEntity)
                            Dim isEmptyRichTextValue As Boolean = (String.IsNullOrEmpty(richTextValue.Value) OrElse Cito.Tester.Common.HtmlHelper.IsEmptyHtml(richTextValue.Value))

                            Dim existing As Boolean = False
                            For Each prop As CustomBankPropertyValueEntity In itemResourceEntity.CustomBankPropertyValueCollection
                                If prop.CustomBankPropertyId = richTextValue.CustomBankPropertyId Then
                                    If Not isEmptyRichTextValue Then
                                        Dim newRichTextValue As RichTextValueCustomBankPropertyValueEntity = DirectCast(prop, RichTextValueCustomBankPropertyValueEntity)
                                        newRichTextValue.Value = richTextValue.Value
                                        newRichTextValue.SetCustomPropertyDisplayValue(customProperty, knownCustomProperties)
                                    Else
                                        itemResourceEntity.CustomBankPropertyValueCollection.Remove(prop)
                                        itemResourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(prop)
                                    End If
                                    existing = True
                                    Exit For
                                End If
                            Next

                            If Not existing Then
                                If Not isEmptyRichTextValue Then
                                    Dim newRichTextValue As New RichTextValueCustomBankPropertyValueEntity
                                    newRichTextValue.IsNew = True
                                    newRichTextValue.ResourceId = itemResourceEntity.ResourceId
                                    newRichTextValue.CustomBankPropertyId = richTextValue.CustomBankPropertyId
                                    newRichTextValue.Value = richTextValue.Value
                                    newRichTextValue.SetCustomPropertyDisplayValue(customProperty, knownCustomProperties)
                                    itemResourceEntity.CustomBankPropertyValueCollection.Add(newRichTextValue)
                                Else

                                End If
                            End If

                        Case GetType(ListCustomBankPropertyValueEntity).ToString
                            Dim listValue As ListCustomBankPropertyValueEntity = DirectCast(changedProp, ListCustomBankPropertyValueEntity)

                            Dim existing As Boolean = False
                            For Each prop As CustomBankPropertyValueEntity In itemResourceEntity.CustomBankPropertyValueCollection
                                If prop.CustomBankPropertyId = listValue.CustomBankPropertyId Then
                                    If listValue.ListCustomBankPropertySelectedValueCollection.Count > 0 Then
                                        Dim newList As ListCustomBankPropertyValueEntity = DirectCast(prop, ListCustomBankPropertyValueEntity)
                                        Dim existingValues As New List(Of ListValueCustomBankPropertyEntity)
                                        If Not overwriteExistingValues Then
                                            existingValues = newList.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue.ToList
                                        End If

                                        Dim entColl As New EntityCollection()
                                        newList.ListCustomBankPropertySelectedValueCollection.Clear()
                                        itemResourceEntity = ResourceFactory.Instance.GetItem(itemResourceEntity, New ResourceRequestDTO() With {.WithCustomProperties = True})

                                        For Each listCustomBankPropertyValue As ListCustomBankPropertyValueEntity In itemResourceEntity.CustomBankPropertyValueCollection.OfType(Of ListCustomBankPropertyValueEntity).Where(Function(x) x.CustomBankPropertyId = newList.CustomBankPropertyId)
                                            entColl.AddRange(listCustomBankPropertyValue.ListCustomBankPropertySelectedValueCollection)
                                        Next

                                        BankFactory.Instance.DeleteCustomPropertyValues(entColl)

                                        For Each listitem As ListCustomBankPropertySelectedValueEntity In listValue.ListCustomBankPropertySelectedValueCollection
                                            Dim newlistitem As New ListCustomBankPropertySelectedValueEntity
                                            newlistitem.ResourceId = itemResourceEntity.ResourceId
                                            newlistitem.CustomBankPropertyId = listValue.CustomBankPropertyId
                                            newlistitem.ListValueBankCustomPropertyId = listitem.ListValueBankCustomPropertyId
                                            newList.ListCustomBankPropertySelectedValueCollection.Add(newlistitem)
                                        Next

                                        For Each existingValue In existingValues
                                            If Not newList.ListCustomBankPropertySelectedValueCollection.Any(Function(c) c.ListValueBankCustomPropertyId = existingValue.ListValueBankCustomPropertyId AndAlso c.CustomBankPropertyId = existingValue.CustomBankPropertyId) Then
                                                Dim newlistitem As New ListCustomBankPropertySelectedValueEntity
                                                newlistitem.ResourceId = itemResourceEntity.ResourceId
                                                newlistitem.CustomBankPropertyId = existingValue.CustomBankPropertyId
                                                newlistitem.ListValueBankCustomPropertyId = existingValue.ListValueBankCustomPropertyId
                                                newList.ListCustomBankPropertySelectedValueCollection.Add(newlistitem)
                                            End If
                                        Next

                                        newList.SetCustomPropertyDisplayValue(customProperty, knownCustomProperties)
                                        itemResourceEntity.CustomBankPropertyValueCollection.Add(newList)
                                    Else
                                        If Me.OverwriteExistingCPListMultiValuesCheckbox.Checked Then
                                            itemResourceEntity.CustomBankPropertyValueCollection.Remove(prop)
                                            itemResourceEntity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(prop)
                                        End If
                                    End If

                                    existing = True
                                    Exit For
                                End If
                            Next

                            If Not existing Then
                                If listValue.ListCustomBankPropertySelectedValueCollection.Count > 0 Then
                                    Dim newList As New ListCustomBankPropertyValueEntity
                                    newList.IsNew = True
                                    newList.ResourceId = itemResourceEntity.ResourceId
                                    newList.CustomBankPropertyId = listValue.CustomBankPropertyId

                                    For Each listitem As ListCustomBankPropertySelectedValueEntity In listValue.ListCustomBankPropertySelectedValueCollection
                                        Dim newlistitem As New ListCustomBankPropertySelectedValueEntity
                                        newlistitem.ResourceId = itemResourceEntity.ResourceId
                                        newlistitem.CustomBankPropertyId = listValue.CustomBankPropertyId
                                        newlistitem.ListValueBankCustomPropertyId = listitem.ListValueBankCustomPropertyId
                                        newList.ListCustomBankPropertySelectedValueCollection.Add(newlistitem)
                                    Next

                                    newList.SetCustomPropertyDisplayValue(customProperty, knownCustomProperties)
                                    itemResourceEntity.CustomBankPropertyValueCollection.Add(newList)
                                End If
                            End If
                    End Select
                End If
            ElseIf MetaDataGrid.SelectedItems.Count > 0 Then
                If MetaDataGrid.SelectedItems(0).GetRow().RowIndex = 0 Then
                    itemResourceEntity.StateId = _clonedItemResourceEntity.StateId

                    If Not requestedMajorVersionComment AndAlso itemResourceEntity.RequiresMajorVersionIncrement() Then
                        Dim windowfacade As IWindowFacade = New WindowFacade()
                        Dim versionResult = windowfacade.OpenMajorVersionDialog(_clonedItemResourceEntity)

                        If versionResult Then
                            requestedMajorVersionComment = True
                        Else
                            e.Cancel = True
                            Exit For
                        End If

                    End If


                    If (itemResourceEntity.RequiresMajorVersionIncrement) Then
                        itemResourceEntity.MajorVersionLabel = _clonedItemResourceEntity.MajorVersionLabel
                        itemResourceEntity.Version = GetNewVersion(itemResourceEntity)
                    End If
                Else
                    itemResourceEntity.Description = _clonedItemResourceEntity.Description
                End If
            End If

            Dim result As String = ResourceFactory.Instance.UpdateItemResource(itemResourceEntity)

            ProcessTabContent.ProgressValueDetail += 1
            Application.DoEvents()
        Next

    End Sub

    Private Function GetNewVersion(ByVal entity As ResourceEntity) As String
        Dim currVersion As Version = Nothing
        Dim success = Version.TryParse(entity.Version, currVersion)

        If success Then
            Return (currVersion.Major + 1).ToString()
        End If

        Return "1"
    End Function

    Private Sub MultiSelectItemEditWizardForm_WizardTabChanged(ByVal sender As Object, ByVal e As WizardTabChangedEventArgs) Handles Me.WizardTabChanged
        Select Case e.CurrentTab.Tag.ToString
            Case "WelcomeTab"
                WelcomeTabContent.Title = My.Resources.MultiSelectItemEditWizardWelcomeTitle
                WelcomeTabContent.Description = My.Resources.MultiSelectItemEditWizardWelcomeDescription

            Case "SelectEdit"
                SelectEditTabContent.Task = My.Resources.MultiSelectItemEditWizardSelectEditTitle
                SelectEditTabContent.TaskDescription = String.Format(My.Resources.MultiSelectItemEditWizardSelectEditDescription, Me.SelectedEntities.Count.ToString)

            Case "Overwrite"
                OverwriteTabContent.Task = My.Resources.MultiSelectItemEditWizardOverwriteTitle
                OverwriteTabContent.TaskDescription = String.Format(My.Resources.MultiSelectItemEditWizardOverwriteDescription, Me.SelectedEntities.Count.ToString)

            Case "Edit"
                EditTabContent.Task = My.Resources.MultiSelectItemEditWizardEditTitle
                EditTabContent.TaskDescription = String.Format(My.Resources.MultiSelectItemEditWizardEditDescription, Me.SelectedEntities.Count.ToString)

            Case "OverviewTab"
                OverviewTabContent.Task = My.Resources.CompleteTheWizard
                OverviewTabContent.TaskDescription = My.Resources.VerifyTheChoicesMadeInTheWizard

            Case "ProcessTab"
                ProcessTabContent.Task = My.Resources.PerformingOperation
                ProcessTabContent.TaskDescription = String.Empty

            Case "ResultTab"
                ResultTabContent.Task = My.Resources.OperationSuccesfull
                ResultTabContent.TaskDescription = String.Format(My.Resources.MultiSelectItemEditWizardResultDescription, Me.SelectedEntities.Count.ToString)

        End Select
    End Sub


    Private Property SelectedEntities() As List(Of ItemResourceEntity)
        Get
            Return _selectedEntitiesNew
        End Get
        Set(ByVal value As List(Of ItemResourceEntity))
            _selectedEntitiesNew = value
        End Set
    End Property



    Private Sub FillMetaDataGrid()
        Dim itemMetaDataDatatable As New DataTable

        itemMetaDataDatatable.Columns.Add("name")

        Dim primKey(0) As DataColumn
        primKey(0) = itemMetaDataDatatable.Columns("name")
        itemMetaDataDatatable.PrimaryKey = primKey

        Dim newDatarow As DataRow = itemMetaDataDatatable.NewRow
        newDatarow.Item("name") = My.Resources.MetaDataStatus
        itemMetaDataDatatable.Rows.Add(newDatarow)

        newDatarow = itemMetaDataDatatable.NewRow
        newDatarow.Item("name") = My.Resources.MetaDataDescription
        itemMetaDataDatatable.Rows.Add(newDatarow)

        MetaDataGrid.DataSource = itemMetaDataDatatable
    End Sub

    Private Function GetPropertiesOfCurrentBankAndParentBanks(ByVal bankId As Integer) As EntityCollection(Of CustomBankPropertyEntity)
        Dim properties As EntityCollection(Of CustomBankPropertyEntity) = New EntityCollection(Of CustomBankPropertyEntity)
        Dim readBank As BankEntity = BankFactory.Instance.GetBankWithOptions(bankId, False, True)

        For Each customBankProperty As CustomBankPropertyEntity In readBank.CustomBankPropertyCollection
            properties.Add(customBankProperty)
        Next

        If (readBank.ParentBankId IsNot Nothing) Then
            properties.AddRange(GetPropertiesOfCurrentBankAndParentBanks(readBank.ParentBankId.Value))
        End If

        Return properties
    End Function

    Private Sub FillCustomPropertiesGrid()
        AvailablePropertiesGrid.DataSource = GetPropertiesOfCurrentBankAndParentBanks(BankId).Where(Function(c) ((Convert.ToInt32(c.ApplicableToMask) And ResourceTypeEnum.ItemResource) = ResourceTypeEnum.ItemResource) AndAlso Not TypeOf (c) Is TreeStructureCustomBankPropertyEntity AndAlso Not TypeOf (c) Is ConceptStructureCustomBankPropertyEntity).ToList
    End Sub

    Private Sub FillParametersGrid()
        Dim itemParameterDatatable As New DataTable

        itemParameterDatatable.Columns.Add("group")
        itemParameterDatatable.Columns.Add("label")
        itemParameterDatatable.Columns.Add("paramColId")

        Dim primKey(1) As DataColumn
        primKey(0) = itemParameterDatatable.Columns("group")
        primKey(1) = itemParameterDatatable.Columns("label")

        itemParameterDatatable.PrimaryKey = primKey

        Dim itemLayoutTemplateNames() As String = SelectedEntities.Select(Function(x) GetExistingAssessmentItem(x).LayoutTemplateSourceName).Distinct().ToArray()

        If itemLayoutTemplateNames.Length = 1 Then
            Dim extractedParameterSets As ParameterSetCollection = Nothing

            Dim adapter As New ItemLayoutAdapter(itemLayoutTemplateNames(0), Nothing, AddressOf ItemParameterGrid_ResourceNeeded)

            extractedParameterSets = adapter.CreateParameterSetsFromItemTemplate()
            TableLayoutPanelSelectEdit.SetRowSpan(AvailableParametersGrid, 4)

            For Each paramCollection As ParameterCollection In extractedParameterSets
                If paramCollection.InnerParameters IsNot Nothing Then
                    For Each parameter As ParameterBase In paramCollection.InnerParameters
                        Dim keyFind(1) As String
                        Dim visible As String = parameter.DesignerSettings.GetSettingValueByKey("visible")
                        Dim redirected As String = parameter.DesignerSettings.GetSettingValueByKey("redirectEnabled")

                        If DirectCast(paramCollection.GetParameterByName(parameter.Name), ParameterBase) IsNot Nothing Then
                            visible = DirectCast(paramCollection.GetParameterByName(parameter.Name), ParameterBase).DesignerSettings.GetSettingValueByKey("visible")
                            keyFind(0) = parameter.DesignerSettings.GetSettingValueByKey("group")
                            keyFind(1) = parameter.DesignerSettings.GetSettingValueByKey("label")

                            If keyFind(0) Is Nothing Then keyFind(0) = "-"
                            If keyFind(1) Is Nothing Then keyFind(1) = parameter.DesignerSettings.GetSettingValueByKey("itemcountlabel")
                        End If

                        If Not String.IsNullOrEmpty(visible) AndAlso String.Equals(visible, Boolean.FalseString, StringComparison.OrdinalIgnoreCase) Then
                        ElseIf String.Compare(redirected, Boolean.TrueString, False) = 0 Then
                        Else
                            Dim existingDataRow As DataRow = itemParameterDatatable.Rows.Find(keyFind)
                            If existingDataRow Is Nothing Then
                                Dim newDatarow As DataRow = itemParameterDatatable.NewRow
                                newDatarow.Item("group") = keyFind(0)
                                newDatarow.Item("label") = keyFind(1)
                                newDatarow.Item("paramColId") = paramCollection.Id
                                itemParameterDatatable.Rows.Add(newDatarow)
                            End If
                        End If
                    Next
                End If
            Next

            AvailableParametersGrid.DataSource = itemParameterDatatable
        Else
            LabelParameters.Text = "Parameters zijn niet wijzigbaar bij meerdere itemlay-outtemplates."
            LabelParameters.ForeColor = Color.Red

            AvailableParametersGrid.Enabled = False
            TableLayoutPanelSelectEdit.SetRowSpan(AvailableParametersGrid, 4)
        End If
    End Sub

    Private Sub FillMetaDataEditor()
        If MetaDataGrid.SelectedItems(0).GetRow().RowIndex = 0 Then
            ResourceMetaDataMultiEditInstance.DisplayField = ResourceMetaDataMultiEdit.FieldName.State
        Else
            ResourceMetaDataMultiEditInstance.DisplayField = ResourceMetaDataMultiEdit.FieldName.Description
        End If
        ResourceMetaDataMultiEditInstance.ResourceEntity = _clonedItemResourceEntity
    End Sub

    Private Sub FillResourceCustomPropertyEditor()
        Dim customProperty As CustomBankPropertyEntity = CType(AvailablePropertiesGrid.SelectedItems(0).GetRow().DataRow, CustomBankPropertyEntity)

        Dim propsToRemove(_clonedItemResourceEntity.CustomBankPropertyValueCollection.Count - 1) As Boolean
        Dim indexer As Integer = 0
        For Each prop As CustomBankPropertyValueEntity In _clonedItemResourceEntity.CustomBankPropertyValueCollection
            If prop.CustomBankPropertyId <> customProperty.CustomBankPropertyId Then
                propsToRemove(indexer) = True
            End If
            indexer += 1
        Next
        For i As Integer = propsToRemove.GetUpperBound(0) To 0 Step -1
            If propsToRemove(i) = True Then
                _clonedItemResourceEntity.CustomBankPropertyValueCollection.RemoveAt(i)
            Else
                Dim propToClear As CustomBankPropertyValueEntity = _clonedItemResourceEntity.CustomBankPropertyValueCollection(i)
                Select Case propToClear.GetType.ToString
                    Case GetType(FreeValueCustomBankPropertyValueEntity).ToString
                        Dim freeValue As FreeValueCustomBankPropertyValueEntity = DirectCast(propToClear, FreeValueCustomBankPropertyValueEntity)
                        freeValue.Value = String.Empty
                    Case GetType(RichTextValueCustomBankPropertyValueEntity).ToString
                        Dim richTextValue As RichTextValueCustomBankPropertyValueEntity = DirectCast(propToClear, RichTextValueCustomBankPropertyValueEntity)
                        richTextValue.Value = String.Empty
                    Case GetType(ListCustomBankPropertyValueEntity).ToString
                        Dim listValue As ListCustomBankPropertyValueEntity = DirectCast(propToClear, ListCustomBankPropertyValueEntity)
                        listValue.ListCustomBankPropertySelectedValueCollection.Clear()
                End Select
            End If
        Next

        resourceCustomPropsEditor.CustomPropertyFilter = customProperty
        resourceCustomPropsEditor.CustomPropertyTypeFilter = ResourceTypeEnum.ItemResource
        resourceCustomPropsEditor.ResourceEntity = CType(_clonedItemResourceEntity, ResourceEntity)
    End Sub

    Private Sub FillParameterSetsEditor()
        Dim assessmentItem As AssessmentItem = GetExistingAssessmentItem(_clonedItemResourceEntity)
        Dim extractedParameterSets As ParameterSetCollection = Nothing
        Dim itemSetupHelper = New AssessmentItemHelper(_resourceManager, assessmentItem.LayoutTemplateSourceName, _clonedItemResourceEntity, Nothing)

        Dim adapter As New ItemLayoutAdapter(assessmentItem.LayoutTemplateSourceName, Nothing, AddressOf ItemParameterGrid_ResourceNeeded)

        extractedParameterSets = adapter.CreateParameterSetsFromItemTemplate()

        Dim dataRow As DataRowView = DirectCast(AvailableParametersGrid.SelectedItems(0).GetRow.DataRow, DataRowView)
        Dim paramColId As String = dataRow.Row.Item("paramColId").ToString
        Dim paramGroup As String = dataRow.Row.Item("group").ToString
        Dim paramLabel As String = dataRow.Row.Item("label").ToString
        Dim param As ParameterBase = Nothing

        For Each paramCollection As ParameterCollection In extractedParameterSets
            If paramCollection.Id = paramColId Then
                If paramCollection.InnerParameters IsNot Nothing Then
                    For Each parameter As ParameterBase In paramCollection.InnerParameters

                        If (parameter.DesignerSettings.GetSettingValueByKey("group") = paramGroup OrElse paramGroup = "-") AndAlso
                         (parameter.DesignerSettings.GetSettingValueByKey("label") = paramLabel OrElse parameter.DesignerSettings.GetSettingValueByKey("itemcountlabel") = paramLabel) Then
                            param = parameter
                            param.SetValue(String.Empty)
                            Exit For
                        End If
                    Next
                End If
            End If
        Next

        paramSetsEditor.ContextIdentifierForEditors = _contextIdentifier
        paramSetsEditor.ResourceEntity = _clonedItemResourceEntity
        paramSetsEditor.FilterParameter = param
        paramSetsEditor.ResourceManager = _resourceManager
        paramSetsEditor.HasLoadedOldItemLayoutTemplate = itemSetupHelper.IsTransFormedTemplate
        paramSetsEditor.ParameterSets = extractedParameterSets

        For Each paramCollection As ParameterCollection In extractedParameterSets
            If paramCollection.InnerParameters IsNot Nothing Then
                For Each parameter As ParameterBase In paramCollection.InnerParameters
                    If String.Compare(parameter.DesignerSettings.GetSettingValueByKey("groupConditionalEnabled"), Boolean.TrueString, False) = 0 Then
                        Dim groupEnabledWhenValue As String = parameter.DesignerSettings.GetSettingValueByKey("groupConditionalEnabledWhenValue")
                        parameter.SetValue(groupEnabledWhenValue)
                    End If

                    If String.Compare(parameter.DesignerSettings.GetSettingValueByKey("conditionalEnabled"), Boolean.TrueString, True) = 0 Then
                        Dim switchedByParameterKey As String = parameter.DesignerSettings.GetSettingValueByKey("conditionalEnabledSwitchParameter")
                        Dim enabledWhenValue As String = GetEnableWhenValue(parameter.DesignerSettings.GetSettingValueByKey("conditionalEnabledWhenValue"))
                        paramCollection.InnerParameters.Find(Function(x) x.Name = switchedByParameterKey).SetValue(enabledWhenValue)
                    End If
                Next
            End If
        Next

        Application.DoEvents()
    End Sub

    Private Function GetEnableWhenValue(conditionalEnabledWhenValue As String) As String
        Dim result As String = String.Empty
        If conditionalEnabledWhenValue.Contains(CONDITIONAL_OR) Then
            Dim multipleResults As String() = conditionalEnabledWhenValue.Split(CONDITIONAL_OR)
            If Not String.IsNullOrEmpty(multipleResults(0)) Then result = GetEnableWhenValue(multipleResults(0))
        ElseIf conditionalEnabledWhenValue.Contains(CONDITIONAL_NOT) AndAlso conditionalEnabledWhenValue.Contains(CONDITIONAL_EMPTY) Then
            result = CONDITIONAL_DUMMYVALUE
        ElseIf conditionalEnabledWhenValue.Contains(CONDITIONAL_NOT) Then
            result = CONDITIONAL_DUMMYVALUE
        ElseIf conditionalEnabledWhenValue.Contains(CONDITIONAL_EMPTY) Then
            result = String.Empty
        Else
            result = conditionalEnabledWhenValue
        End If
        Return result
    End Function

    Private Sub UpdateRedirectedParameters(ByVal sourceParamSetCollection As ParameterSetCollection, ByVal paramCollectionIdOfChangedParam As String, ByVal changedParam As ParameterBase, ByVal targetItemParamCollection As ParameterSetCollection)

        For Each paramCollection As ParameterCollection In sourceParamSetCollection
            Dim redirectedParams = paramCollection.InnerParameters.Where(Function(x) x.DesignerSettings.GetSettingValueByKey("redirectToTargetControlId") = paramCollectionIdOfChangedParam AndAlso x.DesignerSettings.GetSettingValueByKey("redirectToTargetParameterId") = changedParam.Name).ToList()

            For Each redirectedParam As ParameterBase In redirectedParams
                Dim targetRedirectedParamCollection As ParameterCollection = targetItemParamCollection.GetParamCollectionByControlId(paramCollection.Id)
                Dim targetRedirectedParam As ParameterBase = targetItemParamCollection.GetParameter(redirectedParam.Name, paramCollection.Id)
                If targetRedirectedParam IsNot Nothing Then
                    Dim i As Integer = targetRedirectedParamCollection.InnerParameters.IndexOf(targetRedirectedParam)
                    If i > -1 Then
                        Dim newParameter As ParameterBase = SerializeHelper.XmlSerializableClone(changedParam)
                        newParameter.Name = targetRedirectedParam.Name
                        targetRedirectedParamCollection.InnerParameters(i) = newParameter

                        UpdateRedirectedParameters(sourceParamSetCollection, paramCollection.Id, newParameter, targetItemParamCollection)
                    End If
                End If
            Next
        Next

    End Sub



    Private Function GetExistingAssessmentItem(ByVal itemResource As ItemResourceEntity) As AssessmentItem
        Return itemResource.GetAssessmentItem()
    End Function

    Private Sub ItemParameterGrid_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    Private Sub AvailableParametersGrid_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles AvailableParametersGrid.MouseClick
        AvailablePropertiesGrid.SelectedItems.Clear()
        MetaDataGrid.SelectedItems.Clear()
    End Sub

    Private Sub AvailablePropertiesGrid_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles AvailablePropertiesGrid.MouseClick
        AvailableParametersGrid.SelectedItems.Clear()
        MetaDataGrid.SelectedItems.Clear()
    End Sub

    Private Sub MetaDataGrid_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MetaDataGrid.MouseClick
        AvailableParametersGrid.SelectedItems.Clear()
        AvailablePropertiesGrid.SelectedItems.Clear()
    End Sub


    Private Sub AddResource(ByVal itemResourceEntity As ItemResourceEntity, ByVal name As String)
        If Not String.IsNullOrEmpty(name) Then
            Dim resourceEntity As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(itemResourceEntity.Bank.Id, name, New ResourceRequestDTO())
            If TypeOf (resourceEntity) Is GenericResourceEntity Then
                Dim referencedResource As GenericResourceEntity = DirectCast(resourceEntity, GenericResourceEntity)
                If Not itemResourceEntity.ContainsDependentResource(referencedResource) Then
                    Dim depResource As New DependentResourceEntity()
                    depResource.Resource = itemResourceEntity
                    depResource.DependentResource = referencedResource
                    itemResourceEntity.DependentResourceCollection.Add(depResource)
                End If
            ElseIf TypeOf (resourceEntity) Is ItemLayoutTemplateResourceEntity Then
                Dim referencedResource As ItemLayoutTemplateResourceEntity = DirectCast(resourceEntity, ItemLayoutTemplateResourceEntity)
                If Not itemResourceEntity.ContainsDependentResource(referencedResource) Then
                    Dim depResource As New DependentResourceEntity()
                    depResource.Resource = itemResourceEntity
                    depResource.DependentResource = referencedResource
                    itemResourceEntity.DependentResourceCollection.Add(depResource)
                End If
            End If
        End If
    End Sub

    Private Sub RemoveResource(ByVal itemResourceEntity As ItemResourceEntity, ByVal prarams As ParameterSetCollection, ByVal name As String)
        If (Not String.IsNullOrEmpty(name)) Then
            Dim dependentResource As DependentResourceEntity = itemResourceEntity.GetDependentResourceByName(name)
            If dependentResource Is Nothing Then
                Throw New UIException($"Dependent resource could not be found in collection for entity. Name: '{name}'")
            End If

            Dim cnt As Long = 0
            cnt = prarams.LongCount(Function(pc As ParameterCollection)
                                        Return pc.InnerParameters.Any(Function(pb As ParameterBase)
                                                                          Return (TypeOf pb Is ResourceParameter) AndAlso DirectCast(pb, ResourceParameter).Value = name
                                                                      End Function)
                                    End Function)
            Debug.Assert(cnt >= 1)
            If (cnt = 1) Then
                itemResourceEntity.DependentResourceCollection.Remove(dependentResource)
            End If

        End If
    End Sub

End Class