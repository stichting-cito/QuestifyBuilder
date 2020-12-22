Imports System.Configuration

Public NotInheritable Class ReportConfiguration
    Inherits PluginConfiguration

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(ReportHandlerCollection), AddItemName:="handler")>
    Public Overloads ReadOnly Property Handlers As ReportHandlerCollection
        Get
            Return DirectCast(MyBase.Item(""), ReportHandlerCollection)
        End Get
    End Property
End Class