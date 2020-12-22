Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel.Datasources

<XmlRoot("dynamicGroupDataSourceSettings")> _
Public Class DynamicGroupDatasourceConfig
    Inherits ItemDataSourceConfig


    Private _query As ItemQuery = New ItemQuery



    Public Sub New()
    End Sub



    <XmlElement("Query")> _
    Public Property Query() As ItemQuery
        Get
            Return _query
        End Get
        Set(ByVal value As ItemQuery)
            _query = value
        End Set
    End Property


End Class