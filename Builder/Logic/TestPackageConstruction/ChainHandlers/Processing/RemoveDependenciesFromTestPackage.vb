
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestPackageConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestPackageConstruction.ChainHandlers.Processing
    Public Class RemoveDependenciesFromTestPackage
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





        Public Overrides Function ProcessRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Remove AndAlso requestData.Tests.Count > 0 Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function


        Private Function ExecuteRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            TestPackageProcessingHelpers.GetTestResources(requestData, _bankId)

            For Each ref As Datasources.ResourceRef In requestData.Tests
                Dim resourceName As String = ref.Identifier

                RemoveDependentResourceFromTest(_testPackageResource, resourceName)
            Next

            Return ChainHandlerResult.RequestHandled
        End Function


        Private Sub RemoveDependentResourceFromTest(ByVal testPackageResourceEntity As TestPackageResourceEntity, ByVal resourceName As String)
            For Each depResource As DependentResourceEntity In testPackageResourceEntity.DependentResourceCollection
                If depResource.DependentResource.Name.Equals(resourceName) Then
                    testPackageResourceEntity.DependentResourceCollection.Remove(depResource)
                    Exit For
                End If
            Next
        End Sub

    End Class
End Namespace