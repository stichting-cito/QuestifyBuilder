Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HtmlHelpers
Imports Questify.Builder.Logic.PluginExtensibility.Html.Factory
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

Namespace PluginExtensibility.Html

    Public Class HtmlEditorBehaviorDecorator
        Implements IHtmlEditorBehaviour

        Private _decoree As IHtmlEditorBehaviour
        Public Sub New(decoree As IHtmlEditorBehaviour)
            _decoree = decoree
        End Sub


        Public Overridable Function addDependency(inlineElement As InlineElement) As Boolean Implements IHtmlEditorBehaviour.AddDependency
            _decoree.AddDependency(inlineElement)
        End Function

        Public Overridable Function addDependency(nameOfResource As String, isItemLayoutTemplate As Boolean) As Boolean Implements IHtmlEditorBehaviour.AddDependency
            _decoree.AddDependency(nameOfResource, isItemLayoutTemplate)
        End Function

        Public Overridable ReadOnly Property CanCreateReferences As Boolean Implements IHtmlEditorBehaviour.CanCreateReferences
            Get
                Return _decoree.CanCreateReferences
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertAudio As Boolean Implements IHtmlEditorBehaviour.CanInsertAudio
            Get
                Return _decoree.CanInsertAudio
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertControls As Boolean Implements IHtmlEditorBehaviour.CanInsertControls
            Get
                Return _decoree.CanInsertControls
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertFormula As Boolean Implements IHtmlEditorBehaviour.CanInsertFormula
            Get
                Return _decoree.CanInsertFormula
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertImages As Boolean Implements IHtmlEditorBehaviour.CanInsertImages
            Get
                Return _decoree.CanInsertImages
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertMovies As Boolean Implements IHtmlEditorBehaviour.CanInsertMovies
            Get
                Return _decoree.CanInsertMovies
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertReferences As Boolean Implements IHtmlEditorBehaviour.CanInsertReferences
            Get
                Return _decoree.CanInsertReferences
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertCI As Boolean Implements IHtmlEditorBehaviour.CanInsertCI
            Get
                Return _decoree.CanInsertCI
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertPopup As Boolean Implements IHtmlEditorBehaviour.CanInsertPopup
            Get
                Return _decoree.CanInsertPopup
            End Get
        End Property

        Public Overridable ReadOnly Property CanSelectTextToSpeech As Boolean Implements IHtmlEditorBehaviour.CanSelectTextToSpeech
            Get
                Return _decoree.CanSelectTextToSpeech
            End Get
        End Property

        Public Overridable ReadOnly Property CanSetTextToSpeechOptions As Boolean Implements IHtmlEditorBehaviour.CanSetTextToSpeechOptions
            Get
                Return _decoree.CanSetTextToSpeechOptions
            End Get
        End Property

        Public Overridable ReadOnly Property CanRemoveTTS As Boolean Implements IHtmlEditorBehaviour.CanRemoveTTS
            Get
                Return _decoree.CanRemoveTTS
            End Get
        End Property

        Public Overridable ReadOnly Property StoreSizeOfHtml As Boolean Implements IHtmlEditorBehaviour.StoreSizeOfHtml
            Get
                Return _decoree.StoreSizeOfHtml
            End Get
        End Property

        Protected Friend Overridable ReadOnly Property ConvertOldInlineToHtml As Boolean Implements IHtmlEditorBehaviour.ConvertOldInlineToHtml
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property ContextIdentifier As Integer? Implements IHtmlEditorBehaviour.ContextIdentifier
            Get
                Return _decoree.ContextIdentifier
            End Get
        End Property

        Public Overridable Function CreateForAudio(editor As IXHtmlEditor) As HtmlInlineHandler Implements IHtmlEditorBehaviour.CreateForAudio
            Return _decoree.CreateForAudio(editor)
        End Function

        Public Sub AddInlineElementPlaceholder(inlineElementIdentifier As String, placeHolder As XmlNode) Implements IHtmlEditorBehaviour.AddInlineElementPlaceholder
            _decoree.AddInlineElementPlaceholder(inlineElementIdentifier, placeHolder)
        End Sub

        Public ReadOnly Property ShouldSwitchToPreviewModeOnLostFocus As Boolean Implements IHtmlEditorBehaviour.ShouldSwitchToPreviewModeOnLostFocus
            Get
                Return _decoree.ShouldSwitchToPreviewModeOnLostFocus()
            End Get
        End Property

        Public Overridable Function CreateForImage(editor As IXHtmlEditor) As HtmlInlineHandler Implements IHtmlEditorBehaviour.CreateForImage
            Return _decoree.CreateForImage(editor)
        End Function

        Public Overridable Function CreateFormulaHandler(editor As IXHtmlEditor) As HtmlFormulaHandler Implements IHtmlEditorBehaviour.CreateFormulaHandler
            Return _decoree.CreateFormulaHandler(editor)
        End Function

        Public Overridable Function CreateForPasting(editor As IXHtmlEditor) As HtmlHandlerBase Implements IHtmlEditorBehaviour.CreateForPasting
            Return _decoree.CreateForPasting(editor)
        End Function

        Public Overridable Function CreateForVideo(editor As IXHtmlEditor) As HtmlInlineHandler Implements IHtmlEditorBehaviour.CreateForVideo
            Return _decoree.CreateForVideo(editor)
        End Function

        Public Overridable Function CreateHtmlReferencesHandler(editor As IXHtmlEditor) As HtmlReferencesHandler Implements IHtmlEditorBehaviour.CreateHtmlReferencesHandler
            Return _decoree.CreateHtmlReferencesHandler(editor)
        End Function

        Public Overridable Function CreateInlineConverter() As HtmlInlineConverter Implements IHtmlEditorBehaviour.CreateInlineConverter
            Return _decoree.CreateInlineConverter()
        End Function

        Public Overridable Function CreateResourceFactory() As ResourceFactory Implements IHtmlEditorBehaviour.CreateResourceFactory
            Return _decoree.CreateResourceFactory()
        End Function

        Public Overridable ReadOnly Property DefaultNamespaceManager As XmlNamespaceManager Implements IHtmlEditorBehaviour.DefaultNamespaceManager
            Get
                Return _decoree.DefaultNamespaceManager
            End Get
        End Property

        Public Overridable Function GetHtml() As String Implements IHtmlEditorBehaviour.GetHtml
            Return _decoree.GetHtml()
        End Function

        Public Overridable Function GetStyle() As Dictionary(Of String, String) Implements IHtmlEditorBehaviour.GetStyle
            Return _decoree.GetStyle()
        End Function

        Protected Overridable Function ConvertForCalculationOfHtmlSize() As String Implements IHtmlEditorBehaviour.ConvertForCalculationOfHtmlSize
            Return String.Empty
        End Function

        Public Overridable ReadOnly Property InlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)) Implements IHtmlEditorBehaviour.InlineElements
            Get
                Return _decoree.InlineElements
            End Get
        End Property

        Public Overridable Function InlineToEdit(editor As IXHtmlEditor, inlineElement As InlineElement) As HtmlInlineHandler Implements IHtmlEditorBehaviour.InlineToEdit
            Return _decoree.InlineToEdit(editor, inlineElement)
        End Function

        Public Overridable ReadOnly Property IsToolstripVisible As Boolean Implements IHtmlEditorBehaviour.IsToolstripVisible
            Get
                Return _decoree.IsToolstripVisible
            End Get
        End Property

        Public Overridable ReadOnly Property DoHeightUpdate As Boolean Implements IHtmlEditorBehaviour.DoHeightUpdate
            Get
                Return _decoree.DoHeightUpdate
            End Get
        End Property

        Public Overridable Function RemoveDependency(nameOfResource As String) As Boolean Implements IHtmlEditorBehaviour.RemoveDependency
            Return _decoree.RemoveDependency(nameOfResource)
        End Function

        Public Overridable Sub SetHtml(html As String) Implements IHtmlEditorBehaviour.SetHtml
            _decoree.SetHtml(html)
        End Sub

        Public Property Parameters As List(Of ParameterBase) Implements IHtmlEditorBehaviour.Parameters

        Public Property InlineElementPlaceholders As Dictionary(Of String, XmlNode) Implements IHtmlEditorBehaviour.InlineElementPlaceholders



        Private _disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then
                    _decoree.Dispose()
                    _decoree = Nothing
                End If

            End If
            _disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


    End Class

End Namespace
