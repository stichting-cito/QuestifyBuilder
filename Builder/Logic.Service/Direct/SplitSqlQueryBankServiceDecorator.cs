using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.Logic.Service.Direct
{
    public class SplitSqlQueryBankServiceDecorator : BaseBankServiceDecorator
    {
        public const int MaxParametersPerQuery = 500;

        public SplitSqlQueryBankServiceDecorator(IBankService decoree) : base(decoree)
        {
        }

        public override EntityCollection GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(
            List<Guid> customPropertyIds, List<Guid> resourceIds, bool onlyWithEmptyDisplayValue)
        {
            var propertyValues = new EntityCollection(new BankEntityFactory());
            if (resourceIds.Count == 0)
            {
                return propertyValues;
            }
            if (resourceIds.Count < MaxParametersPerQuery)
            {
                return base.GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(customPropertyIds, resourceIds, onlyWithEmptyDisplayValue);
            }
            var splittedResourceList = resourceIds.SplitList(MaxParametersPerQuery);
            var allPropertyValues = new ConcurrentBag<EntityCollection>();
            Parallel.ForEach(splittedResourceList,
                resourceGuids =>
                {
                    allPropertyValues.Add(
                        base.GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(customPropertyIds,
                            resourceGuids, onlyWithEmptyDisplayValue));
                });
            propertyValues.AddRange(allPropertyValues.SelectMany(e => e));
            return propertyValues;
        }

    }
}