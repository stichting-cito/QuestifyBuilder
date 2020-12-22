Imports System.ComponentModel
Imports System.Linq

Namespace Controls.Canvas.Drawable.Shapes
    <DesignerCategory("Code")>
    Friend Class PolygonShape
        Inherits DrawableBase(Of IPolygon)
        Implements IPolygon

        Private _coordinates As List(Of Point)
        Private _bb As Rectangle


        Public Sub New(identifier As String, label As String)
            MyBase.New(identifier, label)
        End Sub

        Public Sub New(identifier As String)
            MyBase.New(identifier)
        End Sub

        Public Sub New()
        End Sub

        Public Overrides ReadOnly Property BoundingBox As Rectangle
            Get
                Return _bb
            End Get
        End Property

        Public Overrides Sub DecHeight()
        End Sub

        Public Overrides Sub IncHeight()
        End Sub

        Public Overrides Sub IncWidth()
        End Sub

        Public Overrides Sub DecWidth()
        End Sub

        Public Overrides ReadOnly Property Shape As IPolygon
            Get
                Return DirectCast(Me, IPolygon)
            End Get
        End Property



        Private _label As String
        Private _blockNotify As Boolean = False

        Public Property Label As String Implements IPolygon.Label
            Get
                Return _label
            End Get
            Set(value As String)
                _label = value
            End Set
        End Property

        Public Function SetDimensions(shape As IShape) As IShape Implements IPolygon.SetDimensions
            Dim polygon = TryCast(shape, IPolygon)

            If polygon IsNot Nothing Then
                Me.Coordinates = polygon.Coordinates
            End If

            Return Me
        End Function

        Public Overrides Property AnchorPoint As Point
            Get
                Return New Point(_bb.Left + (_bb.Width \ 2), _bb.Top + (_bb.Height \ 2))
            End Get
            Set(value As Point)
                _blockNotify = True
                Dim oldPosition = New Point(_bb.Left + (_bb.Width \ 2), _bb.Top + (_bb.Height \ 2))
                Dim translationX As Integer = value.X - oldPosition.X
                Dim translationY As Integer = value.Y - oldPosition.Y
                Dim newCoordinates As New List(Of Point)()
                For Each oldCoordinate In Coordinates
                    Dim newCoordinate As New Point
                    newCoordinate.X = oldCoordinate.X + translationX
                    newCoordinate.Y = oldCoordinate.Y + translationY
                    newCoordinates.Add(newCoordinate)
                Next
                Coordinates = newCoordinates

                _blockNotify = False
                RaisePropertyChanged("AnchorPoint")
            End Set
        End Property

        Public Property Coordinates As List(Of Point) Implements IPolygon.Coordinates
            Get
                Return _coordinates
            End Get
            Set(value As List(Of Point))
                _coordinates = value
                If Not _coordinates.First.Equals(_coordinates.Last) Then
                    _coordinates.Add(_coordinates.Last)
                End If
                CalcBB()
                RaisePropertyChanged("Coordinates")
            End Set
        End Property



        Private Sub CalcBB()
            Dim minX As Integer = _coordinates.Min(Function(p) p.X)
            Dim minY As Integer = _coordinates.Min(Function(p) p.Y)
            Dim maxX As Integer = _coordinates.Max(Function(p) p.X)
            Dim maxY As Integer = _coordinates.Max(Function(p) p.Y)
            _bb = New Rectangle(minX, minY, (maxX - minX), (maxY - minY))
        End Sub

    End Class
End Namespace