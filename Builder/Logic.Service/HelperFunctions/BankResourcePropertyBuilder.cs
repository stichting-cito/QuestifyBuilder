using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Properties;
using Questify.Builder.Model.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.ResourceProperties;

namespace Questify.Builder.Logic.Service.HelperFunctions
{
    public class BankResourcePropertyBuilder
    {
        private readonly IBankService _bankService;
        public BankResourcePropertyBuilder(IBankService bankservice)
        {
            _bankService = bankservice;
        }

        public IEnumerable<ResourcePropertyDefinition> AddStaticPropertyDefinitionsOfResource()
        {
            Type e = typeof(ItemResourceFieldIndex);
            ResourcePropertyDefinitionCollection result = new ResourcePropertyDefinitionCollection();

            result.Add(new ResourcePropertyDefinition(new Guid("{1C954396-41E8-4c46-87F0-1FE423AB805F}"), Enum.GetName(e, ItemResourceFieldIndex.Name), Resources.ResourceProperties_Code, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{65F9D4CF-C95E-4661-9D15-3BAE93E92DA4}"), Enum.GetName(e, ItemResourceFieldIndex.Title), Resources.ResourceProperties_Title, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{77B896D6-1687-440e-9129-CFF75EB305D3}"), Enum.GetName(e, ItemResourceFieldIndex.Description), Resources.ResourceProperties_Description, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{D1A27A4E-90FF-412b-9D13-C428F31E544B}"), Enum.GetName(e, ItemResourceFieldIndex.CreatedBy), Resources.ResourceProperties_CreatedByFullName, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{5BCF2971-EEFD-4bb5-91F1-EDB053C05C53}"), Enum.GetName(e, ItemResourceFieldIndex.CreationDate), Resources.ResourceProperties_CreatedDate, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(DateTime), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{447A67EC-7170-4c78-830E-8E89874F8424}"), Enum.GetName(e, ItemResourceFieldIndex.ModifiedBy), Resources.ResourceProperties_ModifiedByFullName, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{F08C7F44-BCAF-490e-87E0-CC579637722C}"), Enum.GetName(e, ItemResourceFieldIndex.ModifiedDate), Resources.ResourceProperties_ModifiedDate, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(DateTime), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));

            result.Add(new ResourcePropertyDefinition(new Guid("{2EB8CD99-8D6A-47A0-A095-EBE91C35FB2E}"), "StateId", Resources.ResourceProperties_StateName, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue, ResourcePropertyDefinition.ListTypeEnum.List, ResourcePropertyHelpers.GetStateValues(ResourceFactory.Instance.GetAvailableStates()),
ResourcePropertyHelpers.GetPropertyValuesFromClassProperty, true));
            result.Add(new ResourcePropertyDefinition(new Guid("{8C3DA2DF-1260-4c8b-AE28-B3779505B5E0}"), "BankName", Resources.ResourceProperties_BankName, true, false, ResourceTypeEnum.AllResources, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{4D7B1EFE-A076-4048-8CC6-45CACF69421B}"), "ItemLayoutTemplateUsedName", Resources.ResourceProperties_ItemLayoutTemplateUsedName, true, false, ResourceTypeEnum.ItemResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{2168273C-B144-4550-BE67-7E96E2C4C3F1}"), "ItemTypeFromItemLayoutTemplate", Resources.ResourceProperties_ItemTypeFromItemLayoutTemplateString, true, false, ResourceTypeEnum.ItemResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue, ResourcePropertyDefinition.ListTypeEnum.List, ResourcePropertyHelpers.ConvertEnumToResourcePropertyListValueDefinitionCollection(typeof(ItemTypeEnum)),
            ResourcePropertyHelpers.GetPropertyValuesFromClassPropertyAsString));

            result.Add(new ResourcePropertyDefinition(new Guid("{0F896EB2-6904-445c-8200-D81F488B6A04}"), "AlternativesCount", Resources.ResourceProperties_AlternativesCount, true, false, ResourceTypeEnum.ItemResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(int), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{B7D08687-F009-4d59-8A58-2DD1A84004AC}"), "IsSystemItem", Resources.ResourceProperties_IsSystemItem, true, false, ResourceTypeEnum.ItemResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(bool), ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue, ResourcePropertyDefinition.ListTypeEnum.List, ResourcePropertyHelpers.CreateBooleanSingleListValueCollection(),
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{B30B0517-E086-4cd4-84FB-1BD819903633}"), "KeyValues", Resources.ResourceProperties_KeyValues, true, false, ResourceTypeEnum.ItemResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{D4E28AF8-DBE1-4da2-B427-12422FBB6DEA}"), "RawScore", Resources.ResourceProperties_RawScore, true, false, ResourceTypeEnum.ItemResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(int), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{683B5630-ADD2-42ab-884F-A3E2F043BA84}"), "ResponseCount", Resources.ResourceProperties_ResponseCount, true, false, ResourceTypeEnum.ItemResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(int), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));

            result.Add(new ResourcePropertyDefinition(new Guid("{740C0F5E-6B7D-49e7-89A8-B046E28E097D}"), "IsTemplate", Resources.ResourceProperties_IsTemplate, true, false, ResourceTypeEnum.AssessmentTestResource | ResourceTypeEnum.GenericResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(bool), ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue, ResourcePropertyDefinition.ListTypeEnum.List, ResourcePropertyHelpers.CreateBooleanSingleListValueCollection(),
ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));

            result.Add(new ResourcePropertyDefinition(new Guid("{BD56EA3F-C16A-4dd3-A036-97F376756FC7}"), "Dimensions", Resources.ResourceProperties_Dimensions, true, false, ResourceTypeEnum.GenericResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{408AFE2B-2E1C-4546-A172-4CDD81A4569A}"), "MediaType", Resources.ResourceProperties_MediaType, true, false, ResourceTypeEnum.GenericResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{64A9A2C7-4B0D-4c2b-A3F9-8FA08CA092B3}"), "Size", Resources.ResourceProperties_Size, true, false, ResourceTypeEnum.GenericResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(int), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));

            result.Add(new ResourcePropertyDefinition(new Guid("{254D0C42-A652-4caf-A867-997A910E77D9}"), "DataSourceResource", Resources.ResourceProperties_DataSourceType, true, false, ResourceTypeEnum.ControlTemplateResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));
            result.Add(new ResourcePropertyDefinition(new Guid("{18A90E55-D2CF-4f27-AA8F-216E8EA22612}"), "IsTemplate", Resources.ResourceProperties_IsTemplate, true, false, ResourceTypeEnum.ControlTemplateResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue, ResourcePropertyDefinition.ListTypeEnum.List, ResourcePropertyHelpers.CreateBooleanSingleListValueCollection(),
            ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));

            result.Add(new ResourcePropertyDefinition(new Guid("{31110928-4A90-4fc1-9E3B-04B918BF5665}"), "ItemTypeString", Resources.ResourceProperties_ItemTypeString, true, false, ResourceTypeEnum.ItemLayoutTemplateResource, ResourcePropertyDefinition.PropertyTypeEnum.Static, typeof(string), ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText, ResourcePropertyDefinition.ListTypeEnum.NoList, null,
ResourcePropertyHelpers.GetPropertyValuesFromClassProperty));

            return result;
        }

        public ResourcePropertyDefinitionCollection AddDynamicPropertyDefinitionsOfResource(int bankId)
        {
            ResourcePropertyDefinitionCollection result = new ResourcePropertyDefinitionCollection();
            foreach (CustomBankPropertyEntity customBankProperty in _bankService.GetCustomBankPropertiesForBranchById(bankId, ResourceTypeEnum.AllResources))
            {
                if (customBankProperty == null)
                {
                    continue;
                }

                if (customBankProperty is FreeValueCustomBankPropertyEntity)
                {
                    result.Add(new ResourcePropertyDefinition(
                        customBankProperty.CustomBankPropertyId,
                        customBankProperty.Name,
                        customBankProperty.Title,
                        customBankProperty.Publishable,
                        customBankProperty.Scorable,
                        (ResourceTypeEnum)customBankProperty.ApplicableToMask,
                        ResourcePropertyDefinition.PropertyTypeEnum.Dynamic,
                        customBankProperty.GetType(),
                        ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText,
                        ResourcePropertyDefinition.ListTypeEnum.NoList,
                        null,
                        ResourcePropertyHelpers.GetPropertyValuesFromCustomProperty));
                }
                else if (customBankProperty is ListCustomBankPropertyEntity)
                {
                    ListCustomBankPropertyEntity listCustomBankProperty = (ListCustomBankPropertyEntity)customBankProperty;
                    ResourcePropertyDefinition resPropDef = default(ResourcePropertyDefinition);
                    ResourceTypeEnum appl2Mask = (ResourceTypeEnum)customBankProperty.ApplicableToMask;
                    bool isMultiple = listCustomBankProperty.MultipleSelect;
                    IList<ResourcePropertyListValueDefinition> lst = CreateResourcePropertyListDefinitionCollection(listCustomBankProperty.ListValueCustomBankPropertyCollection);
                    resPropDef = new ResourcePropertyDefinition(
                        customBankProperty.CustomBankPropertyId,
                        customBankProperty.Name,
                        customBankProperty.Title,
                        customBankProperty.Publishable,
                        customBankProperty.Scorable,
                        appl2Mask,
                        ResourcePropertyDefinition.PropertyTypeEnum.Dynamic,
                        customBankProperty.GetType(),
                        isMultiple ? ResourcePropertyDefinition.PropertyValueTypeEnum.MultiListValue : ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue,
                        ResourcePropertyDefinition.ListTypeEnum.List,
                        lst,
                        ResourcePropertyHelpers.GetPropertyValuesFromCustomProperty)
                    { Code = customBankProperty.Code };

                    result.Add(resPropDef);
                }
                else if (customBankProperty is ConceptStructureCustomBankPropertyEntity)
                {
                    ConceptStructureCustomBankPropertyEntity conceptStructureCustomBankProperty = (ConceptStructureCustomBankPropertyEntity)customBankProperty;
                    ResourcePropertyDefinition resPropDef = default(ResourcePropertyDefinition);
                    ResourceTypeEnum appl2Mask = ResourceTypeEnum.ItemResource;
                    IList<ResourcePropertyListValueDefinition> lst = CreateResourcePropertyConceptDefinitionCollection(conceptStructureCustomBankProperty.ConceptStructurePartCustomBankPropertyCollection);
                    resPropDef = new ResourcePropertyDefinition(
                        customBankProperty.CustomBankPropertyId,
                        customBankProperty.Name,
                        customBankProperty.Title,
                        customBankProperty.Publishable,
                        customBankProperty.Scorable,
                        appl2Mask,
                        ResourcePropertyDefinition.PropertyTypeEnum.Dynamic,
                        customBankProperty.GetType(),
                        ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue,
                        ResourcePropertyDefinition.ListTypeEnum.Concept,
                        lst,
                        ResourcePropertyHelpers.GetPropertyValuesFromCustomProperty)
                    { Code = customBankProperty.Code };

                    result.Add(resPropDef);
                }
                else if (customBankProperty is TreeStructureCustomBankPropertyEntity)
                {
                    TreeStructureCustomBankPropertyEntity treeStructureCustomBankProperty = (TreeStructureCustomBankPropertyEntity)customBankProperty;
                    ResourcePropertyDefinition resPropDef = default(ResourcePropertyDefinition);
                    ResourceTypeEnum appl2Mask = ResourceTypeEnum.ItemResource;
                    IList<ResourcePropertyListValueDefinition> lst = CreateResourcePropertyTreeDefinitionCollection(treeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection);
                    resPropDef = new ResourcePropertyDefinition(
                        customBankProperty.CustomBankPropertyId,
                        customBankProperty.Name,
                        customBankProperty.Title,
                        customBankProperty.Publishable,
                        customBankProperty.Scorable,
                        appl2Mask,
                        ResourcePropertyDefinition.PropertyTypeEnum.Dynamic,
                        customBankProperty.GetType(),
                        ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue,
                        ResourcePropertyDefinition.ListTypeEnum.Tree,
                        lst,
                        ResourcePropertyHelpers.GetPropertyValuesFromCustomProperty)
                    { Code = customBankProperty.Code };

                    result.Add(resPropDef);
                }
            }

            return result;
        }

        private IList<ResourcePropertyListValueDefinition> CreateResourcePropertyListDefinitionCollection(EntityCollection<ListValueCustomBankPropertyEntity> listValueCustomBankPropertyEntities)
        {
            IList<ResourcePropertyListValueDefinition> result = new List<ResourcePropertyListValueDefinition>();

            foreach (ListValueCustomBankPropertyEntity listValueCustomBankPropertyEntity in listValueCustomBankPropertyEntities.Items)
            {

                if ((listValueCustomBankPropertyEntity != null))
                {
                    result.Add(new ResourcePropertyListValueDefinition(listValueCustomBankPropertyEntity.ListValueBankCustomPropertyId, listValueCustomBankPropertyEntity.Name, listValueCustomBankPropertyEntity.Title));
                }
            }

            return result;
        }

        private IList<ResourcePropertyListValueDefinition> CreateResourcePropertyTreeDefinitionCollection(EntityCollection<TreeStructurePartCustomBankPropertyEntity> treeStructurePartCustomBankPropertyEntities)
        {

            IDictionary<Guid, ResourcePropertyListValueDefinition> nodelist = new Dictionary<Guid, ResourcePropertyListValueDefinition>();

            foreach (TreeStructurePartCustomBankPropertyEntity treeStructurePartCustomBankPropertyEntity in treeStructurePartCustomBankPropertyEntities.Items)
            {
                if ((treeStructurePartCustomBankPropertyEntity != null))
                {
                    CreateTreeItem(ref nodelist, treeStructurePartCustomBankPropertyEntity);
                }
            }
            return nodelist.Values.ToList();
        }

        private ResourcePropertyListValueDefinition CreateTreeItem(ref IDictionary<Guid, ResourcePropertyListValueDefinition> visitedNodeList, TreeStructurePartCustomBankPropertyEntity treeStructurePartCustomBankPropertyEntity)
        {
            ResourcePropertyListValueDefinition result = null;
            if ((visitedNodeList.ContainsKey(treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId)))
            {
                result = visitedNodeList[treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId];
            }
            else
            {
                result = new ResourcePropertyListValueDefinition(treeStructurePartCustomBankPropertyEntity.Code, treeStructurePartCustomBankPropertyEntity.Name, treeStructurePartCustomBankPropertyEntity.Title);
                visitedNodeList.Add(treeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyId, result);
                foreach (var child_loopVariable in treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.OrderBy(n => n.VisualOrder))
                {
                    var child = child_loopVariable;
                    var childNode = CreateTreeItem(ref visitedNodeList, ConvertChildTreeStructureToTreeStructurePart(child));
                    childNode.Parent = result.Key;
                    result.Children.Add(childNode);
                }
            }
            return result;
        }

        private TreeStructurePartCustomBankPropertyEntity ConvertChildTreeStructureToTreeStructurePart(ChildTreeStructurePartCustomBankPropertyEntity childTreeStructurePartCustomBankPropertyEntity)
        {
            return childTreeStructurePartCustomBankPropertyEntity.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.FirstOrDefault(i => i.TreeStructurePartCustomBankPropertyId == childTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyId);
        }

        private IList<ResourcePropertyListValueDefinition> CreateResourcePropertyConceptDefinitionCollection(EntityCollection<ConceptStructurePartCustomBankPropertyEntity> conceptStructurePartCustomBankPropertyEntities)
        {
            IList<ResourcePropertyListValueDefinition> result = new List<ResourcePropertyListValueDefinition>();

            foreach (ConceptStructurePartCustomBankPropertyEntity conceptStructurePartCustomBankPropertyEntity in conceptStructurePartCustomBankPropertyEntities.Items)
            {

                if (conceptStructurePartCustomBankPropertyEntity != null)
                {
                    result.Add(new ResourcePropertyListValueDefinition(conceptStructurePartCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyId, conceptStructurePartCustomBankPropertyEntity.Name, conceptStructurePartCustomBankPropertyEntity.Title));
                }
            }

            return result;
        }
    }
}
