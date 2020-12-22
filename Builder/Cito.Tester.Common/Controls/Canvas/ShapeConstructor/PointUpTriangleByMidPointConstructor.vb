Imports Cito.Tester.Common.Controls.Canvas.Factory

Namespace Controls.Canvas.ShapeConstructor
    Friend Class PointUpTriangleByMidPointConstructor
        Inherits TwoPointConstructorbase(Of IPointUpTriangle)

        Public Sub New(shapeFactory As IDrawableShapeFactory)
            MyBase.New(shapeFactory)
        End Sub


        Friend Overrides Sub SetShape(pSuggested As Point?)
            Dim dX, dY As Integer
            Dim px = If(p1.HasValue, p1.Value, If(pSuggested, Point.Empty))

            If (p1.HasValue AndAlso p2.HasValue) Then
                dX = Math.Abs(px.X - p2.Value.X)
                dY = Math.Abs(px.Y - p2.Value.Y)
            Else
                dX = If(p1.HasValue AndAlso pSuggested.HasValue, Math.Abs(px.X - pSuggested.Value.X), 10)
                dY = If(p1.HasValue AndAlso pSuggested.HasValue, Math.Abs(px.Y - pSuggested.Value.Y), 15)
            End If

            Shape.Left = px.X - dX
            Shape.Top = px.Y - dY
            Shape.Bottom = px.Y + dY
            Shape.Right = px.X + dX
        End Sub

    End Class
End Namespace