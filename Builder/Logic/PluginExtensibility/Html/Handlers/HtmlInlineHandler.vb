Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports HtmlAgilityPack
Imports Questify.Builder.Logic.HtmlHelpers
Imports Questify.Builder.Logic.ItemProcessing
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.Handlers

    Public Class HtmlInlineHandler
        Inherits HtmlHandlerBase


        Public Shared Function ForImage(editor As IXHtmlEditor,
                                        resourceEntity As ResourceEntity,
                                        resourceManager As ResourceManagerBase,
                                        hasLoadedOldItemLayoutTemplate As Boolean,
                                        defaultNamespaceManager As XmlNamespaceManager,
                                        inlineMediaTemplates As Dictionary(Of String, String),
                                        Optional htmlInlineTemplateName As IHtmlInlineTemplateNames = Nothing) As HtmlInlineHandler

            Dim template = If(htmlInlineTemplateName Is Nothing, New DefaultHtmlInlineTemplateNames, htmlInlineTemplateName)

            Return Create(editor, InlineMediaTemplateHelper.GetInlineMediaTemplate(inlineMediaTemplates, "image", template), inlineMediaTemplates, Nothing, Nothing,
                          resourceEntity,
                          resourceEntity.BankId,
                          resourceManager,
                          hasLoadedOldItemLayoutTemplate,
                          defaultNamespaceManager)
        End Function

        Public Shared Function ForAudio(editor As IXHtmlEditor,
                                        resourceEntity As ResourceEntity,
                                        resourceManager As ResourceManagerBase,
                                        hasLoadedOldItemLayoutTemplate As Boolean,
                                        defaultNamespaceManager As XmlNamespaceManager,
                                        inlineMediaTemplates As Dictionary(Of String, String),
                                        Optional htmlInlineTemplateName As IHtmlInlineTemplateNames = Nothing) As HtmlInlineHandler

            Dim template = If(htmlInlineTemplateName Is Nothing, New DefaultHtmlInlineTemplateNames, htmlInlineTemplateName)

            Return Create(editor, InlineMediaTemplateHelper.GetInlineMediaTemplate(inlineMediaTemplates, "audio", template), inlineMediaTemplates, Nothing, Nothing,
                          resourceEntity,
                          resourceEntity.BankId,
                          resourceManager,
                          hasLoadedOldItemLayoutTemplate,
                          defaultNamespaceManager)
        End Function

        Public Shared Function ForVideo(editor As IXHtmlEditor,
                                        resourceEntity As ResourceEntity,
                                        resourceManager As ResourceManagerBase,
                                        hasLoadedOldItemLayoutTemplate As Boolean,
                                        defaultNamespaceManager As XmlNamespaceManager,
                                        inlineMediaTemplates As Dictionary(Of String, String),
                                        Optional htmlInlineTemplateName As IHtmlInlineTemplateNames = Nothing) As HtmlInlineHandler

            Dim template = If(htmlInlineTemplateName Is Nothing, New DefaultHtmlInlineTemplateNames, htmlInlineTemplateName)

            Return Create(editor, InlineMediaTemplateHelper.GetInlineMediaTemplate(inlineMediaTemplates, "video", template), inlineMediaTemplates, Nothing, Nothing,
                          resourceEntity,
                          resourceEntity.BankId,
                          resourceManager,
                          hasLoadedOldItemLayoutTemplate,
                          defaultNamespaceManager)
        End Function

        Public Shared Function Create(editor As IXHtmlEditor,
                                      inlineName As String,
                                      inlineTemplates As Dictionary(Of String, String),
                                      inlineFindingOverride As String,
                                      inlineElement As InlineElement,
                                      resourceEntity As ResourceEntity,
                                      bankId As Integer,
                                      resourceManager As ResourceManagerBase,
                                      hasLoadedOldItemLayoutTemplate As Boolean,
                                      defaultNamespaceManager As XmlNamespaceManager,
                                      stylesheetsForXhtmlEditors As Dictionary(Of String, String),
                                      headerStyleElementContentForXhtmlEditors As String) As HtmlInlineHandler

            Return New HtmlInlineHandler(editor, inlineName, inlineTemplates, inlineFindingOverride, inlineElement, resourceEntity, bankId, resourceManager, hasLoadedOldItemLayoutTemplate, stylesheetsForXhtmlEditors, headerStyleElementContentForXhtmlEditors)
        End Function

        Public Shared Function Create(editor As IXHtmlEditor,
                                      inlineName As String,
                                      inlineTemplates As Dictionary(Of String, String),
                                      inlineFindingOverride As String,
                                      inlineElement As InlineElement,
                                      resourceEntity As ResourceEntity,
                                      bankId As Integer,
                                      resourceManager As ResourceManagerBase,
                                      hasLoadedOldItemLayoutTemplate As Boolean,
                                      defaultNamespaceManager As XmlNamespaceManager) As HtmlInlineHandler

            Return New HtmlInlineHandler(editor, inlineName, inlineTemplates, inlineFindingOverride, inlineElement, resourceEntity, bankId, resourceManager, hasLoadedOldItemLayoutTemplate)
        End Function


        Private _InlineName As String
        Private ReadOnly _inlineFindingOverride As String
        private ReadOnly _inlineElement As InlineElement
        Private ReadOnly _hasLoadedOldItemLayoutTemplate As Boolean
        Private ReadOnly _stylesheetsForXhtmlEditors As Dictionary(Of String, String)
        Private ReadOnly _headerStyleElementContentForXhtmlEditors As String
        Private _canExecute As Nullable(Of Boolean)
        Public Event AddingInlineCustomInteraction As EventHandler(Of InlineElementEventArgs)
        Public Event AddingInlineAspect As EventHandler(Of InlineElementEventArgs)
        Public Event AddingPopup As EventHandler(Of InlineElementEventArgs)


        Private Sub New(editor As IXHtmlEditor,
                        inlineName As String,
                        inlineTemplates As Dictionary(Of String, String),
                        inlineFindingOverride As String,
                        inlineElement As InlineElement,
                        resourceEntity As ResourceEntity,
                        bankId As Integer,
                        resourceManager As ResourceManagerBase,
                        hasLoadedOldItemLayoutTemplate As Boolean,
                        stylesheetsForXhtmlEditors As Dictionary(Of String, String),
                        headerStyleElementContentForXhtmlEditors As String)

            MyBase.New(editor, bankId, resourceManager, inlineTemplates, resourceEntity, Nothing)

            _InlineName = inlineName
            _inlineFindingOverride = inlineFindingOverride
            _inlineElement = inlineElement
            _hasLoadedOldItemLayoutTemplate = hasLoadedOldItemLayoutTemplate
            _stylesheetsForXhtmlEditors = stylesheetsForXhtmlEditors
            _headerStyleElementContentForXhtmlEditors = headerStyleElementContentForXhtmlEditors
        End Sub

        Private Sub New(editor As IXHtmlEditor,
                        inlineName As String,
                        inlineTemplates As Dictionary(Of String, String),
                        inlineFindingOverride As String,
                        inlineElement As InlineElement,
                        resourceEntity As ResourceEntity,
                        bankId As Integer,
                        resourceManager As ResourceManagerBase,
                        hasLoadedOldItemLayoutTemplate As Boolean)

            Me.New(editor, inlineName, inlineTemplates, inlineFindingOverride, inlineElement, resourceEntity, bankId, resourceManager, hasLoadedOldItemLayoutTemplate, Nothing, String.Empty)
        End Sub

        Public Sub New(editor As IXHtmlEditor,
            resourceManager As ResourceManagerBase,
            bankId As Integer,
            inlineTemplates As Dictionary(Of String, String),
            resourceEntity As ResourceEntity,
            parameter As XHtmlParameter)

            MyBase.New(editor, bankId, resourceManager, inlineTemplates, resourceEntity, parameter)
        End Sub



        Public Function CanExecute() As Boolean
            If _canExecute Is Nothing Then
                Dim result As Boolean = (ResManager.GetResource(_InlineName) IsNot Nothing)
                If Not result Then
                    result = Not (CheckUsageOfEmbeddedInlineMediaTemplate.Equals(String.Empty))
                End If
                _canExecute = result
            End If
            Return CBool(_canExecute)
        End Function

        Public Sub AddInlineControlId(id As String, parameterCollection As ParameterSetCollection, inlineName As String)
            For Each parameters In parameterCollection
                For Each possibleControlIds As String In New String() {"controlId", "inlineChoiceId", "inlineGapMatchId", "controllerId", "gapId"}
                    If parameters.GetParameterByName(possibleControlIds, False) IsNot Nothing AndAlso TypeOf parameters.GetParameterByName(possibleControlIds, False) Is PlainTextParameter AndAlso
                         String.IsNullOrEmpty(DirectCast(parameters.GetParameterByName(possibleControlIds), PlainTextParameter).Value) Then
                        Dim parameter = DirectCast(parameters.GetParameterByName(possibleControlIds), PlainTextParameter)

                        parameter.SetValue(id)
                    End If
                Next
            Next
        End Sub

        Private Function CheckUsageOfEmbeddedInlineMediaTemplate() As String
            If ResEntity IsNot Nothing Then
                If (TypeOf ResEntity Is GenericResourceEntity OrElse TypeOf ResEntity Is AspectResourceEntity OrElse TypeOf ResEntity Is ItemResourceEntity) AndAlso InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(_InlineName) Then
                    Return _InlineName
                End If
            End If
            Return String.Empty
        End Function

        Public Sub SetFindingOverrideOnScoringParameters(paramset As ParameterSetCollection, findingOverride As String)
            If Not String.IsNullOrEmpty(findingOverride) Then
                Dim scoringParams = paramset.DeepFetchScoringParameters()
                For Each sp In scoringParams
                    sp.FindingOverride = findingOverride
                Next
            End If
        End Sub

        Public Function ConvertPastedImagesToInlineElements(pastedTags As HtmlNodeCollection, pasteType As HtmlContentHelper.PasteFrom) As Boolean
            Return ConvertPastedImagesToInlineElements(pastedTags, pasteType, Nothing, Nothing)
        End Function

        Public Function ConvertPastedImagesToInlineElements(pastedTags As HtmlNodeCollection, pasteType As HtmlContentHelper.PasteFrom, lastCopiedInlineElements As List(Of InlineElement),
                                                            droppedInlineElements As Dictionary(Of String, String)) As Boolean
            Dim previousPastedInlineLabels As New HashSet(Of String)
            If pastedTags IsNot Nothing AndAlso pastedTags.Any() Then
                Dim layoutTemplateName As String = InlineMediaTemplateHelper.GetInlineMediaTemplate(inlineTemplates, "image", New DefaultHtmlInlineTemplateNames)
                Dim isEmbeddedResourceTemplate = InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(layoutTemplateName)

                If not isEmbeddedResourceTemplate AndAlso ResManager.GetResource(layoutTemplateName) Is Nothing Then
                    MessageBox.Show(String.Format(My.Resources.ItemLayoutTemplateInlineMultimediaMissing, layoutTemplateName))
                    Return False
                End If

                Using pc As New PlaceHolderHelper
                    For Each pastedImageTag As HtmlNode In pastedTags

                        If IsInlineControl(pastedImageTag) Then
                            If CanPasteInlineControl(pastedImageTag, lastCopiedInlineElements) AndAlso TryPasteInlineControl(pastedImageTag, lastCopiedInlineElements, previousPastedInlineLabels) Then
                                Continue For
                            Else
                                MessageBox.Show(My.Resources.CannotCopyPasteInlineControls)
                                Return False
                            End If
                        End If

                        Dim inlineElement As New InlineElement()
                        inlineElement.LayoutTemplateSourceName = layoutTemplateName
                        If Not isEmbeddedResourceTemplate Then
                            Dim adapter = New ItemLayoutAdapter(layoutTemplateName, Nothing, AddressOf GenericHandler_ResourceNeeded)
                            inlineElement.Parameters.AddRange(adapter.CreateParameterSetsFromItemTemplate())
                        Else
                            inlineElement.Parameters.AddRange(InlineMediaTemplateHelper.GetParameterSetFromEmbeddedResource(layoutTemplateName))
                        End If

                        Dim sourceName As String = pastedImageTag.Attributes("src").Value
                        Dim uniquenessSeed As Integer = CreateNewUniqueContextIdentifier(sourceName) - 1
                        Dim placeholderMatchResult As Match = Nothing

                        If (RegexpHelper.TryMatchPlaceHolder(resourceProtocolPrefix & sourceName, placeholderMatchResult)) OrElse (RegexpHelper.TryMatchBase64ImageSource(resourceProtocolPrefix & sourceName, placeholderMatchResult)) Then
                            MessageBox.Show(My.Resources.OnlyImagesSupportCopyPaste)
                            Return False
                        End If

                        Dim imageNameInBank As String

                        If ResourceFactory.Instance.ResourceExists(bankId, Path.GetFileName(sourceName), True) Then
                            imageNameInBank = Path.GetFileName(sourceName)
                        Else
                            Dim result As Uri = Nothing
                            If Uri.TryCreate(sourceName, UriKind.Absolute, result) Then
                                imageNameInBank = LinkPastedImageAsResource(result, uniquenessSeed, bankId, pasteType, "InlineElement")
                            Else
                                imageNameInBank = LinkPastedImageAsResource(New Uri(Path.GetFullPath(sourceName)), uniquenessSeed, bankId, pasteType, "InlineElement")
                            End If

                        End If

                        If Not String.IsNullOrEmpty(imageNameInBank) Then
                            Dim sourceParameter = DirectCast(inlineElement.Parameters(0).GetParameterByName("source"), ResourceParameter)
                            sourceParameter.Value = imageNameInBank
                            Dim width As Integer? = Nothing
                            Dim height As Integer? = Nothing
                            Dim editedSize = False

                            If pastedImageTag.Attributes("width") IsNot Nothing Then
                                width = CType(pastedImageTag.Attributes("width").Value, Integer)
                                editedSize = True
                            End If
                            If pastedImageTag.Attributes("height") IsNot Nothing Then
                                height = CType(pastedImageTag.Attributes("height").Value, Integer)
                                editedSize = True
                            End If

                            If (editedSize) Then
                                sourceParameter.EditSize = True
                                If width.HasValue Then sourceParameter.Width = width.Value
                                If height.HasValue Then sourceParameter.Height = height.Value
                            End If

                            Dim xHtmlDocument As New XHtmlDocument
                            Dim id = InlineElementHelper.GetNewInlineElementIdentifier()
                            inlineElement.Identifier = id

                            Dim pasteEl As XmlNode = pc.InlineElementToPlaceHolderImage(inlineElement, "http://www.w3.org/1999/xhtml")
                            xHtmlDocument.InnerXml = cntHlp.GiveResourceElementsContextNumber(pasteEl.OuterXml, uniquenessSeed)

                            Dim newNode = HtmlNode.CreateNode(xHtmlDocument.SelectSingleNode("//*[@isinlineelement=""true""]").OuterXml)

                            If (droppedInlineElements IsNot Nothing) Then
                                Dim idAttribute = pastedImageTag.Attributes("id")
                                If (idAttribute IsNot Nothing AndAlso Not String.IsNullOrEmpty(idAttribute.Value)) Then
                                    droppedInlineElements(idAttribute.Value) = id
                                End If
                            End If

                            pastedImageTag.ParentNode.ReplaceChild(newNode, pastedImageTag)
                            OnInlineElementAdded(Me, New InlineElementEventArgs(inlineElement))
                            OnResourceAdded(Me, New ResourceNameEventArgs(imageNameInBank))
                        Else
                            Return False
                        End If
                    Next
                End Using
            End If
            Return True
        End Function

        Public Function IsPopupInlineElement(inlineElementTemplate As String) As Boolean
            If String.IsNullOrEmpty(inlineElementTemplate) Then Return False
            If inlineTemplates IsNot Nothing AndAlso inlineTemplates.Any(Function(t) t.Key.Equals("popup", StringComparison.InvariantCultureIgnoreCase) AndAlso t.Value.Equals(inlineElementTemplate, StringComparison.InvariantCultureIgnoreCase)) Then
                Return True
            End If
            Return False
        End Function

        Private Function IsInlineControl(pastedImageTag As HtmlNode) As Boolean
            Return pastedImageTag.Attributes.Contains("isinlinecontrol") AndAlso Boolean.Parse(pastedImageTag.Attributes("isinlinecontrol").Value) = True
        End Function

        Private Function CanPasteInlineControl(pastedImageTag As HtmlNode, lastCopiedElements As List(Of InlineElement)) As Boolean
            Dim result = True

            If param Is Nothing OrElse Not ParamIsSourceOfInlineElement(pastedImageTag, lastCopiedElements) Then
                result = False
            End If

            Return result
        End Function

        Private Function TryPasteInlineControl(pastedImageTag As HtmlNode, lastCopiedElements As List(Of InlineElement), previousPastedLabels As HashSet(Of String)) As Boolean
            If param Is Nothing Then Return False

            Dim inlineElementToCopy = GetInlineElementToCopy(pastedImageTag, lastCopiedElements)
            If inlineElementToCopy Is Nothing Then Return False

            Dim isCut = Not param.GetInlineElements().ContainsKey(inlineElementToCopy.Identifier)

            If (isCut) Then
                OnInlineElementAdded(Me, New InlineElementEventArgs(inlineElementToCopy))
            Else
                Dim newInlineElement = inlineElementToCopy.DeepClone()
                AlterInlineElementIdentifier(newInlineElement, inlineElementToCopy.Identifier)
                AlterInlineElementLabel(newInlineElement, previousPastedLabels)
                Dim newTag = pastedImageTag.CloneNode(True)
                newTag.Attributes("id").Value = newInlineElement.Identifier
                pastedImageTag.ParentNode.ReplaceChild(newTag, pastedImageTag)
                OnInlineElementAdded(Me, New InlineElementEventArgs(newInlineElement))
            End If

            Return True
        End Function

        Private Sub AlterInlineElementIdentifier(inlineElement As InlineElement, oldIdentifier As String)
            Dim newIdentifier = InlineElementHelper.GetNewInlineElementIdentifier()

            inlineElement.Identifier = newIdentifier
            For Each prm In inlineElement.Parameters.UniqueFlattenedParameters().OfType(Of PlainTextParameter)()
                If prm.Value.Equals(oldIdentifier, StringComparison.InvariantCultureIgnoreCase) Then
                    prm.Value = newIdentifier
                End If
            Next
        End Sub

        Private Sub AlterInlineElementLabel(inlineElement As InlineElement, previousPastedLabels As HashSet(Of String))
            Dim labelParams = New String() {"gaplabel", "inlinechoicelabel", "controllabel", "inlinegapmatchlabel"}

            Dim labelParam = inlineElement.Parameters.UniqueFlattenedParameters().OfType(Of PlainTextParameter).FirstOrDefault(Function(prm) labelParams.Contains(prm.Name.ToLower))

            If (labelParam Is Nothing) Then
                Return
            End If


            Dim labelValue = labelParam.Value
            Dim splittedLabelValue = labelValue.Split(New String() {"-"}, StringSplitOptions.None)
            Dim alreadyHasSequenceNr = splittedLabelValue.Length > 1 AndAlso IsNumeric(splittedLabelValue(splittedLabelValue.Length - 1))

            Dim labelWithoutSeq As String = If(alreadyHasSequenceNr, String.Join("-", splittedLabelValue.Take(splittedLabelValue.Length - 1)), labelValue)

            Dim seqNr = 1
            Dim newLabel = String.Format("{0}-{1:00}", labelWithoutSeq, seqNr)
            While previousPastedLabels.Contains(newLabel) OrElse Not IsUniqueLabel(newLabel, labelParam)
                seqNr += 1
                newLabel = String.Format("{0}-{1:00}", labelWithoutSeq, seqNr)
            End While

            previousPastedLabels.Add(newLabel)
            labelParam.Value = newLabel

            Dim scoringsParameters = inlineElement.Parameters.UniqueFlattenedParameters.OfType(Of ScoringParameter)()
            If scoringsParameters.Count > 1 Then
                For Each sp As ScoringParameter In scoringsParameters
                    Dim currentLabel As String = If(String.IsNullOrEmpty(sp.Label), sp.Name, sp.Label)
                    Dim splittedCurrentLabel = currentLabel.Split("-"c)
                    labelWithoutSeq = If(alreadyHasSequenceNr, String.Join("-", splittedCurrentLabel.Take(splittedCurrentLabel.Length - 1)), currentLabel)

                    sp.Label = String.Format("{0}-{1:00}", labelWithoutSeq, seqNr)
                Next
            ElseIf scoringsParameters.Count = 1 Then
                scoringsParameters.First().Label = newLabel
            End If
        End Sub

        Private Function IsUniqueLabel(newLabel As String, labelParameter As PlainTextParameter) As Boolean
            If (param Is Nothing) Then Return True

            For Each inlineElement In param.GetInlineElements()
                Dim lbl = inlineElement.Value.Parameters.UniqueFlattenedParameters.OfType(Of PlainTextParameter).FirstOrDefault(Function(prm) prm.Name.Equals(labelParameter.Name, StringComparison.InvariantCultureIgnoreCase))
                If lbl Is Nothing Then Continue For

                If lbl.Value.Equals(newLabel, StringComparison.InvariantCultureIgnoreCase) Then Return False
            Next

            Return True
        End Function

        Private Function ParamIsSourceOfInlineElement(pastedImageTag As HtmlNode, lastCopiedElements As List(Of InlineElement)) As Boolean
            Dim id = pastedImageTag.GetAttributeValue("id", String.Empty)
            If String.IsNullOrEmpty(id) Then Return False

            Return param.GetInlineElements().ContainsKey(id) OrElse (lastCopiedElements IsNot Nothing AndAlso lastCopiedElements.Any(Function(inl) inl.Identifier.Equals(id, StringComparison.InvariantCultureIgnoreCase)))
        End Function

        Private Function GetInlineElementToCopy(pastedImageTag As HtmlNode, lastCopiedElements As List(Of InlineElement)) As InlineElement
            Dim id = pastedImageTag.GetAttributeValue("id", String.Empty)
            If param.GetInlineElements().ContainsKey(id) Then
                Return param.GetInlineElements(id)
            Else
                Return lastCopiedElements.FirstOrDefault(Function(inl) inl.Identifier.Equals(id, StringComparison.CurrentCultureIgnoreCase))
            End If
        End Function

        Public Sub OnAddingInlineCustomInteraction(e As InlineElementEventArgs)
            If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
                RaiseEvent AddingInlineCustomInteraction(Me, e)
            End If
        End Sub

        Public Sub OnAddingInlineAspect(e As InlineElementEventArgs)
            If e IsNot Nothing AndAlso e.InlineElement IsNot Nothing Then
                RaiseEvent AddingInlineAspect(Me, e)
            End If
        End Sub



        Public ReadOnly Property RequiredResource As String
            Get
                Return _InlineName
            End Get
        End Property

        Public ReadOnly Property InlineFindingOverride as String
            Get
                return _inlineFindingOverride
            End Get
        End Property

        Public ReadOnly Property InlElement as InlineElement
            Get
                return _inlineElement
            End Get
        End Property

        Public ReadOnly Property HasLoadedOldItemLayoutTemplate as Boolean
            Get
                return _hasLoadedOldItemLayoutTemplate
            End Get
        End Property

        Public ReadOnly Property StylesheetsForXhtmlEditors as Dictionary(Of String, String)
            Get
                return _stylesheetsForXhtmlEditors
            End Get
        End Property

        Public ReadOnly Property HeaderStyleElementContentForXhtmlEditors as String
            Get
                return _headerStyleElementContentForXhtmlEditors
            End Get
        End Property


    End Class

End Namespace
