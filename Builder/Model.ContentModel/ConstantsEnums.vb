Imports System

Namespace Questify.Builder.Model.ContentModel

    Public Enum ActionFieldIndex
        [ActionId]
        [Name]
        [Title]
        AmountOfFields
    End Enum


    Public Enum AspectResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        [RawScore]
        AmountOfFields
    End Enum


    Public Enum AssessmentTestResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        [IsTemplate]
        AmountOfFields
    End Enum


    Public Enum BankFieldIndex
        [Id]
        [ParentBankId]
        [Name]
        [Type]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        AmountOfFields
    End Enum


    Public Enum ChildConceptStructurePartCustomBankPropertyFieldIndex
        [Id]
        [ConceptStructurePartCustomBankPropertyId]
        [ChildConceptStructurePartCustomBankPropertyId]
        [VisualOrder]
        AmountOfFields
    End Enum


    Public Enum ChildTreeStructurePartCustomBankPropertyFieldIndex
        [Id]
        [ChildTreeStructurePartCustomBankPropertyId]
        [TreeStructurePartCustomBankPropertyId]
        [VisualOrder]
        AmountOfFields
    End Enum


    Public Enum ConceptStructureCustomBankPropertyFieldIndex
        [CustomBankPropertyId_CustomBankProperty]
        [BankId]
        [ApplicableToMask]
        [Publishable]
        [Scorable]
        [Name]
        [Title]
        [Description]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [Code]
        [StateId]
        [Version]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum ConceptStructureCustomBankPropertySelectedPartFieldIndex
        [ConceptStructurePartId]
        [ResourceId]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum ConceptStructureCustomBankPropertyValueFieldIndex
        [ResourceId_CustomBankPropertyValue]
        [CustomBankPropertyId_CustomBankPropertyValue]
        [DisplayValue]
        [ResourceId]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum ConceptStructurePartCustomBankPropertyFieldIndex
        [ConceptStructurePartCustomBankPropertyId]
        [CustomBankPropertyId]
        [Name]
        [Title]
        [Code]
        [ConceptTypeId]
        AmountOfFields
    End Enum


    Public Enum ConceptTypeFieldIndex
        [ConceptTypeId]
        [Name]
        [ApplicableToMask]
        AmountOfFields
    End Enum


    Public Enum ControlTemplateResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        AmountOfFields
    End Enum


    Public Enum CustomBankPropertyFieldIndex
        [CustomBankPropertyId]
        [BankId]
        [ApplicableToMask]
        [Publishable]
        [Scorable]
        [Name]
        [Title]
        [Description]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [Code]
        [StateId]
        [Version]
        AmountOfFields
    End Enum


    Public Enum CustomBankPropertyValueFieldIndex
        [ResourceId]
        [CustomBankPropertyId]
        [DisplayValue]
        AmountOfFields
    End Enum


    Public Enum DataSourceResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        [DataSourceType]
        [IsTemplate]
        AmountOfFields
    End Enum


    Public Enum DependentResourceFieldIndex
        [ResourceId]
        [DependentResourceId]
        AmountOfFields
    End Enum


    Public Enum FreeValueCustomBankPropertyFieldIndex
        [CustomBankPropertyId_CustomBankProperty]
        [BankId]
        [ApplicableToMask]
        [Publishable]
        [Scorable]
        [Name]
        [Title]
        [Description]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [Code]
        [StateId]
        [Version]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum FreeValueCustomBankPropertyValueFieldIndex
        [ResourceId_CustomBankPropertyValue]
        [CustomBankPropertyId_CustomBankPropertyValue]
        [DisplayValue]
        [ResourceId]
        [CustomBankPropertyId]
        [Value]
        [FreeValueCustomBankPropertyValueId]
        AmountOfFields
    End Enum


    Public Enum GenericResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        [MediaType]
        [Size]
        [Dimensions]
        [IsTemplate]
        AmountOfFields
    End Enum


    Public Enum HiddenResourceFieldIndex
        [ResourceId]
        [BankId]
        AmountOfFields
    End Enum


    Public Enum ItemLayoutTemplateResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        [ItemType]
        AmountOfFields
    End Enum


    Public Enum ItemResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        [IsSystemItem]
        [AlternativesCount]
        [KeyValues]
        [ResponseCount]
        [RawScore]
        [TesterSchemaVersion]
        [Iltname]
        [Iltversion]
        [MaxScore]
        [ItemAutoId]
        [ItemId]
        AmountOfFields
    End Enum


    Public Enum ListCustomBankPropertyFieldIndex
        [CustomBankPropertyId_CustomBankProperty]
        [BankId]
        [ApplicableToMask]
        [Publishable]
        [Scorable]
        [Name]
        [Title]
        [Description]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [Code]
        [StateId]
        [Version]
        [CustomBankPropertyId]
        [MultipleSelect]
        AmountOfFields
    End Enum


    Public Enum ListCustomBankPropertySelectedValueFieldIndex
        [ResourceId]
        [CustomBankPropertyId]
        [ListValueBankCustomPropertyId]
        AmountOfFields
    End Enum


    Public Enum ListCustomBankPropertyValueFieldIndex
        [ResourceId_CustomBankPropertyValue]
        [CustomBankPropertyId_CustomBankPropertyValue]
        [DisplayValue]
        [ResourceId]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum ListValueCustomBankPropertyFieldIndex
        [ListValueBankCustomPropertyId]
        [CustomBankPropertyId]
        [Name]
        [Title]
        [Code]
        AmountOfFields
    End Enum


    Public Enum PackageResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        AmountOfFields
    End Enum


    Public Enum PermissionFieldIndex
        [Id]
        [Name]
        [Description]
        [WhenOwnerCondition]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        AmountOfFields
    End Enum

    Public Enum PermissionTargetFieldIndex
        [Id]
        [Name]
        [TargettedNamedTask]
        [IsApplicationTarget]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        AmountOfFields
    End Enum

    Public Enum ResourceFieldIndex
        [ResourceId]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        AmountOfFields
    End Enum

    Public Enum ResourceDataFieldIndex
        [ResourceId]
        [BinData]
        [Url]
        [FileExtension]
        [Ident]
        AmountOfFields
    End Enum


    Public Enum ResourceHistoryFieldIndex
        [Id]
        [ResourceId]
        [MajorVersion]
        [MinorVersion]
        [ModifiedBy]
        [ModifiedDate]
        [Label]
        [BinData]
        [MetaData]
        AmountOfFields
    End Enum


    Public Enum RichTextValueCustomBankPropertyFieldIndex
        [CustomBankPropertyId_CustomBankProperty]
        [BankId]
        [ApplicableToMask]
        [Publishable]
        [Scorable]
        [Name]
        [Title]
        [Description]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [Code]
        [StateId]
        [Version]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum RichTextValueCustomBankPropertyValueFieldIndex
        [ResourceId_CustomBankPropertyValue]
        [CustomBankPropertyId_CustomBankPropertyValue]
        [DisplayValue]
        [ResourceId]
        [CustomBankPropertyId]
        [Value]
        [RichTextValueCustomBankPropertyValueId]
        AmountOfFields
    End Enum


    Public Enum RoleFieldIndex
        [Id]
        [Name]
        [Description]
        [IsApplicationRole]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        AmountOfFields
    End Enum


    Public Enum RolePermissionFieldIndex
        [RoleId]
        [PermissionTargetId]
        [PermissionId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        AmountOfFields
    End Enum


    Public Enum StateFieldIndex
        [StateId]
        [Name]
        [Title]
        [Description]
        AmountOfFields
    End Enum


    Public Enum StateActionFieldIndex
        [Target]
        [StateId]
        [ActionId]
        AmountOfFields
    End Enum


    Public Enum TestPackageResourceFieldIndex
        [ResourceId_Resource]
        [Version]
        [BankId]
        [Name]
        [Title]
        [Description]
        [StateId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [OriginalVersion]
        [OriginalName]
        [ResourceId]
        AmountOfFields
    End Enum


    Public Enum TreeStructureCustomBankPropertyFieldIndex
        [CustomBankPropertyId_CustomBankProperty]
        [BankId]
        [ApplicableToMask]
        [Publishable]
        [Scorable]
        [Name]
        [Title]
        [Description]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [Code]
        [StateId]
        [Version]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum TreeStructureCustomBankPropertySelectedPartFieldIndex
        [TreeStructurePartId]
        [ResourceId]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum TreeStructureCustomBankPropertyValueFieldIndex
        [ResourceId_CustomBankPropertyValue]
        [CustomBankPropertyId_CustomBankPropertyValue]
        [DisplayValue]
        [ResourceId]
        [CustomBankPropertyId]
        AmountOfFields
    End Enum


    Public Enum TreeStructurePartCustomBankPropertyFieldIndex
        [TreeStructurePartCustomBankPropertyId]
        [CustomBankPropertyId]
        [Name]
        [Title]
        [Code]
        AmountOfFields
    End Enum


    Public Enum UserFieldIndex
        [Id]
        [UserName]
        [Password]
        [FullName]
        [Active]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        [AuthenticationType]
        [UserSettings]
        [ChangePassword]
        [AllowedFeatures]
        AmountOfFields
    End Enum


    Public Enum UserApplicationRoleFieldIndex
        [UserId]
        [ApplicationRoleId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        AmountOfFields
    End Enum


    Public Enum UserBankRoleFieldIndex
        [UserId]
        [BankId]
        [BankRoleId]
        [CreationDate]
        [CreatedBy]
        [ModifiedDate]
        [ModifiedBy]
        AmountOfFields
    End Enum




    Public Enum EntityType
        ActionEntity
        AspectResourceEntity
        AssessmentTestResourceEntity
        BankEntity
        ChildConceptStructurePartCustomBankPropertyEntity
        ChildTreeStructurePartCustomBankPropertyEntity
        ConceptStructureCustomBankPropertyEntity
        ConceptStructureCustomBankPropertySelectedPartEntity
        ConceptStructureCustomBankPropertyValueEntity
        ConceptStructurePartCustomBankPropertyEntity
        ConceptTypeEntity
        ControlTemplateResourceEntity
        CustomBankPropertyEntity
        CustomBankPropertyValueEntity
        DataSourceResourceEntity
        DependentResourceEntity
        FreeValueCustomBankPropertyEntity
        FreeValueCustomBankPropertyValueEntity
        GenericResourceEntity
        HiddenResourceEntity
        ItemLayoutTemplateResourceEntity
        ItemResourceEntity
        ListCustomBankPropertyEntity
        ListCustomBankPropertySelectedValueEntity
        ListCustomBankPropertyValueEntity
        ListValueCustomBankPropertyEntity
        PackageResourceEntity
        PermissionEntity
        PermissionTargetEntity
        ResourceEntity
        ResourceDataEntity
        ResourceHistoryEntity
        RichTextValueCustomBankPropertyEntity
        RichTextValueCustomBankPropertyValueEntity
        RoleEntity
        RolePermissionEntity
        StateEntity
        StateActionEntity
        TestPackageResourceEntity
        TreeStructureCustomBankPropertyEntity
        TreeStructureCustomBankPropertySelectedPartEntity
        TreeStructureCustomBankPropertyValueEntity
        TreeStructurePartCustomBankPropertyEntity
        UserEntity
        UserApplicationRoleEntity
        UserBankRoleEntity
    End Enum








End Namespace


