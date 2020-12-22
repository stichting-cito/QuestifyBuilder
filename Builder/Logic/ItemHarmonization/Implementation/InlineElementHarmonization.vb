Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ItemHarmonization.Implementation.Base
Imports Cito.Tester.ContentModel


Namespace ItemHarmonization.Implementation
    Friend Class InlineElementHarmonization
        Inherits BaseHarmonization
        Public Overrides Function Harmonize(templates As IEnumerable(Of String), item As ItemResourceEntity) As Boolean
            Dim assessmentItem = item.GetAssessmentItem()
            Dim inlineTemplates = assessmentItem.GetInlineElements().[Select](Function(i) i.LayoutTemplateSourceName)
            Return MyBase.Harmonize(templates.Where(Function(t) inlineTemplates.Contains(t)), item)
        End Function

        Public Overrides Function Harmonize(assessmentItem As AssessmentItem, item As ItemResourceEntity) As Boolean
            Return Harmonize(assessmentItem, item, String.Empty)
        End Function

        Public Overrides Function Harmonize(assessmentItem As AssessmentItem, item As ItemResourceEntity, template As String) As Boolean
            Dim isChanged As Boolean = False
            For Each inlineElement As InlineElement In GetInlineElementsWithTemplateName(assessmentItem, template)
                Dim extractedParameterSets As ParameterSetCollection
                If Not (ParametersetCollections.TryGetValue(inlineElement.LayoutTemplateSourceName, extractedParameterSets)) Then
                    extractedParameterSets = GetParameterSetForTemplate(item.BankId, inlineElement.LayoutTemplateSourceName)
                    ParametersetCollections.TryAdd(inlineElement.LayoutTemplateSourceName, extractedParameterSets)
                End If

                If inlineElement.Parameters.AreEqual(extractedParameterSets) Then
                    Continue For
                End If

                Dim scoringParamsPreMerge As List(Of ScoringParameter) = inlineElement.Parameters.FlattenParameters().OfType(Of ScoringParameter)().ToList()

                inlineElement.Parameters.Merge(extractedParameterSets.DeepCloneWithDesignerSettingsAndAttributeReferences())

                Dim scoringParamsPostMerge As List(Of ScoringParameter) = inlineElement.Parameters.FlattenParameters().OfType(Of ScoringParameter)().ToList()

                scoringParamsPreMerge.ForEach(Sub(x)
                                                  Dim postMergeParam = scoringParamsPostMerge.FirstOrDefault(Function(y) y.Name = x.Name AndAlso y.ControllerId = x.ControllerId)
                                                  If postMergeParam IsNot Nothing Then
                                                      postMergeParam.FindingOverride = x.FindingOverride
                                                  End If
                                              End Sub)

                assessmentItem.ReplaceInlineElement(inlineElement)
                isChanged = True
            Next

            If isChanged Then
                item.SetAssessmentItem(assessmentItem)
            End If

            Return isChanged
        End Function

        Private Function GetInlineElementsWithTemplateName(assessmentItem As AssessmentItem, template as string) As IEnumerable(of InlineElement)
            If Not String.IsNullOrEmpty(template) Then
                Return assessmentItem.GetInlineElements().Where(Function(ie) ie.LayoutTemplateSourceName.Equals(template, StringComparison.InvariantCultureIgnoreCase))
            End If
            Return assessmentItem.GetInlineElements()
        End Function
    End Class
End Namespace
