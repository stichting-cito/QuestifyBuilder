Public Class SectionLogicSettingsDependencyChangedEventArgs
    Inherits EventArgs

    Public Property ResourceName() As String

    Public Sub New(resourceName As String)
        Me.ResourceName = resourceName
    End Sub

End Class
