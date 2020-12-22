Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Conversion
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Logic.Service.Model.Entities.Custom
Imports Questify.Builder.UI

Public Class CustomBankPropertyExcelReport
    Inherits ExcelReportBase
    Implements IReportHandler


    Private _selectedEntities As IList(Of ResourceDto)
    Private ReadOnly _optionValidator As SelectColumnsOptionsValidator
    Private _datatable As DataTable
    Private _customPropertiesCount As Integer = 0
    Private _resultText As String



    Public Sub New()
        _optionValidator = New SelectColumnsOptionsValidator()
    End Sub



    Public Event StartCollectColumns(sender As Object, e As StartEventArgs) Implements IReportValidationBase.StartProgress

    Protected Sub OnStartCollectColumns(ByVal startEventArgs As StartEventArgs)
        RaiseEvent StartCollectColumns(Me, startEventArgs)
    End Sub

    Public Event ProgressCollectColumns(sender As Object, e As Cito.Tester.Common.ProgressEventArgs) Implements IReportValidationBase.Progress

    Protected Sub OnProgressCollectColumns(ByVal progressEventArgs As Cito.Tester.Common.ProgressEventArgs)
        RaiseEvent ProgressCollectColumns(Me, progressEventArgs)
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
        End Get
    End Property

    Public Overrides ReadOnly Property ExtraOptionTaskDescription() As String Implements IReportHandler.ExtraOptionTaskDescription
        Get
        End Get
    End Property

    Public Overrides ReadOnly Property Overview() As String Implements IReportHandler.Overview
        Get
            If _optionValidator IsNot Nothing Then
                Return String.Format(My.Resources.OverviewGridToExcel, vbCrLf, Me.Name, _optionValidator.ExportPath)
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
            Return My.Resources.ExportCustomPropertyReportDescription
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String Implements IReportValidationBase.Name
        Get
            Return My.Resources.ReportNameKenmerkenOverzicht
        End Get
    End Property




    Public Overrides Function IsDatasourceSupported() As Boolean Implements IReportValidationBase.IsDatasourceSupported
        Dim returnValue As Boolean = False
        If (_selectedEntities IsNot Nothing) Then
            If _selectedEntities.OfType(Of CustomBankPropertyResourceDto)().Any() Then
                returnValue = True
            End If
        End If
        Return returnValue
    End Function

    Public Overrides Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Dim exporter As New ExcelExport()
        Dim returnValue As Boolean = False

        Try
            exporter.ExportDataTable(_datatable, _optionValidator.ExportPath)
            _resultText = String.Format(My.Resources.ReportSuccessful, Me.Name)
            returnValue = True
        Catch ex As Exception
            _resultText = String.Format(My.Resources.ReportUnSuccessful, Me.Name, vbCrLf, ex.Message)
            returnValue = False
        Finally
            _datatable.Dispose()
        End Try


        Return returnValue
    End Function

    Public Overrides Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        Dim selectReportLocation As New SelectReportLocation
        _optionValidator.ExportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), String.Format("{0}.xlsx", _optionValidator.Filename))
        selectReportLocation.OptionsValidatorBindingSource.DataSource = _optionValidator
        Return selectReportLocation
    End Function

    Public Overrides Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI

    End Function

    Public Overrides Sub InitialiseData() Implements IReportHandler.InitialiseData
        If (_selectedEntities IsNot Nothing AndAlso _selectedEntities.Count > 0) Then
            Dim entities = GetCustomPropertiesFromTheService(_selectedEntities)
            _datatable = GetDataTable(entities)
        End If
    End Sub

    Public Overrides Sub ClearErrors() Implements IReportHandler.ClearErrors
        If _optionValidator IsNot Nothing Then
            _optionValidator.ClearError()
        End If
    End Sub



    Private Function GetDataTable(ByVal customBankProperties As List(Of CustomBankPropertyEntity)) As DataTable
        Dim datatable As New DataTable("ExcelExport")
        If customBankProperties.Count > 0 Then
            _customPropertiesCount = customBankProperties.Count
            AddColumnsToDataTable(datatable)

            FillDataTable(customBankProperties, datatable)
            _optionValidator.Filename = String.Format(My.Resources.FileNameCustomReport, customBankProperties(0).Bank.Name)
        End If
        Return datatable
    End Function

    Private Sub AddColumnsToDataTable(ByRef dataTable As DataTable)
        Dim propertyName As String

        propertyName = GetResourceStringByResourceName(ReportProperty.BankName)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertiesCount)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.Name)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.Title)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertyType)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.ApplicableToMask)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.Publishable)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.Scorable)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertyName)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertyTitle)
        If Not dataTable.Columns.Contains(propertyName) Then
            dataTable.Columns.Add(propertyName)
        End If

    End Sub

    Private Sub FillDataTable(ByVal customBankPropertyEntities As List(Of CustomBankPropertyEntity), ByRef datatable As DataTable)
        OnStartCollectColumns(New StartEventArgs(customBankPropertyEntities.Count))

        For Each customPropery As CustomBankPropertyEntity In customBankPropertyEntities
            OnProgressCollectColumns(New Cito.Tester.Common.ProgressEventArgs(String.Format(My.Resources.ProgressMessage, customPropery.Name)))

            Dim newDataRow As DataRow = datatable.NewRow()
            If TypeOf customPropery Is ListCustomBankPropertyEntity Then
                Dim entity As ListCustomBankPropertyEntity = TryCast(customPropery, ListCustomBankPropertyEntity)
                For Each valueCustomBankPropertyEntity As ListValueCustomBankPropertyEntity In entity.ListValueCustomBankPropertyCollection
                    FillCustomPropertyDataRow(customPropery, newDataRow)
                    FillValueCustomProperyDataRow(valueCustomBankPropertyEntity, newDataRow)
                    datatable.Rows.Add(newDataRow)
                    newDataRow = datatable.NewRow()
                Next
            Else
                FillCustomPropertyDataRow(customPropery, newDataRow)
                datatable.Rows.Add(newDataRow)
            End If
        Next
    End Sub

    Private Sub FillValueCustomProperyDataRow(ByVal listValueCustomBankPropertyEntity As ListValueCustomBankPropertyEntity, ByVal dataRow As DataRow)

        Dim propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertyName)
        If (listValueCustomBankPropertyEntity.Name IsNot Nothing) Then
            dataRow.Item(propertyName) = listValueCustomBankPropertyEntity.Name
        Else
            dataRow.Item(propertyName) = My.Resources.NoInfo
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertyTitle)
        If (listValueCustomBankPropertyEntity.Title IsNot Nothing) Then
            dataRow.Item(propertyName) = listValueCustomBankPropertyEntity.Title
        Else
            dataRow.Item(propertyName) = My.Resources.NoInfo
        End If

    End Sub

    Private Sub FillCustomPropertyDataRow(ByVal customBankPropertyEntity As CustomBankPropertyEntity, ByRef dataRow As DataRow)

        Dim propertyName = GetResourceStringByResourceName(ReportProperty.BankName)
        If (customBankPropertyEntity.Bank.Name IsNot Nothing) Then
            dataRow.Item(propertyName) = customBankPropertyEntity.Bank.Name
        Else
            dataRow.Item(propertyName) = String.Empty
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertiesCount)
        dataRow.Item(propertyName) = _customPropertiesCount

        propertyName = GetResourceStringByResourceName(ReportProperty.Name)
        If (customBankPropertyEntity.Name IsNot Nothing) Then
            dataRow.Item(propertyName) = customBankPropertyEntity.Name
        Else
            dataRow.Item(propertyName) = String.Empty
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.Title)
        If (customBankPropertyEntity.Name IsNot Nothing) Then
            dataRow.Item(propertyName) = customBankPropertyEntity.Title
        Else
            dataRow.Item(propertyName) = String.Empty
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.CustomPropertyType)
        If customBankPropertyEntity.CustomPropertyType IsNot Nothing Then
            dataRow.Item(propertyName) = customBankPropertyEntity.CustomPropertyType
        Else
            dataRow.Item(propertyName) = String.Empty
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.ApplicableToMask)
        If (customBankPropertyEntity.ApplicableToString IsNot Nothing) Then
            dataRow.Item(propertyName) = customBankPropertyEntity.ApplicableToString
        Else
            dataRow.Item(propertyName) = String.Empty
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.Publishable)
        If (customBankPropertyEntity.Publishable) Then
            dataRow.Item(propertyName) = GetResourceStringByResourceName("Yes")
        Else
            dataRow.Item(propertyName) = GetResourceStringByResourceName("No")
        End If

        propertyName = GetResourceStringByResourceName(ReportProperty.Scorable)
        If (customBankPropertyEntity.Scorable) Then
            dataRow.Item(propertyName) = GetResourceStringByResourceName("Yes")
        Else
            dataRow.Item(propertyName) = GetResourceStringByResourceName("No")
        End If

    End Sub

    Private Function GetResourceStringByResourceName(ByVal resourceName As String) As String
        Dim returnValue As String = String.Empty
        Try
            returnValue = My.Resources.ResourceManager.GetString(resourceName)
        Catch ex As Exception
            returnValue = resourceName
        End Try
        If returnValue Is Nothing Then
            returnValue = resourceName
        End If
        Return returnValue
    End Function

    Private Function GetCustomPropertiesFromTheService(ByVal collection As IList(Of ResourceDto)) As List(Of CustomBankPropertyEntity)
        Dim listOfBankEntities As New Dictionary(Of Integer, BankEntity)
        If collection Is Nothing OrElse collection.Count = 0 Then Return New List(Of CustomBankPropertyEntity)

        Dim customBankProperties = BankFactory.Instance.GetCustomBankProperties(collection.OfType(Of CustomBankPropertyResourceDto).Select(Function(c) c.resourceId).ToList).ToList

        For Each customBankProperty As CustomBankPropertyEntity In customBankProperties
            If (customBankProperty.Bank Is Nothing) Then
                If Not listOfBankEntities.TryGetValue(customBankProperty.BankId, customBankProperty.Bank) Then
                    customBankProperty.Bank = BankFactory.Instance.GetBank(customBankProperty.BankId)
                    listOfBankEntities.Add(customBankProperty.BankId, customBankProperty.Bank)
                End If
            End If
        Next

        Return customBankProperties.OrderBy(Function(x) x.Name).ToList()
    End Function


End Class
