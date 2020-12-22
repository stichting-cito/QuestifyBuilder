Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Processing
    Class AddDependenciesToAssessmentTest
        Inherits ChainHandlerBase(Of TestConstructionRequest)


        Private ReadOnly _bankId As Integer
        Private ReadOnly _testResource As AssessmentTestResourceEntity




        Public Sub New(ByVal testResource As AssessmentTestResourceEntity)
            If testResource Is Nothing Then
                Throw New ArgumentNullException("testResource")
            End If
            Me._testResource = testResource
            Me._bankId = testResource.BankId
        End Sub





        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Add AndAlso requestData.Items.Count > 0 _
                Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function

        Private Sub AddDependentResourceToTest(ByVal testResource As AssessmentTestResourceEntity,
                                               ByVal resource As ResourceEntity)
            If testResource Is Nothing Then
                Throw New ArgumentNullException("testResource")
            End If

            If resource Is Nothing Then
                Throw New ArgumentNullException("resource")
            End If
            DependencyManagement.AddDependentResourceToResource(testResource, resource)

        End Sub


        Private Function ExecuteRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim itemResources As IDictionary(Of String, ItemResourceEntity) = ProcessingHelpers.GetItemResources(requestData,
                                                                                                                 _bankId)

            For Each ref As ResourceRef In requestData.Items
                If itemResources.ContainsKey(ref.Identifier) Then
                    Dim itemEntity As ItemResourceEntity = itemResources(ref.Identifier)

                    AddDependentResourceToTest(_testResource, itemEntity)
                Else
                End If
            Next

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace