
Imports Questify.Builder.UI.Controls.Logic
Imports System.Drawing

<TestClass()>
Public Class DrawingMathTests

    <TestMethod(), TestCategory("UILogic")>
    Public Sub CreateRectFrom2PointsTest()
        'Arrange
        Dim p1 = New PointF(10, 12)
        Dim p2 = New PointF(20, 22)
        
        'Act
        Dim rect = DrawingMath.CreateRect(p1, p2)
        
        'Assert
        Assert.AreEqual(10, rect.Left)
        Assert.AreEqual(12, rect.Top)
        Assert.AreEqual(20, rect.Right)
        Assert.AreEqual(22, rect.Bottom)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub CreateRectFrom2PointsTestWithOffset()
        'Arrange
        Dim p1 = New PointF(0, 0)
        Dim p2 = New PointF(100, 100)
        
        'Act
        Dim rect = DrawingMath.CreateRect(p1, p2, 15, 10, 17, 25)
        
        'Assert
        Assert.AreEqual(15, rect.Left)
        Assert.AreEqual(10, rect.Top)
        Assert.AreEqual(83, rect.Right) '=100-17
        Assert.AreEqual(75, rect.Bottom) ' =100-25
    End Sub
    '--
    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_TopLeft()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.TopLeft, rect)
        
        'Assert
        Assert.AreEqual(10.0F, pnt.X)
        Assert.AreEqual(12.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_TopCenter()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.TopCenter, rect)
        
        'Assert
        Assert.AreEqual(45.0F, pnt.X) '=10+70/2
        Assert.AreEqual(12.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_TopRight()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.TopRight, rect)
        
        'Assert
        Assert.AreEqual(80.0F, pnt.X)
        Assert.AreEqual(12.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_MiddleLeft()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.MiddleLeft, rect)
        
        'Assert
        Assert.AreEqual(10.0F, pnt.X)
        Assert.AreEqual(39.0F, pnt.Y) '=12+54/2
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_MiddleCenter()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.MiddleCenter, rect)
        
        'Assert
        Assert.AreEqual(45.0F, pnt.X) '=10+70/2
        Assert.AreEqual(39.0F, pnt.Y) '=12+54/2
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_MiddleRight()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.MiddleRight, rect)
        
        'Assert
        Assert.AreEqual(80.0F, pnt.X)
        Assert.AreEqual(39.0F, pnt.Y) '=12+54/2
    End Sub
    
    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_BottomLeft()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.BottomLeft, rect)
        
        'Assert
        Assert.AreEqual(10.0F, pnt.X)
        Assert.AreEqual(66.0F, pnt.Y)
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_BottomCenter()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.BottomCenter, rect)
        
        'Assert
        Assert.AreEqual(45.0F, pnt.X) '=10+70/2
        Assert.AreEqual(66.0F, pnt.Y)
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_BottomRight()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim pnt = DrawingMath.GetPoint(ContentAlignment.BottomRight, rect)
        
        'Assert
        Assert.AreEqual(80.0F, pnt.X)
        Assert.AreEqual(66.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetCornerPointsFromRect_AndReconstructRect_ShouldBeEqual()
        'Arrange
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)
        
        'Act
        Dim p1 = DrawingMath.GetPoint(ContentAlignment.TopLeft, rect)
        Dim p2 = DrawingMath.GetPoint(ContentAlignment.BottomRight, rect)
        Dim rect2 = DrawingMath.CreateRect(p1, p2)
        
        'Assert
        Assert.AreEqual(rect, rect2)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetSmallerRect()
        'Arrange
        '
        '       A+------------------------+B
        '        |   U                V   |
        '        |   +----------------+   |
        '        |   |                |   |
        '        |   |                |   |
        '        |   |                |   |
        '        |   |                |   |
        '        |   |                |   |
        '        |   |                |   |
        '        |   |                |   |
        '        |   |                |   |
        '        |   +----------------+   |
        '        |   X                W   |
        '      D +------------------------+C
        'The larger rect (abcd) has coords  :
        ' A = -5,-5   top left
        ' C = 105,105 bottom right
        '
        ' Inner rect (uvwx) will get coords
        ' U = A.x+5  a.y+5
        ' W = c.x-5   c.y-5
        Dim rect = New Rectangle(-5, -5, 110, 110)
        
        'Act
        Dim rect2 = DrawingMath.GetSmallerRect(rect, 5)
        
        'Assert
        Assert.AreEqual(rect.Left + 5, rect2.Left)
        Assert.AreEqual(rect.Top + 5, rect2.Top)
        Assert.AreEqual(rect.Right - 5, rect2.Right)
        Assert.AreEqual(rect.Bottom - 5, rect2.Bottom)
        Assert.AreEqual(New Rectangle(0, 0, 100, 100), rect2)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointOffset()
        'Arrange
        Dim rect As New Rectangle(10, 20, 100, 120)
        
        'Act
        Dim p = DrawingMath.GetPoint(ContentAlignment.TopLeft, rect, 5, 10)
        
        'Assert
        Assert.AreEqual(New PointF(10 + 5, 20 + 10), p)
    End Sub

End Class
