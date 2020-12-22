Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ParameterCollectionEditorControl


    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
    Public Event EditResource As EventHandler(Of ResourceNameEventArgs)



    Private ReadOnly _minimumLength As Integer
    Private ReadOnly _maximumLength As Integer
    Private ReadOnly _defaultvalue As Integer




    Protected Sub New()
        MyBase.New()

        InitializeComponent()
    End Sub

    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal parameters As CollectionParameter, ByVal itemResource As ResourceEntity, ByVal resourceManager As ResourceManagerBase, ByVal hasLoadedOldItemLayoutTemplate As Boolean, contextIdentifier As Nullable(Of Integer), Optional additionalParameters As IEnumerable(Of ParameterBase) = Nothing)
        MyBase.New(parent, parameters, itemResource)

        InitializeComponent()

        ParamCountTableLayoutPanel.SuspendLayout()
        MainLayoutPanel.SuspendLayout()
        Me.SuspendLayout()

        Dim sortKeyCollPrm = GetSortKeyFromParameter(parameters)
        Dim paramSet As ParameterCollection = ParameterCollection.DeepClone(collectionParameter.BluePrint)

        Me.AddAttributeReferenceDrivenChangeHandlers(parameters, parent.ParameterSets)

        Try
            Dim minimumLengthSettingValue As String = parameters.DesignerSettings.GetSettingValueByKey("minimumLength")
            If Not String.IsNullOrEmpty(minimumLengthSettingValue) Then
                _minimumLength = Integer.Parse(minimumLengthSettingValue)
            Else
                _minimumLength = If(TypeOf parameters Is ScoringParameter, 1, 2)
            End If

            Dim maximumLengthSettingValue As String = parameters.DesignerSettings.GetSettingValueByKey("maximumLength")
            If Not String.IsNullOrEmpty(maximumLengthSettingValue) Then _maximumLength = Integer.Parse(maximumLengthSettingValue) Else _maximumLength = 8

            Dim defaultValueSettingValue As String = parameters.DesignerSettings.GetSettingValueByKey("defaultvalue")
            If Not String.IsNullOrEmpty(defaultValueSettingValue) Then _defaultvalue = Integer.Parse(defaultValueSettingValue) Else _defaultvalue = _minimumLength

            Dim subsetIds As String = parameters.DesignerSettings.GetSettingValueByKey("subsetidentifiers")
            If Not String.IsNullOrEmpty(subsetIds) Then
                subsetIdentifiers = DirectCast([Enum].Parse(GetType(SubSetIdentifierGeneration), subsetIds), SubSetIdentifierGeneration)
            Else
                subsetIdentifiers = SubSetIdentifierGeneration.Numeric
            End If

            Dim subSetIdentifierListValues = parameters.DesignerSettings.GetSettingValueByKey("subsetidentifiersList")
            PopulateSubSetIdentifierList(subSetIdentifierListValues)

            Dim hideItemCountFieldStringValue As String = parameters.DesignerSettings.GetSettingValueByKey("hideitemcountfield")
            Dim hideItemCountFieldBooleanValue As Boolean
            Dim nrOfitemsControlsVisible As Boolean
            If Boolean.TryParse(hideItemCountFieldStringValue, hideItemCountFieldBooleanValue) Then
                nrOfitemsControlsVisible = Not hideItemCountFieldBooleanValue
            Else
                nrOfitemsControlsVisible = True
            End If

            ItemCountLabel.Text = parameters.DesignerSettings.GetSettingValueByKey("itemcountlabel")
            Dim itemcountDescription As String = collectionParameter.DesignerSettings.GetSettingValueByKey("itemcountdescription")

            If Not String.IsNullOrEmpty(ItemCountLabel.Text) Then ParameterEditorHelper.AddToolTip(ItemCountLabel.Text, itemcountDescription, ItemCountLabel)

            ItemCountLabel.Visible = nrOfitemsControlsVisible
            nrOfItemsComboBox.Visible = nrOfitemsControlsVisible

            Me.Visible = Not (Me.GetAllControlsOfType(Of ParameterEditorControlBase)() IsNot Nothing AndAlso Me.GetAllControlsOfType(Of ParameterEditorControlBase)().Where(Function(pe) pe.Visible = True).Count = 0 AndAlso _maximumLength = _minimumLength)

        Catch ex As Exception
            Throw New AppLogicException(String.Format(My.Resources.ErrorParsingDesignerSettingsForParameter, parameters.Name), ex)
        End Try

        For altNr As Integer = _minimumLength To _maximumLength
            nrOfItemsComboBox.Items.Add(altNr)
        Next

        MyBase.ResourceManager = resourceManager
        MyBase.HasLoadedOldItemLayoutTemplate = hasLoadedOldItemLayoutTemplate

        nrOfItemsComboBox.DisableScrollWheel()

        InitParameters(parameterLayoutPanel, True)
        SetTabIndexForControlsInLayoutPanel(parameterLayoutPanel, parameterLayoutPanel.TabIndex + 1)

        If collectionParameter.Value.Count = 0 AndAlso ((itemResource IsNot Nothing AndAlso itemResource.IsNew) OrElse Not Me.Visible) Then
            nrOfItemsComboBox.SelectedItem = Math.Max(collectionParameter.Value.Count, Math.Max(_minimumLength, _defaultvalue))
        Else
            nrOfItemsComboBox.SelectedItem = collectionParameter.Value.Count
        End If

        If additionalParameters IsNot Nothing AndAlso sortKeyCollPrm > 0 Then
            For Each param In additionalParameters
                Dim sortKeyParam = GetSortKeyFromParameter(param)
                If sortKeyParam > 0 AndAlso sortKeyParam <= sortKeyCollPrm Then
                    CreateParameterRow(MainLayoutPanel, param, paramSet.Id, False, True)
                ElseIf sortKeyParam = 0 OrElse sortKeyCollPrm = 0 OrElse sortKeyParam > sortKeyCollPrm Then
                    CreateParameterRow(MainLayoutPanel, param, paramSet.Id, False)
                End If
            Next
            SetTabIndexForControlsInLayoutPanel(MainLayoutPanel, 0)
        End If

        ParamCountTableLayoutPanel.ResumeLayout()
        MainLayoutPanel.ResumeLayout()
        Me.ResumeLayout()
    End Sub

    Public Sub SetFocusToNextParameterInEditor()
        Me.ParentForm.SelectNextControl(Me.ActiveControl, True, False, True, True)
    End Sub

    Public Sub SetFocusToPreviousParameterInEditor()
        Me.ParentForm.SelectNextControl(Me.ActiveControl, False, False, True, True)
    End Sub



    Private Sub OnResourceNeeded(ByVal sender As Object, ByVal e As Cito.Tester.ContentModel.ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub

    Private Function GetSortKeyFromParameter(prm As ParameterBase) As Integer
        If prm.DesignerSettings IsNot Nothing Then
            Dim sortKey As String = prm.DesignerSettings.GetSettingValueByKey("sortkey")
            Dim sortKeyValue As Integer
            If Not String.IsNullOrEmpty(sortKey) AndAlso Integer.TryParse(sortKey, sortKeyValue) Then
                Return sortKeyValue
            End If
        End If
        Return 0
    End Function

    Private Sub NrOfItemsComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nrOfItemsComboBox.SelectedIndexChanged
        Dim currentNrOfItems As Integer = collectionParameter.Value.Count
        Dim selectedValue As Integer = DirectCast(nrOfItemsComboBox.SelectedItem, Integer)

        Me.SuspendLayout()

        If currentNrOfItems < selectedValue Then
            Dim pcol As New ParameterSetCollection

            For i As Integer = currentNrOfItems + 1 To selectedValue
                Dim paramSet As ParameterCollection = ParameterCollection.DeepClone(collectionParameter.BluePrint)
                paramSet.Id = GetSubSetIdentifier(i)
                For Each param As ParameterBase In paramSet.InnerParameters
                    Dim value As String = param.DesignerSettings.GetSettingValueByKey("defaultvalue")
                    If Not String.IsNullOrEmpty(value) AndAlso Not param.SetValue(value) Then
                        Throw New Cito.Tester.Common.ItemTemplateException(String.Format("Parameter '{0}' tries to set a default value which was not possible.", param.Name))
                    End If

                    Dim visible As String = param.DesignerSettings.GetSettingValueByKey("visible")
                    If String.IsNullOrEmpty(visible) OrElse Not String.Equals(visible, Boolean.FalseString, StringComparison.OrdinalIgnoreCase) Then
                        CreateParameterRow(parameterLayoutPanel, param, paramSet.Id, True)
                    End If
                Next

                pcol.Add(paramSet)
            Next

            SetTabIndexForControlsInLayoutPanel(parameterLayoutPanel, parameterLayoutPanel.TabIndex + 1)
            collectionParameter.Value.AddRange(pcol)

        ElseIf currentNrOfItems > selectedValue Then
            Dim result As DialogResult = MessageBox.Show(My.Resources.ChoicesParameterEditorControl_NrOfAlternativesComboBox_SelectedIndexChanged_ValuesLostWarning, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then

                For index As Integer = currentNrOfItems - 1 To selectedValue Step -1
                    RemoveParameterRow(index)
                Next

                For index As Integer = currentNrOfItems - 1 To selectedValue Step -1
                    collectionParameter.Value.RemoveAt(index)
                Next
            Else
                nrOfItemsComboBox.SelectedItem = currentNrOfItems
            End If
        End If

        Me.ResumeLayout()

        Me.EditorParent.ValidateThisEditor(Me)

        If ParameterEditors.Count > 0 Then
            ParameterEditors.Keys.FirstOrDefault().Focus()
        End If
    End Sub

    Private Sub SetTabIndexForControlsInLayoutPanel(panel As TableLayoutPanel, ByRef tabIndex As Integer)
        For x = 0 To panel.RowCount - 1
            For Each editorControl In panel.Controls
                If panel.GetRow(editorControl) = x Then
                    If TypeOf editorControl Is ParameterEditorControlBase OrElse TypeOf editorControl Is CollectionEditorControlBase Then
                        editorControl.TabIndex = tabIndex
                        tabIndex += 1
                    ElseIf TypeOf editorControl Is TableLayoutPanel Then
                        editorControl.TabIndex = tabIndex
                        tabIndex += 1
                        SetTabIndexForControlsInLayoutPanel(editorControl, tabIndex)
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub RemoveParameterRow(ByVal paramIndex As Integer)
        Dim SetNr As String = GetSubSetIdentifier(paramIndex + 1)
        Dim FirstRowIndex As Integer = -1
        Dim LastRowIndex As Integer = -1

        For r As Integer = parameterLayoutPanel.RowCount - 1 To 0 Step -1
            For c As Integer = 0 To parameterLayoutPanel.ColumnCount - 1
                Dim ControlAtPos As Control = parameterLayoutPanel.GetControlFromPosition(c, r)
                If ControlAtPos IsNot Nothing Then
                    Dim ControlTag As String = ControlAtPos.Tag.ToString()
                    If ControlTag = SetNr Then
                        If LastRowIndex = -1 Then
                            LastRowIndex = r
                        End If
                        FirstRowIndex = r
                    End If

                    Exit For
                End If
            Next
        Next

        If LastRowIndex > -1 Then
            For index As Integer = LastRowIndex To FirstRowIndex Step -1
                For c As Integer = parameterLayoutPanel.ColumnCount - 1 To 0 Step -1
                    Dim ControlAtPos As Control = parameterLayoutPanel.GetControlFromPosition(c, index)

                    If TypeOf ControlAtPos Is ParameterEditorControlBase Then
                        Dim editor As ParameterEditorControlBase = DirectCast(ControlAtPos, ParameterEditorControlBase)
                        editor.RemoveAllResources()
                        ParameterEditors.Remove(editor)
                        editor = Nothing
                    End If

                    Application.DoEvents()

                    parameterLayoutPanel.Controls.Remove(ControlAtPos)
                Next

                parameterLayoutPanel.RowCount -= 1
                parameterLayoutPanel.RowStyles.RemoveAt(index)
            Next
        End If
    End Sub

    Protected Overridable Sub OnEditResource(ByVal sender As Object, ByVal e As ResourceNameEventArgs)
        RaiseEvent EditResource(Me, e)
    End Sub


End Class
