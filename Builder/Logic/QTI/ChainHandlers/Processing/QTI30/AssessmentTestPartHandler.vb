
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Helpers.QTI30.QtiModelHelpers
Imports Questify.Builder.Logic.QTI.Model.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class AssessmentTestPartHandler
        Inherits ChainHandlerBase(Of PublicationRequest)

        Private ReadOnly _testPart As GeneralTestPart
        Private ReadOnly _testName As String
        Protected ReadOnly PackageCreator As PackageCreator

        Public Sub New(ByVal testPart As GeneralTestPart, testName As String, packageCreator As PackageCreator)
            _testPart = testPart
            _testName = testName
            Me.PackageCreator = packageCreator

            If testPart IsNot Nothing AndAlso testPart.Identifier IsNot Nothing Then LastHandledObject = $"test {testName} - testpart {testPart.Identifier}"
        End Sub

        Public Overrides Function ProcessRequest(ByVal requestData As PublicationRequest) As ChainHandlerResult
            Return ExecuteRequest(requestData)
        End Function

        Private Function ExecuteRequest(requestData As PublicationRequest) As ChainHandlerResult
            Dim testDocumentSet As TestDocumentSet = requestData.Tests(_testName)
            Dim testPartHelper = GetTestPartHelper(_testPart)
            AddTestPart(testPartHelper, testDocumentSet)
            Return ChainHandlerResult.RequestHandled
        End Function

        Protected Overridable Function GetTestPartHelper(testPart As GeneralTestPart) As TestPartHelper
            Return New TestPartHelper(testPart)
        End Function

        Protected Overridable Sub AddTestPart(testPartHelper As TestPartHelper, testDocumentSet As TestDocumentSet)
            Dim testPartType As TestPartType = testPartHelper.CreateTestPart
            testPartHelper.AddTestPartToAssessmentTestType(testDocumentSet.AssessmentTestType, testPartType)
        End Sub

    End Class
End Namespace