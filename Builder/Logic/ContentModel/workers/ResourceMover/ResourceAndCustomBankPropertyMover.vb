Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.InvalidateCache.Helper

Namespace ContentModel


    Public Class ResourceAndCustomBankPropertyMover

        Private Delegate Sub ResourceMoveValidator(resourceToCheck As ResourceEntity,
                                                   sourceBank As BankEntity,
                                                   destinationBank As BankEntity,
                                                   viaResourceName As String,
                                                   result As ResourceAndCustomBankPropertyMoveResult,
                                                   resultDetail As ResourceAndCustomBankPropertyMoveResultDetail,
                                                   handledResourceIds As SortedList(Of Guid, ResourceEntity))

        Private Delegate Sub CustomBankPropertyMoveValidator(cbpToCheck As CustomBankPropertyEntity,
                                                             sourceBank As BankEntity,
                                                             destinationBank As BankEntity,
                                                             viaResourceName As String,
                                                             result As ResourceAndCustomBankPropertyMoveResult,
                                                             resultDetail As ResourceAndCustomBankPropertyMoveResultDetail)

        Private Enum TypeOfMove
            NoMove
            Promotion
            Degredation
            MoveOutsidedCurrentBankHierarchy
        End Enum

        Private ReadOnly _sourceBank As BankEntity
        Private ReadOnly _destinationBank As BankEntity
        Private ReadOnly _bankTree As BankHierarchy

        Public Sub New(sourceBankId As Integer, destinationBankId As Integer)
            _sourceBank = BankFactory.Instance.GetBank(sourceBankId)
            _destinationBank = BankFactory.Instance.GetBank(destinationBankId)
            _bankTree = New BankHierarchy(sourceBankId)
        End Sub



        Public Function ValidateResourcesMove(resourcesToMoveIds() As Guid) As ResourceAndCustomBankPropertyMoveResult
            Dim result As New ResourceAndCustomBankPropertyMoveResult(_sourceBank.Id, _destinationBank.Id)

            Dim actionToPerform As TypeOfMove

            If _bankTree.BankInFirstParameterIsDescendantOfBankInSecondParameter(_sourceBank.Id, _destinationBank.Id) Then
                ResourceMoveValidatorToUse = New ResourceMoveValidator(AddressOf ValidateResourcePromotion)
                CustomBankPropertyMoveValidatorToUse = New CustomBankPropertyMoveValidator(AddressOf ValidateCustomBankPropertyPromotion)
                actionToPerform = TypeOfMove.Promotion
            ElseIf _bankTree.BankInFirstParameterIsDescendantOfBankInSecondParameter(_destinationBank.Id, _sourceBank.Id) Then
                ResourceMoveValidatorToUse = New ResourceMoveValidator(AddressOf ValidateResourceDegradation)
                CustomBankPropertyMoveValidatorToUse = New CustomBankPropertyMoveValidator(AddressOf ValidateCustomBankPropertyDegradation)
                actionToPerform = TypeOfMove.Degredation
            ElseIf _destinationBank.Id = _sourceBank.Id Then
                actionToPerform = TypeOfMove.NoMove
            Else
                actionToPerform = TypeOfMove.MoveOutsidedCurrentBankHierarchy
            End If


            Dim handledResourceIds As New SortedList(Of Guid, ResourceEntity)
            If actionToPerform = TypeOfMove.Promotion OrElse actionToPerform = TypeOfMove.Degredation Then
                For Each resourceId As Guid In resourcesToMoveIds
                    ValidateResourceMove(_sourceBank, _destinationBank, resourceId, String.Empty, result, handledResourceIds)
                Next
            End If

            Return result
        End Function

        Public Function MoveResources(resourceToMoveIds() As Guid) As ResourceAndCustomBankPropertyMoveResult
            Dim moveValidationResult As ResourceAndCustomBankPropertyMoveResult = ValidateResourcesMove(resourceToMoveIds)

            If moveValidationResult.AllCanBeMoved Then
                Dim resourcesToUpdate As List(Of Guid) = moveValidationResult.Details.Where(Function(x) x.IsResourceEntity).Select(Function(x) x.ResourceId).Distinct().ToList()
                Dim customBankPropertiesToUpdate As List(Of Guid) = moveValidationResult.Details.Where(Function(x) Not x.IsResourceEntity).Select(Function(x) x.ResourceId).Distinct().ToList()

                Dim updatesWithErrors As List(Of KeyValuePair(Of Guid, String)) = ResourceFactory.Instance.UpdateBankIdOfResourceEntitiesAndCustomBankProperties(_destinationBank.Id, resourcesToUpdate, customBankPropertiesToUpdate)

                updatesWithErrors.ForEach(Sub(x) moveValidationResult.Details.FindAll(Function(y) y.ResourceId = x.Key).ForEach(Sub(z) z.ProblemDescription = x.Value))

                If updatesWithErrors.Count > 0 Then
                    moveValidationResult.AllCanBeMoved = False
                Else
                    moveValidationResult.Details.ForEach(Sub(x) If x.ProblemCode = 0 Then x.ProblemDescription = My.Resources.ResourceAndCustomBankPropertyMover_Moved)
                End If

            End If
            InvalidateCacheHelper.ClearCacheForBank(_sourceBank.Id)
            Return moveValidationResult
        End Function


        Private Property ResourceMoveValidatorToUse As ResourceMoveValidator

        Private Property CustomBankPropertyMoveValidatorToUse As CustomBankPropertyMoveValidator


        Private Sub ValidateResourceMove(sourceBank As BankEntity,
                                         destinationBank As BankEntity,
                                         resourceId As Guid,
                                         viaResourceName As String,
                                         result As ResourceAndCustomBankPropertyMoveResult,
                                         handledResourceIds As SortedList(Of Guid, ResourceEntity))

            Dim resultDetail As ResourceAndCustomBankPropertyMoveResultDetail = FillResultDetailBasicInfo(resourceId, sourceBank, destinationBank, viaResourceName)

            Dim permissionsProblemDescription As String = String.Empty

            If Not PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.DALUpdate, TestBuilderPermissionTarget.Any, sourceBank.Id) Then
                permissionsProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_InsufficientPermissionsInSourceBank, sourceBank.Name)
            ElseIf Not PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.DALUpdate, TestBuilderPermissionTarget.Any, destinationBank.Id) Then
                permissionsProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_InsufficientPermissionsInDestinationBank, destinationBank.Name)
            End If

            Dim resourceToCheck = GetResource(resourceId, handledResourceIds)

            If resourceToCheck IsNot Nothing Then

                FillResultDetailResourceInfo(resourceToCheck, resultDetail)
                If String.IsNullOrEmpty(permissionsProblemDescription) Then
                    ResourceMoveValidatorToUse.Invoke(resourceToCheck, sourceBank, destinationBank, viaResourceName, result, resultDetail, handledResourceIds)
                End If

            Else
                Dim customBankPropertyToCheck As CustomBankPropertyEntity = BankFactory.Instance.GetCustomBankProperty(resourceId)

                If customBankPropertyToCheck IsNot Nothing Then
                    FillResultDetailCustomBankPropertyInfo(customBankPropertyToCheck, resultDetail)

                    If String.IsNullOrEmpty(permissionsProblemDescription) Then
                        CustomBankPropertyMoveValidatorToUse.Invoke(customBankPropertyToCheck, sourceBank, destinationBank, viaResourceName, result, resultDetail)
                    End If
                Else
                    resultDetail.ProblemCode = 1
                    resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_ResourceNotFound, resourceId)
                End If
            End If

            If Not String.IsNullOrEmpty(permissionsProblemDescription) Then
                resultDetail.ProblemCode = 1
                resultDetail.ProblemDescription = permissionsProblemDescription
            End If

            If result.AllCanBeMoved Then result.AllCanBeMoved = (resultDetail.ProblemCode = 0)

            result.Details.Add(resultDetail)
        End Sub

        Private Shared Function GetResource(resourceId As Guid, handledResourceIds As SortedList(Of Guid, ResourceEntity)) As ResourceEntity
            Dim resource As ResourceEntity = Nothing

            If handledResourceIds.TryGetValue(resourceId, resource) Then
                Return resource
            End If
            Dim request = New ResourceRequestDTO With {
                    .WithDependencies = True,
                    .WithReferences = True,
                    .WithCustomProperties = True
                    }
            resource = ResourceFactory.Instance.GetResourceByIdWithOption(resourceId, request)
            handledResourceIds.Add(resourceId, resource)

            Return resource
        End Function

        Private Sub ValidateResourcePromotion(resourceToCheck As ResourceEntity,
                                              sourceBank As BankEntity,
                                              destinationBank As BankEntity,
                                              viaResourceName As String,
                                              result As ResourceAndCustomBankPropertyMoveResult,
                                              resultDetail As ResourceAndCustomBankPropertyMoveResultDetail,
                                              handledResourceIds As SortedList(Of Guid, ResourceEntity))

            Dim problemCausingResource As ResourceEntity = ResourceExistsInDestinationBankHierarchy(resourceToCheck.Name, destinationBank.Id, sourceBank.Id)

            If problemCausingResource IsNot Nothing Then
                resultDetail.ProblemCode = 1
                Dim bankOfProblemCausingResource As BankEntity = BankFactory.Instance.GetBank(problemCausingResource.BankId)
                If bankOfProblemCausingResource IsNot Nothing Then
                    resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_ResourceAlreadyExistsInBank, bankOfProblemCausingResource.Name, resourceToCheck.Name)
                Else
                    resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_HierarchyAlreadyContainsResource, destinationBank.Name, resourceToCheck.Name)
                End If
            End If

            Dim resourceIds = resourceToCheck.DependentResourceCollection.Select(Function(r) r.DependentResourceId).ToList()
            GetResources(resourceIds, handledResourceIds)

            For Each rd As DependentResourceEntity In resourceToCheck.DependentResourceCollection
                Dim dependentResource = GetResource(rd.DependentResourceId, handledResourceIds)

                If dependentResource IsNot Nothing Then
                    If _bankTree.BankInFirstParameterIsDescendantOfBankInSecondParameter(dependentResource.Bank.Id, destinationBank.Id) Then
                        ValidateResourceMove(dependentResource.Bank, destinationBank, dependentResource.ResourceId, resourceToCheck.Name, result, handledResourceIds)
                    End If
                Else
                    resultDetail.ProblemCode = 1
                    resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_ResourceNotFound, rd.DependentResourceId)
                End If
            Next

            CheckResourceCustomBankPropertiesPromotion(resourceToCheck, resourceToCheck.CustomBankPropertyValueCollection, sourceBank, destinationBank, result, handledResourceIds)
        End Sub

        Private Shared Sub GetResources(resourceIds As List(Of Guid), handledResourceIds As SortedList(Of Guid, ResourceEntity))
            Dim resourceIdsToGet = (From resourceId In resourceIds Where Not handledResourceIds.ContainsKey(resourceId)).ToList()
            If Not resourceIdsToGet.Any() Then
                Return
            End If

            Dim request = New ResourceRequestDTO With {
                    .WithDependencies = True,
                    .WithReferences = True,
                    .WithCustomProperties = True
                    }
            Dim resources = ResourceFactory.Instance.GetResourcesByIdsWithOption(resourceIdsToGet, request)

            For Each resource As ResourceEntity In resources
                handledResourceIds.Add(resource.ResourceId, resource)
            Next
        End Sub

        Private Sub CheckResourceCustomBankPropertiesPromotion(resourceToCheck As ResourceEntity,
                                                       cbpCollection As EntityCollection(Of CustomBankPropertyValueEntity),
                                                       sourceBank As BankEntity,
                                                       destinationBank As BankEntity,
                                                       result As ResourceAndCustomBankPropertyMoveResult,
                                                       handledResourceIds As SortedList(Of Guid, ResourceEntity))

            For Each cbpValue As CustomBankPropertyValueEntity In cbpCollection

                If _bankTree.BankInFirstParameterIsDescendantOfBankInSecondParameter(cbpValue.CustomBankProperty.BankId, destinationBank.Id) Then
                    Dim cbpBank As BankEntity = sourceBank
                    If sourceBank.Id <> cbpValue.CustomBankProperty.BankId Then
                        cbpBank = BankFactory.Instance.GetBank(cbpValue.CustomBankProperty.BankId)
                    End If

                    ValidateResourceMove(cbpBank, destinationBank, cbpValue.CustomBankPropertyId, resourceToCheck.Name, result, handledResourceIds)
                End If
            Next

        End Sub

        Private Sub ValidateCustomBankPropertyPromotion(cbpToCheck As CustomBankPropertyEntity,
                                                        sourceBank As BankEntity,
                                                        destinationBank As BankEntity,
                                                        viaResourceName As String,
                                                        result As ResourceAndCustomBankPropertyMoveResult,
                                                        resultDetail As ResourceAndCustomBankPropertyMoveResultDetail)

            Dim problemCausingCustomBankProperty As CustomBankPropertyEntity = CustomBankPropertyExistsInDestinationBankHierarchy(cbpToCheck.Name, destinationBank, sourceBank.Id)

            If problemCausingCustomBankProperty IsNot Nothing Then
                resultDetail.ProblemCode = 1
                Dim bankOfProblemCausingProperty As BankEntity = BankFactory.Instance.GetBank(problemCausingCustomBankProperty.BankId)

                If bankOfProblemCausingProperty IsNot Nothing Then
                    resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_PropertyAlreadyExistsInBank, bankOfProblemCausingProperty.Name, cbpToCheck.Name)
                Else
                    resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_HierarchyAlreadyContainsCustomBankProperty, destinationBank.Name, cbpToCheck.Name)
                End If
            End If

        End Sub

        Private Sub ValidateResourceDegradation(resourceToCheck As ResourceEntity,
                                                sourceBank As BankEntity,
                                                destinationBank As BankEntity,
                                                viaResourceName As String,
                                                result As ResourceAndCustomBankPropertyMoveResult,
                                                resultDetail As ResourceAndCustomBankPropertyMoveResultDetail,
                                                handledResourceIds As SortedList(Of Guid, ResourceEntity))

            If resourceToCheck.BankId <> _sourceBank.Id Then
                If resourceToCheck.BankId <> destinationBank.Id AndAlso Not _bankTree.BankInFirstParameterIsDescendantOfBankInSecondParameter(resourceToCheck.BankId, destinationBank.Id) Then
                    resultDetail.ProblemCode = 1

                    If Not String.IsNullOrEmpty(viaResourceName) Then
                        resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_ResourceReferencedFromOtherSubBank, resourceToCheck.BankName)
                    Else
                        Debug.Assert(False, "viaResourceName not expected to be empty at this point!")
                    End If
                End If
            End If

            Dim resourceIds = resourceToCheck.ReferencedResourceCollection.Select(Function(r) r.ResourceId).ToList()
            GetResources(resourceIds, handledResourceIds)

            For Each rr As DependentResourceEntity In resourceToCheck.ReferencedResourceCollection
                Dim referencingResource As ResourceEntity = GetResource(rr.ResourceId, handledResourceIds)

                If referencingResource IsNot Nothing Then
                    If referencingResource.Bank.Id <> destinationBank.Id AndAlso Not _bankTree.BankInFirstParameterIsDescendantOfBankInSecondParameter(referencingResource.Bank.Id, destinationBank.Id) Then
                        ValidateResourceMove(referencingResource.Bank, destinationBank, referencingResource.ResourceId, resourceToCheck.Name, result, handledResourceIds)
                    End If
                Else
                    resultDetail.ProblemCode = 1
                    resultDetail.ProblemDescription = String.Format(My.Resources.ResourceAndCustomBankPropertyMover_ResourceNotFound, rr.DependentResourceId)
                End If
            Next
        End Sub

        Private Shared Sub ValidateCustomBankPropertyDegradation(cbpToCheck As CustomBankPropertyEntity,
                                                          sourceBank As BankEntity,
                                                          destinationBank As BankEntity,
                                                          viaResourceName As String,
                                                          result As ResourceAndCustomBankPropertyMoveResult,
                                                          resultDetail As ResourceAndCustomBankPropertyMoveResultDetail)


            resultDetail.ProblemCode = 1
            resultDetail.ProblemDescription = "Not supported yet."
        End Sub

        Private Shared Function FillResultDetailBasicInfo(resourceId As Guid, sourceBank As BankEntity, destinationBank As BankEntity, viaResourceName As String) As ResourceAndCustomBankPropertyMoveResultDetail
            Dim resultDetail As New ResourceAndCustomBankPropertyMoveResultDetail

            resultDetail.ResourceId = resourceId
            resultDetail.SourceBankName = sourceBank.Name
            resultDetail.DestinationBankName = destinationBank.Name
            resultDetail.ViaResourceName = viaResourceName
            resultDetail.ProblemCode = 0

            Return resultDetail
        End Function

        Private Shared Sub FillResultDetailResourceInfo(resourceToCheck As ResourceEntity, resultDetail As ResourceAndCustomBankPropertyMoveResultDetail)
            resultDetail.IsResourceEntity = True
            resultDetail.ResourceName = resourceToCheck.Name
            resultDetail.ResourceTitle = resourceToCheck.Title
            resultDetail.ResourceType = resourceToCheck.ToString()
        End Sub

        Private Shared Sub FillResultDetailCustomBankPropertyInfo(cbpToCheck As CustomBankPropertyEntity, resultDetail As ResourceAndCustomBankPropertyMoveResultDetail)
            resultDetail.IsResourceEntity = False
            resultDetail.ResourceName = cbpToCheck.Name
            resultDetail.ResourceTitle = cbpToCheck.Title
            resultDetail.ResourceType = cbpToCheck.ToString()
        End Sub

        Private Shared Function ResourceExistsInDestinationBankHierarchy(resourceName As String, destinationBankId As Integer, sourceBankId As Integer) As ResourceEntity
            Dim resourceEntity As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(destinationBankId, resourceName, new ResourceRequestDTO())
            If resourceEntity IsNot Nothing AndAlso resourceEntity.BankId <> sourceBankId AndAlso resourceEntity.BankId <> destinationBankId Then
                Return resourceEntity
            End If

            Return Nothing
        End Function

        Private Shared Function CustomBankPropertyExistsInDestinationBankHierarchy(cbpName As String, destinationBank As BankEntity, sourceBankId As Integer) As CustomBankPropertyEntity
            Dim customBankProperties As EntityCollection = BankFactory.Instance.CustomBankPropertyExistsInBankhierarchy(destinationBank, cbpName)
            Dim problemCausingCbp As CustomBankPropertyEntity = DirectCast(customBankProperties.FirstOrDefault(Function(x) DirectCast(x, CustomBankPropertyEntity).BankId <> sourceBankId AndAlso DirectCast(x, CustomBankPropertyEntity).BankId <> destinationBank.Id), CustomBankPropertyEntity)

            Return problemCausingCbp
        End Function
    End Class

End Namespace
