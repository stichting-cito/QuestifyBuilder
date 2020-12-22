Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Validating
    Public Class RequestContainsItemsValidationHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)



        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            If requestData.Items.Count = 0 Then
                Throw New NoItemsInRequestException(My.Resources.RequestContainsItemsValidationHandler_DoesNotContainItems, New List(Of Datasources.ResourceRef))
            End If

            Return ChainHandlerResult.RequestHandled
        End Function


    End Class
End Namespace