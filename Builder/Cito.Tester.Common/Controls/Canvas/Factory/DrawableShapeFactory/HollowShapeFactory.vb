Imports Cito.Tester.Common.Controls.Canvas.Drawable.ShapePainter

Namespace Controls.Canvas.Factory.DrawableShapeFactory
    Public Class HollowShapeFactory
        Inherits ShapeFactoryBase

        Protected Overrides Sub SetCirclePainter(drawable As IDrawableItem)
            drawable.SetPainter(New TransparantCirclePainter())
        End Sub

        Protected Overrides Sub SetEllipsePainter(drawable As IDrawableItem)
            drawable.SetPainter(New TransparantEllipsePainter())
        End Sub

        Protected Overrides Sub SetRectanglePainter(drawable As IDrawableItem)
            drawable.SetPainter(New TransparantRectanglePainter())
        End Sub

        Protected Overrides Sub SetTrianglePainter(drawable As IDrawableItem)
            drawable.SetPainter(New TransparantTrianglePainter())
        End Sub

        Protected Overrides Sub SetPolygonPainter(drawable As IDrawableItem)
            drawable.SetPainter(New HollowPolygonPainter)
        End Sub

    End Class
End Namespace

