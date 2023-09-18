Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.Conversion
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Plugins.Reports.Excel.My.Resources

Public Class AssessmentTestMaxScoreExcelReport
    Inherits ExcelReportBase
    Implements IReportHandler

    Private _selectedEntities As IList(Of ResourceDto)
    Private ReadOnly _optionValidator As OptionValidatorGridExport
    Private _datatable As DataTable
    Private _resultText As String
    Private ReadOnly _cachedItemSolutions As New Dictionary(Of String, Solution)

    Public Sub New()
        _optionValidator = New OptionValidatorGridExport()
    End Sub

    Public Overrides ReadOnly Property ExportedReportLocation() As String Implements IReportHandler.ExportedReportLocation
        Get
            If _optionValidator IsNot Nothing Then
                Return _optionValidator.ExportPath
            End If
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property ExtraOptionTask() As String Implements IReportHandler.ExtraOptionTask
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property ExtraOptionTaskDescription() As String Implements IReportHandler.ExtraOptionTaskDescription
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property Overview() As String Implements IReportHandler.Overview
        Get
            If _optionValidator IsNot Nothing Then
                Return String.Format(OverviewGridToExcel, vbCrLf, Me.Name, _optionValidator.ExportPath)
            End If
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property ResultText() As String Implements IReportHandler.ResultText
        Get
            Return _resultText
        End Get
    End Property

    Public Overrides ReadOnly Property ShowExtraOptionsTab() As Boolean Implements IReportHandler.ShowExtraOptionsTab
        Get
            Return False
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

    Public Overrides Property Collection() As IList(Of ResourceDto) Implements IReportValidationBase.Collection
        Set(value As IList(Of ResourceDto))
            _selectedEntities = value
        End Set
        Get
            Return _selectedEntities
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String Implements IReportValidationBase.Description
        Get
            Return ReportDescriptionAssessmentTestMaxScore
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String Implements IReportValidationBase.Name
        Get
            Return ReportNameAssessmentTestMaxScore
        End Get
    End Property

    Public Overrides Function IsDatasourceSupported() As Boolean Implements IReportValidationBase.IsDatasourceSupported
        Dim returnValue As Boolean = False
        If (_selectedEntities IsNot Nothing) Then
            If _selectedEntities.OfType(Of AssessmentTestResourceDto)().Any() Then
                returnValue = True
            End If
        End If
        Return returnValue
    End Function

    Public Overrides Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Dim returnValue As Boolean = False
        Try
            If _selectedEntities IsNot Nothing AndAlso _selectedEntities.Any() Then
                _datatable = GetDataTable()

                If _datatable IsNot Nothing Then
                    Dim exporter As New ExcelExport()
                    exporter.ExportDataTable(_datatable, _optionValidator.ExportPath)

                    _resultText = String.Format(ReportSuccessful, Me.Name)
                    returnValue = True
                End If
            End If
        Catch ioException As IOException
            _resultText = ErrorWhileExportingToExcelFileMightBeInUse
        Catch ex As Exception
            _resultText = String.Format(ReportUnSuccessful, ex.Message)
        Finally
            _datatable.Dispose()
        End Try
        Return returnValue
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        Dim selectReportLocation As New SelectReportLocation
        _optionValidator.Filename = String.Format("Max score per test - {0}", _selectedEntities.FirstOrDefault().BankName)
        _optionValidator.ExportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), String.Format("{0}.xlsx", _optionValidator.Filename))
        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function

    Public Overrides Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI
        Throw New NotImplementedException
    End Function

    Public Overrides Sub ClearErrors() Implements IReportHandler.ClearErrors
        If _optionValidator IsNot Nothing Then
            _optionValidator.ClearError()
        End If
    End Sub

    Private Function GetDataTable() As DataTable
        _datatable = New DataTable("ExcelExport")
        If Collection.Count > 0 Then
            AddColumnsToDataTable()
            FillDataTable()
        End If
        Return _datatable
    End Function

    Private Sub AddColumnsToDataTable()
        _datatable.Columns.Add(DatatableColumn_AssessmentTestCode)
        _datatable.Columns.Add(DatatableColumn_AssessmentTestTitle)
        _datatable.Columns.Add(DatatableColumn_MaxScore)
    End Sub

    Private Sub FillDataTable()
        For Each assessmentTestDto As AssessmentTestResourceDto In Collection
            Dim assessmentTest = assessmentTestDto.GetAssessmentTest()
            Dim itemReferences = assessmentTest.GetAllItemReferencesInTest()

            Dim itemsToGet = itemReferences.Where(Function(ir) Not _cachedItemSolutions.ContainsKey(ir.SourceName)).Select(Function(ir) ir.SourceName).ToList()
            Dim items = ResourceFactory.Instance.GetResourcesByNamesWithOption(assessmentTestDto.BankId, itemsToGet, New ResourceRequestDTO())
            items.ToList().ForEach(Sub(i)
                                       Dim itm = TryCast(i, ItemResourceEntity)
                                       _cachedItemSolutions.Add(itm.Name, itm.GetAssessmentItem().Solution)
                                   End Sub)

            Dim assessmentTestMaxScore = 0

            itemReferences.ToList().ForEach(Sub(ir)
                                                Dim itemSolution = _cachedItemSolutions(ir.SourceName)
                                                Dim itemMaxScore = GetItemMaxScore(itemSolution, ir.Weight)
                                                assessmentTestMaxScore += CType(itemMaxScore, Decimal)
                                            End Sub)

            _datatable.Rows.Add(GetDatarow(assessmentTestDto, assessmentTestMaxScore))
        Next

        _resultText = String.Empty
    End Sub

    Private Function GetItemMaxScore(solution As Solution, itemWeightInTest As Double) As Double
        Dim returnValue As Double = 0

        Dim translatedScore = solution.GetMaxSolutionTranslatedScore
        If translatedScore.HasValue Then
            returnValue = translatedScore.Value * itemWeightInTest
        End If

        Return returnValue
    End Function

    Private Function GetDatarow(assessmentTestDto As AssessmentTestResourceDto, maxScore As Decimal) As DataRow
        Dim row = _datatable.NewRow()

        If assessmentTestDto IsNot Nothing Then
            row.Item(DatatableColumn_AssessmentTestCode) = assessmentTestDto.Name
            row.Item(DatatableColumn_AssessmentTestTitle) = assessmentTestDto.Title
        End If
        row.Item(DatatableColumn_MaxScore) = maxScore

        Return row
    End Function

End Class
