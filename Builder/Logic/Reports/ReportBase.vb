Imports System.Linq
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public MustInherit Class ReportBase
    Implements IReportHandler

    Protected _selectedEntities As IList(Of ResourceDto)
    Protected _helper As ReportHelperClass
    Protected _handlers As List(Of IItemPreviewHandler)
    Protected _optionValidatorBase As OptionValidatorExportBase
    Public Event StartProgress As EventHandler(Of StartEventArgs) Implements IReportValidationBase.StartProgress
    Public Event ReportCompleted As ReportCompletedEventHandler Implements IReportHandler.ReportCompleted
    Public Event IReportValidationBase_Progress As EventHandler(Of Cito.Tester.Common.ProgressEventArgs) Implements IReportValidationBase.Progress
    Protected _test As AssessmentTest2
    Protected _resultText As String


    Public Overridable ReadOnly Property Name() As String Implements IReportHandler.Name
        Get
            If Me.Collection.Any() Then
                Dim resource = TryCast(Me.Collection.First(), ResourceDto)

                Select Case resource.GetType.ToString
                    Case GetType(AssessmentTestResourceDto).ToString
                        Return My.Resources.ImageReportNameTest
                    Case GetType(ItemResourceDto).ToString
                        Return My.Resources.ImageReportNameItem
                End Select
            End If

            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property Description() As String Implements IReportHandler.Description
        Get
            Dim resource = Me.Collection.FirstOrDefault
            If resource IsNot Nothing Then
                Select Case resource.GetType
                    Case GetType(AssessmentTestResourceDto)
                        Return My.Resources.ReportDescriptionTest
                    Case GetType(ItemResourceDto)
                        Return My.Resources.ReportDescriptionItem
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public Property Collection() As IList(Of ResourceDto) Implements IReportHandler.Collection
        Set
            _selectedEntities = value
            If _selectedEntities IsNot Nothing Then
                Dim first = _selectedEntities.FirstOrDefault
                If first IsNot Nothing Then
                    _helper = New ReportHelperClass(first.bankId)
                End If
            End If
        End Set
        Get
            Return _selectedEntities
        End Get
    End Property

    Public ReadOnly Property ValidationErrors() As String Implements IReportHandler.ValidationErrors
        Get
            Dim returnValue As String = String.Empty

            If _optionValidatorBase IsNot Nothing Then
                returnValue = _optionValidatorBase.Error
            End If

            If Handlers IsNot Nothing AndAlso Not Handlers.Any() Then
                returnValue += My.Resources.NoCommonTargetFound + Environment.NewLine
            End If

            Return returnValue
        End Get
    End Property


    Public WriteOnly Property GridToExport() As Object Implements IReportHandler.GridToExport
        Set(ByVal value As Object)
        End Set
    End Property

    Public MustOverride Property BankId As Integer Implements IReportValidationBase.BankId

    Public ReadOnly Property ExtraOptionTask() As String Implements IReportHandler.ExtraOptionTask
        Get
            Return My.Resources.ExtraOptionTask
        End Get
    End Property

    Public ReadOnly Property ExtraOptionTaskDescription() As String Implements IReportHandler.ExtraOptionTaskDescription
        Get
            Return My.Resources.ExtraOptionTaskDescription
        End Get
    End Property

    Public Function IsDatasourceSupported() As Boolean Implements IReportHandler.IsDatasourceSupported
        Return _selectedEntities IsNot Nothing AndAlso
               (_selectedEntities.OfType(Of AssessmentTestResourceDto).Any OrElse _selectedEntities.OfType(Of ItemResourceDto).Any)
    End Function
    Public ReadOnly Property ShouldUseCollectionAsInput() As Boolean Implements IReportHandler.ShouldUseCollectionAsInput
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShouldUseGridAsInput() As Boolean Implements IReportHandler.ShouldUseGridAsInput
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowCreateReportProgressTab() As Boolean Implements IReportHandler.ShowCreateReportProgressTab
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShowExtraOptionsTab() As Boolean Implements IReportHandler.ShowExtraOptionsTab
        Get
            Return True
        End Get
    End Property

    Public OverRidable ReadOnly Property ShowInitialiseProgressTab() As Boolean Implements IReportHandler.ShowInitialiseProgressTab
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ShowSelectLocationTab() As Boolean Implements IReportHandler.ShowSelectLocationTab
        Get
            Return True
        End Get
    End Property


    Public MustOverride ReadOnly Property Overview As String Implements IReportHandler.Overview


    Public ReadOnly Property ResultText() As String Implements IReportHandler.ResultText
        Get
            Return Me._resultText
        End Get
    End Property
    Public ReadOnly Property GenerateDataAsync() As Boolean Implements IReportHandler.IsDataGeneratedAsynchronous
        Get
            Return True
        End Get
    End Property
    Public MustOverride ReadOnly Property ExportedReportLocation As String Implements IReportHandler.ExportedReportLocation

    Public MustOverride ReadOnly Property IsInitDataGeneratedAsynchronous As Boolean Implements IReportHandler.IsInitDataGeneratedAsynchronous

    Public MustOverride Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI

    Public MustOverRide Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI

    Public Overridable Sub InitialiseData() Implements IReportHandler.InitialiseData
        Throw New NotImplementedException
    End Sub

    Public Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Throw New NotImplementedException
    End Function

    Public Sub ClearErrors() Implements IReportHandler.ClearErrors
        If _optionValidatorBase IsNot Nothing Then
            _optionValidatorBase.ClearErrors()
        End If
    End Sub

    Protected MustOverride Function GetAssessmentTest(resource As ResourceDto) As AssessmentTest2

    Protected ReadOnly Property Handlers As List(Of IItemPreviewHandler)
        Get
            If _handlers Is Nothing Then
                Dim resource = Me.Collection.FirstOrDefault
                If resource IsNot Nothing Then
                    Select Case resource.GetType
                        Case GetType(AssessmentTestResourceDto)
                            _handlers = _helper.GetPreviewMethodsByTest(GetAssessmentTest(DirectCast(resource, AssessmentTestResourceDto)))
                        Case GetType(ItemResourceDto)
                            _handlers = _helper.GetPreviewMethodsByItems(_selectedEntities)
                    End Select
                End If
            End If
            Return _handlers
        End Get
    End Property


End Class
