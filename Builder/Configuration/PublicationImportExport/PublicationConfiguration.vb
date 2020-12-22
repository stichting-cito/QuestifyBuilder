Imports System.Configuration

Public NotInheritable Class PublicationConfiguration
    Inherits PluginConfiguration

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(PublicationHandlerCollection), AddItemName:="handler")> _
    Public Overloads ReadOnly Property Handlers As PublicationHandlerCollection
        Get
            Return DirectCast(MyBase.Item(""), PublicationHandlerCollection)
        End Get
    End Property
End Class
