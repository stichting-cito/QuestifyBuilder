
Imports System.Drawing
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports Cito.Tester.Common.Controls.Canvas

<TestClass()>
Public Class ShapeFactoryTests

    <TestMethod()> <TestCategory("Controls")>
    Public Sub HollowShapeFactory_ConstructCircle_Test()
        'Arrange
        Dim fact As IDrawableShapeFactory = New HollowShapeFactory
      
        'Act
        Dim res = fact.CreateShape(Of ICircle)()
      
        'Assert
        Assert.IsNotNull(res)
        Assert.IsInstanceOfType(res, GetType(ICircle))
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub DuplicateShape_WithCircle_Test()
        'Arrange
        Dim fact As IDrawableShapeFactory = New HollowShapeFactory

        'Create Drawable
        Dim res = fact.CreateShape(Of ICircle)()
        'Set it's shape
        Dim shape As ICircle = DirectCast(res, ICircle)
        shape.AnchorPoint = New Point(140, 131)
        shape.Radius = 21

        'Act
        Dim test = fact.DuplicateShape(res)
        Dim shapeCmp As ICircle = DirectCast(test, ICircle)

        'Assert
        Assert.IsNotNull(test)
        Assert.IsInstanceOfType(test, GetType(ICircle))
        Assert.AreEqual(shape.AnchorPoint, shapeCmp.AnchorPoint)
        Assert.AreEqual(shape.Radius, shapeCmp.Radius)
    End Sub

End Class
