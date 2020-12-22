Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestPackageConstruction.Requests

Namespace TestPackageConstruction.ChainHandlers.Processing
    Public Class AddToTestPackageHandler
        Inherits ChainHandlerBase(Of TestPackageConstructionRequest)


        Private ReadOnly _testPackage As TestPackage
        Private ReadOnly _bankId As Integer
        Private ReadOnly _currentPosition As Integer
        Private ReadOnly _currentTestsetContext As TestSet




        Public Sub New(ByVal bankId As Integer, ByVal testpackage As TestPackage, ByVal testset As TestSet, ByVal insertIndex As Integer)
            If testpackage Is Nothing Then
                Throw New ArgumentNullException("testpackage")
            End If

            If testset Is Nothing Then
                Throw New ArgumentNullException("testset")
            End If

            If insertIndex < 0 Then
                Throw New ArgumentOutOfRangeException("insertIndex")
            End If

            _bankId = bankId
            _testPackage = testpackage
            _currentTestsetContext = testset
            _currentPosition = insertIndex
        End Sub





        Public Overrides Function ProcessRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestPackageConstructionRequest.RequestTypeEnum.Add AndAlso requestData.Tests.Count > 0 Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function


        Private Sub AddTestReferenceToTestSet(ByVal newTestReference As TestReference, ByVal addToTestset As TestSet, ByVal insertAtPosition As Integer)
            If newTestReference Is Nothing Then
                Throw New ArgumentNullException("newTestReference")
            End If

            If addToTestset Is Nothing Then
                Throw New ArgumentNullException("addToTestset")
            End If

            If insertAtPosition < 0 Then
                Throw New ArgumentOutOfRangeException("insertAtPosition should have value >=0")
            End If
            newTestReference.LockedOrder = addToTestset.LockedOrder
            addToTestset.Components.Insert(insertAtPosition, newTestReference)
        End Sub




        Private Function CreateNewTestReference(ByVal asssementTestEntity As AssessmentTestResourceEntity) As TestReference
            Dim createdTest As CreatedTestPackageNodeAndViews(Of TestReference, TestReferenceViewBase) = TestPackageFactory.CreateTestReferenceAndViews(_testPackage.IncludedViews)

            createdTest.TestNode.Identifier = asssementTestEntity.ResourceId.ToString()
            createdTest.TestNode.Title = asssementTestEntity.Name
            createdTest.TestNode.SourceName = asssementTestEntity.Name


            Return createdTest.TestNode
        End Function



        Private Function ExecuteRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Dim indexToInsertTest As Integer = _currentPosition

            Dim testResources As IDictionary(Of String, AssessmentTestResourceEntity) = TestPackageProcessingHelpers.GetTestResources(requestData, _bankId)

            For Each res As Datasources.ResourceRef In requestData.Tests
                Dim assessmentTestEntity As AssessmentTestResourceEntity = testResources(res.Identifier)

                Dim newTestRef As TestReference = CreateNewTestReference(assessmentTestEntity)
                AddTestReferenceToTestSet(newTestRef, _currentTestsetContext, indexToInsertTest)

                indexToInsertTest += 1
            Next

            Return ChainHandlerResult.RequestHandled
        End Function



    End Class
End Namespace