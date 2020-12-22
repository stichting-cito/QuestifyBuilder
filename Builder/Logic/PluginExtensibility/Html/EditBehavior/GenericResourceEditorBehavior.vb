
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Text
Imports System.Text.RegularExpressions
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Logic.Service.Factories

Namespace PluginExtensibility.Html.EditBehavior

    Public Class GenericResourceEditorBehaviour
        Inherits BaseHtmlEditorBehavior


        Private Const TTS_STYLESHEET_NAME As String = "Edituserstyle.css"
        Private ReadOnly param As GenericResourceEntity
        Private _toParam As IHtmlConverter
        Private _toEditor As IHtmlConverter
        Private _inlineRetriever As IInlineRetriever
        Private _hideToolstrip As Boolean

        Public Sub New(resourceEntity As ResourceEntity,
               resourceManager As ResourceManagerBase,
               contextIdentifier As Integer?, hideToolstrip As Boolean)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier)

            param = DirectCast(resourceEntity, GenericResourceEntity)
            Debug.Assert(param IsNot Nothing, "ResourceEntity Is nothing??!!")

            MyBase.ForOldItem = True

            _hideToolstrip = hideToolstrip
        End Sub

        Public Overrides ReadOnly Property IsToolstripVisible As Boolean
            Get
                Return Not _hideToolstrip
            End Get
        End Property

        Public Overrides ReadOnly Property DoHeightUpdate As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property ConvertOldInlineToHtml As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Function GetHtml() As String
            If (_toEditor Is Nothing) Then InitConverter()
            Dim tmp As String = Encoding.UTF8.GetString(param.ResourceData.BinData())

            tmp = Regex.Replace(tmp, "^((<\?xml.+?>)|(<!DOCTYPE.+?>))", "")
            tmp = _toEditor.ConvertHtml(tmp)

            MyBase.SetInlineElements(_inlineRetriever.InlineElements)

            Return MyBase.AddStylePlaceholder(tmp)
        End Function

        Public Overrides Sub SetHtml(html As String)
            If (_toParam Is Nothing) Then InitConverter()
            param.ResourceData.BinData = (New UTF8Encoding()).GetBytes(_toParam.ConvertHtml(html))
        End Sub

        Private Sub InitConverter()
            _toEditor = ConstructChain_FromParam2Editor()
            _toEditor.LastConverter.NextConverter = New HtmlConverter_PartialToFull(GetStyle(), HeaderStyleElementContent, ContextIdentifier, DefaultNamespaceManager)
            _toParam = ConstructChain_FromEditor2Param()
            _inlineRetriever = GetInlineRetriever()
        End Sub

        Public Overrides Function addDependency(nameOfResource As String, isItemLayoutTemplate As Boolean) As Boolean
            If isItemLayoutTemplate Then
                Return True
            Else
                Return MyBase.addDependency(nameOfResource, isItemLayoutTemplate)
            End If
        End Function


        Public Overrides ReadOnly Property CanInsertControls As Boolean
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

        Public Overrides ReadOnly Property CanInsertReferences As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property CanSetTextToSpeechOptions As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property CanRemoveTTS As Boolean
            Get
                Return True
            End Get
        End Property



        Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_C1RefToCitoRef()
            ret.LastConverter.NextConverter = New HtmlConverter_OldInlineToHtml(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber()
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML()
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveHyperlinks()
            ret.LastConverter.NextConverter = New HtmlConverter_TextToSpeechToHtml()
            Return ret
        End Function

        Function GetInlineRetriever() As IInlineRetriever
            Debug.Assert(_inlineRetriever IsNot Nothing)
            Return _inlineRetriever
        End Function

        Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_CitoRefToC1Ref()
            ret.LastConverter.NextConverter = New HtmlConverter_AssignDivId()
            ret.LastConverter.NextConverter = New HtmlConverter_RepairElementReference(DefaultNamespaceManager)
            Dim inline As New HtmlConverter_OldHtmlToInline(Me)
            _inlineRetriever = DirectCast(inline, IInlineRetriever)
            ret.LastConverter.NextConverter = inline
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier)
            ret.LastConverter.NextConverter = New HtmlConverter_HtmlToTextToSpeech()
            Return ret
        End Function



        Public Overrides Function GetStyleFromResource() As Dictionary(Of String, String)
            Dim styleSheetsToReference As New Dictionary(Of String, String)
            Dim sbHeaderStyleElementContent As New StringBuilder()

            For Each dependentResource As DependentResourceEntity In ResourceEntity.DependentResourceCollection

                If dependentResource.DependentResource.GetType() Is GetType(GenericResourceEntity) _
                   AndAlso DirectCast(dependentResource.DependentResource, GenericResourceEntity).MediaType = "text/css" Then

                    Dim dependentStylesheetResource As GenericResourceEntity = DirectCast(dependentResource.DependentResource, GenericResourceEntity)
                    Dim resourceDataEntity As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(dependentStylesheetResource)

                    styleSheetsToReference.Add(dependentResource.DependentResource.Name, New System.Text.UTF8Encoding().GetString(resourceDataEntity.BinData))

                    Dim stylesheetEditCounterpart As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(ResourceEntity.BankId, $"Edit{dependentStylesheetResource.Name}", New ResourceRequestDTO())

                    If TypeOf stylesheetEditCounterpart Is GenericResourceEntity AndAlso
                        Not String.Equals(stylesheetEditCounterpart.Name, TTS_STYLESHEET_NAME, StringComparison.InvariantCultureIgnoreCase) AndAlso
                        DirectCast(stylesheetEditCounterpart, GenericResourceEntity).MediaType = "text/css" Then

                        resourceDataEntity = ResourceFactory.Instance.GetResourceData(DirectCast(stylesheetEditCounterpart, GenericResourceEntity))

                        sbHeaderStyleElementContent.Append(New System.Text.UTF8Encoding().GetString(resourceDataEntity.BinData))
                    End If

                End If
            Next

            HeaderStyleElementCont = GetTTSStyleSheet() + sbHeaderStyleElementContent.ToString()

            Return styleSheetsToReference
        End Function

        Private Function GetTTSStyleSheet() As String
            If String.IsNullOrEmpty(HeaderStyleElementCont) Then
                Dim ttsStyleSheet = ResourceFactory.Instance.GetResourceByNameWithOption(ResourceEntity.BankId, TTS_STYLESHEET_NAME, New ResourceRequestDTO())

                If TypeOf ttsStyleSheet Is GenericResourceEntity AndAlso DirectCast(ttsStyleSheet, GenericResourceEntity).MediaType = "text/css" Then
                    Dim resourceDataEntity = ResourceFactory.Instance.GetResourceData(DirectCast(ttsStyleSheet, GenericResourceEntity))

                    If resourceDataEntity IsNot Nothing Then
                        Return New UTF8Encoding().GetString(resourceDataEntity.BinData)
                    End If

                End If
            End If

            Return String.Empty
        End Function

    End Class

End Namespace