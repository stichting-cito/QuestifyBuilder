Public Enum BankRoleGridRowEntityType
    BankRow
    UserRoleRow
End Enum

Public Class BankRoleGridRowEntity
    Private _entityId As Integer
    Private _name As String
    Private _type As BankRoleGridRowEntityType
    Private _parent As BankRoleGridRowEntity
    Private _bankRoleGridRowEntityCollection As New List(Of BankRoleGridRowEntity)

    Public ReadOnly Property EntityId() As Integer
        Get
            Return _entityId
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Type() As BankRoleGridRowEntityType
        Get
            Return _type
        End Get
    End Property

    Public ReadOnly Property Parent() As BankRoleGridRowEntity
        Get
            Return _parent
        End Get
    End Property

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    Public ReadOnly Property BankRoleGridRowEntityCollection() As List(Of BankRoleGridRowEntity)
        Get
            Return _bankRoleGridRowEntityCollection
        End Get
    End Property

    Public Sub New(ByVal entityId As Integer, ByVal name As String, ByVal parent As BankRoleGridRowEntity, ByVal type As BankRoleGridRowEntityType)
        _entityId = entityId
        _name = name
        _parent = parent
        _type = type
    End Sub

End Class
