using Cito.Tester.ContentModel;

namespace Questify.Builder.UnitTests.Fakes
{
    class CreateAspectScoringParameter : ICreateParameter<AddAspectScoringParameterAttribute>
    {
        public ParameterBase CreatePrmType(AddAspectScoringParameterAttribute att)
        {
            return new AspectScoringParameter() { AutoScoringOffPrm = att.AutoScoringOffPrm, FindingOverride = att.FindingOverride, Name = att.Name, ControllerId = att.ControllerID };
        }
    }
}
