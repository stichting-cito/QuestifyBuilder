Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel.Datasources
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Validating
    Public Class RequestContainsItemsAlreadyInContextValidationHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)

        Private ReadOnly _comparer As IEqualityComparer(Of ResourceRef) = New ResourceRefIdentityEqualityComparer

        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim isInvalidRequest As Boolean = False
            Dim message As String = String.Empty

            Dim intersectWithContext As IList(Of ResourceRef) = SetOperations.Intersect(requestData.Items, requestData.ItemContext, _comparer)

            Select Case requestData.RequestType
                Case TestConstructionRequest.RequestTypeEnum.Add
                    If intersectWithContext.Count <> 0 Then
                        isInvalidRequest = True
                        message = String.Format(My.Resources.Message_TestConstruction_AlreadyInItemContext, intersectWithContext.Count)
                    End If

                Case TestConstructionRequest.RequestTypeEnum.Remove
                    Dim missingItems As Integer = intersectWithContext.Count - requestData.Items.Count
                    If missingItems <> 0 Then
                        isInvalidRequest = True
                        message = $"{missingItems} resourcerefs in requestData.Items do not exist in ItemContext"
                    End If

                Case Else
                    isInvalidRequest = True
                    message = "unknown requesttype"
            End Select

            If isInvalidRequest Then
                Throw New InvalidRequestException(message, intersectWithContext)
            End If

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace