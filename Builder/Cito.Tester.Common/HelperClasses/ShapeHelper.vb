Imports System.Text

Public Class ShapeHelper

    Public Shared Function GetRectangleCoords(ByVal topLeftX As Integer, ByVal topLeftY As Integer, ByVal bottomRightX As Integer, ByVal bottomRightY As Integer) As String
        Dim builder As New StringBuilder()

        builder.Append(topLeftX)
        builder.Append(",")
        builder.Append(topLeftY)
        builder.Append(",")
        builder.Append(bottomRightX)
        builder.Append(",")
        builder.Append(bottomRightY)

        Return builder.ToString()
    End Function

    Public Shared Function GetCircleCoords(ByVal xPos As Integer, ByVal yPos As Integer, ByVal radius As Integer) As String
        Dim builder As New StringBuilder()

        builder.Append(xPos)
        builder.Append(",")
        builder.Append(yPos)
        builder.Append(",")
        builder.Append(radius)

        Return builder.ToString()
    End Function

    Public Shared Function GetTriangleCoords(ByVal baseLeft As Point, baseRight As Point, top As Point) As String
        Dim builder = New StringBuilder()
        Dim coords As New List(Of Point)()
        coords.Add(New Point(baseLeft.X, baseLeft.Y))
        coords.Add(New Point(baseLeft.X + Convert.ToInt32((baseRight.X - baseLeft.X) / 2), top.Y))
        coords.Add(New Point(baseRight.X, baseLeft.Y))
        coords.Add(New Point(baseLeft.X, baseLeft.Y))

        For Each point As Point In coords
            builder.Append(point.X.ToString)
            builder.Append(",")
            builder.Append(point.Y.ToString)
            builder.Append(",")
        Next
        builder.Length -= 1
        Return builder.ToString
    End Function

    Public Shared Function GetPolygonCoords(ByVal points As List(Of System.Drawing.Point)) As String
        Dim builder = New StringBuilder()
        For Each point As Point In points
            builder.Append(point.X.ToString)
            builder.Append(",")
            builder.Append(point.Y.ToString)
            builder.Append(",")
        Next
        builder.Length -= 1
        Return builder.ToString
    End Function

End Class
