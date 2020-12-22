
Imports System.Linq
Imports Cito.Tester.Common.Controls.Canvas
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports System.Drawing

<TestClass()>
Public Class CreateToolTests
    Inherits baseToolTests

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleSuggestPoint()
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MoveMouseTo(10, 10, c)

        Assert.AreEqual(0, added + removed + replace)
        Assert.IsNotNull(c.EditItem)
        Assert.AreEqual(0, c.Items.Count)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickAbdSuggestPoint_NoEventsFired()
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MouseClick(10, 10, c)
        MoveMouseTo(10, 10, c)

        Assert.AreEqual(0, added + removed + replace)
        Assert.IsNotNull(c.EditItem)
        Assert.AreEqual(0, c.Items.Count)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleSuggestPointAnchorPointEqualsPoint()
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MoveMouseTo(10, 10, c)

        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickOnce_WithoutSuggestPoint_AnchorIsSet()
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MouseClick(10, 10, c)

        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickOnce_SuggestPoint_AreaIsNotSetToExpectedDimensions()
        Dim c As ICanvas = DirectCast(New Canvas With {.BackgroundImage = New Bitmap(100, 100)}, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MouseClick(10, 10, c)

        MoveMouseTo(20, 30, c)

        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
        Dim r = DirectCast(c.EditItem, IRectangle)
        Assert.AreEqual(0, r.Width)
        Assert.AreEqual(0, r.Height)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickOnce_SuggestPoint_AreaIsSetToExpectedDimensions()
        Dim c As ICanvas = DirectCast(New Canvas With {.BackgroundImage = New Bitmap(100, 100)}, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MouseClick(25, 25, c)
        MoveMouseTo(35, 45, c)

        Assert.AreEqual(New Point(25, 25), c.EditItem.AnchorPoint)
        Dim r = DirectCast(c.EditItem, IRectangle)
        Assert.AreEqual(20, r.Width)
        Assert.AreEqual(40, r.Height)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickTwice_EditedObjectIsStillSet()
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MouseClick(10, 10, c)
        MouseClick(20, 30, c)

        Assert.IsNotNull(c.EditItem)
        Assert.AreEqual(1, added)
        Assert.AreEqual(0, replace)
        Assert.AreEqual(0, removed)
        Assert.AreEqual(0, c.Items.Count)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickTwice_EditedObjectHasExpectedDimensions()
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()

        MouseClick(10, 10, c)
        MouseClick(20, 30, c)

        Assert.AreEqual(1, added)
        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
        Dim r = DirectCast(c.EditItem, IRectangle)
        Assert.AreEqual(20, r.Width)
        Assert.AreEqual(40, r.Height)
    End Sub

End Class
