
Imports System.Xml
Imports FakeItEasy
Imports FakeItEasy.Configuration
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableBorderStyleWorkerTests
    Inherits TableBoundsTest

    Private Shared marker As CssBorder = New CssBorder("") With {.BorderStyle = "dotted", .Width = 4, .Color = Drawing.Color.Black}
    Private Shared normal As CssBorder = New CssBorder("") With {.BorderStyle = "solid", .Width = 1, .Color = Drawing.Color.Black}

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SingleCellHasStyleSet()
        Dim fakeStyleSetter = A.Fake(Of ISetTableBorderStyleStrategy)()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = GetTableSimple(ns)
        Dim style = TableStyleDto.SingleCell()
        style.Box(LineStyle.Dotted, 4)
        style.Inner(LineStyle.Solid, 1)
        Dim worker As New TableBorderStyleWorker(table)

        worker.setStyleFor(New TableBounds(0, 0), style, fakeStyleSetter)

        HasSet_LeftBorderStyle(fakeStyleSetter, marker, "1").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, marker, "1").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, marker, "1").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, marker, "1").MustHaveHappened()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SingleCellHasBackcolorSet()
        Dim fakeStyleSetter = A.Fake(Of ISetTableBorderStyleStrategy)()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = GetTableSimple(ns)
        Dim style = TableStyleDto.SingleCell()
        style.BackColor = Drawing.Color.Black

        Dim worker As New TableBorderStyleWorker(table)

        worker.setStyleFor(New TableBounds(0, 0), style, fakeStyleSetter)

        HasSet_BackcolorStyle(fakeStyleSetter, Drawing.Color.Black, "1")
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub TwoCellsOn1RowHasStyleSet()
        Dim fakeStyleSetter = A.Fake(Of ISetTableBorderStyleStrategy)()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = GetTableSimple(ns)
        Dim style = TableStyleDto.Column()
        style.Box(LineStyle.Dotted, 4)
        style.Inner(LineStyle.Solid, 1)
        Dim worker As New TableBorderStyleWorker(table)

        worker.setStyleFor(New TableBounds(1, 1, 2, 1), style, fakeStyleSetter)

        HasSet_LeftBorderStyle(fakeStyleSetter, marker, "6").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, marker, "6").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, normal, "6").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, marker, "6").MustHaveHappened()
        HasSet_LeftBorderStyle(fakeStyleSetter, normal, "7").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, marker, "7").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, marker, "7").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, marker, "7").MustHaveHappened()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub TwoCellsOn1ColumHasStyleSet()
        Dim fakeStyleSetter = A.Fake(Of ISetTableBorderStyleStrategy)()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = GetTableSimple(ns)
        Dim style = TableStyleDto.Row()
        style.Box(LineStyle.Dotted, 4)
        style.Inner(LineStyle.Solid, 1)
        Dim worker As New TableBorderStyleWorker(table)

        worker.setStyleFor(New TableBounds(0, 1, 1, 2), style, fakeStyleSetter)

        HasSet_LeftBorderStyle(fakeStyleSetter, marker, "5").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, marker, "5").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, marker, "5").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, normal, "5").MustHaveHappened()
        HasSet_LeftBorderStyle(fakeStyleSetter, marker, "9").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, normal, "9").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, marker, "9").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, marker, "9").MustHaveHappened()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetStyleOn4Cells_2x2_allHaveStyle()
        Dim fakeStyleSetter = A.Fake(Of ISetTableBorderStyleStrategy)()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = GetTableSimple(ns)
        Dim style = TableStyleDto.ColAndRow()
        style.Box(LineStyle.Dotted, 4)
        style.Inner(LineStyle.Solid, 1)
        Dim worker As New TableBorderStyleWorker(table)

        worker.setStyleFor(New TableBounds(1, 1, 2, 2), style, fakeStyleSetter)

        HasSet_LeftBorderStyle(fakeStyleSetter, marker, "6").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, marker, "6").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, normal, "6").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, normal, "6").MustHaveHappened()
        HasSet_LeftBorderStyle(fakeStyleSetter, normal, "7").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, marker, "7").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, marker, "7").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, normal, "7").MustHaveHappened()
        HasSet_LeftBorderStyle(fakeStyleSetter, marker, "10").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, normal, "10").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, normal, "10").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, marker, "10").MustHaveHappened()
        HasSet_LeftBorderStyle(fakeStyleSetter, normal, "11").MustHaveHappened()
        HasSet_TopBorderStyle(fakeStyleSetter, normal, "11").MustHaveHappened()
        HasSet_RightBorderStyle(fakeStyleSetter, marker, "11").MustHaveHappened()
        HasSet_BottomBorderStyle(fakeStyleSetter, marker, "11").MustHaveHappened()

    End Sub

    Private Function HasSet_BackcolorStyle(fake As ISetTableBorderStyleStrategy, c As System.Drawing.Color?, txt As String) As IVoidArgumentValidationConfiguration
        Return A.CallTo(Sub() fake.SetBackgroundColor(A(Of System.Drawing.Color?).Ignored,
                     A(Of TableCell).That.Matches(Function(t As TableCell) t.InnerText = txt)))
    End Function
    Private Function HasSet_LeftBorderStyle(fake As ISetTableBorderStyleStrategy, style As CssBorder, txt As String) As IVoidArgumentValidationConfiguration
        Return A.CallTo(Sub() fake.SetLeft_BorderStyle(
                     A(Of CssBorder).That.IsEqualTo(style),
                     A(Of TableCell).That.Matches(Function(t As TableCell) t.InnerText = txt),
                     A(Of Boolean).Ignored))
    End Function

    Private Function HasSet_TopBorderStyle(fake As ISetTableBorderStyleStrategy, style As CssBorder, txt As String) As IVoidArgumentValidationConfiguration
        Return A.CallTo(Sub() fake.SetTop_BorderStyle(
                     A(Of CssBorder).That.IsEqualTo(style),
                     A(Of TableCell).That.Matches(Function(t As TableCell) t.InnerText = txt),
                     A(Of Boolean).Ignored))
    End Function

    Private Function HasSet_RightBorderStyle(fake As ISetTableBorderStyleStrategy, style As CssBorder, txt As String) As IVoidArgumentValidationConfiguration
        Return A.CallTo(Sub() fake.SetRight_BorderStyle(
                     A(Of CssBorder).That.IsEqualTo(style),
                     A(Of TableCell).That.Matches(Function(t As TableCell) t.InnerText = txt),
                     A(Of Boolean).Ignored))
    End Function


    Private Function HasSet_BottomBorderStyle(fake As ISetTableBorderStyleStrategy, style As CssBorder, txt As String) As IVoidArgumentValidationConfiguration
        Return A.CallTo(Sub() fake.SetBottom_BorderStyle(
                     A(Of CssBorder).That.IsEqualTo(style),
                     A(Of TableCell).That.Matches(Function(t As TableCell) t.InnerText = txt),
                     A(Of Boolean).Ignored))
    End Function


End Class
