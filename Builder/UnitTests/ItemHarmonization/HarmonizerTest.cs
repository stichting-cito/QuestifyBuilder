
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.Enums;
using Questify.Builder.Logic.ItemHarmonization.Factory;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UnitTests.Framework;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.ItemHarmonization
{
    [TestClass]
    public class HarmonizerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            FakeDal.Init();
            FakeDal.CanSaveResources();
        }

        [TestCleanup]
        public void Cleanup()
        {
            FakeDal.Deinit();
        }

        [TestMethod]
        public void HarmonizeBaseTemplateAddParameterTest()
        {
            var itemResource = GetItemResource();
            var item = CreateBasicAssessmentItem();
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateBasicParameterSet();
            var dict =
                new ConcurrentDictionary<string, ParameterSetCollection>(new Dictionary<string, ParameterSetCollection>(StringComparer.OrdinalIgnoreCase)
                {
                    {"template", parameterSetCollection}
                }, (StringComparer.OrdinalIgnoreCase));
            var harmonizer = new Harmonizer(HarmonizeOptions.Base);
            harmonizer.Harmonize(dict, itemResource);
            var parameter = itemResource.GetAssessmentItem().Parameters.GetParameters();
            var addedParameter = parameter.FirstOrDefault(p => p.Name.Equals("parameter3", StringComparison.OrdinalIgnoreCase));
            var parameterWithValue = parameter.FirstOrDefault(p => p.Name.Equals("parameter2", StringComparison.OrdinalIgnoreCase));
            Assert.AreEqual(parameter.Count, 3);
            Assert.IsTrue(parameter.Any(p => p.Name.Equals("parameter1", StringComparison.OrdinalIgnoreCase)));
            Assert.IsNotNull(parameterWithValue);
            Assert.AreEqual(parameterWithValue.ToString(), "SomeValue");
            Assert.IsNotNull(addedParameter);
            Assert.AreEqual(Boolean.Parse(addedParameter.ToString()), true);
        }

        [TestMethod]
        public void HarmonizeWithDynamicParametersTestNotHarmonised()
        {
            var itemResource = GetItemResource();
            var item = CreateAssessmentItemWithDynamicProperties();
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateParameterSetWithDynamicProperties();
            var dict = new ConcurrentDictionary<string, ParameterSetCollection>(
        new Dictionary<string, ParameterSetCollection>
        {
                        {"Cito.CTE.CustomInteraction.SC", parameterSetCollection}
        }, (StringComparer.OrdinalIgnoreCase));
            var harmonizer = new Harmonizer(HarmonizeOptions.Base);
            Assert.IsFalse(harmonizer.Harmonize(dict, itemResource));
        }

        [TestMethod]
        public void HarmonizeWithDynamicParametersTestShouldHarmonise()
        {
            var itemResource = GetItemResource();
            var item = CreateAssessmentItemWithDynamicProperties();
            item.Parameters[0].InnerParameters.RemoveAt(0);
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateParameterSetWithDynamicProperties();
            var dict = new ConcurrentDictionary<string, ParameterSetCollection>(
        new Dictionary<string, ParameterSetCollection>
        {
                        {"Cito.CTE.CustomInteraction.SC", parameterSetCollection}
        }, (StringComparer.OrdinalIgnoreCase));
            var harmonizer = new Harmonizer(HarmonizeOptions.Base);
            Assert.IsTrue(harmonizer.Harmonize(dict, itemResource));
        }


        [TestMethod]
        public void HarmonizeBaseTemplateRemoveParameterTest()
        {
            var itemResource = GetItemResource();
            var item = CreateBasicAssessmentItem();
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateBasicParameterSet();
            var parameterToRemove = parameterSetCollection.GetParameter("parameter1", "parameterSet1");
            parameterSetCollection.GetParamCollectionByControlId("parameterSet1").InnerParameters.Remove(parameterToRemove);
            var dict =
                new ConcurrentDictionary<string, ParameterSetCollection>(
                    new Dictionary<string, ParameterSetCollection>
                    {
                        {"template", parameterSetCollection}
                    }, (StringComparer.OrdinalIgnoreCase));
            var harmonizer = new Harmonizer(HarmonizeOptions.Base);
            harmonizer.Harmonize(dict, itemResource);
            var parameter = itemResource.GetAssessmentItem().Parameters.GetParameters();
            Assert.AreEqual(parameter.Count, 2);
            Assert.IsTrue(parameter.Any(p => p.Name.Equals("parameter2", StringComparison.OrdinalIgnoreCase)));
            Assert.IsTrue(parameter.Any(p => p.Name.Equals("parameter3", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void HarmonizeBaseTemplateAddParameterInCollectionTest()
        {
            var itemResource = GetItemResource();
            var item = CreateAssessmentItemWithCollectionParameter();
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateParameterSetWithCollectionParameter();

            var dict = new ConcurrentDictionary<string, ParameterSetCollection>(new Dictionary<string, ParameterSetCollection> { { "template2", parameterSetCollection } }, StringComparer.OrdinalIgnoreCase);
            var harmonizer = new Harmonizer(HarmonizeOptions.Base);
            harmonizer.Harmonize(dict, itemResource);
            var parameter = itemResource.GetAssessmentItem().Parameters.GetParameters();
            Assert.AreEqual(parameter.Count, 3);
            Assert.IsTrue(parameter.Any(p => p.Name.Equals("parameter1", StringComparison.OrdinalIgnoreCase)));
            Assert.IsTrue(parameter.OfType<CollectionParameter>().First().BluePrint.InnerParameters.Count() == 2);
        }

        [TestMethod]
        public void HarmonizeInlineTemplateAddParameter()
        {
            var itemResource = GetItemResource();
            var item = CreatAssessmentItemWithInlineElement();
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateParameterSetForInlineTemplate();
            var dict = new ConcurrentDictionary<string, ParameterSetCollection>(new Dictionary<string, ParameterSetCollection> { { "inlinetemplate", parameterSetCollection } });
            var harmonizer = new Harmonizer(HarmonizeOptions.Inline);
            harmonizer.Harmonize(dict, itemResource);
            var inlineParameter = itemResource.GetAssessmentItem().GetInlineElements().FirstOrDefault();
            Assert.IsNotNull(inlineParameter);
            var parameterWithValue = inlineParameter.Parameters.FlattenParameters().FirstOrDefault(p => p.Name.Equals("source", StringComparison.OrdinalIgnoreCase));
            Assert.AreEqual(inlineParameter.Parameters.FlattenParameters().Count(), 2);
            Assert.IsNotNull(parameterWithValue);
            Assert.AreEqual(parameterWithValue.ToString(), "someImage.png");
        }

        [TestMethod]
        public void HarmonizeInlineTemplateInAspect()
        {
            var itemResource = GetItemResource();
            var item = CreatAssessmentItemWithAspect();
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateParameterSetForInlineTemplate();
            var dict =
                new ConcurrentDictionary<string, ParameterSetCollection>(
                    new Dictionary<string, ParameterSetCollection> { { "inlinetemplate", parameterSetCollection } });
            var harmonizer = new Harmonizer(HarmonizeOptions.Inline);
            harmonizer.Harmonize(dict, itemResource);
            var inlineParameter = itemResource.GetAssessmentItem().GetInlineElements().FirstOrDefault();
            Assert.IsNotNull(inlineParameter);
            var parameterWithValue = inlineParameter.Parameters.FlattenParameters().FirstOrDefault(p => p.Name.Equals("source", StringComparison.OrdinalIgnoreCase));
            Assert.AreEqual(inlineParameter.Parameters.FlattenParameters().Count(), 2);
            Assert.IsNotNull(parameterWithValue);
            Assert.AreEqual(parameterWithValue.ToString(), "someImage.png");
            Assert.IsTrue(itemResource.GetAssessmentItem().Solution.AspectReferenceSetCollection[0].Items[0].Description.Contains("border"));
        }


        [TestMethod]
        public void HarmonizeScoringsParamaterAttributeReference()
        {
            var itemResource = GetItemResource();
            var item = CreateAssessmentItemWithScoringParameter();
            itemResource.SetAssessmentItem(item);
            var parameterSetCollection = CreateParameterSetForScoringsParameterAttributeReference();
            var dict = new ConcurrentDictionary<string, ParameterSetCollection>(new Dictionary<string, ParameterSetCollection> { { "Template2", parameterSetCollection } });
            var harmonizer = new Harmonizer(HarmonizeOptions.All);
            harmonizer.Harmonize(dict, itemResource);
            var mcScoringParameter = itemResource.GetAssessmentItem().Parameters.FlattenParameters().OfType<MultiChoiceScoringParameter>().FirstOrDefault();
            Assert.IsNotNull(mcScoringParameter);
            Assert.AreEqual(mcScoringParameter.MinChoices, 2);
        }


        [TestMethod, TestCategory("Logic")]
        public void HarmonizeMultipleItems()
        {
            CreateItems(ItemLayoutTemplate, ControlTemplate, item1DataXml, item2DataXml);
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            var item1 = ResourceFactory.Instance.GetResourceByNameWithOption(0, "item1", request) as ItemResourceEntity;
            var item2 = ResourceFactory.Instance.GetResourceByNameWithOption(0, "item2", request) as ItemResourceEntity;

            var itemsHarmonized = 0;

            var harmonizer = new Harmonizer(HarmonizeOptions.All);
            itemsHarmonized += harmonizer.Harmonize(item2) ? 1 : 0; itemsHarmonized += harmonizer.Harmonize(item1) ? 1 : 0;
            Assert.AreEqual(2, itemsHarmonized, "Expected both items to be harmonized");
            var newItem1 = ResourceFactory.Instance.GetResourceByNameWithOption(0, "item1", request);
            if (newItem1.ResourceData == null || newItem1.ResourceData.BinData.Length == 0)
                ResourceFactory.Instance.GetResourceData(newItem1);

            var newItem2 = ResourceFactory.Instance.GetResourceByNameWithOption(0, "item2", request);
            if (newItem2.ResourceData == null || newItem2.ResourceData.BinData.Length == 0)
                ResourceFactory.Instance.GetResourceData(newItem2);

            var item1Xml = XDocument.Parse(System.Text.Encoding.UTF8.GetString(newItem1.ResourceData.BinData));
            var item2Xml = XDocument.Parse(System.Text.Encoding.UTF8.GetString(newItem2.ResourceData.BinData));

            Assert.IsTrue(UnitTestHelper.AreSame(item1Xml, new XDocument(expectedItem1))); Assert.IsTrue(UnitTestHelper.AreSame(item2Xml, new XDocument(expectedItem2)));
        }

        [TestMethod, TestCategory("Logic")]
        public void HarmonizeItemWithDuplicateParameterSet()
        {
            CreateItem(ItemLayoutTemplate, ControlTemplate, item3DataXml, "item2");
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            var item = ResourceFactory.Instance.GetResourceByNameWithOption(0, "item2", request) as ItemResourceEntity;
            var harmonizer = new Harmonizer(HarmonizeOptions.All);

            var result = harmonizer.Harmonize(item);

            Assert.AreEqual(true, result, "Expected item to be harmonized, but it is not.");
            var newItem = ResourceFactory.Instance.GetResourceByNameWithOption(0, "item2", request);
            if (newItem.ResourceData == null || newItem.ResourceData.BinData.Length == 0)
                ResourceFactory.Instance.GetResourceData(newItem);
            var newItemXml = XElement.Parse(System.Text.Encoding.UTF8.GetString(newItem.ResourceData.BinData));
            Assert.IsTrue(UnitTestHelper.AreSame(new XDocument(newItemXml), new XDocument(expectedItem2)));
        }


        private ItemResourceEntity GetItemResource()
        {
            var item = new ItemResourceEntity { ResourceId = Guid.NewGuid(), Name = "Item01", Title = "Item 01", ResourceData = new ResourceDataEntity() };
            return item;
        }

        private AssessmentItem CreatAssessmentItemWithAspect()
        {
            return (AssessmentItem)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ItemWithAspect, typeof(AssessmentItem));
        }

        private AssessmentItem CreatAssessmentItemWithInlineElement()
        {
            return (AssessmentItem)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ItemInline1, typeof(AssessmentItem));
        }

        private AssessmentItem CreateBasicAssessmentItem()
        {
            return (AssessmentItem)SerializeHelper.XmlDeserializeFromString(Properties.Resources.Item1, typeof(AssessmentItem));
        }

        private AssessmentItem CreateAssessmentItemWithDynamicProperties()
        {
            return (AssessmentItem)SerializeHelper.XmlDeserializeFromString(item4DataXml.ToString(), typeof(AssessmentItem));
        }

        private ParameterSetCollection CreateParameterSetWithDynamicProperties()
        {
            return (ParameterSetCollection)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ParameterSet3, typeof(ParameterSetCollection));
        }

        private ParameterSetCollection CreateBasicParameterSet()
        {
            return (ParameterSetCollection)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ParameterSet1, typeof(ParameterSetCollection));
        }

        private AssessmentItem CreateAssessmentItemWithScoringParameter()
        {
            return (AssessmentItem)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ItemWithScoringAttrRefParameter, typeof(AssessmentItem));
        }

        private AssessmentItem CreateAssessmentItemWithCollectionParameter()
        {
            return (AssessmentItem)SerializeHelper.XmlDeserializeFromString(Properties.Resources.Item2, typeof(AssessmentItem));
        }

        private ParameterSetCollection CreateParameterSetWithCollectionParameter()
        {
            return (ParameterSetCollection)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ParameterSet2, typeof(ParameterSetCollection));
        }

        private ParameterSetCollection CreateParameterSetForInlineTemplate()
        {
            return (ParameterSetCollection)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ParameterSetInline1, typeof(ParameterSetCollection));
        }

        private ParameterSetCollection CreateParameterSetForScoringsParameterAttributeReference()
        {
            var paramColl = (ParameterSetCollection)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ParameterSetAttributeReference, typeof(ParameterSetCollection));
            var mcParameter = paramColl.FlattenParameters().OfType<MultiChoiceScoringParameter>().FirstOrDefault();
            mcParameter.AttributeReferences.Add(new AttributeReference
            {
                Name = "minChoices",
                Value = "referencedParameternew"
            });
            return paramColl;
        }



        private void CreateItem(XElement itemLayoutTemplateXml, XElement controlTemplateXml, XElement itemXml, string itemIdentifier)
        {

            var actionsAfter = FakeDal.Add
                .ControlTemplate("InlineGapMatchLayoutTemplate", c => SetData(InlineGapMatchLayoutTemplate, c))
                .IsUsedBy
                .ControlTemplate("min.gapmatchcontroltemplate", c => SetData(controlTemplateXml, c))
                .IsUsedBy
                .ItemTemplate("ilt.gapmatch", ilt => SetData(itemLayoutTemplateXml, ilt))
                .IsUsedBy.Item(itemIdentifier, itm => SetData(itemXml, itm));
        }

        private void CreateItems(XElement itemLayoutTemplateXml, XElement controlTemplateXml, params XElement[] itemsXmls)
        {

            var actionsAfter = FakeDal.Add
                .ControlTemplate("InlineGapMatchLayoutTemplate", c => SetData(InlineGapMatchLayoutTemplate, c))
                .IsUsedBy
                .ControlTemplate("min.gapmatchcontroltemplate", c => SetData(controlTemplateXml, c))
                .IsUsedBy
                .ItemTemplate("ilt.gapmatch", ilt => SetData(itemLayoutTemplateXml, ilt));

            var cntr = 1;
            foreach (var itemXml in itemsXmls)
            {
                actionsAfter.IsUsedBy.Item(string.Format("item{0}", cntr++), itm => SetData(itemXml, itm));
            }
        }

        private void SetData(XElement data, ResourceEntity resourceEntity)
        {
            resourceEntity.ResourceId = Guid.NewGuid();
            resourceEntity.ResourceData = new ResourceDataEntity { BinData = System.Text.Encoding.UTF8.GetBytes(data.ToString()) };
            var itemResouce = resourceEntity as ItemResourceEntity;
            if (itemResouce != null)
            {
                var assessmentItem = itemResouce.GetAssessmentItem();
                resourceEntity.Name = assessmentItem.Identifier;
                resourceEntity.Title = assessmentItem.Title;
            }
            resourceEntity.IsNew = false;
        }

        private readonly XElement item1DataXml = XElement.Parse(@"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""item1"" title=""Teenager forum"" layoutTemplateSrc=""ilt.gapmatch"">
  <solution>
    <keyFindings>
      <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
        <keyFact id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac"" occur=""1"">
            <stringValue>
              <typedValue>D</typedValue>
            </stringValue>
          </keyValue>
        </keyFact>
        <keyFact id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50"" occur=""1"">
            <stringValue>
              <typedValue>A</typedValue>
            </stringValue>
          </keyValue>
        </keyFact>
        <keyFact id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8"" occur=""1"">
            <stringValue>
              <typedValue>B</typedValue>
            </stringValue>
          </keyValue>
        </keyFact>
      </keyFinding>
    </keyFindings>
    <conceptFindings>
      <conceptFinding id=""gapMatchController"" scoringMethod=""None"">
        <conceptFact id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac"" occur=""1"">
            <stringValue>
              <typedValue>D</typedValue>
            </stringValue>
          </conceptValue>
          <concepts>
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50"" occur=""1"">
            <stringValue>
              <typedValue>A</typedValue>
            </stringValue>
          </conceptValue>
          <concepts>
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8"" occur=""1"">
            <stringValue>
              <typedValue>B</typedValue>
            </stringValue>
          </conceptValue>
          <concepts>
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac[*]"" occur=""1"">
            <catchAllValue />
          </conceptValue>
          <concepts>
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8[*]"" occur=""1"">
            <catchAllValue />
          </conceptValue>
          <concepts>
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50[*]"" occur=""1"">
            <catchAllValue />
          </conceptValue>
          <concepts>
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
      </conceptFinding>
    </conceptFindings>
    <aspectReferences />
    <ItemScoreTranslationTable>
      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
    </ItemScoreTranslationTable>
  </solution>
  <parameters>
    <parameterSet id=""entireItem"">
      <listedparameter name=""itemStyle"">Default</listedparameter>
      <booleanparameter name=""dualColumnLayout"">True</booleanparameter>
      <booleanparameter name=""showCalculatorButton"" />
      <booleanparameter name=""displayVerklankingOnTheRight"">True</booleanparameter>
      <collectionparameter name=""numberOfAudioContentItems"">
        <definition id="""">
          <resourceparameter name=""audiocontent"" />
          <xhtmlparameter name=""description"" />
        </definition>
      </collectionparameter>
      <texttospeechparameter name=""verklankingLinks"" />
      <texttospeechparameter name=""verklankingRechts"" />
      <xhtmlparameter name=""leftBody"" />
      <xhtmlresourceparameter name=""leftSource"" />
      <integerparameter name=""sourceHeight"">200</integerparameter>
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span lang=""EN-GB"" id=""c1-id-12"">
          </span>
        </p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemQuestion"">
        <p id=""c1-id-11"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">Drag and drop</p>
        <p id=""c1-id-12"" xmlns=""http://www.w3.org/1999/xhtml"">
          <strong id=""c1-id-13"">Vul de tekst aan door de woorden naar de juiste plek te slepen.</strong>
        </p>
        <p id=""c1-id-14"" xmlns=""http://www.w3.org/1999/xhtml"">Let op! Er blijft één antwoord over.</p>
      </xhtmlparameter>
      <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
        <subparameterset id=""A"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bing</gapTextParameter>
        </subparameterset>
        <subparameterset id=""B"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bang</gapTextParameter>
        </subparameterset>
        <subparameterset id=""C"">
          <gapTextParameter name=""gapText"" matchMax=""1"">whopper</gapTextParameter>
        </subparameterset>
        <subparameterset id=""D"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bada</gapTextParameter>
        </subparameterset>
        <definition id="""">
          <gapTextParameter name=""gapText"" matchMax=""1"" />
        </definition>
        <xhtmlParameter name=""itemInlineInput"">
          <p id=""c1-id-11"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">Hi ...</p>
          <p id=""c1-id-12"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">I have ...<cito:InlineElement id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester""><cito:parameters><cito:parameterSet id=""entireItem""><cito:plaintextparameter name=""inlineGapMatchId"">I4371df73-3d68-49c2-81ed-4ceb12ca6aac</cito:plaintextparameter><cito:plaintextparameter name=""inlineGapMatchLabel"">1</cito:plaintextparameter><cito:booleanparameter name=""editSize"">True</cito:booleanparameter><cito:integerparameter name=""width"" /><cito:integerparameter name=""height"" /></cito:parameterSet></cito:parameters></cito:InlineElement> I had ...<cito:InlineElement id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester""><cito:parameters><cito:parameterSet id=""entireItem""><cito:plaintextparameter name=""inlineGapMatchId"">Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8</cito:plaintextparameter><cito:plaintextparameter name=""inlineGapMatchLabel"">2</cito:plaintextparameter><cito:booleanparameter name=""editSize"">True</cito:booleanparameter><cito:integerparameter name=""width"" /><cito:integerparameter name=""height"" /></cito:parameterSet></cito:parameters></cito:InlineElement> he told ...<cito:InlineElement id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester""><cito:parameters><cito:parameterSet id=""entireItem""><cito:plaintextparameter name=""inlineGapMatchId"">I51b6db97-d93a-4b4a-97d2-f2e508f30e50</cito:plaintextparameter><cito:plaintextparameter name=""inlineGapMatchLabel"">3</cito:plaintextparameter><cito:booleanparameter name=""editSize"">True</cito:booleanparameter><cito:integerparameter name=""width"" /><cito:integerparameter name=""height"" /></cito:parameterSet></cito:parameters></cito:InlineElement> she has ...<br id=""c1-id-13"" /><br id=""c1-id-14"" />Please help ...</p>
          <p id=""c1-id-15"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">Love,</p>
          <p id=""c1-id-16"" xmlns=""http://www.w3.org/1999/xhtml"">Ani125</p>
        </xhtmlParameter>
      </gapMatchScoringParameter>
      <xhtmlparameter name=""itemInlineInput"" />
      <xhtmlparameter name=""itemGeneral"" />
      <collectionparameter name=""choices"">
        <definition id="""">
          <plaintextparameter name=""choice"" />
          <integerparameter name=""nrOfConnections"" />
        </definition>
      </collectionparameter>
    </parameterSet>
  </parameters>
</assessmentItem>");


        private readonly XElement item2DataXml = XElement.Parse(@"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""item2"" title=""Categories"" layoutTemplateSrc=""ilt.gapmatch"">
  <solution>
    <keyFindings>
      <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
        <keyFactSet>
          <keyFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
        </keyFactSet>
        <keyFactSet>
          <keyFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
        </keyFactSet>
      </keyFinding>
    </keyFindings>
    <conceptFindings>
      <conceptFinding id=""gapMatchController"" scoringMethod=""None"">
        <conceptFactSet>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
        <conceptFactSet>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
        <conceptFactSet>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
      </conceptFinding>
    </conceptFindings>
    <aspectReferences />
    <ItemScoreTranslationTable>
      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
    </ItemScoreTranslationTable>
  </solution>
  <parameters>
    <parameterSet id=""entireItem"">
      <listedparameter name=""itemStyle"">Default</listedparameter>
      <booleanparameter name=""dualColumnLayout"">True</booleanparameter>
      <booleanparameter name=""showCalculatorButton"" />
      <booleanparameter name=""displayVerklankingOnTheRight"">True</booleanparameter>
      <collectionparameter name=""numberOfAudioContentItems"">
        <definition id="""">
          <resourceparameter name=""audiocontent"" />
          <xhtmlparameter name=""description"" />
        </definition>
      </collectionparameter>
      <texttospeechparameter name=""verklankingLinks"" />
      <texttospeechparameter name=""verklankingRechts"" />
      <xhtmlparameter name=""leftBody"" />
      <xhtmlresourceparameter name=""leftSource"" />
      <integerparameter name=""sourceHeight"">200</integerparameter>
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemQuestion"">
        <p id=""c1-id-18"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-19"" class=""UserSRttsEngels"">Drag and drop</span>
          <br id=""c1-id-20"" />
        </p>
        <p id=""c1-id-21"" xmlns=""http://www.w3.org/1999/xhtml"">
          <strong id=""c1-id-22"">
            <span id=""c1-id-23"" class=""UserSRttsEngels"">Drag the words to the correct category. </span>
          </strong>
        </p>
        <p id=""c1-id-24"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-26"" class=""UserSRttsEngels"">Each category should contain two words.</span>
          <strong id=""c1-id-25"">
            <br id=""c1-id-27"" />
          </strong>
        </p>
      </xhtmlparameter>
      <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
        <subparameterset id=""A"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Foo</gapTextParameter>
        </subparameterset>
        <subparameterset id=""B"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Bar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""C"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Mayhaps</gapTextParameter>
        </subparameterset>
        <subparameterset id=""D"">
          <gapTextParameter name=""gapText"" matchMax=""1"">FooBar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""E"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bada</gapTextParameter>
        </subparameterset>
        <subparameterset id=""F"">
          <gapTextParameter name=""gapText"" matchMax=""1"">green</gapTextParameter>
        </subparameterset>
        <definition id="""">
          <gapTextParameter name=""gapText"" matchMax=""1"" />
        </definition>
        <xhtmlParameter name=""itemInlineInput"">
          <table style=""BORDER-COLLAPSE: collapse; "" width=""100%"" id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
            <colgroup id=""c1-id-12"">
              <col id=""c1-id-13"" />
              <col id=""c1-id-14"" />
              <col id=""c1-id-15"" />
            </colgroup>
            <tbody id=""c1-id-16"">
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-17"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-18"">
                  <p id=""c1-id-19"">order</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-20"">
                  <p id=""c1-id-21"">comparison</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-22"">
                  <p id=""c1-id-23"">emphasis</p>
                </td>
              </tr>
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-24"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-25"">
                  <p id=""c1-id-26"">
                    <cito:InlineElement id=""I877c4041-88c3-402d-852d-953eb9c49f9a"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I877c4041-88c3-402d-852d-953eb9c49f9a</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">1</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-27"">
                    <cito:InlineElement id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I812c5637-bbce-4144-bfc1-3c6dca9b1fd3</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">4</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-28"">
                  <p id=""c1-id-29"">
                    <cito:InlineElement id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I6d1dddae-0d7e-4645-adf5-382990e43bcf</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">2</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-30"">
                    <cito:InlineElement id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">5</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-31"">
                  <p id=""c1-id-32"">
                    <cito:InlineElement id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I086eb640-dd2f-45d5-aa12-c021b1dbc9e5</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">3</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-33"">
                    <cito:InlineElement id=""I43b05777-5ad8-484e-adf4-225364eb833f"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I43b05777-5ad8-484e-adf4-225364eb833f</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">6</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
              </tr>
            </tbody>
          </table>
          <p id=""c1-id-34"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
        </xhtmlParameter>
      </gapMatchScoringParameter>
      <xhtmlparameter name=""itemInlineInput"" />
      <xhtmlparameter name=""itemGeneral"" />
      <collectionparameter name=""choices"">
        <definition id="""">
          <plaintextparameter name=""choice"" />
          <integerparameter name=""nrOfConnections"" />
        </definition>
      </collectionparameter>
    </parameterSet>
  </parameters>
</assessmentItem>");



        private readonly XElement item3DataXml = XElement.Parse(@"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""item2"" title=""Categories"" layoutTemplateSrc=""ilt.gapmatch"">
  <solution>
    <keyFindings>
      <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
        <keyFactSet>
          <keyFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
        </keyFactSet>
        <keyFactSet>
          <keyFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
        </keyFactSet>
      </keyFinding>
    </keyFindings>
    <conceptFindings>
      <conceptFinding id=""gapMatchController"" scoringMethod=""None"">
        <conceptFactSet>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
        <conceptFactSet>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
        <conceptFactSet>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
      </conceptFinding>
    </conceptFindings>
    <aspectReferences />
    <ItemScoreTranslationTable>
      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
    </ItemScoreTranslationTable>
  </solution>
  <parameters>
    <parameterSet id=""entireItem"">
      <listedparameter name=""itemStyle"">Default</listedparameter>
      <booleanparameter name=""dualColumnLayout"">True</booleanparameter>
      <booleanparameter name=""showCalculatorButton"" />
      <booleanparameter name=""displayVerklankingOnTheRight"">True</booleanparameter>
      <collectionparameter name=""numberOfAudioContentItems"">
        <definition id="""">
          <resourceparameter name=""audiocontent"" />
          <xhtmlparameter name=""description"" />
        </definition>
      </collectionparameter>
      <texttospeechparameter name=""verklankingLinks"" />
      <texttospeechparameter name=""verklankingRechts"" />
      <xhtmlparameter name=""leftBody"" />
      <xhtmlresourceparameter name=""leftSource"" />
      <integerparameter name=""sourceHeight"">200</integerparameter>
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemQuestion"">
        <p id=""c1-id-18"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-19"" class=""UserSRttsEngels"">Drag and drop</span>
          <br id=""c1-id-20"" />
        </p>
        <p id=""c1-id-21"" xmlns=""http://www.w3.org/1999/xhtml"">
          <strong id=""c1-id-22"">
            <span id=""c1-id-23"" class=""UserSRttsEngels"">Drag the words to the correct category. </span>
          </strong>
        </p>
        <p id=""c1-id-24"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-26"" class=""UserSRttsEngels"">Each category should contain two words.</span>
          <strong id=""c1-id-25"">
            <br id=""c1-id-27"" />
          </strong>
        </p>
      </xhtmlparameter>
      <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
        <subparameterset id=""A"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Foo</gapTextParameter>
        </subparameterset>
        <subparameterset id=""B"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Bar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""C"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Mayhaps</gapTextParameter>
        </subparameterset>
        <subparameterset id=""D"">
          <gapTextParameter name=""gapText"" matchMax=""1"">FooBar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""E"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bada</gapTextParameter>
        </subparameterset>
        <subparameterset id=""F"">
          <gapTextParameter name=""gapText"" matchMax=""1"">green</gapTextParameter>
        </subparameterset>
        <definition id="""">
          <gapTextParameter name=""gapText"" matchMax=""1"" />
        </definition>
        <xhtmlParameter name=""itemInlineInput"">
          <table style=""BORDER-COLLAPSE: collapse; "" width=""100%"" id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
            <colgroup id=""c1-id-12"">
              <col id=""c1-id-13"" />
              <col id=""c1-id-14"" />
              <col id=""c1-id-15"" />
            </colgroup>
            <tbody id=""c1-id-16"">
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-17"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-18"">
                  <p id=""c1-id-19"">order</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-20"">
                  <p id=""c1-id-21"">comparison</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-22"">
                  <p id=""c1-id-23"">emphasis</p>
                </td>
              </tr>
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-24"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-25"">
                  <p id=""c1-id-26"">
                    <cito:InlineElement id=""I877c4041-88c3-402d-852d-953eb9c49f9a"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I877c4041-88c3-402d-852d-953eb9c49f9a</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">1</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-27"">
                    <cito:InlineElement id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I812c5637-bbce-4144-bfc1-3c6dca9b1fd3</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">4</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-28"">
                  <p id=""c1-id-29"">
                    <cito:InlineElement id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I6d1dddae-0d7e-4645-adf5-382990e43bcf</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">2</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-30"">
                    <cito:InlineElement id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">5</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-31"">
                  <p id=""c1-id-32"">
                    <cito:InlineElement id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I086eb640-dd2f-45d5-aa12-c021b1dbc9e5</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">3</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-33"">
                    <cito:InlineElement id=""I43b05777-5ad8-484e-adf4-225364eb833f"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I43b05777-5ad8-484e-adf4-225364eb833f</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">6</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
              </tr>
            </tbody>
          </table>
          <p id=""c1-id-34"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
        </xhtmlParameter>
      </gapMatchScoringParameter>
      <xhtmlparameter name=""itemInlineInput"" />
      <xhtmlparameter name=""itemGeneral"" />
      <collectionparameter name=""choices"">
        <definition id="""">
          <plaintextparameter name=""choice"" />
          <integerparameter name=""nrOfConnections"" />
        </definition>
      </collectionparameter>
    </parameterSet>
<parameterSet id=""info"">
      <listedparameter name=""itemStyle"">Default</listedparameter>
      <booleanparameter name=""dualColumnLayout"">True</booleanparameter>
      <booleanparameter name=""showCalculatorButton"" />
      <booleanparameter name=""displayVerklankingOnTheRight"">True</booleanparameter>
      <collectionparameter name=""numberOfAudioContentItems"">
        <definition id="""">
          <resourceparameter name=""audiocontent"" />
          <xhtmlparameter name=""description"" />
        </definition>
      </collectionparameter>
      <texttospeechparameter name=""verklankingLinks"" />
      <texttospeechparameter name=""verklankingRechts"" />
      <xhtmlparameter name=""leftBody"" />
      <xhtmlresourceparameter name=""leftSource"" />
      <integerparameter name=""sourceHeight"">200</integerparameter>
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemQuestion"">
        <p id=""c1-id-18"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-19"" class=""UserSRttsEngels"">Drag and drop</span>
          <br id=""c1-id-20"" />
        </p>
        <p id=""c1-id-21"" xmlns=""http://www.w3.org/1999/xhtml"">
          <strong id=""c1-id-22"">
            <span id=""c1-id-23"" class=""UserSRttsEngels"">Drag the words to the correct category. </span>
          </strong>
        </p>
        <p id=""c1-id-24"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-26"" class=""UserSRttsEngels"">Each category should contain two words.</span>
          <strong id=""c1-id-25"">
            <br id=""c1-id-27"" />
          </strong>
        </p>
      </xhtmlparameter>
      <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
        <subparameterset id=""A"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Foo</gapTextParameter>
        </subparameterset>
        <subparameterset id=""B"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Bar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""C"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Mayhaps</gapTextParameter>
        </subparameterset>
        <subparameterset id=""D"">
          <gapTextParameter name=""gapText"" matchMax=""1"">FooBar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""E"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bada</gapTextParameter>
        </subparameterset>
        <subparameterset id=""F"">
          <gapTextParameter name=""gapText"" matchMax=""1"">green</gapTextParameter>
        </subparameterset>
        <definition id="""">
          <gapTextParameter name=""gapText"" matchMax=""1"" />
        </definition>
        <xhtmlParameter name=""itemInlineInput"">
          <table style=""BORDER-COLLAPSE: collapse; "" width=""100%"" id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
            <colgroup id=""c1-id-12"">
              <col id=""c1-id-13"" />
              <col id=""c1-id-14"" />
              <col id=""c1-id-15"" />
            </colgroup>
            <tbody id=""c1-id-16"">
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-17"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-18"">
                  <p id=""c1-id-19"">order</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-20"">
                  <p id=""c1-id-21"">comparison</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-22"">
                  <p id=""c1-id-23"">emphasis</p>
                </td>
              </tr>
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-24"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-25"">
                  <p id=""c1-id-26"">
                    <cito:InlineElement id=""I877c4041-88c3-402d-852d-953eb9c49f9a"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I877c4041-88c3-402d-852d-953eb9c49f9a</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">1</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-27"">
                    <cito:InlineElement id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I812c5637-bbce-4144-bfc1-3c6dca9b1fd3</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">4</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-28"">
                  <p id=""c1-id-29"">
                    <cito:InlineElement id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I6d1dddae-0d7e-4645-adf5-382990e43bcf</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">2</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-30"">
                    <cito:InlineElement id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">5</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-31"">
                  <p id=""c1-id-32"">
                    <cito:InlineElement id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I086eb640-dd2f-45d5-aa12-c021b1dbc9e5</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">3</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-33"">
                    <cito:InlineElement id=""I43b05777-5ad8-484e-adf4-225364eb833f"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I43b05777-5ad8-484e-adf4-225364eb833f</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">6</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
              </tr>
            </tbody>
          </table>
          <p id=""c1-id-34"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
        </xhtmlParameter>
      </gapMatchScoringParameter>
      <xhtmlparameter name=""itemInlineInput"" />
      <xhtmlparameter name=""itemGeneral"" />
      <collectionparameter name=""choices"">
        <definition id="""">
          <plaintextparameter name=""choice"" />
          <integerparameter name=""nrOfConnections"" />
        </definition>
      </collectionparameter>
    </parameterSet>
  </parameters>
</assessmentItem>");


        private readonly XElement item4DataXml =
            XElement.Parse(
                @"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""dtt-dwo-type2-01"" title=""dtt-dwo-type2-01-lin-evaluate"" layoutTemplateSrc=""Cito.CTE.CustomInteraction.SC"">
  <solution>
    <keyFindings>
      <keyFinding id=""CustomInteractions"" scoringMethod=""Dichotomous"">
        <keyFact id=""A-CI_SP0"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""CI_SP0"" occur=""1"">
            <stringComparisonValue>
              <typedComparisonValue>{x:-1,y:-1}{x:2,y:2}</typedComparisonValue>
              <comparisonType>Evaluate</comparisonType>
            </stringComparisonValue>
          </keyValue>
        </keyFact>
      </keyFinding>
    </keyFindings>
    <conceptFindings>
      <conceptFinding id=""CustomInteractions"" scoringMethod=""None"">
        <conceptFact id=""A[*]-CI_SP0"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""CI_SP0"" occur=""1"">
            <catchAllValue />
          </conceptValue>
          <concepts>
            <concept value=""0"" code=""E. Verbanden en formules"" />
            <concept value=""0"" code=""E1"" />
            <concept value=""0"" code=""1. wiskundige structuur"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""A-CI_SP0"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""CI_SP0"" occur=""1"">
            <stringComparisonValue>
              <typedComparisonValue>{x:-1,y:-1}{x:2,y:2}</typedComparisonValue>
              <comparisonType>Evaluate</comparisonType>
            </stringComparisonValue>
          </conceptValue>
          <concepts>
            <concept value=""1"" code=""E. Verbanden en formules"" />
            <concept value=""1"" code=""E1"" />
            <concept value=""1"" code=""1. wiskundige structuur"" />
          </concepts>
        </conceptFact>
      </conceptFinding>
    </conceptFindings>
    <aspectReferences />
    <ItemScoreTranslationTable>
      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
    </ItemScoreTranslationTable>
  </solution>
  <parameters>
    <parameterSet id=""kenmerken"">
      <booleanparameter name=""showCalculatorButton"" />
      <integerparameter name=""hightOfScrollText"" />
      <integerparameter name=""fixedWidthMatrixColumn"">100</integerparameter>
      <booleanparameter name=""showChoicesBottomLayout"" />
      <integerparameter name=""fixedHeightAlternatives"">0</integerparameter>
      <plaintextparameter name=""calculatorDescription"" />
      <listedparameter name=""calculatorMode"">basic</listedparameter>
      <booleanparameter name=""showReset"" />
      <booleanparameter name=""showNotepad"" />
      <plaintextparameter name=""notepadDescription"" />
      <booleanparameter name=""showSymbolPicker"" />
      <plaintextparameter name=""symbolPickerDescription"" />
      <plaintextparameter name=""symbols"" />
      <booleanparameter name=""showRuler"" />
      <plaintextparameter name=""rulerDescription"" />
      <plaintextparameter name=""rulerStartValue"" />
      <plaintextparameter name=""rulerEndValue"" />
      <plaintextparameter name=""rulerStepValue"" />
      <integerparameter name=""rulerStart"" />
      <integerparameter name=""rulerEnd"" />
      <integerparameter name=""rulerStep"" />
      <integerparameter name=""rulerStepSize"" />
      <listedparameter name=""rulerLengthUnit"">centimeter</listedparameter>
      <booleanparameter name=""showProtractor"" />
      <plaintextparameter name=""protractorPickerDescription"" />
      <booleanparameter name=""protractorEnableRotation"">True</booleanparameter>
      <booleanparameter name=""showTriangle"" />
      <plaintextparameter name=""trianglePickerDescription"" />
      <integerparameter name=""triangleMinDegrees"" />
      <integerparameter name=""triangleMaxDegrees"" />
      <booleanparameter name=""showSpellCheck"">False</booleanparameter>
      <listedparameter name=""spellCheckCulture"">nl-NL</listedparameter>
      <booleanparameter name=""showFormulaList"">False</booleanparameter>
      <plaintextparameter name=""formulaListDescription"" />
      <listedparameter name=""formulaType"">default</listedparameter>
      <booleanparameter name=""showTextMarker"">False</booleanparameter>
      <plaintextparameter name=""textMarkerDescription"" />
      <listedparameter name=""itemLanguage"">nl-NL</listedparameter>
    </parameterSet>
    <parameterSet id=""entireItem"">
      <booleanparameter name=""dualColumnLayout"">False</booleanparameter>
      <xhtmlparameter name=""leftBody"" />
      <xhtmlresourceparameter name=""leftSource"" />
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">In het ..</p>
        <p id=""c1-id-12"" xmlns=""http://www.w3.org/1999/xhtml"">ketentest: f(x) = <strong id=""c1-id-14"">x  </strong>(fout: <strong id=""c1-id-16"">2x</strong>)</p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemQuestion"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">          <strong id=""c1-id-12"">Voor de ...</strong>        </p>
      </xhtmlparameter>
      <custominteractionresourceparameter name=""cisource"" scalable=""false"" interactioncount=""0"">DWO_itemtype2_JH1507.ci</custominteractionresourceparameter>
      <integerparameter name=""width"">400</integerparameter>
      <integerparameter name=""height"">400</integerparameter>
      <xhtmlparameter name=""itemGeneral"" />
    </parameterSet>
    <parameterSet id=""__CustomInteractions"" isDynamicCollection=""true"">
      <mathScoringParameter name=""CI_SP0"" label=""A"" ControllerId=""CI_SP0"" findingOverride=""CustomInteractions"">
        <subparameterset id=""A"" />
        <definition id="""" />
      </mathScoringParameter>
    </parameterSet>
  </parameters>
</assessmentItem>");

        private readonly XElement ControlTemplate = XElement.Parse(@"<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""1"">
	<Description>Control template voor koppelitems</Description>
	<Targets>
		<Target xsi:type=""ControlTemplateTarget"" enabled=""true"" name=""ces"">
			<Description>Testplayer 1.x / 2.x</Description>
			<Template>
  <![CDATA[
				<%
				Dim dualColumnLayout As Boolean = parameters.GetParameterByName(""dualColumnLayout"").Value
				%>

				<table id=""content"" cellpadding=""25px"" xml:space=""preserve"">
					<tr>
					<%If DualColumnLayout Then%>
						<td id=""left"">
            <%Dim audioContentItemLeft As TextToSpeechParameter = parameters.GetParameterByName(""verklankingLinks"", False)
              If audioContentItemLeft IsNot Nothing AndAlso Not String.IsNullOrEmpty(audioContentItemLeft.Value) Then %>
							<div id=""audioPlayer"">
							</div>
						<%End If

						If Not Cito.Tester.Common.TemplateHelper.IsXHtmlParameterEmpty(parameters.GetParameterByName(""leftBody"").Nodes) Then%>
							<div id=""body"">
								<%=SetParagraphMargins(parameters.GetParameterByName(""leftBody"").Value)%>
							</div>
						<%End If

						Dim param As XhtmlResourceParameter = parameters.GetParameterByName(""leftSource"")
						If Not String.IsNullOrEmpty(param.Content) Then%>
							<div id=""source"" style=""height:<%#sourceHeight%>px;overflow-x:hidden;overflow-y:scroll;border: 1px solid #000000;padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;"">
								<%=param.Content%>
							</div>
						<%End If%>

						</td>
						<td id=""right"">
					<%Else%>
						<td>
					<%End If

						Dim audioContentItem As TextToSpeechParameter  = parameters.GetParameterByName(""verklankingLinks"", False)
						If dualColumnLayout Then audioContentItem = parameters.GetParameterByName(""verklankingRechts"", False)
						If audioContentItem IsNot Nothing AndAlso Not String.IsNullOrEmpty(audioContentItem.Value) Then%>
							<div id=""audioPlayer"">
							</div>
						<%End If

						Dim numberOfGaps As Integer = GetNumberOfGaps(parameters.GetParameterByName(""gapMatchScoring"").GapXhtmlParameter.Value, ""inlineGapMatchId"")
            If numberOfGaps = 0 Then numberOfGaps = GetNumberOfGaps(parameters.GetParameterByName(""itemInlineInput"").Value, ""inlineGapMatchId"")		'Extra check: previously it was possible to add inline controls to parameter itemInlineInput
						If numberOfGaps = 0 Then numberOfGaps = GetNumberOfGaps(parameters.GetParameterByName(""itemQuestion"").Value, ""inlineGapMatchId"")		'Extra check: previously it was possible to add inline controls to parameter itemQuestion
						If numberOfGaps > 0 Then
							Dim controlValues As String = CreateChoiceValues(parameters.GetParameterByName(""gapMatchScoring"").Value)%>
						<%End If
						If Not Cito.Tester.Common.TemplateHelper.IsXHtmlParameterEmpty(parameters.GetParameterByName(""itemBody"").Nodes) Then%>
							<div id=""body"">
								<%=SetParagraphMargins(parameters.GetParameterByName(""itemBody"").Value)%>
							</div>
						<%End If
						If Not Cito.Tester.Common.TemplateHelper.IsXHtmlParameterEmpty(parameters.GetParameterByName(""itemQuestion"").Nodes) Then%>
							<div id=""question"">
								<%=SetParagraphMargins(parameters.GetParameterByName(""itemQuestion"").Value)%>
							</div>
						<%End If
            If Not Cito.Tester.Common.TemplateHelper.IsXHtmlParameterEmpty(parameters.GetParameterByName(""gapMatchScoring"").GapXhtmlParameter.Nodes) Then
              Dim gapMatchXhtml As String = parameters.GetParameterByName(""gapMatchScoring"").GapXhtmlParameter.Value
              %>
              <div id=""input"">
                <%=gapMatchXhtml%>
              </div>
						<%ElseIf Not Cito.Tester.Common.TemplateHelper.IsXHtmlParameterEmpty(parameters.GetParameterByName(""itemInlineInput"").Nodes) Then%>
							<div id=""input"">
								<%=SetParagraphMargins(parameters.GetParameterByName(""itemInlineInput"").Value)%>
							</div>
						<%End If
						If Not Cito.Tester.Common.TemplateHelper.IsXHtmlParameterEmpty(parameters.GetParameterByName(""itemGeneral"").Nodes) Then%>
							<div id=""intro"">
								<br />
								<%=SetParagraphMargins(parameters.GetParameterByName(""itemGeneral"").Value)%>
							</div>
						<%End If%>
						</td>
					</tr>
				</table>
			]]></Template>
			<ParameterSet id="""" />
		</Target>
	</Targets>

  <SharedParameterSet>
    <booleanparameter name=""dualColumnLayout"">
      <designersetting key=""visible"">false</designersetting>
      <designersetting key=""defaultvalue"">false</designersetting>
    </booleanparameter>
    <booleanparameter name=""showCalculatorButton"">
      <designersetting key=""label"">Rekenmachine</designersetting>
      <designersetting key=""description"">Geef aan of tijdens het beantwoorden van het item de kandidaat gebruik mag maken van de ingebouwde calculator.</designersetting>
      <designersetting key=""required"">False</designersetting>
      <designersetting key=""sortkey"">1</designersetting>
      <designersetting key=""group"">1 Hulpmiddelen</designersetting>
      <designersetting key=""visible"">True</designersetting>
    </booleanparameter>
    <booleanparameter name=""displayVerklankingOnTheRight"">
      <designersetting key=""label"">Toon verklanking rechts</designersetting>
      <designersetting key=""visible"">False</designersetting>
      <designersetting key=""defaultvalue"">True</designersetting>
      <designersetting key=""group"">2 Verklanking</designersetting>
      <designersetting key=""sortkey"">1</designersetting>
    </booleanparameter>
    <collectionparameter name=""numberOfAudioContentItems"">
      <designersetting key=""label""></designersetting>
      <designersetting key=""visible"">False</designersetting>
      <designersetting key=""itemcountlabel"">Aantal audio bestanden</designersetting>
      <designersetting key=""description""></designersetting>
      <designersetting key=""minimumLength"">0</designersetting>
      <designersetting key=""maximumLength"">5</designersetting>
      <designersetting key=""defaultvalue"">0</designersetting>
      <designersetting key=""subsetidentifiers"">Numeric</designersetting>
      <designersetting key=""group"">2 Verklanking</designersetting>
      <designersetting key=""required"">False</designersetting>
      <designersetting key=""sortkey"">2</designersetting>
      <definition>
        <resourceparameter name=""audiocontent"">
          <designersetting key=""label"">Audiobestand</designersetting>
          <designersetting key=""description""></designersetting>
          <designersetting key=""required"">False</designersetting>
          <designersetting key=""filter"">audio/mpeg</designersetting>
          <designersetting key=""editbuttonVisible"">false</designersetting>
          <designersetting key=""deletebuttonVisible"">false</designersetting>
          <designersetting key=""group""></designersetting>
        </resourceparameter>
        <xhtmlparameter name=""description"">
          <designersetting key=""label"">Beschrijving</designersetting>
          <designersetting key=""group""></designersetting>
          <designersetting key=""required"">False</designersetting>
          <designersetting key=""description""></designersetting>
        </xhtmlparameter>
      </definition>
    </collectionparameter>
    <texttospeechparameter name=""verklankingLinks"">
      <designersetting key=""visible"">True</designersetting>
      <designersetting key=""label"">Verklanking</designersetting>
      <designersetting key=""group"">2 Verklanking</designersetting>
      <designersetting key=""sortkey"">1</designersetting>
      <designersetting key=""required"">False</designersetting>
      <designersetting key=""filter"">audio/mpeg|audio/mp3</designersetting>
      <designersetting key=""itemPart"">Both</designersetting>
    </texttospeechparameter>
    <texttospeechparameter name=""verklankingRechts"">
      <designersetting key=""visible"">False</designersetting>
      <designersetting key=""label"">Verklanking rechts</designersetting>
      <designersetting key=""group"">2 Verklanking</designersetting>
      <designersetting key=""sortkey"">2</designersetting>
      <designersetting key=""required"">False</designersetting>
      <designersetting key=""filter"">audio/mpeg|audio/mp3</designersetting>
      <designersetting key=""itemPart"">Right</designersetting>
    </texttospeechparameter>
    <xhtmlparameter name=""leftBody"">
      <designersetting key=""label"">Body links</designersetting>
      <designersetting key=""description"">Schrijf hier wat er in de linkerkolom van het item dient te komen. Dit kan ook plaatjes of tabellen bevatten.</designersetting>
      <designersetting key=""required"">false</designersetting>
      <designersetting key=""visible"">false</designersetting>
      <designersetting key=""group"">3 Linkerkolom</designersetting>
    </xhtmlparameter>
    <xhtmlresourceparameter name=""leftSource"">
      <designersetting key=""label"">Brontekst</designersetting>
      <designersetting key=""description"">Selecteer de brontekst uit de bank.</designersetting>
      <designersetting key=""required"">false</designersetting>
      <designersetting key=""visible"">false</designersetting>
      <designersetting key=""filter"">text/plain|text/html|application/xhtml+xml</designersetting>
      <designersetting key=""group"">3 Linkerkolom</designersetting>
    </xhtmlresourceparameter>
    <integerparameter name=""sourceHeight"">
      <designersetting key=""label"">Hoogte</designersetting>
      <designersetting key=""description"">De hoogte van de brontekst.</designersetting>
      <designersetting key=""group"">3 Linkerkolom</designersetting>     
      <designersetting key=""visible"">False</designersetting>
      <designersetting key=""defaultvalue"">200</designersetting>
    </integerparameter>
    <integerparameter name=""sourcePositionTop"">
      <designersetting key=""label"">Facet: Aantal pixels onder linker bovenhoek</designersetting>
      <designersetting key=""description"">Brontekst wordt links in de bovenhoek getoond. Als daar tekst staat zal de brontekst naar beneden geplaatst moeten worden.</designersetting>
      <designersetting key=""group"">3 Linkerkolom</designersetting>
      <designersetting key=""visible"">True</designersetting>
      <designersetting key=""defaultvalue"">0</designersetting>
      <designersetting key=""sortkey"">3</designersetting>
    </integerparameter>
    <xhtmlparameter name=""itemBody"">
      <designersetting key=""label"">Body</designersetting>
      <designersetting key=""description"">Schrijf hier de stam van dit item. De body kan ook plaatjes of tabellen bevatten.</designersetting>
      <designersetting key=""required"">false</designersetting>
      <designersetting key=""group"">4 Stam</designersetting>
      <designersetting key=""sortkey"">1</designersetting>
    </xhtmlparameter>
    <xhtmlparameter name=""itemQuestion"">
      <designersetting key=""label"">Vraag</designersetting>
      <designersetting key=""description"">Schrijf hier de vraag van het item.</designersetting>
      <designersetting key=""required"">False</designersetting>
      <designersetting key=""visible"">True</designersetting>
      <designersetting key=""inlinetemplate""></designersetting>
      <designersetting key=""group"">4 Stam</designersetting>
      <designersetting key=""sortkey"">2</designersetting>
    </xhtmlparameter>
    <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
      <designersetting key=""label""></designersetting>
      <designersetting key=""itemcountlabel"">Aantal alternatieven</designersetting>
      <designersetting key=""description""></designersetting>
      <designersetting key=""defaultvalue"">0</designersetting>
      <designersetting key=""visible"">True</designersetting>
      <designersetting key=""minimumLength"">2</designersetting>
      <designersetting key=""maximumLength"">12</designersetting>
      <designersetting key=""group"">4 Stam</designersetting>
      <designersetting key=""subsetidentifiers"">Alphabetic</designersetting>
      <designersetting key=""sortkey"">4</designersetting>
      <definition>
        <gapTextParameter name=""gapText"">
          <designersetting key=""label"">Keuze</designersetting>
          <designersetting key=""description""></designersetting>
        </gapTextParameter>
      </definition>
      <xhtmlParameter name=""gapMatchInlineInput"">
        <designersetting key=""label"">Tekst en Interacties</designersetting>
        <designersetting key=""description"">Geef hier de tekst en/of de afbeelding en voeg interacties toe.</designersetting>
        <designersetting key=""required"">true</designersetting>
        <designersetting key=""group"">4 Stam</designersetting>
        <designersetting key=""inlinetemplate"">InlineGapMatchLayoutTemplate</designersetting>
        <designersetting key=""inlinetemplates""></designersetting>
        <designersetting key=""sortkey"">3</designersetting>
      </xhtmlParameter>
    </gapMatchScoringParameter>
    <xhtmlparameter name=""itemInlineInput"">
      <designersetting key=""label"">Invoervak</designersetting>
      <designersetting key=""description"">Geef hier de inline controls (in de tekst) op.</designersetting>
      <designersetting key=""required"">True</designersetting>
      <designersetting key=""visible"">True</designersetting>
      <designersetting key=""inlinetemplate""></designersetting>
      <designersetting key=""group"">4 Stam</designersetting>
      <designersetting key=""sortkey"">3</designersetting>
    </xhtmlparameter>
    <xhtmlparameter name=""itemGeneral"">
      <designersetting key=""label"">Algemeen tekstveld</designersetting>
      <designersetting key=""description"">Geef hier de tekst en/of de afbeelding in zoals die in het rechterdeel onder de antwoorden weergegeven dient te worden.</designersetting>
      <designersetting key=""group"">6 Algemeen tekstveld</designersetting>
      <designersetting key=""required"">False</designersetting>
    </xhtmlparameter>
    <collectionparameter name=""choices"">
      <designersetting key=""label""></designersetting>
      <designersetting key=""itemcountlabel"">Aantal alternatieven</designersetting>
      <designersetting key=""description""></designersetting>
      <designersetting key=""visible"">False</designersetting>
      <designersetting key=""group"">5 Alternatieven</designersetting>
      <designersetting key=""minimumLength"">2</designersetting>
      <designersetting key=""maximumLength"">12</designersetting>
      <designersetting key=""subsetidentifiers"">Alphabetic</designersetting>
      <designersetting key=""sortkey"">4</designersetting>
      <definition>
        <plaintextparameter name=""choice"">
          <designersetting key=""label"">Keuze</designersetting>
          <designersetting key=""description""></designersetting>
          <designersetting key=""required"">True</designersetting>
        </plaintextparameter>
        <integerparameter name=""nrOfConnections"">
          <designersetting key=""label"">Aantal keren te koppelen</designersetting>
          <designersetting key=""description"">Geef aan hoe vaak de tekst te koppelen is.</designersetting>
          <designersetting key=""defaultvalue"">1</designersetting>
          <designersetting key=""required"">true</designersetting>
          <designersetting key=""rangeFrom"">1</designersetting>
          <designersetting key=""rangeTo"">10</designersetting>
        </integerparameter>
      </definition>
    </collectionparameter>
  </SharedParameterSet>
</Template>
");


        private readonly XElement InlineGapMatchLayoutTemplate = XElement.Parse(@"<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""1"">
	<Description></Description>
	<Targets>
		<Target xsi:type=""ControlTemplateTarget"" enabled=""true"" name=""ces"">
			<Description></Description>
			<Template>
				<![CDATA[
						<%
            Dim controlId As String = GetInlineGapMatchId(parameters)
						Dim controlLabel As String = parameters.GetParameterByName(""inlineGapMatchLabel"").Value
            controlLabel = RemoveIllegalCharacters(controlLabel)
						Dim controlWidth As Integer = 20
						%>
						<span class='inlinecontrol'>
						</span>
				]]>
			</Template>
		</Target>
	</Targets>

	<SharedParameterSet>
		<plaintextparameter name=""inlineGapMatchId"">
			<designersetting key=""label"">label</designersetting>
			<designersetting key=""visible"">False</designersetting>
		</plaintextparameter> 
		<plaintextparameter name=""inlineGapMatchLabel"">
			<designersetting key=""label"">Label</designersetting>
			<designersetting key=""description"">Label waaraan de optie (in de score editor) kan worden herkend</designersetting>
			<designersetting key=""visible"">True</designersetting>
			<designersetting key=""required"">True</designersetting>
			<designersetting key=""group"">1 Algemeen</designersetting>
		</plaintextparameter>
		<booleanparameter name=""editSize"">
			<designersetting key=""label"">Afmetingen opgeven</designersetting>
			<designersetting key=""description"">Indicatie of de afmetingen van de afbeelding van de control kunnen worden opgegeven.</designersetting>
			<designersetting key=""defaultvalue"">true</designersetting>
			<designersetting key=""required"">false</designersetting>
			<designersetting key=""visible"">false</designersetting>
			<designersetting key=""group"">Afbeelding</designersetting>
		</booleanparameter>
		<integerparameter name=""width"">
			<designersetting key=""label"">Breedte (in pixels)</designersetting>
			<designersetting key=""description"">De breedte van de afbeelding van de control.</designersetting>
			<designersetting key=""required"">false</designersetting>
			<designersetting key=""visible"">false</designersetting>
			<designersetting key=""group"">Afbeelding</designersetting>
		</integerparameter>
		<integerparameter name=""height"">
			<designersetting key=""label"">Hoogte (in pixels)</designersetting>
			<designersetting key=""description"">De hoogte van de afbeelding van de control.</designersetting>
			<designersetting key=""required"">false</designersetting>
			<designersetting key=""visible"">false</designersetting>
			<designersetting key=""group"">Afbeelding</designersetting>
		</integerparameter>
	</SharedParameterSet>
</Template>");



        private readonly XElement ItemLayoutTemplate = XElement.Parse(@"<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""1"">
    <Description>Gap item (inline) met een kolom</Description>
    <Settings>
        <DesignerSetting key=""sort"">True</DesignerSetting>
    </Settings>
    <Targets>
        <Target xsi:type=""ItemLayoutTemplateTarget"" enabled=""true"" name=""ces"">
            <Description>TestPlayer 1.x / 2.x</Description>
            <Template>
                <![CDATA[
                <html>
                    <head>
                        <link rel=""Stylesheet"" href=""resource://package/Generic.css"" type=""text/css"" />
                        <link rel=""Stylesheet"" href=""resource://package/userstyle.css"" type=""text/css"" /> 
                    </head>
                    <body>
                        <cito:control xmlns:cito=""http://www.cito.nl/citotester"" id=""entireItem"" type=""min.gapmatchcontroltemplate"">
                            <parameter name=""dualColumnLayout"">
                                <designersetting key=""defaultvalue"">True</designersetting>
                            </parameter>
                            <parameter name=""verklankingLinks"">
                                <designersetting key=""label"">Verklanking links</designersetting>
                                <designersetting key=""itemPart"">Left</designersetting>
                            </parameter>
                            <parameter name=""verklankingRechts"">
                                <designersetting key=""visible"">True</designersetting>
                            </parameter>
                            <parameter name=""leftBody"">
                                <designersetting key=""visible"">True</designersetting>
                            </parameter>
                            <parameter name=""leftSource"">
                                <designersetting key=""visible"">True</designersetting>
                            </parameter>
                            <parameter name=""sourceHeight"">
                                <designersetting key=""visible"">True</designersetting>
                            </parameter>
                            <parameter name=""itemInlineInput"">
                                <designersetting key=""visible"">False</designersetting>
                            </parameter>
                        </cito:control>
                    </body>
                </html>
                ]]>
            </Template>
        </Target>
      </Targets>
</Template>");



        private readonly XElement expectedItem1 = XElement.Parse(@"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""item1"" title=""Teenager forum"" layoutTemplateSrc=""ilt.gapmatch"">
  <solution>
    <keyFindings>
      <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
        <keyFact id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac"" occur=""1"">
            <stringValue>
              <typedValue>D</typedValue>
            </stringValue>
          </keyValue>
        </keyFact>
        <keyFact id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50"" occur=""1"">
            <stringValue>
              <typedValue>A</typedValue>
            </stringValue>
          </keyValue>
        </keyFact>
        <keyFact id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8"" occur=""1"">
            <stringValue>
              <typedValue>B</typedValue>
            </stringValue>
          </keyValue>
        </keyFact>
      </keyFinding>
    </keyFindings>
    <conceptFindings>
      <conceptFinding id=""gapMatchController"" scoringMethod=""None"">
        <conceptFact id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac"" occur=""1"">
            <stringValue>
              <typedValue>D</typedValue>
            </stringValue>
          </conceptValue>
          <concepts>
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50"" occur=""1"">
            <stringValue>
              <typedValue>A</typedValue>
            </stringValue>
          </conceptValue>
          <concepts>
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8"" occur=""1"">
            <stringValue>
              <typedValue>B</typedValue>
            </stringValue>
          </conceptValue>
          <concepts>
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac[*]"" occur=""1"">
            <catchAllValue />
          </conceptValue>
          <concepts>
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8[*]"" occur=""1"">
            <catchAllValue />
          </conceptValue>
          <concepts>
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
        <conceptFact id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <conceptValue domain=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50[*]"" occur=""1"">
            <catchAllValue />
          </conceptValue>
          <concepts>
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFact>
      </conceptFinding>
    </conceptFindings>
    <aspectReferences />
    <ItemScoreTranslationTable>
      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
    </ItemScoreTranslationTable>
  </solution>
  <parameters>
    <parameterSet id=""entireItem"">
      <booleanparameter name=""dualColumnLayout"">True</booleanparameter>
      <booleanparameter name=""showCalculatorButton"" />
      <booleanparameter name=""displayVerklankingOnTheRight"">True</booleanparameter>
      <collectionparameter name=""numberOfAudioContentItems"">
        <definition id="""">
          <resourceparameter name=""audiocontent"" />
          <xhtmlparameter name=""description"" />
        </definition>
      </collectionparameter>
      <xhtmlparameter name=""leftBody"" />
      <xhtmlresourceparameter name=""leftSource"" />
      <integerparameter name=""sourceHeight"">200</integerparameter>
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span lang=""EN-GB"" id=""c1-id-12""></span>
        </p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemQuestion"">
        <p id=""c1-id-11"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">Drag and drop</p>
        <p id=""c1-id-12"" xmlns=""http://www.w3.org/1999/xhtml"">
          <strong id=""c1-id-13"">Vul de tekst aan door de woorden naar de juiste plek te slepen.</strong>
        </p>
        <p id=""c1-id-14"" xmlns=""http://www.w3.org/1999/xhtml"">Let op! Er blijft één antwoord over.</p>
      </xhtmlparameter>
      <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
        <subparameterset id=""A"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bing</gapTextParameter>
        </subparameterset>
        <subparameterset id=""B"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bang</gapTextParameter>
        </subparameterset>
        <subparameterset id=""C"">
          <gapTextParameter name=""gapText"" matchMax=""1"">whopper</gapTextParameter>
        </subparameterset>
        <subparameterset id=""D"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bada</gapTextParameter>
        </subparameterset>
        <definition id="""">
          <gapTextParameter name=""gapText"" matchMax=""1"" />
        </definition>
        <xhtmlParameter name=""itemInlineInput"">
          <p id=""c1-id-11"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">Hi ...</p>
          <p id=""c1-id-12"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">I have ...<cito:InlineElement id=""I4371df73-3d68-49c2-81ed-4ceb12ca6aac"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester""><cito:parameters><cito:parameterSet id=""entireItem""><cito:plaintextparameter name=""inlineGapMatchId"">I4371df73-3d68-49c2-81ed-4ceb12ca6aac</cito:plaintextparameter><cito:plaintextparameter name=""inlineGapMatchLabel"">1</cito:plaintextparameter><cito:booleanparameter name=""editSize"">True</cito:booleanparameter><cito:integerparameter name=""width"" /><cito:integerparameter name=""height"" /></cito:parameterSet></cito:parameters></cito:InlineElement> I had ...<cito:InlineElement id=""Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester""><cito:parameters><cito:parameterSet id=""entireItem""><cito:plaintextparameter name=""inlineGapMatchId"">Ie64db2b2-c55c-42ab-bc6b-4ead3c2451f8</cito:plaintextparameter><cito:plaintextparameter name=""inlineGapMatchLabel"">2</cito:plaintextparameter><cito:booleanparameter name=""editSize"">True</cito:booleanparameter><cito:integerparameter name=""width"" /><cito:integerparameter name=""height"" /></cito:parameterSet></cito:parameters></cito:InlineElement> he told ...<cito:InlineElement id=""I51b6db97-d93a-4b4a-97d2-f2e508f30e50"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester""><cito:parameters><cito:parameterSet id=""entireItem""><cito:plaintextparameter name=""inlineGapMatchId"">I51b6db97-d93a-4b4a-97d2-f2e508f30e50</cito:plaintextparameter><cito:plaintextparameter name=""inlineGapMatchLabel"">3</cito:plaintextparameter><cito:booleanparameter name=""editSize"">True</cito:booleanparameter><cito:integerparameter name=""width"" /><cito:integerparameter name=""height"" /></cito:parameterSet></cito:parameters></cito:InlineElement> she has ...<br id=""c1-id-13"" /><br id=""c1-id-14"" />Please help ...</p>
          <p id=""c1-id-15"" class=""UserSRttsEngels"" xmlns=""http://www.w3.org/1999/xhtml"">Love,</p>
          <p id=""c1-id-16"" xmlns=""http://www.w3.org/1999/xhtml"">Ani125</p>
        </xhtmlParameter>
      </gapMatchScoringParameter>
      <xhtmlparameter name=""itemInlineInput"" />
      <xhtmlparameter name=""itemGeneral"" />
      <collectionparameter name=""choices"">
        <definition id="""">
          <plaintextparameter name=""choice"" />
          <integerparameter name=""nrOfConnections"" />
        </definition>
      </collectionparameter>
    </parameterSet>
  </parameters>
</assessmentItem>");


        private readonly XElement expectedItem2 = XElement.Parse(@"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""item2"" title=""Categories"" layoutTemplateSrc=""ilt.gapmatch"">
  <solution>
    <keyFindings>
      <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
        <keyFactSet>
          <keyFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
        </keyFactSet>
        <keyFactSet>
          <keyFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
          <keyFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <keyValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </keyValue>
          </keyFact>
        </keyFactSet>
      </keyFinding>
    </keyFindings>
    <conceptFindings>
      <conceptFinding id=""gapMatchController"" scoringMethod=""None"">
        <conceptFactSet>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""1"" code=""ENG-2"" />
            <concept value=""1"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
        <conceptFactSet>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f[*]-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f[*]"" occur=""1"">
              <catchAllValue />
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
        <conceptFactSet>
          <conceptFact id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" occur=""1"">
              <stringValue>
                <typedValue>F</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I43b05777-5ad8-484e-adf4-225364eb833f-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I43b05777-5ad8-484e-adf4-225364eb833f"" occur=""1"">
              <stringValue>
                <typedValue>E</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" occur=""1"">
              <stringValue>
                <typedValue>C</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" occur=""1"">
              <stringValue>
                <typedValue>D</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" occur=""1"">
              <stringValue>
                <typedValue>B</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <conceptFact id=""I877c4041-88c3-402d-852d-953eb9c49f9a-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <conceptValue domain=""I877c4041-88c3-402d-852d-953eb9c49f9a"" occur=""1"">
              <stringValue>
                <typedValue>A</typedValue>
              </stringValue>
            </conceptValue>
          </conceptFact>
          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
            <concept value=""0"" code=""ENG-2"" />
            <concept value=""0"" code=""ENG-2.1"" />
          </concepts>
        </conceptFactSet>
      </conceptFinding>
    </conceptFindings>
    <aspectReferences />
    <ItemScoreTranslationTable>
      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
    </ItemScoreTranslationTable>
  </solution>
  <parameters>
    <parameterSet id=""entireItem"">
      <booleanparameter name=""dualColumnLayout"">True</booleanparameter>
      <booleanparameter name=""showCalculatorButton"" />
      <booleanparameter name=""displayVerklankingOnTheRight"">True</booleanparameter>
      <collectionparameter name=""numberOfAudioContentItems"">
        <definition id="""">
          <resourceparameter name=""audiocontent"" />
          <xhtmlparameter name=""description"" />
        </definition>
      </collectionparameter>
      <xhtmlparameter name=""leftBody"" />
      <xhtmlresourceparameter name=""leftSource"" />
      <integerparameter name=""sourceHeight"">200</integerparameter>
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemQuestion"">
        <p id=""c1-id-18"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-19"" class=""UserSRttsEngels"">Drag and drop</span>
          <br id=""c1-id-20"" />
        </p>
        <p id=""c1-id-21"" xmlns=""http://www.w3.org/1999/xhtml"">
          <strong id=""c1-id-22"">
            <span id=""c1-id-23"" class=""UserSRttsEngels"">Drag the words to the correct category. </span>
          </strong>
        </p>
        <p id=""c1-id-24"" xmlns=""http://www.w3.org/1999/xhtml"">
          <span id=""c1-id-26"" class=""UserSRttsEngels"">Each category should contain two words.</span>
          <strong id=""c1-id-25"">
            <br id=""c1-id-27"" />
          </strong>
        </p>
      </xhtmlparameter>
      <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
        <subparameterset id=""A"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Foo</gapTextParameter>
        </subparameterset>
        <subparameterset id=""B"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Bar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""C"">
          <gapTextParameter name=""gapText"" matchMax=""1"">Mayhaps</gapTextParameter>
        </subparameterset>
        <subparameterset id=""D"">
          <gapTextParameter name=""gapText"" matchMax=""1"">FooBar</gapTextParameter>
        </subparameterset>
        <subparameterset id=""E"">
          <gapTextParameter name=""gapText"" matchMax=""1"">bada</gapTextParameter>
        </subparameterset>
        <subparameterset id=""F"">
          <gapTextParameter name=""gapText"" matchMax=""1"">green</gapTextParameter>
        </subparameterset>
        <definition id="""">
          <gapTextParameter name=""gapText"" matchMax=""1"" />
        </definition>
        <xhtmlParameter name=""itemInlineInput"">
          <table style=""BORDER-COLLAPSE: collapse; "" width=""100%"" id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
            <colgroup id=""c1-id-12"">
              <col id=""c1-id-13"" />
              <col id=""c1-id-14"" />
              <col id=""c1-id-15"" />
            </colgroup>
            <tbody id=""c1-id-16"">
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-17"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-18"">
                  <p id=""c1-id-19"">order</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-20"">
                  <p id=""c1-id-21"">comparison</p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-22"">
                  <p id=""c1-id-23"">emphasis</p>
                </td>
              </tr>
              <tr style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"" id=""c1-id-24"">
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-25"">
                  <p id=""c1-id-26"">
                    <cito:InlineElement id=""I877c4041-88c3-402d-852d-953eb9c49f9a"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I877c4041-88c3-402d-852d-953eb9c49f9a</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">1</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-27"">
                    <cito:InlineElement id=""I812c5637-bbce-4144-bfc1-3c6dca9b1fd3"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I812c5637-bbce-4144-bfc1-3c6dca9b1fd3</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">4</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-28"">
                  <p id=""c1-id-29"">
                    <cito:InlineElement id=""I6d1dddae-0d7e-4645-adf5-382990e43bcf"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I6d1dddae-0d7e-4645-adf5-382990e43bcf</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">2</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-30"">
                    <cito:InlineElement id=""I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I1a2c2386-f4b0-4ce2-9df6-1689b7ef4267</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">5</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
                <td style=""BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"" id=""c1-id-31"">
                  <p id=""c1-id-32"">
                    <cito:InlineElement id=""I086eb640-dd2f-45d5-aa12-c021b1dbc9e5"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I086eb640-dd2f-45d5-aa12-c021b1dbc9e5</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">3</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement>
                  </p>
                  <p id=""c1-id-33"">
                    <cito:InlineElement id=""I43b05777-5ad8-484e-adf4-225364eb833f"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
                      <cito:parameters>
                        <cito:parameterSet id=""entireItem"">
                          <cito:plaintextparameter name=""inlineGapMatchId"">I43b05777-5ad8-484e-adf4-225364eb833f</cito:plaintextparameter>
                          <cito:plaintextparameter name=""inlineGapMatchLabel"">6</cito:plaintextparameter>
                          <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                          <cito:integerparameter name=""width"" />
                          <cito:integerparameter name=""height"" />
                        </cito:parameterSet>
                      </cito:parameters>
                    </cito:InlineElement> </p>
                </td>
              </tr>
            </tbody>
          </table>
          <p id=""c1-id-34"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
        </xhtmlParameter>
      </gapMatchScoringParameter>
      <xhtmlparameter name=""itemInlineInput"" />
      <xhtmlparameter name=""itemGeneral"" />
      <collectionparameter name=""choices"">
        <definition id="""">
          <plaintextparameter name=""choice"" />
          <integerparameter name=""nrOfConnections"" />
        </definition>
      </collectionparameter>
    </parameterSet>
  </parameters>
</assessmentItem>");

    }
}
