Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

Namespace Questify.Builder.Model.ContentModel.Interfaces
    Public Interface IPropertyEntity

        Property Id As Guid
        Property Name As String
        Property Title As String
        Property Description As String
        Property BankId As Integer
        Property CreationDate As DateTime
        Property CreatedBy As Integer
        Property ModifiedDate As DateTime
        Property ModifiedBy As Integer
        Property Version As String
        Property OriginalVersion As String
        Property OriginalName As String
        Property Bank As BankEntity
        Property Fields As IEntityFields2
        Property IsDirty As Boolean
        Property IsNew As Boolean
        Property ResourceData As ResourceDataEntity
        Property StateId As Integer?
        Property State As StateEntity

        ReadOnly Property ResourceType As String
        ReadOnly Property CreatedByFullName As String
        ReadOnly Property RemovedDependentEntities As HelperClasses.EntityCollection
        ReadOnly Property ModifiedByFullName As String
        ReadOnly Property CopiedFromString As String
        ReadOnly Property DependentResourceCollection As HelperClasses.EntityCollection(Of DependentResourceEntity)
        ReadOnly Property ReferencedResourceCollection As HelperClasses.EntityCollection(Of DependentResourceEntity)
        ReadOnly Property StateName As String

        Function GetDependentResources() As EntityCollection(Of ResourceEntity)
        Function HasChangesInTopology() As Boolean
        Function OnlyChangesInWorkflowMetaData() As Boolean
        Function ContainsDependentResource(ByVal resource As ResourceEntity) As Boolean
        Function ContainsDependentResource(ByVal resourceId As Guid) As Boolean

        Function GetDependentResourceByName(ByVal name As String) As DependentResourceEntity
        Function CustomBankPropertyValueCollection() As EntityCollection(Of CustomBankPropertyValueEntity)

        Sub ValidateEntity()

    End Interface
End Namespace