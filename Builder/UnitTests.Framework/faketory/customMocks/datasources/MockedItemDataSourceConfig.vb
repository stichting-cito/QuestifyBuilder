Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel.Datasources

Namespace Faketory.customMocks.datasources

    <XmlRoot("mockedGroupDataSourceConfig")> _
    Public Class MockedItemDataSourceConfig
        Inherits ItemDataSourceConfig

        Private _groupDefinition As New List(Of String)

        <XmlArray("groupDefinition")>
        Public ReadOnly Property GroupDefinition() As List(Of String)
            Get
                Return _groupDefinition
            End Get
        End Property

    End Class
End NameSpace