Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel.Datasources
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestPackageConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating

Namespace TestPackageConstruction.ChainHandlers.Validating
    Public Class RequestContainsTestsAlreadyInContextValidationHandler
        Inherits ChainHandlerBase(Of TestPackageConstructionRequest)

        Private ReadOnly _comparer As IEqualityComparer(Of ResourceRef) = New ResourceRefIdentityEqualityComparer

        Public Overrides Function ProcessRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Dim isInvalidRequest As Boolean = False
            Dim message As String = String.Empty

            Dim intersectWithContext As IList(Of ResourceRef) = SetOperations.Intersect(requestData.Tests, requestData.TestContext, _comparer)

            Select Case requestData.RequestType
                Case TestPackageConstructionRequest.RequestTypeEnum.Add
                    If intersectWithContext.Count <> 0 Then
                        isInvalidRequest = True
                        message = String.Format(My.Resources.Message_TestPackageConstruction_AlreadyInTestContext, intersectWithContext.Count)
                    End If

                Case TestPackageConstructionRequest.RequestTypeEnum.Remove
                    Dim missingItems As Integer = intersectWithContext.Count - requestData.Tests.Count
                    If missingItems <> 0 Then
                        isInvalidRequest = True
                        message = $"{missingItems} resourcerefs in requestData.Tests do not exist in ItemContext"
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