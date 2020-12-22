using System;
using System.Collections.Generic;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Decorators
{

    public abstract class BaseBankServiceDecorator : IBankService
    {

        private IBankService decoree;

        public BaseBankServiceDecorator(IBankService decoree)
        {
            this.decoree = decoree;
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetBankHierarchy()
        {
            return decoree.GetBankHierarchy();
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetBankHierarchyFilteredByBankIds(System.Int32[] bankIds)
        {
            return decoree.GetBankHierarchyFilteredByBankIds(bankIds);
        }

        public virtual void UpdateBankHierarchy(Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection banks)
        {
            decoree.UpdateBankHierarchy(banks);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.BankEntity GetBank(System.Int32 bankId)
        {
            return decoree.GetBank(bankId);
        }

        public virtual System.String GetBankName(System.Int32 bankId)
        {
            return decoree.GetBankName(bankId);
        }

        public virtual List<int> GetListOfBankIds()
        {
            return decoree.GetListOfBankIds();
        }

        public virtual string GetBankPath(int bankId)
        {
            return decoree.GetBankPath(bankId);
        }

        public virtual CustomClasses.BankStatistics GetBankStatistics(System.Int32 bankId, System.String userName)
        {
            return decoree.GetBankStatistics(bankId, userName);
        }

        public virtual void UpdateBank(Questify.Builder.Model.ContentModel.EntityClasses.BankEntity bank)
        {
            decoree.UpdateBank(bank);
        }

        public virtual System.String DeleteBank(Questify.Builder.Model.ContentModel.EntityClasses.BankEntity bank)
        {
            return decoree.DeleteBank(bank);
        }

        public virtual System.Boolean ClearBank(System.Int32 bankId)
        {
            return decoree.ClearBank(bankId);
        }

        public virtual SerializableDictionaryInteger FetchAllRelations()
        {
            return decoree.FetchAllRelations();
        }

        public virtual System.String UpdateCustomProperty(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity customBankProperty)
        {
            return decoree.UpdateCustomProperty(customBankProperty);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ConceptStructureCustomBankPropertyEntity GetConceptStructureCustomBankProperty(Questify.Builder.Model.ContentModel.EntityClasses.ConceptStructureCustomBankPropertyEntity customProperty)
        {
            return decoree.GetConceptStructureCustomBankProperty(customProperty);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.TreeStructureCustomBankPropertyEntity GetTreeStructureCustomBankProperty(Questify.Builder.Model.ContentModel.EntityClasses.TreeStructureCustomBankPropertyEntity customProperty)
        {
            return decoree.GetTreeStructureCustomBankProperty(customProperty);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ListCustomBankPropertyEntity GetListCustomBankProperty(Questify.Builder.Model.ContentModel.EntityClasses.ListCustomBankPropertyEntity customProperty)
        {
            return decoree.GetListCustomBankProperty(customProperty);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.FreeValueCustomBankPropertyEntity GetFreeValueCustomBankProperty(Questify.Builder.Model.ContentModel.EntityClasses.FreeValueCustomBankPropertyEntity customProperty)
        {
            return decoree.GetFreeValueCustomBankProperty(customProperty);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity GetCustomBankProperty(System.Guid customBankPropertyId)
        {
            return decoree.GetCustomBankProperty(customBankPropertyId);
        }

        public virtual System.Collections.Generic.IList<Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity> GetCustomBankProperties(System.Collections.Generic.IList<System.Guid> customBankPropertyId)
        {
            return decoree.GetCustomBankProperties(customBankPropertyId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAllConceptStructuresForProperty(Questify.Builder.Model.ContentModel.EntityClasses.ConceptStructureCustomBankPropertyEntity conceptStructureCustomBankPropertyEntity)
        {
            return decoree.GetAllConceptStructuresForProperty(conceptStructureCustomBankPropertyEntity);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAllTreeStructuresForProperty(Questify.Builder.Model.ContentModel.EntityClasses.TreeStructureCustomBankPropertyEntity treeStructureCustomBankPropertyEntity)
        {
            return decoree.GetAllTreeStructuresForProperty(treeStructureCustomBankPropertyEntity);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetReferencesForCustomBankProperty(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity customBankProperty)
        {
            return decoree.GetReferencesForCustomBankProperty(customBankProperty);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAllConceptTypes()
        {
            return decoree.GetAllConceptTypes();
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetCustomBankPropertiesForBranch(Questify.Builder.Model.ContentModel.EntityClasses.BankEntity bank, ResourceTypeEnum applicableTo)
        {
            return decoree.GetCustomBankPropertiesForBranch(bank, applicableTo);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetCustomBankPropertiesForBranchById(System.Int32 bankId, ResourceTypeEnum applicableTo)
        {
            return decoree.GetCustomBankPropertiesForBranchById(bankId, applicableTo);
        }

        public virtual Questify.Builder.Model.ContentModel.ResourceProperties.ResourcePropertyDefinitionCollection GetResourcePropertyDefinitions(Questify.Builder.Model.ContentModel.EntityClasses.BankEntity bank)
        {
            return decoree.GetResourcePropertyDefinitions(bank);
        }

        public virtual System.String UpdateCustomProperties(Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection entitiesToUpdate)
        {
            return decoree.UpdateCustomProperties(entitiesToUpdate);
        }

        public virtual System.String DeleteCustomProperties(Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection entitiesToRemove)
        {
            return decoree.DeleteCustomProperties(entitiesToRemove);
        }

        public virtual System.String DeleteCustomPropertiesForced(Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection entitiesToRemove)
        {
            return decoree.DeleteCustomPropertiesForced(entitiesToRemove);
        }

        public virtual System.String DeleteCustomPropertyValues(Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection entitiesToRemove)
        {
            return decoree.DeleteCustomPropertyValues(entitiesToRemove);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ConceptStructurePartCustomBankPropertyEntity PopulateConceptCustomBankPropertyHierarchy(System.Guid id)
        {
            return decoree.PopulateConceptCustomBankPropertyHierarchy(id);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.TreeStructureCustomBankPropertyEntity PopulateTreeCustomBankPropertyHierarchy(System.Guid id)
        {
            return decoree.PopulateTreeCustomBankPropertyHierarchy(id);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection CustomBankPropertyExistsInBankhierarchy(Questify.Builder.Model.ContentModel.EntityClasses.BankEntity anchorBank, System.String customBankPropertyName)
        {
            return decoree.CustomBankPropertyExistsInBankhierarchy(anchorBank, customBankPropertyName);
        }

        public virtual System.Boolean IsCustomBankPropertyValueReferenced(System.Guid id, CustomBankPropertyType customBankPropertyType)
        {
            return decoree.IsCustomBankPropertyValueReferenced(id, customBankPropertyType);
        }

        public virtual EntityCollection GetTreeStructurePartCustomBankPropertiesByCustomBankPropertyIds(List<Guid> ids)
        {
            return decoree.GetTreeStructurePartCustomBankPropertiesByCustomBankPropertyIds(ids);
        }

        public virtual EntityCollection GetReferencedCustomBankPropertiesForListOfResources(List<Guid> resourceIds)
        {
            return decoree.GetReferencedCustomBankPropertiesForListOfResources(resourceIds);
        }

        public virtual System.Boolean BankExists(System.Int32 bankId)
        {
            return decoree.BankExists(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.BankEntity GetBankWithOptions(System.Int32 bankId, System.Boolean withEditInfo, System.Boolean withCustomProperties)
        {
            return decoree.GetBankWithOptions(bankId, withEditInfo, withCustomProperties);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetListCustomBankPropertyValueReferences(System.Guid id)
        {
            return decoree.GetListCustomBankPropertyValueReferences(id);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetListValueCustomBankProperties(System.Collections.Generic.List<System.Guid> ids)
        {
            return decoree.GetListValueCustomBankProperties(ids);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetTreeStructureCustomBankPropertyValueReferences(System.Guid id)
        {
            return decoree.GetTreeStructureCustomBankPropertyValueReferences(id);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetTreeStructurePartCustomBankProperties(System.Collections.Generic.List<System.Guid> ids, System.Boolean withChildTreeStructure)
        {
            return decoree.GetTreeStructurePartCustomBankProperties(ids, withChildTreeStructure);
        }

        public virtual System.String UpdateCustomPropertyValue(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyValueEntity customBankPropertyValue)
        {
            return decoree.UpdateCustomPropertyValue(customBankPropertyValue);
        }

        public virtual System.String UpdateCustomPropertyValues(Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection customBankPropertyValues)
        {
            return decoree.UpdateCustomPropertyValues(customBankPropertyValues);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(System.Collections.Generic.List<System.Guid> customPropertyIds, System.Collections.Generic.List<System.Guid> resourceIds, System.Boolean onlyWithEmptyDisplayValue)
        {
            return decoree.GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(customPropertyIds, resourceIds, onlyWithEmptyDisplayValue);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.RichTextValueCustomBankPropertyEntity GetRichTextValueCustomBankProperty(Questify.Builder.Model.ContentModel.EntityClasses.RichTextValueCustomBankPropertyEntity customProperty)
        {
            return decoree.GetRichTextValueCustomBankProperty(customProperty);
        }

        public virtual string ClearAndDeleteBankHierarchical(int bankId)
        {
            return decoree.ClearAndDeleteBankHierarchical(bankId);
        }
    }
}
