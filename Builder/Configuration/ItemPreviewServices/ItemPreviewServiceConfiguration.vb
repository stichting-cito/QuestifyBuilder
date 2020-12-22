Imports Cito.Tester.Common
Imports System.Configuration
Public Class ItemPreviewServiceConfiguration
    Inherits ProtectedSection

    <ConfigurationProperty("", IsDefaultCollection:=True), ConfigurationCollection(GetType(PluginHandlerCollection), AddItemName:="itemPreviewService")> _
    Public ReadOnly Property ItemPreviewServices As ItemPreviewServiceCollection
        Get
            Return DirectCast(MyBase.Item(""), ItemPreviewServiceCollection)
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub
End Class
