Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public MustInherit Class XhtmlParameterBehaviorDefault
        Inherits XhtmlParameterBehaviorBase

        Protected InlineRetriever As IInlineRetriever

        Public Sub New(resourceEntity As ResourceEntity, resourceManager As ResourceManagerBase, contextIdentifier As Integer?, iltAdapter As ItemLayoutAdapter, stylesheets As Dictionary(Of String, String), headerStyleElementContent As String, param As XHtmlParameter)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier, iltAdapter, stylesheets, headerStyleElementContent, param)
        End Sub

        Public Sub New(resourceEntity As ResourceEntity,
                       resourceManager As ResourceManagerBase,
                       contextIdentifier As Integer?,
                       param As XHtmlParameter)
            Me.New(resourceEntity, resourceManager, contextIdentifier, Nothing, Nothing, String.Empty, param)
        End Sub

        Public Overrides ReadOnly Property CanInsertAudio As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertMovies As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertReferences As Boolean
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
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML()
            ret.LastConverter.NextConverter = New HtmlConverter_DealWithMovedInlineControl(Me)
            ret.LastConverter.NextConverter = New HtmlConverter_DealWithMovedInlineElement(Me)
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveFontInSpan()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveHyperlinks()

            Return ret
        End Function

        Protected Overrides Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_CitoRefToC1Ref()
            InlineRetriever = New HtmlConverter_OldHtmlToInline(Me)
            ret.LastConverter.NextConverter = InlineRetriever
            ret.LastConverter.NextConverter = New HtmlConverter_OldHtmlToInline(Me)
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier)

            Return ret
        End Function

        Protected Overrides Function GetInlineRetriever() As IInlineRetriever
            Debug.Assert(InlineRetriever IsNot Nothing)
            Return InlineRetriever
        End Function

    End Class

End Namespace
