Imports System.Configuration

Public NotInheritable Class ReportHandlerCollection
    Inherits PluginHandlerCollection

    Protected Overloads Overrides Function CreateNewElement() As ConfigurationElement
        Return New ReportHandlerElement()
    End Function
End Class