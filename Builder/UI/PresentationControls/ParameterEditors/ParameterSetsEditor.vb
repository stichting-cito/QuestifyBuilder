

Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Text
Imports System.Linq
Imports Questify.Builder.Logic
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ParameterSetsEditor

#Region " Private declarations "

    Private _itemSaving As Boolean = False
    Private _formClosing As Boolean = False
    Private _cleaningControls As Boolean = False
    Private _parameterSets As New ParameterSetCollection
    Private _parameterEditors As New Dictionary(Of ParameterEditorControlBase, ParameterBase)
    Private _groups As New Dictionary(Of String, ParameterGroupBox)
    Private _referencedStylesheetsFromItemLayoutTemplate As Dictionary(Of String, String) = Nothing
    Private _referencedStylesheetsHeaderStyleElementContent As String = String.Empty
    Private _itemLayoutAdapterForItem As ItemLayoutAdapter = Nothing

    'parameter that is referenced --> list of parameters that refer to the parameter
    Private ReadOnly _redirectedParameterDictionary As New Dictionary(Of ParameterBase, List(Of ParameterBase))
    'parameter for which a condition is defined --> dictionary of parameters that are conditionally visible with conditional value
    Private ReadOnly _conditionalParameterDictionary As New Dictionary(Of ParameterBase, Dictionary(Of ParameterEditorControlBase, String))
    'parameter for which a condition is defined --> parameter on which the visibility of the key parameter is conditionally based
    Private ReadOnly _conditionalParameterDependenciesDictionary As New Dictionary(Of ParameterBase, ParameterBase)
    Private ReadOnly _groupConditionalParameterList As New List(Of ParameterBase)

    Private ReadOnly _shouldRender As Boolean = True

    Private Const CONDITIONAL_OR As Char = "|"c
    Private Const CONDITIONAL_NOT As Char = "!"c
    Private Const CONDITIONAL_EMPTY As String = "(EMPTY)"

#End Region

#Region " Public events "

    ''' <summary>
    ''' Occurs when a resource must be checked whether it is used in an item.
    ''' </summary>
    <Description("This event will be raised when a resource must be checked whether it is used in an item"), Category("ParameterSetsEditor events")>
    Public Event CheckResourceUsed As EventHandler(Of ResourceUsedCheckEventArgs)
    'ToDo [remcor 21-04-2012]: Refactor. This event is no longer needed because the ResourceManager is available as a property

    ''' <summary>
    ''' Occurs when the resource is edited.
    ''' </summary>
    <Description("This event will be raised when a parameter requests the generic resource editor."), Category("ParameterSetsEditor events")>
    Public Event EditResource As EventHandler(Of ResourceNameEventArgs)
    'ToDo [remcor 21-04-2012]: Refactor. This event is no longer needed because the ResourceManager is available as a property

#End Region

#Region " Public properties "

    Public Property Solution As Solution

    Public Property ItemSaving As Boolean
        Get
            Return _itemSaving
        End Get
        Set
            _itemSaving = Value
        End Set
    End Property

    Public Property CleaningControls As Boolean
        Get
            Return _cleaningControls
        End Get
        Set
            _cleaningControls = Value
        End Set
    End Property

    Public Property FormClosing As Boolean
        Get
            Return _formClosing
        End Get
        Set
            _formClosing = Value
            For Each editor As ParameterEditorControlBase In _parameterEditors.Keys
                editor.FormClosing = Value
            Next
        End Set
    End Property

    Public Property HasLoadedOldItemLayoutTemplate As Boolean = False

    Public Property ParentIsInlineElement As Boolean = False

    Public Property ContextIdentifierForEditors As Integer?

    ''' <summary>
    ''' Gets or sets the item resource, which contains dependent resources that could be used by parameter editors to
    ''' control dependent resources.
    ''' </summary>
    Public Property ResourceEntity As ResourceEntity

    Public Property FilterParameter As ParameterBase

    Public Property ShouldSort As Boolean


    ' Suppress CA2227: The collection will be build from outside this control and then passed.
    <SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")>
    Public Property ParameterSets As ParameterSetCollection
        Get
            Return _parameterSets
        End Get
        Set
            _parameterSets = Value
            ' Render editors and bind data
            If _shouldRender Then
                If Value IsNot Nothing Then
                    _referencedStylesheetsFromItemLayoutTemplate = Nothing
                    _referencedStylesheetsHeaderStyleElementContent = String.Empty
                End If
                Me.RenderEditors()
            End If

        End Set
    End Property

    Public Property ResourceManager() As ResourceManagerBase

    Public Property ReferencedStylesheetsFromItemLayoutTemplate As Dictionary(Of String, String)
        Get
            If _referencedStylesheetsFromItemLayoutTemplate Is Nothing AndAlso ResourceEntity IsNot Nothing AndAlso TypeOf ResourceEntity Is ItemResourceEntity Then
                _referencedStylesheetsFromItemLayoutTemplate = DirectCast(ResourceEntity, ItemResourceEntity).GetStylesFromDependentItemLayoutTemplate(_referencedStylesheetsHeaderStyleElementContent, ContextIdentifierForEditors)
            End If
            Return _referencedStylesheetsFromItemLayoutTemplate
        End Get
        Set
            _referencedStylesheetsFromItemLayoutTemplate = Value
        End Set
    End Property

    Public Property ReferencedStylesheetsHeaderStyleElementContent As String
        Get
            If String.IsNullOrEmpty(_referencedStylesheetsHeaderStyleElementContent) AndAlso _referencedStylesheetsFromItemLayoutTemplate Is Nothing AndAlso ResourceEntity IsNot Nothing AndAlso TypeOf ResourceEntity Is ItemResourceEntity Then
                _referencedStylesheetsFromItemLayoutTemplate = DirectCast(ResourceEntity, ItemResourceEntity).GetStylesFromDependentItemLayoutTemplate(_referencedStylesheetsHeaderStyleElementContent, ContextIdentifierForEditors)
            End If
            Return _referencedStylesheetsHeaderStyleElementContent
        End Get
        Set
            _referencedStylesheetsHeaderStyleElementContent = Value
        End Set
    End Property

    Public Property ItemLayoutAdapterForItem As ItemLayoutAdapter
        Get
            If _itemLayoutAdapterForItem Is Nothing AndAlso ResourceEntity IsNot Nothing AndAlso TypeOf ResourceEntity Is ItemResourceEntity Then
                CreateAdapterIfNeeded(_itemLayoutAdapterForItem, DirectCast(ResourceEntity, ItemResourceEntity).ItemLayoutTemplateUsedName)
            End If
            Return _itemLayoutAdapterForItem
        End Get
        Set
            _itemLayoutAdapterForItem = Value
        End Set
    End Property

#End Region

#Region " Methods "

#Region " Friend "

    Friend Function ValidateThisEditor(editor As ParameterEditorControlBase, Optional andSiblings As Boolean = True) As String

        Dim result As String = editor.ValidateParameter()
        ParameterErrorProvider.SetError(editor, result)

        If Not andSiblings Then
            Return result
        End If

        Dim hasError As Boolean = Not String.IsNullOrEmpty(result)

        Dim totalResult As New StringBuilder()
        If hasError Then
            PostItemSave()

            If Not result.StartsWith(My.Resources.ThisParameterContainsSomeInputErrors) Then
                totalResult.Append(My.Resources.ThisParameterContainsSomeInputErrors + vbCrLf)
            End If

            totalResult.Append(result)
        End If

        If editor.ParentTabEnabledContainerControl IsNot Nothing AndAlso
            GetType(ParameterCollectionEditorControl).IsAssignableFrom(editor.ParentTabEnabledContainerControl.GetType()) Then
            Dim siblingsAndSelf As ParameterCollectionEditorControl = DirectCast(editor.ParentTabEnabledContainerControl, ParameterCollectionEditorControl)

            Dim sResult As String
            For Each parameterEditorControlBase As ParameterEditorControlBase In siblingsAndSelf.ParameterEditors.Keys
                If parameterEditorControlBase Is editor Then
                    Continue For
                End If
                sResult = parameterEditorControlBase.ValidateParameter()
                If Not String.IsNullOrEmpty(sResult) Then
                    totalResult.Append(vbCrLf + sResult)
                    hasError = True
                End If
            Next

            If hasError Then
                ParameterErrorProvider.SetError(editor.ParentTabEnabledContainerControl, totalResult.ToString())
            Else
                ParameterErrorProvider.SetError(editor.ParentTabEnabledContainerControl, String.Empty)
            End If
        End If
        Return result
    End Function

#End Region

#Region " Public "

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ReInitializeConditionalObjects()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(itemResourceEntity As ItemResourceEntity, parameterSets As ParameterSetCollection)
        Me.New(itemResourceEntity, parameterSets, True)
    End Sub

    Public Sub New(itemResourceEntity As ItemResourceEntity, parameterSets As ParameterSetCollection, shouldRender As Boolean)
        Me.New()
        _shouldRender = shouldRender
        ResourceEntity = itemResourceEntity
        Me.ParameterSets = parameterSets
    End Sub

    ''' <summary>
    ''' Validates all the parameter editors in this placeholder.
    ''' </summary>
    ''' <returns>A string with the error messages if validation fails.</returns>
    Public Function ValidateParameterEditors() As String
        Dim errorStringBuilder As New StringBuilder()

        For Each editor As ParameterEditorControlBase In _parameterEditors.Keys
            Dim result As String = ValidateThisEditor(editor)
            If Not String.IsNullOrEmpty(result) Then
                errorStringBuilder.Append($" - {result}{Environment.NewLine}")
            End If
        Next

        Return errorStringBuilder.ToString()
    End Function

    Public Sub PreItemSave()
        _itemSaving = True
        For Each editor As ParameterEditorControlBase In _parameterEditors.Keys
            editor.PreItemSave(HasLoadedOldItemLayoutTemplate)
        Next
    End Sub

    Public Sub PostItemSave()
        _itemSaving = False
        For Each editor As ParameterEditorControlBase In _parameterEditors.Keys
            editor.PostItemSave()
        Next
    End Sub

    Public Function GetParameterEditorByParameter(param As ParameterBase) As ParameterEditorControlBase
        Dim foundEditor As ParameterEditorControlBase = Nothing
        If param IsNot Nothing Then
            For Each c As Control In Me.Controls
                If GetParameterEditorByParameterRecursive(param, c) Then
                    foundEditor = TryCast(c, ParameterEditorControlBase)
                    Exit For
                End If
            Next
        End If
        Return foundEditor
    End Function

    Public Function GetParameterEditorByParameterRecursive(param As ParameterBase, ByRef c As Control) As Boolean
        Dim editor = TryCast(c, ParameterEditorControlBase)
        If editor IsNot Nothing AndAlso editor.ParameterBindingSource IsNot Nothing AndAlso editor.ParameterBindingSource.DataSource IsNot Nothing AndAlso editor.ParameterBindingSource.DataSource.Equals(param) Then
            Return True
        End If
        For Each childControl As Control In c.Controls
            c = childControl
            If GetParameterEditorByParameterRecursive(param, c) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub CleanUpEditors()
        _cleaningControls = True

        ' remove all event handlers from parameter editors
        ReInitializeConditionalObjects()

        'Add to List,.. then clear then dispose
        Dim toRemove = Controls.Cast(Of Control)().ToList()
        Controls.Clear()

        Me.Visible = False 'old trick.
        Me.SuspendLayout()
        If _parameterEditors IsNot Nothing Then
            For Each editorControl As ParameterEditorControlBase In _parameterEditors.Keys
                ' type specific additional handlers
                If TypeOf editorControl Is ResourceParameterEditorControl Then
                    RemoveHandler DirectCast(editorControl, ResourceParameterEditorControl).ResourceNeeded, AddressOf ParameterEditorControl_ResourceNeeded
                    RemoveHandler DirectCast(editorControl, ResourceParameterEditorControl).EditResource, AddressOf ParameterEditorControl_EditResource
                End If

                If TypeOf editorControl Is ParameterCollectionEditorControl Then
                    RemoveHandler DirectCast(editorControl, ParameterCollectionEditorControl).ResourceNeeded, AddressOf ParameterEditorControl_ResourceNeeded
                    RemoveHandler DirectCast(editorControl, ParameterCollectionEditorControl).EditResource, AddressOf ParameterEditorControl_EditResource
                End If

                ' explicitly dispose the editor
                editorControl.Dispose()
            Next

            ' clean collection to lose references
            _parameterEditors = New Dictionary(Of ParameterEditorControlBase, ParameterBase)
        End If

        toRemove.ForEach(Sub(c) If c IsNot Nothing Then c.Dispose())
        Try

            If _groups IsNot Nothing Then
                For Each keyValuePair As KeyValuePair(Of String, ParameterGroupBox) In _groups
                    keyValuePair.Value.Dispose()
                Next
                _groups.Clear()
            End If
        Catch e As Exception
        End Try

        Me.ResumeLayout()
        Me.Visible = True 'old trick.
        _cleaningControls = False
    End Sub

#End Region

#Region " Protected "

    Protected Overridable Sub OnCheckResourceUsed(e As ResourceUsedCheckEventArgs)
        RaiseEvent CheckResourceUsed(Me, e)
    End Sub

    Protected Overridable Sub OnEditResource(e As ResourceNameEventArgs)
        RaiseEvent EditResource(Me, e)
    End Sub


#End Region

#Region " Private "

    Private Sub RenderEditors()
        Me.Visible = False

        ' reset this control
        CleanUpEditors()

        If _parameterEditors Is Nothing Then
            _parameterEditors = New Dictionary(Of ParameterEditorControlBase, ParameterBase)
        End If

        If _groups Is Nothing Then
            _groups = New Dictionary(Of String, ParameterGroupBox)
        End If

        GC.Collect()

        Me.SuspendLayout()

        If _parameterSets IsNot Nothing Then

            Dim filterParamGroup As String = Nothing
            Dim filterParamLabel As String = Nothing

            If FilterParameter IsNot Nothing Then
                filterParamGroup = FilterParameter.DesignerSettings.GetSettingValueByKey("group")
                If filterParamGroup Is Nothing Then filterParamGroup = "-"

                filterParamLabel = FilterParameter.DesignerSettings.GetSettingValueByKey("label")
                If filterParamLabel Is Nothing Then filterParamLabel = FilterParameter.DesignerSettings.GetSettingValueByKey("itemcountlabel")
            End If

            Dim parameters As List(Of ParameterBase) = _parameterSets.GetParameters(FromNonDynamicCollections)
            Using parFactory As New ParameterEditorFactory()

                ' only render controls for parametercollections which have parameters assigned.
                For Each param As ParameterBase In parameters
                    Dim visibleSetting As String = param.DesignerSettings.GetSettingValueByKey("visible")
                    Dim redirected As String = param.DesignerSettings.GetSettingValueByKey("redirectEnabled")
                    OptionalRegisterGroupCondition(param)

                    If (Not String.IsNullOrEmpty(redirected) AndAlso String.Equals(redirected, Boolean.TrueString, StringComparison.OrdinalIgnoreCase)) Then
                        AddToRedirectionDictionary(param)
                    ElseIf (Not String.IsNullOrEmpty(visibleSetting) AndAlso String.Equals(visibleSetting, Boolean.FalseString, StringComparison.OrdinalIgnoreCase)) Then
                        ' no editor is needed. This parameter has been given a default value (in the control of itemlayouttemplate) which can't be edited
                    Else
                        'Filter the rendering if a filter parameter is given
                        Dim cont As Boolean = True
                        If Not FilterParameter Is Nothing Then
                            Dim paramGroup As String = param.DesignerSettings.GetSettingValueByKey("group")
                            If paramGroup Is Nothing Then paramGroup = "-"

                            If paramGroup.Equals(filterParamGroup) Then
                                Dim paramLabel As String = param.DesignerSettings.GetSettingValueByKey("label")
                                If paramLabel Is Nothing Then paramLabel = param.DesignerSettings.GetSettingValueByKey("itemcountlabel")

                                If filterParamLabel Is Nothing OrElse paramLabel Is Nothing OrElse Not paramLabel.Equals(filterParamLabel) Then
                                    cont = False
                                End If
                            Else
                                cont = False
                            End If
                        End If
                        If cont Then
                            Dim paramUIControl As ParameterEditorControlBase = parFactory.CreateControl(param, Me)
                            ' Create and bind parameterUI controls
                            If paramUIControl IsNot Nothing Then
                                ' get groupname for this control
                                Dim group As String = param.DesignerSettings.GetSettingValueByKey("group")
                                If String.IsNullOrEmpty(group) Then
                                    group = My.Resources.GroupNameConstant_General
                                End If
                                ' check whether the group already exists, if not then create it (case-insensative)
                                Dim groupControl As ParameterGroupBox = Nothing
                                If Not _groups.TryGetValue(group.ToLower.ToLower, groupControl) Then
                                    ' create the group control
                                    groupControl = New ParameterGroupBox()
                                    groupControl.Dock = DockStyle.Top
                                    groupControl.GroupName = group
                                    groupControl.Margin = New Padding(0, 0, 0, 5)
                                    groupControl.TabStop = False
                                    groupControl.AccessibleName = "GROUP :: " + group
                                    groupControl.SetExpandedState()
                                    _groups.Add(group.ToLower, groupControl)
                                End If

                                'add editor to group
                                groupControl.AddParameterEditorToGroup(paramUIControl, param)

                                ' add editor to list of editors on this control
                                _parameterEditors.Add(paramUIControl, param)
                            End If
                        End If
                    End If
                Next

            End Using
            'if all parametersets are added to a groupbox, the parameters that are conditional enabled can be set.
            'Because its checked if the are any enabled controls in the groups it should be done after all parametersets are added to a groupbox.
            For Each paramUIControl As ParameterEditorControlBase In _parameterEditors.Keys
                Dim parameterCollectionSetId As String = _parameterSets.GetParameterSetCollectionNameByParameter(_parameterEditors.Item(paramUIControl))
                InitialiseConditionalEnabledParameter(_parameterEditors.Item(paramUIControl), paramUIControl, parameterCollectionSetId)
            Next
            For Each prm As ParameterBase In _conditionalParameterDictionary.Keys.Where(Function(k) If(_conditionalParameterDependenciesDictionary IsNot Nothing, Not _conditionalParameterDependenciesDictionary.ContainsKey(k), True))
                PerformEnablingDisablingConditionalParameters(prm, False)
            Next
        End If

        ' Next
        If _groups.Values.Count = 0 Then
            ' there are no parameters for this item. Create a label to represent this fact.
            Dim noParametersLabel As New Label()
            noParametersLabel.Text = My.Resources.ParameterSetsEditor_RenderEditors_NoEditors
            noParametersLabel.AutoSize = True
            Me.Controls.Add(noParametersLabel)
        Else
            ' add groupcontrols on this control
            Dim tabIndex As Integer = 0
            If ShouldSort Then
                Dim sortedGroups As New SortedDictionary(Of String, ParameterGroupBox)(_groups)
                For Each groupControl As ParameterGroupBox In sortedGroups.Values
                    AddGroupControl(groupControl, tabIndex)
                Next
            Else
                For Each groupControl As ParameterGroupBox In _groups.Values
                    AddGroupControl(groupControl, tabIndex)
                Next
            End If
        End If
        ' End If
        '
        InitialGroupVisiblity()

        Me.ResumeLayout()
        Me.Visible = True
    End Sub


    'Do not display parameterCollections form parameter set where is dynamic.
    'Scoring parameters for custom interactions should not be displayed.
    Private Function FromNonDynamicCollections() As Func(Of ParameterCollection, Boolean)
        Return Function(parameterCollection) Not parameterCollection.IsDynamicCollection
    End Function


    Private Sub ReorderControls()
        If ShouldSort AndAlso _groups IsNot Nothing Then
            SuspendLayout()
            Dim sortedGroups As New SortedDictionary(Of String, ParameterGroupBox)(_groups)
            For Each groupControl As ParameterGroupBox In sortedGroups.Values.OrderByDescending(Function(v) v.GroupName)
                Me.Controls.SetChildIndex(groupControl, Me.Controls.Count - 1)
            Next
            ResumeLayout(True)
        End If
    End Sub

    Private Function CheckEnableParameter(conditionalValue As String, parameterValue As String, checkIfMatch As Boolean) As Boolean
        Dim result As Boolean = False
        If checkIfMatch = True Then
            If conditionalValue.StartsWith(CONDITIONAL_NOT) AndAlso conditionalValue.Contains(CONDITIONAL_EMPTY) Then
                If Not (String.IsNullOrEmpty(parameterValue)) Then result = True
            ElseIf conditionalValue.StartsWith(CONDITIONAL_NOT) Then
                If Not (conditionalValue.Substring(1) = parameterValue) Then result = True
            ElseIf conditionalValue.Contains(CONDITIONAL_EMPTY) Then
                If String.IsNullOrEmpty(parameterValue) Then result = True
            Else
                If conditionalValue = parameterValue Then result = True
            End If
        Else
            If conditionalValue.StartsWith(CONDITIONAL_NOT) AndAlso conditionalValue.Contains(CONDITIONAL_EMPTY) Then
                If String.IsNullOrEmpty(parameterValue) Then result = True
            ElseIf conditionalValue.StartsWith(CONDITIONAL_NOT) Then
                If conditionalValue.Substring(1) = parameterValue Then result = True
            ElseIf conditionalValue.Contains(CONDITIONAL_EMPTY) Then
                If Not (String.IsNullOrEmpty(parameterValue)) Then result = True
            Else
                If Not (conditionalValue = parameterValue) Then result = True
            End If
        End If
        Return result
    End Function

    Private Sub InitialGroupVisiblity()
        For Each parameterBase In _groupConditionalParameterList
            SetGroupConditionalEnabled(parameterBase, True)
        Next
    End Sub

    Private Sub GroupConditionalParameter_ParameterChanged(sender As Object, e As PropertyChangedEventArgs)
        Dim parameter As ParameterBase = Nothing
        parameter = TryCast(sender, ParameterBase)
        SetGroupConditionalEnabled(parameter, True)
    End Sub

    Private Sub SetGroupConditionalEnabled(parameter As ParameterBase, onlyInvisible As Boolean)
        If parameter IsNot Nothing AndAlso _groupConditionalParameterList.Contains(parameter) Then
            If parameter.DesignerSettings.GetDesignerSettingByKey("groupConditionalEnabledSwitch") IsNot Nothing AndAlso parameter.DesignerSettings.GetDesignerSettingByKey("groupConditionalEnabledWhenValue") IsNot Nothing Then
                Dim groupName As String = parameter.DesignerSettings.GetDesignerSettingByKey("groupConditionalEnabledSwitch").Value
                Dim value As String = parameter.DesignerSettings.GetDesignerSettingByKey("groupConditionalEnabledWhenValue").Value
                If _groups.ContainsKey(groupName.ToLower) Then
                    Dim isVisible As Boolean = parameter.ToString.Equals(value, StringComparison.CurrentCultureIgnoreCase)
                    If onlyInvisible = True OrElse (onlyInvisible = False AndAlso isVisible = False) Then 'to make sure that when there are no controls in the group it isn't won't be visible agian
                        _groups(groupName.ToLower).Visible = isVisible
                        ReorderControls()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ParameterEditorSetConditionalEnabled(referencedParameterName As String, paramEditor As ParameterEditorControlBase, value As Boolean)
        If Not TypeOf paramEditor Is ParameterCollectionEditorControl Then
            paramEditor.SetConditionalEnabled(value)
        Else
            Dim collectionParameterEditor As ParameterCollectionEditorControl = DirectCast(paramEditor, ParameterCollectionEditorControl)
            collectionParameterEditor.SetConditionalEnabled(referencedParameterName, value)
        End If
    End Sub

    Private Sub AddGroupControl(groupControl As ParameterGroupBox, ByRef tabIndex As Integer)
        ' add an row to push the rows up; wraps content
        groupControl.TableLayoutControl.RowStyles.Add(New RowStyle(SizeType.Percent, 100))
        groupControl.SetTabIndex(tabIndex)

        Me.Controls.Add(groupControl)
        groupControl.BringToFront() ' MSDN: Controls are docked in reverse z-order, therefor BringToFront/SendToBack is the correct method to use here.
    End Sub

    Friend Sub ParameterEditorControl_ResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
        Dim resource As BinaryResource = Nothing
        Dim request = New ResourceRequestDTO()
        If e.TypedResourceType IsNot Nothing Then
            resource = ResourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
        Else
            resource = ResourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
        End If
        e.BinaryResource = resource
    End Sub

    Friend Sub ParameterEditorControl_EditResource(sender As Object, e As ResourceNameEventArgs)
        OnEditResource(e)
    End Sub

    Protected Sub CreateAdapterIfNeeded(adapter As ItemLayoutAdapter, itemLayoutTemplateName As String)
        If adapter Is Nothing Then
            _itemLayoutAdapterForItem = New ItemLayoutAdapter(itemLayoutTemplateName, Nothing, AddressOf ParameterEditorControl_ResourceNeeded)
        End If
    End Sub

#End Region

#End Region

#Region "Conditions"
    Private Sub ReInitializeConditionalObjects()
        CleanUpConditionalObjects()

        _redirectedParameterDictionary.Clear()
        _conditionalParameterDictionary.Clear()
        _conditionalParameterDependenciesDictionary.Clear()
        _groupConditionalParameterList.Clear()
    End Sub

    Private Sub CleanUpConditionalObjects()
        CleanUpDependencies()
        CleanUpRedirected()
        CleanUpConditional()
        CleanUpGroupConditional()
    End Sub

    Private Sub CleanUpDependencies()
        If (_conditionalParameterDependenciesDictionary IsNot Nothing) Then
            For Each pair In _conditionalParameterDependenciesDictionary
                RemoveHandler pair.Value.PropertyChanged, AddressOf ConditionalParameter_ParameterChanged
            Next
        End If
    End Sub

    Private Sub CleanUpGroupConditional()

        If (_groupConditionalParameterList IsNot Nothing) Then
            For Each parameterBase As ParameterBase In _groupConditionalParameterList
                RemoveHandler parameterBase.PropertyChanged, AddressOf GroupConditionalParameter_ParameterChanged
            Next
        End If
    End Sub

    Private Sub CleanUpConditional()

        If (_conditionalParameterDictionary IsNot Nothing) Then
            For Each parameterBase As ParameterBase In _conditionalParameterDictionary.Keys
                RemoveHandler parameterBase.PropertyChanged, AddressOf ConditionalParameter_ParameterChanged
            Next
        End If
    End Sub

    Private Sub CleanUpRedirected()

        If (_redirectedParameterDictionary IsNot Nothing) Then
            For Each parameterBase As ParameterBase In _redirectedParameterDictionary.Keys
                RemoveHandler parameterBase.PropertyChanged, AddressOf ReferencedParameter_ParameterChanged
            Next
        End If
    End Sub

#Region "Redirection"

    Private Sub AddToRedirectionDictionary(parameter As ParameterBase)
        If parameter.DesignerSettings.GetDesignerSettingByKey("redirectToTargetControlId") IsNot Nothing AndAlso parameter.DesignerSettings.GetDesignerSettingByKey("redirectToTargetParameterId") IsNot Nothing Then
            Dim redirectToTargetControlId As String = parameter.DesignerSettings.GetDesignerSettingByKey("redirectToTargetControlId").Value
            Dim redirectToTargetParameterId As String = parameter.DesignerSettings.GetDesignerSettingByKey("redirectToTargetParameterId").Value
            If Not String.IsNullOrEmpty(redirectToTargetControlId) AndAlso Not String.IsNullOrEmpty(redirectToTargetParameterId) Then
                Dim referenceParameter As ParameterBase = _parameterSets.GetParameter(redirectToTargetParameterId, redirectToTargetControlId)
                If referenceParameter IsNot Nothing Then

                    If Not _redirectedParameterDictionary.ContainsKey(referenceParameter) Then
                        'Add handler so the references can be updated when the referenced changed
                        _redirectedParameterDictionary.Add(referenceParameter, New List(Of ParameterBase))
                        AddHandler referenceParameter.PropertyChanged, AddressOf ReferencedParameter_ParameterChanged
                    End If
                    Dim parameterReferenceList As List(Of ParameterBase) = _redirectedParameterDictionary.Item(referenceParameter)

                    If Not parameterReferenceList.Contains(parameter) Then
                        parameterReferenceList.Add(parameter)
                    End If

                    'Take current value of referenced parameter. TFS#7841
                    parameter.SetValue(referenceParameter.ToString)

                Else
                    Debug.Assert(False, "Referenced parameter not found!")
                End If
            End If
        End If
    End Sub

    Private Sub ReferencedParameter_ParameterChanged(sender As Object, e As PropertyChangedEventArgs)
        'Update the parameter here
        Dim parameter As ParameterBase = Nothing
        parameter = TryCast(sender, ParameterBase)

        If parameter IsNot Nothing AndAlso _redirectedParameterDictionary.ContainsKey(parameter) Then
            For Each referenceParameter As ParameterBase In _redirectedParameterDictionary.Item(parameter)
                referenceParameter.SetValue(parameter.ToString)
            Next
        End If
    End Sub

#End Region ''redirection

#Region "Visibility"

    Private Function AddConditionalParameterToDictionary(referenceParameter As ParameterBase, paramUI As ParameterEditorControlBase, conditionalEnabledWhenValue As String) As Boolean
        Dim returnValue As Boolean = True
        If referenceParameter IsNot Nothing Then
            If Not _conditionalParameterDictionary.ContainsKey(referenceParameter) Then
                _conditionalParameterDictionary.Add(referenceParameter, New Dictionary(Of ParameterEditorControlBase, String))
                AddHandler referenceParameter.PropertyChanged, AddressOf ConditionalParameter_ParameterChanged

                'Add possible conditional parameter dependencies to dictionary
                AddConditionalParameterDependencyToDictionary(referenceParameter)
            End If
            Dim parameterReferenceList As Dictionary(Of ParameterEditorControlBase, String) = _conditionalParameterDictionary.Item(referenceParameter)
            If Not parameterReferenceList.ContainsKey(paramUI) Then parameterReferenceList.Add(paramUI, conditionalEnabledWhenValue)
            If Not referenceParameter.GetType Is GetType(CollectionParameter) Then
                If conditionalEnabledWhenValue.Contains(CONDITIONAL_OR) Then
                    Dim conditionalValues As String() = conditionalEnabledWhenValue.Split(CONDITIONAL_OR)
                    For Each conditionalValue As String In conditionalValues
                        returnValue = AddConditionalParameterToDictionary(referenceParameter, paramUI, conditionalValue)
                        If returnValue = True Then Exit For
                    Next
                ElseIf conditionalEnabledWhenValue.StartsWith(CONDITIONAL_NOT) AndAlso conditionalEnabledWhenValue.Contains(CONDITIONAL_EMPTY) Then
                    returnValue = Not String.IsNullOrEmpty(referenceParameter.ToString)
                ElseIf conditionalEnabledWhenValue.StartsWith(CONDITIONAL_NOT) Then
                    returnValue = Not (conditionalEnabledWhenValue.Substring(1) = referenceParameter.ToString)
                ElseIf conditionalEnabledWhenValue.Contains(CONDITIONAL_EMPTY) Then
                    returnValue = String.IsNullOrEmpty(referenceParameter.ToString)
                Else
                    returnValue = conditionalEnabledWhenValue = referenceParameter.ToString
                End If
            End If
        End If
        Return returnValue
    End Function

    Private Sub AddConditionalParameterDependencyToDictionary(referenceParameter As ParameterBase)
        'Check: does referenceParameter have conditionalDesignerSettings as well
        Dim conditionalDesignerSettings As Dictionary(Of String, String) = GetConditionalDesignerSettings(referenceParameter)
        If conditionalDesignerSettings.ContainsKey("enabled") AndAlso conditionalDesignerSettings.ContainsKey("switchPrm") Then
            Dim parameterCollectionSetId As String = _parameterSets.GetParameterSetCollectionNameByParameter(referenceParameter)
            Dim conditionalParameter As ParameterBase = _parameterSets.GetParameter(conditionalDesignerSettings("switchPrm"), parameterCollectionSetId)

            If Not _conditionalParameterDependenciesDictionary.ContainsKey(referenceParameter) Then
                _conditionalParameterDependenciesDictionary.Add(referenceParameter, conditionalParameter)
            End If
        End If
    End Sub

    Private Sub ConditionalParameter_ParameterChanged(sender As Object, e As PropertyChangedEventArgs)
        Dim parameter As ParameterBase = Nothing
        parameter = TryCast(sender, ParameterBase)
        PerformEnablingDisablingConditionalParameters(parameter, False)
        ReorderControls()
    End Sub

    Private Sub PerformEnablingDisablingConditionalParameters(parameter As ParameterBase, dependsOnDisabledParameter As Boolean)
        If parameter IsNot Nothing AndAlso _conditionalParameterDictionary.ContainsKey(parameter) Then
            If Not dependsOnDisabledParameter Then
                'first enable controls then disable the controls that should be disabled, because otherwise the groupbox will be made invisible first then visible. otherwise the groupbox could be made invisible first and the back to visible
                For Each paramEditor As ParameterEditorControlBase In _conditionalParameterDictionary.Item(parameter).Keys
                    If Not TypeOf parameter Is CollectionParameter Then
                        If _conditionalParameterDictionary.Item(parameter).Item(paramEditor).Contains(CONDITIONAL_OR) Then
                            Dim conditionalValues As String() = _conditionalParameterDictionary.Item(parameter).Item(paramEditor).Split(CONDITIONAL_OR)
                            For Each conditionalValue As String In conditionalValues
                                If CheckEnableParameter(conditionalValue, parameter.ToString, True) = True Then
                                    ParameterEditorSetConditionalEnabled(parameter.Name, paramEditor, True)
                                    Exit For
                                End If
                            Next
                        Else
                            If CheckEnableParameter(_conditionalParameterDictionary.Item(parameter).Item(paramEditor), parameter.ToString, True) = True Then ParameterEditorSetConditionalEnabled(parameter.Name, paramEditor, True)
                        End If
                    Else
                        ' JKEV 20120511
                        '  if a collectionparameter is used as conditional param, then the condition is TRUE if the collection parameter has one or more items
                        If DirectCast(parameter, CollectionParameter).Value.Count > 0 Then
                            ParameterEditorSetConditionalEnabled(parameter.Name, paramEditor, True)
                        End If
                    End If
                Next
            End If

            For Each paramEditor As ParameterEditorControlBase In _conditionalParameterDictionary.Item(parameter).Keys
                Dim disableParameter As Boolean = dependsOnDisabledParameter
                If Not disableParameter Then
                    If Not parameter.GetType Is GetType(CollectionParameter) Then
                        If _conditionalParameterDictionary.Item(parameter).Item(paramEditor).Contains(CONDITIONAL_OR) Then
                            Dim conditionalValues As String() = _conditionalParameterDictionary.Item(parameter).Item(paramEditor).Split(CONDITIONAL_OR)
                            Dim disablePrm As Boolean = True
                            For Each conditionalValue As String In conditionalValues
                                disablePrm = CheckEnableParameter(conditionalValue, parameter.ToString, False)
                                If disablePrm = False Then Exit For
                            Next
                            disableParameter = disablePrm
                        Else
                            If CheckEnableParameter(_conditionalParameterDictionary.Item(parameter).Item(paramEditor), parameter.ToString, False) = True Then disableParameter = True
                        End If
                    Else
                        ' JKEV 20120511
                        '  if a collectionparameter is used as conditional param, then the condition is TRUE if the collection parameter has one or more items
                        If DirectCast(parameter, CollectionParameter).Value.Count = 0 Then disableParameter = True
                    End If
                End If
                If disableParameter Then ParameterEditorSetConditionalEnabled(parameter.Name, paramEditor, False)
            Next

            'perform action for parameter on which this parameter depends as well
            If _conditionalParameterDependenciesDictionary IsNot Nothing AndAlso _conditionalParameterDependenciesDictionary.ContainsValue(parameter) Then
                _conditionalParameterDependenciesDictionary.Where(Function(c) c.Value.Name.Equals(parameter.Name)).ToList().ForEach(Sub(kvp)
                                                                                                                                        PerformEnablingDisablingConditionalParameters(kvp.Key, (dependsOnDisabledParameter OrElse CheckEnableParameter(kvp.Key.DesignerSettings.GetDesignerSettingByKey("conditionalEnabledWhenValue").Value, parameter.ToString, False)))
                                                                                                                                    End Sub)
            End If
        End If
    End Sub

    Private Sub InitialiseConditionalEnabledParameter(parameter As ParameterBase, paramUIControl As ParameterEditorControlBase, parameterCollectionSetId As String)
        Dim conditionalDesignerSettings As Dictionary(Of String, String) = GetConditionalDesignerSettings(parameter)
        If conditionalDesignerSettings.ContainsKey("enabled") Then InitializeVisiblityCondition(parameter, paramUIControl, parameterCollectionSetId, conditionalDesignerSettings)
        If TypeOf parameter Is CollectionParameter Then
            For Each innerParameter As ParameterBase In DirectCast(parameter, CollectionParameter).BluePrint.InnerParameters
                InitialiseConditionalEnabledParameter(innerParameter, paramUIControl, parameterCollectionSetId)
            Next
        End If
    End Sub

    Private Function InitializeVisiblityCondition(parameter As ParameterBase, paramUI As ParameterEditorControlBase, parameterCollectionSetId As String, conditionalDesignerSettings As Dictionary(Of String, String)) As Boolean
        Dim returnValue As Boolean = True
        If conditionalDesignerSettings.ContainsKey("switchPrm") AndAlso conditionalDesignerSettings.ContainsKey("value") Then
            If Not String.IsNullOrEmpty(conditionalDesignerSettings("switchPrm")) Then
                Dim referenceParameter As ParameterBase = _parameterSets.GetParameter(conditionalDesignerSettings("switchPrm"), parameterCollectionSetId)
                returnValue = AddConditionalParameterToDictionary(referenceParameter, paramUI, conditionalDesignerSettings("value"))
            End If
        End If
        Return returnValue
    End Function

    Private Function GetConditionalDesignerSettings(parameter As ParameterBase) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        Dim conditionalEnabled As String = parameter.DesignerSettings.GetSettingValueByKey("conditionalEnabled")
        If (Not String.IsNullOrEmpty(conditionalEnabled) AndAlso String.Equals(conditionalEnabled, Boolean.TrueString, StringComparison.OrdinalIgnoreCase)) AndAlso parameter.DesignerSettings.GetDesignerSettingByKey("conditionalEnabledSwitchParameter") IsNot Nothing AndAlso parameter.DesignerSettings.GetDesignerSettingByKey("conditionalEnabledWhenValue") IsNot Nothing Then
            result.Add("enabled", conditionalEnabled)
            Dim conditionalSwitchPrm As String = parameter.DesignerSettings.GetDesignerSettingByKey("conditionalEnabledSwitchParameter").Value
            If Not String.IsNullOrEmpty(conditionalSwitchPrm) Then result.Add("switchPrm", conditionalSwitchPrm)
            Dim conditionalValue As String = parameter.DesignerSettings.GetDesignerSettingByKey("conditionalEnabledWhenValue").Value
            If Not String.IsNullOrEmpty(conditionalValue) Then result.Add("value", conditionalValue)
        End If
        Return result
    End Function

#End Region ''visibility

#Region "Group"

    Private Sub AddGroupConditionalParameterToDictionary(parameter As ParameterBase)
        If (parameter.DesignerSettings.GetDesignerSettingByKey("groupConditionalEnabledSwitch") IsNot Nothing) AndAlso
                parameter.DesignerSettings.GetDesignerSettingByKey("groupConditionalEnabledWhenValue") IsNot Nothing Then

            If Not _groupConditionalParameterList.Contains(parameter) Then
                _groupConditionalParameterList.Add(parameter)
                AddHandler parameter.PropertyChanged, AddressOf GroupConditionalParameter_ParameterChanged
            End If

        End If
    End Sub

    Private Sub OptionalRegisterGroupCondition(param As ParameterBase)

        Dim groupConditionalEnabled As String = param.DesignerSettings.GetSettingValueByKey("groupConditionalEnabled")
        If (Not String.IsNullOrEmpty(groupConditionalEnabled) AndAlso String.Equals(groupConditionalEnabled, Boolean.TrueString, StringComparison.OrdinalIgnoreCase)) Then
            AddGroupConditionalParameterToDictionary(param)
        End If
    End Sub

#End Region ''Groups

#End Region ''Conditions

End Class
