

Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Security

Public Class TestGrid


    Public Overrides ReadOnly Property AllowAddNew() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AddNew, TestBuilderPermissionTarget.TestEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowEdit() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function EditIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowAddNew AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.TestEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowDelete() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowDelete AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.TestEntity, bankId)
    End Function

    Public Overrides Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property AllowPublish() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function PublishIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowPublish AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Publish, TestBuilderPermissionTarget.TestEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowShowProperties() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ViewProperties, TestBuilderPermissionTarget.TestEntity, bankId)
    End Function

    Public Overrides Sub AddNew()
        OnAddNewItem(New EventArgs)
    End Sub

    Public Overrides Sub Publish()
        OnPublishEntity(New EventArgs)
    End Sub

    Public Overrides ReadOnly Property AllowSynchronize() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowSynchronize
    End Function

    Public Overrides ReadOnly Property AllowPreview() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function PreviewIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowPreview
    End Function

    Public Overrides Sub Preview(ByVal entity As ResourceDto)
        OnPreviewEntity(New EntityActionEventArgs(entity))
    End Sub

    Public Overrides Function ExportIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.TestEntity, bankId)
    End Function


End Class
