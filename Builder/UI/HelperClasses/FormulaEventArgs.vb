Public Class FormulaEventArgs
    Inherits EventArgs

    Private _image As Byte()
    Private _imageName As String
    Private _mathMlValue As String

    Public Sub New(ByVal image As Byte(), ByVal imageName As String, ByVal mathMlValue As String)
        _image = image
        _imageName = imageName
        _mathMlValue = mathMlValue
    End Sub

    Public ReadOnly Property Image() As Byte()
        Get
            Return _image
        End Get
    End Property

    Public ReadOnly Property NewImageName() As String
        Get
            Return _imageName
        End Get
    End Property

    Public ReadOnly Property MathMlValue() As String
        Get
            Return _mathMlValue
        End Get
    End Property

    Public Property VerticalAlignValue() As Double

End Class

