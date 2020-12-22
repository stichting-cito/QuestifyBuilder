Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources

Public Interface IDataSourceReporting(Of C)
    Inherits IDataSourceReporting

    ReadOnly Property Config() As C

End Interface

Public Interface IDataSourceReporting

    Function GetReport(items As IEnumerable(Of ResourceRef), resourceManager As ResourceManagerBase) As DataSourceReportingResult

End Interface
