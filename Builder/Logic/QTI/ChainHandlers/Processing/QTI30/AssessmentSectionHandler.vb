
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Helpers.QTI30.QtiModelHelpers
Imports Questify.Builder.Logic.QTI.Model.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class AssessmentSectionHandler
        Inherits ChainHandlerBase

        Private ReadOnly _section As GeneralTestSection
        Private ReadOnly _testName As String
        Protected _adaptiveDirectoryName As String

        Public Sub New(ByVal testSection As GeneralTestSection, testName As String, packageCreator As PackageCreator)
            MyBase.New(packageCreator)
            _section = testSection
            _testName = testName

            If testSection IsNot Nothing AndAlso testSection.Identifier IsNot Nothing Then
                LastHandledObject = $"test {testName} - section {testSection.Identifier}"
            End If
        End Sub

        Public Overloads Overrides Function ProcessRequest(requestData As PublicationRequest) As ChainHandlerResult
            _adaptiveDirectoryName = requestData.FileTypeDictionary(PackageCreatorConstants.FileDirectoryType.adaptive)
            Return ExecuteRequest(requestData)
        End Function

        Private Function ExecuteRequest(requestData As PublicationRequest) As ChainHandlerResult
            Dim testDocumentSet As TestDocumentSet = requestData.Tests(_testName)

            Dim sectionHelper = GetSectionHelper(_section)
            AddSectionToTestDocumentSet(testDocumentSet, sectionHelper, _section, requestData, _testName)
            Return ChainHandlerResult.RequestHandled
        End Function

        Protected Overridable Sub AddSectionToTestDocumentSet(testDocumentSet As TestDocumentSet, sectionHelper As TestSectionHelper, testSection As GeneralTestSection, requestData As PublicationRequest, testName As String)
            Dim sectionType As AssessmentSectionType = sectionHelper.CreateSection()
            sectionHelper.AddAdaptiveModuleToSection(sectionType, requestData.Resources, requestData.FilesPerEntity, testName, PackageCreator.TempWorkingDirectory.FullName, _adaptiveDirectoryName, requestData.ResourceTypeDictionary, PackageCreator)
            sectionHelper.AddTestSectionToAssessmentTestType(testDocumentSet.AssessmentTestType, sectionType, testSection.Parent.Identifier)
        End Sub

        Protected Overridable Function GetSectionHelper(testSection As GeneralTestSection) As TestSectionHelper
            Return New TestSectionHelper(testSection)
        End Function

    End Class
End Namespace