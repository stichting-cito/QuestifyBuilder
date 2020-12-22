Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text
Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Enums
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Questify.Builder.Logic.TestConstruction
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class TestConstructionValidator
    Inherits BaseValidator

    Private WithEvents _constructionFacade As TestConstructionFacade = New TestConstructionFacade

    Private ReadOnly _historicConstructionExceptions As New List(Of ChainHandlerException)

    Public Overrides ReadOnly Property Description() As String
        Get
            Return My.Resources.TestConstructionValidator_Description
        End Get
    End Property

    Public Overrides Function GetValidationNameLocalized() As String
        Return My.Resources.TestConstructionValidator_Name
    End Function

    Public Overrides Function IsDatasourceSupported() As Boolean
        Return Collection.OfType(Of AssessmentTestResourceDto).Any
    End Function

    Public Overrides Function doValidation() As ValidationResult
        Dim returnValue As ValidationResult = ValidationResult.Valid
        Dim validationMessagesBuilder As New StringBuilder()
        _resText = New StringBuilder()
        Using resourceManager As DataBaseResourceManager = New DataBaseResourceManager(_bankId)


            For Each testResource In _entities.OfType(Of AssessmentTestResourceDto)
                Dim test As AssessmentTest2 = testResource.GetAssessmentTest
                Dim itemsInTest As ReadOnlyCollection(Of ItemReference2) = test.GetAllItemReferencesInTest()

                Dim tempTest As AssessmentTest2 = CreateTempAssessmentTestWithOnePartAndSection()
                Dim tempSection As TestSection2 = tempTest.TestParts(0).Sections(0)
                Dim itemsToAdd As IList(Of ResourceRef) = ResourceRef.FromItemReferences(itemsInTest)

                _historicConstructionExceptions.Clear()

                If Not TestConstructionOp.ValidateAddItemsToTest(tempTest, resourceManager, itemsToAdd, tempSection, 0, Me._constructionFacade) Then
                    returnValue = ValidationResult.Warning
                    With validationMessagesBuilder
                        .AppendLine(String.Format(My.Resources.TestConstructionValidator_TestContainsValidationErrors, test.Title, _historicConstructionExceptions.Count))
                        For Each ex As ChainHandlerException In _historicConstructionExceptions
                            .AppendLine(String.Format("  - {0}", ex.Message))
                        Next
                        .AppendLine()
                    End With
                End If
            Next

        End Using
        _exportString = validationMessagesBuilder.ToString
        _resText.Append(_exportString)
        Return returnValue
    End Function

    Private Function CreateTempAssessmentTestWithOnePartAndSection() As AssessmentTest2
        Dim tempTest As New AssessmentTest2
        Dim tempPart As New TestPart2
        Dim tempSection As New TestSection2

        tempTest.TestParts.Add(tempPart)
        tempPart.Sections.Add(tempSection)

        Return tempTest
    End Function

    Private Sub _constructionFacade_ResolveValidationError(ByVal sender As Object, ByVal e As TestConstructionValidationEventArgs) Handles _constructionFacade.ResolveValidationError
        e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.Abort
        _historicConstructionExceptions.Add(e.UnderlyingException)
    End Sub
End Class