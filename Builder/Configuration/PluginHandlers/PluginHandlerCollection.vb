Imports System.Configuration
Imports System.Diagnostics.CodeAnalysis

<SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")> _
Public Class PluginHandlerCollection
    Inherits ConfigurationElementCollection

    Protected Overloads Overrides Function CreateNewElement() As ConfigurationElement
        Return New PluginHandlerElement()
    End Function

    Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
        Return DirectCast(element, PluginHandlerElement).Type
    End Function

End Class
