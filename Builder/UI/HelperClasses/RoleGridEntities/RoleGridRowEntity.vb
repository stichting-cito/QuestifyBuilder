Public Class RoleGridRowEntity

    Private _roleId As Integer
    Private _name As String
    Private _isInRoleOriginalValue As Boolean
    Private _isInRoleNewValue As Boolean

    Public ReadOnly Property RoleId() As Integer
        Get
            Return _roleId
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property IsInRoleOriginalValue() As Boolean
        Get
            Return _isInRoleOriginalValue
        End Get
    End Property

    Public Property IsInRoleNewValue() As Boolean
        Get
            Return _isInRoleNewValue
        End Get
        Set(ByVal value As Boolean)
            _isInRoleNewValue = value
        End Set
    End Property

    Public Sub New(ByVal roleId As Integer, ByVal description As String, ByVal isInRole As Boolean)
        _roleId = roleId
        _name = description
        _isInRoleOriginalValue = isInRole
        _isInRoleNewValue = isInRole
    End Sub

End Class
