Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

Imports Questify.Builder.Plugins.Reports.Excel.My.Resources


Public Class ResourceReferencesReport
    Inherits MediaReferencesReportBase

    Public Overrides ReadOnly Property Name As String = ResourcesReferencedBy_Name
    Public Overrides ReadOnly Property Description As String = ResourcesReferencedBy_Description

    Public Overrides Function IsDatasourceSupported() As Boolean
        Return _selectedEntities.Any()
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl
        _optionValidator.Filename = $"{ResourceReferenceBy_ReportName} - {_selectedEntities.FirstOrDefault()?.BankName}"
        Dim selectReportLocation As New SelectReportLocation
        If String.IsNullOrEmpty(ReportSettings.ExcelReport) Then _
            ReportSettings.ExcelReport = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        _optionValidator.ExportPath = Path.Combine(ReportSettings.ExcelReport, String.Format("{0}.xlsx", _optionValidator.Filename))
        _optionValidator.OverwriteExisting = False
        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function

    Protected Overrides Sub AddColumnsToDataTable()
        If _selectedEntities.OfType(Of ItemResourceDto).Any() Then
            _datatable.Columns.Add(DatatableColumn_ItemId)
        End If

        _datatable.Columns.Add(DatatableColumn_Code)
        _datatable.Columns.Add(DatatableColumn_Title)
        _datatable.Columns.Add(DatatableColumn_References)
    End Sub

    Protected Overrides Sub FillDatatable()
        Dim request = New ResourceRequestDTO With {.WithReferences = True}
        Using resources As EntityCollection = ResourceFactory.Instance.GetResourcesByIdsWithOption(Collection.Select(Function(dto) dto.resourceId).ToList(), request)
            For Each resource In resources
                FillDatatableForResource(resource)
            Next
        End Using
    End Sub

    Private Sub FillDatatableForResource(resource As ResourceEntity)
        Dim row = _datatable.NewRow()

        If TypeOf resource Is ItemResourceEntity Then
            row.Item(DatatableColumn_ItemId) = CType(resource, ItemResourceEntity).ItemId
        End If

        row.Item(DatatableColumn_Code) = resource.Name
        row.Item(DatatableColumn_Title) = resource.Title
        row.Item(DatatableColumn_References) = String.Join("; ", resource.ReferencedResourceCollection.Select(Function(refR) refR.Resource.Name))

        _datatable.Rows.Add(row)
    End Sub
End Class
