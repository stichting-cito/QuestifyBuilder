Imports System.ComponentModel.Composition
Imports System.IO
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Security
Imports Questify.Builder.UI

<Export(GetType(IResourcePropertyDialogObjectFactory))>
Public Class ResourcePropertyDialogObjectFactory
    Implements IResourcePropertyDialogObjectFactory








    Public Function GetRequiredObjectsForPropertyEntity(id As Guid, type As Type) As IPropertyEntity Implements IResourcePropertyDialogObjectFactory.GetRequiredObjectsForPropertyEntityWithId
        Select Case type
            Case GetType(TestPackageResourceEntity)
                Return ResourceFactory.Instance.GetTestPackage(New TestPackageResourceEntity(id))
            Case GetType(AssessmentTestResourceEntity)
                Return ResourceFactory.Instance.GetAssessmentTest(New AssessmentTestResourceEntity(id))
            Case GetType(ItemResourceEntity)
                Return ResourceFactory.Instance.GetItem(New ItemResourceEntity(id), New ResourceRequestDTO() With {.WithDependencies = True, .WithReferences = True})
            Case GetType(GenericResourceEntity)
                Return ResourceFactory.Instance.GetGenericResource(New GenericResourceEntity(id))
            Case GetType(DataSourceResourceEntity)
                Return ResourceFactory.Instance.GetDataSource(New DataSourceResourceEntity(id))
            Case GetType(ControlTemplateResourceEntity)
                Return ResourceFactory.Instance.GetControlTemplate(New ControlTemplateResourceEntity(id))
            Case GetType(ItemLayoutTemplateResourceEntity)
                Return ResourceFactory.Instance.GetItemLayoutTemplate(New ItemLayoutTemplateResourceEntity(id))
            Case GetType(AspectResourceEntity)
                Return ResourceFactory.Instance.GetAspect(New AspectResourceEntity(id))
            Case GetType(FreeValueCustomBankPropertyEntity)
                Return BankFactory.Instance.GetFreeValueCustomBankProperty(New FreeValueCustomBankPropertyEntity(id))
            Case GetType(RichTextValueCustomBankPropertyEntity)
                Return BankFactory.Instance.GetRichTextValueCustomBankProperty(New RichTextValueCustomBankPropertyEntity(id))
            Case GetType(ListCustomBankPropertyEntity)
                Return BankFactory.Instance.GetListCustomBankProperty(New ListCustomBankPropertyEntity(id))
            Case GetType(ConceptStructureCustomBankPropertyEntity)
                Return BankFactory.Instance.GetConceptStructureCustomBankProperty(New ConceptStructureCustomBankPropertyEntity(id))
            Case GetType(TreeStructureCustomBankPropertyEntity)
                Return BankFactory.Instance.GetTreeStructureCustomBankProperty(New TreeStructureCustomBankPropertyEntity(id))
        End Select

        Throw New ArgumentException("Type not supported. Type: " & type.Name)
    End Function

    Public Function GetBinData(ByVal id As Guid) As Byte() Implements IResourcePropertyDialogObjectFactory.GetBinData
        Return ResourceFactory.Instance.GetResourceData(New ResourceEntity(id)).BinData
    End Function

    Public Function GetReferences(ByVal entity As IPropertyEntity) As EntityCollection Implements IResourcePropertyDialogObjectFactory.GetReferences
        Dim resourceEntity = TryCast(entity, ResourceEntity)
        If resourceEntity IsNot Nothing Then
            Return ResourceFactory.Instance.GetReferencesForResource(resourceEntity)
        Else
            Dim customBankPropertyEntity = TryCast(entity, CustomBankPropertyEntity)
            If customBankPropertyEntity IsNot Nothing Then
                Return BankFactory.Instance.GetReferencesForCustomBankProperty(customBankPropertyEntity)
            Else
                Throw New ArgumentException("This type is not supported: " & entity.GetType().ToString())
            End If
        End If
    End Function

    Public Function SaveResourcePropertyDialog(ByVal propertyEntity As IPropertyEntity, Optional ByVal pathToNewResource As String = Nothing, Optional ByVal identifierAndCodeFieldDiffer As Boolean = False) As String Implements IResourcePropertyDialogObjectFactory.SaveResourcePropertyDialog
        Dim resourceMetaData As New ResourceMetaData()

        If Not String.IsNullOrEmpty(pathToNewResource) AndAlso File.Exists(pathToNewResource) Then
            Dim mayChangeData As Boolean = resourceMetaData.CanUpdateResource(pathToNewResource.Length > 0)
            ReplaceEntity(propertyEntity, pathToNewResource, identifierAndCodeFieldDiffer, mayChangeData)
        End If

        Return UpdateEntity(propertyEntity)
    End Function

    Private Sub ReplaceEntity(ByRef propertyEntity As IPropertyEntity, ByVal pathToNewResource As String, ByVal identifierAndCodeFieldDiffer As Boolean, ByVal mayChangeData As Boolean)
        If mayChangeData AndAlso pathToNewResource.Length > 0 Then
            Dim newResourceData() As Byte = FileHelper.MakeByteArrayFromFile(pathToNewResource)

            If Not identifierAndCodeFieldDiffer Then
                If propertyEntity.ResourceData Is Nothing Then
                    propertyEntity.ResourceData = ResourceFactory.Instance.GetResourceData(DirectCast(propertyEntity, ResourceEntity))
                End If
                propertyEntity.ResourceData.BinData = Nothing
                propertyEntity.ResourceData.BinData = newResourceData
            End If
            Dim mediaResource = TryCast(propertyEntity, GenericResourceEntity)
            If (mediaResource IsNot Nothing) Then
                Dim fileMimeType As String = FileHelper.GetMimeFromFile(pathToNewResource)
                If fileMimeType = "application/x-zip-compressed" AndAlso QuestifyThemeValidator.TryValidate(pathToNewResource) Then
                    fileMimeType = LogicFileHelper.QuestifyThemeMimeType
                End If
                mediaResource.MediaType = fileMimeType
                mediaResource.Size = CInt(mediaResource.ResourceData.BinData.Length / 1024)

                If mediaResource.Size = 0 Then
                    Dim realSize As Double = mediaResource.ResourceData.BinData.Length / 1024
                    If realSize > 0 Then mediaResource.Size = 1
                End If

                Dim helper As New MediaDimensionsHelper()
                Dim size As Size = helper.GetDimensions(mediaResource.MediaType, mediaResource.ResourceData.BinData)
                If Not size.IsEmpty Then
                    mediaResource.Dimensions = $"{size.Width} x {size.Height}"
                End If
            End If
        End If
    End Sub

    Private Function CanUpdateAccordingToState(resourceEntity As IPropertyEntity) As Boolean
        If resourceEntity.IsNew Then
            Return True
        End If

        Dim currentStateAction = ResourceFactory.Instance.GetStateAction(resourceEntity.BankId, resourceEntity.Name, "resourceediting")
        Select Case currentStateAction?.Name.ToLower()
            Case "permit"
                Return True

            Case "warn"
                Return MessageBox.Show(My.Resources.AllEditors_SaveChangesForResourceWithStateWarning, "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes

            Case "prohibit"
                Return PermissionFactory.Instance.TryUserIsPermittedToNamedTask(
                    TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask,
                    TestBuilderPermissionNamedTask.ChangeWorkflowMetadataWhenProhibittedByState,
                    resourceEntity.BankId, 0)

            Case Else
                Return True
        End Select
    End Function

    Private Function UpdateEntity(ByVal propertyEntity As IPropertyEntity) As String
        Dim updateResult As String = String.Empty
        If Not CanUpdateAccordingToState(propertyEntity) Then
            Return My.Resources.AllEditors_CannotUpdateBecauseOfState
        End If

        Select Case propertyEntity.GetType().Name
            Case GetType(TestPackageResourceEntity).Name
                Dim entity As TestPackageResourceEntity = DirectCast(propertyEntity, TestPackageResourceEntity)
                updateResult = ResourceFactory.Instance.UpdateTestPackageResource(entity)
            Case GetType(AssessmentTestResourceEntity).Name
                Dim entity As AssessmentTestResourceEntity = DirectCast(propertyEntity, AssessmentTestResourceEntity)
                updateResult = ResourceFactory.Instance.UpdateAssessmentTestResource(entity)
            Case GetType(ItemResourceEntity).Name
                Dim entity As ItemResourceEntity = DirectCast(propertyEntity, ItemResourceEntity)
                UpdateItemResourcePropertiesFromAssessmentItem(entity)
                updateResult = ResourceFactory.Instance.UpdateItemResource(entity)
            Case GetType(GenericResourceEntity).Name
                Dim entity As GenericResourceEntity = DirectCast(propertyEntity, GenericResourceEntity)
                updateResult = ResourceFactory.Instance.UpdateGenericResource(entity)
            Case GetType(ItemLayoutTemplateResourceEntity).Name
                Dim entity As ItemLayoutTemplateResourceEntity = DirectCast(propertyEntity, ItemLayoutTemplateResourceEntity)
                updateResult = ResourceFactory.Instance.UpdateItemLayoutTemplateResource(entity)
            Case GetType(ControlTemplateResourceEntity).Name
                Dim entity As ControlTemplateResourceEntity = DirectCast(propertyEntity, ControlTemplateResourceEntity)
                updateResult = ResourceFactory.Instance.UpdateControlTemplateResource(entity)
            Case GetType(AspectResourceEntity).Name
                Dim entity As AspectResourceEntity = DirectCast(propertyEntity, AspectResourceEntity)
                updateResult = ResourceFactory.Instance.UpdateAspectResource(entity)
            Case GetType(DataSourceResourceEntity).Name
                Dim entity As DataSourceResourceEntity = DirectCast(propertyEntity, DataSourceResourceEntity)
                updateResult = ResourceFactory.Instance.UpdateDataSourceResource(entity)
            Case GetType(ConceptStructureCustomBankPropertyEntity).Name
                Dim entity As ConceptStructureCustomBankPropertyEntity = (DirectCast(propertyEntity, ConceptStructureCustomBankPropertyEntity))
                updateResult = BankFactory.Instance.UpdateCustomProperty(entity)
            Case GetType(TreeStructureCustomBankPropertyEntity).Name
                Dim entity As TreeStructureCustomBankPropertyEntity = (DirectCast(propertyEntity, TreeStructureCustomBankPropertyEntity))
                updateResult = BankFactory.Instance.UpdateCustomProperty(entity)
            Case GetType(ListCustomBankPropertyEntity).Name
                Dim entity As ListCustomBankPropertyEntity = (DirectCast(propertyEntity, ListCustomBankPropertyEntity))
                updateResult = BankFactory.Instance.UpdateCustomProperty(entity)
            Case GetType(FreeValueCustomBankPropertyEntity).Name
                Dim entity As FreeValueCustomBankPropertyEntity = (DirectCast(propertyEntity, FreeValueCustomBankPropertyEntity))
                updateResult = BankFactory.Instance.UpdateCustomProperty(entity)
            Case Else
                Throw New ArgumentException(String.Format(My.Resources.CouldNotSaveResourceUnsupportedType, propertyEntity.GetType().Name))
        End Select

        Return updateResult
    End Function

    Private Shared Sub UpdateItemResourcePropertiesFromAssessmentItem(ByRef itemResourceEntity As ItemResourceEntity)
        Dim assessmentItem As AssessmentItem = itemResourceEntity.GetAssessmentItem()
        assessmentItem.SetScorePropertiesForItem(itemResourceEntity)

        If Not itemResourceEntity.Title.Equals(assessmentItem.Title) Then
            assessmentItem.Title = itemResourceEntity.Title
            itemResourceEntity.SetAssessmentItem(assessmentItem)
        End If
    End Sub

    Public Function GetResourceHistory(resourceHistory As ResourceHistoryEntity) As ResourceHistoryEntity Implements IResourcePropertyDialogObjectFactory.GetResourceHistory
        Return ResourceFactory.Instance.GetResourceHistory(resourceHistory)
    End Function

    Public Function GetResourceHistoryByResource(resourceEntity As ResourceEntity) As EntityCollection Implements IResourcePropertyDialogObjectFactory.GetResourceHistoryByResource
        Return ResourceFactory.Instance.GetResourceHistoryForResource(resourceEntity.ResourceId)
    End Function

    Public Function UpdateResourceHistory(resourceHistoryEntity As ResourceHistoryEntity) As String Implements IResourcePropertyDialogObjectFactory.UpdateResourceHistory
        Return ResourceFactory.Instance.UpdateResourceHistory(resourceHistoryEntity)
    End Function

    Public Function GetResourceManager(bankId As Integer) As ResourceManagerBase Implements IResourcePropertyDialogObjectFactory.GetResourceManager
        Using resourceManager As DataBaseResourceManager = New DataBaseResourceManager(bankId)
            Return resourceManager
        End Using
    End Function
End Class
