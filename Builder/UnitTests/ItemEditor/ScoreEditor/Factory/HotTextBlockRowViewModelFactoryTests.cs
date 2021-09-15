
using System;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class HotTextBlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithSingleParamCollection_NoSolution_CreatesSingleBlockRowVM()
        {
            //Arrange
            var solution = new Solution(); 
            var param = new HotTextScoringParameter() { Name = "hottext", ControllerId = "HT" , HotTextText = new XHtmlParameter() { Value = HotTextTextWithOneInlineHottextElement.ToString() } };

            //Act
            CombinedScoringMapKey combinedKey = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedKey, solution)
                .ToList();
            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreatedTypeIs_HotTextBlockRowViewModel()
        {
            //Arrange
            var solution = new Solution();
            var param = new HotTextScoringParameter() { Name = "hottext", ControllerId = "HT", HotTextText = new XHtmlParameter() { Value = HotTextTextWithOneInlineHottextElement.ToString() } };

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
                .ToList();
            //Assert
            Assert.IsInstanceOfType(result.First(), typeof(HotTextBlockRowViewModel));
        }

        /// <summary>
        /// ScoringParameter with three parameter collection_ no solution_ creates three block row vm.
        /// </summary>
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithThreeParamCollection_NoSolution_CreatesThreeBlockRowVM()
        {
            //Arrange
            var solution = new Solution();
            var param = new HotTextScoringParameter() { Name = "hottext", ControllerId = "HT", HotTextText = new XHtmlParameter() { Value = HotTextTextWithThreeInlineHottextElements.ToString() } };

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
                .ToList();
            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv"), ExpectedException(typeof(NotImplementedException))]
        public void InsertingBlockRowViewModelThrowsException()
        {
            //Arrange
            var solution = Deserialize<Solution>(SolutionData);
            var param = new HotTextScoringParameter() { FindingOverride = "sharedHotTextFinding", ControllerId = "HT", HotTextText = new XHtmlParameter() { Value = HotTextTextWithThreeInlineHottextElements.ToString() } };
            var viewModels = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0).ToList();
            //Act
            var result = BlockRowViewModelFactory.InsertInstance(param, viewModels[1].ScoreKey, 0, 1, solution);
            //Assert
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void TheContentLabelOfTheInlineElementsBecomeTheNameOfTheViewModel()
        {
            //Arrange
            var solution = new Solution();
            var param = new HotTextScoringParameter() { FindingOverride = "sharedHotTextFinding", ControllerId = "HT", HotTextText = new XHtmlParameter() { Value = HotTextTextWithOneInlineHottextElement.ToString() } };

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
                .ToList();

            //Assert
            Assert.AreEqual("Ruim 100 jaar lang werden alle huizen en kantoren ...", ((HotTextBlockRowViewModel)result[0]).Name);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void TheContentLabelOfTheInlineElementsIsHtmlDecoded()
        {
            //Arrange
            var solution = new Solution();
            var param = new HotTextScoringParameter() { FindingOverride = "sharedHotTextFinding", ControllerId = "HT", HotTextText = new XHtmlParameter() { Value = HotTextTextWithOneInlineHottextElementWithEncodedLabel.ToString() } };

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
                .ToList();

            //Assert
            Assert.AreEqual("This & that.", ((HotTextBlockRowViewModel)result[0]).Name);
        }

        private static XElement HotTextTextWithOneInlineHottextElement = 
            XElement.Parse(@"<p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
            <strong id=""c1-id-12"">
              Het einde van de gloeilamp<br id=""c1-id-13"" />
            </strong>
            <br id=""c1-id-14"" />
            <cito:InlineElement id=""Id123"" layoutTemplateSourceName=""InlineHottextLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:listedparameter name=""controlType"">hottext</cito:listedparameter>
                  <cito:plaintextparameter name=""controlId"">Id123</cito:plaintextparameter>
                  <cito:plaintextparameter name=""controlLabel"">Ruim 100 jaar lang werden alle huizen en kantoren ...</cito:plaintextparameter>
                  <cito:booleanparameter name=""addHottextCorrection"">False</cito:booleanparameter>
                  <cito:plaintextparameter name=""hottextValue"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement>
            <span id=""SId213789c-6ee7-45ee-a024-0b10a10f6375"" style=""background-color: #C7B8CE;"">Ruim 100 jaar lang werden alle huizen en kantoren ...</span>
            </p>"
            );

        private static XElement HotTextTextWithOneInlineHottextElementWithEncodedLabel =
     XElement.Parse(@"<p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
            <strong id=""c1-id-12"">
              Het einde van de gloeilamp<br id=""c1-id-13"" />
            </strong>
            <br id=""c1-id-14"" />
            <cito:InlineElement id=""Id123"" layoutTemplateSourceName=""InlineHottextLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:listedparameter name=""controlType"">hottext</cito:listedparameter>
                  <cito:plaintextparameter name=""controlId"">Id123</cito:plaintextparameter>
                  <cito:plaintextparameter name=""controlLabel"">This &amp; that.</cito:plaintextparameter>
                  <cito:booleanparameter name=""addHottextCorrection"">False</cito:booleanparameter>
                  <cito:plaintextparameter name=""hottextValue"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement>
            <span id=""SId213789c-6ee7-45ee-a024-0b10a10f6375"" style=""background-color: #C7B8CE;"">Ruim 100 jaar ...</span>
            </p>"
     );

        private static XElement HotTextTextWithThreeInlineHottextElements =
                        XElement.Parse(@"<p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
            <strong id=""c1-id-12"">
              Het einde van de gloeilamp<br id=""c1-id-13"" />
            </strong>
            <br id=""c1-id-14"" />
            <cito:InlineElement id=""Id123"" layoutTemplateSourceName=""InlineHottextLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:listedparameter name=""controlType"">hottext</cito:listedparameter>
                  <cito:plaintextparameter name=""controlId"">Id123</cito:plaintextparameter>
                  <cito:plaintextparameter name=""controlLabel"">Ruim 100 jaar lang ...</cito:plaintextparameter>
                  <cito:booleanparameter name=""addHottextCorrection"">False</cito:booleanparameter>
                  <cito:plaintextparameter name=""hottextValue"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement>
            <span id=""Id213789c-6ee7-45ee-a024-0b10a10f6375"" style=""background-color: #C7B8CE;"">Ruim 100 jaar lang...</span>
            <cito:InlineElement id=""Id456"" layoutTemplateSourceName=""InlineHottextLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:listedparameter name=""controlType"">hottext</cito:listedparameter>
                  <cito:plaintextparameter name=""controlId"">Id456</cito:plaintextparameter>
                  <cito:plaintextparameter name=""controlLabel""> De lampen waren ...</cito:plaintextparameter>
                  <cito:booleanparameter name=""addHottextCorrection"">False</cito:booleanparameter>
                  <cito:plaintextparameter name=""hottextValue"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement>
            <span id=""SIa1384693-9aac-4a5d-bb43-9c2b262e864a"" style=""background-color: #C7B8CE;""> De lampen waren ...</span>
            <cito:InlineElement id=""Id789"" layoutTemplateSourceName=""InlineHottextLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:listedparameter name=""controlType"">hottext</cito:listedparameter>
                  <cito:plaintextparameter name=""controlId"">Id789</cito:plaintextparameter>
                  <cito:plaintextparameter name=""controlLabel""> Ging er een ...</cito:plaintextparameter>
                  <cito:booleanparameter name=""addHottextCorrection"">False</cito:booleanparameter>
                  <cito:plaintextparameter name=""hottextValue"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement>
            <span id=""SIc80067a6-e42f-46ef-90d5-ed0afe150abc"" style=""background-color: #C7B8CE;""> Ging er een ...</span>
            </p>"
            );


        private static XElement SolutionData =
            XElement.Parse(@"<solution>
                                <keyFindings>
                                  <keyFinding id=""sharedOrderFinding"" scoringMethod=""Dichotomous"">
                                      <keyFact id=""Id123-HT"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""Id123-HT"" occur=""1"">
                                          <booleanValue>
                                            <typedValue>true</typedValue>
                                          </booleanValue>
                                        </keyValue>
                                      </keyFact>
                                  </keyFinding>
                                </keyFindings>
                                <aspectReferences />                                            
                                </solution>");


        private T Deserialize<T>(XElement input)
        {
            T ret = default(T);
            var s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var m = new System.IO.StringReader(input.ToString()))
            {
                ret = (T)s.Deserialize(m);
            }

            return ret;
        }




    }
}
