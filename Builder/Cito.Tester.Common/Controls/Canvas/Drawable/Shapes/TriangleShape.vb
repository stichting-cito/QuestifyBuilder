Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.Shapes
    <DesignerCategory("Code")>
    Friend MustInherit Class TriangleShape
        Inherits DrawableBase(Of ITriangle)
        Implements ITriangle, IPolygon

        Private _top As Integer
        Private _right As Integer
        Private _left As Integer
        Private _bottom As Integer
        Friend _blockNotify As Boolean = False


        Public Sub New(identifier As String, label As String)
            MyBase.New(identifier, label)
            Coordinates = New List(Of Point)()
        End Sub

        Public Sub New()
            Coordinates = New List(Of Point)()
        End Sub

        Public Sub New(identifier As String)
            MyBase.New(identifier)
            Coordinates = New List(Of Point)()
        End Sub



        Private _label As String

        Public Property Label As String Implements ITriangle.Label
            Get
                Return _label
            End Get
            Set(value As String)
                _label = value
            End Set
        End Property

        Public Function SetDimensions(shape As IShape) As IShape Implements ITriangle.SetDimensions
            Dim triangle = TryCast(shape, ITriangle)

            If triangle IsNot Nothing Then
                Me.Top = triangle.Top
                Me.Bottom = triangle.Bottom
                Me.Left = triangle.Left
                Me.Right = triangle.Right
            End If
            Return Me
        End Function

        Public Property Bottom As Integer Implements ITriangle.Bottom
            Get
                Return _bottom
            End Get
            Set(value As Integer)
                _bottom = value
                If Not (_blockNotify) Then RaisePropertyChanged("_bottom")
            End Set
        End Property

        Public Property Left As Integer Implements ITriangle.Left
            Get
                Return _left
            End Get
            Set(value As Integer)
                _left = value
                If Not (_blockNotify) Then RaisePropertyChanged("Left")
            End Set
        End Property


        Public Property Right As Integer Implements ITriangle.Right
            Get
                Return _right
            End Get
            Set(value As Integer)
                _right = value
                If Not (_blockNotify) Then RaisePropertyChanged("Right")
            End Set
        End Property

        Public Property Top As Integer Implements ITriangle.Top
            Get
                Return _top
            End Get
            Set(value As Integer)
                _top = value
                If Not (_blockNotify) Then RaisePropertyChanged("Top")
            End Set
        End Property

        Public MustOverride ReadOnly Property Height As Integer Implements ITriangle.Height

        Public ReadOnly Property Width As Integer Implements ITriangle.Width
            Get
                Return Right() - Left
            End Get
        End Property

        Public MustOverride Overrides Property AnchorPoint As Point




        Public Overrides Sub DecWidth()
            Right = Math.Max(Right() - 1, Left)
        End Sub

        Public Overrides Sub IncWidth()
            Right = Math.Max(Right() + 1, Left)
        End Sub

        Public Overrides ReadOnly Property Shape As ITriangle
            Get
                Return DirectCast(Me, ITriangle)
            End Get
        End Property




        Public Property Coordinates As List(Of Point) Implements IPolygon.Coordinates
            Get
                Dim coords As New List(Of Point)()
                coords.Add(New Point(Me.Left, Me.Bottom))
                coords.Add(New Point(Me.Left + Convert.ToInt32((Me.Right - Me.Left) / 2), Me.Top))
                coords.Add(New Point(Me.Right, Me.Bottom))
                coords.Add(New Point(Me.Left, Me.Bottom))
                Return coords
            End Get
            Set(value As List(Of Point))
            End Set
        End Property
    End Class

End Namespace
