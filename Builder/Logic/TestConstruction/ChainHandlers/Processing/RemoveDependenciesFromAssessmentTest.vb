Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Processing
    Class RemoveDependenciesFromAssessmentTest
        Inherits ChainHandlerBase(Of TestConstructionRequest)


        Private ReadOnly _testResource As AssessmentTestResourceEntity




        Public Sub New(ByVal testResource As AssessmentTestResourceEntity)
            If testResource Is Nothing Then
                Throw New ArgumentNullException("testResource")
            End If

            Me._testResource = testResource
        End Sub





        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Remove AndAlso requestData.Items.Count > 0 _
                Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function



        Private Function ExecuteRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            For Each ref As ResourceRef In requestData.Items
                Dim resourceName As String = ref.Identifier

                RemoveDependentResourceFromTest(_testResource, resourceName)
            Next

            Return ChainHandlerResult.RequestHandled
        End Function


        Private Sub RemoveDependentResourceFromTest(ByVal testResourceEntity As AssessmentTestResourceEntity,
                                            ByVal resourceName As String)
            For Each depResource As DependentResourceEntity In testResourceEntity.DependentResourceCollection
                If depResource.DependentResource.Name.Equals(resourceName) Then
                    testResourceEntity.DependentResourceCollection.Remove(depResource)
                    Exit For
                End If
            Next
        End Sub

    End Class
End Namespace