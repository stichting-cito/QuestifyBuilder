
Imports System.Configuration

''' <summary>
''' Handler collection for use in the TestPreviewConfigurationSection.
''' </summary>
Public NotInheritable Class PluginHandlerConfigCollection
    Inherits ConfigurationElementCollection

    ''' <summary>
    ''' When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"></see>.
    ''' </summary>
    ''' <returns>
    ''' A new <see cref="T:System.Configuration.ConfigurationElement"></see>.
    ''' </returns>
    Protected Overrides Function CreateNewElement() As ConfigurationElement
        Return New PluginHandlerConfigElement()
    End Function

    ''' <summary>
    ''' When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"></see>.
    ''' </summary>
    ''' <param name="element">The element.</param>
    ''' <returns>
    ''' A new <see cref="T:System.Configuration.ConfigurationElement"></see>.
    ''' </returns>
    Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
        Return DirectCast(element, PluginHandlerConfigElement).Key
    End Function
    
End Class
