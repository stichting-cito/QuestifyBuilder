using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.DataTemplate
{
    [TestClass]
    public class ScoreEditorSelectorTests : UsesTheItemEditorVM
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void ValidateResult_ItemDoesNotImplement_IBlockRowViewModel_noResult()
        {
            var dataTemplateSelector = new ScoreEditorSelector();
            var result = dataTemplateSelector.SelectTemplate(new SomeDataContextClass(), new TextBlock());
            Assert.IsNull(result, "No result expected");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void ValidateResult_IBlockRowViewModel_DefaultResult()
        {
            var dataTemplateSelector = new ScoreEditorSelector();
            var result = dataTemplateSelector.SelectTemplate(new BlockRowViewModel_Without_DataTemplate(), new TextBlock());
            Assert.IsInstanceOfType(result, typeof(System.Windows.DataTemplate), "Expected a Datatemplate");
            System.Windows.DataTemplate d = (System.Windows.DataTemplate)result;
        }


        internal override void SetFakeViewDataContext(ref Cinch.IWorkSpaceAware fakeView, global::Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.IItemEditorViewModel itemEditorViewModel)
        {

        }

        protected override IEnumerable<ComposablePartCatalog> GetTypesForInjection()
        {
            return new List<ComposablePartCatalog>();
        }
    }
}
