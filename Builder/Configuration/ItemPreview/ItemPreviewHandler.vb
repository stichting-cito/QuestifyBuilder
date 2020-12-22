Imports System.Configuration

Public NotInheritable Class ItemPreviewHandler
    Inherits PluginHandlerElement

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(PluginHandlerConfigCollection))> _
    Public Overloads ReadOnly Property HandlerConfig As PluginHandlerConfigCollection
        Get
            Return DirectCast(MyBase.Item(""), PluginHandlerConfigCollection)
        End Get
    End Property

End Class


