
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ItemHarmonization.Implementation;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;
using Questify.Builder.UnitTests.Framework.Faketory.@interface;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.ItemHarmonization
{
    [TestClass]
    public class CustomInteractionHamonizationTests
    {
        [TestInitialize]
        public void Init()
        {
            FakeDal.Init();
        }

        [TestCleanup]
        public void Cleanup()
        {
            FakeDal.Deinit();
        }

        [TestMethod, TestCategory("Logic")]
        public void HarmonizeAnItemWithCiParameters()
        {
            //Arrange
            AddTemplates().IsUsedBy.Item("someItem", i => SetItemData(i, itemData));

            var items = ResourceFactory.Instance.GetItemsForBank(0);
            ItemResourceEntity item = (ItemResourceEntity)items.First();

            var x = new BaseTemplateHarmonization();
            //Act
            var result = x.Harmonize(item);
            //Assert
            var stringResult = item.ResourceData.BinData.AsString();
            Debug.WriteLine(stringResult);
            var assessment = item.ResourceData.BinData.To<AssessmentItem>();
            Assert.AreEqual(2,assessment.Parameters.Count,"Two parameter sets should be present.");
            Assert.AreEqual(itemData.ToString(),XElement.Parse(stringResult).ToString());
        }

        private IActionsAfter AddTemplates()
        {
            return FakeDal.Add.ControlTemplate("min.customInteraction", c => SetControlTemplateData(c, controltemplateCI))
                    .IsUsedBy
                    .ItemTemplate("ilt.customInteraction", i => SetItemTemplateData(i, itemlayoutTemplate));
        }

        private void SetItemTemplateData(ItemLayoutTemplateResourceEntity itemLayoutTemplateResourceEntity, XElement itemtemplate)
        {
            itemLayoutTemplateResourceEntity.ResourceData = new ResourceDataEntity { BinData = itemtemplate.ToBytes() };
        }

        private void SetControlTemplateData(ControlTemplateResourceEntity controlTemplateResourceEntity, XElement controltemplate)
        {
            controlTemplateResourceEntity.ResourceData = new ResourceDataEntity { BinData = controltemplate.ToBytes() };
        }

        private void SetItemData(ItemResourceEntity itemResourceEntity, XElement item)
        {
            itemResourceEntity.ResourceData = new ResourceDataEntity { BinData = item.ToBytes() };
        }


        #region DATA

        readonly XElement controltemplateCI = XElement.Parse(@"<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""2"">
                                                                  <Description></Description>
                                                                  <Targets>
                                                                    <Target xsi:type=""ControlTemplateTarget"" enabled=""true"" name=""ces"">
                                                                      <Template>
                                                                        <![CDATA[
				
				                                                                ]]>
                                                                      </Template>
                                                                      <ParameterSet id="""">
                                                                      </ParameterSet>
                                                                    </Target>
                                                                  </Targets>
                                                                  <SharedFunctions/>
                                                                  <SharedParameterSet>
                                                                    <custominteractionresourceparameter name=""source"">
                                                                      <designersetting key=""label"">Custom Interaction bestand</designersetting>
                                                                      <designersetting key=""description"">Selecteer het Custom Interaction bestand</designersetting>
                                                                      <designersetting key=""required"">true</designersetting>
                                                                      <designersetting key=""filter"">application/x-customInteraction</designersetting>
                                                                      <designersetting key=""group"">Custom Interaction</designersetting>
                                                                      <designersetting key=""sortkey"">1</designersetting>
                                                                    </custominteractionresourceparameter>
                                                                  </SharedParameterSet>
                                                                </Template>");

        private readonly XElement itemlayoutTemplate = XElement.Parse(@"<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""2"">
                                                                          <Targets>
                                                                            <Target xsi:type=""ItemLayoutTemplateTarget"" enabled=""true"" name=""ces"">
                                                                              <Template>
                                                                                <![CDATA[
				                                                                        <html>
					                                                                        <body>
						                                                                        <cito:control xmlns:cito=""http://www.cito.nl/citotester"" id=""invoer"" type=""min.customInteraction"" />
					                                                                        </body>
				                                                                        </html>
			                                                                        ]]>
                                                                              </Template>
                                                                            </Target>
                                                                          </Targets>
                                                                        </Template>");

        private readonly XElement itemData = XElement.Parse(@"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""2001"" title=""2001"" layoutTemplateSrc=""ilt.customInteraction"">
                                                              <solution>
                                                                <keyFindings />
                                                                <aspectReferences />
                                                              </solution>
                                                              <parameters>
                                                                <parameterSet id=""invoer"">
                                                                  <custominteractionresourceparameter name=""source"" scalable=""false"" interactioncount=""0"">SomeCi.ci</custominteractionresourceparameter>
                                                                </parameterSet>
                                                                <parameterSet id=""__CustomInteractions"" isDynamicCollection=""true"">
                                                                  <decimalScoringParameter label=""coordinate 1"" ControllerId=""CI_SP0"" findingOverride=""CustomInteractions"" integerPartMaxLength=""4"" fractionPartMaxLength=""1"">
                                                                    <subparameterset id=""X"" />
                                                                    <subparameterset id=""Y"" />
                                                                  </decimalScoringParameter>
                                                                  <decimalScoringParameter label=""coordinate 2"" ControllerId=""CI_SP1"" findingOverride=""CustomInteractions"" integerPartMaxLength=""4"" fractionPartMaxLength=""1"">
                                                                    <subparameterset id=""X"" />
                                                                    <subparameterset id=""Y"" />
                                                                  </decimalScoringParameter>
                                                                </parameterSet>
                                                              </parameters>                                                            
                                                            </assessmentItem>");

        #endregion
    }
}
