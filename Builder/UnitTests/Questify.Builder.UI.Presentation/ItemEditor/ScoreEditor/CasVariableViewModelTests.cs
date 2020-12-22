﻿
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class CasVariableViewModelTests
    {
        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS"), WorkItem(27705)]
        public void MaxNrOfParametersEquals5()
        {
            var vm = new CasVariableViewModel();
            Assert.AreEqual(5, vm.NrOfParamsList.Count);
        }

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void DefaultConstructor_TwoRowsAreCreated()
        {
            var vm = new CasVariableViewModel();
            Assert.AreEqual(2, vm.DataRows.Count);
        }

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void DefaultConstructor_EachRowHasTwoValues()
        {
            var vm = new CasVariableViewModel();
            Assert.IsTrue(vm.DataRows.All(kvp => kvp.Data.Count == 2));
        }

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void SetTo5Variables()
        {
            var vm = new CasVariableViewModel();
            vm.CurrentNrOfParams.DataValue = 5;
            Assert.AreEqual(5, vm.DataRows.Count);
        }

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void SetTo1Variables()
        {
            var vm = new CasVariableViewModel();
            vm.CurrentNrOfParams.DataValue = 1;
            Assert.AreEqual(1, vm.DataRows.Count);
        }

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void SetTo6Solutions()
        {
            var vm = new CasVariableViewModel();
            vm.CurrentNrOfSolutions.DataValue = 6;
            Assert.IsTrue(vm.DataRows.All(kvp => kvp.Data.Count == 6));
        }

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void SetTo1Solutions()
        {
            var vm = new CasVariableViewModel();
            vm.CurrentNrOfSolutions.DataValue = 1;
            Assert.IsTrue(vm.DataRows.All(kvp => kvp.Data.Count == 1));
        }


        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void FromString_Empty_SettingsShouldBeSameForAsDefault()
        {
            var vmDefault = new CasVariableViewModel();
            var vm = CasVariableViewModel.CreateFor(string.Empty);
            Assert.AreEqual(vmDefault.CurrentNrOfParams.DataValue, vm.CurrentNrOfParams.DataValue);
            Assert.AreEqual(vmDefault.CurrentNrOfSolutions.DataValue, vm.CurrentNrOfSolutions.DataValue);
        }


        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
        public void FromString_Example()
        {

            var vm = CasVariableViewModel.CreateFor("{x:4,y:4,z:4}{x:3,y:4,z:4}{x:4,y:4,z:4}{x:4,y:4,z:4}{x:4,y:4,z:4}");
            Assert.AreEqual(3, vm.CurrentNrOfParams.DataValue, "For x,y and z");
            Assert.AreEqual(5, vm.CurrentNrOfSolutions.DataValue);
        }

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
#if DEBUG
        [ExpectedException(typeof(ArgumentException))]
#endif
        public void FromString_AlmostValidExample()
        {
            var vm = CasVariableViewModel.CreateFor("{x:4,y:4,z:4}{x:3,y:4,z:4}{x:4,y:4,z:4}{x:4,y:4,z:4}{x:4,y:4}");
        }


        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("CAS")]
#if DEBUG
        [ExpectedException(typeof(ArgumentException))]
#endif
        public void FromString_GarbageExample()
        {
            var vm = CasVariableViewModel.CreateFor("k&ntrn23p8chLB7uy3h5jtbpa8 UASP9824Y5");
        }
    }
}
