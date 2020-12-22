Imports System.Configuration
Imports System.Diagnostics.CodeAnalysis

<SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")> _
Public NotInheritable Class PublicationHandlerCollection
    Inherits PluginHandlerCollection

    Protected Overloads Overrides Function CreateNewElement() As ConfigurationElement
        Return New PublicationHandlerElement()
    End Function

End Class


