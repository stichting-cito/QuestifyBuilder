Imports System.Configuration

Public NotInheritable Class PluginHandlerConfigElement
    Inherits ConfigurationElement

    <ConfigurationProperty("key", DefaultValue:="", IsKey:=True, IsRequired:=True)>
    Public Property Key As String
        Get
            Return DirectCast(Me("key"), String)
        End Get
        Set
            Me("key") = value
        End Set
    End Property

    <ConfigurationProperty("value", DefaultValue:="", IsKey:=False, IsRequired:=True)>
    Public Property Value As String
        Get
            Return DirectCast(Me("value"), String)
        End Get

        Set
            Me("value") = value
        End Set
    End Property

End Class
