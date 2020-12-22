Imports Cito.Tester.ContentModel

Namespace Faketory.interface

    Public Interface IAssesmentTestMaker

        Function MakeAssessment(name As String, ByVal ParamArray constructor() As Func(Of ITestPartMaker, TestPart2)) As AssessmentTest2

        Function MakeAssessment(ByVal ParamArray constructor() As Func(Of ITestSectionMaker, TestSection2)) As AssessmentTest2
    End Interface

End NameSpace