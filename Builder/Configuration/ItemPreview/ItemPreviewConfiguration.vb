Imports System.Configuration

Public NotInheritable Class ItemPreviewerConfiguration
    Inherits PluginConfiguration

    Public Sub New()
        MyBase.New()
    End Sub

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(ItemPreviewHandlerCollection), AddItemName:="handler")> _
    Public Overloads ReadOnly Property Handlers As ItemPreviewHandlerCollection
        Get
            Return DirectCast(MyBase.Item(""), ItemPreviewHandlerCollection)
        End Get
    End Property
End Class
