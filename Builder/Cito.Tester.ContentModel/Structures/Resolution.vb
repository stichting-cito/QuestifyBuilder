Imports System.ComponentModel
Imports System.Drawing
Imports System.Xml.Serialization


<XmlRoot("Resolution", [Namespace]:="http://Cito.Tester.Server/xml/serialization", IsNullable:=True), _
Serializable> _
Public Class Resolution
    Implements INotifyPropertyChanged


    Private Shared _empty As New Resolution(0, 0)



    Private _backColor As Color
    Private _type As ResolutionType
    Private _x As Integer
    Private _y As Integer



    Public Sub New(x As Integer, y As Integer, type As ResolutionType, backColor As Color)
        Me.X = x
        Me.Y = y
        Me.Type = type
        Me.BackColor = backColor
    End Sub

    Public Sub New(x As Integer, y As Integer)
        Me.New(x, y, ResolutionType.ScaleKeepingAspectRatio, Color.Black)
    End Sub

    Public Sub New()
        Me.New(0, 0, ResolutionType.ScaleKeepingAspectRatio, Color.Black)
    End Sub



    <XmlIgnore> _
    Public Property BackColor As Color
        Get
            Return _backColor
        End Get
        Set
            _backColor = value
            OnPropertyChanged(New PropertyChangedEventArgs("BackColor"))
        End Set
    End Property

    <XmlAttribute("backcolor")> _
    Public Property BackColorHtml As String
        Get
            Return ColorTranslator.ToHtml(_backColor)
        End Get
        Set
            _backColor = ColorTranslator.FromHtml(value)
            OnPropertyChanged(New PropertyChangedEventArgs("BackColor"))
        End Set
    End Property

    <XmlIgnore> _
    Public Shared ReadOnly Property Empty As Resolution
        Get
            Return _empty
        End Get
    End Property

    <XmlAttribute("type")> _
    Public Property Type As ResolutionType
        Get
            Return _type
        End Get
        Set
            _type = value
            OnPropertyChanged(New PropertyChangedEventArgs("Type"))
        End Set
    End Property

    Public Property X As Integer
        Get
            Return _x
        End Get
        Set
            _x = value
            OnPropertyChanged(New PropertyChangedEventArgs("X"))
        End Set
    End Property

    Public Property Y As Integer
        Get
            Return _y
        End Get
        Set
            _y = value
            OnPropertyChanged(New PropertyChangedEventArgs("Y"))
        End Set
    End Property



    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub



    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing Then
            Throw New ArgumentNullException("obj")
        End If

        If TypeOf (obj) Is Resolution Then
            Return Me.X = DirectCast(obj, Resolution).X AndAlso Me.Y = DirectCast(obj, Resolution).Y
        Else
            Return False
        End If
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return X.GetHashCode() + Y.GetHashCode()
    End Function

    Public Shared Function IsEmpty(resolution As Resolution) As Boolean
        If resolution Is Nothing Then
            Throw New ArgumentNullException("resolution")
        End If

        Return resolution.X = Empty.X AndAlso resolution.Y = Empty.Y
    End Function

    Public Overrides Function ToString() As String
        Return $"{Me.X} x {Me.Y}"
    End Function



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged



    Public Enum ResolutionType
        Scale
        ScaleKeepingAspectRatio
        [Static]
        ScaleKeepingAspectRatioWithSmallDeformation
    End Enum


End Class