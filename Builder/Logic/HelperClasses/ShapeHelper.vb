Imports Cito.Tester.ContentModel
Imports shapeHlpr = Cito.Tester.Common.ShapeHelper

Public Class ShapeHelper

    Public Shared Function GetShapeCoordinates(ByVal shape As Shape) As String
        If TypeOf shape Is CircleShape Then
            Dim circleShape As CircleShape = DirectCast(shape, CircleShape)
            Return shapeHlpr.GetCircleCoords(circleShape.AnchorPoint.X, circleShape.AnchorPoint.Y, circleShape.Radius)
        ElseIf TypeOf shape Is RectangleShape Then
            Dim rectangleShape As RectangleShape = DirectCast(shape, RectangleShape)
            Return shapeHlpr.GetRectangleCoords(rectangleShape.TopLeft.X, rectangleShape.TopLeft.Y, rectangleShape.BottomRight.X, rectangleShape.BottomRight.Y)
        ElseIf TypeOf shape Is PolygonShape Then
            Return shapeHlpr.GetPolygonCoords(DirectCast(shape, PolygonShape).Coords)
        Else
            Return String.Empty
        End If
    End Function

End Class
