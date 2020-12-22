Imports System.Xml
Imports System.Xml.Serialization
Namespace Versioning
    <Serializable()>
    <Serialization.XmlRoot(ElementName:="DependentResource")> _
    Public Class DependentResourceMetaData
        Inherits MetaDataBase

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Guid, ByVal name As String, ByVal version As String)
            MyBase.New(id, name, version)
        End Sub

        <XmlIgnore()> _
        Public Overrides ReadOnly Property Category As String
            Get
                Return My.Resources.Category_DependentResources
            End Get
        End Property
    End Class
End Namespace