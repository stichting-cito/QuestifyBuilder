Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.Shapes
    <DesignerCategory("Code")>
    Friend NotInheritable Class CircleShape
        Inherits DrawableBase(Of ICircle)
        Implements ICircle

        Private _bb As Rectangle
        Private _radius As Integer = 12

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
            DecWidth()
        End Sub

        Public Overrides Sub IncHeight()
            IncWidth()
        End Sub

        Public Overrides Sub IncWidth()
            Radius = Math.Max(1, Radius + 1)
        End Sub

        Public Overrides Sub DecWidth()
            Radius = Math.Max(1, Radius - 1)
        End Sub

        Public Overrides ReadOnly Property Shape As ICircle
            Get
                Return DirectCast(Me, ICircle)
            End Get
        End Property



        Private _label As String

        Public Property Label As String Implements ICircle.Label
            Get
                Return _label
            End Get
            Set(value As String)
                _label = value
            End Set
        End Property

        Public Function SetDimensions(shape As IShape) As IShape Implements ICircle.SetDimensions
            Dim circle = TryCast(shape, ICircle)

            If circle IsNot Nothing Then
                Me.Radius = circle.Radius
                Me.AnchorPoint = circle.AnchorPoint
            End If

            Return Me
        End Function

        Public Overrides Property AnchorPoint As Point
            Get
                Return MyBase.AnchorPoint
            End Get
            Set(value As Point)
                MyBase.AnchorPoint = value
                CalcBB()
            End Set
        End Property

        Public Property Radius As Integer Implements ICircle.Radius
            Get
                Return _radius
            End Get
            Set(value As Integer)
                _radius = value
                CalcBB()
                RaisePropertyChanged("Radius")
            End Set
        End Property


        Sub CalcBB()
            _bb = CalcBB(Radius)
        End Sub

        Function CalcBB(r As Integer) As Rectangle
            Dim a = AnchorPoint
            Return New Rectangle(a.X - r, a.Y - r,
                                r * 2, r * 2)
        End Function

    End Class
End Namespace