Imports System
Imports System.Collections
Imports System.Data
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.DatabaseSpecific
    Friend NotInheritable Class PersistenceInfoProviderSingleton
        Private Shared ReadOnly _providerInstance As IPersistenceInfoProvider = New PersistenceInfoProviderCore()

        Private Sub New()
        End Sub

        Shared Sub New()
        End Sub

        Public Shared Function GetInstance() As IPersistenceInfoProvider
            Return _providerInstance
        End Function
    End Class

    Friend Class PersistenceInfoProviderCore
        Inherits PersistenceInfoProviderBase

        Friend Sub New()
            Init()
        End Sub

        Private Sub Init()
            Me.InitClass()
            InitActionEntityMappings()
            InitAspectResourceEntityMappings()
            InitAssessmentTestResourceEntityMappings()
            InitBankEntityMappings()
            InitChildConceptStructurePartCustomBankPropertyEntityMappings()
            InitChildTreeStructurePartCustomBankPropertyEntityMappings()
            InitConceptStructureCustomBankPropertyEntityMappings()
            InitConceptStructureCustomBankPropertySelectedPartEntityMappings()
            InitConceptStructureCustomBankPropertyValueEntityMappings()
            InitConceptStructurePartCustomBankPropertyEntityMappings()
            InitConceptTypeEntityMappings()
            InitControlTemplateResourceEntityMappings()
            InitCustomBankPropertyEntityMappings()
            InitCustomBankPropertyValueEntityMappings()
            InitDataSourceResourceEntityMappings()
            InitDependentResourceEntityMappings()
            InitFreeValueCustomBankPropertyEntityMappings()
            InitFreeValueCustomBankPropertyValueEntityMappings()
            InitGenericResourceEntityMappings()
            InitHiddenResourceEntityMappings()
            InitItemLayoutTemplateResourceEntityMappings()
            InitItemResourceEntityMappings()
            InitListCustomBankPropertyEntityMappings()
            InitListCustomBankPropertySelectedValueEntityMappings()
            InitListCustomBankPropertyValueEntityMappings()
            InitListValueCustomBankPropertyEntityMappings()
            InitPackageResourceEntityMappings()
            InitPermissionEntityMappings()
            InitPermissionTargetEntityMappings()
            InitResourceEntityMappings()
            InitResourceDataEntityMappings()
            InitResourceHistoryEntityMappings()
            InitRichTextValueCustomBankPropertyEntityMappings()
            InitRichTextValueCustomBankPropertyValueEntityMappings()
            InitRoleEntityMappings()
            InitRolePermissionEntityMappings()
            InitStateEntityMappings()
            InitStateActionEntityMappings()
            InitTestPackageResourceEntityMappings()
            InitTreeStructureCustomBankPropertyEntityMappings()
            InitTreeStructureCustomBankPropertySelectedPartEntityMappings()
            InitTreeStructureCustomBankPropertyValueEntityMappings()
            InitTreeStructurePartCustomBankPropertyEntityMappings()
            InitUserEntityMappings()
            InitUserApplicationRoleEntityMappings()
            InitUserBankRoleEntityMappings()
        End Sub

        Private Sub InitActionEntityMappings()
            Me.AddElementMapping("ActionEntity", "QuestifyBuilder", "dbo", "Action", 3, 0)
            Me.AddElementFieldMapping("ActionEntity", "ActionId", "actionId", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("ActionEntity", "Name", "name", False, "VarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("ActionEntity", "Title", "title", True, "VarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
        End Sub

        Private Sub InitAspectResourceEntityMappings()
            Me.AddElementMapping("AspectResourceEntity", "QuestifyBuilder", "dbo", "AspectResource", 2, 0)
            Me.AddElementFieldMapping("AspectResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("AspectResourceEntity", "RawScore", "rawScore", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
        End Sub

        Private Sub InitAssessmentTestResourceEntityMappings()
            Me.AddElementMapping("AssessmentTestResourceEntity", "QuestifyBuilder", "dbo", "AssessmentTestResource", 2, 0)
            Me.AddElementFieldMapping("AssessmentTestResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("AssessmentTestResourceEntity", "IsTemplate", "isTemplate", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 1)
        End Sub

        Private Sub InitBankEntityMappings()
            Me.AddElementMapping("BankEntity", "QuestifyBuilder", "dbo", "Bank", 8, 0)
            Me.AddElementFieldMapping("BankEntity", "Id", "id", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("BankEntity", "ParentBankId", "parentBankId", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
            Me.AddElementFieldMapping("BankEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("BankEntity", "Type", "type", True, "VarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("BankEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 4)
            Me.AddElementFieldMapping("BankEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 5)
            Me.AddElementFieldMapping("BankEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 6)
            Me.AddElementFieldMapping("BankEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 7)
        End Sub

        Private Sub InitChildConceptStructurePartCustomBankPropertyEntityMappings()
            Me.AddElementMapping("ChildConceptStructurePartCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "ChildConceptStructurePartCustomBankProperty", 4, 0)
            Me.AddElementFieldMapping("ChildConceptStructurePartCustomBankPropertyEntity", "Id", "id", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ChildConceptStructurePartCustomBankPropertyEntity", "ConceptStructurePartCustomBankPropertyId", "conceptStructurePartCustomBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("ChildConceptStructurePartCustomBankPropertyEntity", "ChildConceptStructurePartCustomBankPropertyId", "childConceptStructurePartCustomBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 2)
            Me.AddElementFieldMapping("ChildConceptStructurePartCustomBankPropertyEntity", "VisualOrder", "visualOrder", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 3)
        End Sub

        Private Sub InitChildTreeStructurePartCustomBankPropertyEntityMappings()
            Me.AddElementMapping("ChildTreeStructurePartCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "ChildTreeStructurePartCustomBankProperty", 4, 0)
            Me.AddElementFieldMapping("ChildTreeStructurePartCustomBankPropertyEntity", "Id", "id", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ChildTreeStructurePartCustomBankPropertyEntity", "ChildTreeStructurePartCustomBankPropertyId", "childTreeStructurePartCustomBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("ChildTreeStructurePartCustomBankPropertyEntity", "TreeStructurePartCustomBankPropertyId", "treeStructurePartCustomBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 2)
            Me.AddElementFieldMapping("ChildTreeStructurePartCustomBankPropertyEntity", "VisualOrder", "visualOrder", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 3)
        End Sub

        Private Sub InitConceptStructureCustomBankPropertyEntityMappings()
            Me.AddElementMapping("ConceptStructureCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "ConceptStructureCustomBankProperty", 1, 0)
            Me.AddElementFieldMapping("ConceptStructureCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
        End Sub

        Private Sub InitConceptStructureCustomBankPropertySelectedPartEntityMappings()
            Me.AddElementMapping("ConceptStructureCustomBankPropertySelectedPartEntity", "QuestifyBuilder", "dbo", "ConceptStructureCustomBankPropertySelectedPart", 3, 0)
            Me.AddElementFieldMapping("ConceptStructureCustomBankPropertySelectedPartEntity", "ConceptStructurePartId", "conceptStructurePartId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ConceptStructureCustomBankPropertySelectedPartEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("ConceptStructureCustomBankPropertySelectedPartEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 2)
        End Sub

        Private Sub InitConceptStructureCustomBankPropertyValueEntityMappings()
            Me.AddElementMapping("ConceptStructureCustomBankPropertyValueEntity", "QuestifyBuilder", "dbo", "ConceptStructureCustomBankPropertyValue", 2, 0)
            Me.AddElementFieldMapping("ConceptStructureCustomBankPropertyValueEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ConceptStructureCustomBankPropertyValueEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
        End Sub

        Private Sub InitConceptStructurePartCustomBankPropertyEntityMappings()
            Me.AddElementMapping("ConceptStructurePartCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "ConceptStructurePartCustomBankProperty", 6, 0)
            Me.AddElementFieldMapping("ConceptStructurePartCustomBankPropertyEntity", "ConceptStructurePartCustomBankPropertyId", "conceptStructurePartCustomBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ConceptStructurePartCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("ConceptStructurePartCustomBankPropertyEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("ConceptStructurePartCustomBankPropertyEntity", "Title", "title", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("ConceptStructurePartCustomBankPropertyEntity", "Code", "code", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 4)
            Me.AddElementFieldMapping("ConceptStructurePartCustomBankPropertyEntity", "ConceptTypeId", "conceptTypeId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 5)
        End Sub

        Private Sub InitConceptTypeEntityMappings()
            Me.AddElementMapping("ConceptTypeEntity", "QuestifyBuilder", "dbo", "ConceptType", 3, 0)
            Me.AddElementFieldMapping("ConceptTypeEntity", "ConceptTypeId", "conceptTypeId", False, "Int", 0, 10, 0, False, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("ConceptTypeEntity", "Name", "name", False, "VarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("ConceptTypeEntity", "ApplicableToMask", "applicableToMask", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
        End Sub

        Private Sub InitControlTemplateResourceEntityMappings()
            Me.AddElementMapping("ControlTemplateResourceEntity", "QuestifyBuilder", "dbo", "ControlTemplateResource", 1, 0)
            Me.AddElementFieldMapping("ControlTemplateResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
        End Sub

        Private Sub InitCustomBankPropertyEntityMappings()
            Me.AddElementMapping("CustomBankPropertyEntity", "QuestifyBuilder", "dbo", "CustomBankProperty", 15, 0)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "BankId", "bankId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "ApplicableToMask", "applicableToMask", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "Publishable", "publishable", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 3)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "Scorable", "scorable", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 4)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 5)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "Title", "title", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 6)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "Description", "description", True, "NVarChar", 2147483647, 0, 0, False, "", Nothing, GetType(System.String), 7)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 8)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 9)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 10)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 11)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "Code", "code", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 12)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "StateId", "stateId", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 13)
            Me.AddElementFieldMapping("CustomBankPropertyEntity", "Version", "version", False, "VarChar", 20, 0, 0, False, "", Nothing, GetType(System.String), 14)
        End Sub

        Private Sub InitCustomBankPropertyValueEntityMappings()
            Me.AddElementMapping("CustomBankPropertyValueEntity", "QuestifyBuilder", "dbo", "CustomBankPropertyValue", 3, 0)
            Me.AddElementFieldMapping("CustomBankPropertyValueEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("CustomBankPropertyValueEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("CustomBankPropertyValueEntity", "DisplayValue", "displayValue", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 2)
        End Sub

        Private Sub InitDataSourceResourceEntityMappings()
            Me.AddElementMapping("DataSourceResourceEntity", "QuestifyBuilder", "dbo", "DataSourceResource", 3, 0)
            Me.AddElementFieldMapping("DataSourceResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("DataSourceResourceEntity", "DataSourceType", "dataSourceType", False, "VarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("DataSourceResourceEntity", "IsTemplate", "isTemplate", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 2)
        End Sub

        Private Sub InitDependentResourceEntityMappings()
            Me.AddElementMapping("DependentResourceEntity", "QuestifyBuilder", "dbo", "DependentResource", 2, 0)
            Me.AddElementFieldMapping("DependentResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("DependentResourceEntity", "DependentResourceId", "dependentResourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
        End Sub

        Private Sub InitFreeValueCustomBankPropertyEntityMappings()
            Me.AddElementMapping("FreeValueCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "FreeValueCustomBankProperty", 1, 0)
            Me.AddElementFieldMapping("FreeValueCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
        End Sub

        Private Sub InitFreeValueCustomBankPropertyValueEntityMappings()
            Me.AddElementMapping("FreeValueCustomBankPropertyValueEntity", "QuestifyBuilder", "dbo", "FreeValueCustomBankPropertyValue", 4, 0)
            Me.AddElementFieldMapping("FreeValueCustomBankPropertyValueEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("FreeValueCustomBankPropertyValueEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("FreeValueCustomBankPropertyValueEntity", "Value", "value", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("FreeValueCustomBankPropertyValueEntity", "FreeValueCustomBankPropertyValueId", "freeValueCustomBankPropertyValueId", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 3)
        End Sub

        Private Sub InitGenericResourceEntityMappings()
            Me.AddElementMapping("GenericResourceEntity", "QuestifyBuilder", "dbo", "GenericResource", 5, 0)
            Me.AddElementFieldMapping("GenericResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("GenericResourceEntity", "MediaType", "mediaType", False, "VarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("GenericResourceEntity", "Size", "size", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
            Me.AddElementFieldMapping("GenericResourceEntity", "Dimensions", "dimensions", True, "VarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("GenericResourceEntity", "IsTemplate", "isTemplate", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 4)
        End Sub

        Private Sub InitHiddenResourceEntityMappings()
            Me.AddElementMapping("HiddenResourceEntity", "QuestifyBuilder", "dbo", "HiddenResource", 2, 0)
            Me.AddElementFieldMapping("HiddenResourceEntity", "ResourceId", "ResourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("HiddenResourceEntity", "BankId", "BankId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
        End Sub

        Private Sub InitItemLayoutTemplateResourceEntityMappings()
            Me.AddElementMapping("ItemLayoutTemplateResourceEntity", "QuestifyBuilder", "dbo", "ItemLayoutTemplateResource", 2, 0)
            Me.AddElementFieldMapping("ItemLayoutTemplateResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ItemLayoutTemplateResourceEntity", "ItemType", "itemType", True, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 1)
        End Sub

        Private Sub InitItemResourceEntityMappings()
            Me.AddElementMapping("ItemResourceEntity", "QuestifyBuilder", "dbo", "ItemResource", 12, 0)
            Me.AddElementFieldMapping("ItemResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ItemResourceEntity", "IsSystemItem", "isSystemItem", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 1)
            Me.AddElementFieldMapping("ItemResourceEntity", "AlternativesCount", "alternativesCount", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
            Me.AddElementFieldMapping("ItemResourceEntity", "KeyValues", "keyValues", True, "NVarChar", 500, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("ItemResourceEntity", "ResponseCount", "responseCount", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 4)
            Me.AddElementFieldMapping("ItemResourceEntity", "RawScore", "rawScore", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 5)
            Me.AddElementFieldMapping("ItemResourceEntity", "TesterSchemaVersion", "TesterSchemaVersion", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 6)
            Me.AddElementFieldMapping("ItemResourceEntity", "Iltname", "ILTName", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 7)
            Me.AddElementFieldMapping("ItemResourceEntity", "Iltversion", "ILTVersion", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 8)
            Me.AddElementFieldMapping("ItemResourceEntity", "MaxScore", "MaxScore", True, "Decimal", 0, 9, 2, False, "", Nothing, GetType(System.Decimal), 9)
            Me.AddElementFieldMapping("ItemResourceEntity", "ItemAutoId", "ItemAutoId", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 10)
            Me.AddElementFieldMapping("ItemResourceEntity", "ItemId", "ItemId", True, "VarChar", 6, 0, 0, False, "", Nothing, GetType(System.String), 11)
        End Sub

        Private Sub InitListCustomBankPropertyEntityMappings()
            Me.AddElementMapping("ListCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "ListCustomBankProperty", 2, 0)
            Me.AddElementFieldMapping("ListCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ListCustomBankPropertyEntity", "MultipleSelect", "multipleSelect", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 1)
        End Sub

        Private Sub InitListCustomBankPropertySelectedValueEntityMappings()
            Me.AddElementMapping("ListCustomBankPropertySelectedValueEntity", "QuestifyBuilder", "dbo", "ListCustomBankPropertySelectedValue", 3, 0)
            Me.AddElementFieldMapping("ListCustomBankPropertySelectedValueEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ListCustomBankPropertySelectedValueEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("ListCustomBankPropertySelectedValueEntity", "ListValueBankCustomPropertyId", "listValueBankCustomPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 2)
        End Sub

        Private Sub InitListCustomBankPropertyValueEntityMappings()
            Me.AddElementMapping("ListCustomBankPropertyValueEntity", "QuestifyBuilder", "dbo", "ListCustomBankPropertyValue", 2, 0)
            Me.AddElementFieldMapping("ListCustomBankPropertyValueEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ListCustomBankPropertyValueEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
        End Sub

        Private Sub InitListValueCustomBankPropertyEntityMappings()
            Me.AddElementMapping("ListValueCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "ListValueCustomBankProperty", 5, 0)
            Me.AddElementFieldMapping("ListValueCustomBankPropertyEntity", "ListValueBankCustomPropertyId", "listValueBankCustomPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ListValueCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("ListValueCustomBankPropertyEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("ListValueCustomBankPropertyEntity", "Title", "title", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("ListValueCustomBankPropertyEntity", "Code", "code", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 4)
        End Sub

        Private Sub InitPackageResourceEntityMappings()
            Me.AddElementMapping("PackageResourceEntity", "QuestifyBuilder", "dbo", "PackageResource", 1, 0)
            Me.AddElementFieldMapping("PackageResourceEntity", "ResourceId", "ResourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
        End Sub

        Private Sub InitPermissionEntityMappings()
            Me.AddElementMapping("PermissionEntity", "QuestifyBuilder", "dbo", "Permission", 8, 0)
            Me.AddElementFieldMapping("PermissionEntity", "Id", "id", False, "Int", 0, 10, 0, False, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("PermissionEntity", "Name", "name", False, "NVarChar", 20, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("PermissionEntity", "Description", "description", True, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("PermissionEntity", "WhenOwnerCondition", "whenOwnerCondition", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 3)
            Me.AddElementFieldMapping("PermissionEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 4)
            Me.AddElementFieldMapping("PermissionEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 5)
            Me.AddElementFieldMapping("PermissionEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 6)
            Me.AddElementFieldMapping("PermissionEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 7)
        End Sub

        Private Sub InitPermissionTargetEntityMappings()
            Me.AddElementMapping("PermissionTargetEntity", "QuestifyBuilder", "dbo", "PermissionTarget", 8, 0)
            Me.AddElementFieldMapping("PermissionTargetEntity", "Id", "id", False, "Int", 0, 10, 0, False, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("PermissionTargetEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("PermissionTargetEntity", "TargettedNamedTask", "targettedNamedTask", True, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("PermissionTargetEntity", "IsApplicationTarget", "isApplicationTarget", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 3)
            Me.AddElementFieldMapping("PermissionTargetEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 4)
            Me.AddElementFieldMapping("PermissionTargetEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 5)
            Me.AddElementFieldMapping("PermissionTargetEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 6)
            Me.AddElementFieldMapping("PermissionTargetEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 7)
        End Sub

        Private Sub InitResourceEntityMappings()
            Me.AddElementMapping("ResourceEntity", "QuestifyBuilder", "dbo", "Resource", 13, 0)
            Me.AddElementFieldMapping("ResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ResourceEntity", "Version", "version", False, "VarChar", 20, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("ResourceEntity", "BankId", "bankId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
            Me.AddElementFieldMapping("ResourceEntity", "Name", "name", False, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("ResourceEntity", "Title", "title", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 4)
            Me.AddElementFieldMapping("ResourceEntity", "Description", "description", True, "NVarChar", 2147483647, 0, 0, False, "", Nothing, GetType(System.String), 5)
            Me.AddElementFieldMapping("ResourceEntity", "StateId", "stateId", True, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 6)
            Me.AddElementFieldMapping("ResourceEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 7)
            Me.AddElementFieldMapping("ResourceEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 8)
            Me.AddElementFieldMapping("ResourceEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 9)
            Me.AddElementFieldMapping("ResourceEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 10)
            Me.AddElementFieldMapping("ResourceEntity", "OriginalVersion", "originalVersion", True, "VarChar", 20, 0, 0, False, "", Nothing, GetType(System.String), 11)
            Me.AddElementFieldMapping("ResourceEntity", "OriginalName", "originalName", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 12)
        End Sub

        Private Sub InitResourceDataEntityMappings()
            Me.AddElementMapping("ResourceDataEntity", "QuestifyBuilder", "dbo", "ResourceData", 5, 0)
            Me.AddElementFieldMapping("ResourceDataEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("ResourceDataEntity", "BinData", "binData", True, "Image", 2147483647, 0, 0, False, "", Nothing, GetType(System.Byte()), 1)
            Me.AddElementFieldMapping("ResourceDataEntity", "Url", "url", True, "NVarChar", 500, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("ResourceDataEntity", "FileExtension", "fileExtension", True, "VarChar", 8, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("ResourceDataEntity", "Ident", "ident", False, "BigInt", 0, 19, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int64), 4)
        End Sub

        Private Sub InitResourceHistoryEntityMappings()
            Me.AddElementMapping("ResourceHistoryEntity", "QuestifyBuilder", "dbo", "ResourceHistory", 9, 0)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "Id", "Id", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "ResourceId", "ResourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "MajorVersion", "MajorVersion", False, "SmallInt", 0, 5, 0, False, "", Nothing, GetType(System.Int16), 2)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "MinorVersion", "MinorVersion", False, "SmallInt", 0, 5, 0, False, "", Nothing, GetType(System.Int16), 3)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "ModifiedBy", "ModifiedBy", False, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 4)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "ModifiedDate", "ModifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 5)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "Label", "Label", True, "NVarChar", 4000, 0, 0, False, "", Nothing, GetType(System.String), 6)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "BinData", "BinData", True, "VarBinary", 2147483647, 0, 0, False, "", Nothing, GetType(System.Byte()), 7)
            Me.AddElementFieldMapping("ResourceHistoryEntity", "MetaData", "MetaData", True, "VarBinary", 2147483647, 0, 0, False, "", Nothing, GetType(System.Byte()), 8)
        End Sub

        Private Sub InitRichTextValueCustomBankPropertyEntityMappings()
            Me.AddElementMapping("RichTextValueCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "RichTextValueCustomBankProperty", 1, 0)
            Me.AddElementFieldMapping("RichTextValueCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
        End Sub

        Private Sub InitRichTextValueCustomBankPropertyValueEntityMappings()
            Me.AddElementMapping("RichTextValueCustomBankPropertyValueEntity", "QuestifyBuilder", "dbo", "RichTextValueCustomBankPropertyValue", 4, 0)
            Me.AddElementFieldMapping("RichTextValueCustomBankPropertyValueEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("RichTextValueCustomBankPropertyValueEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("RichTextValueCustomBankPropertyValueEntity", "Value", "value", True, "NVarChar", 2147483647, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("RichTextValueCustomBankPropertyValueEntity", "RichTextValueCustomBankPropertyValueId", "richTextValueCustomBankPropertyValueId", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 3)
        End Sub

        Private Sub InitRoleEntityMappings()
            Me.AddElementMapping("RoleEntity", "QuestifyBuilder", "dbo", "Role", 8, 0)
            Me.AddElementFieldMapping("RoleEntity", "Id", "id", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("RoleEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("RoleEntity", "Description", "description", True, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("RoleEntity", "IsApplicationRole", "isApplicationRole", True, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 3)
            Me.AddElementFieldMapping("RoleEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 4)
            Me.AddElementFieldMapping("RoleEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 5)
            Me.AddElementFieldMapping("RoleEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 6)
            Me.AddElementFieldMapping("RoleEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 7)
        End Sub

        Private Sub InitRolePermissionEntityMappings()
            Me.AddElementMapping("RolePermissionEntity", "QuestifyBuilder", "dbo", "RolePermission", 7, 0)
            Me.AddElementFieldMapping("RolePermissionEntity", "RoleId", "roleId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("RolePermissionEntity", "PermissionTargetId", "permissionTargetId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
            Me.AddElementFieldMapping("RolePermissionEntity", "PermissionId", "permissionId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
            Me.AddElementFieldMapping("RolePermissionEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 3)
            Me.AddElementFieldMapping("RolePermissionEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 4)
            Me.AddElementFieldMapping("RolePermissionEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 5)
            Me.AddElementFieldMapping("RolePermissionEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 6)
        End Sub

        Private Sub InitStateEntityMappings()
            Me.AddElementMapping("StateEntity", "QuestifyBuilder", "dbo", "State", 4, 0)
            Me.AddElementFieldMapping("StateEntity", "StateId", "stateId", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("StateEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("StateEntity", "Title", "title", True, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("StateEntity", "Description", "description", True, "VarChar", 4000, 0, 0, False, "", Nothing, GetType(System.String), 3)
        End Sub

        Private Sub InitStateActionEntityMappings()
            Me.AddElementMapping("StateActionEntity", "QuestifyBuilder", "dbo", "StateAction", 3, 0)
            Me.AddElementFieldMapping("StateActionEntity", "Target", "target", False, "VarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 0)
            Me.AddElementFieldMapping("StateActionEntity", "StateId", "stateId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
            Me.AddElementFieldMapping("StateActionEntity", "ActionId", "actionId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
        End Sub

        Private Sub InitTestPackageResourceEntityMappings()
            Me.AddElementMapping("TestPackageResourceEntity", "QuestifyBuilder", "dbo", "TestPackageResource", 1, 0)
            Me.AddElementFieldMapping("TestPackageResourceEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
        End Sub

        Private Sub InitTreeStructureCustomBankPropertyEntityMappings()
            Me.AddElementMapping("TreeStructureCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "TreeStructureCustomBankProperty", 1, 0)
            Me.AddElementFieldMapping("TreeStructureCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
        End Sub

        Private Sub InitTreeStructureCustomBankPropertySelectedPartEntityMappings()
            Me.AddElementMapping("TreeStructureCustomBankPropertySelectedPartEntity", "QuestifyBuilder", "dbo", "TreeStructureCustomBankPropertySelectedPart", 3, 0)
            Me.AddElementFieldMapping("TreeStructureCustomBankPropertySelectedPartEntity", "TreeStructurePartId", "treeStructurePartId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("TreeStructureCustomBankPropertySelectedPartEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("TreeStructureCustomBankPropertySelectedPartEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 2)
        End Sub

        Private Sub InitTreeStructureCustomBankPropertyValueEntityMappings()
            Me.AddElementMapping("TreeStructureCustomBankPropertyValueEntity", "QuestifyBuilder", "dbo", "TreeStructureCustomBankPropertyValue", 2, 0)
            Me.AddElementFieldMapping("TreeStructureCustomBankPropertyValueEntity", "ResourceId", "resourceId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("TreeStructureCustomBankPropertyValueEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
        End Sub

        Private Sub InitTreeStructurePartCustomBankPropertyEntityMappings()
            Me.AddElementMapping("TreeStructurePartCustomBankPropertyEntity", "QuestifyBuilder", "dbo", "TreeStructurePartCustomBankProperty", 5, 0)
            Me.AddElementFieldMapping("TreeStructurePartCustomBankPropertyEntity", "TreeStructurePartCustomBankPropertyId", "treeStructurePartCustomBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 0)
            Me.AddElementFieldMapping("TreeStructurePartCustomBankPropertyEntity", "CustomBankPropertyId", "customBankPropertyId", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 1)
            Me.AddElementFieldMapping("TreeStructurePartCustomBankPropertyEntity", "Name", "name", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("TreeStructurePartCustomBankPropertyEntity", "Title", "title", True, "NVarChar", 255, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("TreeStructurePartCustomBankPropertyEntity", "Code", "code", False, "UniqueIdentifier", 0, 0, 0, False, "", Nothing, GetType(System.Guid), 4)
        End Sub

        Private Sub InitUserEntityMappings()
            Me.AddElementMapping("UserEntity", "QuestifyBuilder", "dbo", "User", 13, 0)
            Me.AddElementFieldMapping("UserEntity", "Id", "id", False, "Int", 0, 10, 0, True, "SCOPE_IDENTITY()", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("UserEntity", "UserName", "userName", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 1)
            Me.AddElementFieldMapping("UserEntity", "Password", "password", False, "NVarChar", 2147483647, 0, 0, False, "", Nothing, GetType(System.String), 2)
            Me.AddElementFieldMapping("UserEntity", "FullName", "fullName", False, "NVarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 3)
            Me.AddElementFieldMapping("UserEntity", "Active", "Active", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 4)
            Me.AddElementFieldMapping("UserEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 5)
            Me.AddElementFieldMapping("UserEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 6)
            Me.AddElementFieldMapping("UserEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 7)
            Me.AddElementFieldMapping("UserEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 8)
            Me.AddElementFieldMapping("UserEntity", "AuthenticationType", "authenticationType", False, "VarChar", 50, 0, 0, False, "", Nothing, GetType(System.String), 9)
            Me.AddElementFieldMapping("UserEntity", "UserSettings", "userSettings", False, "NVarChar", 2147483647, 0, 0, False, "", Nothing, GetType(System.String), 10)
            Me.AddElementFieldMapping("UserEntity", "ChangePassword", "changePassword", False, "Bit", 0, 0, 0, False, "", Nothing, GetType(System.Boolean), 11)
            Me.AddElementFieldMapping("UserEntity", "AllowedFeatures", "allowedFeatures", True, "NVarChar", 2000, 0, 0, False, "", Nothing, GetType(System.String), 12)
        End Sub

        Private Sub InitUserApplicationRoleEntityMappings()
            Me.AddElementMapping("UserApplicationRoleEntity", "QuestifyBuilder", "dbo", "UserApplicationRole", 6, 0)
            Me.AddElementFieldMapping("UserApplicationRoleEntity", "UserId", "userId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("UserApplicationRoleEntity", "ApplicationRoleId", "applicationRoleId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
            Me.AddElementFieldMapping("UserApplicationRoleEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 2)
            Me.AddElementFieldMapping("UserApplicationRoleEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 3)
            Me.AddElementFieldMapping("UserApplicationRoleEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 4)
            Me.AddElementFieldMapping("UserApplicationRoleEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 5)
        End Sub

        Private Sub InitUserBankRoleEntityMappings()
            Me.AddElementMapping("UserBankRoleEntity", "QuestifyBuilder", "dbo", "UserBankRole", 7, 0)
            Me.AddElementFieldMapping("UserBankRoleEntity", "UserId", "userId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 0)
            Me.AddElementFieldMapping("UserBankRoleEntity", "BankId", "bankId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 1)
            Me.AddElementFieldMapping("UserBankRoleEntity", "BankRoleId", "bankRoleId", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 2)
            Me.AddElementFieldMapping("UserBankRoleEntity", "CreationDate", "creationDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 3)
            Me.AddElementFieldMapping("UserBankRoleEntity", "CreatedBy", "createdBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 4)
            Me.AddElementFieldMapping("UserBankRoleEntity", "ModifiedDate", "modifiedDate", False, "DateTime", 0, 0, 0, False, "", Nothing, GetType(System.DateTime), 5)
            Me.AddElementFieldMapping("UserBankRoleEntity", "ModifiedBy", "modifiedBy", False, "Int", 0, 10, 0, False, "", Nothing, GetType(System.Int32), 6)
        End Sub

    End Class
End Namespace