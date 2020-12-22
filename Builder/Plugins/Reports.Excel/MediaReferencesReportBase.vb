Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Conversion
Imports Questify.Builder.Plugins.Reports.Excel.My.Resources
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public MustInherit Class MediaReferencesReportBase
    Implements IReportHandler


    Protected _datatable As DataTable
    Protected _selectedEntities As IList(Of ResourceDto)
    Protected ReadOnly _optionValidator As OptionValidatorMediaReferences
    Protected _resultText As String




    Public Sub New()
        _optionValidator = New OptionValidatorMediaReferences()
    End Sub



    Public Event StartProgress As EventHandler(Of StartEventArgs) Implements IReportValidationBase.StartProgress
    Public Event Progress As EventHandler(Of Cito.Tester.Common.ProgressEventArgs) Implements IReportValidationBase.Progress

    Public Event ReportCompleted(sender As Object, e As ReportCompletedEventArgs) _
        Implements IReportHandler.ReportCompleted

    Public Overridable ReadOnly Property Description As String Implements IReportValidationBase.Description
        Get
            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property Name As String Implements IReportValidationBase.Name
        Get
            Return ReferencedMediaExcelExport_Name
        End Get
    End Property

    Public OverRidable Property Collection() As IList(Of ResourceDto) Implements IReportValidationBase.Collection
        Set(ByVal value As IList(Of ResourceDto))
            _selectedEntities = value
        End Set
        Get
            Return _selectedEntities
        End Get
    End Property

    Public Property BankId As Integer Implements IReportValidationBase.BankId

    Public Overridable Function IsDatasourceSupported() As Boolean Implements IReportHandler.IsDatasourceSupported
        Return False
    End Function

    Public ReadOnly Property ResultText As String Implements IReportHandler.ResultText
        Get
            Return _resultText
        End Get
    End Property

    Public OverRidable ReadOnly Property ExportedReportLocation() As String Implements IReportHandler.ExportedReportLocation
        Get
            If _optionValidator IsNot Nothing Then
                Return _optionValidator.ExportPath
            End If
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property ExtraOptionTask As String Implements IReportHandler.ExtraOptionTask
        Get
            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property ExtraOptionTaskDescription As String Implements IReportHandler.ExtraOptionTaskDescription
        Get
            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property ShowExtraOptionsTab As Boolean Implements IReportHandler.ShowExtraOptionsTab
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowInitialiseProgressTab As Boolean Implements IReportHandler.ShowInitialiseProgressTab
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowCreateReportProgressTab As Boolean _
        Implements IReportHandler.ShowCreateReportProgressTab
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowSelectLocationTab As Boolean Implements IReportHandler.ShowSelectLocationTab
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsDataGeneratedAsynchronous As Boolean _
        Implements IReportHandler.IsDataGeneratedAsynchronous
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsInitDataGeneratedAsynchronous As Boolean _
        Implements IReportHandler.IsInitDataGeneratedAsynchronous
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property ShouldUseCollectionAsInput As Boolean Implements IReportHandler.ShouldUseCollectionAsInput
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShouldUseGridAsInput As Boolean Implements IReportHandler.ShouldUseGridAsInput
        Get
            Return False
        End Get
    End Property

    Public WriteOnly Property GridToExport As Object Implements IReportHandler.GridToExport
        Set(value As Object)
        End Set
    End Property

    Public Overridable Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI
        Throw New NotImplementedException
    End Function

    Public Overridable Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        Throw New NotImplementedException
    End Function

    Public Sub InitialiseData() Implements IReportHandler.InitialiseData
    End Sub

    Public Overridable Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Dim returnValue As Boolean = False
        Try
            If _selectedEntities IsNot Nothing AndAlso _selectedEntities.Any() Then
                _datatable = GetDataTable()

                If _datatable IsNot Nothing Then
                    Dim exporter As New ExcelExport()

                    exporter.ExportDataTable(_datatable, True, _optionValidator.ExportPath)

                    _resultText = String.Format(ReportSuccessful, Me.Name)
                    returnValue = True
                End If
            End If
        Catch ioException As IOException
            _resultText = ErrorWhileExportingToExcelFileMightBeInUse
        Catch ex As Exception
            _resultText = String.Format(ErrorOccured, ex.Message)
        End Try
        Return returnValue
    End Function

    Public Overridable Sub ClearErrors() Implements IReportHandler.ClearErrors
        If _optionValidator IsNot Nothing Then
            _optionValidator.ClearError()
        End If
    End Sub

    Public ReadOnly Property Overview As String Implements IReportHandler.Overview
        Get
            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property ValidationErrors() As String Implements IReportHandler.ValidationErrors
        Get
            Dim returnValue As String = String.Empty
            If _optionValidator IsNot Nothing Then
                returnValue = _optionValidator.Error
            End If
            Return returnValue
        End Get
    End Property



    Protected Overridable Function GetDatarow(item As ItemResourceEntity, mediaRef As GenericResourceEntity, resourcePrm As ResourceParameter, inlineElement As InlineElement) As DataRow
        Dim row = _datatable.NewRow()
        If item IsNot Nothing Then
            row.Item(DatatableColumn_Itemcode) = item.Name
            row.Item(DatatableColumn_Itemtitle) = item.Title
        End If
        If mediaRef IsNot Nothing Then
            row.Item(DatatableColumn_Mediacode) = If(String.IsNullOrEmpty(mediaRef.Name), String.Empty, mediaRef.Name)
            row.Item(DatatableColumn_Mediatitle) = If(String.IsNullOrEmpty(mediaRef.Title), String.Empty, mediaRef.Title)
            row.Item(DatatableColumn_Mediatype) = If(String.IsNullOrEmpty(mediaRef.MediaType), String.Empty, mediaRef.MediaType)

            If Not String.IsNullOrEmpty(mediaRef.Dimensions) Then
                Dim dimensions = mediaRef.Dimensions.Split("x"c)
                row.Item(DatatableColumn_FileWidth) = dimensions(0).Trim()
                row.Item(DatatableColumn_FileHeight) = dimensions(1).Trim()
            End If

            If item IsNot Nothing Then

                Dim height As Integer
                Dim width As Integer
                If resourcePrm IsNot Nothing Then
                    height = resourcePrm.Height
                    width = resourcePrm.Width
                End If
                If inlineElement IsNot Nothing AndAlso width = 0 AndAlso height = 0 Then
                    GetDimensionsOfInlineElement(inlineElement, mediaRef.Name, height, width)
                End If
                If width > 0 Then row.Item(DatatableColumn_FileWidthInItem) = width.ToString
                If height > 0 Then row.Item(DatatableColumn_FileHeightInItem) = height.ToString
            End If
        End If
        Return row
    End Function

    Protected Sub GetDimensionsOfInlineElement(inlineElement As InlineElement, sourceName As String,
                                           ByRef height As Integer, ByRef width As Integer)
        width = 0
        height = 0
        If inlineElement.Parameters IsNot Nothing Then
            Dim inlineParams = inlineElement.Parameters.FlattenParameters()
            Dim prm =
                    inlineParams.FirstOrDefault(
                        Function(p) _
                                                   p.Name IsNot Nothing AndAlso
                                                   p.Name.StartsWith("source", StringComparison.OrdinalIgnoreCase) AndAlso
                                                   TypeOf p Is ResourceParameter AndAlso
                                                   DirectCast(p, ResourceParameter).Value = sourceName)
            If prm IsNot Nothing Then
                Dim widthParam = inlineParams.OfType(Of IntegerParameter).FirstOrDefault(
                        Function(p) _
                                                                                p.Name IsNot Nothing AndAlso
                                                                                p.Name.StartsWith("width",
                                                                                                  StringComparison.
                                                                                                     OrdinalIgnoreCase))
                if widthParam IsNot Nothing Then
                    width = widthParam.Value
                End If

                Dim heightPararm = inlineParams.OfType(Of IntegerParameter).FirstOrDefault(
                        Function(p) _
                                                                                p.Name IsNot Nothing AndAlso
                                                                                p.Name.StartsWith("height",
                                                                                                  StringComparison.
                                                                                                     OrdinalIgnoreCase))
                If heightPararm IsNot Nothing Then
                    height = heightPararm.Value
                End If

            End If
        End If
    End Sub



    Private Function GetDataTable() As DataTable
        _datatable = New DataTable("ExcelExport")
        If Collection.Count > 0 Then
            AddColumnsToDataTable()
            FillDatatable()
        End If
        Return _datatable
    End Function


    Protected MustOverride Sub AddColumnsToDataTable()
    Protected MustOverride Sub FillDatatable()

End Class
