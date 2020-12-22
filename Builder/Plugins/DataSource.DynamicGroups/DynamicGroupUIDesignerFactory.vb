Imports System.Windows.Forms
Imports Cito.Tester.Common

Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.UI

Public Class DynamicGroupUIDesignerFactory
    Implements IDataSourceSettingsDesignerFactory


    Public Function CreateDesigner(ByVal mode As IDataSourceSettingsDesignerFactory.DesignerMode, ByVal settings As DataSourceSettings, ByVal config As DataSourceConfig, ByVal resourceManager As ResourceManagerBase) As DataSourceUIControlBase Implements IDataSourceSettingsDesignerFactory.CreateDesigner
        Dim configDesignerInstance As DataSourceUIControlBase = Nothing
        Dim debugMode As Boolean = ((Control.ModifierKeys And Keys.Shift) = Keys.Shift)

        Select Case mode
            Case IDataSourceSettingsDesignerFactory.DesignerMode.Config
                If debugMode Then
                    configDesignerInstance = New DynamicGroupSourceEditor
                Else
                    configDesignerInstance = New DynamicGroupEditor
                End If
            Case IDataSourceSettingsDesignerFactory.DesignerMode.Selection
        End Select

        If configDesignerInstance IsNot Nothing Then
            configDesignerInstance.Initialize(settings, config, resourceManager)
        End If

        Return configDesignerInstance
    End Function


End Class