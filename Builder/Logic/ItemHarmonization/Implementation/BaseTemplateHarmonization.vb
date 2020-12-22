Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ItemHarmonization.Implementation.Base
Imports Cito.Tester.ContentModel


Namespace ItemHarmonization.Implementation
    Friend Class BaseTemplateHarmonization
        Inherits BaseHarmonization
        Public Overrides Function Harmonize(templates As IEnumerable(Of String), item As ItemResourceEntity) As Boolean
            Dim assessmentItem As AssessmentItem = item.GetAssessmentItem()
            Return MyBase.Harmonize(templates.Where(Function(t) t = assessmentItem.LayoutTemplateSourceName), item)
        End Function

        Public Overrides Function Harmonize(assessmentItem As AssessmentItem, item As ItemResourceEntity, template As String) As Boolean
            Return Harmonize(assessmentItem, item)
        End Function

        Public Overrides Function Harmonize(assessmentItem As AssessmentItem, item As ItemResourceEntity) As Boolean
            Dim extractedParameterSets As ParameterSetCollection
            If Not (ParametersetCollections.TryGetValue(assessmentItem.LayoutTemplateSourceName, extractedParameterSets)) Then
                extractedParameterSets = GetParameterSetForTemplate(item.BankId, assessmentItem.LayoutTemplateSourceName)
                ParametersetCollections.TryAdd(assessmentItem.LayoutTemplateSourceName, extractedParameterSets)
            End If
            If assessmentItem.Parameters.AreEqual(extractedParameterSets) Then
                Return False
            End If
            assessmentItem.Parameters.Merge(extractedParameterSets.DeepCloneWithDesignerSettingsAndAttributeReferences())
            item.SetAssessmentItem(assessmentItem)
            Return True
        End Function
    End Class
End Namespace

