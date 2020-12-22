Imports System.Configuration

Public Class CachedElement
    Inherits ConfigurationElement

    <ConfigurationProperty("type", defaultvalue:="", IsKey:=True, IsRequired:=True)> _
    Public Property Type As String
        Get
            Return DirectCast(Me("type"), String)
        End Get

        Set
            Me("type") = value
        End Set
    End Property

    <ConfigurationProperty("timeInSeconds", defaultvalue:="10", IsKey:=False, IsRequired:=True)> _
    Public Property TimeInSeconds As Integer
        Get
            Return DirectCast(Me("timeInSeconds"), Integer)
        End Get
        Set
            Me("timeInSeconds") = value
        End Set
    End Property

    <ConfigurationProperty("sliding", defaultvalue:="true", IsKey:=False, IsRequired:=True)> _
    Public Property Sliding As Boolean
        Get
            Return DirectCast(Me("sliding"), Boolean)
        End Get
        Set
            Me("sliding") = value
        End Set
    End Property

    <ConfigurationProperty("maxSizeInKb", defaultvalue:="50", IsKey:=False, IsRequired:=False)> _
    Public Property MaxSizeInKb As Integer
        Get
            Return DirectCast(Me("maxSizeInKb"), Integer)
        End Get
        Set
            Me("maxSizeInKb") = value
        End Set
    End Property
End Class
