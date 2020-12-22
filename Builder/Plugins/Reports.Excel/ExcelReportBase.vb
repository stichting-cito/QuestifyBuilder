Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public MustInherit Class ExcelReportBase
    Implements IReportHandler

    Public ReadOnly Property IsDataGeneratedAsynchronous As Boolean Implements IReportHandler.IsDataGeneratedAsynchronous
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property IsInitDataGeneratedAsynchronous As Boolean Implements IReportHandler.IsInitDataGeneratedAsynchronous
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property ShouldUseCollectionAsInput() As Boolean Implements IReportHandler.ShouldUseCollectionAsInput
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShouldUseGridAsInput() As Boolean Implements IReportHandler.ShouldUseGridAsInput
        Get
            Return False
        End Get
    End Property

    Public WriteOnly Overridable Property GridToExport() As Object Implements IReportHandler.GridToExport
        Set(ByVal value As Object)
        End Set
    End Property

    Public Overridable Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI
        Throw New NotImplementedException
    End Function

    Public Overridable Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        Throw New NotImplementedException
    End Function

    Public Overridable Sub InitialiseData() Implements IReportHandler.InitialiseData

    End Sub

    Public Overridable Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Throw New NotImplementedException
    End Function

    Public Overridable Sub ClearErrors() Implements IReportHandler.ClearErrors
        Throw New NotImplementedException
    End Sub

    Public Overridable ReadOnly Property ShowCreateReportProgressTab() As Boolean Implements IReportHandler.ShowCreateReportProgressTab
        Get
            Return False
        End Get
    End Property

    Public Event ReportCompleted As ReportCompletedEventHandler Implements IReportHandler.ReportCompleted
    Public Overridable ReadOnly Property Overview As String Implements IReportHandler.Overview
    Public Overridable ReadOnly Property ValidationErrors As String Implements IReportHandler.ValidationErrors
    Public Overridable ReadOnly Property ResultText As String Implements IReportHandler.ResultText
    Public Overridable ReadOnly Property ExportedReportLocation As String Implements IReportHandler.ExportedReportLocation
    Public Overridable ReadOnly Property ExtraOptionTask As String Implements IReportHandler.ExtraOptionTask
    Public Overridable ReadOnly Property ExtraOptionTaskDescription() As String Implements IReportHandler.ExtraOptionTaskDescription
        Get
            Return My.Resources.ExtraOptionTaskDescription
        End Get
    End Property

    Public Overridable ReadOnly Property ShowExtraOptionsTab() As Boolean Implements IReportHandler.ShowExtraOptionsTab
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShowInitialiseProgressTab() As Boolean Implements IReportHandler.ShowInitialiseProgressTab
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShowSelectLocationTab() As Boolean Implements IReportHandler.ShowSelectLocationTab
        Get
            Return True
        End Get
    End Property



    Public Event StartProgress As EventHandler(Of StartEventArgs) Implements IReportValidationBase.StartProgress
    Public Event Progress As EventHandler(Of ProgressEventArgs) Implements IReportValidationBase.Progress
    Public Overridable ReadOnly Property Name As String Implements IReportValidationBase.Name
    Public Overridable ReadOnly Property Description As String Implements IReportValidationBase.Description
    Public Overridable Property Collection As IList(Of ResourceDto) Implements IReportValidationBase.Collection
    Public Overridable Property BankId As Integer Implements IReportValidationBase.BankId

    Public Overridable Function IsDatasourceSupported() As Boolean Implements IReportValidationBase.IsDatasourceSupported
        Throw New NotImplementedException
    End Function
End Class
