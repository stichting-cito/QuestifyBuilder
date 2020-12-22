Imports Cito.Tester.Common.Controls.Canvas.Factory

Namespace Controls.Canvas.ShapeConstructor
    Friend Class EllipseByMidPointConstructor
        Inherits TwoPointConstructorbase(Of IEllipse)

        Friend minHRadius As Integer = 15
        Friend minVRadius As Integer = 10
        Private hasbeenMinRadius As Boolean = False

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
                dX = If(p1.HasValue AndAlso pSuggested.HasValue, Math.Abs(px.X - pSuggested.Value.X), minHRadius)
                dY = If(p1.HasValue AndAlso pSuggested.HasValue, Math.Abs(px.Y - pSuggested.Value.Y), minVRadius)
                hasbeenMinRadius = hasbeenMinRadius OrElse (dX > minHRadius) OrElse (dY > minVRadius)
            End If
            Shape.AnchorPoint = px
            Shape.HRadius = If(hasbeenMinRadius, dX, Math.Max(dX, minHRadius))
            Shape.VRadius = If(hasbeenMinRadius, dY, Math.Max(dY, minVRadius))
        End Sub


    End Class
End Namespace
