
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ItemEditor;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.ItemEditor
{
    [TestClass]
    public class CollectionParameterEvaluatorTest
    {
        [TestMethod, TestCategory("ParameterEditor")]
        public void LoadUpCollectionParameter_Has1Group()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_collectionPrmNoSubParameters.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            var groups = factory.GetGroups();
            Assert.AreEqual(1, groups.Count());
        }

        [TestMethod, TestCategory("ParameterEditor")]
        public void LoadUpCollectionParameter_Has0Groups()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_collectionPrmNoSubParameters.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            IHasGroups prm = factory.GetGroups().First().Parameters.OfType<IHasGroups>().Single();
            Assert.AreEqual(0, prm.Groups.Count());
        }

        [TestMethod, TestCategory("ParameterEditor")]
        public void LoadUpCollectionParameter_Has1Groups()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_collectionPrm1SubParameters.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            IHasGroups prm = factory.GetGroups().First().Parameters.OfType<IHasGroups>().Single();
            Assert.AreEqual(1, prm.Groups.Count());
        }

        [TestMethod, TestCategory("ParameterEditor")]
        public void LoadUpCollectionParameter_Has1GroupsWith1IntegerParameter()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_collectionPrm1SubParameters.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            int countOfIntegerParameters = factory.GetGroups().First().Parameters.OfType<IHasGroups>().Single().Groups.Single().Parameters.Count(prm => prm.Parameter is IntegerParameter);
            Assert.AreEqual(1, countOfIntegerParameters);
        }

        [TestMethod, TestCategory("ParameterEditor")]
        public void AddGroup_EndsupWith2Groups()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(_collectionPrm1SubParameters.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            var collectionParam = factory.GetGroups().First().Parameters.OfType<IHasGroups>().Single();
            collectionParam.AddGroup();
            var groups = collectionParam.Groups;
            Assert.AreEqual(2, groups.Count());
        }



        private readonly XElement _collectionPrmNoSubParameters = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">

    <collectionparameter name=""multiChoice"">
      <designersetting key=""label""><listvalues /></designersetting>
      <designersetting key=""itemcountlabel"">Aantal alternatieven<listvalues /></designersetting>
      <designersetting key=""description""><listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""minimumLength"">2<listvalues /></designersetting>
      <designersetting key=""maximumLength"">12<listvalues /></designersetting>
      <designersetting key=""subsetidentifiers"">Alphabetic<listvalues /></designersetting>
      <designersetting key=""sortkey"">4<listvalues /></designersetting>
      
      <definition id="""">
        <xhtmlparameter name=""choice"">
          <designersetting key=""label"">Keuze<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">false<listvalues /></designersetting>
        </xhtmlparameter>
        <xhtmlparameter name=""choice2"">
          <designersetting key=""label"">Keuze kolom 2<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">False<listvalues /></designersetting>
          <designersetting key=""visible"">False<listvalues /></designersetting>
        </xhtmlparameter>
      </definition>

    </collectionparameter>
  </ParameterCollection>
</ArrayOfParameterCollection>");

        private readonly XElement _collectionPrm1SubParameters = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">

    <collectionparameter name=""collection"">
      <designersetting key=""label""><listvalues /></designersetting>
      <designersetting key=""itemcountlabel"">Aantal alternatieven<listvalues /></designersetting>
      <designersetting key=""description""><listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""minimumLength"">2<listvalues /></designersetting>
      <designersetting key=""maximumLength"">12<listvalues /></designersetting>
      <designersetting key=""subsetidentifiers"">Alphabetic<listvalues /></designersetting>
      <designersetting key=""sortkey"">4<listvalues /></designersetting>
      
        <definition id="""">
     
            <integerparameter name=""int"">
                <designersetting key=""label"">Keuze<listvalues /></designersetting>
                <designersetting key=""description""><listvalues /></designersetting>
                <designersetting key=""required"">false<listvalues /></designersetting>
            </integerparameter>
     
        </definition>

        <subparameterset id=""1"">

            <integerparameter name=""int"">
                <designersetting key=""label"">Keuze<listvalues /></designersetting>
                <designersetting key=""description""><listvalues /></designersetting>
                <designersetting key=""required"">false<listvalues /></designersetting>
                42
            </integerparameter>

        </subparameterset>

    </collectionparameter>
  </ParameterCollection>
</ArrayOfParameterCollection>");

    }
}
