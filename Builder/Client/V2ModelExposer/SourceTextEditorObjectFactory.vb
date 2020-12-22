Imports System.ComponentModel.Composition
Imports Cito.Tester.Common
Imports Enums
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.UI

Namespace V2ModelExposer

    <Export(GetType(ISourceTextEditorObjectFactory))>
    Public Class SourceTextEditorObjectFactory : Implements ISourceTextEditorObjectFactory

        Private ReadOnly _resourceService As IResourceService
        Private ReadOnly _bankService As IBankService

        Sub New()
            _resourceService = ResourceFactory.Instance
            _bankService = BankFactory.Instance
        End Sub

        Public Function GetRequiredObjectsForSourceTextWithId(id As Guid) As Tuple(Of GenericResourceEntity, Integer, ResourceManagerBase) Implements ISourceTextEditorObjectFactory.GetRequiredObjectsForSourceTextWithId
            Dim sw = Stopwatch.StartNew()
            Dim stre = _resourceService.GetGenericResource(New GenericResourceEntity With {.ResourceId = id})
            stre.ResourceData = _resourceService.GetResourceData(stre)
            Dim resourceManager = New DataBaseResourceManager(stre.BankId)

            sw.Stop()
            Debug.WriteLine($"GetRequiredObjectsForSourceTextWithId took [{sw.ElapsedMilliseconds}] ms")

            Return New Tuple(Of GenericResourceEntity, Integer, ResourceManagerBase)(stre, stre.BankId, resourceManager)
        End Function

        Public Function GetRequiredObjectsForNewSourceText(bankId As Integer, makeSourceTextTemplate As Boolean) As Tuple(Of GenericResourceEntity, Integer, ResourceManagerBase) Implements ISourceTextEditorObjectFactory.GetRequiredObjectsForNewSourceText
            Dim sw = Stopwatch.StartNew()
            Dim stre = New GenericResourceEntity(Guid.NewGuid) With {
                .BankId = bankId,
                .IsTemplate = makeSourceTextTemplate,
                .Version = "0.1",
                .MediaType = "application/xhtml+xml"
            }

            stre.ResourceData = New ResourceDataEntity(stre.ResourceId)

            Dim resourceManager = New DataBaseResourceManager(stre.BankId)

            sw.Stop()
            Debug.WriteLine($"GetRequiredObjectsForNewSourceText took [{sw.ElapsedMilliseconds}] ms")

            Return New Tuple(Of GenericResourceEntity, Integer, ResourceManagerBase)(stre, bankId, resourceManager)
        End Function

        Public Function UpdateSourceTextResource(resource As GenericResourceEntity) As String Implements ISourceTextEditorObjectFactory.UpdateSourceTextResource

            If resource.IsTemplate AndAlso resource.Name IsNot Nothing Then
                If Not resource.Name.EndsWith(".xhtml", StringComparison.InvariantCultureIgnoreCase) Then
                    resource.Name = $"{resource.Name}.xhtml"
                End If
            End If

            If resource.CustomBankPropertyValueCollection IsNot Nothing AndAlso
resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker IsNot Nothing AndAlso
resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Count > 0 Then

                BankFactory.Instance.DeleteCustomPropertyValues(DirectCast(resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker, EntityCollection))

                resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Clear()
                resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = Nothing
            End If

            Return ResourceFactory.Instance.UpdateGenericResource(resource)
        End Function


        Public Function GetCustomBankPropertiesForBranch(ByVal bankId As Integer) As EntityCollection Implements ISourceTextEditorObjectFactory.GetCustomBankPropertiesForBranch
            Return _bankService.GetCustomBankPropertiesForBranchById(bankId, ResourceTypeEnum.GenericResource)
        End Function

        Public Function GetAvailableStates() As EntityCollection Implements ISourceTextEditorObjectFactory.GetAvailableStates
            Return ResourceFactory.Instance.GetAvailableStates()
        End Function

        Public Function SelectStyleSheetToLink(ByVal bankId As Integer, ByVal contextIdentifier As Integer?, ByVal resourceManager As ResourceManagerBase) As GenericResourceDto Implements ISourceTextEditorObjectFactory.SelectStyleSheetToLink
            Dim selectMedia As New SelectMediaResourceDialog(bankId, contextIdentifier, resourceManager)

            selectMedia.Filter = "text/css"
            selectMedia.CanPickFiles = False
            selectMedia.ShowAddNew = False

            If selectMedia.ShowDialog() = DialogResult.OK Then
                Return selectMedia.SelectedEntity
            End If

            Return Nothing
        End Function
    End Class
End Namespace
