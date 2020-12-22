Imports Cito.Tester
Imports Cito.Tester.Common

Public Class CTContentSampleDataHelper

    Public Shared Function GetAssessmentTestModel() As ContentModel.AssessmentTest2
        Dim testModel As ContentModel.AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromByteArray(My.Resources.AssessmentTestModelv2, GetType(ContentModel.AssessmentTest2)), ContentModel.AssessmentTest2)
        Return testModel
    End Function

End Class
