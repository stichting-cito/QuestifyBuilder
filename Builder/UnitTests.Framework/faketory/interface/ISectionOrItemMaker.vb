Imports Cito.Tester.ContentModel

Namespace Faketory.interface

    Public Interface ISectionOrItemMaker

        Function MakeItem(name As String) As ItemReference2

        Function MakeItem(name As String, postAction As Action(Of ItemReference2)) As ItemReference2

        Function MakeSection(name As String, ByVal ParamArray constructor() As Func(Of ISectionOrItemMaker, TestComponent2)) As TestSection2

    End Interface
End NameSpace