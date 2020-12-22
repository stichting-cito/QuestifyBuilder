Imports System.Configuration

Public Class CachedConfigCollection
    Inherits ConfigurationElementCollection

    Protected Overrides Function CreateNewElement() As ConfigurationElement
        Return New CachedElement()
    End Function

    Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
        Return DirectCast(element, CachedElement).Type
    End Function
End Class
