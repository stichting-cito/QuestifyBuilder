using System;
using System.Globalization;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UnitTests.Fakes
{
    class CreateParameterByType : ICreateParameter<AddParameterAttribute>
    {
        public ParameterBase CreatePrmType(AddParameterAttribute att)
        {
            var type = att.TypeOfScoreAttribute;
            if (!typeof(ParameterBase).IsAssignableFrom(type))
            {
                throw new Exception();
            }

            var ret = (ParameterBase)Activator.CreateInstance(type);

            var cp = ret as ChoiceScoringParameter;
            if (cp != null)
            {
                cp.MaxChoices = 1;
            }

            if (!typeof(ScoringParameter).IsAssignableFrom(type))
            {
                return ret;
            }

            var sp = (ScoringParameter)ret;
            sp.FindingOverride = att.FindingOverride;
            sp.InlineId = att.InlineID;
            sp.ControllerId = att.ControllerID;
            sp.Name = att.Name;
            sp.Value = new ParameterSetCollection();

            if (string.IsNullOrEmpty(att.SubParameterIds))
            {
                return ret;
            }

            foreach (var id in att.SubParameterIds.ToCharArray())
            {
                sp.Value.Add(new ParameterCollection() { Id = id.ToString(CultureInfo.InvariantCulture) });
            }

            return ret;
        }
    }
}
