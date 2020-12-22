Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources

Namespace Faketory.customMocks.datasources

    Public Class MockedItemDataSource
        Inherits ItemDataSource

        Sub New(config As ItemDataSourceConfig)
            MyBase.New(config)

        End Sub

        Public Overrides Function [Get](resourceManager As ResourceManagerBase) As IEnumerable(Of ResourceRef)
            Dim itms As MockedItemDataSourceConfig = DirectCast(Config, MockedItemDataSourceConfig)
            Return (From a In itms.GroupDefinition Select New ResourceRef(a))
        End Function

    End Class
End NameSpace