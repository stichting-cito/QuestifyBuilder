Imports Cito.Tester.Common
Imports System.Configuration

Public Class CacheConfiguration
    Inherits ProtectedSection

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(PluginHandlerCollection), AddItemName:="cachedEntity")> _
    Public ReadOnly Property CachedEntities As CachedConfigCollection
        Get
            Return DirectCast(MyBase.Item(""), CachedConfigCollection)
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

End Class
