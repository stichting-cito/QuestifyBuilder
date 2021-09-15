
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
        Private _toParam As IHtmlConverter 'How to convert the html to value to be stored
        Private _toEditor As IHtmlConverter 'how to convert the stored value to html
        Private _inlineRetriever As IInlineRetriever
        Private _hideToolstrip As Boolean

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GenericResourceEditorBehaviour" /> class.
        ''' </summary>
        ''' <param name="resourceEntity">The resource entity.</param>
        ''' <param name="resourceManager">The resource manager.</param>
        ''' <param name="contextIdentifier">The context identifier.</param>
        Public Sub New(resourceEntity As ResourceEntity,
                       resourceManager As ResourceManagerBase,
                       contextIdentifier As Integer?, hideToolstrip As Boolean)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier)

            param = DirectCast(resourceEntity, GenericResourceEntity)
            Debug.Assert(param IsNot Nothing, "ResourceEntity Is nothing??!!")

            MyBase.ForOldItem = True 'Assume that it is plain html instead of an InlineElement object

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

            ' Strip documenttype declarations:
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
            'a dependent resource won't be added because Questify Player before version 2.1 can't handle it.
            If isItemLayoutTemplate Then
                Return True
            Else
                Return MyBase.addDependency(nameOfResource, isItemLayoutTemplate)
            End If
        End Function

#Region "Business Logic"

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

#End Region

#Region "Chain"

        Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_C1RefToCitoRef() 'Translates "fixed" referential tags to cito format (brontekst)
            ret.LastConverter.NextConverter = New HtmlConverter_OldInlineToHtml(Me, DefaultNamespaceManager)
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber() 'Removes the contextId
            ret.LastConverter.NextConverter = New HtmlConverter_MathImageToMathML() 'Only save MathML, not img tag, in html
            ret.LastConverter.NextConverter = New HtmlConverter_FullToPartial() 'Retrieve only the content of the body, do not store full html
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveHyperlinks()
            ret.LastConverter.NextConverter = New HtmlConverter_TextToSpeechToHtml() ' Removes textual contents from TTS pause-tags
            Return ret
        End Function

        Function GetInlineRetriever() As IInlineRetriever
            Debug.Assert(_inlineRetriever IsNot Nothing)
            Return _inlineRetriever
        End Function

        Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim ret = New HtmlConverter_CitoRefToC1Ref() 'Converts sourcetext reference tags to C1 editor specific tags.          
            ret.LastConverter.NextConverter = New HtmlConverter_AssignDivId() 'C1 editor translates empty DIVS this is a fix to preserve lines.
            ret.LastConverter.NextConverter = New HtmlConverter_RepairElementReference(DefaultNamespaceManager) 'Swaps <u> inside <span> to surround <span>
            Dim inline As New HtmlConverter_OldHtmlToInline(Me)
            _inlineRetriever = DirectCast(inline, IInlineRetriever)
            ret.LastConverter.NextConverter = inline
            ret.LastConverter.NextConverter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin) 'Converts the MathML to a math img tag
            ret.LastConverter.NextConverter = New HtmlConverter_AddContextNumber(Me.ContextIdentifier) 'Adds the contextIdentifier to url's
            ret.LastConverter.NextConverter = New HtmlConverter_HtmlToTextToSpeech() 'Adds the contextual contents for TTS pause-tags
            Return ret
        End Function

#End Region

#Region "StyleSheet"

        ''' <summary>
        ''' Gets the style sheets from dependencies.
        ''' </summary>
        Public Overrides Function GetStyleFromResource() As Dictionary(Of String, String)
            Dim styleSheetsToReference As New Dictionary(Of String, String) 'Name - Content of the stylesheet
            Dim sbHeaderStyleElementContent As New StringBuilder()

            For Each dependentResource As DependentResourceEntity In ResourceEntity.DependentResourceCollection

                If dependentResource.DependentResource.GetType() Is GetType(GenericResourceEntity) _
                   AndAlso DirectCast(dependentResource.DependentResource, GenericResourceEntity).MediaType = "text/css" Then

                    Dim dependentStylesheetResource As GenericResourceEntity = DirectCast(dependentResource.DependentResource, GenericResourceEntity)
                    Dim resourceDataEntity As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(dependentStylesheetResource)

                    styleSheetsToReference.Add(dependentResource.DependentResource.Name, New System.Text.UTF8Encoding().GetString(resourceDataEntity.BinData))

                    ' Now check if a stylesheet exists with the name "Edit" + Name and if so load it and append its content to the headerStyleElementContent var. "Edit" stylesheets are intended to set or extend styles that are specific to "Edit" mode.
                    ' This feature is introduced because the userstyle.css UserSRGroep needs a visual effect during edit that is not allowed during presentation.
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

#End Region
    End Class

End Namespace