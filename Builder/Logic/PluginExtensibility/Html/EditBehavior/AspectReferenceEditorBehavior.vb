Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public Class AspectReferenceEditorBehavior
        Inherits BaseHtmlEditorBehavior

        Private ReadOnly param As AspectReference
        Private _toEditor As IHtmlConverter
        Private _toParam As IHtmlConverter
        Private _inlineRetriever As IInlineRetriever
        Private _aspectResourceEntity As AspectResourceEntity

        <Obsolete("Initialize with AspectResourceEntity and not with another type of ResourceEntity")>
        Public Sub New(resourceEntity As ResourceEntity,
               resourceManager As ResourceManagerBase,
               contextIdentifier As Integer?,
               aspect As AspectReference)

            MyBase.New(resourceEntity, resourceManager, contextIdentifier)
            param = aspect

            Debug.Assert(param IsNot Nothing, "AspectReference Is nothing??!!")
        End Sub

        Public Sub New(itemResourceEntity As ResourceEntity,
               aspectResourceEntity As AspectResourceEntity,
               resourceManager As ResourceManagerBase,
               contextIdentifier As Integer?,
               aspect As AspectReference)

            MyBase.New(itemResourceEntity, resourceManager, contextIdentifier)
            param = aspect
            _aspectResourceEntity = aspectResourceEntity
            Debug.Assert(param IsNot Nothing, "AspectReference Is nothing??!!")
        End Sub



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

        Public Overrides ReadOnly Property CanInsertControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanSelectTextToSpeech As Boolean
            Get
                Return False
            End Get
        End Property


        Public Overrides ReadOnly Property IsToolstripVisible As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property DoHeightUpdate As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function GetHtml() As String
            If (_toEditor Is Nothing) Then InitConverter()
            Dim tmp As String = _toEditor.ConvertHtml(param.Description)
            Debug.Assert(_inlineRetriever IsNot Nothing)
            MyBase.SetInlineElements(_inlineRetriever.InlineElements)

            Return MyBase.AddStylePlaceholder(tmp)
        End Function

        Protected Overrides Function GetStylesheetResources() As IEnumerable(Of GenericResourceEntity)

            If _aspectResourceEntity Is Nothing Then
                Return MyBase.GetStylesheetResources()
            End If

            Dim sortedDictionary As New SortedDictionary(Of String, GenericResourceEntity)
            For Each dependedResource As DependentResourceEntity In _aspectResourceEntity.DependentResourceCollection
                If TypeOf dependedResource.DependentResource Is GenericResourceEntity AndAlso
                                      DirectCast(dependedResource.DependentResource, GenericResourceEntity).MediaType = "text/css" Then

                    Dim dependentStylesheetResource As GenericResourceEntity = DirectCast(dependedResource.DependentResource, GenericResourceEntity)
                    If Not sortedDictionary.ContainsKey(dependentStylesheetResource.Name) Then
                        sortedDictionary.Add(dependentStylesheetResource.Name, dependentStylesheetResource)
                    End If
                End If
            Next
            If (sortedDictionary.Count > 0) Then
                Return sortedDictionary.Values
            End If
            Return MyBase.GetStylesheetResources()
        End Function

        Public Overrides Sub SetHtml(html As String)
            If (_toParam Is Nothing) Then InitConverter()
            param.Description = _toParam.ConvertHtml(html)
        End Sub

        Private Sub InitConverter()
            _toEditor = ConstructChain_FromParam2Editor()
            _toEditor.LastConverter.NextConverter = New HtmlConverter_PartialToFull(GetStyle(), HeaderStyleElementCont, ContextIdentifier, DefaultNamespaceManager, Me)
            _toParam = ConstructChain_FromEditor2Param()
        End Sub


        Private Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_C1RefToCitoRef()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML()
            ret.LastConverter.NextConverter = New HtmlConverter_Inline2Cito(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_OldInlineToHtml(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveFontInSpan()
            Return ret
        End Function

        Private Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_HandleNULLString()
            ret.LastConverter.NextConverter = New HtmlConverter_CitoRefToC1Ref()
            _inlineRetriever = New HtmlConverter_Cito2Inline(Me)
            ret.LastConverter.NextConverter = _inlineRetriever
            _inlineRetriever = New HtmlConverter_OldHtmlToInline(Me, _inlineRetriever.InlineElements)
            ret.LastConverter.NextConverter = _inlineRetriever
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier)
            Return ret
        End Function


    End Class

End Namespace
