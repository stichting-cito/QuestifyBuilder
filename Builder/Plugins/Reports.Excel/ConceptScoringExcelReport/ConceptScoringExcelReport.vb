Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Scoring.Reporting
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Conversion
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.UI
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ConceptScoringExcelReport
    Inherits ExcelReportBase
    Implements IReportHandlerWithConfig

    Private Enum ReportDataTableColumnNames
        BankName
        ItemCode
        ItemId
        ItemTitle
        Itemlayouttemplate
        ConceptCode
        ConceptResponseLabel
        KeyValuesAndConceptScores
        AdditionalKeyValuesAndConceptScores
        IsGrouped
        GroupElementCount
        InteractionCount
        AttributeLevelConceptResponseCount
        SubAttributLevelConceptResponseCount
    End Enum


    Private _optionValidator As SelectColumnsOptionsValidator
    Private _resultText As String

    Private ReadOnly _bankDictionary As New Dictionary(Of String, String)
    Private _bankId As Integer

    Private _itemScoringDataFetcher As ItemScoringReportDataFetcher
    Private _datatable As DataTable


    Public Property SelectedEntities() As IList(Of ResourceDto) Implements IReportHandler.Collection

    Public Overrides ReadOnly Property Description() As String Implements IReportHandler.Description
        Get
            Return My.Resources.ReportDescriptionConceptScoring
        End Get
    End Property

    Public Overrides ReadOnly Property ResultText() As String Implements IReportHandler.ResultText
        Get
            Return _resultText
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String Implements IReportHandler.Name
        Get
            Return My.Resources.ReportNameConceptScoring
        End Get
    End Property

    Public Overrides ReadOnly Property Overview() As String Implements IReportHandler.Overview
        Get
            If _optionValidator IsNot Nothing Then
                Return String.Format(My.Resources.OverviewConceptScoring, vbCrLf, Me.Name, _optionValidator.ExportPath, IIf(_optionValidator.IncludeConceptsWithoutScore, My.Resources.Yes, My.Resources.No))
            End If
            Return String.Empty
        End Get
    End Property


    Public ReadOnly Property GenerateDataAsync() As Boolean Implements IReportHandler.IsDataGeneratedAsynchronous
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ExportedReportLocation() As String Implements IReportHandler.ExportedReportLocation
        Get
            If _optionValidator IsNot Nothing Then
                Return _optionValidator.ExportPath
            End If
            Return Nothing
        End Get
    End Property


    Public Overrides Property BankId As Integer Implements IReportValidationBase.BankId
        Set(ByVal value As Integer)
            _bankId = value
        End Set
        Get
            Return _bankId
        End Get
    End Property

    Public Overrides ReadOnly Property ValidationErrors() As String Implements IReportHandler.ValidationErrors
        Get
            Dim returnValue As String = String.Empty
            If _optionValidator IsNot Nothing Then
                returnValue = _optionValidator.Error
            End If
            Return returnValue
        End Get
    End Property

    Public Overrides ReadOnly Property ExtraOptionTask() As String Implements IReportHandler.ExtraOptionTask
        Get
            Return My.Resources.ExtraOptionConceptcodesTask
        End Get
    End Property

    Public Overrides ReadOnly Property ExtraOptionTaskDescription() As String Implements IReportHandler.ExtraOptionTaskDescription
        Get
            Return My.Resources.ExtraOptionConceptcodesTaskDescription
        End Get
    End Property


    Public Overrides Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI
        Dim conceptOptionsUI As New ConceptScoringOptions
        conceptOptionsUI.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return conceptOptionsUI
    End Function

    Public Property HandlerConfig As PluginHandlerConfigCollection Implements IReportHandlerWithConfig.HandlerConfig

    Public Overrides Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Dim returnValue As Boolean = False
        Try
            If SelectedEntities IsNot Nothing AndAlso SelectedEntities.Any() Then
                Dim entities = ResourceFactory.Instance.GetResourcesByIdsWithOption(SelectedEntities.Select(Function(a) a.ResourceId).ToList, New ResourceRequestDTO()).OfType(Of ResourceEntity).ToList
                _datatable = GetDatatable(entities)

                If _datatable IsNot Nothing Then
                    Dim exporter As New ExcelExport()

                    exporter.ExportDataTable(_datatable, True, _optionValidator.ExportPath)

                    _resultText = String.Format(My.Resources.ReportSuccessful, Me.Name)
                    returnValue = True
                End If
            End If
        Catch ioException As IOException
            _resultText = My.Resources.ErrorWhileExportingToExcelFileMightBeInUse
        Catch ex As Exception
            _resultText = String.Format(My.Resources.ErrorOccured, ex.Message)
        End Try
        Return returnValue
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        Dim selectReportLocation As New SelectReportLocation
        _optionValidator.Filename = GetDefaultExportFilename()
        _optionValidator.ExportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), String.Format("{0}.xlsx", _optionValidator.Filename))
        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function

    Public Overrides Sub ClearErrors() Implements IReportHandler.ClearErrors
        If _optionValidator IsNot Nothing Then
            _optionValidator.ClearError()
        End If
    End Sub

    Public Overrides Function IsDatasourceSupported() As Boolean Implements IReportHandler.IsDatasourceSupported
        Return SelectedEntities IsNot Nothing AndAlso
               (SelectedEntities.OfType(Of AssessmentTestResourceDto).Any OrElse
               SelectedEntities.OfType(Of ItemResourceDto).Any)
    End Function




    Public Event ReportProgress(ByVal sender As Object, ByVal e As Cito.Tester.Common.ProgressEventArgs) Implements IReportHandler.Progress

    Private Sub OnExportDataFetchingProgress(ByVal e As Cito.Tester.Common.ProgressEventArgs)
        RaiseEvent ReportProgress(Me, e)
    End Sub

    Public Event StartReportProgress(ByVal sender As Object, ByVal e As StartEventArgs) Implements IReportHandler.StartProgress

    Private Sub OnStartExportDataFetching(startEventArgs As StartEventArgs)
        RaiseEvent StartReportProgress(Me, startEventArgs)
    End Sub




    Private Function GetDefaultExportFilename() As String
        Dim defaultFileName = ""
        Dim firstEntity = SelectedEntities.FirstOrDefault
        If firstEntity IsNot Nothing Then
            Select Case firstEntity.GetType
                Case GetType(AssessmentTestResourceDto)
                    defaultFileName = String.Format("ItemConceptScoring_Test_{0}", DirectCast(firstEntity, AssessmentTestResourceDto).Name)
                Case GetType(ItemResourceDto)
                    defaultFileName = String.Format("ItemConceptScoring_Bank_{0}", DirectCast(firstEntity, ItemResourceDto).BankName)
            End Select
        End If

        Return defaultFileName
    End Function

    Private Function GetDatatable(ByVal collection As IList(Of ResourceEntity)) As DataTable
        Dim returnDatatable As New DataTable("ConceptScoringExcelExport")

        DefineDataTableColumns(returnDatatable)

        If collection IsNot Nothing AndAlso collection.Any() Then
            _itemScoringDataFetcher = New ItemScoringReportDataFetcher(_bankId)

            Dim entity As EntityBase2 = DirectCast(collection.Item(0), EntityBase2)
            Select Case entity.GetType.ToString
                Case GetType(AssessmentTestResourceEntity).ToString
                    Dim test As AssessmentTestResourceEntity = DirectCast(entity, AssessmentTestResourceEntity)
                    FillDatatableForTest(test, returnDatatable)
                Case GetType(ItemResourceEntity).ToString
                    FillDataTableForItem(collection, returnDatatable)
                Case Else
            End Select

        End If
        Return returnDatatable
    End Function

    Private Sub DefineDataTableColumns(dataTable As DataTable)
        For Each colCode As String In System.Enum.GetNames(GetType(ReportDataTableColumnNames))
            Dim addedColumn = dataTable.Columns.Add(colCode)
            addedColumn.Caption = My.Resources.ResourceManager.GetString(String.Format("ConceptScoringReportColumnCaption_{0}", colCode))
        Next
    End Sub


    Private Sub FillDatatableForTest(ByVal assessmenttestResource As AssessmentTestResourceEntity, ByRef datatable As DataTable)
        Dim fullTest As AssessmentTestResourceEntity = ResourceFactory.Instance.GetAssessmentTest(assessmenttestResource)
        Dim assessmentTest As AssessmentTest2 = GetTestFromResource(fullTest)
        Dim allItemsInTestCollection = assessmentTest.GetAllItemReferencesInTest()

        Dim itemCodeList As List(Of String) = allItemsInTestCollection.Select(Function(x) x.SourceName).ToList()
        Dim itemCollection As EntityCollectionBase2(Of ItemResourceEntity) = ResourceFactory.Instance.GetItemsByCodes(itemCodeList, _bankId, New ItemResourceRequestDTO() With {.WithCustomProperties = True, .WithReportData = True, .WithFullCustomProperties = True})

        Try
            OnStartExportDataFetching(New StartEventArgs(itemCollection.Count))
            For Each itemReference As ItemReference2 In allItemsInTestCollection
                Dim itemCode As String = itemReference.SourceName
                OnExportDataFetchingProgress(New Cito.Tester.Common.ProgressEventArgs(String.Format(My.Resources.ProgressMessageConceptScoringReport, itemCode)))
                Dim itemEntity As ItemResourceEntity = itemCollection.FirstOrDefault(Function(itm As ItemResourceEntity) itm.Name.Equals(itemCode, StringComparison.InvariantCultureIgnoreCase))
                If itemEntity IsNot Nothing Then
                    FillDataRowsForItem(itemEntity, datatable)
                End If
            Next
        Finally

        End Try
    End Sub

    Private Function GetTestFromResource(ByVal testEntity As AssessmentTestResourceEntity) As AssessmentTest2
        Dim testDefinition As AssessmentTest2 = Nothing
        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testEntity)
        Dim result As ReturnedAssessmentTestModelInfo
        result = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(data.BinData, True)
        testDefinition = result.AssessmentTestv2
        testEntity.ResourceData = data

        Return testDefinition
    End Function

    Private Sub FillDataTableForItem(ByVal collection As IList(Of ResourceEntity), ByRef datatable As DataTable)
        Try
            OnStartExportDataFetching(New StartEventArgs(collection.Count))

            Dim itemCodeList As List(Of String) = collection.Select(Function(r) r.Name).ToList()
            Dim itemCollection = ResourceFactory.Instance.GetItemsByCodes(itemCodeList, _bankId, New ItemResourceRequestDTO() With {.WithCustomProperties = True, .WithReportData = True, .WithFullCustomProperties = True})

            For Each entity As ResourceEntity In collection
                Dim item As ItemResourceEntity = itemCollection.FirstOrDefault(Function(itm) itm.ResourceId.Equals(entity.ResourceId))
                If item IsNot Nothing Then
                    OnExportDataFetchingProgress(New Cito.Tester.Common.ProgressEventArgs(String.Format(My.Resources.ProgressMessageConceptScoringReport, item.Name)))
                    FillDataRowsForItem(item, datatable)
                End If
            Next
        Finally

        End Try
    End Sub

    Private Sub FillDataRowsForItem(ByVal itemResource As ItemResourceEntity, ByVal datatable As DataTable)

        Dim publicationHandlerTypeName As String = GetPublicationHandlerTypeNameFromConfig()
        Dim itemScoringData As Collection(Of IItemConceptScoringReportData)
        If Not String.IsNullOrEmpty(publicationHandlerTypeName) Then
            itemScoringData = _itemScoringDataFetcher.FetchConceptScoringReportData(itemResource, _optionValidator.IncludeConceptsWithoutScore, publicationHandlerTypeName)
        Else
            itemScoringData = _itemScoringDataFetcher.FetchConceptScoringReportData(itemResource, _optionValidator.IncludeConceptsWithoutScore)
        End If

        If itemScoringData IsNot Nothing AndAlso itemScoringData.Any() Then

            If Not _bankDictionary.ContainsKey(itemResource.BankId.ToString) Then
                Dim bank As BankEntity = BankFactory.Instance.GetBank(itemResource.BankId)
                _bankDictionary.Add(itemResource.BankId.ToString, bank.Name)
            End If

            For Each reportingData As ItemConceptScoringReportData In itemScoringData
                Dim newRow As DataRow = datatable.NewRow

                newRow.Item(ReportDataTableColumnNames.BankName) = _bankDictionary(itemResource.BankId.ToString())
                newRow.Item(ReportDataTableColumnNames.ItemCode) = reportingData.ItemCode
                newRow.Item(ReportDataTableColumnNames.ItemId) = reportingData.ItemId
                newRow.Item(ReportDataTableColumnNames.ItemTitle) = reportingData.ItemTitle
                newRow.Item(ReportDataTableColumnNames.Itemlayouttemplate) = reportingData.Itemlayouttemplate
                newRow.Item(ReportDataTableColumnNames.ConceptCode) = reportingData.ConceptCode
                newRow.Item(ReportDataTableColumnNames.ConceptResponseLabel) = reportingData.ConceptResponseLabel
                newRow.Item(ReportDataTableColumnNames.KeyValuesAndConceptScores) = reportingData.KeyValuesAndConceptScores
                newRow.Item(ReportDataTableColumnNames.AdditionalKeyValuesAndConceptScores) = reportingData.AdditionalKeyValuesAndConceptScores
                newRow.Item(ReportDataTableColumnNames.IsGrouped) = reportingData.IsGrouped
                newRow.Item(ReportDataTableColumnNames.GroupElementCount) = IIf(reportingData.GroupElementCount > 0, reportingData.GroupElementCount, String.Empty)
                newRow.Item(ReportDataTableColumnNames.InteractionCount) = reportingData.InteractionCount
                newRow.Item(ReportDataTableColumnNames.AttributeLevelConceptResponseCount) = reportingData.AttributeLevelConceptResponseCount
                newRow.Item(ReportDataTableColumnNames.SubAttributLevelConceptResponseCount) = reportingData.SubAttributLevelConceptResponseCount

                datatable.Rows.Add(newRow)
            Next
        End If

    End Sub

    Private Function GetPublicationHandlerTypeNameFromConfig() As String
        If HandlerConfig IsNot Nothing Then
            For Each el As PluginHandlerConfigElement In HandlerConfig
                If (el.Key.Equals("PublicationHandlerTypeName", StringComparison.InvariantCultureIgnoreCase)) Then
                    Return el.Value
                End If
            Next
        End If

        Return Nothing
    End Function



    Public Sub New()
        _optionValidator = New SelectColumnsOptionsValidator
    End Sub



End Class
