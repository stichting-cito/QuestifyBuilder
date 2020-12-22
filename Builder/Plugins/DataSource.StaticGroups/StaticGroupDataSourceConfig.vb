Imports Cito.Tester.ContentModel.Datasources
Imports System.Xml.Serialization
Imports Questify.Builder.PlugIns.DataSource.StaticGroups.Entities

<XmlRoot("staticGroupDataSourceSettings")>
Public Class StaticGroupDataSourceConfig
    Inherits ItemDataSourceConfig


    Private _groupDefinition As New StaticGroupEntryCollection
    Private _groupEntryType As GroupEntryTypes



    <XmlArray("groupDefinition"),
 XmlArrayItem(GetType(ItemReference), ElementName:="itemReference"),
 XmlArrayItem(GetType(GroupReference), ElementName:="groupReference"),
 XmlArrayItem(GetType(ItemGroup), ElementName:="itemGroup")>
    Public ReadOnly Property GroupDefinition As StaticGroupEntryCollection
        Get
            Return _groupDefinition
        End Get
    End Property

    <XmlAttribute("groupEntryType")>
    Public Property GroupEntryType As GroupEntryTypes
        Get
            Return _groupEntryType
        End Get
        Set
            _groupEntryType = Value
        End Set
    End Property

    Public Sub New()
        _groupEntryType = GroupEntryTypes.Items
    End Sub


End Class
