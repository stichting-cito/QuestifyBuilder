Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources

Public Class DataSourceUIReportControlBase

    Private Shadows Sub Initialize(settings As DataSourceSettings, DataSourceConfig As DataSourceConfig, resourceManager As ResourceManagerBase)
        MyBase.Initialize(settings, DataSourceConfig, resourceManager)
    End Sub

    Public Overridable Shadows Sub Initialize(result As DataSourceReportingResult, settings As DataSourceSettings, dataSourceConfig As DataSourceConfig, resourceManager As ResourceManagerBase)
        Throw New NotImplementedException()
    End Sub

End Class
