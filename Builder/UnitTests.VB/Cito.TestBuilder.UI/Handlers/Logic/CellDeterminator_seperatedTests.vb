
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class CellDeterminator_seperatedTests
    Inherits baseFakedTable

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellLeft()
        'Arrange
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()
       
        'Act
        Dim result = sd.GetStyleLeft(cell)
        
        'Assert
        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellTop()
        'Arrange
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()
       
        'Act
        Dim result = sd.GetStyleTop(cell)
       
        'Assert
        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellRight()
        'Arrange
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()
        
        'Act
        Dim result = sd.GetStyleRight(cell)
        
        'Assert
        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOfSimpleCellBottom()
        'Arrange
        Dim tabel = MyBase.GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(tabel)
        Dim cell = tabel.GetCellByCoords(0, 0)
        Dim sd As New CellDeterminator_seperated()
       
        'Act
        Dim result = sd.GetStyleBottom(cell)
        
        'Assert
        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

End Class
