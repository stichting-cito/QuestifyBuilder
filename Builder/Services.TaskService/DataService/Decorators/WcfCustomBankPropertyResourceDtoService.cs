using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities.Custom;
using Questify.Builder.Services.TasksService.Interfaces;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfCustomBankPropertyResourceDtoService : WcfResourceDtoServiceDecorator<CustomBankPropertyResourceDto>, ICustomBankPropertyResourceDtoRepository
    {
        private readonly IConvertEnumToLocalizedString _enumConverter;


        internal WcfCustomBankPropertyResourceDtoService(ICustomBankPropertyResourceDtoRepository decoree, IConvertEnumToLocalizedString enumConverter)
            : base(decoree)
        {
            _enumConverter = enumConverter;
        }

        public override IEnumerable<CustomBankPropertyResourceDto> GetResourcesForBank(int id)
        {
            var list = base.GetResourcesForBank(id).ToList();
            foreach (var customProperty in list)
            {
                customProperty.ApplicableToString = GetApplicableToString(customProperty.ApplicableToMask);
                customProperty.CustomPropertyTypeString = _enumConverter.ConvertToString(customProperty.CustomPropertyType);
            }
            return list;
        }

        private string GetApplicableToString(int? applicableToMask)
        {
            if (!applicableToMask.HasValue) return String.Empty;
            var returnValue = new StringBuilder();
            if ((applicableToMask & Convert.ToInt32(ResourceTypeEnum.ItemResource)) == Convert.ToInt32(ResourceTypeEnum.ItemResource))
            {
                returnValue.AppendFormat("{0}, ", _enumConverter.ConvertToString(ResourceTypeEnum.ItemResource));
            }
            if ((applicableToMask & Convert.ToInt32(ResourceTypeEnum.AssessmentTestResource)) == Convert.ToInt32(ResourceTypeEnum.AssessmentTestResource))
            {
                returnValue.AppendFormat("{0}, ", _enumConverter.ConvertToString(ResourceTypeEnum.AssessmentTestResource));
            }
            if ((applicableToMask & Convert.ToInt32(ResourceTypeEnum.GenericResource)) == Convert.ToInt32(ResourceTypeEnum.GenericResource))
            {
                returnValue.AppendFormat("{0}, ", _enumConverter.ConvertToString(ResourceTypeEnum.GenericResource));
            }
            return returnValue.ToString().TrimEnd(new char[] { ' ', ',' });
        }
    }
}