Imports Cito.Tester.Common.Controls.Canvas.Factory

Namespace Controls.Canvas.ShapeConstructor
    Friend Class CircleByMidPointConstructor
        Inherits TwoPointConstructorbase(Of ICircle)

        Friend ReadOnly MinRadius As Integer = 15
        Private hasBeenMinRadius As Boolean = False

        Public Sub New(shapeFactory As IDrawableShapeFactory)
            MyBase.New(shapeFactory)
        End Sub

        Friend Overrides Sub SetShape(pSuggested As Point?)
            Shape.AnchorPoint = If(p1.HasValue, p1.Value, If(pSuggested, Point.Empty))
            If (p1.HasValue AndAlso p2.HasValue) Then
                Shape.Radius = CInt(Distance(p1.Value, p2.Value))
            Else
                Dim px As Point? = If(p2, pSuggested)
                Shape.Radius = If(p1.HasValue AndAlso px.HasValue, CInt(Distance(p1.Value, px.Value)), minRadius)
                hasbeenMinRadius = hasbeenMinRadius OrElse (Shape.Radius > minRadius)
                Shape.Radius = If(hasbeenMinRadius, Shape.Radius, minRadius)

            End If
        End Sub

    End Class
End Namespace