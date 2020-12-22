
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources

Public Class DataSourceReportContainerControl


    Private Function CreateUIFactory(settings As DataSourceSettings) As IDataSourceSettingsDesignerFactory
        Dim UIFactoryType As Type
        Dim UIFactoryInstance As IDataSourceSettingsDesignerFactory

        UIFactoryType = Type.GetType(settings.DataSourceConfigUIFactoryType)
        UIFactoryInstance = CType(Activator.CreateInstance(UIFactoryType), IDataSourceSettingsDesignerFactory)

        Return UIFactoryInstance
    End Function

    Private Sub RemoveReports()
        Dim controlsToBeDeleted As New List(Of Control)

        For Each control As Control In Me.Controls
            If TypeOf control Is DataSourceUIControlBase Then
                controlsToBeDeleted.Add(control)
            End If
        Next

        For Each control As Control In controlsToBeDeleted
            Me.Controls.Remove(control)
        Next
    End Sub



    Public Overridable Sub Initialize(dataSourceWithItems As Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef)), resourceManager As ResourceManagerBase)
        RemoveReports()
        NoConfigLabel.Visible = True

        For Each settingsAndItems As KeyValuePair(Of DataSourceSettings, IEnumerable(Of ResourceRef)) In dataSourceWithItems
            Dim dataSource As IDataSource = settingsAndItems.Key.CreateGetDataSource()
            If TypeOf dataSource Is IDataSourceReporting Then
                Dim reportUIFactory As IDataSourceSettingsDesignerFactory = CreateUIFactory(settingsAndItems.Key)
                Dim reportUI As DataSourceUIControlBase = reportUIFactory.CreateDesigner(IDataSourceSettingsDesignerFactory.DesignerMode.Reporting, settingsAndItems.Key, settingsAndItems.Key.DataSourceConfig, resourceManager)
                If reportUI IsNot Nothing AndAlso TypeOf reportUI Is DataSourceUIReportControlBase Then
                    Dim result As DataSourceReportingResult = DirectCast(dataSource, IDataSourceReporting).GetReport(settingsAndItems.Value, resourceManager)
                    DirectCast(reportUI, DataSourceUIReportControlBase).Initialize(result, settingsAndItems.Key, settingsAndItems.Key.DataSourceConfig, resourceManager)

                    Me.Controls.Add(reportUI)
                    reportUI.Dock = DockStyle.Fill
                    reportUI.BringToFront()

                    NoConfigLabel.Visible = False
                Else
                    Throw New Exception("Datasource implements reporting, but there is no designer available from the factory!")
                End If
            End If
        Next

    End Sub


End Class