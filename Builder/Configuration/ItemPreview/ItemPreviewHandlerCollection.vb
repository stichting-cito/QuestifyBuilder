Imports System.Configuration

Public NotInheritable Class ItemPreviewHandlerCollection
    Inherits PluginHandlerCollection

    Protected Overloads Overrides Function CreateNewElement() As ConfigurationElement
        Return New ItemPreviewHandler
    End Function

End Class
