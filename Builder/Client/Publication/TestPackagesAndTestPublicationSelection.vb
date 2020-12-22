Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Client.ValidationService
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class TestPackagesAndTestPublicationSelection
    Implements IPublicationSelection

    Private ReadOnly _selectedEntities As List(Of ResourceDto)
    Private _testsToPublish As List(Of AssessmentTestResourceDto)
    Private _testPackagesToPublish As List(Of TestPackageResourceDto)
    Private _testToBeValidated As List(Of AssessmentTestResourceDto)
    Private _testsToPublishString As String = String.Empty
    Private _testPackagesToPublishString As String = String.Empty
    Private ReadOnly _bankId As Integer
    Private _title As String
    Private _testModels As Dictionary(Of String, Object)




    Sub New(bankId As Integer, selectedEntities As List(Of ResourceDto))
        _bankId = bankId
        _selectedEntities = selectedEntities
    End Sub

    Public ReadOnly Property Title As String Implements IPublicationSelection.Title
        Get
            Return _title
        End Get
    End Property

    Public ReadOnly Property TestNames As IEnumerable(Of String) Implements IPublicationSelection.TestNames
        Get
            Return _testsToPublish.Select(Function(t) t.name).ToList()
        End Get
    End Property

    Public ReadOnly Property TestPackageNames As IEnumerable(Of String) Implements IPublicationSelection.TestPackageNames
        Get
            Return _testPackagesToPublish.Select(Function(t) t.name).ToList()
        End Get
    End Property

    Public ReadOnly Property TestNamesToBeValidated As IEnumerable(Of String) Implements IPublicationSelection.TestNamesToBeValidated
        Get
            Return _testToBeValidated.Where(Function(entity) TypeOf entity Is AssessmentTestResourceDto).Cast(Of AssessmentTestResourceDto)().Select(Function(t) t.name).ToList()
        End Get
    End Property

    Public ReadOnly Property IsEmpty As Boolean Implements IPublicationSelection.IsEmpty
        Get
            Return _testPackagesToPublish.Count = 0 AndAlso _testsToPublish.Count = 0
        End Get
    End Property

    Public ReadOnly Property ContainsItems As Boolean Implements IPublicationSelection.ContainsItems
        Get
            For Each testObject As KeyValuePair(Of String, Object) In _testModels
                If TypeOf testObject.Value Is AssessmentTest2 Then
                    ContainsItems = GeneralHelper.DoesTestContainsItems(DirectCast(testObject.Value, AssessmentTest2))
                Else
                    Throw New NotSupportedException(
                        $"Test object is of unknown type '{testObject.GetType().ToString()}'")
                End If
                If ContainsItems Then
                    Exit For
                End If
            Next
            Return ContainsItems
        End Get
    End Property


    Public ReadOnly Property IsBulkExport As Boolean Implements IPublicationSelection.IsBulkExport
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property WizardDescription As String Implements IPublicationSelection.WizardDescription
        Get
            Return My.Resources.ThisWizardHelpsYouToPublicateTheSelectedTest
        End Get
    End Property



    Public Sub Initialise() Implements IPublicationSelection.Initialise
        _testModels = New Dictionary(Of String, Object)
        _testsToPublish = New List(Of AssessmentTestResourceDto)
        _testPackagesToPublish = New List(Of TestPackageResourceDto)
        _testToBeValidated = New List(Of AssessmentTestResourceDto)
        For Each e In _selectedEntities
            If TypeOf e Is AssessmentTestResourceDto Then
                _testsToPublish.Add(DirectCast(e, AssessmentTestResourceDto))
            ElseIf TypeOf e Is TestPackageResourceDto Then
                _testPackagesToPublish.Add(DirectCast(e, TestPackageResourceDto))
            Else
            End If
        Next

        If _testPackagesToPublish IsNot Nothing AndAlso Not _testPackagesToPublish.Count = 0 Then
            Dim listOfTestCodes As New List(Of String)
            For Each testPackageEntity As TestPackageResourceDto In _testPackagesToPublish
                Dim serializedTestPackage As Byte() = GeneralHelper.GetSerializedEntityFromEntityToPublish(testPackageEntity.resourceId)
                Dim testPackage As TestPackage = TestPackageFactory.ReturnTestPackageModelFromByteArray(serializedTestPackage)
                For Each testReference As TestReference In testPackage.GetAllTestReferencesInTestPackage
                    listOfTestCodes.Add(testReference.Title)
                Next
                _testPackagesToPublishString = String.Concat(_testPackagesToPublishString, testPackageEntity.title)
            Next
            _testsToPublish = DtoFactory.Test.GetTestsByCodes(listOfTestCodes, _bankId).ToList
            _testToBeValidated = _testsToPublish
            _title = String.Format(My.Resources.PublicationOfTheTestPackage, _testPackagesToPublishString)
        Else
            _testToBeValidated = _selectedEntities.OfType(Of AssessmentTestResourceDto).ToList()
            _title = String.Format(My.Resources.PublicationOfTheTest, _testsToPublishString)
        End If
        FillTestModel(_testsToPublish)
    End Sub


    Public Function AtLeastOneHandlerAvailable() As Boolean Implements IPublicationSelection.AtLeastOneHandlerAvailable
        Using validationClient = New ValidationServiceClient()
            Return validationClient.AtLeastOneHandlerAvailable(_bankId, TestNamesToBeValidated.ToArray())
        End Using
    End Function

    Public Function DefaultPublicationName(fileExtension As String) As String Implements IPublicationSelection.DefaultPublicationName
        If TestPackageNames.Count() = 1 Then
            Return $"{TestPackageNames.First()}{fileExtension}"
        ElseIf TestNames.Count() = 1 Then
            Return $"{TestNames.First()}{fileExtension}"
        End If
        Return ""
    End Function



    Private Sub FillTestModel(testsToPublish As IEnumerable(Of AssessmentTestResourceDto))
        For Each testEntity In testsToPublish
            Dim serializedTest As Byte() = GeneralHelper.GetSerializedEntityFromEntityToPublish(testEntity.resourceId)

            Dim returnValue As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(serializedTest, True)
            _testModels.Add(testEntity.name, returnValue.AssessmentTestv2)

            If Not String.IsNullOrEmpty(_testsToPublishString) Then
                _testsToPublishString = String.Concat(_testsToPublishString, ", ")
            End If
            _testsToPublishString = String.Concat(_testsToPublishString, testEntity.name)
        Next
    End Sub





End Class
