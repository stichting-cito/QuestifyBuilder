Public Class MetaDataCode
    Inherits MetaData

    Private _code As Guid



    Public Property Code() As Guid
        Get
            Return _code
        End Get
        Set(value As Guid)
            _code = value
        End Set
    End Property


End Class
