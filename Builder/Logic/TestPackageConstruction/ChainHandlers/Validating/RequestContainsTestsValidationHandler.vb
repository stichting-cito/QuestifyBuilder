Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestPackageConstruction.Requests
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating

Namespace TestPackageConstruction.ChainHandlers.Validating

    Public Class RequestContainsTestsValidationHandler
        Inherits ChainHandlerBase(Of TestPackageConstructionRequest)

        Public Overrides Function ProcessRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            If requestData.Tests.Count = 0 Then
                Throw New NoItemsInRequestException(My.Resources.RequestContainsTestValidationHandler_DoesNotContainTest, New List(Of Datasources.ResourceRef))
            End If

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace