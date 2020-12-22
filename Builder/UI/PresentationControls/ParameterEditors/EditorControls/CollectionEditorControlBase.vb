Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Model
Imports Questify.Builder.Model.ContentModel.EntityClasses

#If DEBUG Then
Public Class CollectionEditorControlBase
#Else
Public Mustinherit Class CollectionEditorControlBase
#End If


    Protected Enum SubSetIdentifierGeneration As Integer
        Alphabetic
        Numeric
        List
    End Enum



    Private ReadOnly _parameterEditors As New Dictionary(Of ParameterEditorControlBase, ParameterBase)

    Protected subsetIdentifiers As SubSetIdentifierGeneration
    Protected resourceEntity As ResourceEntity
    Protected collectionParameter As CollectionParameter
    Protected disabledParameters As New List(Of String)
    Protected factory As New ParameterEditorFactory
    Protected SubSetIdentifierList As List(Of String)



    Protected Sub New()
        MyBase.New()

        SubSetIdentifierList = New List(Of String)()

        InitializeComponent()
    End Sub

    Public Sub New(parent As ParameterSetsEditor, parameters As CollectionParameter, ByVal itemResource As ContentModel.EntityClasses.ResourceEntity)
        MyBase.New(parent)

        SubSetIdentifierList = New List(Of String)()

        InitializeComponent()

        If parameters Is Nothing Then
            Throw New ArgumentNullException("parameters")
        End If

        If parent Is Nothing Then
            Throw New ArgumentNullException("parent")
        End If

        collectionParameter = parameters
        resourceEntity = itemResource
    End Sub


    Protected Overridable Sub ParameterEditorAddingResource(ByVal sender As Object, ByVal e As ResourceNameEventArgs)
        OnAddingResource(e)
    End Sub

    Protected Overridable Sub ParameterEditorRemovingResource(ByVal sender As Object, ByVal e As ResourceNameEventArgs)
        If Not String.IsNullOrEmpty(e.ResourceName) Then
            Dim dependentResource As DependentResourceEntity = resourceEntity.GetDependentResourceByName(e.ResourceName)
            If dependentResource IsNot Nothing Then
                If Not IsResourceUsedInThisParameter(dependentResource.DependentResource, DirectCast(sender, ParameterEditorControlBase)) Then
                    OnRemovingResource(e)
                End If
            End If
        End If
    End Sub

    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        If resource Is Nothing Then
            Throw New ArgumentNullException("resource")
        End If

        Return IsResourceUsedInThisParameter(resource, Nothing)
    End Function

    Public Overrides Sub RemoveAllResources()
        For Each editor As ParameterEditorControlBase In ParameterEditors.Keys
            editor.RemoveAllResources()
        Next
    End Sub

    Public Overrides Sub PreItemSave(ByVal hasLoadedOldItemLayoutTemplate As Boolean)
        For Each editor As ParameterEditorControlBase In ParameterEditors.Keys
            editor.PreItemSave(hasLoadedOldItemLayoutTemplate)
        Next
    End Sub

    Public Overrides Sub PostItemSave()
        For Each editor As ParameterEditorControlBase In ParameterEditors.Keys
            editor.PostItemSave()
        Next
    End Sub

    Public Overrides Function ValidateParameter() As String
        Dim hasErrors As Boolean
        Dim errors As String = String.Empty
        For Each editor As ParameterEditorControlBase In ParameterEditors.Keys
            If Not (Me.EditorParent Is Nothing) Then
                Dim singleParemeterError As String = Me.EditorParent.ValidateThisEditor(editor, False)
                If Not String.IsNullOrEmpty(singleParemeterError) Then
                    If Not String.IsNullOrEmpty(errors) Then errors += vbCrLf
                    errors += singleParemeterError
                    hasErrors = True
                End If
            End If
        Next

        If hasErrors Then
            If Not errors.StartsWith(My.Resources.ThisParameterContainsSomeInputErrors) Then errors = My.Resources.ThisParameterContainsSomeInputErrors + vbCrLf + errors
            Return errors
        Else
            Return String.Empty
        End If
    End Function

    Protected Overridable Sub CreateParameterRow(ByVal parameterLayoutPanel As TableLayoutPanel, ByVal param As ParameterBase, ByVal paramSetId As String, ByVal enableLabelNumbering As Boolean,
                                             Optional insertAbove As Boolean = False)
        Dim paramUIControl As ParameterEditorControlBase = Nothing
        parameterLayoutPanel.RowCount += 1
        Dim rowIndex As Integer = CType(IIf(insertAbove, 0, parameterLayoutPanel.RowCount - 1), Integer)
        parameterLayoutPanel.RowStyles.Insert(rowIndex, New RowStyle(SizeType.AutoSize))
        If insertAbove Then ReorderControls(parameterLayoutPanel, rowIndex)

        Dim paramLabel As New Label() With {.Margin = New Padding(0, 3, 3, 3)}

        Dim description As String = param.DesignerSettings.GetSettingValueByKey("description")
        Dim label As String = param.DesignerSettings.GetSettingValueByKey("label")
        If Not String.IsNullOrEmpty(label) Then ParameterEditorHelper.AddToolTip(label, description, paramLabel)

        Dim userFriendlyParamSetId As String = paramSetId
        If Not String.IsNullOrEmpty(paramSetId) AndAlso Not IsNumeric(paramSetId) Then userFriendlyParamSetId = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(paramSetId)
        If enableLabelNumbering Then paramLabel.Text = String.Format("{0} ({1})", label, userFriendlyParamSetId) Else paramLabel.Text = String.Format("{0}", label)
        paramLabel.Tag = paramSetId
        paramLabel.AutoSize = True
        paramLabel.TextAlign = ContentAlignment.MiddleLeft
        paramLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right

        paramUIControl = factory.CreateControl(param, EditorParent)
        paramUIControl.Name = param.Name
        paramUIControl.Margin = New Padding(3, 3, 15, 3)
        paramUIControl.Dock = DockStyle.Fill
        paramUIControl.Tag = paramSetId
        paramUIControl.ParentTabEnabledContainerControl = Me

        AddHandler paramUIControl.AddingResource, AddressOf ParameterEditorAddingResource
        AddHandler paramUIControl.RemovingResource, AddressOf ParameterEditorRemovingResource

        Dim thisParamUIControlRequiresTwoRows As Boolean = (TypeOf param Is CollectionParameter OrElse TypeOf param Is XHtmlParameter OrElse parameterLayoutPanel.ColumnCount = 1)

        If thisParamUIControlRequiresTwoRows Then
            If Not String.IsNullOrEmpty(paramLabel.Text) Then
                parameterLayoutPanel.Controls.Add(paramLabel, 0, rowIndex)
                parameterLayoutPanel.RowCount += 1
                rowIndex += 1
                parameterLayoutPanel.RowStyles.Insert(rowIndex, New RowStyle(SizeType.AutoSize))
            End If
            If TypeOf param Is CollectionParameter AndAlso (param.GetType() IsNot GetType(AreaParameter) AndAlso param.GetType() IsNot GetType(MultiChoiceScoringParameter)) Then
                paramUIControl.Margin = New Padding(20, 3, 15, 3)
                paramLabel.Margin = New Padding(20, 3, 15, 3)
            End If
            parameterLayoutPanel.Controls.Add(paramUIControl, 0, CType(IIf(insertAbove, 0, rowIndex), Integer))
            ParameterEditors.Add(paramUIControl, param)
            parameterLayoutPanel.SetColumnSpan(paramUIControl, 2)
        Else
            parameterLayoutPanel.Controls.Add(paramLabel, 0, CType(IIf(insertAbove, 0, rowIndex), Integer))
            parameterLayoutPanel.Controls.Add(paramUIControl, 1, CType(IIf(insertAbove, 0, rowIndex), Integer))
            ParameterEditors.Add(paramUIControl, param)
        End If

        If disabledParameters.Contains(param.Name) Then
            paramUIControl.SetConditionalEnabled(False)
        End If
    End Sub

    Private Sub ReorderControls(parameterLayoutPanel As TableLayoutPanel, rowIndex As Integer)
        For Each control In parameterLayoutPanel.Controls
            If parameterLayoutPanel.GetRow(control) >= rowIndex Then
                parameterLayoutPanel.SetRow(control, parameterLayoutPanel.GetRow(control) + 1)
            End If
        Next
    End Sub

    Protected Overridable Sub InitParameters(ByVal parameterLayoutPanel As TableLayoutPanel, ByVal enableLabelNumbering As Boolean)
        Me.SuspendLayout()

        For Each paramSet As ParameterCollection In collectionParameter.Value
            For Each param As ParameterBase In paramSet.InnerParameters
                Dim visible As String = param.DesignerSettings.GetSettingValueByKey("visible")
                If String.IsNullOrEmpty(visible) OrElse Not String.Equals(visible, Boolean.FalseString, StringComparison.OrdinalIgnoreCase) Then
                    CreateParameterRow(parameterLayoutPanel, param, paramSet.Id, enableLabelNumbering)
                Else
                    Dim value As String = param.DesignerSettings.GetSettingValueByKey("defaultvalue")
                    If Not String.IsNullOrEmpty(value) AndAlso Not param.SetValue(value) Then
                        Throw New Cito.Tester.Common.ItemTemplateException(String.Format("Parameter '{0}' tries to set a default value which was not possible.", param.Name))
                    End If
                End If
            Next
        Next

        Me.ResumeLayout()
    End Sub

    Protected Function IsResourceUsedInThisParameter(ByVal resource As ContentModel.EntityClasses.ResourceEntity, ByVal removedFrom As ParameterEditorControlBase) As Boolean
        For Each editor As ParameterEditorControlBase In ParameterEditors.Keys
            If Not editor.Equals(removedFrom) AndAlso editor.ResourceUsedInThisParameter(resource) Then
                Return True
            End If
        Next

        Return False
    End Function

    Protected Function GetSubSetIdentifier(ByVal id As Integer) As String

        Select Case subsetIdentifiers
            Case SubSetIdentifierGeneration.Alphabetic
                Return Convert.ToChar(id + 64).ToString()
            Case SubSetIdentifierGeneration.Numeric
                Return id.ToString()
            Case SubSetIdentifierGeneration.List
                Return GetSubSetIdentifierFromList(id)
            Case Else
                Throw New AppLogicException(String.Format(My.Resources.ErrorGeneratingIdentifierForItemInCollectionparameterGeneratingType, subsetIdentifiers.ToString()))
        End Select

    End Function

    Private Function GetSubSetIdentifierFromList(id As Integer) As String

        Dim index As Integer = id - 1
        Dim subSetId As String = String.Empty

        If index >= 0 AndAlso index < SubSetIdentifierList.Count Then
            subSetId = SubSetIdentifierList(index)
        End If

        If Not String.IsNullOrEmpty(subSetId) Then
            Return subSetId
        End If

        Return id.ToString()
    End Function

    Protected Sub PopulateSubSetIdentifierList(listString As String)

        Dim listParts As String() = listString.Split(New Char() {";"c})

        For Each part As String In listParts
            SubSetIdentifierList.Add(part.Trim())
        Next

    End Sub

    Public Overloads Sub SetConditionalEnabled(parameterName As String, value As Boolean)
        Dim collectionParameterConditionalEnabled As Boolean = False
        If collectionParameter.DesignerSettings IsNot Nothing _
         AndAlso Boolean.TryParse(collectionParameter.DesignerSettings.GetSettingValueByKey("conditionalEnabled"), collectionParameterConditionalEnabled) _
         AndAlso collectionParameterConditionalEnabled AndAlso parameterName = collectionParameter.DesignerSettings.GetDesignerSettingByKey("conditionalEnabledSwitchParameter").Value Then
            Me.SetConditionalEnabled(value)
        Else
            For Each parameterEditor As ParameterEditorControlBase In ParameterEditors.Keys
                Dim innerParameter As ParameterBase = ParameterEditors.Item(parameterEditor)
                Dim conditionalEnabled As String = innerParameter.DesignerSettings.GetSettingValueByKey("conditionalEnabled")
                If (Not String.IsNullOrEmpty(conditionalEnabled) AndAlso String.Equals(conditionalEnabled, Boolean.TrueString, StringComparison.OrdinalIgnoreCase)) Then
                    Dim conditionalEnabledSwitchParameter As String = innerParameter.DesignerSettings.GetDesignerSettingByKey("conditionalEnabledSwitchParameter").Value
                    If parameterName = conditionalEnabledSwitchParameter Then
                        parameterEditor.SetConditionalEnabled(value)
                        If value AndAlso disabledParameters.Contains(innerParameter.Name) Then
                            disabledParameters.Remove(innerParameter.Name)
                        ElseIf Not value AndAlso Not disabledParameters.Contains(innerParameter.Name) Then
                            disabledParameters.Add(innerParameter.Name)
                        End If
                    End If
                End If
            Next
        End If

    End Sub

    Public ReadOnly Property CollectionParameterName As String
        Get
            Return collectionParameter.Name
        End Get
    End Property

    Public ReadOnly Property ParameterEditors As Dictionary(Of ParameterEditorControlBase, ParameterBase)
        Get
            Return _parameterEditors
        End Get
    End Property


End Class
