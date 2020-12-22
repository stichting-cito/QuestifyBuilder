using Cito.Tester.Common;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UnitTests.Fakes
{
    class CreateXhtmlParameter : ICreateParameter<AddXhtmlParameterAttribute>
    {
        public ParameterBase CreatePrmType(AddXhtmlParameterAttribute att)
        {
            return (XHtmlParameter)SerializeHelper.XmlDeserializeFromString(att.XhtmlParameter, typeof(XHtmlParameter));
        }
    }
}
