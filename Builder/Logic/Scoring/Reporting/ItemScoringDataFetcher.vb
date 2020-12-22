Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text
Imports Enums
Imports Questify.Builder.Logic.PublicationService
Imports Questify.Builder.Logic.Service.Factories

Namespace Scoring.Reporting

    Public Class ItemScoringReportDataFetcher
        Implements IConceptScoringBrowserDataProvider
        Implements IConceptScoringBrowserObjectFactory
        Private ReadOnly _bankId As Integer

        Private Enum ConceptTypeIdRecordId
            AttributeLevel = 2
            SubAttributeLevel = 3
        End Enum


        Public Sub New(bankId As Integer)
            _bankId = bankId
            PublicationServiceFactory = New DefaultPublicationServiceFactory()
        End Sub

        Public Function FetchConceptScoringReportData(itemResource As ItemResourceEntity, includeConceptsWithoutScore As Boolean) As Collection(Of IItemConceptScoringReportData)
            Return FetchConceptScoringReportData(itemResource, includeConceptsWithoutScore, "Questify.Tester.Plugins.Publication.Facet70.Facet70PublicationHandler, Questify.Tester.Plugins.Publication.Facet70")
        End Function

        Public Function FetchConceptScoringReportData(itemResource As ItemResourceEntity, includeConceptsWithoutScore As Boolean, publicationHandlerTypeName As String) As Collection(Of IItemConceptScoringReportData)
            Dim result As Collection(Of IItemConceptScoringReportData) = New Collection(Of IItemConceptScoringReportData)()
            Dim assessmentItem As AssessmentItem = itemResource.GetAssessmentItem()
            Dim scoringParameters As List(Of ScoringParameter) = assessmentItem.Parameters.DeepFetchInlineScoringParameters().ToList()
            Dim solution As Solution = assessmentItem.Solution

            Dim conceptScoringBrowser As New ConceptScoringBrowser(Me, Me, itemResource, scoringParameters, solution)

            If conceptScoringBrowser.[Structure] IsNot Nothing AndAlso conceptScoringBrowser.[Structure].Any() Then
                Dim combinedScoreMapsIncludedInConceptResponseLevelCounts As New List(Of CombinedScoringMapKey)()
                Dim conceptResponseCountAtAttributeLevel As Integer = 0
                Dim conceptResponseCountAtSubAttributeLevel As Integer = 0

                Dim conceptResponseLabelsAndFirstFactsPerConceptCode As Dictionary(Of String, Dictionary(Of [String], [String])) = GetPublicationConceptResponses(itemResource.ResourceId, publicationHandlerTypeName)

                If includeConceptsWithoutScore Then
                    For Each conceptPart As IConceptScoringBrowserHierarchyPart In conceptScoringBrowser.[Structure]
                        If conceptResponseLabelsAndFirstFactsPerConceptCode.Keys.Contains(conceptPart.PartName) Then
                            AddConceptWithScoreData(assessmentItem, conceptScoringBrowser, conceptPart.PartName, conceptResponseLabelsAndFirstFactsPerConceptCode(conceptPart.PartName), result, combinedScoreMapsIncludedInConceptResponseLevelCounts, _
                                conceptResponseCountAtAttributeLevel, conceptResponseCountAtSubAttributeLevel)
                        Else
                            AddConceptWithoutScoreData(assessmentItem, conceptPart.PartName, result)
                        End If
                    Next
                Else
                    For Each conceptCode As String In conceptResponseLabelsAndFirstFactsPerConceptCode.Keys
                        AddConceptWithScoreData(assessmentItem, conceptScoringBrowser, conceptCode, conceptResponseLabelsAndFirstFactsPerConceptCode(conceptCode), result, combinedScoreMapsIncludedInConceptResponseLevelCounts, _
                            conceptResponseCountAtAttributeLevel, conceptResponseCountAtSubAttributeLevel)
                    Next
                End If

                If result.Any() Then
                    Dim interactionCount As Integer = scoringParameters.Count
                    For Each item As IItemConceptScoringReportData In result
                        Adjust(item, interactionCount, conceptResponseCountAtAttributeLevel, conceptResponseCountAtSubAttributeLevel)
                    Next
                End If
            End If

            Return result
        End Function

        Private Sub Adjust(input As IItemConceptScoringReportData, interactionCount As Integer, conceptResponseCountAtAttributeLevel As Integer, conceptResponseCountAtSubAttributeLevel As Integer)
            input.InteractionCount = interactionCount
            input.AttributeLevelConceptResponseCount = conceptResponseCountAtAttributeLevel
            input.SubAttributLevelConceptResponseCount = conceptResponseCountAtSubAttributeLevel
        End Sub
        Private Sub AddConceptWithoutScoreData(assessmentItem As AssessmentItem, conceptCode As String, result As Collection(Of IItemConceptScoringReportData))
            Dim newData As New ItemConceptScoringReportData()

            newData.ItemCode = assessmentItem.Identifier
            newData.ItemId = assessmentItem.ItemId
            newData.ItemTitle = assessmentItem.Title
            newData.Itemlayouttemplate = assessmentItem.LayoutTemplateSourceName
            newData.ConceptCode = conceptCode

            result.Add(newData)
        End Sub

        Private Sub AddConceptWithScoreData(assessmentItem As AssessmentItem, conceptScoringBrowser As ConceptScoringBrowser, conceptCode As String, conceptResponseLabelsAndFirstFactsPerConceptCode As Dictionary(Of [String], [String]), result As Collection(Of IItemConceptScoringReportData), combinedScoreMapsIncludedInConceptResponseLevelCounts As List(Of CombinedScoringMapKey), _
            ByRef conceptResponseCountAtAttributeLevel As Integer, ByRef conceptResponseCountAtSubAttributeLevel As Integer)
            For Each conceptResponseAndFirstFactTuple As KeyValuePair(Of [String], [String]) In conceptResponseLabelsAndFirstFactsPerConceptCode
                Dim conceptResonseLabel As String = conceptResponseAndFirstFactTuple.Key
                Dim firstFactId As String = conceptResponseAndFirstFactTuple.Value

                Dim relatedCombinedScoringMapKey As List(Of CombinedScoringMapKey) = conceptScoringBrowser.ScorableKeyCombinations.Where(Function(x) x.Key.Any(Function(y) DefaultStringOperations.FactIdEquals(y.GetFactId(), firstFactId))).[Select](Function(z) z.Key).ToList()
                If relatedCombinedScoringMapKey.Count <> 1 Then
                    Continue For
                End If

                conceptScoringBrowser.CurrentScoringMapKey = relatedCombinedScoringMapKey.First()

                If Not combinedScoreMapsIncludedInConceptResponseLevelCounts.Contains(conceptScoringBrowser.CurrentScoringMapKey) Then
                    combinedScoreMapsIncludedInConceptResponseLevelCounts.Add(conceptScoringBrowser.CurrentScoringMapKey)

                    conceptResponseCountAtAttributeLevel += GetConceptResponseCount(conceptScoringBrowser, ConceptTypeIdRecordId.AttributeLevel)
                    conceptResponseCountAtSubAttributeLevel += GetConceptResponseCount(conceptScoringBrowser, ConceptTypeIdRecordId.SubAttributeLevel)
                End If

                Dim conceptsWithScore As List(Of IConceptScoringBrowserHierarchyPart) = conceptScoringBrowser.[Structure].Where(Function(s) s.PartName = conceptCode AndAlso s.IsSelected AndAlso s.ConceptScorePart IsNot Nothing AndAlso s.ConceptScorePart.Any(Function(cs) cs.IntScore.HasValue)).Distinct(New ConceptScoringBrowserHierarchyPartNameComparer()).ToList()
                If conceptsWithScore.Any() Then
                    For Each conceptWithScore As IConceptScoringBrowserHierarchyPart In conceptsWithScore
                        Dim newData As New ItemConceptScoringReportData()

                        newData.ItemCode = assessmentItem.Identifier
                        newData.ItemId = assessmentItem.ItemId
                        newData.ItemTitle = assessmentItem.Title
                        newData.Itemlayouttemplate = assessmentItem.LayoutTemplateSourceName
                        newData.ConceptCode = conceptWithScore.PartName
                        newData.ConceptResponseLabel = conceptResonseLabel

                        Dim keyValuesAndConceptsRelatedToCorrectAnswerColumnContent As New StringBuilder()
                        Dim keyValuesAndConceptsRelatedToAdditionalAnswerColumnContent As New StringBuilder()

                        For Each conceptScore As IConceptScoringBrowserScoreContainer In conceptWithScore.ConceptScorePart
                            If conceptScore.IntScore.HasValue Then
                                Dim scorableItemColumn As ScorableItemColumn = conceptScoringBrowser.ScorableItemColumns.First(Function(x) x.ConceptId = conceptScore.ConceptId)

                                Dim sbToUse As StringBuilder
                                If scorableItemColumn.IsRelatedToCorrectAnswer Then
                                    sbToUse = keyValuesAndConceptsRelatedToCorrectAnswerColumnContent
                                Else
                                    sbToUse = keyValuesAndConceptsRelatedToAdditionalAnswerColumnContent
                                End If

                                sbToUse.AppendFormat("[{0} / {1:00}]", scorableItemColumn.Caption, conceptScore.IntScore)
                            End If
                        Next

                        newData.KeyValuesAndConceptScores = keyValuesAndConceptsRelatedToCorrectAnswerColumnContent.ToString()
                        newData.AdditionalKeyValuesAndConceptScores = keyValuesAndConceptsRelatedToAdditionalAnswerColumnContent.ToString()

                        newData.IsGrouped = If((conceptScoringBrowser.CurrentScoringMapKey.IsGroup), My.Resources.Yes, My.Resources.No)
                        If conceptScoringBrowser.CurrentScoringMapKey.IsGroup Then
                            newData.GroupElementCount = conceptScoringBrowser.CurrentScoringMapKey.Count()
                        End If


                        result.Add(newData)
                    Next
                End If
            Next
        End Sub

        Private Shared Function GetConceptResponseCount(conceptScoringBrowser As ConceptScoringBrowser, level As ConceptTypeIdRecordId) As Integer
            Return conceptScoringBrowser.[Structure].Where(Function(x) x.Part.ConceptTypeId = CInt(level) AndAlso x.ConceptScorePart.Any(Function(y) y.IntScore.HasValue)).Distinct(New ConceptScoringBrowserHierarchyPartNameComparer()).Count()
        End Function

        Private Function GetPublicationConceptResponses(itemResourceId As Guid, publicationHandlerTypeName As String) As Dictionary(Of [String], Dictionary(Of [String], [String]))
            Dim pubServiceClient As IPublicationService = PublicationServiceFactory.Create()

            Dim conceptResponseEntries = pubServiceClient.GetConceptRelatedResponseProcessingForReportingPurposes(publicationHandlerTypeName, itemResourceId)

            Dim conceptResponseLabelsAndFirstFactsPerConceptCode As New Dictionary(Of [String], Dictionary(Of [String], [String]))()

            If conceptResponseEntries IsNot Nothing Then
                For Each conceptResponseEntry As ConceptProcessingLabelEntry In conceptResponseEntries
                    Dim conceptCode As String = conceptResponseEntry.ConceptCode
                    Dim conceptResponseLabel As String = conceptResponseEntry.ConceptResponseLabel
                    Dim idFirstFact As String = conceptResponseEntry.FactIdFirstFact

                    Dim conceptLabelDict As Dictionary(Of String, String)
                    If conceptResponseLabelsAndFirstFactsPerConceptCode.ContainsKey(conceptCode) Then
                        conceptLabelDict = conceptResponseLabelsAndFirstFactsPerConceptCode(conceptCode)
                    Else
                        conceptLabelDict = New Dictionary(Of String, String)()
                        conceptResponseLabelsAndFirstFactsPerConceptCode.Add(conceptCode, conceptLabelDict)
                    End If

                    If Not conceptLabelDict.Keys.Contains(conceptResponseLabel) Then
                        conceptLabelDict.Add(conceptResponseLabel, idFirstFact)
                    End If
                Next
            End If

            Return conceptResponseLabelsAndFirstFactsPerConceptCode
        End Function

        Private Class ConceptScoringBrowserHierarchyPartNameComparer
            Implements IEqualityComparer(Of IConceptScoringBrowserHierarchyPart)

            Public Overloads Function Equals(x As IConceptScoringBrowserHierarchyPart, y As IConceptScoringBrowserHierarchyPart) As Boolean Implements IEqualityComparer(Of IConceptScoringBrowserHierarchyPart).Equals
                Return x.PartName.Equals(y.PartName)
            End Function

            Public Overloads Function GetHashCode(obj As IConceptScoringBrowserHierarchyPart) As Integer Implements IEqualityComparer(Of IConceptScoringBrowserHierarchyPart).GetHashCode
                Return obj.PartName.GetHashCode()
            End Function
        End Class


        Private Function IConceptScoringBrowserDataProvider_PopulateConceptCustomBankPropertyHierarchy(id As Guid) As ConceptStructurePartCustomBankPropertyEntity Implements IConceptScoringBrowserDataProvider.PopulateConceptCustomBankPropertyHierarchy
            Return BankFactory.Instance.PopulateConceptCustomBankPropertyHierarchy(id)
        End Function

        Private Function IConceptScoringBrowserDataProvider_ReadConceptStructureCustomBankProperty(customBankPropertyId As Guid) As ConceptStructureCustomBankPropertyEntity Implements IConceptScoringBrowserDataProvider.ReadConceptStructureCustomBankProperty
            Return BankFactory.Instance.GetCustomBankPropertiesForBranchById(_bankId, ResourceTypeEnum.ItemResource).OfType(Of ConceptStructureCustomBankPropertyEntity)().FirstOrDefault(Function(c) c.CustomBankPropertyId = customBankPropertyId)
        End Function



        Private Function IConceptScoringBrowserObjectFactory_CreateHierarchyPart(conceptPart As ConceptStructurePartCustomBankPropertyEntity, parent As IConceptScoringBrowserHierarchyPart) As IConceptScoringBrowserHierarchyPart Implements IConceptScoringBrowserObjectFactory.CreateHierarchyPart
            Return New ConceptScoringReportHierarchyPart(conceptPart, parent)
        End Function

        Private Function IConceptScoringBrowserObjectFactory_CreatePartScoreContainer(conceptStructurePartTheScoreRelatesTo As IConceptScoringBrowserHierarchyPart, conceptId As String, score As Nullable(Of Integer), conceptScoreManipulator As IConceptScoreManipulator) As IConceptScoringBrowserScoreContainer Implements IConceptScoringBrowserObjectFactory.CreatePartScoreContainer
            Return New ConceptScoringReportHierarchyPartScoreContainer(conceptStructurePartTheScoreRelatesTo, conceptId, score)
        End Function


        Friend Property PublicationServiceFactory As IPublicationServiceFactory


        Friend Interface IPublicationServiceFactory
            Function Create() As IPublicationService
        End Interface

        Friend Class DefaultPublicationServiceFactory
            Implements IPublicationServiceFactory
            Public Function Create() As IPublicationService Implements IPublicationServiceFactory.Create
                Return New PublicationServiceClient()
            End Function
        End Class


    End Class
End Namespace
