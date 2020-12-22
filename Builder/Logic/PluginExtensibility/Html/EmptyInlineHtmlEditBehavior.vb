Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

Namespace PluginExtensibility.Html

    Public Class EmptyInlineHtmlEditBehavior
        Inherits HtmlEditorBehaviorDecorator
        Implements IInlineHtmlEditBehavior

        Public Sub New(decoree As IHtmlEditorBehaviour)
            MyBase.New(decoree)
        End Sub


        Public Function CanCreate(inlineTemplate As String) As Boolean Implements IInlineHtmlEditBehavior.CanCreate
            Return False
        End Function

        Public Function CreateForInlineControl(editor As IXHtmlEditor) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForInlineControl
            Return Nothing
        End Function

        Public Function CreateForInlineControl(editor As IXHtmlEditor, inlineTemplate As String) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForInlineControl
            Return Nothing
        End Function

        Public Function CreateForCustomInteraction(editor As IXHtmlEditor) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForCustomInteraction
            Return Nothing
        End Function

        Public Function CreateForPopup(editor As IXHtmlEditor) As HtmlInlineHandler Implements IInlineHtmlEditBehavior.CreateForPopup
            Return Nothing
        End Function

        Public Property DefaultInlineTemplate As String Implements IInlineHtmlEditBehavior.DefaultInlineTemplate

        Public Function GetIconNameForTemplate(InlineTemplateName As String) As String Implements IInlineHtmlEditBehavior.GetIconNameForTemplate
            Return String.Empty
        End Function

        Public Function GetTitleForTemplate(InlineTemplateName As String) As String Implements IInlineHtmlEditBehavior.GetInlineTemplate
            Return String.Empty
        End Function

        Public ReadOnly Property InlineTemplates As IEnumerable(Of String) Implements IInlineHtmlEditBehavior.InlineTemplates
            Get
                Return New List(Of String)
            End Get
        End Property

        Public ReadOnly Property InlineCustomInteractionTemplate As String Implements IInlineHtmlEditBehavior.InlineCustomInteractionTemplate
            Get
                Return String.Empty
            End Get
        End Property

        Public Function ParseInlineTemplatesString(inlineTemplates As String, inlineFindingOverride As String) As Boolean Implements IInlineHtmlEditBehavior.ParseInlineTemplatesString
            Return False
        End Function

        Public Function GetPropertiesForTemplate(InlineTemplateName As String) As IDictionary(Of String, String) Implements IInlineHtmlEditBehavior.GetPropertiesForTemplate
            Return New Dictionary(Of String, String)
        End Function


    End Class

End Namespace