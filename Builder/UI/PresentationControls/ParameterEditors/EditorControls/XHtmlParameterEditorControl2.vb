

Imports Questify.Builder.Model.ContentModel
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ContentModel.ParamValidator
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Logic.Service.Exceptions

Public Class XHtmlParameterEditorControl2
    Inherits ParameterEditorControlBase
    
#Region "Fields"
    Private ReadOnly _xhtmlParameter As XHtmlParameter
    Private ReadOnly _resourceEntity As EntityClasses.ResourceEntity

    Private _required As Boolean
    Private _inlineTemplate As String
    Private _inlineTemplates As String
    Private _inlineFindingOverride As String

    '----[Helper Classes]
    Private ReadOnly _htmlContentValidator As New HtmlContentValidator
    Private _behavior As BaseHtmlEditorBehavior = Nothing
    '-----------------------------------------------------------

    Public Event AddedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs)
    Public Event RemovedInlineCustomInteraction As EventHandler(Of InlineElementEventArgs)
    Public Event AddedInlineAspect As EventHandler(Of InlineElementEventArgs)
    Public Event RemovedInlineAspect As EventHandler(Of InlineElementEventArgs)
    Public Event HtmlSizeStored As EventHandler(Of SizeEventArgs)
#End Region

#Region "Constructor"

    Public Sub New(ByVal parent As ParameterSetsEditor,
               ByVal xhtmlParameter As XHtmlParameter,
               ByVal resourceEntity As EntityClasses.ResourceEntity,
               ByVal resourceManager As ResourceManagerBase,
               ByVal hasLoadedOldItemLayoutTemplate As Boolean,
               ByVal contextIdentifier As Nullable(Of Integer))
        MyBase.New(parent) 'Call base constructor

        'Take parameters.
        _xhtmlParameter = xhtmlParameter
        _resourceEntity = resourceEntity
        MyBase.ResourceManager = resourceManager
        MyBase.HasLoadedOldItemLayoutTemplate = hasLoadedOldItemLayoutTemplate

        InitializeComponent()
        LoadDesignerSetting(_xhtmlParameter)

        _behavior = GetBehavior(contextIdentifier)

        _behavior.Parameters = EditorParent.ParameterSets.GetParameters()

        editor.Initialize(_behavior)
        AddHandler editor.AddedInlineCustomInteraction, AddressOf HtmlEditor_AddedInlineCustomInteraction
        AddHandler editor.RemovedInlineCustomInteraction, AddressOf HtmlEditor_RemovedInlineCustomInteraction
        AddHandler editor.AddedInlineAspect, AddressOf HtmlEditor_AddedInlineAspect
        AddHandler editor.RemovedInlineAspect, AddressOf HtmlEditor_RemovedInlineAspect
        AddHandler editor.HtmlSizeStored, AddressOf HtmlEditor_HtmlSizeStored
        AddHandler editor.EditorReceivedFocus, AddressOf HtmlEditor_ReceivedFocus
    End Sub

    Private Function GetBehavior(contextIdentifier As Nullable(Of Integer)) As BaseHtmlEditorBehavior

        Dim behavior As BaseHtmlEditorBehavior = Nothing

        If (HasLoadedOldItemLayoutTemplate) Then
            behavior = New XOldHtmlParameterBehavior(_resourceEntity, ResourceManager, contextIdentifier, _xhtmlParameter)
        Else

            If _parent.ParentIsInlineElement AndAlso (ParentContainsMultiChoiceInteraction() OrElse ParentContainsInlineChoiceInteraction()) Then
                Dim ib = New InlineChoiceOptionBehavior(_resourceEntity, ResourceManager, contextIdentifier, EditorParent.ItemLayoutAdapterForItem, EditorParent.ReferencedStylesheetsFromItemLayoutTemplate, EditorParent.ReferencedStylesheetsHeaderStyleElementContent, _xhtmlParameter)
                behavior = ib
            ElseIf ParentContainsGapMatchRichText() Then
                Dim gb = New GapTextRichTextBehavior(_resourceEntity, ResourceManager, contextIdentifier, EditorParent.ItemLayoutAdapterForItem, EditorParent.ReferencedStylesheetsFromItemLayoutTemplate, EditorParent.ReferencedStylesheetsHeaderStyleElementContent, _xhtmlParameter)
                behavior = gb
            ElseIf ParameterIsPopupTrigger() Then
                Dim ptb = New PopupTriggerBehavior(_resourceEntity, ResourceManager, contextIdentifier, EditorParent.ItemLayoutAdapterForItem, EditorParent.ReferencedStylesheetsFromItemLayoutTemplate, EditorParent.ReferencedStylesheetsHeaderStyleElementContent, _xhtmlParameter)
                behavior = ptb
            ElseIf ParameterIsPopupContent() Then
                Dim pcb = New PopupContentBehavior(_resourceEntity, ResourceManager, contextIdentifier, EditorParent.ItemLayoutAdapterForItem, EditorParent.ReferencedStylesheetsFromItemLayoutTemplate, EditorParent.ReferencedStylesheetsHeaderStyleElementContent, _xhtmlParameter)
                behavior = pcb
            Else
                Dim newBehavior = New XHtmlParameterBehavior(_resourceEntity, ResourceManager, contextIdentifier, EditorParent.ItemLayoutAdapterForItem,
                                                         EditorParent.ReferencedStylesheetsFromItemLayoutTemplate, EditorParent.ReferencedStylesheetsHeaderStyleElementContent,
                                                         _xhtmlParameter) With {.DefaultInlineTemplate = _inlineTemplate}
                newBehavior.ParseInlineTemplatesString(_inlineTemplates, _inlineFindingOverride)
                behavior = newBehavior
            End If

        End If

        Return behavior
    End Function

    Private Function ParentContainsInlineChoiceInteraction() As Boolean
        Return _parent.ParameterSets.First().InnerParameters.OfType(Of InlineChoiceScoringParameter).Any()
    End Function

    Private Function ParentContainsMultiChoiceInteraction() As Boolean
        Return _parent.ParameterSets.First().InnerParameters.OfType(Of MultiChoiceScoringParameter).Any()
    End Function

    Private Function ParentContainsGapMatchRichText() As Boolean
        Return _parent.ParameterSets.Any(Function(p) p.InnerParameters.OfType(Of GapMatchRichTextScoringParameter).Any()) AndAlso _xhtmlParameter.Name = "gapTextRichText"
    End Function

    Private Function ParameterIsPopupTrigger() As Boolean
        Return _xhtmlParameter.Name = "triggerContent"
    End Function

    Private Function ParameterIsPopupContent() As Boolean
        Return _xhtmlParameter.Name = "popupContent"
    End Function
#End Region

#Region "Overrides"

    Public Overrides Property FormClosing As Boolean
        Get
            Return _formClosing
        End Get
        Set(value As Boolean)
            _formClosing = value
            If editor IsNot Nothing Then editor.FormClosing = value
        End Set
    End Property

    Public ReadOnly Property CanStoreSizeOfHtml As Boolean
        Get
            Return _behavior.StoreSizeOfHtml
        End Get
    End Property

    Public ReadOnly Property GetDesignerSettingValue(designerSettingKey As String) As String
        Get
            Return If(_xhtmlParameter.DesignerSettings.GetSettingValueByKey(designerSettingKey), String.Empty)
        End Get
    End Property

    Public Overrides Sub PreItemSave(ByVal hasLoadedOldItemLayoutTemplate As Boolean)
        _itemSaving = True
        editor.StopEditor()
    End Sub

    Public Overrides Sub PostItemSave()
        _itemSaving = False
    End Sub

    Public Overrides Sub RemoveAllResources()
        For Each resource As String In HtmlResourceExtractor.GetAllResourcesInHtml(_behavior.InlineElements.Values.Select(Function(v) v.Item1).ToList, _xhtmlParameter.Value)
            OnRemovingResource(New ResourceNameEventArgs(resource))
        Next
    End Sub

    Private Sub HtmlEditor_AddedInlineCustomInteraction(ByVal sender As Object, ByVal e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent AddedInlineCustomInteraction(Me, e)
        End If
    End Sub

    Private Sub HtmlEditor_RemovedInlineCustomInteraction(ByVal sender As Object, ByVal e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent RemovedInlineCustomInteraction(Me, e)
        End If
    End Sub

    Private Sub HtmlEditor_AddedInlineAspect(ByVal sender As Object, ByVal e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent AddedInlineAspect(Me, e)
        End If
    End Sub

    Private Sub HtmlEditor_RemovedInlineAspect(ByVal sender As Object, ByVal e As InlineElementEventArgs)
        If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
            RaiseEvent RemovedInlineAspect(Me, e)
        End If
    End Sub

    Private Sub HtmlEditor_HtmlSizeStored(sender As Object, e As SizeEventArgs)
        If e IsNot Nothing Then
            RaiseEvent HtmlSizeStored(sender, e)
        End If
    End Sub

    Private Sub XHtmlParameterEditorControl2_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        If Not EditorParent.CleaningControls Then
            HtmlEditor_ReceivedFocus(Me, True)
        End If
    End Sub

    Private Sub XHtmlParameterEditorControl2_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        HtmlEditor_ReceivedFocus(Me, False)
    End Sub

    Private Sub HtmlEditor_ReceivedFocus(sender As Object, setFocus As Boolean)
        If Not _itemSaving AndAlso Not _formClosing Then
            LayoutPanel.BackColor = If(setFocus, Color.FromArgb(0, 120, 215), Color.FromArgb(122, 122, 122))

            If setFocus Then
                editor.SetFocus()
            Else
                editor.SetMouseFocused(False)
            End If
        End If
    End Sub

    Public Overrides Function ValidateParameter() As String
        If (_required) Then
            'Does html contain value?
            If (Not _htmlContentValidator.HtmlContainsValue(_xhtmlParameter.Value)) Then
                Dim result As String = String.Empty
                Dim label As String = _xhtmlParameter.DesignerSettings.GetSettingValueByKey("label")

                If Not String.IsNullOrEmpty(label) Then
                    result = String.Format(My.Resources.ThisEditorMustBeFilledPleaseEnterContent, label)
                Else
                    result = My.Resources.MandatoryParameterMessage
                End If

                Return result
            End If
        End If
        Return String.Empty
    End Function

    Public Overrides Function ResourceUsedInThisParameter(resource As EntityClasses.ResourceEntity) As Boolean
        Return HtmlResourceExtractor.GetAllResourcesInHtml(_behavior.InlineElements.Values.Select(Function(v) v.Item1).ToList, _xhtmlParameter.Value).Contains(resource.Name)
    End Function

#End Region

#Region "Designer and Data Retrieval code"

    Private Sub LoadDesignerSetting(xhtmlParameter As XHtmlParameter)
        ' get designer settings
        If Not Boolean.TryParse(xhtmlParameter.DesignerSettings.GetSettingValueByKey("required"), _required) Then
            Throw New AppLogicException(String.Format(My.Resources.ErrorParsingDesignerSettingsForParameterSetting, xhtmlParameter.Name, "required"))
        End If

        'martijnh - 20121119 - retrieve value of inlinetemplate
        _inlineTemplate = xhtmlParameter.DesignerSettings.GetSettingValueByKey("inlinetemplate")

        'Not that these is a plural form!  inlineTemplates 
        _inlineTemplates = If(xhtmlParameter.DesignerSettings.GetSettingValueByKey("inlinetemplates"), String.Empty)
        _inlineFindingOverride = If(xhtmlParameter.DesignerSettings.GetSettingValueByKey("inlinefindingoverride"), String.Empty)
        Debug.Assert(_inlineTemplates IsNot Nothing)
    End Sub

    Private Sub HandleHtmlViewerResize(sender As System.Object, e As EventArgs) Handles editor.Resize
        Me.Height = editor.Height + 2
    End Sub

#End Region

#Region "Control Validation"

    Protected Overrides Sub OnValidated(e As EventArgs)
        If Me.EditorParent IsNot Nothing Then
            Me.EditorParent.ValidateThisEditor(Me) 'Validate            
        End If
    End Sub

#End Region

#Region "handling of consumed resources"

    Public Sub AddDependentResource(ByVal resourceName As String)
        OnAddingResource(New ResourceNameEventArgs(resourceName))
    End Sub

#End Region

#Region "Dispose Behaviour"

    Private Sub DoDispose(disposing As Boolean)

        'Dispose Behavior
        If disposing Then
            If editor IsNot Nothing Then
                RemoveHandler editor.AddedInlineCustomInteraction, AddressOf HtmlEditor_AddedInlineCustomInteraction
                RemoveHandler editor.RemovedInlineCustomInteraction, AddressOf HtmlEditor_RemovedInlineCustomInteraction
                RemoveHandler editor.AddedInlineAspect, AddressOf HtmlEditor_AddedInlineAspect
                RemoveHandler editor.RemovedInlineAspect, AddressOf HtmlEditor_RemovedInlineAspect
                RemoveHandler editor.HtmlSizeStored, AddressOf HtmlEditor_HtmlSizeStored
                RemoveHandler editor.EditorReceivedFocus, AddressOf HtmlEditor_ReceivedFocus

                editor.Dispose()
                editor = Nothing
            End If
        End If

    End Sub

#End Region

End Class
