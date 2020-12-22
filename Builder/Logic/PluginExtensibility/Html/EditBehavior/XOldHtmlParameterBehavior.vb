Imports Cito.Tester.Common
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public Class XOldHtmlParameterBehavior
        Inherits XhtmlParameterBehaviorBase


        Private ReadOnly _factory As New C1HtmlConverter
        Private _inlineRetriever As IInlineRetriever

        Public Sub New(resourceEntity As ResourceEntity,
                           resourceManager As ResourceManagerBase,
                           contextIdentifier As Integer?,
                           param As Cito.Tester.ContentModel.XHtmlParameter)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier, param)
            ForOldItem = True
        End Sub


        Public Overrides ReadOnly Property CanInsertMovies As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertAudio As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanCreateReferences As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property ConvertOldInlineToHtml As Boolean
            Get
                Return True
            End Get
        End Property



        Protected Overrides Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_C1RefToCitoRef()
            ret.LastConverter.NextConverter = New HtmlConverter_OldInlineToHtml(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML()
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveHyperlinks()
            ret.LastConverter.NextConverter = New HtmlConverter_DealWithMovedInlineElement(Me)
            Return ret
        End Function
        Protected Overrides Function GetInlineRetriever() As IInlineRetriever
            Debug.Assert(_inlineRetriever IsNot Nothing)
            Return _inlineRetriever
        End Function
        Protected Overrides Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_CitoRefToC1Ref()
            ret.LastConverter.NextConverter = New HtmlConverter_AssignDivId()
            Dim inline As New HtmlConverter_OldHtmlToInline(Me)
            _inlineRetriever = DirectCast(inline, IInlineRetriever)
            ret.LastConverter.NextConverter = inline
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier)
            Return ret
        End Function


        Public Overrides Function addDependency(nameOfResource As String, isItemLayoutTemplate As Boolean) As Boolean
            If isItemLayoutTemplate Then
                Return True
            Else
                Return MyBase.addDependency(nameOfResource, isItemLayoutTemplate)
            End If
        End Function

    End Class

End Namespace