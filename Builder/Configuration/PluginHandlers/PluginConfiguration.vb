Imports Cito.Tester.Common
Imports System.Configuration

Public Class PluginConfiguration
    Inherits ProtectedSection

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(PluginHandlerCollection), AddItemName:="handler")>
    Public ReadOnly Property Handlers As PluginHandlerCollection
        Get
            Return DirectCast(MyBase.Item(""), PluginHandlerCollection)
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

End Class

