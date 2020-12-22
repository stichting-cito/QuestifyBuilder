Imports Cito.Tester.Common

Namespace Datasources

    Public Interface IDataSourceWithManyOutputSupport(Of T)
        Inherits IDataSource(Of T)


        Function GetMany(resourceManager As ResourceManagerBase, numberOfRequests As Integer) As IList(Of IEnumerable(Of T))


        Function GetAllItemcodes(resourceManager As ResourceManagerBase) As IEnumerable(Of String)
    End Interface

End Namespace
