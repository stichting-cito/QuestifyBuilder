Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Microsoft.Ajax.Utilities
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel
    Public Module AssessmentItemExtensions

        <Extension>
        Public Function GetResources(assessmentItem As AssessmentItem) As HashSet(Of String)
            Dim resources As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Dim otherResources As New HashSet(Of String)

            For Each p In assessmentItem.Parameters.GetParametersWithResources
                If TypeOf p Is XHtmlParameter Then
                    resources.UnionWith(DirectCast(p, XHtmlParameter).GetResources())
                ElseIf TypeOf p Is ResourceParameter Then
                    Dim r = DirectCast(p, ResourceParameter)

                    If r.Value IsNot Nothing Then
                        If Not String.IsNullOrEmpty(r.Value.Trim()) Then
                            resources.UnionWith(New String() {DirectCast(p, ResourceParameter).Value})
                        End If
                    End If
                End If
            Next

            otherResources.Add(assessmentItem.LayoutTemplateSourceName)

            If assessmentItem.Solution IsNot Nothing Then
                If assessmentItem.Solution.AspectReferenceSetCollection IsNot Nothing Then
                    assessmentItem.Solution.AspectReferenceSetCollection.ForEach(Sub(ac) ac.Items.ForEach(Sub(a) otherResources.UnionWith(a.GetResources)))
                End If
            End If

            resources.UnionWith(otherResources)

            Return resources
        End Function

        <Extension>
        Public Function ValidateResources(ByVal item As AssessmentItem, ByVal bankId As Integer, ByRef missingResourcesMessage As String) As Boolean
            Dim missingResources As New HashSet(Of String)

            Dim resources = item.GetResources()
            For Each resource In resources
                If Not ResourceFactory.Instance.ResourceExists(bankId, resource, true) Then
                    If Not missingResources.Contains(resource) Then missingResources.Add(resource)
                End If
            Next

            missingResourcesMessage = If(missingResources.Count > 0,
                                         String.Format(My.Resources.ResourcesCouldNotBeFound, String.Join(vbCrLf + "- ", missingResources)),
                                         String.Empty)

            Return missingResources.Count = 0
        End Function

        <Extension>
        Public Sub ReplaceInlineImages(assessmentItem As AssessmentItem, itemResource As ItemResourceEntity)
            assessmentItem.Parameters.GetParametersWithResources.OfType(Of XHtmlParameter).ForEach(Sub(p)
                                                                                                       p.ReplaceInlineImages(itemResource.BankId, String.Empty, itemResource.Name)
                                                                                                   End Sub)
        End Sub

        <Extension>
        Public Sub SetScorePropertiesForItem(assessmentItem As AssessmentItem, itemResource As ItemResourceEntity)
            Dim scoreParameters = assessmentItem.Parameters.DeepFetchInlineScoringParameters()
            If (scoreParameters.Any()) Then
                Dim valueToSet = New ScoringDisplayValueCalculator(scoreParameters, assessmentItem.Solution).GetScoreDisplayValue()

                itemResource.KeyValues = valueToSet.TruncateWithEllipsis(500)
                itemResource.ResponseCount = ScoringPropertiesCalculator.GetResponseCount(assessmentItem.Solution)
                itemResource.AlternativesCount = ScoringPropertiesCalculator.GetAlternativesCount(scoreParameters)
            Else
                itemResource.KeyValues = ScoringPropertiesCalculator.GetKeyValuesAsString(assessmentItem.Solution, itemResource.BankId)
                itemResource.ResponseCount = Nothing
                itemResource.AlternativesCount = Nothing
            End If

            itemResource.RawScore = ScoringPropertiesCalculator.GetRawScore(assessmentItem.Solution)
            itemResource.MaxScore = ScoringPropertiesCalculator.GetMaxScore(assessmentItem.Solution)
        End Sub

        <Extension>
        Public Function GetInlineElements(assessmentItem As AssessmentItem) As IEnumerable(Of InlineElement)
            Dim xhtmlParameters As List(Of XHtmlParameter) = assessmentItem.Parameters.FlattenParameters.OfType(Of XHtmlParameter).ToList
            If assessmentItem.Solution IsNot Nothing AndAlso assessmentItem.Solution.AspectReferenceSetCollection IsNot Nothing Then
                For Each aspectRef In assessmentItem.Solution.AspectReferenceSetCollection
                    xhtmlParameters.AddRange(From aspectReference In aspectRef.Items Select New XHtmlParameter With {.Value = aspectReference.Description})
                Next
            End If
            Dim returnList As New List(Of InlineElement)
            For Each xhtmlParameter In xhtmlParameters
                returnList.AddRange(xhtmlParameter.GetInlineElements.Values)
            Next
            Return returnList
        End Function

        <Extension>
        Public Function ReplaceInlineElement(assessmentItem As AssessmentItem, inlineElement As InlineElement) As Boolean
            Return assessmentItem.ReplaceInlineElement(inlineElement.Identifier, inlineElement)
        End Function

        <Extension>
        Public Function ReplaceInlineElement(assessmentItem As AssessmentItem, inlineElementId As String, inlineElement As InlineElement) As Boolean
            Dim xhtmlParameters As List(Of XHtmlParameter) = assessmentItem.Parameters.FlattenParameters.OfType(Of XHtmlParameter).ToList

            Dim isReplaced = xhtmlParameters.Any(Function(xhtmlParameter)
                                                     Dim xHtmlInlineElementsManipulator = New XHtmlInlineElementsManipulator(xhtmlParameter)
                                                     Return xHtmlInlineElementsManipulator.ReplaceInlineElement(inlineElementId, inlineElement)
                                                 End Function)
            If Not isReplaced Then
                If assessmentItem.Solution IsNot Nothing AndAlso assessmentItem.Solution.AspectReferenceSetCollection IsNot Nothing Then
                    For Each aspectRef In assessmentItem.Solution.AspectReferenceSetCollection
                        For Each aspectReference In aspectRef.Items
                            Dim aspectXhtmlParameter As New XHtmlParameter With {.Value = aspectReference.Description}
                            Dim xHtmlInlineElementsManipulator = New XHtmlInlineElementsManipulator(aspectXhtmlParameter)
                            isReplaced = xHtmlInlineElementsManipulator.ReplaceInlineElement(inlineElementId, inlineElement)
                            aspectReference.Description = aspectXhtmlParameter.Value
                        Next
                    Next
                End If
            End If
            Return isReplaced
        End Function

    End Module

End Namespace


