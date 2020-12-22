Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Logic.CustomProperties
Imports Questify.Builder.Logic.Service.Interfaces

Public Class TreeStructureHelper

    Public Shared Function TreeStructureValueIsConnectedToResourcesAndUserCancelled(ByVal selectedEntity As TreeStructurePartCustomBankPropertyEntity, ByVal bankId As Integer, ByVal bankFactory As IBankService, ByVal resourceFactory As IResourceService) As Boolean
        Dim userInput = CheckTreeStructureValueConnectedToResourcesAndRequestUserInput(selectedEntity, bankId, bankFactory, resourceFactory)
        Return (userInput <> DialogResult.Yes)
    End Function

    Public Shared Function CheckTreeStructureValueConnectedToResourcesAndRequestUserInput(ByVal selectedEntity As TreeStructurePartCustomBankPropertyEntity, ByVal bankId As Integer, ByVal bankFactory As IBankService, ByVal resourceFactory As IResourceService) As DialogResult
        Dim referencedEntities As New List(Of IPropertyEntity)
        If IsTreeStructureValueConnectedToResources(selectedEntity, bankId, bankFactory, resourceFactory, referencedEntities) Then
            Return RequestUserInput(referencedEntities)
        End If
        Return DialogResult.Yes
    End Function

    Public Shared Function IsTreeStructureValueConnectedToResources(ByVal selectedEntity As TreeStructurePartCustomBankPropertyEntity, ByVal bankId As Integer, ByVal bankFactory As IBankService, ByVal resourceFactory As IResourceService, ByRef referencedEntities As List(Of IPropertyEntity)) As Boolean
        IsTreeStructureValueConnectedToItem(selectedEntity, bankFactory, resourceFactory, referencedEntities, 25)
        IsTreeStructureValueConnectedToTest(selectedEntity, bankId, resourceFactory, referencedEntities, 5)
        Return referencedEntities.Any()
    End Function

    Public Shared Function IsTreeStructureValueConnectedToItem(ByVal selectedEntity As TreeStructurePartCustomBankPropertyEntity, ByVal bankFactory As IBankService, ByVal resourceFactory As IResourceService, ByRef referencedEntities As List(Of IPropertyEntity), Optional maxNrOfItemsAsResult As Integer = 0) As Boolean
        Dim valueReferences = bankFactory.GetTreeStructureCustomBankPropertyValueReferences(selectedEntity.TreeStructurePartCustomBankPropertyId)
        If valueReferences IsNot Nothing AndAlso valueReferences.Any() Then
            Dim resourceList As New List(Of Guid)
            If maxNrOfItemsAsResult > 0 Then
                resourceList = valueReferences.Select(Function(r) CType(r, TreeStructureCustomBankPropertySelectedPartEntity).ResourceId).Take(25).ToList()
            Else
                resourceList = valueReferences.Select(Function(r) CType(r, TreeStructureCustomBankPropertySelectedPartEntity).ResourceId).ToList()
            End If

            For Each resource As ResourceEntity In resourceFactory.GetResourcesByIdsWithOption(resourceList, new ResourceRequestDTO())
                referencedEntities.Add(resource)
            Next
        End If

        Return referencedEntities.Count > 0
    End Function

    Public Shared Function IsTreeStructureValueConnectedToTest(ByVal selectedEntity As TreeStructurePartCustomBankPropertyEntity, ByVal bankId As Integer, ByVal resourceFactory As IResourceService, ByRef referencedEntities As List(Of IPropertyEntity), Optional maxNrOfTestsAsResult As Integer = 0) As Boolean
        AssessmentTestCutOffScoringHelper.CheckUsageOfTreeStructurePartsInTests(bankId, selectedEntity, referencedEntities)

        Return referencedEntities.Count > 0
    End Function

    Private Shared Function RequestUserInput(referencedEntities As List(Of IPropertyEntity)) As DialogResult
        Dim messageText As String
        Dim messageTextTests As String = String.Empty

        If referencedEntities.OfType(Of AssessmentTestResourceEntity).Any Then
            messageTextTests = String.Format(My.Resources.DeleteTreeWhenConnectedToOneOrMoreTests, String.Join(", ", referencedEntities.OfType(Of AssessmentTestResourceEntity).Select(Function(i) i.Name)))
        End If

        messageText = String.Format(My.Resources.DeleteTreeWhenConnectedToOneOrMoreItems, String.Join(", ", referencedEntities.OfType(Of ItemResourceEntity).Select(Function(i) i.Name)), messageTextTests)
        Return MessageBox.Show(messageText, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
    End Function
End Class
