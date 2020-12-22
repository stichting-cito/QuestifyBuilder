
Imports Questify.Builder.UI.Controls.Logic
Imports System.Drawing

<TestClass()>
Public Class DrawingMathTests

    <TestMethod(), TestCategory("UILogic")>
    Public Sub CreateRectFrom2PointsTest()
        Dim p1 = New PointF(10, 12)
        Dim p2 = New PointF(20, 22)

        Dim rect = DrawingMath.CreateRect(p1, p2)

        Assert.AreEqual(10, rect.Left)
        Assert.AreEqual(12, rect.Top)
        Assert.AreEqual(20, rect.Right)
        Assert.AreEqual(22, rect.Bottom)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub CreateRectFrom2PointsTestWithOffset()
        Dim p1 = New PointF(0, 0)
        Dim p2 = New PointF(100, 100)

        Dim rect = DrawingMath.CreateRect(p1, p2, 15, 10, 17, 25)

        Assert.AreEqual(15, rect.Left)
        Assert.AreEqual(10, rect.Top)
        Assert.AreEqual(83, rect.Right)
        Assert.AreEqual(75, rect.Bottom)
    End Sub
    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_TopLeft()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.TopLeft, rect)

        Assert.AreEqual(10.0F, pnt.X)
        Assert.AreEqual(12.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_TopCenter()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.TopCenter, rect)

        Assert.AreEqual(45.0F, pnt.X)
        Assert.AreEqual(12.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_TopRight()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.TopRight, rect)

        Assert.AreEqual(80.0F, pnt.X)
        Assert.AreEqual(12.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_MiddleLeft()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.MiddleLeft, rect)

        Assert.AreEqual(10.0F, pnt.X)
        Assert.AreEqual(39.0F, pnt.Y)
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_MiddleCenter()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.MiddleCenter, rect)

        Assert.AreEqual(45.0F, pnt.X)
        Assert.AreEqual(39.0F, pnt.Y)
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_MiddleRight()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.MiddleRight, rect)

        Assert.AreEqual(80.0F, pnt.X)
        Assert.AreEqual(39.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_BottomLeft()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.BottomLeft, rect)

        Assert.AreEqual(10.0F, pnt.X)
        Assert.AreEqual(66.0F, pnt.Y)
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_BottomCenter()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.BottomCenter, rect)

        Assert.AreEqual(45.0F, pnt.X)
        Assert.AreEqual(66.0F, pnt.Y)
    End Sub


    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointFromRect_BottomRight()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim pnt = DrawingMath.GetPoint(ContentAlignment.BottomRight, rect)

        Assert.AreEqual(80.0F, pnt.X)
        Assert.AreEqual(66.0F, pnt.Y)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetCornerPointsFromRect_AndReconstructRect_ShouldBeEqual()
        Dim rect = Rectangle.FromLTRB(10, 12, 80, 66)

        Dim p1 = DrawingMath.GetPoint(ContentAlignment.TopLeft, rect)
        Dim p2 = DrawingMath.GetPoint(ContentAlignment.BottomRight, rect)
        Dim rect2 = DrawingMath.CreateRect(p1, p2)

        Assert.AreEqual(rect, rect2)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetSmallerRect()
        Dim rect = New Rectangle(-5, -5, 110, 110)

        Dim rect2 = DrawingMath.GetSmallerRect(rect, 5)

        Assert.AreEqual(rect.Left + 5, rect2.Left)
        Assert.AreEqual(rect.Top + 5, rect2.Top)
        Assert.AreEqual(rect.Right - 5, rect2.Right)
        Assert.AreEqual(rect.Bottom - 5, rect2.Bottom)
        Assert.AreEqual(New Rectangle(0, 0, 100, 100), rect2)
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub GetPointOffset()
        Dim rect As New Rectangle(10, 20, 100, 120)

        Dim p = DrawingMath.GetPoint(ContentAlignment.TopLeft, rect, 5, 10)

        Assert.AreEqual(New PointF(10 + 5, 20 + 10), p)
    End Sub

End Class
