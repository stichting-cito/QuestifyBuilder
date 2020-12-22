Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Plugins.Reports.Excel.My.Resources
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Public Class ReferencedMediaExcelExport
    Inherits MediaReferencesReportBase


    Public Overrides ReadOnly Property Name As String
        Get
            If _selectedEntities.Any Then
                Dim entityBase2 = ResourceFactory.Instance.GetResourceByNameWithOption(BankId, _selectedEntities(0).Name, New ResourceRequestDTO())
                Select Case entityBase2.GetType.ToString
                    Case GetType(GenericResourceEntity).ToString
                        Return ReferencedMediaExcelExport_Name
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property Description As String
        Get
            Return ReferencedMediaExcelExport_Description
        End Get
    End Property

    Public Overrides Function IsDatasourceSupported() As Boolean
        Return _selectedEntities IsNot Nothing AndAlso _selectedEntities.OfType(Of GenericResourceDto).Any
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl
        _optionValidator.Filename = String.Format("Referenced media - {0}", _selectedEntities.FirstOrDefault().BankName)
        Dim selectReportLocation As New SelectReportLocation
        If String.IsNullOrEmpty(ReportSettings.MediaReferencesReport) Then _
            ReportSettings.MediaReferencesReport = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        _optionValidator.ExportPath = Path.Combine(ReportSettings.MediaReferencesReport,
                                                   String.Format("{0}.xlsx", _optionValidator.Filename))
        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function



    Protected Overrides Sub AddColumnsToDataTable()
        _datatable.Columns.Add(DatatableColumn_Mediacode)
        _datatable.Columns.Add(DatatableColumn_Mediatitle)
        _datatable.Columns.Add(DatatableColumn_Mediatype)
        _datatable.Columns.Add(DatatableColumn_FileWidth)
        _datatable.Columns.Add(DatatableColumn_FileHeight)
        _datatable.Columns.Add(DatatableColumn_Itemcode)
        _datatable.Columns.Add(DatatableColumn_Itemtitle)
        _datatable.Columns.Add(DatatableColumn_FileWidthInItem)
        _datatable.Columns.Add(DatatableColumn_FileHeightInItem)
    End Sub

    Protected Overrides Sub FillDatatable()

        For Each mediaDto As GenericResourceDto In Collection
            Dim rowAdded As Boolean = False
            Dim request = New ResourceRequestDTO With {.WithReferences = True}
            Dim mediaResource = TryCast(ResourceFactory.Instance.GetResourceByIdWithOption(mediaDto.ResourceId, New GenericResourceEntityFactory(), request), GenericResourceEntity)

            For Each resourceEntity In ResourceFactory.Instance.GetResourcesByIdsWithOption(mediaResource.ReferencedResourceCollection.Items.Select(Function(i) i.ResourceId).ToList(), New ResourceRequestDTO()).OfType(Of ItemResourceEntity)
                Dim assessmentItem = DirectCast(resourceEntity, ItemResourceEntity).GetAssessmentItem()

                For Each param In assessmentItem.Parameters.GetParametersWithResources
                    If TypeOf param Is XHtmlParameter Then
                        For Each inlineElement In DirectCast(param, XHtmlParameter).GetInlineElements().Where(Function(ie) ie.Value.Parameters.GetParametersWithResources.Any(Function(p) TypeOf p Is ResourceParameter AndAlso DirectCast(p, ResourceParameter).Value.Equals(mediaResource.Name, StringComparison.InvariantCultureIgnoreCase))).Select(Function(ie) ie.Value)
                            For Each resourcePrm In inlineElement.Parameters.GetParametersWithResources.Where(Function(p) TypeOf p Is ResourceParameter AndAlso DirectCast(p, ResourceParameter).Value.Equals(mediaResource.Name, StringComparison.InvariantCultureIgnoreCase)).OfType(Of ResourceParameter)
                                If resourcePrm.Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(resourcePrm.Value.Trim()) Then
                                    _datatable.Rows.Add(GetDatarow(DirectCast(resourceEntity, ItemResourceEntity), mediaResource, resourcePrm, inlineElement))
                                    rowAdded = True
                                End If
                            Next
                        Next
                    ElseIf TypeOf param Is ResourceParameter Then
                        Dim r = DirectCast(param, ResourceParameter)
                        If r.Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Value.Trim()) AndAlso r.Value.Equals(mediaResource.Name, StringComparison.InvariantCultureIgnoreCase) Then
                            _datatable.Rows.Add(GetDatarow(DirectCast(resourceEntity, ItemResourceEntity), mediaResource, r, Nothing))
                            rowAdded = True
                        End If
                    End If
                Next
            Next
            If Not rowAdded Then _datatable.Rows.Add(GetDatarow(Nothing, mediaResource, Nothing, Nothing))
        Next
        _resultText = String.Empty
    End Sub

End Class
