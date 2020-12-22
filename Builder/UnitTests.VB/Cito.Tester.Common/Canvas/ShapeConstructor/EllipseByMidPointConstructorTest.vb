
Imports System.Drawing
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports Cito.Tester.Common.Controls.Canvas.ShapeConstructor

<TestClass()>
Public Class EllipseByMidPointConstructorTest

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestFirstPoint_MinRadiusIsSet()
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())

        c.SuggestPoint(New Point(0, 0))

        Assert.AreEqual(c.minHRadius, c.Shape.HRadius)
        Assert.AreEqual(c.minVRadius, c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CommitFirstPoint_MinRadiusIsSet()
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())

        c.CommitPoint(New Point(0, 0))

        Assert.AreEqual(c.minHRadius, c.Shape.HRadius)
        Assert.AreEqual(c.minVRadius, c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestSeccondPointLessThanMinRadius_MinRadiusIsSet()
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
        c.CommitPoint(New Point(0, 0))

        c.SuggestPoint(New Point(c.minHRadius \ 2, c.minVRadius \ 2))

        Assert.AreEqual(c.minHRadius, c.Shape.HRadius)
        Assert.AreEqual(c.minVRadius, c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestSecondPoint2TimesMinRadius_RadiusLargerThanMinRadius()
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
        c.CommitPoint(New Point(0, 0))

        c.SuggestPoint(New Point(c.minHRadius * 2, c.minVRadius * 2))

        Assert.IsTrue(c.minHRadius < c.Shape.HRadius)
        Assert.IsTrue(c.minVRadius < c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestSecondBeondMinRadius_thenLessThanMinRad_RadiusSmallerThanMin()
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
        c.CommitPoint(New Point(0, 0))

        c.SuggestPoint(New Point(c.minHRadius * 2, c.minVRadius * 2))
        c.SuggestPoint(New Point(c.minHRadius \ 2, c.minVRadius \ 2))

        Assert.IsTrue(c.minHRadius > c.Shape.HRadius)
        Assert.IsTrue(c.minVRadius > c.Shape.VRadius)
    End Sub

End Class
