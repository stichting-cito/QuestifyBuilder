Public Class GotoItemEventArgs
    Inherits EventArgs


    Private _force As Boolean
    Private _index As Integer



    Private Sub New()
        _force = False
    End Sub

    Public Sub New(index As Integer)
        Me.New()
        _index = index
    End Sub

    Public Sub New(index As Integer, force As Boolean)
        Me.New()
        _index = index
        _force = force
    End Sub



    Public Property Force As Boolean
        Get
            Return _force
        End Get
        Set
            _force = value
        End Set
    End Property

    Public Property Index As Integer
        Get
            Return _index
        End Get
        Set
            _index = value
        End Set
    End Property


End Class