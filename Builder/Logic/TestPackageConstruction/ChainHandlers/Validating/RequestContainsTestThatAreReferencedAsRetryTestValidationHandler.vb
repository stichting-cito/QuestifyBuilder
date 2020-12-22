Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestPackageConstruction.Requests

Namespace TestPackageConstruction.ChainHandlers.Validating
    Public Class RequestContainsTestThatAreReferencedAsRetryTestValidationHandler
        Inherits ChainHandlerBase(Of TestPackageConstructionRequest)

        Public Property TestContainTestToRemoveAsRetry As List(Of TestReference)
        Public Property TestPackage As TestPackage

        Public Sub New(testPackage As TestPackage)
            _TestPackage = testPackage
        End Sub

        Public Overrides Function ProcessRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Return ChainHandlerResult.RequestHandled
        End Function

    End Class

End Namespace