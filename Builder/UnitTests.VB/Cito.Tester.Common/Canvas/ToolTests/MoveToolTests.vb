
Imports System.Drawing
Imports System.Linq
Imports Cito.Tester.Common.Controls.Canvas
Imports System.Windows.Forms
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory

<TestClass()>
Public Class MoveToolTests

    Private added, removed, replace As Integer

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SelectDeselect_ItmChanged()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas)
        Dim itm As IDrawableItem = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        c.AddItem(itm)

        AddHandler c.CollectionChanged, TrackAndResetCounters()

        'Act
        c.Select(itm)
        c.DeSelect()
       
        'Assert
        Assert.AreEqual(0, added)
        Assert.AreEqual(0, removed)
        Assert.AreEqual(1, replace)
    End Sub

    <TestMethod()> <TestCategory("Controls")> <WorkItem(9858)>
    Public Sub MoveItem_ItmChanged()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas)

        Dim shape As IRectangle = CreateRectangle(0, 0, 10, 10)
        c.AddItem(shape)

        AddHandler c.CollectionChanged, TrackAndResetCounters()
      
        'Act
        c.Tool.MouseDown(c, New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0))
        c.Tool.MouseMove(c, New MouseEventArgs(MouseButtons.Left, 0, 100, 100, 0))
        c.Tool.MouseUp(c, New MouseEventArgs(MouseButtons.Left, 0, 100, 100, 0))

        'Assert
        Assert.AreEqual(0, added)
        Assert.AreEqual(0, removed)
        Assert.AreEqual(1, replace) 'MouseUp on tool should replace the item on collection
        Assert.AreEqual(0, c.Items.Count()) 'Item is being edited thus not in collection.
    End Sub

    <TestMethod()> <TestCategory("Controls")> <WorkItem(9858)>
    Public Sub MoveItemRelease_ItmChanged()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas)
        Dim drawingList As New List(Of IDrawableItem)
        Dim itm As IDrawableItem = New DefaultShapeFactory().CreateShape(Of IRectangle)()

        Dim shape As IRectangle = DirectCast(itm, IRectangle)
        shape.Left = 0 : shape.Right = 10 : shape.Top = 0 : shape.Bottom = 10

        c.AddItem(itm)
        AddHandler c.CollectionChanged, Sub(s As Object, e As NotifyCollectionChangedEventArgs)
                                            Select Case e.Action
                                                Case NotifyCollectionChangedAction.Add
                                                    added += 1
                                                Case NotifyCollectionChangedAction.Remove
                                                    removed += 1
                                                Case NotifyCollectionChangedAction.Replace
                                                    Dim old = DirectCast(e.OldItems(0), IDrawableItem)
                                                    Assert.IsNotNull(old)
                                                    drawingList.Add(old) 'Add old for comparison
                                                    replace += 1
                                                Case Else
                                                    Throw New Exception() 'Not expected!
                                            End Select
                                        End Sub
        'Act
        c.Tool.MouseDown(c, New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0))
        c.Tool.MouseMove(c, New MouseEventArgs(MouseButtons.Left, 0, 100, 100, 0))
        c.Tool.MouseUp(c, New MouseEventArgs(MouseButtons.Left, 0, 100, 100, 0)) 'Causes Element Changed
        c.DeSelect() 'Causes Element Changed

        'Assert
        Assert.AreEqual(0, added)
        Assert.AreEqual(0, removed)
        Assert.AreEqual(2, replace) 'MouseUp on tool should replace the item on collection
        Assert.AreEqual(2, drawingList.Count)
        Assert.AreNotEqual(drawingList(0).BoundingBox, drawingList(1).BoundingBox) 'OldItem had to be changed.
    End Sub


    <TestMethod()> <TestCategory("Controls")> <WorkItem(9858)>
    Public Sub MoveWithKeyboard_ItmChanged()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas With {.BackgroundImage = New Bitmap(100,100)}, ICanvas)
        Dim drawingList As New List(Of IDrawableItem)
        Dim itm As IDrawableItem = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        drawingList.Add(itm)
        Dim shape As IRectangle = DirectCast(itm, IRectangle)
        shape.Left = 0 : shape.Right = 10 : shape.Top = 0 : shape.Bottom = 10
        c.AddItem(itm)
        AddHandler c.CollectionChanged, Sub(s As Object, e As NotifyCollectionChangedEventArgs)
                                            Select Case e.Action
                                                Case NotifyCollectionChangedAction.Add
                                                    added += 1
                                                Case NotifyCollectionChangedAction.Remove
                                                    removed += 1
                                                Case NotifyCollectionChangedAction.Replace
                                                    Dim old = DirectCast(e.OldItems(0), IDrawableItem)
                                                    drawingList.Add(old) 'Add old for comparison
                                                    replace += 1
                                                Case Else
                                                    Throw New Exception() 'Not expected!
                                            End Select
                                        End Sub
       
        'Act
        c.Tool.MouseDown(c, New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0))
        c.Tool.KeyDown(c, New KeyEventArgs(Keys.Down))

        'Assert
        Assert.AreEqual(0, added)
        Assert.AreEqual(0, removed)
        Assert.AreEqual(1, replace) 'MouseUp on tool should replace the item on collection
        Assert.AreEqual(2, drawingList.Count)
        Assert.AreNotEqual(drawingList(0).BoundingBox, drawingList(1).BoundingBox) 'OldItem had to be changed.
    End Sub

    Private Function TrackAndResetCounters() As EventHandler(Of NotifyCollectionChangedEventArgs)
        added = 0 : removed = 0 : replace = 0
        Return Sub(s As Object, e As NotifyCollectionChangedEventArgs)
                   Select Case e.Action
                       Case NotifyCollectionChangedAction.Add
                           added += 1
                       Case NotifyCollectionChangedAction.Remove
                           removed += 1
                       Case NotifyCollectionChangedAction.Replace
                           replace += 1
                       Case Else
                           Throw New Exception() 'Not expected!
                   End Select
               End Sub
    End Function

    Private Function CreateRectangle(x As Integer, y As Integer, width As Integer, height As Integer) As IRectangle
        Dim itm = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        itm.Left = x : itm.Right = x + width : itm.Top = y : itm.Bottom = y + height
        Return itm
    End Function

End Class
