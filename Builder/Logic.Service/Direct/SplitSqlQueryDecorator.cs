using System;
using Cito.Tester.Common;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Logging;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.Logic.Service.Direct
{
    public class SplitSqlQueryDecorator : BaseResourceServiceDecorator
    {
        public const int MaxParametersPerQuery = 2000;

        public SplitSqlQueryDecorator(IResourceService decoree) : base(decoree)
        {
        }

        public override EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, ResourceRequestDTO request)
        {
            return GetResourcesByIdsWithOption(resourceIds, new ResourceEntityFactory(), request);
        }

        public override EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, IEntityFactory2 factory, ResourceRequestDTO request)
        {
            var resources = new EntityCollection(factory);
            if (resourceIds.Count == 0)
            {
                return resources;
            }
            if (resourceIds.Count < MaxParametersPerQuery)
            {
                return base.GetResourcesByIdsWithOption(resourceIds, factory, request);
            }

            Dictionary<string, string> props = new Dictionary<string, string>();
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            props.Add("callstack", st.ToString());
            LogHelper.TrackEvent(EventsToTrack.SplitSQLQueryDecoratorUsage, props);

            var splittedResourceList = resourceIds.SplitList(MaxParametersPerQuery);
            var allResources = new ConcurrentBag<EntityCollection>();
            Parallel.ForEach(splittedResourceList,
                resourceGuids =>
                {
                    allResources.Add(base.GetResourcesByIdsWithOption(resourceGuids, factory, request));
                });
            resources.AddRange(allResources.SelectMany(e => e));
            return resources;
        }

        public override EntityCollection GetResourcesByNamesWithOption(int bankId, List<string> names,
            ResourceRequestDTO request)
        {
            var resources = new EntityCollection(new ResourceEntityFactory());
            if (names.Count == 0)
            {
                return resources;
            }
            if (names.Count < MaxParametersPerQuery)
            {
                return base.GetResourcesByNamesWithOption(bankId, names, request);
            }
            var splittedNameList = names.SplitList(MaxParametersPerQuery);
            var allResources = new ConcurrentBag<EntityCollection>();
            Parallel.ForEach(splittedNameList,
                namesToCheck =>
                {
                    allResources.Add(base.GetResourcesByNamesWithOption(bankId, namesToCheck, request));
                });
            resources.AddRange(allResources.SelectMany(e => e));
            return resources;
        }
    }
}