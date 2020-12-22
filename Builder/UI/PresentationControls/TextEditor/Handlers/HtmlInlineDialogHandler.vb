Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ItemProcessing
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class HtmlInlineDialogHandler

    Private _handler As HtmlInlineHandler
    Private ReadOnly _resourceManager As ResourceManagerBase
    Private ReadOnly _resourceEntity As ResourceEntity

    Public Sub New(editor As IXHtmlEditor,
                   resourceManager As ResourceManagerBase,
                   bankId As Integer,
                   inlineTemplates As Dictionary(Of String, String),
                   resourceEntity As ResourceEntity,
                   parameter As XHtmlParameter)

        _handler = New HtmlInlineHandler(editor, resourceManager, bankId, inlineTemplates, resourceEntity, parameter)
        _resourceManager = resourceManager
        _resourceEntity = resourceEntity
    End Sub

    Public Sub New(handler As HtmlInlineHandler)
        _handler = handler
        _resourceManager = handler.ResManager
        _resourceEntity = handler.ResEntity
    End Sub

    Public Function Execute() As KeyValuePair(Of InlineElement, XmlNode)
        Return DialogHandled(showDialog:=True)
    End Function

    Public Function ExecuteNoDialog() As KeyValuePair(Of InlineElement, XmlNode)
        Dim result As KeyValuePair(Of InlineElement, XmlNode) = DialogHandled(showDialog:=False)
        Return result
    End Function

    Public Function ExecuteNoDialog(cachingStrategy As IITemSetupCacheHelper) As KeyValuePair(Of InlineElement, XmlNode)
        Dim result As KeyValuePair(Of InlineElement, XmlNode) = DialogHandled(showDialog:=False, cachingStrategy:=cachingStrategy)
        Return result
    End Function

    Private Function DialogHandled(showDialog As Boolean) As KeyValuePair(Of InlineElement, XmlNode)
        Return DialogHandled(showDialog, Nothing)
    End Function

    Private Function DialogHandled(showDialog As Boolean, cachingStrategy As IITemSetupCacheHelper) As KeyValuePair(Of InlineElement, XmlNode)
        Dim returnValue As KeyValuePair(Of InlineElement, XmlNode) = Nothing
        Using inlineElementPropertiesDialog As New InlineElementPropertiesDialog(_handler.InlElement, _handler.RequiredResource, _resourceEntity, _resourceManager, _handler.HasLoadedOldItemLayoutTemplate, cachingStrategy, _handler.StylesheetsForXhtmlEditors, _handler.HeaderStyleElementContentForXhtmlEditors)
            Dim originalParameters As ParameterSetCollection = Nothing
            If _handler.InlElement IsNot Nothing Then
                originalParameters = ParameterSetCollection.DeepClone(_handler.InlElement.Parameters)
            End If
            If ((Not showDialog) OrElse (inlineElementPropertiesDialog.ShowDialog() = DialogResult.OK)) Then
                Dim inlineElement As InlineElement
                If _handler.InlElement Is Nothing Then
                    inlineElement = New InlineElement()
                    Dim Id = InlineElementHelper.GetNewInlineElementIdentifier()
                    inlineElement.Identifier = Id
                    inlineElement.LayoutTemplateSourceName = _handler.RequiredResource

                    Dim prmSet As ParameterSetCollection
                    If (showDialog) Then
                        prmSet = inlineElementPropertiesDialog.ParameterSetsEditorInstance.ParameterSets
                    Else
                        prmSet = inlineElementPropertiesDialog.InlineElementParameters
                        If prmSet Is Nothing Then
                            Dim psHelper As New ParameterSetCollectionHelper(_resourceManager, _handler.RequiredResource)
                            prmSet = psHelper.GetExtractedParameters
                        End If
                    End If

                    inlineElement.InlineFindingOverride = _handler.InlineFindingOverride

                    _handler.AddInlineControlId(Id, prmSet, _handler.RequiredResource)
                    inlineElement.Parameters.AddRange(prmSet)
                Else
                    inlineElement = _handler.InlElement
                End If

                _handler.SetFindingOverrideOnScoringParameters(inlineElement.Parameters, inlineElement.InlineFindingOverride)

                If inlineElementPropertiesDialog.ShouldUpdateCustomInteractionParameters Then
                    _handler.OnAddingInlineCustomInteraction(New InlineElementEventArgs(inlineElement))
                ElseIf inlineElementPropertiesDialog.AddedInlineAspect Then
                    _handler.OnAddingInlineAspect(New InlineElementEventArgs(inlineElement))
                End If

                Using pHelper As New PlaceHolderHelper()
                    Dim inlineElementNode = pHelper.InlineElementToPlaceHolderImage(inlineElement, "http://www.w3.org/1999/xhtml", _resourceManager, _handler.IsPopupInlineElement(inlineElement.LayoutTemplateSourceName))
                    returnValue = New KeyValuePair(Of InlineElement, XmlNode)(inlineElement, inlineElementNode)
                End Using
            Else
                If _handler.InlElement IsNot Nothing Then
                    _handler.InlElement.Parameters.Clear()
                    _handler.InlElement.Parameters.AddRange(originalParameters)
                End If
            End If
        End Using
        Return returnValue
    End Function

    Public ReadOnly Property RequiredResource As String
        Get
            Return _handler.RequiredResource
        End Get
    End Property


End Class
