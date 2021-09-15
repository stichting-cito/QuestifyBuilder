
Imports System.Drawing
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports Cito.Tester.Common.Controls.Canvas

<TestClass()>
Public Class RectangleTests

    <TestMethod()> <TestCategory("Controls")>
    Public Sub MoveRectangle_ShapeNotifies()
        'Arrange
        Dim r = CreateRectangle(0, 0, 10, 10)
        Dim fired As Boolean = False
        AddHandler r.PropertyChanged, Sub(s, e) fired = True
        
        'Act
        r.AnchorPoint = New Point(10, 10)
        
        'Assert
        Assert.IsTrue(fired)
    End Sub


    <TestMethod()> <TestCategory("Controls")>
    Public Sub IncWidthRectangle_ShapeNotifies()
        'Arrange
        Dim r = CreateRectangle(0, 0, 10, 10)
        Dim fired As Boolean = False
        AddHandler r.PropertyChanged, Sub(s, e) fired = True
        Dim shapeMod = DirectCast(r, IDimensionManipulator)
       
        'Act
        shapeMod.IncWidth()
       
        'Assert
        Assert.IsTrue(fired)
    End Sub

    Private Function CreateRectangle(x As Integer, y As Integer, width As Integer, height As Integer) As IRectangle
        Dim itm = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        itm.Left = x : itm.Right = x + width : itm.Top = y : itm.Bottom = y + height
        Return itm
    End Function

End Class
