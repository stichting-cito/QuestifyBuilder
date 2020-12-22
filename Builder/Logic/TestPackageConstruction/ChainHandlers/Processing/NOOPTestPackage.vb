Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestPackageConstruction.Requests

Namespace TestPackageConstruction.ChainHandlers.Processing
    Public Class NOOPTestPackage
        Inherits ChainHandlerBase(Of TestPackageConstructionRequest)

        Private ReadOnly _chainResultToReturn As ChainHandlerResult

        Public Sub New(ByVal chainResultToReturn As ChainHandlerResult)
            _chainResultToReturn = chainResultToReturn
        End Sub


        Public Overrides Function ProcessRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Return _chainResultToReturn
        End Function

    End Class
End Namespace
