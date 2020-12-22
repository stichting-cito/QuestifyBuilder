
Imports Questify.Builder.Security

Public Class TestTemplateGrid


    Public Overrides ReadOnly Property AllowAddNew() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AddNew, TestBuilderPermissionTarget.TestTemplateEntity, bankId)
    End Function


    Public Overrides Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property AllowEdit() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function EditIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.TestTemplateEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowDelete() As Boolean = Not UseGridAsItemPicker

    Public Overrides Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowDelete AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.TestTemplateEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowShowProperties() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowShowProperties AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ViewProperties, TestBuilderPermissionTarget.TestTemplateEntity, bankId)
    End Function

    Public Overrides Sub AddNew()
        OnAddNewItem(New EventArgs)
    End Sub

    Public Overrides ReadOnly Property AllowSynchronize() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowSynchronize
    End Function

    Public Overrides Function ExportIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.TestTemplateEntity, bankId)
    End Function



End Class
