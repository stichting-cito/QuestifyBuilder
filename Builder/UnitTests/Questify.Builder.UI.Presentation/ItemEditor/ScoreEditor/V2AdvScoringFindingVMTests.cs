
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.HelperClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class V2AdvScoringFindingVMTests
    {


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void TestFactSetNumberOfBlockRowVM_newSolution()
        {
            //Arrange
            var solution = new Solution();
            var integerSP =
                new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "Int" }
                    .AddSubParameters("1", "2", "3");
            var vm =
                new V2AdvScoringFindingVM(
                    new CreateObjectJIT<KeyFinding>(() => solution.GetFindingOrMakeIt("sharedIntegerFinding")), new[] { integerSP },
                    solution);
            //Act
            vm.AddBlockGridData();
            //Assert

            Assert.AreEqual(3, vm.BlockGridData.Count);

            Assert.AreEqual(1, vm.BlockGridData[0].Count);
            Assert.AreEqual(1, vm.BlockGridData[0][0].Count);
            Assert.AreEqual(null, lst(vm.BlockGridData[0][0], "Int.1")[0].FactSetNumber);

            Assert.AreEqual(1, vm.BlockGridData[1].Count);
            Assert.AreEqual(1, vm.BlockGridData[1][0].Count);
            Assert.AreEqual(null, lst(vm.BlockGridData[1][0], "Int.2")[0].FactSetNumber);

            Assert.AreEqual(1, vm.BlockGridData[2].Count);
            Assert.AreEqual(1, vm.BlockGridData[2][0].Count);
            Assert.AreEqual(null, lst(vm.BlockGridData[2][0], "Int.3")[0].FactSetNumber);

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void TestFactSetNumberOfBlockRowVM_solutionWith2IntPrmsIn1Grp()
        {
            //Arrange
            var solution = solutionWith2IntPrmsIn1Grp.Deserialize<Solution>();
            var integerSP =
                new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "Int" }
                    .AddSubParameters("1", "2", "3");
            var vm =
                new V2AdvScoringFindingVM(
                    new CreateObjectJIT<KeyFinding>(() => solution.GetFindingOrMakeIt("sharedIntegerFinding")), new[] { integerSP },
                    solution);
            //Act
            vm.AddBlockGridData();
            //Assert

            Assert.AreEqual(2, vm.BlockGridData.Count);

            Assert.AreEqual(1, vm.BlockGridData[0].Count);
            Assert.AreEqual(2, vm.BlockGridData[0][0].Count);
            Assert.AreEqual(0, lst(vm.BlockGridData[0][0], "Int.1")[0].FactSetNumber);
            Assert.AreEqual(0, lst(vm.BlockGridData[0][0], "Int.2")[0].FactSetNumber);

            Assert.AreEqual(1, vm.BlockGridData[1].Count);
            Assert.AreEqual(1, vm.BlockGridData[1][0].Count);
            Assert.AreEqual(null, lst(vm.BlockGridData[1][0], "Int.3")[0].FactSetNumber);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void Test_ScoringParameterWithMultipleSubParameters_IsSubfixedWithSubParameterId()
        {
            //Arrange
            var solution = new Solution();
            var integerSP =
                new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "Int" }
                    .AddSubParameters("1", "2", "3");
            var vm =
                new V2AdvScoringFindingVM(
                    new CreateObjectJIT<KeyFinding>(() => solution.GetFindingOrMakeIt("sharedIntegerFinding")), new[] { integerSP },
                    solution);
            //Act
            vm.AddBlockGridData();

            //Assert
            Assert.AreEqual("Int.1", ((List<IBlockRowViewModel>)vm.BlockGridData[0][0].Values.First()).First().Name);
            Assert.AreEqual("Int.2", ((List<IBlockRowViewModel>)vm.BlockGridData[1][0].Values.First()).First().Name);
            Assert.AreEqual("Int.3", ((List<IBlockRowViewModel>)vm.BlockGridData[2][0].Values.First()).First().Name);

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void Test_ScoringParameterWithOneSubParameter_IsNotSubfixedWithSubParameterId()
        {
            //Arrange
            var solution = new Solution();
            var integerSP =
                new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore", Name = "Int" }
                    .AddSubParameters("1");
            var vm =
                new V2AdvScoringFindingVM(
                    new CreateObjectJIT<KeyFinding>(() => solution.GetFindingOrMakeIt("sharedIntegerFinding")), new[] { integerSP },
                    solution);
            //Act
            vm.AddBlockGridData();

            //Assert
            Assert.AreEqual("Int", ((List<IBlockRowViewModel>)vm.BlockGridData[0][0].Values.First()).First().Name);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoSolutionAndOrderParameter_GridIsInitializedAsGroup()
        {
            //Arrange
            var solution = new Solution();
            var orderParam =
                new OrderScoringParameter() { FindingOverride = "sharedFinding", ControllerId = "global", Name = "order" }.AddSubParameters("A", "B", "C", "D");

            var vm =
                new V2AdvScoringFindingVM(
                    new CreateObjectJIT<KeyFinding>(() => solution.GetFindingOrMakeIt("sharedFinding")), new[] { orderParam },
                    solution);
            //Act
            vm.AddBlockGridData();
            //Assert
            Assert.AreEqual(1, vm.BlockGridData.Count, "Expects 1 BlockGridBlockRow");
            Assert.AreEqual(1, vm.BlockGridData[0].Count, "Expects 1 Block in Row");
            Assert.AreEqual(4, vm.BlockGridData[0][0].Count, "Expected to create 4 blockRows");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoSolutionAndOrderParameter_GridIsInitializedAsGroup_SetScore_GetConcept()
        {
            //Arrange
            var solution = new Solution();
            var orderParam =
                new OrderScoringParameter() { FindingOverride = "sharedFinding", ControllerId = "global", Name = "order" }.AddSubParameters("A", "B", "C", "D");
                    
            var vm =
                new V2AdvScoringFindingVM(
                    new CreateObjectJIT<KeyFinding>(() => solution.GetFindingOrMakeIt("sharedFinding")), new[] { orderParam },
                    solution);

            vm.AddBlockGridData();
            
            var viewModels = new List<OrderBlockRowViewModel>();
            foreach (var values in vm.BlockGridData[0][0].Values)
            {
                foreach (var value in values)
                {
                    viewModels.Add((OrderBlockRowViewModel)value);
                }
            }
            
            //Act
            viewModels[0].ScoreManipulator.SetKey("A",1);
            viewModels[1].ScoreManipulator.SetKey("B", 2);
            new ScoringMap(new[] {orderParam}, solution).GetMap().First().GetConceptManipulator(solution);
            //Assert
            Assert.AreEqual(ExpectedResultOrder1.ToString(), solution.DoSerialize().ToString());
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoSolutionAndOrderParameter_GridIsInitializedAsGroup_GetConcept_SetScore()
        {
            //Arrange
            var solution = new Solution();
            var orderParam =
                new OrderScoringParameter() { FindingOverride = "sharedFinding", ControllerId = "global", Name = "order" }.AddSubParameters("A", "B", "C", "D");

            var vm =
                new V2AdvScoringFindingVM(
                    new CreateObjectJIT<KeyFinding>(() => solution.GetFindingOrMakeIt("sharedFinding")), new[] { orderParam },
                    solution);

            vm.AddBlockGridData();

            var viewModels = new List<OrderBlockRowViewModel>();
            foreach (var values in vm.BlockGridData[0][0].Values)
            {
                foreach (var value in values)
                {
                    viewModels.Add((OrderBlockRowViewModel)value);
                }
            }

            //Act
            new ScoringMap(new[] { orderParam }, solution).GetMap().First().GetConceptManipulator(solution);
            viewModels[0].ScoreManipulator.SetKey("A", 1);
            viewModels[1].ScoreManipulator.SetKey("B", 2);
            
            //Assert
            Assert.AreEqual(ExpectedResultOrder2.ToString(), solution.DoSerialize().ToString());
        }

        
        #region Data

        private readonly XElement solutionWith2IntPrmsIn1Grp =
            XElement.Parse(@"<solution>
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

        private readonly XElement ExpectedResultOrder1 = XElement.Parse(@"<solution xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                                              <keyFindings>
                                                                                <keyFinding id=""sharedFinding"" scoringMethod=""None"">
                                                                                  <keyFactSet>
                                                                                    <keyFact id=""A-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <keyValue domain=""global"" occur=""1"">
                                                                                        <integerValue>
                                                                                          <typedValue>1</typedValue>
                                                                                        </integerValue>
                                                                                      </keyValue>
                                                                                    </keyFact>
                                                                                    <keyFact id=""B-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <keyValue domain=""global"" occur=""1"">
                                                                                        <integerValue>
                                                                                          <typedValue>2</typedValue>
                                                                                        </integerValue>
                                                                                      </keyValue>
                                                                                    </keyFact>
                                                                                  </keyFactSet>
                                                                                </keyFinding>
                                                                              </keyFindings>
                                                                              <conceptFindings>
                                                                                <conceptFinding id=""sharedFinding"" scoringMethod=""None"">
                                                                                  <conceptFactSet>
                                                                                    <conceptFact id=""A-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <integerValue>
                                                                                          <typedValue>1</typedValue>
                                                                                        </integerValue>
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                    <conceptFact id=""B-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <integerValue>
                                                                                          <typedValue>2</typedValue>
                                                                                        </integerValue>
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                  </conceptFactSet>
                                                                                  <conceptFactSet>
                                                                                    <conceptFact id=""A[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                    <conceptFact id=""B[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                    <conceptFact id=""C[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                    <conceptFact id=""D[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                  </conceptFactSet>
                                                                                </conceptFinding>
                                                                              </conceptFindings>
                                                                              <aspectReferences />
                                                                            </solution>");

        private readonly XElement ExpectedResultOrder2 = XElement.Parse(@"<solution xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                                              <keyFindings>
                                                                                <keyFinding id=""sharedFinding"" scoringMethod=""None"">
                                                                                  <keyFactSet>
                                                                                    <keyFact id=""A-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <keyValue domain=""global"" occur=""1"">
                                                                                        <integerValue>
                                                                                          <typedValue>1</typedValue>
                                                                                        </integerValue>
                                                                                      </keyValue>
                                                                                    </keyFact>
                                                                                    <keyFact id=""B-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <keyValue domain=""global"" occur=""1"">
                                                                                        <integerValue>
                                                                                          <typedValue>2</typedValue>
                                                                                        </integerValue>
                                                                                      </keyValue>
                                                                                    </keyFact>
                                                                                  </keyFactSet>
                                                                                </keyFinding>
                                                                              </keyFindings>
                                                                              <conceptFindings>
                                                                                <conceptFinding id=""sharedFinding"" scoringMethod=""None"">
                                                                                  <conceptFactSet />
                                                                                  <conceptFactSet>
                                                                                    <conceptFact id=""A[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                    <conceptFact id=""B[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                    <conceptFact id=""C[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                    <conceptFact id=""D[*]-global"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                      <conceptValue domain=""global"" occur=""1"">
                                                                                        <catchAllValue />
                                                                                      </conceptValue>
                                                                                    </conceptFact>
                                                                                  </conceptFactSet>
                                                                                </conceptFinding>
                                                                              </conceptFindings>
                                                                              <aspectReferences />
                                                                            </solution>");


        #endregion

        private List<IBlockRowViewModel> lst(Cinch.ObservableDictionary<string, System.Collections.IEnumerable> observableDictionary, string key)
        {
            return (List<IBlockRowViewModel>)observableDictionary[key];
        }

    }
}
