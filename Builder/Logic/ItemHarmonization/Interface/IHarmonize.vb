Imports System.Collections.Concurrent
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel

Namespace ItemHarmonization.Interface
    Friend Interface IHarmonize
        Function Harmonize(item As ItemResourceEntity) As Boolean
        Function Harmonize(assementItem As AssessmentItem, item As ItemResourceEntity) As Boolean
        Function Harmonize(assementItem As AssessmentItem, item As ItemResourceEntity, template As String) As Boolean
        Function Harmonize(templates As IEnumerable(Of String), item As ItemResourceEntity) As Boolean
        Function Harmonize(parametersetCollections As ConcurrentDictionary(Of String, ParameterSetCollection), item As ItemResourceEntity) As Boolean
    End Interface
End Namespace
