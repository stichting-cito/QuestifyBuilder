Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Linq
Imports System.Xml
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.HtmlHelpers
Imports Cito.Tester.ContentModel
Imports Enums
Imports HtmlAgilityPack
Imports Questify.Builder.Logic.PluginExtensibility.Html.Factory
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

Namespace PluginExtensibility.Html.EditBehavior


    Public MustInherit Class BaseHtmlEditorBehavior
        Implements IHtmlEditorBehaviour

        Private _forOldItem As Boolean = False
        Private _defaultNamespaceManager As XmlNamespaceManager

        Private _resourceEntity As ResourceEntity
        Private _resourceManager As ResourceManagerBase

        Private ReadOnly _contextIdentifier As Nullable(Of Integer)
        Private _inlineElements As New Dictionary(Of String, Tuple(Of InlineElement, Boolean))
        Private _inlineElementPlaceholders As Dictionary(Of String, XmlNode)
        Private _stylesheets As Dictionary(Of String, String)
        Private _inlineTemplates As Dictionary(Of String, String)
        Private _inlineMediaTemplates As Dictionary(Of String, String)
        Private _inlineAudioTemplate As String = Nothing
        Private _inlineVideoTemplate As String = Nothing
        Private _inlineCustomInteractionTemplate As String = Nothing
        Private _popupTemplate As String = Nothing
        Private _inlineFindingOverride As String = Nothing
        Private _canSetTTSOptions As Boolean?

        Protected HeaderStyleElementCont As String
        Protected Adapter As ItemLayoutAdapter
        Protected Property InlineElementPlaceholdersDirty As Boolean


        Public Sub New(resourceEntity As ResourceEntity,
               resourceManager As ResourceManagerBase,
               contextIdentifier As Integer?,
               iltAdapter As ItemLayoutAdapter,
               stylesheets As Dictionary(Of String, String),
               headerStyleElementContent As String)
            _resourceEntity = resourceEntity
            _stylesheets = stylesheets
            HeaderStyleElementCont = headerStyleElementContent
            Adapter = iltAdapter

            If resourceEntity IsNot Nothing AndAlso resourceEntity.DependentResourceCollection.Count = 0 Then
                If TypeOf (resourceEntity) Is GenericResourceEntity AndAlso Not resourceEntity.IsNew Then
                    _resourceEntity = New GenericResourceEntity(resourceEntity.ResourceId)
                    _resourceEntity = Service.Factories.ResourceFactory.Instance.GetGenericResource(DirectCast(_resourceEntity, GenericResourceEntity))
                End If
            End If

            _resourceManager = resourceManager
            _contextIdentifier = contextIdentifier

            _defaultNamespaceManager = HtmlInlineConverter.CreateXmlNamespaceManager(New NameTable())
        End Sub

        Public Sub New(resourceEntity As ResourceEntity,
               resourceManager As ResourceManagerBase,
               contextIdentifier As Integer?)
            Me.New(resourceEntity, resourceManager, contextIdentifier, Nothing, Nothing, String.Empty)
        End Sub

        Public Sub New(resourceEntity As ResourceEntity, resourceManager As ResourceManagerBase)
            Me.New(resourceEntity, resourceManager, Nothing)
        End Sub


        Protected Property ForOldItem As Boolean
            Get
                Return _forOldItem
            End Get
            Set(value As Boolean)
                _forOldItem = value
            End Set
        End Property

        Protected ReadOnly Property ResourceEntity As ResourceEntity
            Get
                Return _resourceEntity
            End Get
        End Property

        Protected ReadOnly Property ResourceManager As ResourceManagerBase
            Get
                Return _resourceManager
            End Get
        End Property

        Public Property Parameters As List(Of ParameterBase) Implements IHtmlEditorBehaviour.Parameters

        Public Property InlineElementPlaceholders As Dictionary(Of String, XmlNode) Implements IHtmlEditorBehaviour.InlineElementPlaceholders
            Get
                Return _inlineElementPlaceholders
            End Get
            Set(value As Dictionary(Of String, XmlNode))
                _inlineElementPlaceholders = value
                InlineElementPlaceholdersDirty = True
            End Set
        End Property

        Public Sub AddInlineElementPlaceholder(inlineElementIdentifier As String, placeHolder As XmlNode) Implements IHtmlEditorBehaviour.AddInlineElementPlaceholder
            If InlineElementPlaceholders Is Nothing Then
                InlineElementPlaceholders = New Dictionary(Of String, XmlNode)
            End If
            If InlineElementPlaceholders.ContainsKey(inlineElementIdentifier) Then
                InlineElementPlaceholders(inlineElementIdentifier) = placeHolder
            Else
                InlineElementPlaceholders.Add(inlineElementIdentifier, placeHolder)
            End If
            InlineElementPlaceholdersDirty = True
        End Sub

        Public Overridable ReadOnly Property ShouldSwitchToPreviewModeOnLostFocus As Boolean Implements IHtmlEditorBehaviour.ShouldSwitchToPreviewModeOnLostFocus
            Get
                Return False
            End Get
        End Property

        Protected Overridable ReadOnly Property CanSetTTSOptions As Boolean
            Get
                If Not _canSetTTSOptions.HasValue Then
                    _canSetTTSOptions = CheckCanSetTtsOptionsFromConfig()
                End If

                Return CBool(_canSetTTSOptions)
            End Get
        End Property



        Public Overridable ReadOnly Property IsToolstripVisible As Boolean Implements IHtmlEditorBehaviour.IsToolstripVisible
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property DoHeightUpdate As Boolean Implements IHtmlEditorBehaviour.DoHeightUpdate
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanCreateReferences As Boolean Implements IHtmlEditorBehaviour.CanCreateReferences
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertAudio As Boolean Implements IHtmlEditorBehaviour.CanInsertAudio
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertControls As Boolean Implements IHtmlEditorBehaviour.CanInsertControls
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertImages As Boolean Implements IHtmlEditorBehaviour.CanInsertImages
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertMovies As Boolean Implements IHtmlEditorBehaviour.CanInsertMovies
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertFormula As Boolean Implements IHtmlEditorBehaviour.CanInsertFormula
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertCI As Boolean Implements IHtmlEditorBehaviour.CanInsertCI
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertReferences As Boolean Implements IHtmlEditorBehaviour.CanInsertReferences
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanInsertPopup As Boolean Implements IHtmlEditorBehaviour.CanInsertPopup
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanSelectTextToSpeech As Boolean Implements IHtmlEditorBehaviour.CanSelectTextToSpeech
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property CanSetTextToSpeechOptions As Boolean Implements IHtmlEditorBehaviour.CanSetTextToSpeechOptions
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property CanRemoveTTS As Boolean Implements IHtmlEditorBehaviour.CanRemoveTTS
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property StoreSizeOfHtml As Boolean Implements IHtmlEditorBehaviour.StoreSizeOfHtml
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property ConvertOldInlineToHtml As Boolean Implements IHtmlEditorBehaviour.ConvertOldInlineToHtml
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property ContextIdentifier As Integer? Implements IHtmlEditorBehaviour.ContextIdentifier
            Get
                Return _contextIdentifier
            End Get
        End Property

        Public ReadOnly Property InlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)) Implements IHtmlEditorBehaviour.InlineElements
            Get
                Return _inlineElements
            End Get
        End Property

        Protected ReadOnly Property DefaultNamespaceManager As XmlNamespaceManager Implements IHtmlEditorBehaviour.DefaultNamespaceManager
            Get
                Return _defaultNamespaceManager
            End Get
        End Property

        Public MustOverride Function GetHtml() As String Implements IHtmlEditorBehaviour.GetHtml

        Public MustOverride Sub SetHtml(html As String) Implements IHtmlEditorBehaviour.SetHtml

        Private Function InsertImage(editor As IXHtmlEditor) As HtmlInlineHandler Implements IHtmlEditorBehaviour.CreateForImage
            If _resourceEntity Is Nothing Then Return Nothing
            Return HtmlInlineHandler.ForImage(editor, _resourceEntity, _resourceManager, _forOldItem, _defaultNamespaceManager, GetInlineTemplates(), GetInlineTemplateNames())
        End Function

        Private Function CreateForVideo(editor As IXHtmlEditor) As HtmlInlineHandler Implements IHtmlEditorBehaviour.CreateForVideo
            If _resourceEntity Is Nothing Then Return Nothing
            Return HtmlInlineHandler.ForVideo(editor, _resourceEntity, _resourceManager, _forOldItem, _defaultNamespaceManager, GetInlineTemplates())
        End Function

        Private Function CreateForAudio(editor As IXHtmlEditor) As HtmlInlineHandler Implements IHtmlEditorBehaviour.CreateForAudio
            If _resourceEntity Is Nothing Then Return Nothing
            Return HtmlInlineHandler.ForAudio(editor, _resourceEntity, _resourceManager, _forOldItem, _defaultNamespaceManager, GetInlineTemplates())
        End Function

        Private Function InlineToEdit(editor As IXHtmlEditor, inlineElement As InlineElement) As HtmlInlineHandler Implements IHtmlEditorBehaviour.InlineToEdit
            If _resourceEntity Is Nothing Then Return Nothing
            Return HtmlInlineHandler.Create(editor, inlineElement.LayoutTemplateSourceName, _inlineTemplates, inlineElement.InlineFindingOverride, inlineElement, _resourceEntity, _resourceEntity.BankId, _resourceManager, _forOldItem, _defaultNamespaceManager, _stylesheets, HeaderStyleElementContent)
        End Function

        Private Function CreateHtmlReferencesHandler(editor As IXHtmlEditor) As HtmlReferencesHandler Implements IHtmlEditorBehaviour.CreateHtmlReferencesHandler
            If _resourceEntity Is Nothing Then Return Nothing
            Return New HtmlReferencesHandler(editor, _resourceManager, _defaultNamespaceManager, _contextIdentifier, CanCreateReferences)
        End Function

        Private Function CreateFormuleHandler(editor As IXHtmlEditor) As HtmlFormulaHandler Implements IHtmlEditorBehaviour.CreateFormulaHandler
            If _resourceEntity Is Nothing Then Return Nothing
            Return New HtmlFormulaHandler(editor, _resourceManager, _defaultNamespaceManager, _resourceEntity.BankId)
        End Function

        Protected Overridable Function CreateForPasting(editor As IXHtmlEditor) As HtmlHandlerBase Implements IHtmlEditorBehaviour.CreateForPasting
            If _resourceEntity Is Nothing Then Return Nothing
            Return New HtmlHandlerBase(editor, _resourceEntity.BankId, _resourceManager, GetInlineTemplates())
        End Function

        Private Function CreateInlineConverter() As HtmlInlineConverter Implements IHtmlEditorBehaviour.CreateInlineConverter
            If _resourceEntity Is Nothing Then Return Nothing
            Return New HtmlInlineConverter(_resourceManager, _defaultNamespaceManager, GetInlineTemplates(), GetInlineTemplateNames())
        End Function

        Private Function CreateResourceFactory() As ResourceFactory Implements IHtmlEditorBehaviour.CreateResourceFactory
            If _resourceEntity Is Nothing Then Return Nothing
            Return New ResourceFactory(_resourceEntity.BankId)
        End Function

        Private Function GetInlineMediaTemplatesFromResource() As Dictionary(Of String, String)
            Dim inlineMediaTemplates As New Dictionary(Of String, String)
            If ResourceEntity IsNot Nothing Then
                If Adapter Is Nothing Then
                    If GetItemLayoutAdapterForResourceEntity() Is Nothing Then
                        Return inlineMediaTemplates
                    End If
                End If
                inlineMediaTemplates = Adapter.GetInlineMediaTemplates()
            End If

            Return inlineMediaTemplates
        End Function

        Private Function GetInlineCustomInteractionTemplateFromResource() As String
            Dim result As String = String.Empty
            If ResourceEntity IsNot Nothing Then
                If Adapter Is Nothing Then
                    If GetItemLayoutAdapterForResourceEntity() Is Nothing Then
                        Return result
                    End If
                End If
                result = Adapter.GetInlineCustomInteractionTemplate()
            End If

            Return result
        End Function

        Private Function GetPopupTemplateFromResource() As String
            Dim result As String = String.Empty
            If ResourceEntity IsNot Nothing Then
                If Adapter Is Nothing Then
                    If GetItemLayoutAdapterForResourceEntity() Is Nothing Then
                        Return result
                    End If
                End If
                result = Adapter.GetPopupTemplate()
            End If

            Return result
        End Function

        Private Function GetInlineFindingOverrideFromResource() As String
            Dim result As String = String.Empty
            If ResourceEntity IsNot Nothing Then
                If Adapter Is Nothing Then
                    If GetItemLayoutAdapterForResourceEntity() Is Nothing Then
                        Return result
                    End If
                End If
                result = Adapter.GetInlineFindingOverride()
            End If

            Return result
        End Function

        Protected Function GetItemLayoutAdapterForResourceEntity() As ItemLayoutAdapter
            Dim dependentResources = Service.Factories.ResourceFactory.Instance.GetItemLayoutTemplatesFromListOfResourceIds(ResourceEntity.DependentResourceCollection.Where(Function(d) d IsNot Nothing AndAlso (TypeOf d.DependentResource Is ItemLayoutTemplateResourceEntity OrElse d.DependentResource Is Nothing)).Select(Function(d) d.DependentResourceId), False)
            If dependentResources IsNot Nothing AndAlso dependentResources.Any() Then
                For Each resourceEntity As ResourceEntity In dependentResources
                    Dim itmLayT = TryCast(resourceEntity, ItemLayoutTemplateResourceEntity)
                    If itmLayT IsNot Nothing Then
                        Dim currentType As ItemTypeEnum = Nothing
                        If Not String.IsNullOrEmpty(itmLayT.ItemType) Then currentType = DirectCast([Enum].Parse(GetType(ItemTypeEnum), itmLayT.ItemType, True), ItemTypeEnum)
                        If currentType = ItemTypeEnum.Inline Then
                            Continue For
                        End If

                        CreateAdapterIfNeeded(Adapter, itmLayT.Name)

                        If Adapter IsNot Nothing AndAlso
                                     Adapter.Template IsNot Nothing AndAlso
                                     Adapter.Template.DesignerSettings IsNot Nothing Then
                            Return Adapter
                        End If
                    End If
                Next
            End If
            Return Nothing
        End Function

        Protected Sub CreateAdapterIfNeeded(adapter As ItemLayoutAdapter, itemLayoutTemplateName As String)
            If adapter Is Nothing Then
                Me.Adapter = New ItemLayoutAdapter(itemLayoutTemplateName, Nothing, AddressOf GenericHandler_ResourceNeeded)
            End If
        End Sub

        Private Sub GenericHandler_ResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
            Dim resource As BinaryResource = Nothing
            Dim request = New ResourceRequestDTO()
            If e.TypedResourceType IsNot Nothing Then
                resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
            Else
                resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
            End If
            e.BinaryResource = resource
        End Sub

        Public Function GetStyle() As Dictionary(Of String, String) Implements IHtmlEditorBehaviour.GetStyle
            If (_stylesheets Is Nothing) Then _stylesheets = GetStyleFromResource()
            Return _stylesheets
        End Function

        Protected Overridable Function ConvertForCalculationOfHtmlSize() As String Implements IHtmlEditorBehaviour.ConvertForCalculationOfHtmlSize
            Return String.Empty
        End Function

        Protected Function GetInlineTemplates() As Dictionary(Of String, String)
            If (_inlineTemplates Is Nothing) Then
                _inlineTemplates = GetInlineMediaTemplates()
                If Not String.IsNullOrEmpty(GetPopupTemplate()) Then
                    _inlineTemplates.Add("popup", _popupTemplate)
                End If
            End If
            Return _inlineTemplates
        End Function

        Protected Function GetInlineMediaTemplates() As Dictionary(Of String, String)
            If (_inlineMediaTemplates Is Nothing) Then _inlineMediaTemplates = GetInlineMediaTemplatesFromResource()
            Return _inlineMediaTemplates
        End Function

        Protected Function GetInlineAudioTemplate() As String
            If (_inlineAudioTemplate Is Nothing) Then
                _inlineAudioTemplate = If(GetInlineMediaTemplates().Any(Function(t) t.Key.Equals("audio", StringComparison.InvariantCultureIgnoreCase)), GetInlineMediaTemplates().First(Function(t) t.Key.Equals("audio", StringComparison.InvariantCultureIgnoreCase)).Value, String.Empty)
            End If
            Return _inlineAudioTemplate
        End Function

        Protected Function GetInlineVideoTemplate() As String
            If (_inlineVideoTemplate Is Nothing) Then
                _inlineVideoTemplate = If(GetInlineMediaTemplates().Any(Function(t) t.Key.Equals("video", StringComparison.InvariantCultureIgnoreCase)), GetInlineMediaTemplates().First(Function(t) t.Key.Equals("video", StringComparison.InvariantCultureIgnoreCase)).Value, String.Empty)
            End If
            Return _inlineVideoTemplate
        End Function

        Protected Function GetInlineCustomInteractionTemplate() As String
            If (_inlineCustomInteractionTemplate Is Nothing) Then _inlineCustomInteractionTemplate = GetInlineCustomInteractionTemplateFromResource()
            Return _inlineCustomInteractionTemplate
        End Function

        Protected Function GetPopupTemplate() As String
            If (_popupTemplate Is Nothing) Then _popupTemplate = GetPopupTemplateFromResource()
            Return _popupTemplate
        End Function

        Protected Function GetInlineFindingOverride() As String
            If (_inlineFindingOverride Is Nothing) Then _inlineFindingOverride = GetInlineFindingOverrideFromResource()
            Return _inlineFindingOverride
        End Function

        Public Function addDependency(inlineElement As InlineElement) As Boolean Implements IHtmlEditorBehaviour.AddDependency
            If InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(inlineElement.LayoutTemplateSourceName) Then
                Return True
            Else
                Return addDependency(inlineElement.LayoutTemplateSourceName, True)
            End If
        End Function

        Public Overridable Function addDependency(nameOfResource As String, isItemLayoutTemplate As Boolean) As Boolean Implements IHtmlEditorBehaviour.AddDependency
            If _resourceEntity Is Nothing Then Return Nothing
            If Not String.IsNullOrEmpty(nameOfResource) Then
                Dim referencedResource As ResourceEntity = Service.Factories.ResourceFactory.Instance.GetResourceByNameWithOption(ResourceEntity.BankId, nameOfResource, New ResourceRequestDTO())
                Debug.Assert(referencedResource IsNot Nothing, "Trying to add nothing as dependency??!! Is this correct?")

                If Not ResourceEntity.ContainsDependentResource(referencedResource) Then
                    Dim tmpDependency = ResourceEntity.DependentResourceCollection.AddNew()
                    With tmpDependency
                        .ResourceId = ResourceEntity.ResourceId
                        .DependentResourceId = referencedResource.ResourceId
                    End With

                    Return True
                End If
            End If
            Return False
        End Function

        Protected Function RemoveDependency(nameOfResource As String) As Boolean Implements IHtmlEditorBehaviour.RemoveDependency
            If _resourceEntity Is Nothing Then Return Nothing
            If Not String.IsNullOrEmpty(nameOfResource) Then
                Dim referencedResource As DependentResourceEntity = ResourceEntity.GetDependentResourceByName(nameOfResource)
                If referencedResource IsNot Nothing Then ResourceEntity.DependentResourceCollection.Remove(referencedResource)
                Return True
            End If

            Return False
        End Function



        Protected Sub SetInlineElements(newInlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)))
            _inlineElements = newInlineElements
        End Sub

        Public Overridable Function GetStyleFromResource() As Dictionary(Of String, String)
            If TypeOf ResourceEntity Is ItemResourceEntity Then
                Return DirectCast(ResourceEntity, ItemResourceEntity).GetStylesFromDependentItemLayoutTemplate(HeaderStyleElementCont, ContextIdentifier)
            End If
            Return New Dictionary(Of String, String)
        End Function

        Protected Overridable Function GetStylesheetResources() As IEnumerable(Of GenericResourceEntity)
            If TypeOf ResourceEntity Is ItemResourceEntity Then
                Return DirectCast(ResourceEntity, ItemResourceEntity).GetStylesheetResources()
            End If
            Return Nothing
        End Function

        Protected Function AddStylePlaceholder(html As String) As String
            Dim htmlDoc As New HtmlDocument()
            htmlDoc.OptionWriteEmptyNodes = True
            htmlDoc.LoadHtml(html)

            Dim bodyNode As HtmlNode = htmlDoc.DocumentNode.SelectSingleNode("//*/body")

            If Not bodyNode.Attributes.Contains("style") Then
                bodyNode.Attributes.Append("style")
            End If

            Dim styleAttributeValue As String = bodyNode.GetAttributeValue("style", String.Empty)
            Dim newStyle As String = String.Concat("fontFamilyPlaceholderKey:fontFamilyPlaceholderValue;", styleAttributeValue)
            bodyNode.SetAttributeValue("style", newStyle)

            Return htmlDoc.DocumentNode.OuterHtml
        End Function

        Protected Overridable Function GetInlineTemplateNames() As IHtmlInlineTemplateNames
            Return Nothing
        End Function


        ReadOnly Property HeaderStyleElementContent As String
            Get
                Return HeaderStyleElementCont
            End Get
        End Property


        Private _disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then
                    _defaultNamespaceManager = Nothing
                    _resourceEntity = Nothing
                    _resourceManager = Nothing
                    If _inlineElements IsNot Nothing Then _inlineElements.Clear()
                    _inlineElements = Nothing
                    If _stylesheets IsNot Nothing Then _stylesheets.Clear()
                    _stylesheets = Nothing
                    If _inlineTemplates IsNot Nothing Then _inlineTemplates.Clear()
                    _inlineTemplates = Nothing
                    If _inlineMediaTemplates IsNot Nothing Then _inlineMediaTemplates.Clear()
                    _inlineMediaTemplates = Nothing
                    If Adapter IsNot Nothing AndAlso
                        Adapter.Template IsNot Nothing AndAlso
                        Adapter.Template.Targets IsNot Nothing Then
                        Adapter.Template.Targets.Clear()
                    End If
                    Adapter = Nothing
                    HeaderStyleElementCont = Nothing
                    If Parameters IsNot Nothing Then Parameters.Clear()
                    Parameters = Nothing
                End If
            End If
            _disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


        Protected Function CheckCanSetTtsOptionsFromConfig() As Boolean
            Dim ttsSettings = TryCast(ConfigurationManager.GetSection("ttsSettings"), NameValueCollection)
            If ttsSettings IsNot Nothing Then
                Dim canSetTttsOptions = ttsSettings("CanSetTTSOptions")
                If Not String.IsNullOrEmpty(canSetTttsOptions) Then
                    Return CBool(canSetTttsOptions)
                End If
            End If
            Return False
        End Function

    End Class

End Namespace