Imports System.Configuration

Public Class ItemPreviewServiceElement
    Inherits ConfigurationElement

    <ConfigurationProperty("name", defaultvalue:="", IsKey:=True, IsRequired:=True)> _
    Public Property Name As String
        Get
            Return DirectCast(Me("name"), String)
        End Get

        Set
            Me("name") = value
        End Set
    End Property

    <ConfigurationProperty("url", IsKey:=False, IsRequired:=True)> _
    Public Property Url As String
        Get
            Return DirectCast(Me("url"), String)
        End Get
        Set
            Me("url") = value
        End Set
    End Property

    <ConfigurationProperty("type", IsKey:=False, IsRequired:=True)> _
    Public Property Type As String
        Get
            Return DirectCast(MyBase.Item("type"), String)
        End Get
        Set
            MyBase.Item("type") = value
        End Set
    End Property
End Class
