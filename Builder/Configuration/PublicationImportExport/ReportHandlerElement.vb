Imports System.Configuration


Public NotInheritable Class ReportHandlerElement
    Inherits PluginHandlerElement


    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(PluginHandlerConfigCollection))>
    Public Overloads ReadOnly Property HandlerConfig As PluginHandlerConfigCollection
        Get
            Return DirectCast(MyBase.Item(""), PluginHandlerConfigCollection)
        End Get
    End Property


End Class