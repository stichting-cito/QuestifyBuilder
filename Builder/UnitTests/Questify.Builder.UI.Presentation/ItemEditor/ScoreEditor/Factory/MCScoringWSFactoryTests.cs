
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class MCScoringWSFactoryTests : Runner
    {

        /// <summary>
        /// Tests basic Factory functionality
        /// </summary>
        public MCScoringWSFactoryTests()
            : base(new ScoreFactoryBoilerplate<MCScoringVWFactory, MultiChoiceScoringParameter, MCScoringViewModel>())
        {
            decoree.CreateViewModel = MakeViewModel;
        }

        private MCScoringViewModel MakeViewModel(Cinch.TestViewAwareStatus fakeVAS)
        {
            return new MCScoringViewModel(fakeVAS);
        }

    }
}
