Namespace Controls.Canvas.ShapeConstructor
    Friend Module DrawingExtensions

        Public Function Distance(p1 As Point, p2 As Point) As Double
            Return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2))
        End Function

    End Module
End Namespace