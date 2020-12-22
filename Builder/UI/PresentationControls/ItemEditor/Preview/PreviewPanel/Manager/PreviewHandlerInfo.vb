Public Class PreviewHandlerInfo


    Private _CLSID As Guid
    Private _displayName As String



    Public Property CLSID() As Guid
        Get
            Return _CLSID
        End Get
        Set(ByVal value As Guid)
            _CLSID = value
        End Set
    End Property

    Public Property DisplayName() As String
        Get
            Return _displayName
        End Get
        Set(ByVal value As String)
            _displayName = value
        End Set
    End Property


    Public Sub New()
    End Sub

    Public Sub New(ByVal CLSID As Guid, ByVal displayName As String)
        Me.CLSID = CLSID
        Me.DisplayName = displayName
    End Sub

End Class