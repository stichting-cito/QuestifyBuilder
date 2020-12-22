Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.TestPackageConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestPackageConstruction.ChainHandlers.Processing

    Class AddDependenciesToTestPackage
        Inherits ChainHandlerBase(Of TestPackageConstructionRequest)


        Private ReadOnly _bankId As Integer
        Private ReadOnly _testPackageResource As TestPackageResourceEntity




        Public Sub New(ByVal testPackageResource As TestPackageResourceEntity)
            If testPackageResource Is Nothing Then
                Throw New ArgumentNullException("testPackageResource")
            End If

            _testPackageResource = testPackageResource
            _bankId = testPackageResource.BankId
        End Sub





        Public Overloads Overrides Function ProcessRequest(requestData As TestPackageConstructionRequest) _
            As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Add AndAlso requestData.Tests.Count > 0 _
                Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function


        Private Sub AddDependentResourceToTest(ByVal testPackageResource As TestPackageResourceEntity,
                                       ByVal resource As ResourceEntity)
            If testPackageResource Is Nothing Then
                Throw New ArgumentNullException("testPackageResource")
            End If

            If resource Is Nothing Then
                Throw New ArgumentNullException("resource")
            End If

            If Not testPackageResource.ContainsDependentResource(resource) Then
                DependencyManagement.AddDependentResourceToResource(testPackageResource, resource)
            End If
        End Sub



        Private Function ExecuteRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Dim testResources As IDictionary(Of String, AssessmentTestResourceEntity) =
                    TestPackageProcessingHelpers.GetTestResources(requestData, _bankId)

            For Each ref As ResourceRef In requestData.Tests
                Dim testEntity As AssessmentTestResourceEntity = testResources(ref.Identifier)

                AddDependentResourceToTest(_testPackageResource, testEntity)
            Next

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace