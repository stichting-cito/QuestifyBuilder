
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl

    Friend Class AssessmentTestMaker
        Implements IAssesmentTestMaker

        Sub New()
        End Sub

        Friend Function MakeAssessment(name As String, ParamArray constructed() As Func(Of ITestPartMaker, TestPart2)) As AssessmentTest2 Implements IAssesmentTestMaker.MakeAssessment
            Dim ret As New AssessmentTest2 With {.Identifier = name, .Title = name}

            If (Not constructed Is Nothing AndAlso constructed.Length > 0) Then
                ret.TestParts.AddRange(From e In constructed Select e(New TestPartMaker()))
            End If

            Return ret
        End Function

        Function MakeAssessment(ByVal ParamArray constructor() As Func(Of ITestSectionMaker, TestSection2)) As AssessmentTest2 Implements IAssesmentTestMaker.MakeAssessment
            Return MakeAssessment("Test", Function(tp) tp.MakeTestPart("tp1", constructor))
        End Function

    End Class
End NameSpace