Imports Questify.Builder.Model.ContentModel

Public Class NewRoleDialog

    Private _role As EntityClasses.RoleEntity

    Public Property Role() As EntityClasses.RoleEntity
        Get
            Return _role
        End Get
        Private Set(ByVal value As EntityClasses.RoleEntity)
            _role = value
        End Set
    End Property


    Public Sub New()

        InitializeComponent()

        Me.Role = New EntityClasses.RoleEntity()
        Me.Role.IsApplicationRole = True
        BindData()
    End Sub

    Private Sub BindData()
        RoleEntityBindingSource.DataSource = Me.Role
    End Sub

    Protected Overrides Function OnOk() As Boolean
        Return True
    End Function

End Class
