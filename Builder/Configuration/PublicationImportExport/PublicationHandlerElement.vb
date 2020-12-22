Imports System.Configuration


Public NotInheritable Class PublicationHandlerElement
    Inherits PluginHandlerElement

    <ConfigurationProperty("requireRestrictedPackagePublicationPermission", IsKey:=False, IsRequired:=True)> _
    Public Property RequireRestrictedPackagePublicationPermission As Boolean
        Get
            Return DirectCast(MyBase.Item("requireRestrictedPackagePublicationPermission"), Boolean)
        End Get
        Set
            MyBase.Item("requireRestrictedPackagePublicationPermission") = value
        End Set
    End Property

    <ConfigurationProperty("order", IsKey:=False, IsRequired:=False, DefaultValue:=0)> _
    Public Property Order As Int32
        Get
            Return DirectCast(MyBase.Item("order"), Int32)
        End Get
        Set
            MyBase.Item("order") = value
        End Set
    End Property

    <ConfigurationProperty("testpreviewers", IsDefaultCollection:=False), ConfigurationCollection(GetType(TestPreviewers), AddItemName:="testpreviewer")> _
    Public ReadOnly Property TestPreviewers As TestPreviewers
        Get
            Return DirectCast(MyBase.Item("testpreviewers"), TestPreviewers)
        End Get
    End Property

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(PluginHandlerConfigCollection))> _
    Public Overloads ReadOnly Property HandlerConfig As PluginHandlerConfigCollection
        Get
            Return DirectCast(MyBase.Item(""), PluginHandlerConfigCollection)
        End Get
    End Property

End Class