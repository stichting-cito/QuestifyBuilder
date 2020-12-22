Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Namespace ContentModel
    Public Module AssessmentTestDtoExtensions

        <Extension>
        Public Function GetAssessmentTest(test As AssessmentTestResourceDto) As AssessmentTest2
            Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceDataByResourceId(test.resourceId)
            Return AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(data.BinData, True).AssessmentTestv2
        End Function

        <Extension>
        Public Function GetAssessmentView(Of T As AssessmentTestViewBase)(ByVal test As AssessmentTestResourceDto, assessmentType As String) As T
            Dim assessmentTest = test.GetAssessmentTest
            If Not assessmentTest.IncludedViews.Contains(assessmentType) Then Return Nothing
            Return TryCast(AssessmentTestv2Factory.CreateView(assessmentTest, assessmentType), T)
        End Function

    End Module
End Namespace