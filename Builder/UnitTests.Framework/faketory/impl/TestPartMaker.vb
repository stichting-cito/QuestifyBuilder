Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl

    Friend Class TestPartMaker
        Implements ITestPartMaker

        Function Make(name As String, ParamArray constructed() As Func(Of ITestSectionMaker, TestSection2)) As TestPart2 Implements ITestPartMaker.MakeTestPart
            Dim ret As New TestPart2 With {.Identifier = name, .Title = name}

            If (Not constructed Is Nothing AndAlso constructed.Length > 0) Then
                ret.Sections.AddRange(From e In constructed Select e(New TestSectionMaker(ret)))
            End If

            Return ret
        End Function

    End Class
End NameSpace