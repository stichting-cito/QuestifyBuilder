Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl

    Friend Class TestSectionMaker
        Implements ITestSectionMaker

        Private _parent As AssessmentTestNode

        Sub New(parent As AssessmentTestNode)
            _parent = parent
        End Sub

        Friend Function MakeSection(name As String, postAction As Action(Of TestSection2),
                                    ParamArray constructed() As Func(Of ISectionOrItemMaker, TestComponent2)) As TestSection2 Implements ITestSectionMaker.MakeSection

            Dim ret As New TestSection2 With {.Identifier = name, .Title = name, .Parent = _parent}

            If Not postAction Is Nothing Then postAction(ret)

            ret.Components.AddRange(From e In constructed Select e(New SectionOrItemMaker(ret)))

            Return ret
        End Function

        Friend Function MakeSection(name As String, ByVal ParamArray constructor() As Func(Of ISectionOrItemMaker, TestComponent2)) As TestSection2 Implements ITestSectionMaker.MakeSection
            Return Me.MakeSection(name, Nothing, constructor)
        End Function

    End Class
End NameSpace