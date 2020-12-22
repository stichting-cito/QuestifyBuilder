Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Processing
    Public Class NOOP
        Inherits ChainHandlerBase(Of TestConstructionRequest)

        Private ReadOnly _chainResultToReturn As ChainHandlerResult

        Public Sub New(ByVal chainResultToReturn As ChainHandlerResult)
            _chainResultToReturn = chainResultToReturn
        End Sub

        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Return _chainResultToReturn
        End Function

    End Class
End Namespace