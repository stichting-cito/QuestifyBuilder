using Cito.Tester.ContentModel;

namespace Questify.Builder.UnitTests.Fakes
{
    interface ICreateParameter<in T>
        where T : OrderedAttribute
    {
        ParameterBase CreatePrmType(T att);
    }
}
