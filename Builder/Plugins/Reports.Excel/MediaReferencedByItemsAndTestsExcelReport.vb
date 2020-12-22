Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Plugins.Reports.Excel.My.Resources
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Cito.Tester.ContentModel
Imports HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

Public Class MediaReferencedByItemsAndTestsExcelReport
    Inherits MediaReferencesReportBase

    Public Overrides ReadOnly Property Name As String
        Get
            If _selectedEntities.Any Then
                Dim entityBase2 = ResourceFactory.Instance.GetResourceByNameWithOption(BankId, _selectedEntities(0).Name, New ResourceRequestDTO())
                Select Case entityBase2.GetType.ToString
                    Case GetType(AssessmentTestResourceEntity).ToString
                        Return MediaReferencedByTestsExcelReport_Name
                    Case GetType(ItemResourceEntity).ToString
                        Return MediaReferencedByItemsExcelReport_Name
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property Description As String
        Get
            If _selectedEntities.Any Then
                Dim entityBase2 = ResourceFactory.Instance.GetResourceByNameWithOption(BankId, _selectedEntities(0).Name, New ResourceRequestDTO())
                Select Case entityBase2.GetType.ToString
                    Case GetType(AssessmentTestResourceEntity).ToString
                        Return MediaReferencedByTestsExcelReport_Description
                    Case GetType(ItemResourceEntity).ToString
                        Return MediaReferencedByItemsExcelReport_Description
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public Overrides Function IsDatasourceSupported() As Boolean
        Return _selectedEntities IsNot Nothing AndAlso
               (_selectedEntities.OfType(Of AssessmentTestResourceDto).Any OrElse
                _selectedEntities.OfType(Of ItemResourceDto).Any)
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl
        Dim firstDto = _selectedEntities.FirstOrDefault()
        If firstDto IsNot Nothing Then
            Select Case firstDto.GetType
                Case GetType(AssessmentTestResourceDto)
                    _optionValidator.Filename = String.Format("Media in toets - {0}", firstDto.Title)
                Case GetType(ItemResourceDto)
                    _optionValidator.Filename = String.Format("Media in items - {0}", firstDto.BankName)
            End Select
        End If
        Dim selectReportLocation As New SelectReportLocation
        If String.IsNullOrEmpty(ReportSettings.MediaReferencedByEntities) Then _
            ReportSettings.MediaReferencedByEntities = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        _optionValidator.ExportPath = Path.Combine(ReportSettings.MediaReferencedByEntities,
                                                   String.Format("{0}.xlsx", _optionValidator.Filename))
        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function


    Protected Overrides Sub AddColumnsToDataTable()
        _datatable.Columns.Add(DatatableColumn_Itemcode)
        _datatable.Columns.Add(DatatableColumn_Itemtitle)
        _datatable.Columns.Add(DatatableColumn_Mediacode)
        _datatable.Columns.Add(DatatableColumn_Mediatitle)
        _datatable.Columns.Add(DatatableColumn_Mediatype)
        _datatable.Columns.Add(DatatableColumn_Filesize)
        _datatable.Columns.Add(DatatableColumn_FileWidth)
        _datatable.Columns.Add(DatatableColumn_FileHeight)
        _datatable.Columns.Add(DatatableColumn_FileWidthInItem)
        _datatable.Columns.Add(DatatableColumn_FileHeightInItem)
    End Sub

    Protected Overrides Sub FillDatatable()
        Dim itemResources As EntityCollectionBase2(Of ItemResourceEntity) = New ItemResourceEntityCollection()

        Dim mediaResourceCustomPropertyValues As New Dictionary(Of Guid, List(Of Tuple(Of String, String)))

        Dim request = New ResourceRequestDTO With {.WithDependencies = True}
        If TypeOf Collection.FirstOrDefault Is AssessmentTestResourceDto Then

            For Each testDto As ResourceDto In Collection
                Dim test = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(testDto.ResourceId, New AssessmentTestResourceEntityFactory(), request), AssessmentTestResourceEntity)
                Dim itemsInTest As EntityCollectionBase2(Of ItemResourceEntity) = New ItemResourceEntityCollection()
                ExcelReportHelper.GetAllItemsInTestCollection(test, itemsInTest, True, False)
                For Each itemInTest In itemsInTest
                    itemResources.Add(itemInTest)
                Next
                itemsInTest.Dispose()
                itemsInTest = Nothing
            Next
        ElseIf TypeOf Collection.FirstOrDefault Is ItemResourceDto Then
            Using items As EntityCollection = ResourceFactory.Instance.GetResourcesByIdsWithOption(Collection.Select(Function(r) r.ResourceId).ToList(), New ItemResourceEntityFactory(), request)
                itemResources.AddRange(items.OfType(Of ItemResourceEntity))
            End Using
        End If

        If itemResources.Any() Then
            Dim cps = BankFactory.Instance.GetCustomBankPropertiesForBranchById(itemResources.First().BankId, Enums.ResourceTypeEnum.GenericResource).OfType(Of CustomBankPropertyEntity)
            If cps IsNot Nothing AndAlso cps.Any() Then
                cps.ToList().ForEach(Sub(cp)
                                         If Not _datatable.Columns.Contains($"[{cp.Name}]") Then
                                             _datatable.Columns.Add($"[{cp.Name}]")
                                         End If
                                     End Sub)
            End If


            For Each itemResource In itemResources
                Dim mediaRefs = itemResource.DependentResourceCollection.Items.Where(Function(dre) TypeOf dre.DependentResource Is GenericResourceEntity).ToList

                If mediaRefs.Any() Then
                    Dim resourcesToRetrieve = mediaRefs.Select(Function(m) m.DependentResourceId).Except(mediaResourceCustomPropertyValues.Keys)
                    If resourcesToRetrieve.Any() Then
                        Dim resourcesAndCps = BankFactory.Instance.GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(cps.Select(Function(cp) DirectCast(cp, CustomBankPropertyEntity).CustomBankPropertyId).ToList(), resourcesToRetrieve.ToList(), False).OfType(Of CustomBankPropertyValueEntity)
                        resourcesAndCps.ToList().ForEach(Sub(rcp)
                                                             Dim cpCode = cps.First(Function(cp) cp.CustomBankPropertyId = rcp.CustomBankPropertyId).Name

                                                             If Not mediaResourceCustomPropertyValues.ContainsKey(rcp.ResourceId) Then
                                                                 mediaResourceCustomPropertyValues.Add(rcp.ResourceId, New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)(cpCode, rcp.DisplayValue)})
                                                             Else
                                                                 mediaResourceCustomPropertyValues(rcp.ResourceId).Add(New Tuple(Of String, String)(cpCode, rcp.DisplayValue))
                                                             End If
                                                         End Sub)
                    End If
                End If

                If Not mediaRefs.Any Then
                    mediaRefs.Add(New DependentResourceEntity())
                End If
                Dim assessmentitem = itemResource.GetAssessmentItem()
                Dim itemParams As List(Of ParameterBase) = assessmentitem.Parameters.GetParametersWithResources
                For Each mediaRef In mediaRefs
                    If Item IsNot Nothing Then
                        If mediaRef.DependentResource Is Nothing Then
                            _datatable.Rows.Add(GetDatarow(itemResource, Nothing, Nothing, Nothing))
                        Else
                            Dim knownMediaResources As List(Of Tuple(Of String, String)) = Nothing
                            If mediaResourceCustomPropertyValues.ContainsKey(mediaRef.DependentResourceId) Then
                                knownMediaResources = mediaResourceCustomPropertyValues(mediaRef.DependentResourceId)
                            End If

                            For Each param In itemParams
                                If TypeOf param Is XHtmlParameter Then
                                    For Each inlineElement In DirectCast(param, XHtmlParameter).GetInlineElements().Where(Function(ie) ie.Value.Parameters.GetParametersWithResources.Any(Function(p) TypeOf p Is ResourceParameter AndAlso DirectCast(p, ResourceParameter).Value.Equals(mediaRef.DependentResource.Name, StringComparison.InvariantCultureIgnoreCase))).Select(Function(ie) ie.Value)
                                        For Each resourcePrm In inlineElement.Parameters.GetParametersWithResources.Where(Function(p) TypeOf p Is ResourceParameter AndAlso DirectCast(p, ResourceParameter).Value.Equals(mediaRef.DependentResource.Name, StringComparison.InvariantCultureIgnoreCase)).OfType(Of ResourceParameter)
                                            If resourcePrm.Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(resourcePrm.Value.Trim()) Then
                                                _datatable.Rows.Add(EnrichDataRowWithCustomPropertyValues(GetDatarow(itemResource, DirectCast(mediaRef.DependentResource, GenericResourceEntity), resourcePrm, inlineElement), knownMediaResources))
                                            End If
                                        Next
                                    Next
                                ElseIf TypeOf param Is ResourceParameter Then
                                    Dim r = DirectCast(param, ResourceParameter)
                                    If r.Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Value.Trim()) AndAlso r.Value.Equals(mediaRef.DependentResource.Name, StringComparison.InvariantCultureIgnoreCase) Then
                                        _datatable.Rows.Add(EnrichDataRowWithCustomPropertyValues(GetDatarow(itemResource, DirectCast(mediaRef.DependentResource, GenericResourceEntity), r, Nothing), knownMediaResources))
                                    End If
                                End If
                            Next

                        End If
                    End If
                Next
            Next
        End If

        mediaResourceCustomPropertyValues = Nothing
        itemResources.Dispose()
        itemResources = Nothing
    End Sub

    Protected Overrides Function GetDatarow(item As ItemResourceEntity, mediaRef As GenericResourceEntity, resourcePrm As ResourceParameter, inlineElement As InlineElement) As DataRow
        Dim row = MyBase.GetDatarow(item, mediaRef, resourcePrm, inlineElement)
        If mediaRef IsNot Nothing Then
            row.Item(DatatableColumn_Filesize) = String.Format("{0} KB", mediaRef.Size)
        End If
        Return row
    End Function

    Private Function EnrichDataRowWithCustomPropertyValues(row As DataRow, cpdvForMedia As List(Of Tuple(Of String, String))) As DataRow
        cpdvForMedia?.ForEach(Sub(cp)
                                  If row.Item($"[{cp.Item1}]") IsNot Nothing Then
                                      row.Item($"[{cp.Item1}]") = cp.Item2
                                  End If
                              End Sub)
        Return row
    End Function

End Class
