
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ItemEditor;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.ItemEditor
{
    [TestClass]
    public class ParameterGroupConditionalEnabledEvaluatorTests
    {

        [TestMethod, TestCategory("ParameterEditor")]  
        public void GetGroups_WithTemplateAllGroupsShouldBeVisible_Eq2()
        {
            //Arrange
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_simpleTemplate_AllGroupsVisible.ToString()); /*DeSerializes WITH designersettings*/
            var factory = new ParameterViewFactory(parameterSetCollection);
            //Act
            var groups = factory.GetGroups().ToList();
            //Assert
            Assert.AreEqual(2,groups.Count());
            Assert.AreEqual(2, groups.Count(g => g.IsVisible));
        }

        [TestMethod, TestCategory("ParameterEditor")]  
        public void GetGroups_WithTemplate1GroupShouldBeVisible_Eq1()
        {
            //Arrange
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_simpleTemplate_1GroupVisible.ToString()); /*DeSerializes WITH designersettings*/
            var factory = new ParameterViewFactory(parameterSetCollection);
            //Act
            var groups = factory.GetGroups().ToList();
            //Assert
            Assert.AreEqual(2, groups.Count(),"Two groups,.. just 1 is not visible.");
            Assert.AreEqual(1, groups.Count(g => g.IsVisible));
        }

        #region Data

        /* Groups : Master Control ; Switchee
         */
        private readonly XElement _simpleTemplate_AllGroupsVisible = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">Switchee<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>42</integerparameter>

    <booleanparameter name=""showGroup"">
      <designersetting key=""groupConditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledSwitch"">Switchee<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Master Control<listvalues /></designersetting>
      <designersetting key=""description"">This is the Master Control<listvalues /></designersetting>
      <designersetting key=""group"">Master Control<listvalues /></designersetting>
      <designersetting key=""sortkey"">0<listvalues /></designersetting>      
      <designersetting key=""required"">true<listvalues /></designersetting>
        True
    </booleanparameter>
   
  </ParameterCollection>  
</ArrayOfParameterCollection>");

        /* Groups : Master Control ; Switchee
       */
        private readonly XElement _simpleTemplate_1GroupVisible = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">Switchee<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>42</integerparameter>

    <booleanparameter name=""showGroup"">
      <designersetting key=""groupConditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledSwitch"">Switchee<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Master Control<listvalues /></designersetting>
      <designersetting key=""description"">This is the Master Control<listvalues /></designersetting>
      <designersetting key=""group"">Master Control<listvalues /></designersetting>
      <designersetting key=""sortkey"">0<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
        False
    </booleanparameter>
   
  </ParameterCollection>  
</ArrayOfParameterCollection>");

        /* Groups : Master Control ; Switchee
*/
        private readonly XElement _simpleTemplate_1GroupVisible2Switches = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
        
    <integerparameter name=""someNumber"">
      <designersetting key=""label"">SomeNumber<listvalues /></designersetting>
      <designersetting key=""description"">Some number<listvalues /></designersetting>
      <designersetting key=""group"">Switchee<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>42</integerparameter>

    <booleanparameter name=""showGroup"">
      <designersetting key=""groupConditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledSwitch"">Switchee<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Master Control<listvalues /></designersetting>
      <designersetting key=""description"">This is the Master Control<listvalues /></designersetting>
      <designersetting key=""group"">Master Control<listvalues /></designersetting>
      <designersetting key=""sortkey"">0<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
        False
    </booleanparameter>
   
    <booleanparameter name=""showGroup2"">
      <designersetting key=""groupConditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledSwitch"">Switchee<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Master Control<listvalues /></designersetting>
      <designersetting key=""description"">This is the Master Control<listvalues /></designersetting>
      <designersetting key=""group"">Master Control<listvalues /></designersetting>
      <designersetting key=""sortkey"">0<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
        False
    </booleanparameter>
  </ParameterCollection>  
</ArrayOfParameterCollection>");

        #endregion
    }
}
