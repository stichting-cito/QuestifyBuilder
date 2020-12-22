
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.DTO
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ItemProcessing
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common
Imports Enums
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI


Namespace V2ModelExposer

    <Export(GetType(IItemEditorObjectFactory))>
    Public Class ItemEditorObjectFactory : Implements IItemEditorObjectFactory

        Private ReadOnly _resourceService As IResourceService
        Private ReadOnly _bankService As IBankService
        Private ReadOnly _cache As New Dictionary(Of Integer, InMemoryParameterSetCacheByBank)
        Private ReadOnly _cachedItlBank As New Dictionary(Of Integer, Integer)

        Sub New()
            _resourceService = ResourceFactory.Instance
            _bankService = BankFactory.Instance
        End Sub

        Public Function GetRequiredObjectsForItemWithId(id As Guid) As ItemEditorObjectFactoryResult Implements IItemEditorObjectFactory.GetRequiredObjectsForItemWithId
            Dim sw = Stopwatch.StartNew()
            Dim itemResourceEntity = _resourceService.GetItem(New ItemResourceEntity With {.ResourceId = id}, New ResourceRequestDTO() With {.WithDependencies = True, .WithCustomProperties = True, .WithUserInfo = True})
            Dim currentAction = _resourceService.GetStateAction(CType(itemResourceEntity.Fields("StateId").DbValue, Integer), "resourceediting")

            Using resourceManager = New DataBaseResourceManager(itemResourceEntity.BankId)
                If Not _cachedItlBank.ContainsKey(itemResourceEntity.BankId) Then
                    Dim ilte = DirectCast(ResourceFactory.Instance.GetResourceByNameWithOption(itemResourceEntity.BankId, itemResourceEntity.ItemLayoutTemplateUsedName, New ResourceRequestDTO()), ItemLayoutTemplateResourceEntity)
                    _cachedItlBank.Add(itemResourceEntity.BankId, ilte.BankId)
                End If
                Dim iltBankId = _cachedItlBank(itemResourceEntity.BankId)

                If (Not _cache.ContainsKey(iltBankId)) Then
                    _cache.Add(iltBankId, New InMemoryParameterSetCacheByBank(iltBankId))
                End If
                Dim itemSetupHelper = New AssessmentItemHelper(resourceManager, itemResourceEntity.ItemLayoutTemplateUsedName, itemResourceEntity, _cache(iltBankId))
                Dim assessmentItem = itemSetupHelper.GetExistingAssessmentItem()
                Dim warnErr As New WarningsAndErrors

                itemSetupHelper.MergeParameters(assessmentItem, warnErr)
                If ((warnErr.ErrorList.Count > 0) OrElse (warnErr.WarningList.Count > 0)) Then
                    If (warnErr.ErrorList.Count > 0) Then Throw New Exception(String.Join(Environment.NewLine, warnErr.ErrorList))
                    If (warnErr.WarningList.Count > 0) Then Throw New Exception(String.Join(Environment.NewLine, warnErr.WarningList))
                End If

                itemSetupHelper.ReFillParameterSet(assessmentItem)

                Dim prmSet = assessmentItem.Parameters
                sw.Stop()
                Debug.WriteLine($"GetRequiredObjectsForItemWithId took [{sw.ElapsedMilliseconds}] ms")

                Return ItemEditorObjectFactoryResult.Create(itemResourceEntity,
                                                            assessmentItem,
                                                            prmSet,
                                                            resourceManager,
                                                            currentAction,
                                                            itemSetupHelper.IsTransFormedTemplate)
            End Using
        End Function

        Public Function GetItemLayout(itemLayoutId As Guid, bankId As Integer) As ItemEditorObjectFactoryResult Implements IItemEditorObjectFactory.GetObjectsForNewItem
            Dim sw = Stopwatch.StartNew()

            If (Not _cache.ContainsKey(bankId)) Then
                _cache.Add(bankId, New InMemoryParameterSetCacheByBank(bankId))
            End If

            Dim ilt = ResourceFactory.Instance.GetItemLayoutTemplate(New ItemLayoutTemplateResourceEntity(itemLayoutId))

            Dim newItemResource As New ItemResourceEntity(Guid.NewGuid()) With {.ResourceData = New ResourceDataEntity()}
            newItemResource.BankId = bankId

            Using resourceManager = New DataBaseResourceManager(bankId)

                Dim itemSetupHelper = New AssessmentItemHelper(resourceManager, ilt.Name, newItemResource, _cache(bankId))
                Dim err As New WarningsAndErrors()
                Dim assessmentItem = itemSetupHelper.CreateNewAssessmentItem(newItemResource, ilt, err)

                Dim prmSet = assessmentItem.Parameters

                sw.Stop()
                Debug.WriteLine($"GetRequiredObjectsForItemWithId took [{sw.ElapsedMilliseconds}] ms")

                Return ItemEditorObjectFactoryResult.Create(newItemResource,
                                                assessmentItem,
                                                prmSet,
                                                resourceManager,
                                                Nothing,
                                                itemSetupHelper.IsTransFormedTemplate)
            End Using
        End Function


        Public Function RenameItem(itemResourceEntity As ItemResourceEntity, assessmentItem As AssessmentItem, ByRef referencedResources As EntityCollection) As Boolean Implements IItemEditorObjectFactory.RenameItem
            referencedResources = ResourceFactory.Instance.GetReferencesForResource(itemResourceEntity)
            Dim dlg As New ChangeItemCodeDialog(assessmentItem.Identifier, referencedResources.Count)
            AddHandler dlg.ValidateNewCodeName, Sub(sender As Object, e As ValidateNewCodeNameEventArgs)
                                                    Dim msg As String = ValidationHelper.IsValidResourceCode(e.NewCodeName)
                                                    If String.IsNullOrEmpty(msg) AndAlso ResourceFactory.Instance.ResourceExists(itemResourceEntity.BankId, e.NewCodeName, True) Then
                                                        msg = My.Resources.ItemEditor_ItemCodeNameAlreadyExistsInBankHierarchy
                                                    End If
                                                    e.Valid = String.IsNullOrEmpty(msg)
                                                    If (Not e.Valid) Then MessageBox.Show(msg,
                                                            Application.ProductName,
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Exclamation)
                                                End Sub

            Dim result As DialogResult = dlg.ShowDialog()
            If result = DialogResult.OK Then
                assessmentItem.Identifier = dlg.NewCodeName
                Return True
            End If
            referencedResources = Nothing
            Return False
        End Function

        Public Function UpdateItemResource(resource As ItemResourceEntity) As String Implements IItemEditorObjectFactory.UpdateItemResource
            Return ResourceFactory.Instance.UpdateItemResource(resource)
        End Function


        Public Function GetCustomBankPropertiesForBranch(ByVal bankId As Integer) As EntityCollection Implements IItemEditorObjectFactory.GetCustomBankPropertiesForBranch
            Return _bankService.GetCustomBankPropertiesForBranchById(bankId, ResourceTypeEnum.ItemResource)
        End Function


        Public Function GetGenericResource(bankId As Integer, resourceName As String) As GenericResourceEntity Implements IItemEditorObjectFactory.GetGenericResource
            Return CType(ResourceFactory.Instance.GetResourceByNameWithOption(bankId, resourceName, New ResourceRequestDTO()), GenericResourceEntity)
        End Function

        Public Function PopulateConceptCustomBankPropertyHierarchy(ByVal id As Guid) As ConceptStructurePartCustomBankPropertyEntity Implements IItemEditorObjectFactory.PopulateConceptCustomBankPropertyHierarchy
            Return _bankService.PopulateConceptCustomBankPropertyHierarchy(id)
        End Function

    End Class
End Namespace
