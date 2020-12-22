using System;
using System.Collections.Generic;
using System.Linq;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UnitTests.Fakes
{
    class HandleFakeParameterSet
    {
        private readonly ParameterSetCollection _parameterSetCollection;
        private readonly Dictionary<Type, Func<OrderedAttribute, ParameterBase>> _addParameterDictionary;

        public HandleFakeParameterSet()
        {
            _addParameterDictionary = new Dictionary<Type, Func<OrderedAttribute, ParameterBase>>
           {
               {
                   typeof (AddXhtmlParameterAttribute),
                   (attribute) => new CreateXhtmlParameter().CreatePrmType((AddXhtmlParameterAttribute) attribute)
               },
               {
                   typeof (AddAspectScoringParameterAttribute),
                   (attribute) => new CreateAspectScoringParameter().CreatePrmType((AddAspectScoringParameterAttribute) attribute)
               },
               {
                   typeof (AddParameterAttribute),
                   (attribute) => new CreateParameterByType().CreatePrmType((AddParameterAttribute) attribute)
               }
           };
            _parameterSetCollection = new ParameterSetCollection();
        }

        internal void DealWith(OrderedAttribute att)
        {
            string prmCollId = att.PrmCollToAddToId == null ? string.Empty : att.PrmCollToAddToId;
            if (_parameterSetCollection.Count == 0 || !_parameterSetCollection.Any(pc => pc.Id.Equals(prmCollId, StringComparison.InvariantCultureIgnoreCase)))
            {
                _parameterSetCollection.Add(new ParameterCollection() { Id = prmCollId, IsDynamicCollection = att.PrmCollToAddToIsDynCollection });
            }
            _parameterSetCollection.First(pc => pc.Id.Equals(prmCollId, StringComparison.InvariantCultureIgnoreCase)).InnerParameters.Add(CreatePrmType(att));
        }

        internal void GetResult(Cinch.DataWrapper<ParameterSetCollection> param)
        {
            param.DataValue = _parameterSetCollection;
        }

        private ParameterBase CreatePrmType(OrderedAttribute att)
        {
            return _addParameterDictionary[att.GetType()].Invoke(att);
        }
    }
}
