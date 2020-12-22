Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Text
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public MustInherit Class BaseValidator
    Implements IValidateHandler


    Protected _bankId As Integer
    Protected _entities As IList(Of ResourceDto)
    Protected _resText As New StringBuilder()
    Protected _exportString As String
    Protected _isReportAvailable As Boolean = False



    Public Event Progress(sender As Object, e As ProgressEventArgs) Implements IReportValidationBase.Progress
    Public Event StartProgress(sender As Object, e As StartEventArgs) Implements IReportValidationBase.StartProgress

    Protected Sub OnValidationProgress(ByVal e As ProgressEventArgs)
        RaiseEvent Progress(Me, e)
    End Sub

    Protected Sub OnStartValidationProgress(ByVal e As StartEventArgs)
        RaiseEvent StartProgress(Me, e)
    End Sub

    Public Function DatasourceMustBeAssesment(ByVal extraValidation As Func(Of ResourceDto, Boolean)) As Boolean
        Dim returnValue As Boolean = False
        If _entities IsNot Nothing Then
            For Each assessment In _entities.OfType(Of AssessmentTestResourceDto)

                WhenObject(assessment,
                              IsType(Of AssessmentTestResourceDto)(Sub(assesment)
                                                                       If (extraValidation Is Nothing) Then
                                                                           returnValue = True
                                                                       Else
                                                                           returnValue = extraValidation(assesment)
                                                                       End If
                                                                   End Sub),
                                Otherwise(Sub() Debug.Assert(False, "Not Handled")))
            Next
        End If
        Return returnValue
    End Function



    Protected Property BankId As Integer Implements IReportValidationBase.BankId
        Set(value As Integer)
            _bankId = value
        End Set
        Get
            Return _bankId
        End Get
    End Property

    Public ReadOnly Property IsReportAvailable As Boolean Implements IValidateHandler.IsReportAvailable
        Get
            Return _isReportAvailable
        End Get
    End Property

    Public Property Collection As IList(Of ResourceDto) Implements IValidateHandler.Collection
        Get
            Return _entities
        End Get
        Set(value As IList(Of ResourceDto))
            _entities = value
        End Set
    End Property

    Public ReadOnly Property Name As String Implements IReportValidationBase.Name
        Get
            Return GetValidationNameLocalized()
        End Get
    End Property


    Public MustOverride ReadOnly Property Description As String Implements IReportValidationBase.Description

    Public MustOverride Function IsDatasourceSupported() As Boolean Implements IReportValidationBase.IsDatasourceSupported


    Protected Function TestContainsAdaptiveSections(ByVal cbtView As AssessmentTestViewBase) As Boolean
        If cbtView IsNot Nothing Then
            For Each testPart As TestPartViewBase In cbtView.TestParts
                For Each section As TestComponentViewBase In testPart.Sections
                    If IsAdaptive(section) Then
                        Return True
                    End If
                Next
            Next
        End If
        Return False
    End Function

    Protected Overridable Function IsAdaptive(ByVal section As TestComponentViewBase) As Boolean
        Return False
    End Function

    Public Function Validate() As ValidationResult Implements IValidateHandler.Validate
        Dim res As ValidationResult = DoValidation()

        If Not (res = ValidationResult.Valid) Then
            _isReportAvailable = _resText.Length > 0
        End If

        Return res
    End Function

    Public ReadOnly Property ResultText As String Implements IValidateHandler.ResultText
        Get
            Return _resText.ToString()
        End Get
    End Property

    Public Function GenerateReport() As String Implements IValidateHandler.GenerateReport
        Return _exportString
    End Function

    Public Sub AddValidationLine(ByVal txt As String)
        _resText.AppendLine(txt)
    End Sub

    Public MustOverride Function DoValidation() As ValidationResult

    Public MustOverride Function GetValidationNameLocalized() As String

    Protected Function GetTestFromResource(ByVal testEntity As AssessmentTestResourceDto) As AssessmentTest2
        Dim testDefinition As AssessmentTest2 = Nothing
        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceDataByResourceId(testEntity.resourceId)
        Dim result As ReturnedAssessmentTestModelInfo
        result = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(data.BinData, True)
        testDefinition = result.AssessmentTestv2
        Return testDefinition
    End Function

End Class
