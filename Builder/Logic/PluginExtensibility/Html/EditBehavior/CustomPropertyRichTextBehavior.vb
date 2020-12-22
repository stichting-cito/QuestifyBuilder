Imports Cito.Tester.Common
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior
    Public Class CustomPropertyRichTextBehavior
        Inherits BaseHtmlEditorBehavior

        Private _param As RichTextValueCustomBankPropertyValueEntity
        Private _toEditor As IHtmlConverter
        Private _toParam As IHtmlConverter

        Public Sub New(resourceEntity As ResourceEntity,
               resourceManager As ResourceManagerBase,
               contextIdentifier As Integer?,
               richTextValue As RichTextValueCustomBankPropertyValueEntity)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier)

            _param = richTextValue
        End Sub


        Public Overrides ReadOnly Property DoHeightUpdate As Boolean
            Get
                Return False
            End Get
        End Property

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

        Public Overrides ReadOnly Property CanCreateReferences As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertReferences As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanSelectTextToSpeech As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertAudio As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertImages As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertMovies As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertFormula As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertPopup As Boolean
            Get
                Return False
            End Get
        End Property


        Public Overrides Function GetHtml() As String
            If (_toEditor Is Nothing) Then InitConverter()
            Dim tmp = _toEditor.ConvertHtml(_param.Value)
            Return tmp
        End Function

        Public Overrides Sub SetHtml(html As String)
            If (_toParam Is Nothing) Then InitConverter()
            _param.Value = _toParam.ConvertHtml(html)
        End Sub

        Private Sub InitConverter()
            _toEditor = ConstructChain_FromParam2Editor()
            _toEditor.LastConverter.NextConverter = New HtmlConverter_PartialToFull(GetStyle(), HeaderStyleElementCont, ContextIdentifier, DefaultNamespaceManager, Me)
            _toParam = ConstructChain_FromEditor2Param()
        End Sub


        Private Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveFontInSpan()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveHyperlinks()
            Return ret
        End Function

        Private Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_HandleNULLString()
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier)
            Return ret
        End Function

    End Class

End Namespace
