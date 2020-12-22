Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories

Public Class ItemDataSourcePreview


    Protected Overrides Sub PreviewDataSource(ByVal settings As Datasources.DataSourceSettings, ByVal datasource As Datasources.IDataSource)
        Dim itemDataSource As Datasources.ItemDataSource = CType(datasource, Datasources.ItemDataSource)
        Dim result As IEnumerable(Of Datasources.ResourceRef) = Nothing

        Try
            result = itemDataSource.Get(Me.ResourceManager)
        Catch ex As Exception
            ExceptionHelper.ShowDialog(ex)
        End Try

        If result IsNot Nothing Then
            Dim fullItemCollection As New List(Of ItemResourceEntity)
            Dim currentBankId As Integer = ActionCommand.Instance.CurrentBankId
            Dim identifiers As IList(Of String) = CType(Datasources.ResourceRef.ToIdentifierList(result), Generic.IList(Of String))
            Dim request = New ItemResourceRequestDTO() With {.WithReportData = True}
            fullItemCollection.AddRange(ResourceFactory.Instance.GetItemsByCodes(identifiers.ToList, currentBankId, request))
            Me.ResultItemGrid.DataSource = fullItemCollection
            OnDataSourcePreviewExecuted(New DataSourcePreviewExecutedEventArgs(settings, itemDataSource, result))
        End If
    End Sub


End Class
