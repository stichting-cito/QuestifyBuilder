
Imports System.Drawing
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports Cito.Tester.Common.Controls.Canvas
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory

<TestClass()>
Public Class AnchorPointBehaviourTest

    <TestMethod()> <TestCategory("Controls")>
    Public Sub MoveCircleByAnchorTest()
        Dim fact As IDrawableShapeFactory = New HollowShapeFactory
        Dim res = fact.CreateShape(Of ICircle)()
        Dim shape As ICircle = DirectCast(res, ICircle)
        shape.Radius = 10
        Dim bbOld = res.BoundingBox

        res.AnchorPoint = New Point(10, 10)

        Assert.AreEqual(New Rectangle(0, 0, 20, 20), res.BoundingBox)
        Assert.AreNotEqual(bbOld, res.BoundingBox)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub MoveEllipseByAnchorTest()
        Dim fact As IDrawableShapeFactory = New HollowShapeFactory
        Dim res = fact.CreateShape(Of IEllipse)()
        Dim shape As IEllipse = DirectCast(res, IEllipse)
        shape.VRadius = 10
        shape.HRadius = 10
        Dim bbOld = res.BoundingBox

        res.AnchorPoint = New Point(10, 10)

        Assert.AreEqual(New Rectangle(0, 0, 20, 20), res.BoundingBox)
        Assert.AreNotEqual(bbOld, res.BoundingBox)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub MoveRectangleByAnchorTest()
        Dim fact As IDrawableShapeFactory = New HollowShapeFactory
        Dim res = fact.CreateShape(Of IRectangle)()
        Dim shape As IRectangle = DirectCast(res, IRectangle)
        shape.Top = 10
        shape.Left = 10
        shape.Right = 40
        shape.Bottom = 20
        Dim bbOld = res.BoundingBox
        Dim anchorOld = res.AnchorPoint

        Dim b4 = res.AnchorPoint
        res.AnchorPoint = New Point(10, 10)

        Assert.AreEqual(New Point(25, 15), b4)
        Assert.AreEqual(New Rectangle(-5, 5, 30, 10), res.BoundingBox)
        Assert.AreNotEqual(bbOld, res.BoundingBox)
    End Sub

End Class
