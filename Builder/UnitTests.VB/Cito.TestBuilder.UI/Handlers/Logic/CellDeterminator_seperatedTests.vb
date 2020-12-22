
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class CellDeterminator_seperatedTests
    Inherits baseFakedTable

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellLeft()
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()

        Dim result = sd.GetStyleLeft(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellTop()
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()

        Dim result = sd.GetStyleTop(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellRight()
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()

        Dim result = sd.GetStyleRight(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellBottom()
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()

        Dim result = sd.GetStyleBottom(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

End Class
