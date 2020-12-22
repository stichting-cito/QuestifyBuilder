Namespace Controls.Canvas.Drawable.Shapes
    Friend Class PointDownTriangleShape
        Inherits TriangleShape
        Implements IPointDownTriangle

        Public Sub New(identifier As String, label As String)
            MyBase.New(identifier, label)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(identifier As String)
            MyBase.New(identifier)
        End Sub

        Public Overrides Property AnchorPoint As Point
            Get
                Return New Point(Left() + (Width \ 2), Bottom + (Height \ 2))
            End Get
            Set(value As Point)
                _blockNotify = True
                Dim w = Width
                Dim h = Height
                Left = value.X - (w \ 2)
                Bottom = value.Y - (h \ 2)
                Top = Bottom + h
                Right = Left + w
                _blockNotify = False
                RaisePropertyChanged("AnchorPoint")
            End Set
        End Property

        Public Overrides ReadOnly Property BoundingBox As Rectangle
            Get
                Return New Rectangle(Left, Bottom, Width, Height)
            End Get
        End Property

        Public Overrides ReadOnly Property Height As Integer Implements ITriangle.Height
            Get
                Return Top - Bottom
            End Get
        End Property

        Public Overrides Sub DecHeight()
            Top = Math.Max(Bottom, Top - 1)
        End Sub

        Public Overrides Sub IncHeight()
            Top = Math.Max(Bottom, Top + 1)
        End Sub

    End Class
End Namespace
