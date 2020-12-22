
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ItemEditor;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.ItemEditor
{
    [TestClass]
    public class ParameterEvaluatorVisibilityTests
    {
        [TestMethod, TestCategory("ParameterEditor")]
        public void GetGroup_AllParametersVisible_ShouldEquals2()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_simpleAllParansVisible.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            var groups = factory.GetGroups();
            Assert.AreEqual(2, groups.Count());
        }

        [TestMethod, TestCategory("ParameterEditor")]
        public void GetGroup_GroupBBB_has2ParamsButWithCasingError_ShouldHave2Groups()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_simpleGroups_CasingError.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            var groups = factory.GetGroups();
            Assert.AreEqual(2, groups.Count());
        }

        [TestMethod, TestCategory("ParameterEditor")]
        public void GetGroup_1ParametersVisible_ShouldEquals1()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_simpleBBBParansVisible.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            var groups = factory.GetGroups();
            Assert.AreEqual(1, groups.Count());
        }



        private readonly XElement _simpleAllParansVisible = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">AAA<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>42</integerparameter>

    <booleanparameter name=""showGroup"">
      <designersetting key=""group"">BBB<listvalues /></designersetting>      
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
        True
    </booleanparameter>
   
  </ParameterCollection>  
</ArrayOfParameterCollection>");



        private readonly XElement _simpleBBBParansVisible = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">AAA<listvalues /></designersetting>
      <designersetting key=""visible"">false<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>42</integerparameter>

    <booleanparameter name=""showGroup"">
      <designersetting key=""group"">BBB<listvalues /></designersetting>      
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
        True
    </booleanparameter>
   
  </ParameterCollection>  
</ArrayOfParameterCollection>");



        private readonly XElement _simpleGroups_CasingError = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">AAA<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>42</integerparameter>

    <booleanparameter name=""showGroup"">
      <designersetting key=""group"">BBB<listvalues /></designersetting>      
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
        True
    </booleanparameter>

 <booleanparameter name=""showGroup2"">
      <designersetting key=""group"">BbB<listvalues /></designersetting>      
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
        True
    </booleanparameter>
   
  </ParameterCollection>  
</ArrayOfParameterCollection>");


    }
}
