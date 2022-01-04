Imports Cito.Tester.Common
Imports Questify.Builder.Logic.HtmlHelpers
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public Class PopupContentBehavior
        Inherits XhtmlParameterBehaviorBase

        Private _inlineRetriever As IInlineRetriever

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

        Public Overrides ReadOnly Property IsToolstripVisible As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertReferences As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property StoreSizeOfHtml As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property ConvertOldInlineToHtml As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property CanSetTextToSpeechOptions As Boolean
            Get
                Return MyBase.CanSetTTSOptions
            End Get
        End Property

        Protected Overrides Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_C1RefToCitoRef()
            ret.LastConverter.NextConverter = New HtmlConverter_OldInlineToHtml(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_Inline2Cito(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML()
            ret.LastConverter.NextConverter = New HtmlConverter_DealWithMovedInlineControl(Me)
            ret.LastConverter.NextConverter = New HtmlConverter_DealWithMovedInlineElement(Me)
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveFontInSpan()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveHyperlinks()
            ret.LastConverter.NextConverter = New HtmlConverter_TextToSpeechToHtml()
            Return ret
        End Function

        Protected Overrides Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_CitoRefToC1Ref()
            _inlineRetriever = New HtmlConverter_OldHtmlToInline(Me)
            ret.LastConverter.NextConverter = _inlineRetriever
            _inlineRetriever = New HtmlConverter_Cito2Inline(Me, _inlineRetriever.InlineElements)
            ret.LastConverter.NextConverter = _inlineRetriever
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(ContextIdentifier)
            ret.LastConverter.NextConverter = New HtmlConverter_HtmlToTextToSpeech()
            Return ret
        End Function

        Protected Overrides Function ConvertForCalculationOfHtmlSize() As String
            Dim ret As IHtmlConverter = New HtmlConverter_PartialToFull(GetStyle(), HeaderStyleElementCont, ContextIdentifier, DefaultNamespaceManager())
            _inlineRetriever = New HtmlConverter_OldHtmlToInline(Me)
            ret.LastConverter.NextConverter = _inlineRetriever
            _inlineRetriever = New HtmlConverter_Cito2Inline(Me, _inlineRetriever.InlineElements)
            ret.LastConverter.NextConverter = _inlineRetriever
            Dim tmp = ret.ConvertHtml(Param.Value)
            Return tmp
        End Function

        Protected Overrides Function GetInlineRetriever() As IInlineRetriever
            Debug.Assert(_inlineRetriever IsNot Nothing)
            Return _inlineRetriever
        End Function

        Protected Overrides Function GetInlineTemplateNames() As IHtmlInlineTemplateNames
            Return New PopupHtmlInlineTemplateNames()
        End Function

    End Class

End Namespace