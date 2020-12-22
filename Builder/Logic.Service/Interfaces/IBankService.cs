using System;
using System.Collections.Generic;
using CustomClasses;
using Enums;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.ResourceProperties;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Interfaces
{

    public interface IBankService
    {


        EntityCollection GetBankHierarchy();

        EntityCollection GetBankHierarchyFilteredByBankIds(int[] bankIds);


        void UpdateBankHierarchy(EntityCollection banks);



        BankEntity GetBank(int bankId);

        bool BankExists(int bankId);

        BankEntity GetBankWithOptions(int bankId, bool withEditInfo, bool withCustomProperties);

        string GetBankPath(int bankId);

        string GetBankName(int bankId);

        List<int> GetListOfBankIds();

        BankStatistics GetBankStatistics(int bankId, string userName);

        void UpdateBank(BankEntity bank);

        string DeleteBank(BankEntity bank);

        bool ClearBank(int bankId);

        string ClearAndDeleteBankHierarchical(int bankId);

        SerializableDictionaryInteger FetchAllRelations();



        string UpdateCustomProperty(CustomBankPropertyEntity customBankProperty);

        ConceptStructureCustomBankPropertyEntity GetConceptStructureCustomBankProperty(ConceptStructureCustomBankPropertyEntity customProperty);

        TreeStructureCustomBankPropertyEntity GetTreeStructureCustomBankProperty(TreeStructureCustomBankPropertyEntity customProperty);

        ListCustomBankPropertyEntity GetListCustomBankProperty(ListCustomBankPropertyEntity customProperty);

        FreeValueCustomBankPropertyEntity GetFreeValueCustomBankProperty(FreeValueCustomBankPropertyEntity customProperty);

        RichTextValueCustomBankPropertyEntity GetRichTextValueCustomBankProperty(RichTextValueCustomBankPropertyEntity customProperty);

        CustomBankPropertyEntity GetCustomBankProperty(Guid customBankPropertyId);

        IList<CustomBankPropertyEntity> GetCustomBankProperties(IList<Guid> customBankPropertyId);

        EntityCollection GetAllConceptStructuresForProperty(ConceptStructureCustomBankPropertyEntity conceptStructureCustomBankPropertyEntity);

        EntityCollection GetAllTreeStructuresForProperty(TreeStructureCustomBankPropertyEntity treeStructureCustomBankPropertyEntity);

        EntityCollection GetReferencesForCustomBankProperty(CustomBankPropertyEntity customBankProperty);

        EntityCollection GetReferencedCustomBankPropertiesForListOfResources(List<Guid> resourceIds);

        EntityCollection GetAllConceptTypes();

        EntityCollection GetCustomBankPropertiesForBranch(BankEntity bank, ResourceTypeEnum applicableTo);


        EntityCollection GetCustomBankPropertiesForBranchById(int bankId, ResourceTypeEnum applicableTo);

        ResourcePropertyDefinitionCollection GetResourcePropertyDefinitions(BankEntity bank);

        string UpdateCustomProperties(EntityCollection entitiesToUpdate);

        string DeleteCustomProperties(EntityCollection entitiesToRemove);

        string DeleteCustomPropertiesForced(EntityCollection entitiesToRemove);

        string DeleteCustomPropertyValues(EntityCollection entitiesToRemove);

        ConceptStructurePartCustomBankPropertyEntity PopulateConceptCustomBankPropertyHierarchy(Guid id);

        TreeStructureCustomBankPropertyEntity PopulateTreeCustomBankPropertyHierarchy(Guid id);

        EntityCollection CustomBankPropertyExistsInBankhierarchy(BankEntity anchorBank, string customBankPropertyName);

        bool IsCustomBankPropertyValueReferenced(Guid id, CustomBankPropertyType customBankPropertyType);

        EntityCollection GetListCustomBankPropertyValueReferences(Guid id);

        EntityCollection GetListValueCustomBankProperties(List<Guid> ids);

        EntityCollection GetTreeStructureCustomBankPropertyValueReferences(Guid id);

        EntityCollection GetTreeStructurePartCustomBankProperties(List<Guid> ids, bool withChildTreeStructure);

        string UpdateCustomPropertyValue(CustomBankPropertyValueEntity customBankPropertyValue);

        string UpdateCustomPropertyValues(EntityCollection customBankPropertyValues);

        EntityCollection GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(List<Guid> customPropertyIds, List<Guid> resourceIds, bool onlyWithEmptyDisplayValue);

        EntityCollection GetTreeStructurePartCustomBankPropertiesByCustomBankPropertyIds(List<Guid> ids);

    }
}
