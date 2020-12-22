Public Class GridInUseAsItemGridBase
    inherits GridBase

    Public Overrides ReadOnly Property AllowAddNew() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides ReadOnly Property AllowDelete() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides ReadOnly Property AllowEdit() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides ReadOnly Property AllowMoveResources As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides ReadOnly Property AllowShowProperties() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property

    Public Overrides ReadOnly Property AllowSynchronize() As Boolean
        Get
            Return Not UseGridAsItemPicker
        End Get
    End Property
End Class
