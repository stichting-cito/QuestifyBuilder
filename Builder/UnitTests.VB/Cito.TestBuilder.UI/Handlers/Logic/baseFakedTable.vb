
Imports FakeItEasy
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports System.Drawing

Public Class baseFakedTable

    Friend Function GetFakeTable() As Table
        Return A.Fake(Of Table)()
    End Function

    Friend Function GetFakeTable(columns As Integer, rows As Integer) As Table
        Dim ret = A.Fake(Of Table)()
        A.CallTo(Function() ret.GetColumnCount()).ReturnsLazily(Function() columns)
        A.CallTo(Function() ret.GetRowCount()).ReturnsLazily(Function() rows)
        Return ret
    End Function

    Friend Function GetFakeTableCell(owner As Table, cssStyleList As CssStyleList) As TableCell
        Dim ret = A.Fake(Of TableCell)(Sub(o)
                                           o.WithArgumentsForConstructor(New Object() {owner, cssStyleList})
                                       End Sub)
        ret.ColSpan = 1
        ret.RowSpan = 1
        A.CallTo(Sub() ret.ApplyStyles()).DoesNothing()
        Return ret
    End Function

    Friend Sub SetUpEachSideIsOverridable(fakeTabel As Table, overrideStyle As Action(Of CssStyleList, Integer, Integer))
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).ReturnsLazily(Function(arg) TabelCelWithSolidBorder(fakeTabel, arg.GetArgument(Of Integer)(0), arg.GetArgument(Of Integer)(1), overrideStyle))
    End Sub

    Friend Sub SetUpEachSideAndSizeIsOverridable(fakeTabel As Table, dict As Dictionary(Of Point, TableCell), overrideStyle As Action(Of CssStyleList, Integer, Integer), postaction As Func(Of Integer, Integer, Dictionary(Of Point, TableCell), TableCell, TableCell))
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).ReturnsLazily(
            Function(arg)
                Return TabelCelWithSolidBorder(fakeTabel,
                                        arg.GetArgument(Of Integer)(0),
                                        arg.GetArgument(Of Integer)(1),
                                        dict,
                                        overrideStyle,
                                        postaction)
            End Function)
    End Sub

    Friend Function TabelCelWithSolidBorder(owner As Table, x As Integer, y As Integer, dict As Dictionary(Of Point, TableCell),
                                            overrideStyle As Action(Of CssStyleList, Integer, Integer),
                                            postaction As Func(Of Integer, Integer, Dictionary(Of Point, TableCell), TableCell, TableCell)) As TableCell
        Dim style = GetStyleList(Function() BlackSolid1px()) 'Default
        overrideStyle(style, x, y)
        Dim ret = GetFakeTableCell(owner, style)
        ret.ColNumber = x
        ret.RowNumber = y
        ret.ColSpan = 1
        ret.RowSpan = 1
        Return postaction(x, y, dict, ret)
    End Function

    Friend Function TabelCelWithSolidBorder(owner As Table, x As Integer, y As Integer, overrideStyle As Action(Of CssStyleList, Integer, Integer)) As TableCell

        Dim style = GetStyleList(Function() BlackSolid1px()) 'Default
        overrideStyle(style, x, y)

        Dim ret = GetFakeTableCell(owner, style)
        ret.ColNumber = x
        ret.RowNumber = y
        Return ret
    End Function

    Friend Sub SetUpForEachCellSolidBorder(fakeTabel As Table)
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).ReturnsLazily(Function(arg) TabelCelWithSolidBorder(fakeTabel, arg.GetArgument(Of Integer)(0), arg.GetArgument(Of Integer)(1)))
    End Sub

    Friend Function TabelCelWithSolidBorder(owner As Table, x As Integer, y As Integer) As TableCell
        Dim ret = GetFakeTableCell(owner, GetStyleList(Function() BlackSolid1px()))
        ret.ColNumber = x
        ret.RowNumber = y
        Return ret
    End Function

    Friend Sub SetUpForEachCellDashedBorder(fakeTabel As Table)
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).ReturnsLazily(Function(arg) TabelCelWithDashedBorder(fakeTabel, arg.GetArgument(Of Integer)(0), arg.GetArgument(Of Integer)(1)))
    End Sub

    Friend Function TabelCelWithDashedBorder(owner As Table, x As Integer, y As Integer) As TableCell
        Dim ret = GetFakeTableCell(owner, GetStyleList(Function() BlackDashed1px()))
        ret.ColNumber = x
        ret.RowNumber = y
        Return ret
    End Function

    Friend Function GetStyleList(getStyle As Func(Of CssBorder)) As CssStyleList
        Dim ret As New CssStyleList()
        ret.BorderLeft = getStyle()
        ret.BorderTop = getStyle()
        ret.BorderRight = getStyle()
        ret.BorderBottom = getStyle()
        Return ret
    End Function

    Friend Function BlackSolid1px() As CssBorder
        Return New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 1}
    End Function

    Friend Function RedSolid1px() As CssBorder
        Return New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Red, .Width = 1}
    End Function

    Friend Function BlackDashed1px() As CssBorder
        Return New CssBorder() With {.BorderStyle = "dashed", .Color = Drawing.Color.Black, .Width = 1}
    End Function

    Friend Function MARKER_DASHED_RED_3PX() As CssBorder
        Return New CssBorder() With {.BorderStyle = "Dashed", .Color = Drawing.Color.Red, .Width = 3}
    End Function
End Class
