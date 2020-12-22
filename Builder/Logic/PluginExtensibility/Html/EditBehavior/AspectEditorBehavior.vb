
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public Class AspectEditorBehavior
        Inherits BaseHtmlEditorBehavior

        Private ReadOnly _param As Aspect
        Private _toEditor As IHtmlConverter
        Private _toParam As IHtmlConverter
        Private _inlineRetriever As IInlineRetriever

        Public Sub New(resourceEntity As ResourceEntity,
               resourceManager As ResourceManagerBase,
               contextIdentifier As Integer?,
               aspect As Aspect)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier)

            _param = aspect

            Debug.Assert(_param IsNot Nothing, "Aspect Is nothing??!!")
        End Sub

        Public Overrides ReadOnly Property IsToolstripVisible As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property DoHeightUpdate As Boolean
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

        Public Overrides Function GetHtml() As String
            If (_toEditor Is Nothing) Then _toEditor = ConstructChainToEditor()
            Dim tmp As String = _toEditor.ConvertHtml(_param.Description)
            Debug.Assert(_inlineRetriever IsNot Nothing)
            SetInlineElements(_inlineRetriever.InlineElements)

            Return MyBase.AddStylePlaceholder(tmp)
        End Function

        Public Overrides Sub SetHtml(html As String)
            If (_toParam Is Nothing) Then _toParam = ConstructChainToParameter()
            _param.Description = _toParam.ConvertHtml(html)
        End Sub

        Public Overrides Function addDependency(nameOfResource As String, isItemLayoutTemplate As Boolean) As Boolean
            If ForOldItem Then
                If isItemLayoutTemplate Then
                    Return True
                Else
                    Return MyBase.addDependency(nameOfResource, isItemLayoutTemplate)
                End If
            Else
                Return MyBase.addDependency(nameOfResource, isItemLayoutTemplate)
            End If
        End Function


        Private Function ConstructChainToEditor() As IHtmlConverter
            Dim ret As IHtmlConverter
            _inlineRetriever = New HtmlConverter_OldHtmlToInline(Me)
            ret = _inlineRetriever
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier)
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
            ret.LastConverter.NextConverter = New HtmlConverter_PartialToFull(GetStyle(), String.Empty, ContextIdentifier, DefaultNamespaceManager)
            Return ret
        End Function

        Private Function ConstructChainToParameter() As IHtmlConverter
            Dim ret As IHtmlConverter
            ret = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_OldInlineToHtml(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML()
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveFontInSpan()
            Return ret
        End Function




        Public Overrides Function GetStyleFromResource() As System.Collections.Generic.Dictionary(Of String, String)
            Dim styleSheetsToReference As New Dictionary(Of String, String)

            Dim stylesheets As String = _param.Stylesheet

            If Not String.IsNullOrEmpty(stylesheets) Then
                For Each stylesheet As String In stylesheets.Split(";"c)
                    styleSheetsToReference.Add(stylesheet, New System.Text.UTF8Encoding().GetString(GetStylesheetResourceData(stylesheet).BinData))
                Next
            End If
            Return styleSheetsToReference

        End Function

        Private Function GetStylesheetResourceData(ByVal stylesheet As String) As ResourceDataEntity
            If Not String.IsNullOrEmpty(stylesheet) Then
                Dim stylesheetResourceEntity As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(Me.ResourceEntity.BankId, stylesheet, New ResourceRequestDTO())
                Return ResourceFactory.Instance.GetResourceData(stylesheetResourceEntity)
            End If
            Return Nothing
        End Function


    End Class

End Namespace