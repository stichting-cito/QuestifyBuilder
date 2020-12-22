Imports System.Xml
Imports Questify.Builder.Logic.HtmlHelpers
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Factory
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

Public Interface IHtmlEditorBehaviour : Inherits IDisposable

    Function GetHtml() As String

    Sub SetHtml(html As String)

    ReadOnly Property IsToolstripVisible As Boolean
    ReadOnly Property DoHeightUpdate As Boolean
    ReadOnly Property CanInsertControls As Boolean
    ReadOnly Property CanInsertImages As Boolean
    ReadOnly Property CanInsertMovies As Boolean
    ReadOnly Property CanInsertAudio As Boolean
    ReadOnly Property CanInsertFormula As Boolean
    ReadOnly Property CanInsertCI As Boolean
    ReadOnly Property CanInsertPopup As Boolean
    ReadOnly Property CanCreateReferences As Boolean
    ReadOnly Property CanInsertReferences As Boolean
    ReadOnly Property CanSelectTextToSpeech As Boolean
    ReadOnly Property CanSetTextToSpeechOptions As Boolean
    ReadOnly Property CanRemoveTTS as Boolean
    ReadOnly Property StoreSizeOfHtml As Boolean
    ReadOnly Property ConvertOldInlineToHtml As Boolean
    ReadOnly Property ContextIdentifier As Integer?
    ReadOnly Property DefaultNamespaceManager As XmlNamespaceManager
    ReadOnly Property InlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean))

    Property Parameters As List(Of ParameterBase)

    Property InlineElementPlaceholders As Dictionary(Of String, XmlNode)
    Sub AddInlineElementPlaceholder(inlineElementIdentifier As string, placeHolder As XmlNode)

    ReadOnly Property ShouldSwitchToPreviewModeOnLostFocus As Boolean

    Function CreateForImage(editor As IXHtmlEditor) As HtmlInlineHandler

    Function CreateForVideo(editor As IXHtmlEditor) As HtmlInlineHandler

    Function CreateForAudio(editor As IXHtmlEditor) As HtmlInlineHandler

    Function InlineToEdit(editor As IXHtmlEditor, inlineElement As InlineElement) As HtmlInlineHandler

    Function CreateHtmlReferencesHandler(editor As IXHtmlEditor) As HtmlReferencesHandler

    Function CreateFormulaHandler(editor As IXHtmlEditor) As HtmlFormulaHandler

    Function CreateForPasting(editor As IXHtmlEditor) As HtmlHandlerBase

    Function CreateInlineConverter() As HtmlInlineConverter

    Function CreateResourceFactory() As ResourceFactory

    Function GetStyle() As Dictionary(Of String, String)

    Function AddDependency(inlineElement As InlineElement) As Boolean

    Function AddDependency(nameOfResource As String, isItemLayoutTemplate As Boolean) As Boolean
    Function RemoveDependency(nameOfResource As String) As Boolean

    Function ConvertForCalculationOfHtmlSize() As String

End Interface