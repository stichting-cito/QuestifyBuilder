Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.HelperClasses

Namespace TestConstruction.ChainHandlers.Validating

    Public Class ItemContainsSupportedTestViewsValidationHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)

        Private ReadOnly _test As AssessmentTest2
        Private ReadOnly _resourceManager As DataBaseResourceManager

        Sub New(ByVal resourceManager As DataBaseResourceManager, ByVal test As AssessmentTest2)
            _resourceManager = resourceManager
            _test = test
        End Sub
        Public Overrides Function ProcessRequest(requestData As TestConstructionRequest) As ChainHandlerResult

            If (requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Remove) Then
                Return ChainHandlerResult.RequestHandled
            End If

            Dim validator As New ItemSupportedViewsValidator(AddressOf ResourceNeeded)
            Dim invalidItems As New List(Of ResourceRef)

            Dim itemCode As List(Of String) = requestData.Items.Select(Function(i) i.Identifier).ToList()
            Dim request = New ItemResourceRequestDTO() With {.WithDependencies = True}
            Dim items = ResourceFactory.Instance.GetItemsByCodes(itemCode, _resourceManager.BankId, request)
            Dim supportedViews = GeneralHelper.GetViewsWithoutGeneral(_test.IncludedViews)

            For Each item As ItemResourceEntity In items
                If Not validator.ContainsItemSupportedViews(item, supportedViews) Then
                    invalidItems.Add(New ResourceRef(item.Name))
                End If
            Next
            If invalidItems.Count > 0 Then
                Throw New InvalidRequestException(String.Format(My.Resources.TestEditor_InvalidAdd, invalidItems.Count), invalidItems)
            End If
            Return ChainHandlerResult.RequestHandled

        End Function

        Private Sub ResourceNeeded(ByVal sender As System.Object, ByVal e As Cito.Tester.ContentModel.ResourceNeededEventArgs)
            Dim _resource As BinaryResource = Nothing
            Dim request = New ResourceRequestDTO()
            If e.TypedResourceType IsNot Nothing Then
                _resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
            Else
                _resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
            End If
            e.BinaryResource = _resource
        End Sub
    End Class
End Namespace