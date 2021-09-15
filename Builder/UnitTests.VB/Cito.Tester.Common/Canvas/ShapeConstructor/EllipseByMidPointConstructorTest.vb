
Imports System.Drawing
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports Cito.Tester.Common.Controls.Canvas.ShapeConstructor

<TestClass()>
Public Class EllipseByMidPointConstructorTest

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestFirstPoint_MinRadiusIsSet()
        'Arrange
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
       
        'Act
        c.SuggestPoint(New Point(0, 0))
       
        'Assert
        Assert.AreEqual(c.minHRadius, c.Shape.HRadius)
        Assert.AreEqual(c.minVRadius, c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CommitFirstPoint_MinRadiusIsSet()
        'Arrange
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
       
        'Act
        c.CommitPoint(New Point(0, 0))
       
        'Assert
        Assert.AreEqual(c.minHRadius, c.Shape.HRadius)
        Assert.AreEqual(c.minVRadius, c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestSeccondPointLessThanMinRadius_MinRadiusIsSet()
        'Arrange
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
        c.CommitPoint(New Point(0, 0))
       
        'Act
        c.SuggestPoint(New Point(c.minHRadius \ 2, c.minVRadius \ 2))

        'Assert
        Assert.AreEqual(c.minHRadius, c.Shape.HRadius)
        Assert.AreEqual(c.minVRadius, c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestSecondPoint2TimesMinRadius_RadiusLargerThanMinRadius()
        'Arrange
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
        c.CommitPoint(New Point(0, 0))
      
        'Act
        c.SuggestPoint(New Point(c.minHRadius * 2, c.minVRadius * 2))

        'Assert
        Assert.IsTrue(c.minHRadius < c.Shape.HRadius)
        Assert.IsTrue(c.minVRadius < c.Shape.VRadius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestSecondBeondMinRadius_thenLessThanMinRad_RadiusSmallerThanMin()
        'Arrange
        Dim c As New EllipseByMidPointConstructor(New DefaultShapeFactory())
        c.CommitPoint(New Point(0, 0))
     
        'Act
        c.SuggestPoint(New Point(c.minHRadius * 2, c.minVRadius * 2)) 'First well beyond the min radius
        c.SuggestPoint(New Point(c.minHRadius \ 2, c.minVRadius \ 2)) 'Now less than min radius

        'Assert
        Assert.IsTrue(c.minHRadius > c.Shape.HRadius)
        Assert.IsTrue(c.minVRadius > c.Shape.VRadius)
    End Sub

End Class
