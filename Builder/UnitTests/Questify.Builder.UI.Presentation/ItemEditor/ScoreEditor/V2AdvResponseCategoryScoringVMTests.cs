
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{

    [TestClass]
    public class V2AdvResponseCategoryScoringVMTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void BlockGridIsNotNull()
        {
            var solution = Deserialize<Solution>(SingleSolution);
            var intSP = new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "integerScore" }.AddSubParameters("1");
            var combinedScoreMapKeys = new ScoringMap(new ScoringParameter[] { intSP }, solution).GetMap();

            var vm = new V2AdvResponseCategoryScoringVM(combinedScoreMapKeys.First(), solution);
            vm.AddBlockGridData();

            Assert.IsNotNull(vm.BlockGridData);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void BlockGrid_SingelSolution_HasSingleRow_SingleBlock()
        {
            var solution = Deserialize<Solution>(SingleSolution);
            var intSP = new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "integerScore" }.AddSubParameters("1");
            var combinedScoreMapKeys = new ScoringMap(new ScoringParameter[] { intSP }, solution).GetMap();

            var vm = new V2AdvResponseCategoryScoringVM(combinedScoreMapKeys.First(), solution);
            vm.AddBlockGridData();

            Assert.AreEqual(1, vm.BlockGridData.Count, "Expects single row");
            Assert.AreEqual(1, vm.BlockGridData[0].Count, "Expects Single Block");
            Assert.AreEqual(1, vm.BlockGridData[0][0].Count, "Expects Single Row in block");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void With2ScoringParams_AddingAltToInt1_TheOnlyInt2_BlockVM_ShouldHaveIndex1()
        {
            var solution = new Solution();
            var sp1 = new IntegerScoringParameter() { FindingOverride = "Finding", ControllerId = "Int1", Name = "Integer1", SupportCasScoring = true }.AddSubParameters("1");
            var sp2 = new IntegerScoringParameter() { FindingOverride = "Finding", ControllerId = "Int2", Name = "Integer2", SupportCasScoring = true }.AddSubParameters("1");
            var combinedScoringMap =
                CombinedScoringMapKey.Create(
                    new ScoringMapKey[] { new ScoringMapKey(sp1, "1"), new ScoringMapKey(sp2, "1") }, new Int32[] { 0 });

            var vm = new V2AdvResponseCategoryScoringVM(combinedScoringMap, solution);
            vm.AddBlockGridData();
            var blockInRowData = vm.BlockGridData[0][0];
            var blockRowViewModel = getBlockRowVmLstList(blockInRowData, "Integer1").First();
            vm.AddScoreAlternative(blockInRowData, blockRowViewModel, solution);
            var lst = getBlockRowVmLstList(blockInRowData, "Integer2");
            Assert.AreEqual(1, lst.Count);
            Assert.AreEqual(0, lst[0].Index);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void WhenAddingANewAlternative_TheLocationInsertedIsTheNextLocation()
        {
            var solution = Deserialize<Solution>(integerSolution);
            var sp1 = new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "Integer1", SupportCasScoring = true }.AddSubParameters("1");

            var combinedScoringMap =
                CombinedScoringMapKey.Create(new ScoringMapKey[] { new ScoringMapKey(sp1, "1") });

            var vm = new V2AdvResponseCategoryScoringVM(combinedScoringMap, solution);
            vm.AddBlockGridData();
            var blockInRowData = vm.BlockGridData[0][0];
            var blockRowViewModel = getBlockRowVmLstList(blockInRowData, "Integer1")[1]; vm.AddScoreAlternative(blockInRowData, blockRowViewModel, solution);
            var lst = GetValues(blockInRowData, "Integer1", solution);
            Assert.AreEqual(5, lst.Count);

            Assert.AreEqual(1, lst[0].Value.IntegerValue);
            Assert.AreEqual(2, lst[1].Value.IntegerValue);
            Assert.AreEqual(null, lst[2].Value.IntegerValue);
            Assert.AreEqual(3, lst[3].Value.IntegerValue);
            Assert.AreEqual(4, lst[4].Value.IntegerValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void WhenAddingANewAlternative_AllIndexesAreDistinct()
        {
            var solution = Deserialize<Solution>(integerSolution);
            var sp1 = new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "Integer1", SupportCasScoring = true }.AddSubParameters("1");

            var combinedScoringMap =
                CombinedScoringMapKey.Create(new ScoringMapKey[] { new ScoringMapKey(sp1, "1") });

            var vm = new V2AdvResponseCategoryScoringVM(combinedScoringMap, solution);
            vm.AddBlockGridData();
            var blockInRowData = vm.BlockGridData[0][0];
            var blockRowViewModel = getBlockRowVmLstList(blockInRowData, "Integer1")[1]; vm.AddScoreAlternative(blockInRowData, blockRowViewModel, solution);
            var lst = getBlockRowVmLstList(blockInRowData, "Integer1").Select(x => x.Index).Distinct();
            Assert.AreEqual(5, lst.Count());
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void WhenAddingANewAlternativeToAGroup_ValidateValues()
        {
            var solution = Deserialize<Solution>(integerGroupedSolution);
            var sp1 = new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "Integer1", SupportCasScoring = true }.AddSubParameters("1", "2");

            var combinedScoringMap = new ScoringMap(new[] { sp1 }, solution).GetMap().First();

            var vm = new V2AdvResponseCategoryScoringVM(combinedScoringMap, solution);
            vm.AddBlockGridData();

            var blockInRowData = vm.BlockGridData[0][0];
            var blockRowViewModel = getBlockRowVmLstList(blockInRowData, "Integer1.1")[0]; vm.AddScoreAlternative(blockInRowData, blockRowViewModel, solution);


            var lst1 = GetValues(blockInRowData, "Integer1.1", solution);
            var lst2 = GetValues(blockInRowData, "Integer1.2", solution);

            Assert.AreEqual(2, lst1.Count);
            Assert.AreEqual(6, lst1[0].Value.IntegerValue);
            Assert.AreEqual(null, lst1[1].Value.IntegerValue);

            Assert.AreEqual(1, lst2.Count);
            Assert.AreEqual(14, lst2[0].Value.IntegerValue);
        }


        private XElement SingleSolution = XElement.Parse(@"<solution>
	                                                        <keyFindings>
		                                                        <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
			                                                        <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
				                                                        <keyValue domain=""integerScore"" occur=""1"">
					                                                        <integerValue>
						                                                        <typedValue>42</typedValue>
					                                                        </integerValue>
				                                                        </keyValue>
			                                                        </keyFact>
		                                                        </keyFinding>
	                                                        </keyFindings>
                                                        </solution>");

        private XElement integerSolution = XElement.Parse(@"<solution>
                                                                <keyFindings>
                                                                  <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
                                                                    <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                      <keyValue domain=""integerScore"" occur=""1"">
                                                                        <integerValue>
                                                                          <typedValue>1</typedValue>
                                                                        </integerValue>
                                                                        <integerValue>
                                                                          <typedValue>2</typedValue>
                                                                        </integerValue>
                                                                        <integerValue>
                                                                          <typedValue>3</typedValue>
                                                                        </integerValue>
                                                                        <integerValue>
                                                                          <typedValue>4</typedValue>
                                                                        </integerValue>
                                                                      </keyValue>
                                                                    </keyFact>
                                                                  </keyFinding>
                                                                </keyFindings>   
                                                            </solution>");


        private XElement integerGroupedSolution = XElement.Parse(@"<solution>
                                                                    <keyFindings>
                                                                      <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
                                                                        <keyFactSet>
                                                                          <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                            </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>14</typedValue>
                                                                              </integerValue>
                                                                            </keyValue>
                                                                          </keyFact>
                                                                        </keyFactSet>
                                                                      </keyFinding>
                                                                    </keyFindings> 
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

        private List<IBlockRowViewModel> getBlockRowVmLstList(Cinch.ObservableDictionary<string, IEnumerable> blockInRowData, string key)
        {
            return blockInRowData[key].Cast<IBlockRowViewModel>().ToList();
        }

        private List<GapValue<MultiType>> GetValues(ObservableDictionary<string, IEnumerable> blockInRowData, string key, Solution solution)
        {
            return getBlockRowVmLstList(blockInRowData, key)
                .Select(x => ((IntegerScoringParameter)x.ScoringParameter).GetScoreManipulator(solution)
                    .GetValue(x.ScoreKey, x.Index)).ToList();
        }

    }
}
