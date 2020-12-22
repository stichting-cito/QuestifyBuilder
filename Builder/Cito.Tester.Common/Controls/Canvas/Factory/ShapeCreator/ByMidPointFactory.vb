Imports Cito.Tester.Common.Controls.Canvas.ShapeConstructor

Namespace Controls.Canvas.Factory.ShapeCreator
    Public Class ByMidPointFactory
        Implements IShapeConstructorFactory


        Public Function Create(Of T As IShape)(shapeFactory As IDrawableShapeFactory) As IShapeConstructor(Of T) Implements IShapeConstructorFactory.Create
            Dim ret As IShapeConstructor(Of T) = Nothing

            If GetType(T) Is GetType(ICircle) Then
                ret = DirectCast(New CircleByMidPointConstructor(shapeFactory), IShapeConstructor(Of T))
            End If

            If GetType(T) Is GetType(IRectangle) Then
                ret = DirectCast(New RectangleByMidPointConstructor(shapeFactory), IShapeConstructor(Of T))
            End If

            If GetType(T) Is GetType(IEllipse) Then
                ret = DirectCast(New EllipseByMidPointConstructor(shapeFactory), IShapeConstructor(Of T))
            End If

            If GetType(T) Is GetType(IPointUpTriangle) Then
                ret = DirectCast(New PointUpTriangleByMidPointConstructor(shapeFactory), IShapeConstructor(Of T))
            End If

            If GetType(T) Is GetType(IPointDownTriangle) Then
                ret = DirectCast(New PointDownTriangleByMidPointConstructor(shapeFactory), IShapeConstructor(Of T))
            End If

            Debug.Assert(ret IsNot Nothing)

            Return ret
        End Function
    End Class
End Namespace
