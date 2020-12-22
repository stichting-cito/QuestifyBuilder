Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.UI

Public Class StaticGroupUIDesignerFactory
    Implements IDataSourceSettingsDesignerFactory

    Public Function CreateDesigner(mode As IDataSourceSettingsDesignerFactory.DesignerMode, settings As DataSourceSettings, config As DataSourceConfig, resourceManager As ResourceManagerBase) As DataSourceUIControlBase Implements IDataSourceSettingsDesignerFactory.CreateDesigner
        Dim configDesignerInstance As DataSourceUIControlBase = Nothing

        Select Case mode
            Case IDataSourceSettingsDesignerFactory.DesignerMode.Config
                configDesignerInstance = New StaticGroupEditor()

            Case IDataSourceSettingsDesignerFactory.DesignerMode.Selection
                Return Nothing
        End Select

        If configDesignerInstance IsNot Nothing Then
            configDesignerInstance.Initialize(settings, config, resourceManager)
        End If

        Return configDesignerInstance
    End Function

End Class
