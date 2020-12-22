Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ExcelReportForTestItems
    Inherits ExcelReport

    Public Overrides ReadOnly Property Name() As String
        Get
            Return My.Resources.ReportNameTestItems
        End Get
    End Property

    Public Overrides ReadOnly Property Description() As String
        Get
            Return My.Resources.ReportDescriptionTestItems
        End Get
    End Property

    Public Overrides Function IsDatasourceSupported() As Boolean
        Return _selectedEntities IsNot Nothing AndAlso _selectedEntities.OfType(Of AssessmentTestResourceDto).Any
    End Function

    Public Overrides Function GenerateData() As Boolean
        Dim returnValue As Boolean = False

        If _optionValidator.SelectedColumns Is Nothing Then
            Return returnValue
        End If

        _resourceManager = New DataBaseResourceManager(_bankId)
        Try
            Dim exportPath As String = _optionValidator.ExportPath
            If MultipleTestReportRequested() Then
                For Each res In _selectedEntities
                    exportPath = Path.Combine(ReportSettings.ExcelReport, String.Format("{0}.xlsx", GetFileName(res)))
                    GenerateDataForEntity(_datatable.AsEnumerable.Where(
                            Function(r As DataRow) r(My.Resources.DataTableKey1).ToString = res.Name).ToArray, exportPath)
                Next
            Else
                GenerateDataForEntity(_datatable.AsEnumerable().ToArray(), exportPath)
            End If
            returnValue = True
        Catch ex As Exception
            _resultText = String.Format(My.Resources.ReportUnSuccessful, Name, vbCrLf, ex.Message)
            Debug.Print(_resultText)
            returnValue = False
        Finally
            _resourceManager.Dispose()
            _resourceManager = Nothing
        End Try

        If returnValue Then
            _resultText = String.Format(My.Resources.ReportSuccessful, Me.Name)
        End If

        Return returnValue
    End Function

    Protected Overrides Function GetFileName(resource As ResourceDto) As String
        Dim fileName As String = String.Empty
        If resource IsNot Nothing Then
            Select Case resource.GetType
                Case GetType(AssessmentTestResourceDto)
                    fileName = String.Format($"Items_in_{resource.Name}_Bank_{resource.BankName}")
            End Select
        End If
        Return fileName
    End Function

    Protected Overrides Function GetTable(tableCreator As DataTableConvertHelper) As DataTable
        Return tableCreator.CreateTableForTestItems(_selectedEntities, IncludeItemParameters)
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl
        Dim multipleTestRepReq As Boolean = MultipleTestReportRequested()
        Dim selectReportLocation As New SelectReportLocation(multipleTestRepReq)
        If String.IsNullOrEmpty(ReportSettings.ExcelReport) Then
            ReportSettings.ExcelReport = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        End If

        _optionValidator.FileLocationOnly = multipleTestRepReq
        If Not multipleTestRepReq Then
            _optionValidator.ExportPath = Path.Combine(ReportSettings.ExcelReport, String.Format("{0}.xlsx", _optionValidator.Filename))
        Else
            _optionValidator.ExportPath = ReportSettings.ExcelReport
        End If

        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function

    Private Function MultipleTestReportRequested() As Boolean
        Return _selectedEntities IsNot Nothing AndAlso _selectedEntities.OfType(Of AssessmentTestResourceDto).Count > 1
    End Function

End Class
