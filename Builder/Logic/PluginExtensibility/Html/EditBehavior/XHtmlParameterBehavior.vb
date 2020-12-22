
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

Namespace PluginExtensibility.Html.EditBehavior
    Public Class XHtmlParameterBehavior
        Inherits XhtmlParameterBehaviorBase
        Implements IInlineHtmlEditBehavior

        Private Const INLINETEMPLATE_FINDINGOVERRIDE_PROPERTY As String = "inlineFO"

        Private _inlineRetriever As IInlineRetriever
        Private _inlineTemplates As New Dictionary(Of String, Dictionary(Of String, String))
        Private _canInsertCI As Boolean?

        Public Sub New(resourceEntity As ResourceEntity,
                   resourceManager As ResourceManagerBase,
                   contextIdentifier As Integer?,
                   iltAdapter As ItemLayoutAdapter,
                   stylesheets As Dictionary(Of String, String),
                   headerStyleElementContent As String,
                   param As XHtmlParameter)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier, iltAdapter, stylesheets, headerStyleElementContent, param)
        End Sub

        Public Sub New(resourceEntity As ResourceEntity,
                           resourceManager As ResourceManagerBase,
                           contextIdentifier As Integer?,
                           param As XHtmlParameter)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier, param)
        End Sub

        Public Overrides ReadOnly Property CanInsertControls As Boolean
            Get
                Return Not String.IsNullOrEmpty(DefaultInlineTemplate)
            End Get
        End Property

        public Overrides ReadOnly Property CanInsertCI As Boolean
            Get
                If _canInsertCI Is Nothing Then
                    _canInsertCI = CheckCanInsertCI()
                End If
                Return CBool(_canInsertCI)
            End Get
        End Property

        public Overrides ReadOnly Property CanSetTextToSpeechOptions As Boolean
            Get
                Return MyBase.CanSetTTSOptions
            End Get
        End Property



        Protected Overrides Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_C1RefToCitoRef()
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_DealWithMovedInlineControl(Me)
            ret.LastConverter.NextConverter = New HtmlConverter_DealWithMovedInlineElement(Me)
            ret.LastConverter.NextConverter = New HtmlConverter_Inline2Cito(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_OldInlineToHtml(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveFontInSpan()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveHyperlinks()
            ret.LastConverter.NextConverter = New HtmlConverter_TextToSpeechToHtml()
            Return ret
        End Function

        Protected Overrides Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_CitoRefToC1Ref()
            _inlineRetriever = New HtmlConverter_Cito2Inline(Me)
            ret.LastConverter.NextConverter = _inlineRetriever
            _inlineRetriever = New HtmlConverter_OldHtmlToInline(Me, _inlineRetriever.InlineElements)
            ret.LastConverter.NextConverter = _inlineRetriever
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier)
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
            ret.LastConverter.NextConverter = New HtmlConverter_HtmlToTextToSpeech()
            Return ret
        End Function

        Protected Overrides Function GetInlineRetriever() As IInlineRetriever
            Debug.Assert(_inlineRetriever IsNot Nothing)
            Return _inlineRetriever
        End Function


        Public Function CanCreate(inlineTemplate As String) As Boolean Implements IInlineHtmlEditBehavior.CanCreate
            Return True
        End Function

        Public Function CreateForInlineControl(editor As IXHtmlEditor) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForInlineControl
            Return CreateForInlineControl(editor, DefaultInlineTemplate)
        End Function

        Public Function CreateForInlineControl(editor As IXHtmlEditor, inlineTemplate As String) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForInlineControl
            If ResourceEntity Is Nothing Then Return Nothing
            Return HtmlInlineHandler.Create(editor, inlineTemplate, GetInlineTemplates(), GetFindingOverride(inlineTemplate), Nothing, ResourceEntity, ResourceEntity.BankId, ResourceManager, ForOldItem, DefaultNamespaceManager, GetStyle(), HeaderStyleElementContent)
        End Function

        Public Function CreateForCustomInteraction(editor As IXHtmlEditor) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForCustomInteraction
            Dim inlineTemplate As String = GetInlineCustomInteractionTemplate()
            If Not String.IsNullOrEmpty(inlineTemplate) AndAlso ResourceEntity IsNot Nothing Then
                Return HtmlInlineHandler.Create(editor, inlineTemplate, GetInlineTemplates(), GetFindingOverride(inlineTemplate), Nothing, ResourceEntity, ResourceEntity.BankId, ResourceManager, ForOldItem, DefaultNamespaceManager, GetStyle(), HeaderStyleElementContent)
            End If
            Return Nothing
        End Function

        Public Function CreateForPopup(editor As IXHtmlEditor) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForPopup
            Dim popupTemplate As String = GetPopupTemplate()
            If Not String.IsNullOrEmpty(popupTemplate) AndAlso ResourceEntity IsNot Nothing Then
                Return HtmlInlineHandler.Create(editor, popupTemplate, GetInlineTemplates(), GetFindingOverride(popupTemplate), Nothing, ResourceEntity, ResourceEntity.BankId, ResourceManager, ForOldItem, DefaultNamespaceManager, GetStyle(), HeaderStyleElementContent)
            End If
            Return Nothing
        End Function

        Public Property DefaultInlineTemplate As String Implements IInlineHtmlEditBehavior.DefaultInlineTemplate

        Public Function GetIconNameForTemplate(inlineTemplateName As String) As String Implements IInlineHtmlEditBehavior.GetIconNameForTemplate
            Dim d = _inlineTemplates(inlineTemplateName)
            If d.ContainsKey("icon") Then
                Return d("icon")
            End If
            Return String.Empty
        End Function

        Public Function GetInlineTemplate(name As String) As String Implements IInlineHtmlEditBehavior.GetInlineTemplate
            Dim d = _inlineTemplates(name)
            If d.ContainsKey("template") Then
                Return d("template")
            Else
                Debug.Assert(False, "'template' was NOT found in Grouped property!")
            End If
            Return String.Empty
        End Function

        Public ReadOnly Property InlineTemplates As IEnumerable(Of String) Implements IInlineHtmlEditBehavior.InlineTemplates
            Get
                Return _inlineTemplates.Keys
            End Get
        End Property

        Public ReadOnly Property InlineCustomInteractionTemplate As String Implements IInlineHtmlEditBehavior.InlineCustomInteractionTemplate
            Get
                Return GetInlineCustomInteractionTemplate()
            End Get
        End Property

        Public Function ParseInlineTemplatesString(inlineTemplates As String, inlineFindingOverride As String) As Boolean Implements IInlineHtmlEditBehavior.ParseInlineTemplatesString
            Dim reader As New GroupedPropertyReader(inlineTemplates)

            _inlineTemplates = reader.GetAsDictionary("text")

            If Not String.IsNullOrEmpty(inlineFindingOverride) Then
                For Each key As String In _inlineTemplates.Keys
                    If Not _inlineTemplates(key).ContainsKey(INLINETEMPLATE_FINDINGOVERRIDE_PROPERTY) Then
                        _inlineTemplates(key).Add(INLINETEMPLATE_FINDINGOVERRIDE_PROPERTY, inlineFindingOverride)
                    End If
                Next
            End If

        End Function

        Public Function GetPropertiesForTemplate(InlineTemplateName As String) As IDictionary(Of String, String) Implements IInlineHtmlEditBehavior.GetPropertiesForTemplate
            If (_inlineTemplates.ContainsKey(InlineTemplateName)) Then
                Return _inlineTemplates(InlineTemplateName)
            Else
                Return New Dictionary(Of String, String)
            End If
        End Function



        Private Function GetFindingOverride(inlineTemplatename As String) As String
            Dim result As String = MyBase.GetInlineFindingOverride
            If String.IsNullOrEmpty(result) Then
                Dim d = _inlineTemplates.FirstOrDefault(Function(x) x.Value.ContainsKey("template") AndAlso x.Value("template") = inlineTemplatename)

                If d.Value IsNot Nothing AndAlso d.Value.ContainsKey(INLINETEMPLATE_FINDINGOVERRIDE_PROPERTY) Then
                    Return d.Value(INLINETEMPLATE_FINDINGOVERRIDE_PROPERTY)
                End If
            End If
            Return result
        End Function

        Private Function CheckCanInsertCI() As Boolean
            Return Not String.IsNullOrEmpty(GetInlineCustomInteractionTemplate())
        End Function

        Protected Overrides Function CreateForPasting(editor As IXHtmlEditor) As HtmlHandlerBase
            Return New HtmlHandlerBase(editor, Me.ResourceEntity.BankId, Me.ResourceManager, GetInlineTemplates(), Me.ResourceEntity, Me.Param)
        End Function


    End Class

End Namespace