Imports Questify.Builder.Security

Public Class ItemLayoutTemplateGrid


    Public Overrides ReadOnly Property AllowAddNew() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property AllowEdit() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Function EditIsPermitted(ByVal bankId As Integer) As Boolean
        Return False
    End Function

    Public Overrides Function AddNewIsPermitted(ByVal bankId As Integer) As Boolean
        Return False
    End Function

    Public Overrides ReadOnly Property AllowDelete() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function DeleteIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowDelete AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.ItemLayoutTemplateEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowShowProperties() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function ShowPropertiesIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowShowProperties AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ViewProperties, TestBuilderPermissionTarget.ItemLayoutTemplateEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowSynchronize() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides ReadOnly Property AllowMoveResources As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function ShowReportIsPermitted(ByVal bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides Function SynchronizeIsPermitted(ByVal bankId As Integer) As Boolean
        Return AllowSynchronize
    End Function

    Public Overrides Sub AddNew()
        OnAddNewItem(New EventArgs)
    End Sub

    Public Overrides Function ExportIsPermitted(ByVal bankId As Integer) As Boolean
        Return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Export, TestBuilderPermissionTarget.ItemLayoutTemplateEntity, bankId)
    End Function

    Public Overrides ReadOnly Property AllowharmonizeDependantItems As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function HarmonizeDependantItemsIsPermitted(bankid As Integer) As Boolean
        Return AllowharmonizeDependantItems AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.ImportRawData, TestBuilderPermissionTarget.ItemEntity, bankid)
    End Function

    Public Overrides ReadOnly Property AllowToggleResourceVisibility As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides Function ToggleResourceVisibilityIsPermitted(bankId As Integer) As Boolean
        Return AllowToggleResourceVisibility AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.ItemLayoutTemplateEntity, bankId)
    End Function


End Class
