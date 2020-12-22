
Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.Shapes
    <DesignerCategory("Code")>
    Friend NotInheritable Class EllipseShape
        Inherits DrawableBase(Of IEllipse)
        Implements IEllipse

        Private _bb As Rectangle
        Private _radiusH As Integer = 12
        Private _radiusV As Integer = 7

        Public Sub New(identifier As String, label As String)
            MyBase.New(identifier, label)
        End Sub

        Public Sub New(identifier As String)
            MyBase.New(identifier)
        End Sub

        Public Sub New()
        End Sub

        Public Overrides Sub DecHeight()
            VRadius = Math.Max(1, VRadius - 1)
        End Sub

        Public Overrides Sub IncHeight()
            VRadius = Math.Max(1, VRadius + 1)
        End Sub

        Public Overrides Sub IncWidth()
            HRadius = Math.Max(1, HRadius + 1)
        End Sub

        Public Overrides Sub DecWidth()
            HRadius = Math.Max(1, HRadius - 1)
        End Sub

        Public Overrides ReadOnly Property Shape As IEllipse
            Get
                Return DirectCast(Me, IEllipse)
            End Get
        End Property

        Public Overrides ReadOnly Property BoundingBox As Rectangle
            Get
                Return _bb
            End Get
        End Property


        Private _label As String

        Public Property Label As String Implements IEllipse.Label
            Get
                Return _label
            End Get
            Set(value As String)
                _label = value
            End Set
        End Property

        Public Function SetDimensions(shape As IShape) As IShape Implements IEllipse.SetDimensions
            Dim ellipse = TryCast(shape, IEllipse)

            If ellipse IsNot Nothing Then
                Me.AnchorPoint = ellipse.AnchorPoint
                Me.HRadius = ellipse.HRadius
                Me.VRadius = ellipse.VRadius
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

        Public Property HRadius As Integer Implements IEllipse.HRadius
            Get
                Return _radiusH
            End Get
            Set(value As Integer)
                _radiusH = value
                RaisePropertyChanged("HRadius")
                CalcBB()
            End Set
        End Property

        Public Property VRadius As Integer Implements IEllipse.VRadius
            Get
                Return _radiusV
            End Get
            Set(value As Integer)
                _radiusV = value
                RaisePropertyChanged("VRadius")
                CalcBB()
            End Set
        End Property


        Sub CalcBB()
            _bb = CalcBB(HRadius, VRadius)
        End Sub

        Function CalcBB(rh As Integer, rv As Integer) As Rectangle
            Dim a = AnchorPoint
            Return New Rectangle(a.X - rh, a.Y - rv,
                                rh * 2, rv * 2)
        End Function


    End Class

End Namespace
