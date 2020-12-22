
Imports Cito.Tester.Common
Imports System.Windows.Forms
Imports System.IO
Imports System.Linq
Imports System.Text
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Conversion
Imports System.ComponentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.UI
Imports HtmlDocument = HtmlAgilityPack.HtmlDocument

Public Class ExcelReport
    Inherits ExcelReportBase
    Implements IReportHandler

    Protected _datatable As DataTable
    Protected _resourceManager As DataBaseResourceManager
    Protected _selectedEntities As IList(Of ResourceDto)
    Protected _optionValidator As SelectColumnsOptionsValidator
    Protected _resultText As String
    Protected _bankId As Integer

    Private _exportedReportsLocations As List(Of String)

    Public Sub New()
        _optionValidator = New SelectColumnsOptionsValidator
        _exportedReportsLocations = New List(Of String)
    End Sub

    Public Event StartCollectColumns As EventHandler(Of StartEventArgs) Implements IReportHandler.StartProgress

    Protected Sub OnStartCollectColumns(ByVal e As StartEventArgs)
        RaiseEvent StartCollectColumns(Me, e)
    End Sub

    Public Event ProgressCollectColumns As EventHandler(Of Cito.Tester.Common.ProgressEventArgs) Implements IReportHandler.Progress

    Protected Sub OnProgressCollectColumns(ByVal e As Cito.Tester.Common.ProgressEventArgs)
        RaiseEvent ProgressCollectColumns(Me, e)
    End Sub

    Public Event ReportCompleted(ByVal sender As Object, ByVal e As ReportCompletedEventArgs) Implements IReportHandler.ReportCompleted

    Public Overridable ReadOnly Property Name() As String Implements IReportHandler.Name
        Get
            If _selectedEntities.Any Then
                Select Case _selectedEntities.FirstOrDefault.GetType
                    Case GetType(AssessmentTestResourceDto)
                        Return My.Resources.ReportNameTest
                    Case GetType(ItemResourceDto)
                        Return My.Resources.ReportNameItem
                    Case GetType(GenericResourceDto)
                        Return My.Resources.ReportNameMedia
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property Description() As String Implements IReportHandler.Description
        Get
            Dim first = _selectedEntities.FirstOrDefault
            If first IsNot Nothing Then
                Select Case first.GetType
                    Case GetType(AssessmentTestResourceDto)
                        Return My.Resources.ReportDescriptionTest
                    Case GetType(ItemResourceDto)
                        Return My.Resources.ReportDescriptionItem
                    Case GetType(GenericResourceDto)
                        Return My.Resources.ReportDescriptionMedia
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property ExportedReportLocation() As String Implements IReportHandler.ExportedReportLocation
        Get
            Return String.Join(";", _exportedReportsLocations.ToArray())
        End Get
    End Property

    Public ReadOnly Property Overview() As String Implements IReportHandler.Overview
        Get
            If _optionValidator IsNot Nothing Then
                Dim listOfColumns As New StringBuilder
                For Each column As String In _optionValidator.SelectedColumns
                    If Not String.IsNullOrEmpty(listOfColumns.ToString) Then
                        listOfColumns.Append(vbCrLf)
                    End If
                    listOfColumns.Append("- ")
                    listOfColumns.Append(column)
                Next
                Return String.Format(My.Resources.Overview, vbCrLf, Me.Name, _optionValidator.ExportPath, listOfColumns.ToString.Trim(CChar(" ")).Trim(CChar(",")))
            End If
            Return String.Empty
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

    Public Overrides ReadOnly Property ResultText() As String Implements IReportHandler.ResultText
        Get
            Return _resultText
        End Get
    End Property

    Public Property Collection() As IList(Of ResourceDto) Implements IReportHandler.Collection
        Set(ByVal value As IList(Of ResourceDto))
            _selectedEntities = value
        End Set
        Get
            Return _selectedEntities
        End Get
    End Property

    Public Property BankId As Integer Implements IReportValidationBase.BankId
        Set(ByVal value As Integer)
            _bankId = value
        End Set
        Get
            Return _bankId
        End Get
    End Property

    Public Overrides ReadOnly Property ExtraOptionTask() As String Implements IReportHandler.ExtraOptionTask
        Get
            Return My.Resources.ExtraOptionTask
        End Get
    End Property

    Public Overrides ReadOnly Property IsInitDataGeneratedAsynchronous As Boolean Implements IReportHandler.IsInitDataGeneratedAsynchronous
        Get
            Return True
        End Get
    End Property

    Public Overridable ReadOnly Property IncludeItemParameters As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub InitialiseData() Implements IReportHandler.InitialiseData
        Dim bgWorker As New BackgroundWorker()
        _optionValidator.Filename = GetFileName()
        bgWorker.WorkerReportsProgress = True
        Dim totalItemsToCheck = 100
        OnStartCollectColumns(New StartEventArgs(100))
        AddHandler bgWorker.ProgressChanged, Sub(sender As Object, e As ProgressChangedEventArgs)
                                                 Dim message = String.Empty
                                                 If e.UserState IsNot Nothing Then
                                                     message = e.UserState.ToString
                                                 End If
                                                 OnProgressCollectColumns(New ProgressEventArgs(message, e.ProgressPercentage))
                                             End Sub

        AddHandler bgWorker.DoWork, Sub(sender As Object, e As DoWorkEventArgs)
                                        Using tableCreator As New DataTableConvertHelper(Me.BankId)
                                            AddHandler tableCreator.StartCreateTable, (Sub(sender1 As Object, e1 As StartEventArgs)
                                                                                           totalItemsToCheck = e1.NumberOfResources
                                                                                           bgWorker.ReportProgress(0)
                                                                                       End Sub)
                                            AddHandler tableCreator.CreateProgress, (Sub(sender1 As Object, e1 As Cito.Tester.Common.ProgressEventArgs)
                                                                                         If e1.ProgessValue.HasValue AndAlso totalItemsToCheck <> 0 Then
                                                                                             Dim value = CType(Math.Floor(((e1.ProgessValue.Value / totalItemsToCheck) * 100)), Integer)
                                                                                             bgWorker.ReportProgress(value, e1.StatusMessage)
                                                                                         Else
                                                                                             bgWorker.ReportProgress(0, e1.StatusMessage)
                                                                                         End If
                                                                                     End Sub)
                                            _datatable = GetTable(tableCreator)
                                            _datatable.TableName = "ExcelReport"
                                        End Using

                                    End Sub
        AddHandler bgWorker.RunWorkerCompleted, Sub(sender As Object, e As RunWorkerCompletedEventArgs)
                                                    RaiseEvent ReportCompleted(Me, New ReportCompletedEventArgs(True))
                                                End Sub
        bgWorker.RunWorkerAsync()
    End Sub

    Public Overrides Sub ClearErrors() Implements IReportHandler.ClearErrors
        If _optionValidator IsNot Nothing Then
            _optionValidator.ClearError()
        End If
    End Sub

    Public Overrides Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI
        If _datatable Is Nothing Then
            Return Nothing
        End If

        If _optionValidator.AvailableColumns.Count = 0 Then
            For Each dc As DataColumn In _datatable.Columns
                _optionValidator.AvailableColumns.Add(dc.ColumnName)
            Next
        End If

        Dim selectColumns As New SelectColumns
        selectColumns.OptionsValidatorBindingSource.DataSource = _optionValidator
        AddHandler selectColumns.Enter, (Sub(sender As Object, e As EventArgs) Me.LoadUserSettings(selectColumns))
        AddHandler selectColumns.Validated, (Sub(sender As Object, e As EventArgs) Me.StoreUserSettings(selectColumns))

        Return selectColumns
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        Dim selectReportLocation As New SelectReportLocation()
        If String.IsNullOrEmpty(ReportSettings.ExcelReport) Then
            ReportSettings.ExcelReport = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        End If

        _optionValidator.ExportPath = Path.Combine(ReportSettings.ExcelReport, String.Format("{0}.xlsx", _optionValidator.Filename))

        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function

    Public Overrides Function IsDatasourceSupported() As Boolean Implements IReportHandler.IsDatasourceSupported
        Return _selectedEntities IsNot Nothing AndAlso
               (_selectedEntities.OfType(Of AssessmentTestResourceDto).Any OrElse
                _selectedEntities.OfType(Of ItemResourceDto).Any) OrElse
                _selectedEntities.OfType(Of GenericResourceDto).Any
    End Function

    Public Overrides Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Dim returnValue As Boolean = False

        If _optionValidator.SelectedColumns Is Nothing Then
            Return returnValue
        End If

        _resourceManager = New DataBaseResourceManager(_bankId)
        Try
            Dim exportPath As String = _optionValidator.ExportPath
            GenerateDataForEntity(_datatable.AsEnumerable().ToArray(), exportPath)
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

    Protected Sub GenerateDataForEntity(dataRows As DataRow(), exportPath As String)
        Const PARAGRAPHENDTAG As String = "</p>"
        Using dataTableToExport = New DataTable
            For Each selectedColumn As String In _optionValidator.SelectedColumns
                dataTableToExport.Columns.Add(selectedColumn)
            Next

            For Each dataRow As DataRow In dataRows
                Dim newDataRow As DataRow = dataTableToExport.NewRow
                For Each dc As DataColumn In dataTableToExport.Columns

                    Dim cellValue = dataRow.Item(dc.ColumnName).ToString
                    cellValue = cellValue.Replace(PARAGRAPHENDTAG, PARAGRAPHENDTAG + " ")

                    newDataRow.Item(dc.ColumnName) = StripHtmlTags(cellValue)
                Next

                dataTableToExport.Rows.Add(newDataRow)
            Next

            Dim exporter As New ExcelExport()
            exporter.ExportDataTable(dataTableToExport, exportPath)
            _exportedReportsLocations.Add(exportPath)
        End Using
    End Sub

    Protected Overridable Function GetFileName() As String
        Return GetFileName(Me.Collection.FirstOrDefault())
    End Function

    Protected Overridable Function GetFileName(resource As ResourceDto) As String
        Dim fileName As String = String.Empty
        If resource IsNot Nothing Then
            Select Case resource.GetType
                Case GetType(AssessmentTestResourceDto)
                    fileName = String.Format("Tests_Bank_{0}", resource.BankName)
                Case GetType(ItemResourceDto)
                    fileName = String.Format("Items_Bank_{0}", resource.BankName)
                Case GetType(GenericResourceDto)
                    fileName = String.Format("Media_Bank_{0}", resource.BankName)
            End Select
        End If
        Return fileName
    End Function

    Protected Overridable Function GetTable(tableCreator As DataTableConvertHelper) As DataTable
        Return tableCreator.CreateTable(_selectedEntities, IncludeItemParameters)
    End Function

    Private Sub LoadUserSettings(selectColumnsControl As SelectColumns)
        If selectColumnsControl IsNot Nothing Then
            selectColumnsControl.SelectedColumns = UserSettings.GetUserWizardSettingsForWizard("ExcelReport").GetTabSettingsForTab("SelectColumns")
        End If
    End Sub

    Private Sub StoreUserSettings(selectColumnsControl As SelectColumns)
        UserSettings.GetUserWizardSettingsForWizard("ExcelReport").SetTabSettingsForTab("SelectColumns", DirectCast(selectColumnsControl.SelectedColumns, List(Of String)))
        UserSettings.StoreUserWizardSettings()
    End Sub

    Private Function StripHtmlTags(text As String) As String
        Dim doc As New HtmlDocument
        doc.LoadHtml(text)
        Return doc.DocumentNode.InnerText
    End Function

End Class
