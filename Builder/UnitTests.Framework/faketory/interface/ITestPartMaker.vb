Imports Cito.Tester.ContentModel

Namespace Faketory.interface

    Public Interface ITestPartMaker

        Function MakeTestPart(name As String, ByVal ParamArray constructor() As Func(Of ITestSectionMaker, TestSection2)) As TestPart2

    End Interface
End NameSpace