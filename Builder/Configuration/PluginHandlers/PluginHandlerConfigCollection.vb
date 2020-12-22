
Imports System.Configuration

Public NotInheritable Class PluginHandlerConfigCollection
    Inherits ConfigurationElementCollection

    Protected Overrides Function CreateNewElement() As ConfigurationElement
        Return New PluginHandlerConfigElement()
    End Function

    Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
        Return DirectCast(element, PluginHandlerConfigElement).Key
    End Function

End Class
