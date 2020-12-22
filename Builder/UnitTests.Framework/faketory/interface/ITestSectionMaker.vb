Imports Cito.Tester.ContentModel

Namespace Faketory.interface

    Public Interface ITestSectionMaker

        Function MakeSection(name As String, postAction As Action(Of TestSection2), ByVal ParamArray constructor() As Func(Of ISectionOrItemMaker, TestComponent2)) As TestSection2

        Function MakeSection(name As String, ByVal ParamArray constructor() As Func(Of ISectionOrItemMaker, TestComponent2)) As TestSection2

    End Interface
End NameSpace