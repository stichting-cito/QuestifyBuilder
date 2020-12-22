


Public Class ResourceCacheLocationNeededEventArgs
    Inherits EventArgs


    Private _resourceName As String
    Private _resourceNameCacheLocation As String
    Private _resourceNameInclCacheLocation As String




    Public Property ResourceNameInclCacheLocation As String
        Get
            Return _resourceNameInclCacheLocation
        End Get
        Set
            _resourceNameInclCacheLocation = value
        End Set
    End Property

    Public Property ResourceName As String
        Get
            Return _resourceName
        End Get
        Set
            _resourceName = value
        End Set
    End Property

    Public Property ResourceNameCacheLocation As String
        Get
            Return _resourceNameCacheLocation
        End Get
        Set
            _resourceNameCacheLocation = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(name As String)
        _resourceName = name
    End Sub


End Class

