Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl

    Friend Class SectionOrItemMaker
        Implements ISectionOrItemMaker

        Private _parent As AssessmentTestNode

        Sub New(parent As AssessmentTestNode)
            _parent = parent
        End Sub

        Public Function MakeItem(name As String, postAction As Action(Of ItemReference2)) As ItemReference2 Implements ISectionOrItemMaker.MakeItem
            Dim ret As New ItemReference2 With {.Identifier = name, .Title = name, .SourceName = name, .Parent = _parent}
            If Not postAction Is Nothing Then postAction(ret)
            Return ret
        End Function

        Public Function MakeItem(name As String) As ItemReference2 Implements ISectionOrItemMaker.MakeItem
            Return MakeItem(name, Nothing)
        End Function

        Public Function MakeSection(name As String, ParamArray constructed() As Func(Of ISectionOrItemMaker, TestComponent2)) As TestSection2 Implements ISectionOrItemMaker.MakeSection
            Return New TestSectionMaker(_parent).MakeSection(name, Nothing, constructed)
        End Function
    End Class
End NameSpace