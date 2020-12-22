Public Class ToggleNavigationBarEventArgs
    Inherits EventArgs

    Private _enabled As Boolean
    Private _targetNavigationBarPart As NavigationBarPart

    Public ReadOnly Property TargetNavigationBarPart As NavigationBarPart
        Get
            Return _targetNavigationBarPart
        End Get
    End Property

    Public ReadOnly Property Enabled As Boolean
        Get
            Return _enabled
        End Get
    End Property

    Public Sub New(targetNavigationBarPart As NavigationBarPart, enabled As Boolean)
        _targetNavigationBarPart = targetNavigationBarPart
        _enabled = enabled
    End Sub

    Public Sub New(enabled As Boolean)
        Me.New(NavigationBarPart.EntireNavigationBar, enabled)
    End Sub

End Class
