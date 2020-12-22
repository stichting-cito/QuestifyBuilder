Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources

Public Interface IDataSourceSettingsDesignerFactory

    Enum DesignerMode
        Config = 0
        Selection = 1
        Reporting = 2
    End Enum

    Function CreateDesigner(mode As DesignerMode, settings As DataSourceSettings, config As DataSourceConfig, resourceManager As ResourceManagerBase) As DataSourceUIControlBase

End Interface
