Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ItemProcessing
Imports System.Linq
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Factories

Public Class InlineElementPropertiesDialog

    Private ReadOnly _resourceManager As ResourceManagerBase
    Private ReadOnly _inlineElement As InlineElement
    Private ReadOnly _resourceEntity As ResourceEntity
    Private ReadOnly _shouldSortParameters As Boolean = False
    Private _originalHash As Byte()
    Private ReadOnly _isNew As Boolean = True
    Private ReadOnly _hasLoadedOldItemLayoutTemplate As Boolean = False
    Private ReadOnly _parameterSet As ParameterSetCollection
    Private ReadOnly _bankId As Integer
    Private ReadOnly _contextIdentifier As Integer

    Private _shouldUpdateCustomInteractionParameters As Boolean = False
    Private _compareCustomInteractionResourceParameter As Tuple(Of String, String)
    Private _compareCustomInteractionIsScorableParameter As Tuple(Of Boolean, Boolean)

    Private Const COPYDIMENSIONSFROMTOPREFIX As String = "copyDimensionsFromToPrefix"

    Public Sub New(ByVal inlineElement As InlineElement, ByVal inlineName As String, ByVal resourceEntity As ResourceEntity, ByVal resourceManager As ResourceManagerBase, ByVal hasLoadedOldItemLayoutTemplate As Boolean)
        Me.New(inlineElement, inlineName, resourceEntity, resourceManager, hasLoadedOldItemLayoutTemplate, Nothing)
    End Sub

    Public Sub New(ByVal inlineElement As InlineElement, ByVal inlineName As String, ByVal resourceEntity As ResourceEntity, ByVal resourceManager As ResourceManagerBase, ByVal hasLoadedOldItemLayoutTemplate As Boolean, ByVal cachingStrategy As IITemSetupCacheHelper)
        Me.New(inlineElement, inlineName, resourceEntity, resourceManager, hasLoadedOldItemLayoutTemplate, cachingStrategy, Nothing, String.Empty)
    End Sub

    Public Sub New(ByVal inlineElement As InlineElement, ByVal inlineName As String, ByVal resourceEntity As ResourceEntity, ByVal resourceManager As ResourceManagerBase, ByVal hasLoadedOldItemLayoutTemplate As Boolean, ByVal cachingStrategy As IITemSetupCacheHelper, stylesheetsForXhtmlEditors As Dictionary(Of String, String), headerStyleElementContentForXhtmlEditors As String)
        InitializeComponent()

        _resourceEntity = resourceEntity
        _resourceManager = resourceManager

        _contextIdentifier = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(_resourceManager)
        Me.ParameterSetsEditorInstance.ContextIdentifierForEditors = _contextIdentifier
        If stylesheetsForXhtmlEditors IsNot Nothing Then
            Me.ParameterSetsEditorInstance.ReferencedStylesheetsFromItemLayoutTemplate = stylesheetsForXhtmlEditors
            Me.ParameterSetsEditorInstance.ReferencedStylesheetsHeaderStyleElementContent = headerStyleElementContentForXhtmlEditors
        End If
        Me.ParameterSetsEditorInstance.ParentIsInlineElement = True

        _hasLoadedOldItemLayoutTemplate = hasLoadedOldItemLayoutTemplate

        If Not InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(inlineName) Then
            Dim psHelper As New ParameterSetCollectionHelper(resourceManager, inlineName)
            If cachingStrategy IsNot Nothing Then psHelper.CachingStrategy = cachingStrategy
            _parameterSet = psHelper.GetExtractedParameters
        Else
            _parameterSet = InlineMediaTemplateHelper.GetParameterSetFromEmbeddedResource(inlineName)
        End If
        _shouldSortParameters = _parameterSet.ShouldSort

        If (inlineElement Is Nothing) Then
            _inlineElement = New InlineElement()
            _inlineElement.Parameters.AddRange(_parameterSet)
            _isNew = True
        Else
            _inlineElement = inlineElement
            _isNew = False
        End If

        _bankId = _resourceEntity.BankId
        Me.Height = ParameterSetsEditorInstance.Height
    End Sub

    Protected ReadOnly Property IsDirty() As Boolean
        Get
            Return (ResourceDataIsDirty OrElse _isNew)
        End Get
    End Property

    Protected ReadOnly Property ResourceDataIsDirty() As Boolean
        Get
            Dim currentEntityHash As Byte() = If(_inlineElement IsNot Nothing, _inlineElement.GetMD5Hash(), Nothing)
            Return Not ArrayHelper.CompareByteArray(_originalHash, currentEntityHash)
        End Get
    End Property

    Public ReadOnly Property InlineElementParameters() As ParameterSetCollection
        Get
            If _inlineElement IsNot Nothing Then
                Return _inlineElement.Parameters
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property ShouldUpdateCustomInteractionParameters() As Boolean
        Get
            Return _shouldUpdateCustomInteractionParameters AndAlso (Not _compareCustomInteractionResourceParameter.Item1.Equals(_compareCustomInteractionResourceParameter.Item2, StringComparison.OrdinalIgnoreCase) OrElse Not _compareCustomInteractionIsScorableParameter.Item1.Equals(_compareCustomInteractionIsScorableParameter.Item2))
        End Get
    End Property

    Public ReadOnly Property AddedInlineAspect() As Boolean
        Get
            Return _isNew AndAlso _inlineElement.Parameters.Any(Function(ps) ps.InnerParameters.Any(Function(p) TypeOf p Is AspectScoringParameter))
        End Get
    End Property

    Private Sub BindControls()
        With ParameterSetsEditorInstance
            .HasLoadedOldItemLayoutTemplate = _hasLoadedOldItemLayoutTemplate
            .ShouldSort = _shouldSortParameters
            .ResourceEntity = _resourceEntity
            .ResourceManager = _resourceManager
            .ParameterSets = _inlineElement.Parameters
        End With
        Me.ValidateInlineElement(False)
    End Sub

    Private Sub FillDimensionsWhenSettingHtmlSize()
        If _parameterSet Is Nothing Then
            Return
        End If

        Dim params = _parameterSet.GetParameters()
        For Each xhtmlParameterEditor In ParameterSetsEditorInstance.GetAllControlsOfType(Of XHtmlParameterEditorControl2)()
            If Not xhtmlParameterEditor.CanStoreSizeOfHtml Then
                Continue For
            End If

            Dim widthParam = GetSpecialParameter(Of Integer)(params, SpecialParameter.Width)
            Dim heightParam = GetSpecialParameter(Of Integer)(params, SpecialParameter.Height)

            AddHandler xhtmlParameterEditor.HtmlSizeStored, Sub(o As Object, e As SizeEventArgs)
                                                                If e IsNot Nothing Then
                                                                    If e.Size.Width > 0 Then
                                                                        widthParam.SetValue(e.Size.Width - 4)
                                                                    End If
                                                                    If e.Size.Height > 0 Then
                                                                        heightParam.SetValue(e.Size.Height - 4)
                                                                    End If
                                                                End If
                                                            End Sub
        Next
    End Sub

    Private Sub CopyDimensionsOnChangingEditSize()
        For Each boolEditor In ParameterSetsEditorInstance.GetAllControlsOfType(Of BooleanParameterEditorControl)()
            If _parameterSet IsNot Nothing AndAlso Not String.IsNullOrEmpty(boolEditor.GetDesignerSettingValue(COPYDIMENSIONSFROMTOPREFIX)) Then
                Dim params = _parameterSet.GetParameters()
                If boolEditor.GetDesignerSettingValue(COPYDIMENSIONSFROMTOPREFIX).Split("|"c).Count >= 2 Then
                    Dim fromPrmPrefix As String = boolEditor.GetDesignerSettingValue(COPYDIMENSIONSFROMTOPREFIX).Split("|"c)(0)
                    Dim toPrmPrefix As String = boolEditor.GetDesignerSettingValue(COPYDIMENSIONSFROMTOPREFIX).Split("|"c)(1)
                    Dim fromWidthParam = GetSpecialParameter(Of Integer)(params, SpecialParameter.Width, fromPrmPrefix)
                    Dim fromHeightParam = GetSpecialParameter(Of Integer)(params, SpecialParameter.Height, fromPrmPrefix)
                    Dim toWidthParam = GetSpecialParameter(Of Integer)(params, SpecialParameter.Width, toPrmPrefix)
                    Dim toHeightParam = GetSpecialParameter(Of Integer)(params, SpecialParameter.Height, toPrmPrefix)

                    Dim copyDimensions As Boolean = (fromWidthParam IsNot Nothing AndAlso fromHeightParam IsNot Nothing AndAlso
                                                     toWidthParam IsNot Nothing AndAlso toHeightParam IsNot Nothing
                                                     )

                    AddHandler boolEditor.BooleanParameter.PropertyChanged, Sub(o As Object, e As System.ComponentModel.PropertyChangedEventArgs)
                                                                                If boolEditor.BooleanParameter.Value = False AndAlso copyDimensions Then
                                                                                    toWidthParam.SetValue(fromWidthParam.Value.ToString)
                                                                                    toHeightParam.SetValue(fromHeightParam.Value.ToString)
                                                                                End If
                                                                            End Sub
                End If
            End If
        Next
    End Sub

    Private Sub UpdateCustomInteractionParametersWhenUpdatingCI()
        For Each resourceParameterEditor In ParameterSetsEditorInstance.GetAllControlsOfType(Of CustomInteractionResourceParameterEditorControl)()
            If resourceParameterEditor.ResourceParameter IsNot Nothing Then
                Dim prm = resourceParameterEditor.ResourceParameter
                If TypeOf prm Is CustomInteractionResourceParameter AndAlso DirectCast(prm, CustomInteractionResourceParameter).InlineUsage Then
                    _compareCustomInteractionResourceParameter = New Tuple(Of String, String)(DirectCast(prm, CustomInteractionResourceParameter).Value, DirectCast(prm, CustomInteractionResourceParameter).Value)

                    Dim isScorableParam = GetScorableParameter()
                    If isScorableParam IsNot Nothing Then
                        _compareCustomInteractionIsScorableParameter = New Tuple(Of Boolean, Boolean)(isScorableParam.Value, isScorableParam.Value)

                        AddHandler isScorableParam.PropertyChanged, Sub(o As Object, e As System.ComponentModel.PropertyChangedEventArgs)
                                                                        _shouldUpdateCustomInteractionParameters = True
                                                                        If TypeOf o Is BooleanParameter Then
                                                                            _compareCustomInteractionIsScorableParameter = New Tuple(Of Boolean, Boolean)(_compareCustomInteractionIsScorableParameter.Item1, DirectCast(o, BooleanParameter).Value)
                                                                        End If
                                                                    End Sub
                    Else
                        _compareCustomInteractionIsScorableParameter = New Tuple(Of Boolean, Boolean)(prm.Scorable, prm.Scorable)
                        AddHandler resourceParameterEditor.ScorableChanged, Sub(o As Object, e As EventArgs)
                                                                                _shouldUpdateCustomInteractionParameters = True
                                                                                If TypeOf o Is CheckBox Then
                                                                                    _compareCustomInteractionIsScorableParameter = New Tuple(Of Boolean, Boolean)(prm.Scorable, DirectCast(o, CheckBox).Checked)
                                                                                End If
                                                                            End Sub
                    End If

                    AddHandler resourceParameterEditor.AddingResource, Sub(o As Object, e As ResourceNameEventArgs)
                                                                           _shouldUpdateCustomInteractionParameters = True
                                                                           _compareCustomInteractionResourceParameter = New Tuple(Of String, String)(_compareCustomInteractionResourceParameter.Item1, e.ResourceName)
                                                                       End Sub

                    AddHandler resourceParameterEditor.RemovingResource, Sub(o As Object, e As ResourceNameEventArgs)
                                                                             _shouldUpdateCustomInteractionParameters = True
                                                                             _compareCustomInteractionResourceParameter = New Tuple(Of String, String)(_compareCustomInteractionResourceParameter.Item1, String.Empty)
                                                                         End Sub

                End If
            End If
        Next
    End Sub

    Private Function GetSpecialParameter(Of T)(params As List(Of ParameterBase), type As SpecialParameter, Optional prefix As String = "") As Parameter(Of T)
        Dim name As String = String.Empty
        Select Case type
            Case SpecialParameter.Width
                name = If(String.IsNullOrEmpty(prefix), "width", prefix + "width")
            Case SpecialParameter.Height
                name = If(String.IsNullOrEmpty(prefix), "height", prefix + "height")
            Case SpecialParameter.EditSize
                name = If(String.IsNullOrEmpty(prefix), "editsize", String.Format("edit{0}size", prefix))
        End Select
        Dim param As ParameterBase = params.FirstOrDefault(Function(p) p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        If (TypeOf param Is Parameter(Of T)) Then
            Return DirectCast(param, Parameter(Of T))
        Else
            Return Nothing
        End If
    End Function

    Private Function GetScorableParameter() As BooleanParameter
        If _parameterSet IsNot Nothing Then
            Dim param As ParameterBase = _parameterSet.GetParameters().FirstOrDefault(Function(p) p.Name.Equals("isScorable", StringComparison.OrdinalIgnoreCase))
            Return TryCast(param, BooleanParameter)
        End If
        Return Nothing
    End Function

    Private Sub InlineElementPropertiesDialog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not SetupInlineElement() Then
            Me.Close()
            Exit Sub
        End If

        BindControls()
        FillDimensionsWhenSettingHtmlSize()
        CopyDimensionsOnChangingEditSize()
        UpdateCustomInteractionParametersWhenUpdatingCI()

        CenterForm()
    End Sub

    Private Sub CenterForm()
        Dim r As Rectangle = Screen.FromPoint(Me.Location).WorkingArea

        Dim x = r.Left + (r.Width - Me.Width) \ 2
        Dim y = r.Top + (r.Height - Me.Height) \ 2

        Me.Location = New Point(x, y)
    End Sub

    Protected Overrides Function OnOk() As Boolean
        Me.DialogResult = DialogResult.OK
        Return False
    End Function

    Private Sub ItemEditor_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (Me.DialogResult = DialogResult.OK) Then
            If Not (SaveInlineElementParameters()) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Function SaveInlineElementParameters() As Boolean
        Dim result As Boolean = False
        ParameterSetsEditorInstance.PreItemSave()
        If (ValidateInlineElement(True)) Then
            result = True
        End If
        ParameterSetsEditorInstance.PostItemSave()
        Return result
    End Function

    Private Function ValidateInlineElement(ByVal showMessageBox As Boolean) As Boolean
        Dim validatingFailed As Boolean = False
        Dim errorMessageBuilder As New System.Text.StringBuilder()
        errorMessageBuilder.AppendFormat(My.Resources.ItemEditor_ValidateItem_ValidationErrors, Environment.NewLine)

        Dim editorsErrorResult As String = ParameterSetsEditorInstance.ValidateParameterEditors()
        If Not String.IsNullOrEmpty(editorsErrorResult) Then
            errorMessageBuilder.Append(editorsErrorResult)
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

    Private Function SetupInlineElement() As Boolean
        If _isNew Then
            If Not AddResourceParameterDefaultValuesToDependencyCollection(_inlineElement.Parameters) Then
                Me.Close()
                Return False
            End If

            _originalHash = _inlineElement.GetMD5Hash()

        Else
            ParameterHandler.Merge(_parameterSet, _inlineElement.Parameters)
            _inlineElement.Parameters.Clear()
            _inlineElement.Parameters.AddRange(_parameterSet)
        End If
        Return True
    End Function

    Private Function AddResourceParameterDefaultValuesToDependencyCollection(ByVal paramSetCollection As ParameterSetCollection) As Boolean
        If paramSetCollection IsNot Nothing Then
            For Each paramSet As ParameterCollection In paramSetCollection
                If paramSet IsNot Nothing Then
                    Dim referencedResources = ResourceFactory.Instance.GetResourcesByNamesWithOption(_resourceEntity.BankId, paramSet.InnerParameters.Where(Function(prm) TypeOf prm Is ResourceParameter AndAlso Not String.IsNullOrEmpty(CType(prm, ResourceParameter).Value)).Select(Function(prm) CType(prm, ResourceParameter).Value).ToList(), New ResourceRequestDTO())

                    For Each parameter As ParameterBase In paramSet.InnerParameters.Where(Function(prm) TypeOf prm Is ResourceParameter AndAlso Not String.IsNullOrEmpty(CType(prm, ResourceParameter).Value))
                        Dim value As String = DirectCast(parameter, ResourceParameter).Value

                        Dim referencedResource = referencedResources.FirstOrDefault(Function(r) CType(r, ResourceEntity).Name.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                        If referencedResource IsNot Nothing Then
                            If Not _resourceEntity.ContainsDependentResource(CType(referencedResource, ResourceEntity)) Then
                                Dim depResource As New DependentResourceEntity() With {.Resource = _resourceEntity, .DependentResource = CType(referencedResource, ResourceEntity)}
                                _resourceEntity.DependentResourceCollection.Add(depResource)
                            End If
                        Else
                            MessageBox.Show(String.Format(My.Resources.ResourceParameterContainsValueNotInBank, parameter.Name, value), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Return False
                        End If
                    Next
                End If
            Next
        End If

        Return True
    End Function

    Enum SpecialParameter
        Width
        Height
        EditSize
    End Enum

End Class