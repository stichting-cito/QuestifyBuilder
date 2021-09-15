
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

Namespace PluginExtensibility.Html.EditBehavior

    Public MustInherit Class XhtmlParameterBehaviorBase
        Inherits BaseHtmlEditorBehavior

        Private ReadOnly _param As XHtmlParameter
        Private _toParam As IHtmlConverter 'How to convert the html to value to be stored
        Private _toEditor As IHtmlConverter 'how to convert the stored value to html
        Private _inlineRetriever As IInlineRetriever
        Private _prevPrmValue As String
        Private _prevHtml As String
        Private _canInsertAudio As Boolean?
        Private _canInsertMovies As Boolean?

        Public Sub New(resourceEntity As ResourceEntity,
                        resourceManager As ResourceManagerBase,
                        contextIdentifier As Integer?,
                        iltAdapter As ItemLayoutAdapter,
                        stylesheets As Dictionary(Of String, String),
                        headerStyleElementContent As String,
                        param As XHtmlParameter)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier, iltAdapter, stylesheets, headerStyleElementContent)

            _param = param
            Debug.Assert(param IsNot Nothing, "XHtmlParameter Is nothing??!!")
        End Sub

        Public Sub New(resourceEntity As ResourceEntity,
                       resourceManager As ResourceManagerBase,
                       contextIdentifier As Integer?,
                       param As XHtmlParameter)
            Me.New(resourceEntity, resourceManager, contextIdentifier, Nothing, Nothing, String.Empty, param)
        End Sub

        Protected ReadOnly Property Param As XHtmlParameter
            Get
                Return _param
            End Get
        End Property

        Public Overrides ReadOnly Property CanCreateReferences As Boolean
            Get
                Return False 'We can not create references (but can insert!)
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertAudio As Boolean
            Get
                If _canInsertAudio Is Nothing Then
                    _canInsertAudio = MyBase.CanInsertAudio
                    If _canInsertAudio Then
                        _canInsertAudio = CheckCanInsertAudio()
                    End If
                End If
                Return CBool(_canInsertAudio)
            End Get
        End Property

        Public Overrides ReadOnly Property CanInsertMovies As Boolean
            Get
                If _canInsertMovies Is Nothing Then
                    _canInsertMovies = MyBase.CanInsertMovies
                    If _canInsertMovies Then
                        _canInsertMovies = CheckCanInsertMovies()
                    End If
                End If
                Return CBool(_canInsertMovies)
            End Get
        End Property

        Public Overrides ReadOnly Property ShouldSwitchToPreviewModeOnLostFocus As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Function GetHtml() As String
            If (_toEditor Is Nothing) Then InitConverter()
            If Not Param.Value.Equals(_prevPrmValue, StringComparison.InvariantCultureIgnoreCase) Then
                Dim converted = _toEditor.ConvertHtml(Param.Value)
                MyBase.SetInlineElements(_inlineRetriever.InlineElements)
                _prevPrmValue = Param.Value
                _prevHtml = converted
                Return converted
            End If
            Return _prevHtml
        End Function

        Public Overrides Sub SetHtml(html As String)
            If (_toParam Is Nothing) Then InitConverter()
            If Not html.Equals(_prevHtml, StringComparison.InvariantCultureIgnoreCase) OrElse InlineElementPlaceholdersDirty Then
                _param.Value = _toParam.ConvertHtml(html)
                _prevHtml = html
                InlineElementPlaceholdersDirty = False
            Else
                _param.Value = _prevPrmValue
            End If
        End Sub

        Private Sub InitConverter()
            _toEditor = ConstructChain_FromParam2Editor()
            _toEditor.LastConverter.NextConverter = New HtmlConverter_PartialToFull(GetStyle(), HeaderStyleElementCont, ContextIdentifier, DefaultNamespaceManager, Me)
            _toParam = ConstructChain_FromEditor2Param()
            _inlineRetriever = GetInlineRetriever()
        End Sub

        Private Function CheckCanInsertAudio() As Boolean
            Return Not String.IsNullOrEmpty(GetInlineAudioTemplate())
        End Function

        Private Function CheckCanInsertMovies() As Boolean
            Return Not String.IsNullOrEmpty(GetInlineVideoTemplate())
        End Function

        Protected MustOverride Function ConstructChain_FromParam2Editor() As IHtmlConverter
        Protected MustOverride Function ConstructChain_FromEditor2Param() As IHtmlConverter
        Protected MustOverride Function GetInlineRetriever() As IInlineRetriever

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                DisposeConverterChainRecursively(_inlineRetriever)
                _inlineRetriever.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub DisposeConverterChainRecursively(converter As IHtmlConverter)
            If converter.NextConverter IsNot Nothing Then
                DisposeConverterChainRecursively(converter.NextConverter)
            End If

            converter.Dispose()
        End Sub

    End Class
End Namespace
