Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

Public Interface IInlineHtmlEditBehavior
    Inherits IHtmlEditorBehaviour

    Property DefaultInlineTemplate As String

    ReadOnly Property InlineTemplates As IEnumerable(Of String)

    ReadOnly Property InlineCustomInteractionTemplate As String

    Function CreateForInlineControl(editor As IXHtmlEditor) As HtmlInlineHandler

    Function CreateForInlineControl(editor As IXHtmlEditor, inlineTemplate As String) As HtmlInlineHandler

    Function CreateForCustomInteraction(editor As IXHtmlEditor) As HtmlInlineHandler

    Function CreateForPopup(editor As IXHtmlEditor) As HtmlInlineHandler

    Function CanCreate(inlineTemplate As String) As Boolean

    Function ParseInlineTemplatesString(inlineTemplates As String, inlineFindingOverride As String) As Boolean

    Function GetIconNameForTemplate(text As String) As String

    Function GetInlineTemplate(text As String) As String

    Function GetPropertiesForTemplate(InlineTemplateName As String) As IDictionary(Of String, String)

End Interface