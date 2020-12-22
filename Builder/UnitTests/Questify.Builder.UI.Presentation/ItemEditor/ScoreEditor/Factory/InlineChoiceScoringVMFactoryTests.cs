
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class InlineChoiceScoringVMFactoryTests : Runner
    {

        public InlineChoiceScoringVMFactoryTests()
    : base(new ScoreFactoryBoilerplate<InlineChoiceScoringVMFactory, InlineChoiceScoringParameter, InlineChoiceScoringViewModel>())
        {
            decoree.CreateViewModel = MakeViewModel;
        }

        private InlineChoiceScoringViewModel MakeViewModel(Cinch.TestViewAwareStatus fakeVAS)
        {
            return new InlineChoiceScoringViewModel(fakeVAS);
        }
    }
}
