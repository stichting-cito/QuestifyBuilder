Namespace Versioning
    <Serializable()>
    Public MustInherit Class MetaDataBase
        Private _id As Guid
        Private _name As String
        Private _version As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Guid, ByVal name As String, ByVal version As String)
            _id = id
            _name = name
            _version = version
        End Sub

        Public MustOverride ReadOnly Property Category As String

        Public Property Id As Guid
            Get
                Return _id
            End Get
            Set(value As Guid)
                _id = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        Public Property Version As String
            Get
                Return _version
            End Get
            Set(value As String)
                _version = value
            End Set
        End Property

    End Class
End Namespace