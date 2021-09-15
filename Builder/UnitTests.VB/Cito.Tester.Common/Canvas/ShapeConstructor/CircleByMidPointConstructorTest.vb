
Imports System.Drawing
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports Cito.Tester.Common.Controls.Canvas.ShapeConstructor

<TestClass()>
Public Class CircleByMidPointConstructorTest

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestFirstPoint_MinradiusIsSet()
        'Arrange
        Dim c As New CircleByMidPointConstructor(New DefaultShapeFactory())
       
        'Act
        c.SuggestPoint(New Point(0, 0))
       
        'Assert
        Assert.AreEqual(c.minRadius, c.Shape.Radius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub CommitFirstPoint_MinradiusIsSet()
        'Arrange
        Dim c As New CircleByMidPointConstructor(New DefaultShapeFactory())
       
        'Act
        c.CommitPoint(New Point(0, 0))
       
        'Assert
        Assert.AreEqual(c.minRadius, c.Shape.Radius)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub SuggestSeccondPoint_RadiusIsLarger()
        'Arrange
        Dim c As New CircleByMidPointConstructor(New DefaultShapeFactory())
        c.CommitPoint(New Point(0, 0))
        
        'Act
        c.SuggestPoint(New Point(c.minRadius + 10, 0))
      
        'Assert
        Assert.IsTrue(c.minRadius < c.Shape.Radius)
    End Sub

End Class
