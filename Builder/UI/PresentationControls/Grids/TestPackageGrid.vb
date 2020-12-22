Imports Questify.Builder.Security

Public Class TestPackageGrid


    Public Overrides Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AddNew, TestBuilderPermissionTarget.TestPackageEntity, bankId)
    End Function

    Public Overrides Function EditIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.TestPackageEntity, bankId)
    End Function

    Public Overrides Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowDelete AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.TestPackageEntity, bankId)
    End Function

    Public Overrides Function PublishIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowPublish AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Publish, TestBuilderPermissionTarget.TestPackageEntity, bankId)
    End Function

    Public Overrides Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ViewProperties, TestBuilderPermissionTarget.TestPackageEntity, bankId)
    End Function

    Public Overrides Function ExportIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.TestPackageEntity, bankId)
    End Function


End Class
