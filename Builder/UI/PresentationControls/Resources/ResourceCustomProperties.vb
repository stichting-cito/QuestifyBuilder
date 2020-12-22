Imports System.Linq
Imports System.ComponentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.Text
Imports Enums
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories

Public Class ResourceCustomProperties

    Const FREEVALUETAG As String = "freevalue"


    Private _entity As ResourceEntity
    Private _removedEntities As New EntityCollection
    Private _resourceManager As DataBaseResourceManager = Nothing
    Private _customPropertyValuesBeforeEditing As Dictionary(Of Guid, HashSet(Of String)) = Nothing
    Private _customProperties As EntityCollection = Nothing



    <Description("This event will be raised when data is changed on this control"), Category("ResourceMetaData Control events")>
    Public Event DataChanged As EventHandler(Of EventArgs)

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub



    <Description("Filter custom properties"), DefaultValue(ResourceTypeEnum.None), Bindable(False), Category("Custom Property Specific")>
    Public Property CustomPropertyTypeFilter As ResourceTypeEnum

    Public Property CustomPropertyFilter As CustomBankPropertyEntity

    Public Property ResourceEntity() As ResourceEntity
        Get
            Return _entity
        End Get
        Set(ByVal value As ResourceEntity)
            If value IsNot Nothing Then
                If _entity Is Nothing OrElse Not _entity.Equals(value) Then
                    _entity = value
                    CreateCustomPropertyEditors()
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property RemovedEntities() As EntityCollection
        Get
            Return _removedEntities
        End Get
    End Property



    Public Sub DoPostSaveTasks()
        ResetFreeTextEditorHandlers()
        _customPropertyValuesBeforeEditing = Nothing
    End Sub



    Private Sub CreateCustomPropertyEditors()
        _customProperties = BankFactory.Instance.GetCustomBankPropertiesForBranchById(Me.ResourceEntity.BankId, Me.CustomPropertyTypeFilter)

        CustomPropertiesTableLayoutPanel.SuspendLayout()
        CustomPropertiesTableLayoutPanel.Controls.Clear()

        For Each customProperty As CustomBankPropertyEntity In _customProperties
            Dim cont As Boolean = False

            If CustomPropertyFilter IsNot Nothing Then
                If customProperty.Name = CustomPropertyFilter.Name Then
                    cont = True
                End If
            Else
                cont = True
            End If

            If cont Then
                Select Case customProperty.GetType.ToString
                    Case GetType(FreeValueCustomBankPropertyEntity).ToString
                        Dim freeValue As FreeValueCustomBankPropertyEntity = DirectCast(customProperty, FreeValueCustomBankPropertyEntity)
                        AddFreeTextEditor(customProperty.CustomBankPropertyId.ToString, freeValue.Title, freeValue)
                    Case GetType(ListCustomBankPropertyEntity).ToString
                        Dim listValue As ListCustomBankPropertyEntity = DirectCast(customProperty, ListCustomBankPropertyEntity)
                        AddListEditor(customProperty.CustomBankPropertyId.ToString, listValue.Title, listValue)
                    Case GetType(TreeStructureCustomBankPropertyEntity).ToString
                        If Not TypeOf _entity Is ItemResourceEntity Then
                            Dim treeValue As TreeStructureCustomBankPropertyEntity = DirectCast(customProperty, TreeStructureCustomBankPropertyEntity)
                            AddTreeEditor(customProperty.CustomBankPropertyId.ToString, treeValue.Title, treeValue)
                        End If
                    Case GetType(RichTextValueCustomBankPropertyEntity).ToString
                        Dim richTextValue As RichTextValueCustomBankPropertyEntity = DirectCast(customProperty, RichTextValueCustomBankPropertyEntity)
                        AddRichTextEditor(customProperty.CustomBankPropertyId.ToString, richTextValue.Title, richTextValue)
                End Select
            End If
        Next

        CustomPropertiesTableLayoutPanel.ResumeLayout(False)
        CustomPropertiesTableLayoutPanel.PerformLayout()
    End Sub

    Private Sub AddFreeTextEditor(ByVal id As String, ByVal Caption As String, ByVal freeValue As FreeValueCustomBankPropertyEntity)
        Dim captionLabel As New Label
        Dim propertyTextbox As New TextBox

        With captionLabel
            .Name = String.Format("customPropCaption_{0}", id)
            .AutoSize = True
            .Anchor = AnchorStyles.Left
            .Text = Caption
        End With

        With propertyTextbox
            .Name = String.Format("CustomPropField_{0}", id)
            .AutoSize = True
            .Anchor = AnchorStyles.Left Or AnchorStyles.Right
            .Tag = FREEVALUETAG
            .MaxLength = 255
        End With

        Dim filter As IPredicate
        Dim indexes As List(Of Integer)

        filter = (CustomBankPropertyValueFields.CustomBankPropertyId = freeValue.CustomBankPropertyId)
        indexes = _entity.CustomBankPropertyValueCollection.FindMatches(filter)

        Dim value As FreeValueCustomBankPropertyValueEntity

        If indexes.Count = 1 Then
            value = DirectCast(_entity.CustomBankPropertyValueCollection(indexes(0)), FreeValueCustomBankPropertyValueEntity)
        Else
            value = New FreeValueCustomBankPropertyValueEntity
            value.CustomBankPropertyId = freeValue.CustomBankPropertyId
            value.Value = Nothing
        End If

        AddHandler propertyTextbox.TextChanged, AddressOf FreeTextEditor_OnTextChanged

        If value IsNot Nothing Then
            Dim binding As New Binding("Text", value, "value", False, DataSourceUpdateMode.OnPropertyChanged)
            propertyTextbox.DataBindings.Add(binding)

            CustomPropertiesTableLayoutPanel.Controls.Add(captionLabel)
            CustomPropertiesTableLayoutPanel.SetColumnSpan(propertyTextbox, 2)
            CustomPropertiesTableLayoutPanel.Controls.Add(propertyTextbox)
        End If
    End Sub

    Private Sub FreeTextEditor_OnTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim value As FreeValueCustomBankPropertyValueEntity = CType(CType(sender, Control).DataBindings("Text").DataSource, FreeValueCustomBankPropertyValueEntity)
        value.SetCustomPropertyDisplayValue(CType(sender, Control).Text)

        If Not _entity.CustomBankPropertyValueCollection.Contains(value) Then
            _entity.CustomBankPropertyValueCollection.Add(value)
        End If
    End Sub

    Private Sub ResetFreeTextEditorHandlers()
        For Each freevalueTB In CustomPropertiesTableLayoutPanel.Controls.OfType(Of TextBox).Where(Function(tb) tb.Tag IsNot Nothing AndAlso tb.Tag.ToString = FREEVALUETAG)
            RemoveHandler freevalueTB.TextChanged, AddressOf FreeTextEditor_OnTextChanged
            AddHandler freevalueTB.TextChanged, AddressOf FreeTextEditor_OnTextChanged
        Next
    End Sub

    Private Function AddControlSet(
              ByVal id As String,
              ByVal caption As String,
              ByVal treeValue As Object,
              text As String) As Button
        Dim captionLabel As New Label
        Dim propertyLabel As New TextBox
        Dim editButton As New Button

        With captionLabel
            .Name = String.Format("customPropCaption_{0}", id)
            .AutoSize = True
            .Anchor = AnchorStyles.Left
            .Text = caption
        End With

        With propertyLabel
            .Name = String.Format("customPropField_{0}", id)
            .AutoSize = True
            .Anchor = AnchorStyles.Left Or AnchorStyles.Right
            .BorderStyle = BorderStyle.Fixed3D
            .Text = text
            .ReadOnly = True
        End With

        With editButton
            .Name = String.Format("customPropButton_{0}", id)
            .Text = "..."
            .AutoSize = True
            .AutoSizeMode = AutoSizeMode.GrowAndShrink
            .Tag = treeValue
        End With

        CustomPropertiesTableLayoutPanel.Controls.Add(captionLabel)
        CustomPropertiesTableLayoutPanel.Controls.Add(propertyLabel)
        CustomPropertiesTableLayoutPanel.Controls.Add(editButton)
        Return editButton
    End Function

    Private Sub AddTreeEditor(ByVal id As String, ByVal caption As String, ByVal treeValue As TreeStructureCustomBankPropertyEntity)
        Dim text as String = GetDisplayTextForCustomProperty(treeValue, False)
        Dim btn = AddControlSet(id, caption, treeValue, text)
        AddHandler btn.Click, AddressOf EditTreeStructureButton_Click
    End Sub

    Private Sub AddListEditor(ByVal id As String, ByVal caption As String, ByVal listValue As ListCustomBankPropertyEntity)
        Dim text = GetDisplayTextForCustomProperty(listValue, False)
        Dim btn = AddControlSet(id, caption, listValue, text)
        AddHandler btn.Click, AddressOf EditListValueButton_Click
    End Sub

    Private Sub AddRichTextEditor(ByVal id As String, ByVal caption As String, ByVal richTextValue As RichTextValueCustomBankPropertyEntity)
        Dim text = GetDisplayTextForRichTextValue(richTextValue)
        Dim btn = AddControlSet(id, caption, richTextValue, text)
        AddHandler btn.Click, AddressOf EditRichTextValueButton_Click
    End Sub

    Private Sub SetTreeEditorValue(ByVal id As String, ByVal treeValue As TreeStructureCustomBankPropertyEntity)
        Dim name As String = String.Format("customPropField_{0}", id)
        Dim ctls() As Control = CustomPropertiesTableLayoutPanel.Controls.Find(name, False)

        For Each ctl As Control In ctls
            Dim txt As TextBox = TryCast(ctl, TextBox)
            txt.Text = GetDisplayTextForCustomProperty(treeValue, True)
        Next
    End Sub

    Private Sub SetListEditorValue(ByVal id As String, ByVal listValue As ListCustomBankPropertyEntity)
        Dim name As String = String.Format("customPropField_{0}", id)
        Dim ctls() As Control = CustomPropertiesTableLayoutPanel.Controls.Find(name, False)

        For Each ctl As Control In ctls
            Dim txt As TextBox = TryCast(ctl, TextBox)
            txt.Text = GetDisplayTextForCustomProperty(listValue, True)
        Next
    End Sub

    Private Sub SetRichTextEditorValue(ByVal id As String, ByVal richTextValue As RichTextValueCustomBankPropertyEntity)
        Dim name As String = String.Format("customPropField_{0}", id)
        Dim ctls() As Control = CustomPropertiesTableLayoutPanel.Controls.Find(name, False)

        For Each ctl As Control In ctls
            Dim txt As TextBox = TryCast(ctl, TextBox)
            txt.Text = GetDisplayTextForRichTextValue(richTextValue)
        Next
    End Sub

    Private Function GetDisplayTextForCustomProperty(cp As CustomBankPropertyEntity, updateAfterEdit As Boolean) As String
        Dim displayValue As String = String.Empty
        Dim selectedValue = _entity.CustomBankPropertyValueCollection.FirstOrDefault(Function(c) c.CustomBankPropertyId = cp.CustomBankPropertyId)
        If selectedValue IsNot Nothing Then
            If Not updateAfterEdit Then
                displayValue = selectedValue.DisplayValue
            ElseIf TypeOf cp Is ListCustomBankPropertyEntity Then
                displayValue = GetDisplayTextForSelectedListValues(CType(cp, ListCustomBankPropertyEntity))
            ElseIf TypeOf cp Is TreeStructureCustomBankPropertyEntity Then
                displayValue = GetDisplayTextForSelectedTrees(CType(cp, TreeStructureCustomBankPropertyEntity))
            End If
        End If
        Return displayValue
    End Function

    Private Function GetDisplayTextForSelectedTrees(ByVal treeValue As TreeStructureCustomBankPropertyEntity) As String
        Dim selectedValues As New StringBuilder()
        Dim filter As IPredicate
        Dim indexes As List(Of Integer)

        filter = (CustomBankPropertyValueFields.CustomBankPropertyId = treeValue.CustomBankPropertyId)
        indexes = _entity.CustomBankPropertyValueCollection.FindMatches(filter)

        If indexes.Count = 1 Then
            Dim value As TreeStructureCustomBankPropertyValueEntity = DirectCast(_entity.CustomBankPropertyValueCollection(indexes(0)), TreeStructureCustomBankPropertyValueEntity)

            For Each selectedValue As TreeStructureCustomBankPropertySelectedPartEntity In value.TreeStructureCustomBankPropertySelectedPartCollection
                Dim treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity = treeValue.TreeStructurePartCustomBankPropertyCollection.First(Function(i) i.TreeStructurePartCustomBankPropertyId = selectedValue.TreeStructurePartId)

                selectedValues.Append(String.Format("{0} - {1}", treeStructurePartCustomBankPropertyEntity.Name, treeStructurePartCustomBankPropertyEntity.Title))
                selectedValues.Append(";")
            Next
        End If

        Dim values = selectedValues.ToString().TrimEnd(";"c)

        _entity.CustomBankPropertyValueCollection.FirstOrDefault(Function(c) c.CustomBankPropertyId = treeValue.CustomBankPropertyId).SetCustomPropertyDisplayValue(values)

        Return values
    End Function

    Private Function GetDisplayTextForSelectedListValues(ByVal listValue As ListCustomBankPropertyEntity) As String
        Dim filter As IPredicate
        Dim indexes As List(Of Integer)
        Dim selectedValues As New StringBuilder

        filter = (CustomBankPropertyValueFields.CustomBankPropertyId = listValue.CustomBankPropertyId)
        indexes = _entity.CustomBankPropertyValueCollection.FindMatches(filter)

        Dim value As ListCustomBankPropertyValueEntity
        If indexes.Count = 1 Then
            value = DirectCast(_entity.CustomBankPropertyValueCollection(indexes(0)), ListCustomBankPropertyValueEntity)

            For Each selectedValue As ListCustomBankPropertySelectedValueEntity In value.ListCustomBankPropertySelectedValueCollection
                Dim valueFilter As IPredicate
                Dim valueIndexes As List(Of Integer)

                valueFilter = (ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId = selectedValue.ListValueBankCustomPropertyId)
                valueIndexes = listValue.ListValueCustomBankPropertyCollection.FindMatches(valueFilter)

                Dim displayText As String
                If valueIndexes.Count = 1 Then
                    displayText = listValue.ListValueCustomBankPropertyCollection(valueIndexes(0)).ToString()
                Else
                    displayText = selectedValue.ListValueBankCustomPropertyId.ToString
                End If

                If selectedValues.Length > 0 Then
                    selectedValues.Append(";")
                End If
                selectedValues.Append(displayText)
            Next
        End If

        _entity.CustomBankPropertyValueCollection.FirstOrDefault(Function(c) c.CustomBankPropertyId = listValue.CustomBankPropertyId).SetCustomPropertyDisplayValue(selectedValues.ToString())

        Return selectedValues.ToString
    End Function

    Private Function GetDisplayTextForRichTextValue(ByVal richTextValue As RichTextValueCustomBankPropertyEntity) As String
        Dim filter As IPredicate
        Dim indexes As List(Of Integer)
        filter = (CustomBankPropertyValueFields.CustomBankPropertyId = richTextValue.CustomBankPropertyId)
        indexes = _entity.CustomBankPropertyValueCollection.FindMatches(filter)
        Dim value As RichTextValueCustomBankPropertyValueEntity
        If indexes.Count = 1 Then
            value = DirectCast(_entity.CustomBankPropertyValueCollection(indexes(0)), RichTextValueCustomBankPropertyValueEntity)
            If Not String.IsNullOrEmpty(value.Value) AndAlso Not HtmlHelper.IsEmptyHtml(value.Value) Then
                Return value.Value.TruncateWithEllipsis(50)
            End If
        End If
        Return String.Empty
    End Function

    Private Function GetCustomPropertyValue(ByVal CustomBankPropertyId As Guid) As CustomBankPropertyValueEntity
        Dim filter As IPredicate
        Dim indexes As List(Of Integer)

        filter = (CustomBankPropertyValueFields.CustomBankPropertyId = CustomBankPropertyId)
        indexes = _entity.CustomBankPropertyValueCollection.FindMatches(filter)

        If indexes.Count = 1 Then
            Return _entity.CustomBankPropertyValueCollection(indexes(0))
        Else
            Return Nothing
        End If
    End Function

    Private Sub EditTreeStructureButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim clickedButton As Button = DirectCast(sender, Button)
        Dim treevalue As TreeStructureCustomBankPropertyEntity = DirectCast(clickedButton.Tag, TreeStructureCustomBankPropertyEntity)
        Dim value As TreeStructureCustomBankPropertyValueEntity = DirectCast(GetCustomPropertyValue(treevalue.CustomBankPropertyId), TreeStructureCustomBankPropertyValueEntity)

        Using editor As New TreeStructureEditor(treevalue, value)
            editor.StartPosition = FormStartPosition.CenterParent

            If value Is Nothing Then
                value = New TreeStructureCustomBankPropertyValueEntity()
                value.ResourceId = _entity.ResourceId
                value.CustomBankPropertyId = treevalue.CustomBankPropertyId
                _entity.CustomBankPropertyValueCollection.Add(value)
            End If

            If editor.ShowDialog() = DialogResult.OK Then
                value.TreeStructureCustomBankPropertySelectedPartCollection.RemovedEntitiesTracker = Me.RemovedEntities

                While value.TreeStructureCustomBankPropertySelectedPartCollection.Count > 0
                    value.TreeStructureCustomBankPropertySelectedPartCollection.RemoveAt(0)
                End While

                For Each tempValue As TreeStructurePartCustomBankPropertyEntity In editor.GetEntitiesOfCheckedCheckBoxesOfTreeView(editor.TreeStructureViewerUserControl1.TreeStructureTreeView.Nodes)
                    Dim filter As IPredicate = (TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId = tempValue.TreeStructurePartCustomBankPropertyId)
                    Dim indexes As List(Of Integer) = Me.RemovedEntities.FindMatches(filter)

                    If indexes.Count > 0 Then
                        Dim selected As TreeStructureCustomBankPropertySelectedPartEntity = DirectCast(Me.RemovedEntities(indexes(0)), TreeStructureCustomBankPropertySelectedPartEntity)
                        value.TreeStructureCustomBankPropertySelectedPartCollection.Add(selected)

                        Me.RemovedEntities.RemoveAt(indexes(0))
                    Else
                        Dim newSelected As New TreeStructureCustomBankPropertySelectedPartEntity
                        newSelected.CustomBankPropertyId = tempValue.CustomBankPropertyId
                        newSelected.TreeStructurePartId = tempValue.TreeStructurePartCustomBankPropertyId

                        value.TreeStructureCustomBankPropertySelectedPartCollection.Add(newSelected)
                    End If
                Next

                SetTreeEditorValue(treevalue.CustomBankPropertyId.ToString, treevalue)
                Me.OnDataChanged(New EventArgs)
            End If
        End Using
    End Sub

    Private Sub EditListValueButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim clickedButton As Button = DirectCast(sender, Button)
        Dim listvalue As ListCustomBankPropertyEntity
        Dim value As ListCustomBankPropertyValueEntity

        listvalue = DirectCast(clickedButton.Tag, ListCustomBankPropertyEntity)

        If _entity.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
            _entity.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
        End If

        value = DirectCast(GetCustomPropertyValue(listvalue.CustomBankPropertyId), ListCustomBankPropertyValueEntity)

        If value Is Nothing Then
            value = New ListCustomBankPropertyValueEntity
            value.ResourceId = _entity.ResourceId
            value.CustomBankPropertyId = listvalue.CustomBankPropertyId
        End If

        Using editor = New ListValueEditor
            editor.ListCustomBankProperty = listvalue
            editor.ListCustomBankPropertyValue = value

            editor.StartPosition = FormStartPosition.CenterParent

            If editor.ShowDialog() = DialogResult.OK Then

                Dim selectedListValuesHashset As New HashSet(Of String)
                Dim selectedListValues As EntityCollection(Of ListValueCustomBankPropertyEntity) = editor.GetSelectedListValues
                selectedListValues.ToList().ForEach(Sub(lv)
                                                        selectedListValuesHashset.Add(lv.ListValueBankCustomPropertyId.ToString())
                                                    End Sub)
                If Not editor.SelectedListValuesBeforeEditing.SetEquals(selectedListValuesHashset) Then
                    If value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker Is Nothing Then
                        value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker = New EntityCollection
                    End If

                    While value.ListCustomBankPropertySelectedValueCollection.Count > 0
                        value.ListCustomBankPropertySelectedValueCollection.RemoveAt(0)
                    End While

                    If selectedListValues.Count > 0 Then
                        For Each tempValue As ListValueCustomBankPropertyEntity In selectedListValues
                            Dim PropertyId As Guid = listvalue.CustomBankPropertyId
                            Dim ListValueId As Guid = tempValue.ListValueBankCustomPropertyId

                            Dim filter As IPredicate = (ListValueCustomBankPropertyFields.CustomBankPropertyId = PropertyId) And (ListValueCustomBankPropertyFields.ListValueBankCustomPropertyId = ListValueId)
                            Dim indexes As List(Of Integer) = value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker.FindMatches(filter)

                            If indexes.Count > 0 Then
                                Dim selected As ListCustomBankPropertySelectedValueEntity = DirectCast(value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker(indexes(0)), ListCustomBankPropertySelectedValueEntity)
                                value.ListCustomBankPropertySelectedValueCollection.Add(selected)

                                value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker.RemoveAt(indexes(0))
                            Else
                                Dim newSelected As New ListCustomBankPropertySelectedValueEntity
                                newSelected.CustomBankPropertyId = PropertyId
                                newSelected.ListValueBankCustomPropertyId = ListValueId

                                value.ListCustomBankPropertySelectedValueCollection.Add(newSelected)
                            End If
                        Next

                        If Not _entity.CustomBankPropertyValueCollection.Contains(value) Then
                            _entity.CustomBankPropertyValueCollection.Add(value)
                        End If
                    Else
                        If _entity.CustomBankPropertyValueCollection.Contains(value) Then
                            _entity.CustomBankPropertyValueCollection.Remove(value)
                        End If
                    End If

                    SetListEditorValue(listvalue.CustomBankPropertyId.ToString, listvalue)
                    Me.OnDataChanged(New EventArgs)
                End If

                AddCustomPropertyValuesBeforeEditing(listvalue.CustomBankPropertyId, editor.SelectedListValuesBeforeEditing)
                value.IsDirty = Not _customPropertyValuesBeforeEditing(listvalue.CustomBankPropertyId).SetEquals(selectedListValuesHashset)
                If Not value.IsDirty Then
                    If value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker IsNot Nothing Then
                        value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker.Clear()
                        value.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker = Nothing
                    End If

                    For Each listValueSelectedValue In value.ListCustomBankPropertySelectedValueCollection
                        listValueSelectedValue.IsDirty = False
                        listValueSelectedValue.IsNew = False
                    Next

                    Dim filter As IPredicate = (ListValueCustomBankPropertyFields.CustomBankPropertyId = listvalue.CustomBankPropertyId)
                    Dim indexes As List(Of Integer) = _entity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.FindMatches(filter)
                    If indexes.Count > 0 Then
                        _entity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.RemoveAt(indexes(0))
                    End If
                End If
            End If
        End Using
    End Sub

    Private Sub EditRichTextValueButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim clickedButton As Button = DirectCast(sender, Button)
        Dim richTextValue As RichTextValueCustomBankPropertyEntity
        Dim value As RichTextValueCustomBankPropertyValueEntity
        Dim oldValue As String = String.Empty

        richTextValue = DirectCast(clickedButton.Tag, RichTextValueCustomBankPropertyEntity)

        value = DirectCast(GetCustomPropertyValue(richTextValue.CustomBankPropertyId), RichTextValueCustomBankPropertyValueEntity)

        If value Is Nothing Then
            value = New RichTextValueCustomBankPropertyValueEntity
            value.ResourceId = _entity.ResourceId
            value.CustomBankPropertyId = richTextValue.CustomBankPropertyId
        Else
            oldValue = value.Value
        End If

        Using editor = New RichTextValueEditor
            editor.RichTextCustomBankProperty = richTextValue
            editor.RichTextCustomBankPropertyValue = value
            editor.InitEditor(_entity, GetResourceManager, Nothing)
            editor.StartPosition = FormStartPosition.CenterParent

            If editor.ShowDialog() = DialogResult.OK Then
                If Not String.IsNullOrEmpty(value.Value) AndAlso Not HtmlHelper.IsEmptyHtml(value.Value) Then
                    If Not _entity.CustomBankPropertyValueCollection.Contains(value) Then
                        _entity.CustomBankPropertyValueCollection.Add(value)
                    End If
                ElseIf _entity.CustomBankPropertyValueCollection.Contains(value) Then
                    _entity.CustomBankPropertyValueCollection.Remove(value)
                    If _entity.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                        _entity.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                    End If
                    _entity.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(value)
                End If

                value.IsDirty = True

                SetRichTextEditorValue(richTextValue.CustomBankPropertyId.ToString, richTextValue)

                Me.OnDataChanged(New EventArgs)
            Else
                value.Value = oldValue
            End If
        End Using
    End Sub

    Private Sub AddCustomPropertyValuesBeforeEditing(customPropertyId As Guid, valuesBeforeEditing As HashSet(Of String))
        If _customPropertyValuesBeforeEditing Is Nothing Then _customPropertyValuesBeforeEditing = New Dictionary(Of Guid, HashSet(Of String))
        If Not _customPropertyValuesBeforeEditing.ContainsKey(customPropertyId) Then
            _customPropertyValuesBeforeEditing.Add(customPropertyId, valuesBeforeEditing)
        End If
    End Sub

    Public Function GetResourceManager() As DataBaseResourceManager
        If _resourceManager Is Nothing Then
            _resourceManager = New DataBaseResourceManager(_entity.BankId)
        End If
        Return _resourceManager
    End Function




    Public Sub New()
        InitializeComponent()
    End Sub

End Class
