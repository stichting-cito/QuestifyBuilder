Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Factories
Imports Cito.Tester.ContentModel.Datasources
Imports System.Linq

Namespace ContentModel

    Friend Class ItemManipulationInDatasource

        Private ReadOnly _datasource As DataSourceResourceEntity

        Sub New(Datasource As DataSourceResourceEntity)
            _datasource = Datasource
        End Sub

        Public Function Rename(currentItemCode As String, newItemCode As String) As Boolean
            Dim settings As DataSourceSettings = Parsers.ParseItemDataSourceSettingsFromResourceEntity(_datasource)
            If settings IsNot Nothing Then
                Dim itemsRenamed = CType(settings.CreateGetDataSource(), ItemDataSource).RenameItem(currentItemCode, newItemCode)
                If itemsRenamed Then
                    _datasource.SetDataSource(settings)
                    ResourceFactory.Instance.UpdateDataSourceResource(_datasource)
                End If
            Else
                Debug.Assert(False, "Settings not found in datasource")
            End If

            Return True
        End Function

    End Class

End Namespace

