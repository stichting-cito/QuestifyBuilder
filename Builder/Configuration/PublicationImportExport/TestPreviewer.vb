Imports System.Configuration

Public Class TestPreviewer
    Inherits ConfigurationElement
    <ConfigurationProperty("name", IsKey:=True, IsRequired:=True)> _
    Public Property Name As String
        Get
            Return DirectCast(Me("name"), String)
        End Get
        Set
            Me("name") = value
        End Set
    End Property

    <ConfigurationProperty("url", IsKey:=False, IsRequired:=False, DefaultValue:="")> _
    Public Property Url As String
        Get
            Return DirectCast(Me("url"), String)
        End Get
        Set
            Me("url") = value
        End Set
    End Property

    <ConfigurationProperty("defaultClient", IsKey:=False, IsRequired:=False, DefaultValue:="")> _
    Public Property DefaultClient As String
        Get
            Return DirectCast(MyBase.Item("defaultClient"), String)
        End Get
        Set
            MyBase.Item("defaultClient") = value
        End Set
    End Property

    <ConfigurationProperty("clickOnce", IsKey:=False, IsRequired:=False, DefaultValue:=False)> _
    Public Property ClickOnce As Boolean
        Get
            Return DirectCast(MyBase.Item("clickOnce"), Boolean)
        End Get
        Set
            MyBase.Item("clickOnce") = value
        End Set
    End Property

End Class
