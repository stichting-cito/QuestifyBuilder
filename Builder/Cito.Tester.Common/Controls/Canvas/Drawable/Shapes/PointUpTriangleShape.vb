
Namespace Controls.Canvas.Drawable.Shapes
    Friend Class PointUpTriangleShape
        Inherits TriangleShape
        Implements IPointUpTriangle

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
                Return New Point(Left + (Width \ 2), Top + (Height \ 2))
            End Get
            Set(value As Point)
                _blockNotify = True
                Dim w = Width
                Dim h = Height
                Left = value.X - (w \ 2)
                Top = value.Y - (h \ 2)
                Bottom = Top + h
                Right = Left + w
                _blockNotify = False
                RaisePropertyChanged("AnchorPoint")
            End Set
        End Property

        Public Overrides ReadOnly Property BoundingBox As Rectangle
            Get
                Return New Rectangle(Left, Top, Width, Height)
            End Get
        End Property

        Public Overrides ReadOnly Property Height As Integer Implements ITriangle.Height
            Get
                Return Bottom - Top
            End Get
        End Property

        Public Overrides Sub DecHeight()
            Bottom = Math.Max(Bottom - 1, Top)
        End Sub

        Public Overrides Sub IncHeight()
            Bottom = Math.Max(Bottom + 1, Top)
        End Sub

    End Class
End Namespace