using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Logic.Service.Model.Entities.Custom;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenResourceProxy<T> where T : ResourceDto
    {


        public T GetResource(T resource, ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            resource.ResourceId = resourceEntity.ResourceId;
            resource.BankId = resourceEntity.BankId;
            resource.BankName = resourceEntity.BankName;
            resource.Name = resourceEntity.Name;
            resource.Title = resourceEntity.Title;
            resource.DependentResourceIds = resourceEntity.DependentResourceCollection.Select(x => x.DependentResourceId).ToList();
            resource.ReferencedResourceIds = resourceEntity.ReferencedResourceCollection.Select(x => x.ResourceId).ToList();
            resource.ReferenceCount = resourceEntity.ReferenceCount;
            resource.CreatedBy = resourceEntity.CreatedBy;
            resource.CreationDate = resourceEntity.CreationDate;
            resource.CreatedByFullName = resourceEntity.CreatedByFullName;
            resource.ModifiedBy = resourceEntity.ModifiedBy;
            resource.ModifiedDate = resourceEntity.ModifiedDate;
            resource.ModifiedByFullName = resourceEntity.ModifiedByFullName;
            resource.Version = resourceEntity.Version;
            resource.StateId = resourceEntity.StateId;
            resource.StateName = resourceEntity.StateName;
            resource.IsSelectable = IsSelectable(resourceEntity);
            resource.CustomPropertyDisplayValues = GetCustomPropertiesForResource(resourceEntity.CustomBankPropertyValueCollection, customProperties.ToList());
            if (resourceEntity.HiddenResourceCollection != null)
            {
                resource.SetToInvisibleAtBankIds = resourceEntity.HiddenResourceCollection.Select(x => x.BankId).ToList();
            }
            return resource;
        }




        private IList<CustomPropertyDisplayValueDto> GetCustomPropertiesForResource(IEnumerable<CustomBankPropertyValueEntity> llblGenCustomBankProperties, IList<CustomBankPropertyDto> customPropertiesDto)
        {
            var customPropertyList = new List<CustomPropertyDisplayValueDto>();

            if (llblGenCustomBankProperties != null)
            {
                var properties = from cbp in llblGenCustomBankProperties
                                 join cpd in customPropertiesDto on cbp.CustomBankPropertyId equals cpd.CustomBankPropertyId
                                 select new { cbp.CustomBankPropertyId, bankId = cpd.BankId, cbp.DisplayValue };

                foreach (var property in properties)
                {
                    customPropertyList.Add(new CustomPropertyDisplayValueDto()
                    {
                        CustomPropertyId = property.CustomBankPropertyId,
                        BankId = property.bankId,
                        DisplayValue = property.DisplayValue
                    });
                }
            }
            return customPropertyList;
        }




        private bool IsSelectable(ResourceEntity resource)
        {
            if (resource.State != null && resource.State.StateActionCollection != null)
                return resource.State.StateActionCollection.Where(actionTargets => actionTargets.Target == "selectable")
                                                .All(actionTargets => actionTargets.Action == null || actionTargets.Action.Name != "prohibit");
            return true;
        }
    }
}
