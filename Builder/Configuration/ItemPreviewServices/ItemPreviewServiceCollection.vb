Imports System.Configuration

Public Class ItemPreviewServiceCollection
    Inherits ConfigurationElementCollection

    Protected Overrides Function CreateNewElement() As ConfigurationElement
        Return New ItemPreviewServiceElement()
    End Function

    Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
        Return DirectCast(element, ItemPreviewServiceElement).Name
    End Function
End Class
