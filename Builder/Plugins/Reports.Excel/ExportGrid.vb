Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports System.IO
Imports Janus.Windows.GridEX.Export
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ExportGrid
    Implements IReportHandler

    Private _grid As GridEX
    Private _optionValidator As OptionValidatorGridExport
    Private _resultText As String
    Private _bank As Integer

    Public Property Collection() As IList(Of ResourceDto) Implements IReportHandler.Collection
        Set(ByVal value As IList(Of ResourceDto))
        End Set
        Get
            If _grid IsNot Nothing Then
                Dim result As New List(Of ResourceDto)
                Select Case _grid.Parent.Name
                    Case "ItemGrid"
                        GetListOfItemsInGrid(result, _grid.GetRows())
                    Case "MediaGrid"
                        GetListOfMediaInGrid(result, _grid.GetRows())
                    Case "TestGrid", "TestPackageGrid", "DataSourceGrid"
                        GetListOfResourcesInGrid(result, _grid.GetRows())
                End Select

                Return result
            End If
            Return Nothing
        End Get
    End Property

    Private Sub GetListOfItemsInGrid(result As List(Of ResourceDto), rows As GridEXRow())
        For Each row As GridEXRow In rows
            If row.RowType = RowType.GroupHeader Then
                GetListOfItemsInGrid(result, row.GetChildRows())
            ElseIf row.RowType = RowType.Record Then
                result.Add(New ItemResourceDto() With {.name = row.Cells(3).Value.ToString()})
            End If
        Next
    End Sub

    Private Sub GetListOfMediaInGrid(result As List(Of ResourceDto), rows As GridEXRow())
        For Each row As GridEXRow In rows
            If row.RowType = RowType.GroupHeader Then
                GetListOfMediaInGrid(result, row.GetChildRows())
            ElseIf row.RowType = RowType.Record Then
                result.Add(New GenericResourceDto() With {.name = row.Cells(2).Value.ToString(), .mediaType = row.Cells(4).Value.ToString()})
            End If
        Next
    End Sub

    Private Sub GetListOfResourcesInGrid(result As List(Of ResourceDto), rows As GridEXRow())
        For Each row As GridEXRow In rows
            If row.RowType = RowType.GroupHeader Then
                GetListOfResourcesInGrid(result, row.GetChildRows())
            ElseIf row.RowType = RowType.Record Then
                result.Add(New ResourceDto() With {.name = row.Cells(2).Value.ToString()})
            End If
        Next
    End Sub

    Public Overridable ReadOnly Property Description() As String Implements IReportHandler.Description
        Get
            Return My.Resources.ExportGridDescription
        End Get
    End Property

    Public ReadOnly Property ResultText() As String Implements IReportHandler.ResultText
        Get
            Return _resultText
        End Get
    End Property

    Public ReadOnly Property ShouldUseCollectionAsInput() As Boolean Implements IReportHandler.ShouldUseCollectionAsInput
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShouldUseGridAsInput() As Boolean Implements IReportHandler.ShouldUseGridAsInput
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShowCreateReportProgressTab() As Boolean Implements IReportHandler.ShowCreateReportProgressTab
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowExtraOptionsTab() As Boolean Implements IReportHandler.ShowExtraOptionsTab
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowInitialiseProgressTab() As Boolean Implements IReportHandler.ShowInitialiseProgressTab
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowSelectLocationTab() As Boolean Implements IReportHandler.ShowSelectLocationTab
        Get
            Return True
        End Get
    End Property

    Public Overridable ReadOnly Property Name() As String Implements IReportHandler.Name
        Get
            Return My.Resources.ExportGridName
        End Get
    End Property

    Public ReadOnly Property Overview() As String Implements IReportHandler.Overview
        Get
            If _optionValidator IsNot Nothing Then
                Return String.Format(My.Resources.OverviewGridToExcel, vbCrLf, Me.Name, _optionValidator.ExportPath)
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


    Public Property BankId As Integer Implements IReportValidationBase.BankId
        Set(ByVal value As Integer)
            _bank = value
        End Set
        Get
            Return _bank
        End Get
    End Property

    Public ReadOnly Property ValidationErrors() As String Implements IReportHandler.ValidationErrors
        Get
            Dim returnValue As String = String.Empty
            If _optionValidator IsNot Nothing Then
                returnValue = _optionValidator.Error
            End If
            Return returnValue
        End Get
    End Property

    Public ReadOnly Property ExtraOptionTask() As String Implements IReportHandler.ExtraOptionTask
        Get
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property ExtraOptionTaskDescription() As String Implements IReportHandler.ExtraOptionTaskDescription
        Get
            Return String.Empty
        End Get
    End Property


    Public Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI
        Return Nothing
    End Function

    Public Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Dim returnValue As Boolean = False
        Try
            Using GridEXExporter As New GridEXExporter
                GridEXExporter.GridEX = _grid
                GridEXExporter.IncludeExcelProcessingInstruction = True
                GridEXExporter.IncludeFormatStyle = False
                GridEXExporter.IncludeHeaders = True
                GridEXExporter.IncludeChildTables = False
                GridEXExporter.IncludeCollapsedRows = True
                Dim stream As FileStream = New FileStream(_optionValidator.ExportPath, FileMode.Create)
                GridEXExporter.Export(stream)
                stream.Flush()
                stream.Close()
            End Using
            _resultText = String.Format(My.Resources.ReportSuccessful, Me.Name)
            returnValue = True
        Catch ioException As IOException
            _resultText = My.Resources.ErrorWhileExportingToExcelFileMightBeInUse
        Catch ex As Exception
            _resultText = String.Format(My.Resources.ErrorOccured, ex.Message)
        End Try
        Return returnValue
    End Function

    Public Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        Dim fileName As String = "GridExport"
        If _grid IsNot Nothing Then
            Select Case _grid.Parent.Name
                Case "TestGrid"
                    fileName = My.Resources.TestGrid
                Case "ItemGrid"
                    fileName = My.Resources.ItemGrid
                Case "AspectGrid"
                    fileName = My.Resources.AspectGrid
                Case "ControlTemplateGrid"
                    fileName = My.Resources.ControlTemplateGrid
                Case "ItemLayoutTemplateGrid"
                    fileName = My.Resources.ItemLayoutTemplateGrid
                Case "DataSourceGrid"
                    fileName = My.Resources.DataSourceGrid
                Case "TestTemplateGrid"
                    fileName = My.Resources.TestTemplateGrid
            End Select
        End If
        _optionValidator.Filename = fileName
        Dim selectReportLocation As New SelectReportLocation
        selectReportLocation.SaveFileDialog.Filter = "excel 2003 (*.xls)|*.xls|Alle bestanden (*.*)|*.*"
        _optionValidator.ExportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), String.Format("{0}.xls", _optionValidator.Filename))
        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function

    Public Sub ClearErrors() Implements IReportHandler.ClearErrors
        If _optionValidator IsNot Nothing Then
            _optionValidator.ClearError()
        End If
    End Sub

    Public WriteOnly Property GridToExport() As Object Implements IReportHandler.GridToExport
        Set(ByVal value As Object)
            If TypeOf value Is GridEX Then
                _grid = DirectCast(value, GridEX)
            End If
        End Set
    End Property

    Public ReadOnly Property IsInitDataGeneratedAsynchronous As Boolean Implements IReportHandler.IsInitDataGeneratedAsynchronous
        Get
            Return True
        End Get
    End Property

    Public Sub InitialiseData() Implements IReportHandler.InitialiseData
        Throw New NotImplementedException()
    End Sub

    Public Function IsDatasourceSupported() As Boolean Implements IReportHandler.IsDatasourceSupported
        Return _grid IsNot Nothing
    End Function





    Public Event ReportProgress(ByVal sender As Object, ByVal e As ProgressEventArgs) Implements IReportHandler.Progress

    Public Event StartReportProgress(ByVal sender As Object, ByVal e As StartEventArgs) Implements IReportHandler.StartProgress

    Public Event ReportCompleted(ByVal sender As Object, ByVal e As ReportCompletedEventArgs) Implements IReportHandler.ReportCompleted


    Public Sub New()
        _optionValidator = New OptionValidatorGridExport
    End Sub



End Class
