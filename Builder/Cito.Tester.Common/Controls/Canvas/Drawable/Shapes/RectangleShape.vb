Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.Shapes
    <DesignerCategory("Code")>
    Friend Class RectangleShape
        Inherits DrawableBase(Of IRectangle)
        Implements IRectangle

        Private _top As Integer
        Private _bottom As Integer
        Private _right As Integer
        Private _left As Integer
        Private _blockNotify As Boolean = False


        Public Sub New(identifier As String, label As String)
            MyBase.New(identifier, label)
        End Sub

        Public Sub New()

        End Sub

        Public Sub New(identifier As String)
            MyBase.New(identifier)
        End Sub



        Private _label As String

        Public Property Label As String Implements IRectangle.Label
            Get
                Return _label
            End Get
            Set(value As String)
                _label = value
            End Set
        End Property

        Public Function SetDimensions(shape As IShape) As IShape Implements IRectangle.SetDimensions
            Dim rectangle = TryCast(shape, IRectangle)

            If rectangle IsNot Nothing Then
                Me.Top = rectangle.Top
                Me.Bottom = rectangle.Bottom
                Me.Left = rectangle.Left
                Me.Right = rectangle.Right
            End If

            Return Me
        End Function

        Public Property Bottom As Integer Implements IRectangle.Bottom
            Get
                Return _bottom
            End Get
            Set(value As Integer)
                _bottom = value
                If Not (_blockNotify) Then
                    RaisePropertyChanged("Bottom")
                End If
            End Set
        End Property

        Public Property Left As Integer Implements IRectangle.Left
            Get
                Return _left
            End Get
            Set(value As Integer)
                _left = value
                If Not (_blockNotify) Then
                    RaisePropertyChanged("Left")
                End If
            End Set
        End Property


        Public Property Right As Integer Implements IRectangle.Right
            Get
                Return _right
            End Get
            Set(value As Integer)
                _right = value
                If Not (_blockNotify) Then
                    RaisePropertyChanged("Right")
                End If
            End Set
        End Property

        Public Property Top As Integer Implements IRectangle.Top
            Get
                Return _top
            End Get
            Set(value As Integer)
                _top = value
                If Not (_blockNotify) Then
                    RaisePropertyChanged("Top")
                End If
            End Set
        End Property

        Public ReadOnly Property Height As Integer Implements IRectangle.Height
            Get
                Return Bottom - Top
            End Get
        End Property

        Public ReadOnly Property Width As Integer Implements IRectangle.Width
            Get
                Return Right() - Left
            End Get
        End Property

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
                Right = Left + w
                Bottom = Top + h
                _blockNotify = False
                RaisePropertyChanged("AnchorPoint")
            End Set
        End Property


        Public Overrides Sub DecHeight()
            Bottom = Math.Max(Bottom - 1, Top)
        End Sub

        Public Overrides Sub DecWidth()
            Right = Math.Max(Right() - 1, Left)
        End Sub

        Public Overrides Sub IncHeight()
            Bottom = Math.Max(Bottom + 1, Top)
        End Sub

        Public Overrides Sub IncWidth()
            Right = Math.Max(Right() + 1, Left)
        End Sub

        Public Overrides ReadOnly Property Shape As IRectangle
            Get
                Return DirectCast(Me, IRectangle)
            End Get
        End Property

        Public Overrides ReadOnly Property BoundingBox As Rectangle
            Get
                Return New Rectangle(Left, Top, Width, Height)
            End Get
        End Property


    End Class

End Namespace
