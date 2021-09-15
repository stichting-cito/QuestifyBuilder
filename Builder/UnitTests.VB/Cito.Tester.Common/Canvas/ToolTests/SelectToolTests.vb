
Imports System.Linq
Imports Cito.Tester.Common.Controls.Canvas
Imports System.Windows.Forms
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory

<TestClass()>
Public Class SelectToolTests

    Protected Added As Integer
    Protected Removed As Integer
    Protected Replace As Integer

    <TestMethod()> <TestCategory("Controls")>
    Public Sub Select_CanvasEditItemIsSet()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas)
        Dim itm As IRectangle = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        itm.Bottom = 1 : itm.Right = 1
        c.AddItem(itm)

        AddHandler c.CollectionChanged, TrackAndResetCounters()

        'Act
        c.Tool.MouseDown(c, New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0))
       
        'Assert
        Assert.AreEqual(0, added)
        Assert.AreEqual(0, removed)
        Assert.AreEqual(0, replace)
        Assert.IsNotNull(c.EditItem) 'Items is edited
        Assert.AreEqual(0, c.Items.Count) 'Items is edited thus this one is empty, this is so redrawing is index based.
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub Select_ClickOnEmptyCanvas_NothingSelected()
        'Arrange
        Dim c As ICanvas = DirectCast(New Canvas, ICanvas)
        Dim itm As IDrawableItem = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        c.AddItem(itm)

        AddHandler c.CollectionChanged, TrackAndResetCounters()

        'Act
        c.Tool.MouseDown(c, New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)) 'Will select the object
        c.Tool.MouseDown(c, New MouseEventArgs(MouseButtons.Left, 1, 9999, -9999, 0)) 'Unlikely that something is here, will deselect.
        
        'Assert
        Assert.AreEqual(0, added)
        Assert.AreEqual(0, removed)
        Assert.AreEqual(0, replace)
        Assert.AreEqual(1, c.Items.Count) 'Items is edited thus this one is empty
        Assert.IsNull(c.EditItem) 'Items is edited
    End Sub

    Private Function TrackAndResetCounters() As EventHandler(Of NotifyCollectionChangedEventArgs)
        Added = 0 : Removed = 0 : Replace = 0
        Return Sub(s As Object, e As NotifyCollectionChangedEventArgs)
                   Select Case e.Action
                       Case NotifyCollectionChangedAction.Add
                           Added += 1
                       Case NotifyCollectionChangedAction.Remove
                           Removed += 1
                       Case NotifyCollectionChangedAction.Replace
                           Replace += 1
                       Case Else
                           Throw New Exception() 'Not expected!
                   End Select
               End Sub
    End Function

End Class
