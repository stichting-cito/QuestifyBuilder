Imports System.IO
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports System.Text
Imports System.Xml.Serialization
Imports Cinch
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Factories


Namespace ItemProcessing

    Public Class ItemTemplateSwitching


        Private ReadOnly _resourceManager As ResourceManagerBase
        Private ReadOnly _itemToConvert As ItemResourceEntity
        Private ReadOnly _messageBoxService As IMessageBoxService
        Private _fromTemplate As ItemLayoutTemplateResourceEntity = Nothing
        Private _inlineCustomInteractionTemplate As String = String.Empty
        Private _inlineCustomInteractionParameterCollectionPrefix As String = "__CI_"
        Private _runSolutionCleaner As Boolean = False
        Private _switchedParamCollectionIds As Dictionary(Of String, String) = Nothing


        Public Property LastErrorOrWarning As WarningsAndErrors

        Public WriteOnly Property SwitchedParamCollectionIds As Dictionary(Of String, String)
            Set
                _switchedParamCollectionIds = Value
            End Set
        End Property


        Sub New(itemToConvert As ItemResourceEntity, resourceManager As ResourceManagerBase, messageBoxService As IMessageBoxService)
            _resourceManager = resourceManager
            _itemToConvert = itemToConvert
            _messageBoxService = messageBoxService
        End Sub


        Public Function SwitchToTemplate(nameOfTemplate As String, ByRef newAssessmentItem As AssessmentItem) As Boolean
            Dim warningsAndErrors As New WarningsAndErrors()
            newAssessmentItem = Nothing
            _runSolutionCleaner = False

            Try
                Dim currentAssessmentItm As AssessmentItem = _itemToConvert.GetAssessmentItem()
                Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = False}
                Dim templateResources = ResourceFactory.Instance.GetResourcesByNamesWithOption(_itemToConvert.BankId, New List(Of String) From {nameOfTemplate, currentAssessmentItm.LayoutTemplateSourceName}, request)
                Dim newTemplate As ItemLayoutTemplateResourceEntity = Nothing
                If templateResources.ToList().Any(Function(res) TypeOf res Is ItemLayoutTemplateResourceEntity AndAlso CType(res, ItemLayoutTemplateResourceEntity).Name.Equals(nameOfTemplate, StringComparison.InvariantCultureIgnoreCase)) Then
                    newTemplate = DirectCast(templateResources.First(Function(res) TypeOf res Is ItemLayoutTemplateResourceEntity AndAlso CType(res, ItemLayoutTemplateResourceEntity).Name.Equals(nameOfTemplate, StringComparison.InvariantCultureIgnoreCase)), ItemLayoutTemplateResourceEntity)
                End If
                If templateResources.ToList().Any(Function(res) TypeOf res Is ItemLayoutTemplateResourceEntity AndAlso CType(res, ItemLayoutTemplateResourceEntity).Name.Equals(currentAssessmentItm.LayoutTemplateSourceName, StringComparison.InvariantCultureIgnoreCase)) Then
                    _fromTemplate = DirectCast(templateResources.First(Function(res) TypeOf res Is ItemLayoutTemplateResourceEntity AndAlso CType(res, ItemLayoutTemplateResourceEntity).Name.Equals(currentAssessmentItm.LayoutTemplateSourceName, StringComparison.InvariantCultureIgnoreCase)), ItemLayoutTemplateResourceEntity)
                End If

                If newTemplate Is Nothing OrElse _fromTemplate Is Nothing Then
                    warningsAndErrors.ErrorList.Add(My.Resources.ItemTemplateSwitchingUnableToLoadTemplate)
                    _messageBoxService.ShowError(warningsAndErrors.ErrorList.First())
                    Return False
                End If

                Dim itmHelper As New AssessmentItemHelper(_resourceManager, newTemplate.Name, _itemToConvert, Nothing)
                Dim itmHelperCurrent As New AssessmentItemHelper(_resourceManager, currentAssessmentItm.LayoutTemplateSourceName, _itemToConvert, Nothing)
                Dim currentDependencies = _itemToConvert.DependentResourceCollection.ToList()

                GetInlineCustomInteractionTemplateName()

                Dim assmntItem = itmHelper.CreateNewAssessmentItem(_itemToConvert, newTemplate, warningsAndErrors)
                assmntItem.Identifier = currentAssessmentItm.Identifier
                assmntItem.Title = currentAssessmentItm.Title
                assmntItem.ItemId = currentAssessmentItm.ItemId

                CopyParameterValues(currentAssessmentItm, itmHelperCurrent.GetExtractedParameters(), assmntItem, warningsAndErrors)
                UpdateRedirectedParameters(assmntItem, itmHelperCurrent.GetExtractedParameters(), warningsAndErrors)
                assmntItem.Parameters.FixReferencedAttributes()
                RemoveUnsupportedInlineElements(assmntItem, newTemplate, warningsAndErrors)

                If _runSolutionCleaner Then
                    assmntItem.Solution.FixRemovedScoringParameters(assmntItem.Parameters.DeepFetchInlineScoringParameters)
                End If

                If ValidateSolution(currentAssessmentItm.Solution, assmntItem) Then
                    assmntItem.Solution = currentAssessmentItm.Solution
                Else
                    warningsAndErrors.WarningList.Add(My.Resources.SolutionWillBeDeletedNoMatchPossible)
                End If

                If (warningsAndErrors.Errors) Then
                    _messageBoxService.ShowError(warningsAndErrors.ErrorList.First())
                    ResetDependencies(currentDependencies)
                    Return False
                End If

                If (warningsAndErrors.Warnings) Then
                    If (UserConfirmed(warningsAndErrors, nameOfTemplate)) Then
                        warningsAndErrors.WarningList.Clear()
                    Else
                        ResetDependencies(currentDependencies)
                        Return False
                    End If
                End If

                newAssessmentItem = assmntItem
                _itemToConvert.SetAssessmentItem(assmntItem)

                UpdateItemDependencies(_fromTemplate)

            Catch ex As Exception
                warningsAndErrors.ErrorList.Add($"Error while switching to template '{nameOfTemplate}': {ex.Message}")
            End Try

            LastErrorOrWarning = warningsAndErrors
            Return Not warningsAndErrors.Errors AndAlso Not warningsAndErrors.Warnings
        End Function

        Private Sub ResetDependencies(currentDependencies As List(Of DependentResourceEntity))
            _itemToConvert.DependentResourceCollection.Clear()
            _itemToConvert.DependentResourceCollection.AddRange(currentDependencies)
        End Sub

        Private Sub UpdateItemDependencies(fromTemplate As ItemLayoutTemplateResourceEntity)


            If (fromTemplate IsNot Nothing) Then
                Dim fromTemplateDependency = _itemToConvert.DependentResourceCollection.Where(Function(dpr) dpr.DependentResourceId = fromTemplate.ResourceId).ToList()
                For i = 0 To fromTemplateDependency.Count - 1
                    _itemToConvert.DependentResourceCollection.Remove(fromTemplateDependency(i))
                Next
            End If

            _itemToConvert.ReloadItemLayoutTemplateUsed()

            _itemToConvert.UpdateDependencies()
        End Sub


        Private Function UserConfirmed(warnAndErr As WarningsAndErrors, targetTemplateName As String) As Boolean
            Dim msg As String = String.Format(My.Resources.SwitchingTemplateConsequencesWarning, targetTemplateName, GetWarningMessage(warnAndErr))
            Return _messageBoxService.ShowYesNo(msg, CustomDialogIcons.Question) = CustomDialogResults.Yes
        End Function

        Private Function GetWarningMessage(warnAndErr As WarningsAndErrors) As String
            Dim sb As New StringBuilder()
            For Each w In warnAndErr.WarningList
                sb.AppendLine($"- {w}")
            Next

            Return sb.ToString()
        End Function

        Private Function ValidateSolution(currentSolution As Solution, newAssessmentItem As AssessmentItem) As Boolean

            Dim serializedSolution = SerializeToXml(currentSolution)
            Dim scoringsPrms = newAssessmentItem.Parameters.DeepFetchScoringParameters()
            currentSolution.FixRemovedScoringParameters(scoringsPrms)

            Dim serializedFixedSolution = SerializeToXml(currentSolution)

            Return serializedSolution.Equals(serializedFixedSolution, StringComparison.InvariantCultureIgnoreCase)
        End Function

        Private Function SerializeToXml(solution As Solution) As String
            Dim serializer As New XmlSerializer(GetType(Solution))
            Dim sb As New StringBuilder()
            Using writer As New StringWriter(sb)
                serializer.Serialize(writer, solution)
            End Using

            Return sb.ToString().Trim()
        End Function

        Private Sub CopyParameterValues(current As AssessmentItem, extractedParameters As ParameterSetCollection, target As AssessmentItem, ByRef warnAndErr As WarningsAndErrors)
            For Each dynamicCollection In current.Parameters.Where(Function(p) p.IsDynamicCollection)
                target.Parameters.Add(dynamicCollection)
            Next

            For Each paramCollection In current.Parameters.Where(Function(p) Not p.IsDynamicCollection)
                Dim targetCollection As ParameterCollection = target.Parameters.GetParameterCollectionByName(paramCollection.Id)
                If targetCollection IsNot Nothing Then
                    For Each param In paramCollection.InnerParameters
                        Dim paramFromCurrentTemplate = extractedParameters.GetParameter(param.Name, GetCorrectParamCollectionId(paramCollection.Id))
                        CopyParameterValue(param, paramFromCurrentTemplate, targetCollection, warnAndErr, target)
                    Next
                Else
                    ReportCollectionToBeDeleted(paramCollection, warnAndErr, target)
                End If
            Next
        End Sub

        Private Function GetCorrectParamCollectionId(prmCollectionId As String) As String
            If _switchedParamCollectionIds IsNot Nothing AndAlso _switchedParamCollectionIds.ContainsKey(prmCollectionId) Then
                Return _switchedParamCollectionIds(prmCollectionId)
            End If
            Return prmCollectionId
        End Function

        Private Sub CopyParameterValue(
         sourceParameter As ParameterBase,
         sourceParamFromTemplate As ParameterBase,
         targetCollection As ParameterCollection,
         warnAndErr As WarningsAndErrors,
         assessmentItem As AssessmentItem)

            Dim targetParameter = targetCollection.InnerParameters.FirstOrDefault(Function(tp) tp.Name.Equals(sourceParameter.Name))

            Dim paramName = If(sourceParamFromTemplate IsNot Nothing, sourceParamFromTemplate.GetDesignerValue("label", String.Empty), Nothing)
            If String.IsNullOrEmpty(paramName) Then
                paramName = sourceParameter.Name
            End If

            If targetParameter Is Nothing OrElse Not sourceParameter.GetType() = targetParameter.GetType() Then
                warnAndErr.WarningList.Add(String.Format(My.Resources.ParameterWillBeDeletedMessage, paramName))
                If TypeOf sourceParameter Is XHtmlParameter Then
                    CleanPossibleInlineCiParametersForXhtmParameter(DirectCast(sourceParameter, XHtmlParameter), assessmentItem)
                End If
            ElseIf Not targetParameter.IsVisible() Then
                If sourceParamFromTemplate IsNot Nothing Then
                    If sourceParamFromTemplate.IsVisible() AndAlso Not sourceParameter.EqualsByValue(targetParameter) Then
                        warnAndErr.WarningList.Add(String.Format(My.Resources.ParameterWillBeDeletedMessage, paramName))
                    ElseIf Not sourceParamFromTemplate.IsVisible Then
                        Dim sourceDefault = sourceParamFromTemplate.GetDesignerValue("defaultvalue", String.Empty)
                        If String.IsNullOrEmpty(sourceDefault) AndAlso sourceParameter.Nodes IsNot Nothing AndAlso sourceParameter.Nodes.Count > 0 Then
                            targetParameter.Nodes = sourceParameter.Nodes
                        End If
                    End If
                ElseIf sourceParameter IsNot Nothing AndAlso Not sourceParameter.EqualsByValue(targetParameter) Then
                    targetParameter.Nodes = sourceParameter.Nodes
                End If

                Dim defaultValue = targetParameter.GetDesignerValue("defaultvalue", String.Empty)

                If Not String.IsNullOrEmpty(defaultValue) Then
                    targetParameter.SetValue(defaultValue)
                End If

                If TypeOf sourceParameter Is XHtmlParameter Then
                    CleanPossibleInlineCiParametersForXhtmParameter(DirectCast(sourceParameter, XHtmlParameter), assessmentItem)
                End If
            ElseIf targetParameter.IsVisible() Then
                If TypeOf sourceParameter Is ScoringParameter Then
                    CopyScoringParameter(DirectCast(targetParameter, ScoringParameter), DirectCast(sourceParameter, ScoringParameter), warnAndErr)
                ElseIf TypeOf sourceParameter Is CollectionParameter Then
                    CopyCollectionParameter(DirectCast(targetParameter, CollectionParameter), DirectCast(sourceParameter, CollectionParameter), DirectCast(sourceParamFromTemplate, CollectionParameter), warnAndErr, assessmentItem)
                Else
                    Dim defaultValue = targetParameter.GetDesignerValue("defaultvalue", String.Empty)
                    If sourceParamFromTemplate IsNot Nothing AndAlso Not sourceParamFromTemplate.IsVisible() AndAlso Not String.IsNullOrEmpty(defaultValue) Then
                        targetParameter.SetValue(defaultValue)
                    Else
                        targetParameter.Nodes = sourceParameter.Nodes
                    End If
                End If
            End If
        End Sub

        Private Sub CopyScoringParameter(ByRef target As ScoringParameter, source As ScoringParameter, warnAndErr As WarningsAndErrors)
            Dim merger = ParameterHandler.GetConcreteMerger(target.GetType())
            merger.Merge(target, source, warnAndErr)

            For Each prm In target.BluePrint.InnerParameters
                Dim defaultValue = prm.GetDesignerValue("defaultvalue", String.Empty)
                Dim visible = prm.GetDesignerValue("visible", True)

                If visible Then
                    Continue For
                End If

                For Each subPrmSet In target.Value
                    Dim targetPrm = subPrmSet.InnerParameters.FirstOrDefault(Function(p) p.Name.Equals(prm.Name, StringComparison.InvariantCultureIgnoreCase))
                    Dim msg = String.Format(My.Resources.ParameterWillBeDeletedMessage, targetPrm.Name)

                    If Not warnAndErr.WarningList.Contains(msg) Then
                        warnAndErr.WarningList.Add(msg)
                    End If
                    targetPrm.SetValue(defaultValue)
                Next
            Next
        End Sub

        Private Sub CopyCollectionParameter(ByRef target As CollectionParameter, source As CollectionParameter, sourceFromTemplate As CollectionParameter,
                                    warnAndErr As WarningsAndErrors, assessmentItem As AssessmentItem)

            For Each sourceCollection In source.Value
                Dim targetCollection = target.BluePrint.DeepCloneWithDesignerSettingsAndAttributeReferences
                targetCollection.Id = sourceCollection.Id
                target.Value.Add(targetCollection)

                For Each sourcePrm In sourceCollection.InnerParameters
                    Dim tmplPrm As ParameterBase = Nothing
                    If sourceFromTemplate IsNot Nothing Then
                        tmplPrm = DirectCast(sourceFromTemplate.BluePrint.GetParameterByName(sourcePrm.Name, False), ParameterBase)
                    End If

                    CopyParameterValue(sourcePrm, tmplPrm, targetCollection, warnAndErr, assessmentItem)
                Next
            Next
        End Sub


        Private Sub UpdateRedirectedParameters(
                assmntItem As AssessmentItem,
                paramsFromTemplate As ParameterSetCollection,
                errAndWarn As WarningsAndErrors)

            Dim redirectedParams = assmntItem.Parameters.SelectMany(Function(paramColl) paramColl.InnerParameters.Where(Function(param) param.IsRedirected))
            For Each param In redirectedParams
                Dim targetControlId = param.GetDesignerValue("redirectToTargetControlId", String.Empty)
                Dim targetParamId = param.GetDesignerValue("redirectToTargetParameterId", String.Empty)

                If String.IsNullOrEmpty(targetControlId) OrElse String.IsNullOrEmpty(targetParamId) Then
                    Continue For
                End If

                Dim targetParam = assmntItem.Parameters.GetParameter(targetParamId, targetControlId)
                If targetParam IsNot Nothing Then

                    If TypeOf param Is CollectionParameter Then
                        Dim paramFromCurrentTemplate = paramsFromTemplate.GetParameter(param.Name, targetControlId)
                        CopyCollectionParameter(DirectCast(param, CollectionParameter), DirectCast(targetParam, CollectionParameter), DirectCast(paramFromCurrentTemplate, CollectionParameter), errAndWarn, assmntItem)
                    Else
                        param.Nodes = targetParam.Nodes
                    End If
                End If
            Next
        End Sub

        Private Sub RemoveUnsupportedInlineElements(
                assmntItem As AssessmentItem,
                newTemplate As ItemLayoutTemplateResourceEntity,
                errAndWarn As WarningsAndErrors)

            For Each prm In assmntItem.Parameters.GetXhtmlParameters()
                Dim elementsRemoved = RemoveUnsupportedInlineElements(DirectCast(prm, XHtmlParameter), newTemplate, assmntItem)

                If elementsRemoved Then
                    Dim paramName = prm.GetDesignerValue("label", String.Empty)
                    If String.IsNullOrEmpty(paramName) Then
                        paramName = prm.Name
                    End If
                    errAndWarn.WarningList.Add(String.Format(My.Resources.InlineElementRemovedFromParameter, paramName))
                End If
            Next
        End Sub

        Private Function RemoveUnsupportedInlineElements(
                prm As XHtmlParameter,
                newTemplate As ItemLayoutTemplateResourceEntity,
                assessmentItem As AssessmentItem) As Boolean

            Dim inlineElements = prm.GetInlineElements()
            Dim inlineElementRemoved = False

            For Each inlineElement In inlineElements.Select(Function(ie) ie.Value)
                Dim templateName = inlineElement.LayoutTemplateSourceName

                If Not ParamStillHasInlineTemplate(prm, templateName, newTemplate) Then
                    inlineElementRemoved = True
                    Dim id = inlineElement.Identifier

                    If IsInlineCustomInteraction(templateName) Then
                        CleanupForInlineCiElements(assessmentItem, inlineElement)
                    End If
                    prm.RemoveInlineElement(id)

                    CheckScoringParametersForInlineElement(assessmentItem, id)
                End If
            Next

            Return inlineElementRemoved
        End Function

        Private Sub CheckScoringParametersForInlineElement(assessmentItem As AssessmentItem, id As String)
            Dim scoringsPrms = assessmentItem.Parameters.FlattenParameters.OfType(Of ScoringParameter)()
            For Each scoringParam In scoringsPrms
                Select Case scoringParam.GetType.ToString()
                    Case GetType(HotTextScoringParameter).ToString
                        Dim valuesToRemove = scoringParam.Value.Where(Function(subSet) subSet.Id.Equals(id)).ToList()
                        For Each v In valuesToRemove
                            scoringParam.Value.Remove(v)
                        Next
                End Select
            Next
        End Sub

        Private Sub CleanPossibleInlineCiParametersForXhtmParameter(prm As XHtmlParameter, assessmentItem As AssessmentItem)
            prm.GetInlineElements().ToList.ForEach(Sub(ie)
                                                       If IsInlineCustomInteractionElement(ie.Value) Then
                                                           CleanupForInlineCiElements(assessmentItem, ie.Value)
                                                       End If
                                                   End Sub)
        End Sub

        Private Sub CleanupForInlineCiElements(assessmentItem As AssessmentItem, inlineElement As InlineElement)
            Dim collectionName As String = GetParameterSetCollectionNameForInlineElement(inlineElement)
            If Not String.IsNullOrEmpty(collectionName) Then
                Dim toRemove = assessmentItem.Parameters.FirstOrDefault(Function(p) p.Id = collectionName)
                If toRemove IsNot Nothing Then
                    assessmentItem.Parameters.Remove(toRemove)
                    _runSolutionCleaner = True
                End If
            End If
        End Sub

        Private Function ParamStillHasInlineTemplate(
            prm As XHtmlParameter,
            templateName As String,
            newTemplate As ItemLayoutTemplateResourceEntity) As Boolean

            Dim inlineTemplate As String = prm.GetDesignerValue("inlinetemplate", String.Empty)
            Dim inlineTemplates As String = prm.GetDesignerValue("inlinetemplates", String.Empty)
            Dim inlineTemplateSettings As String() = {Cito.Tester.ContentModel.Constants.DESIGNERSETTING_INLINEIMAGETEMPLATE, Cito.Tester.ContentModel.Constants.DESIGNERSETTING_INLINEAUDIOTEMPLATE, Cito.Tester.ContentModel.Constants.DESIGNERSETTING_INLINEVIDEOTEMPLATE}

            If inlineTemplate IsNot Nothing AndAlso inlineTemplate.Equals(templateName, StringComparison.InvariantCultureIgnoreCase) Then
                Return True
            ElseIf inlineTemplates IsNot Nothing AndAlso inlineTemplates.Contains("(template=" & templateName & ";") Then
                Return True
            End If

            Dim ilt = newTemplate.GetItemLayoutTemplate()
            If ilt.DesignerSettings IsNot Nothing Then
                For Each setting In inlineTemplateSettings.Select(Function(s) ilt.DesignerSettings.GetSettingValueByKey(s))
                    If setting.Equals(templateName, StringComparison.InvariantCultureIgnoreCase) Then
                        Return True
                    End If
                Next
            End If

            Dim iltFrom = _fromTemplate.GetItemLayoutTemplate()
            If iltFrom.DesignerSettings IsNot Nothing AndAlso ilt.DesignerSettings IsNot Nothing Then
                If IsInlineCustomInteraction(templateName) Then
                    Return (templateName.Equals(ilt.DesignerSettings.GetSettingValueByKey(Cito.Tester.ContentModel.Constants.DESIGNERSETTING_INLINECITEMPLATE), StringComparison.OrdinalIgnoreCase))
                End If
            End If

        End Function

        Private Sub ReportCollectionToBeDeleted(paramCollection As ParameterCollection, warnAndErr As WarningsAndErrors, assessmentItem As AssessmentItem)
            For Each param In paramCollection.InnerParameters
                warnAndErr.WarningList.Add(String.Format(My.Resources.ParameterWillBeDeletedMessage, param.Name))
                If TypeOf param Is XHtmlParameter Then
                    CleanPossibleInlineCiParametersForXhtmParameter(DirectCast(param, XHtmlParameter), assessmentItem)
                End If
            Next
        End Sub

        Private Sub GetInlineCustomInteractionTemplateName()
            Dim ilt = _fromTemplate.GetItemLayoutTemplate()
            If ilt IsNot Nothing Then
                Dim inlineCiTemplate As String = ilt.DesignerSettings.GetSettingValueByKey(Cito.Tester.ContentModel.Constants.DESIGNERSETTING_INLINECITEMPLATE)
                If Not String.IsNullOrEmpty(inlineCiTemplate) Then
                    _inlineCustomInteractionTemplate = inlineCiTemplate
                End If
            End If
        End Sub

        Private Function IsInlineCustomInteractionElement(inlineElement As InlineElement) As Boolean
            Return IsInlineCustomInteraction(inlineElement.LayoutTemplateSourceName)
        End Function

        Private Function IsInlineCustomInteraction(templateName As String) As Boolean
            If String.IsNullOrEmpty(_inlineCustomInteractionTemplate) Then
                GetInlineCustomInteractionTemplateName()
            End If
            Return (Not String.IsNullOrEmpty(_inlineCustomInteractionTemplate) AndAlso templateName.Equals(_inlineCustomInteractionTemplate, StringComparison.OrdinalIgnoreCase))
        End Function

        Private Function GetParameterSetCollectionNameForInlineElement(inlineElement As InlineElement) As String
            Dim controlId = inlineElement.Parameters.FlattenParameters().ToList().First(Function(p) p.Name.Equals("controlId", StringComparison.OrdinalIgnoreCase))
            If controlId IsNot Nothing Then
                Return $"{_inlineCustomInteractionParameterCollectionPrefix}{controlId}"
            End If
            Return String.Empty
        End Function

    End Class
End Namespace