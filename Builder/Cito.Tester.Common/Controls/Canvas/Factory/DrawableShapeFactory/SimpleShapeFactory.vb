Imports Cito.Tester.Common.Controls.Canvas.Drawable.ShapePainter

Namespace Controls.Canvas.Factory.DrawableShapeFactory
    Public Class SimpleShapeFactory
        Inherits ShapeFactoryBase

        Protected Overrides Sub SetCirclePainter(drawable As IDrawableItem)
            drawable.SetPainter(New HollowCirclePainter())
        End Sub

        Protected Overrides Sub SetEllipsePainter(drawable As IDrawableItem)
            drawable.SetPainter(New HollowEllipsePainter())
        End Sub

        Protected Overrides Sub SetRectanglePainter(drawable As IDrawableItem)
            drawable.SetPainter(New HollowRectanglePainter())
        End Sub

        Protected Overrides Sub SetTrianglePainter(drawable As IDrawableItem)
            drawable.SetPainter(New HollowTrianglePainter())
        End Sub

        Protected Overrides Sub SetPolygonPainter(drawable As IDrawableItem)
            drawable.SetPainter(New HollowPolygonPainter)
        End Sub
    End Class

End Namespace
