
Imports Cito.Tester.Common.Controls.Canvas.Drawable.ShapePainter
Imports Cito.Tester.Common.Controls.Canvas.LabelGenerator

Namespace Controls.Canvas.Factory.DrawableShapeFactory
    Public Class LabeledShapeFactory
        Inherits ShapeFactoryBase

        Private _idGenerator As IIdentifierGenerator(Of String)

        Public Sub New(idGenerator As IIdentifierGenerator(Of String))
            _idGenerator = idGenerator
        End Sub


        Public Overrides Function CreateShape(Of T As IShape)() As T
            Dim id As String = If(_idGenerator Is Nothing, String.Empty, _idGenerator.GetNewIdentifier())
            Dim label As String = If(String.IsNullOrEmpty(id), String.Empty, If(TypeOf _idGenerator Is AlphabeticIdentifierGenerator, AlphabeticIdentifierHelper.GetAlphabeticIdentifier(id), String.Empty))
            If Not String.IsNullOrEmpty(label) Then
                Return MyBase.CreateShape(Of T)(id, label)
            Else
                Return MyBase.CreateShape(Of T)(id)
            End If
        End Function

        Protected Overrides Sub SetCirclePainter(drawable As IDrawableItem)
            drawable.SetPainter(New LabeledCirclePainter())
        End Sub

        Protected Overrides Sub SetEllipsePainter(drawable As IDrawableItem)
            drawable.SetPainter(New LabeledEllipsePainter())
        End Sub

        Protected Overrides Sub SetRectanglePainter(drawable As IDrawableItem)
            drawable.SetPainter(New LabeledRectanglePainter())
        End Sub

        Protected Overrides Sub SetTrianglePainter(drawable As IDrawableItem)
            drawable.SetPainter(New LabeledTrianglePainter())
        End Sub

        Protected Overrides Sub SetPolygonPainter(drawable As IDrawableItem)
            drawable.SetPainter(New LabeledPolygonPainter)
        End Sub

    End Class

End Namespace
