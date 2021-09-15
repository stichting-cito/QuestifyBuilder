
Imports System.Linq
Imports Cito.Tester.Common.Controls.Canvas
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports System.Drawing

<TestClass()>
Public Class CreateToolTests
    Inherits baseToolTests

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleSuggestPoint()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
        
        'Act
        MoveMouseTo(10, 10, c) 'Will Suggest point 1st
       
        'Assert
        Assert.AreEqual(0, added + removed + replace) 'No events fired!
        Assert.IsNotNull(c.EditItem) 'Edited item
        Assert.AreEqual(0, c.Items.Count) 'Items is edited thus this one is empty, this is so redrawing is index based.
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickAbdSuggestPoint_NoEventsFired()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
       
        'Act
        MouseClick(10, 10, c) 'Add firstPoint
        MoveMouseTo(10, 10, c) 'Will Suggest point 1st
       
        'Assert
        Assert.AreEqual(0, added + removed + replace) 'No events fired!
        Assert.IsNotNull(c.EditItem) 'Edited item
        Assert.AreEqual(0, c.Items.Count) 'Items is edited thus this one is empty, this is so redrawing is index based.
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleSuggestPointAnchorPointEqualsPoint()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
       
        'Act
        MoveMouseTo(10, 10, c) 'Will Suggest point 1st
        
        'Assert
        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickOnce_WithoutSuggestPoint_AnchorIsSet()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
       
        'Act
        MouseClick(10, 10, c)
      
        'Assert
        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickOnce_SuggestPoint_AreaIsNotSetToExpectedDimensions()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas With {.BackgroundImage = New Bitmap(100,100)}, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
     
        'Act
        MouseClick(10, 10, c)

        'Will Suggest 2nd Point. but because rectangle grows by 20px in height, the shape will fall outside the canvas (y = -10)
        'And therefor, shape will not be resized
        MoveMouseTo(20, 30, c) 
      
        'Assert
        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
        Dim r = DirectCast(c.EditItem, IRectangle)
        Assert.AreEqual(0, r.Width) 
        Assert.AreEqual(0, r.Height)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickOnce_SuggestPoint_AreaIsSetToExpectedDimensions()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas With {.BackgroundImage = New Bitmap(100,100)}, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
     
        'Act
        MouseClick(25, 25, c)
        MoveMouseTo(35, 45, c) 'Will Suggest 2nd Point 
      
        'Assert
        Assert.AreEqual(New Point(25, 25), c.EditItem.AnchorPoint)
        Dim r = DirectCast(c.EditItem, IRectangle)
        Assert.AreEqual(20, r.Width) '(mid x - suggested = 10; thus width is 20)
        Assert.AreEqual(40, r.Height) '(mid y - suggested = 20; thus height is 40)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickTwice_EditedObjectIsStillSet()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
       
        'Act
        MouseClick(10, 10, c)
        MouseClick(20, 30, c) 'Will Suggest 2nd Point 
       
        'Assert
        Assert.IsNotNull(c.EditItem)
        Assert.AreEqual(1, added) 'But has been added
        Assert.AreEqual(0, replace) 'Nothing Else
        Assert.AreEqual(0, removed) 'Nothing Else
        Assert.AreEqual(0, c.Items.Count) 'Item has been created but not added to list.
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CreateRectangleClickTwice_EditedObjectHasExpectedDimensions()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas) : AddHandler c.CollectionChanged, TrackAndResetCounters()
        c.Tool = CreateRectangle()
       
        'Act
        MouseClick(10, 10, c)
        MouseClick(20, 30, c) 'Will Suggest 2nd Point 
     
        'Assert
        Assert.AreEqual(1, added) 'But has been added
        Assert.AreEqual(New Point(10, 10), c.EditItem.AnchorPoint)
        Dim r = DirectCast(c.EditItem, IRectangle)
        Assert.AreEqual(20, r.Width) '(mid x - suggested = 10; thus width is 20)
        Assert.AreEqual(40, r.Height) '(mid y - suggested = 20; thus width is 40)
    End Sub

End Class
