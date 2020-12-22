Public Class ApplicableToMaskItem


    Private _caption As String
    Private _bitmask As Integer
    Private _enabled As Boolean



    Public Property Caption() As String
        Get
            Return _caption
        End Get
        Set(ByVal value As String)
            _caption = value
        End Set
    End Property

    Public Property Bitmask() As Integer
        Get
            Return _bitmask
        End Get
        Set(ByVal value As Integer)
            _bitmask = value
        End Set
    End Property

    Public Property Enabled As Boolean
        Get
            Return _enabled
        End Get
        Set(value As Boolean)
            _enabled = value
        End Set
    End Property


End Class