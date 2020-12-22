Imports Cito.Tester.Common.Controls.Canvas.Drawable.Shapes
Imports System.Runtime.CompilerServices

Namespace Controls.Canvas.Factory.DrawableShapeFactory
    Public MustInherit Class ShapeFactoryBase
        Implements IDrawableShapeFactory


        Public Overridable Function CreateShape(Of T As IShape)() As T Implements IDrawableShapeFactory.CreateShape
            Return CreateShape(Of T)(String.Empty)
        End Function

        Public Function CreateShape(Of T As IShape)(takeDimensions As T) As T Implements IDrawableShapeFactory.CreateShape
            Dim ret As IShape = Nothing

            If TryCast(takeDimensions, ICircle) IsNot Nothing Then
                ret = CreateShape(Of ICircle)().SetDimensions(takeDimensions)
            ElseIf TryCast(takeDimensions, IRectangle) IsNot Nothing Then
                ret = CreateShape(Of IRectangle)().SetDimensions(takeDimensions)
            ElseIf TryCast(takeDimensions, IPointUpTriangle) IsNot Nothing Then
                ret = CreateShape(Of IPointUpTriangle)().SetDimensions(takeDimensions)
            ElseIf TryCast(takeDimensions, IPointDownTriangle) IsNot Nothing Then
                ret = CreateShape(Of IPointDownTriangle)().SetDimensions(takeDimensions)
            ElseIf TryCast(takeDimensions, IEllipse) IsNot Nothing Then
                ret = CreateShape(Of IEllipse)().SetDimensions(takeDimensions)
            ElseIf TryCast(takeDimensions, IPolygon) IsNot Nothing Then
                ret = CreateShape(Of IPolygon)().SetDimensions(takeDimensions)
            End If

            Debug.Assert(ret IsNot Nothing)

            Return DirectCast(ret, T)
        End Function

        Public Function DuplicateShape(shape As IShape) As IShape Implements IDrawableShapeFactory.DuplicateShape
            Dim ret As IShape = Nothing

            If shape IsNot Nothing Then
                If TryCast(shape, ICircle) IsNot Nothing Then
                    ret = CreateShape(Of ICircle)(shape.ID, shape.Label, DirectCast(shape, ICircle))
                ElseIf TryCast(shape, IRectangle) IsNot Nothing Then
                    ret = CreateShape(Of IRectangle)(shape.ID, shape.Label, DirectCast(shape, IRectangle))
                ElseIf TryCast(shape, IPointUpTriangle) IsNot Nothing Then
                    ret = CreateShape(Of IPointUpTriangle)(shape.ID, shape.Label, DirectCast(shape, IPointUPTriangle))
                ElseIf TryCast(shape, IPointDownTriangle) IsNot Nothing Then
                    ret = CreateShape(Of IPointDownTriangle)(shape.ID, shape.Label, DirectCast(shape, IPointDownTriangle))
                ElseIf TryCast(shape, IEllipse) IsNot Nothing Then
                    ret = CreateShape(Of IEllipse)(shape.ID, shape.Label, DirectCast(shape, IEllipse))
                ElseIf TryCast(shape, IPolygon) IsNot Nothing Then
                    ret = CreateShape(Of IPolygon)(shape.ID, shape.Label, DirectCast(shape, IPolygon))
                End If

                Debug.Assert(ret IsNot Nothing)
            End If

            Return ret
        End Function

        Public Function CreateShape(Of T As IShape)(id As String, takeDimensions As T) As T Implements IDrawableShapeFactory.CreateShape
            Dim ret = CreateShape(Of T)(id).SetDimensions(takeDimensions)
            Return DirectCast(ret, T)
        End Function

        Public Function CreateShape(Of T As IShape)(id As String, label As String, takeDimensions As T) As T Implements IDrawableShapeFactory.CreateShape
            Dim ret = CreateShape(Of T)(id, label).SetDimensions(takeDimensions)
            Return DirectCast(ret, T)
        End Function

        Public Function CreateShape(Of T As IShape)(id As String, label As String) As T Implements IDrawableShapeFactory.CreateShape
            Dim ret As IShape = CreateShape(Of T)(id)
            ret.Label = label
            Return DirectCast(ret, T)
        End Function

        Public Function CreateShape(Of T As IShape)(id As String) As T Implements IDrawableShapeFactory.CreateShape
            Dim ret As IShape = Nothing

            If GetType(T).HasType(GetType(ICircle)) Then
                ret = If(String.IsNullOrEmpty(id), New CircleShape(), New CircleShape(id))
                SetCirclePainter(ret)
            ElseIf GetType(T).HasType(GetType(IRectangle)) Then
                ret = If(String.IsNullOrEmpty(id), New RectangleShape(), New RectangleShape(id))
                SetRectanglePainter(ret)
            ElseIf GetType(T).HasType(GetType(IPointUpTriangle)) Then
                ret = If(String.IsNullOrEmpty(id), New PointUpTriangleShape(), New PointUpTriangleShape(id))
                SetTrianglePainter(ret)
            ElseIf GetType(T).HasType(GetType(IPointDownTriangle)) Then
                ret = If(String.IsNullOrEmpty(id), New PointDownTriangleShape(), New PointDownTriangleShape(id))
                SetTrianglePainter(ret)
            ElseIf GetType(T).HasType(GetType(IEllipse)) Then
                ret = If(String.IsNullOrEmpty(id), New EllipseShape(), New EllipseShape(id))
                SetEllipsePainter(ret)
            ElseIf GetType(T).HasType(GetType(IPolygon)) Then
                ret = If(String.IsNullOrEmpty(id), New PolygonShape(), New PolygonShape(id))
                SetPolygonPainter(ret)
            End If

            Debug.Assert(ret IsNot Nothing)

            Return DirectCast(ret, T)
        End Function

        Protected MustOverride Sub SetCirclePainter(drawable As IDrawableItem)
        Protected MustOverride Sub SetEllipsePainter(drawable As IDrawableItem)
        Protected MustOverride Sub SetRectanglePainter(drawable As IDrawableItem)
        Protected MustOverride Sub SetTrianglePainter(drawable As IDrawableItem)
        Protected MustOverride Sub SetPolygonPainter(drawable As IDrawableItem)

    End Class

    Public Module TypeExtensions
        <Extension()>
        Public Function HasType(ByRef type As Type, base As Type) As Boolean
            Return type Is base OrElse type.GetInterface(base.Name) IsNot Nothing
        End Function
    End Module
End Namespace
