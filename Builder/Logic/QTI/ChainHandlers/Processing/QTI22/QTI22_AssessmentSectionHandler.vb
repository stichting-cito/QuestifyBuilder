
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Helpers.QTI22.QtiModelHelpers
Imports Questify.Builder.Logic.QTI.Model.QTI22
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Requests.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.ChainHandlers.Processing.QTI22

    Public Class QTI22_AssessmentSectionHandler
        Inherits QTI22_ChainHandlerBase

        Private ReadOnly _section As GeneralTestSection
        Private ReadOnly _testName As String
        Private ReadOnly _tempFolder As String
        Protected _adaptiveDirectoryName As String

        Public Sub New(ByVal testSection As GeneralTestSection, testName As String, tempFolder As String, packageCreator As QTI22PackageCreator)
            MyBase.New(packageCreator)
            _section = testSection
            _testName = testName
            _tempFolder = tempFolder

            If testSection IsNot Nothing AndAlso testSection.Identifier IsNot Nothing Then
                LastHandledObject = $"test {testName} - section {testSection.Identifier}"
            End If
        End Sub

        Public Overloads Overrides Function ProcessRequest(requestData As QTI22PublicationRequest) As ChainHandlerResult
            _adaptiveDirectoryName = requestData.FileTypeDictionary(PackageCreatorConstants.FileDirectoryType.adaptive)
            Return ExecuteRequest(requestData)
        End Function

        Private Function ExecuteRequest(requestData As QTI22PublicationRequest) As ChainHandlerResult
            Dim testDocumentSet As TestDocumentSet = requestData.Tests(_testName)

            Dim sectionHelper = GetSectionHelper(_section)
            AddSectionToTestDocumentSet(testDocumentSet, sectionHelper, _section, requestData, _testName, _tempFolder)
            Return ChainHandlerResult.RequestHandled
        End Function

        Protected Overridable Sub AddSectionToTestDocumentSet(testDocumentSet As TestDocumentSet, sectionHelper As TestSectionHelper, testSection As GeneralTestSection, requestData As QTI22PublicationRequest, testName As String, tempFolder As String)
            Dim sectionType As AssessmentSectionType = sectionHelper.CreateSection
            sectionHelper.AddTestSectionToAssessmentTestType(testDocumentSet.AssessmentTestType, sectionType, testSection.Parent.Identifier)
        End Sub

        Protected Overridable Function GetSectionHelper(testSection As GeneralTestSection) As TestSectionHelper
            Return New TestSectionHelper(testSection)
        End Function

    End Class
End Namespace