Public Class PersistStateEventArgs
    Inherits EventArgs


    Private _data As Response



    Public Property Data As Response
        Get
            Return _data
        End Get
        Set
            _data = value
        End Set
    End Property



    Public Sub New(data As Response)
        _data = data
    End Sub


End Class
