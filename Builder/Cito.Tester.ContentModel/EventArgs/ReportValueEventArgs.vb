Public Class ReportValueEventArgs
    Inherits EventArgs


    Private _category As String
    Private _name As String
    Private _value As String



    Public Sub New(category As String, name As String, value As String)
        Me._category = category
        Me._name = name
        Me._value = value
    End Sub


    Private Sub New()
    End Sub



    Public ReadOnly Property Category As String
        Get
            Return _category
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Value As String
        Get
            Return _value
        End Get
    End Property


End Class