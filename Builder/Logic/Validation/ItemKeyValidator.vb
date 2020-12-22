Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text
Imports Cito.Tester.Common
Imports Enums
Imports HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Public Class ItemKeyValidator
    Inherits BaseValidator

    Public Overrides ReadOnly Property Description As String
        Get
            Return My.Resources.DescriptionItemKeyValidator
        End Get
    End Property

    Public Overrides Function IsDatasourceSupported() As Boolean
        Return Collection.OfType(Of AssessmentTestResourceDto).Any
    End Function

    Public Overrides Function GetValidationNameLocalized() As String
        Return My.Resources.NameItemKeyValidator
    End Function

    Public Overrides Function DoValidation() As ValidationResult
        Dim result As Boolean = True
        Dim assessmentIds = _entities.OfType(Of AssessmentTestResourceDto).Select(Function(a) a.resourceId)
        Dim assessmentResources = ResourceFactory.Instance.GetResourcesByIdsWithOption(assessmentIds.ToList, New AssessmentTestResourceEntityFactory(), New ResourceRequestDTO()).OfType(Of AssessmentTestResourceEntity)
        For Each assessmentTest In assessmentResources
            result = result And ValidateAssessmentTest(assessmentTest)
        Next
        If result Then
            Return ValidationResult.Valid
        End If

        Return ValidationResult.Warning
    End Function

    Private Function ValidateAssessmentTest(assessmentTestResourceEntity As AssessmentTestResourceEntity) As Boolean
        Dim test As AssessmentTest2 = GetTestDefinitionFromResource(assessmentTestResourceEntity)
        If test Is Nothing Then
            Return True
        End If
        Dim itemsInTest As ReadOnlyCollection(Of ItemReference2) = test.GetAllItemReferencesInTest()
        Dim itemResources As ItemResourceEntityCollection = ResourceFactory.Instance.GetItemsByCodes(itemsInTest.Select(Function(i) i.SourceName).ToList(), assessmentTestResourceEntity.BankId, New ItemResourceRequestDTO())
        Dim report As New StringBuilder()
        Dim valid As Boolean = True
        Dim invalidCount As Integer = 0
        For Each itemResource As ItemResourceEntity In itemResources
            If String.IsNullOrWhiteSpace(itemResource.KeyValues) AndAlso Not IsItemInformational(itemResource) Then
                invalidCount += 1
                valid = False
                report.AppendLine(String.Format(My.Resources.WarningItemNoKey, itemResource.Name))
            End If
        Next
        If (Not valid) Then
            _exportString = report.ToString()
            _isReportAvailable = True
            _resText.Append(String.Format(My.Resources.WarningItemsWithoutKeys, invalidCount))

        End If
        Return valid
    End Function

    Private Function IsItemInformational(ByVal itemEntity As ItemResourceEntity) As Boolean
        Return itemEntity.ItemTypeFromItemLayoutTemplate = ItemTypeEnum.Informational
    End Function

    Private Shared Function GetTestDefinitionFromResource(ByVal testEntity As AssessmentTestResourceEntity) As AssessmentTest2

        testEntity.ResourceData = ResourceFactory.Instance.GetResourceData(testEntity)
        Dim result As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(testEntity.ResourceData.BinData, True)
        Return result.AssessmentTestv2
    End Function

End Class
