
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ItemEditor;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.ItemEditor
{
    [TestClass]
    public class ParameterDefaultValueTests
    {
        [TestMethod, TestCategory("ParameterEditor")]
        public void ParameterWithDefaultValue_ValueIs42()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_defaultValue.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);

            var groups = factory.GetGroups();
            var param = groups.First().Parameters.OfType<ParameterEvaluator>().Single();
            Assert.AreEqual("42", param.Parameter.ToString());
        }

        [TestMethod, TestCategory("ParameterEditor")]
        public void ParameterWithoutDefaultValue_ValueIs0()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_noDefaultValue.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);

            var groups = factory.GetGroups();
            var param = groups.First().Parameters.OfType<ParameterEvaluator>().Single();
            Assert.AreEqual("0", param.Parameter.ToString());
        }


        private readonly XElement _defaultValue = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">AAA<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">42<listvalues /></designersetting></integerparameter>
   
  </ParameterCollection>  
</ArrayOfParameterCollection>");

        private readonly XElement _noDefaultValue = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">AAA<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting></integerparameter>   
  </ParameterCollection>  
</ArrayOfParameterCollection>");
    }
}
