
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    /// <summary>
    /// Tests basic Factory functionality
    /// </summary>
    [TestClass]
    public class StringScoringFactoryTests : Runner 
    {
        public StringScoringFactoryTests()
            : base(new ScoreFactoryBoilerplate<StringScoringFactory, StringScoringParameter, StringGapViewModel>())
        {
            decoree.CreateViewModel = MakeViewModel;
        }

        private StringGapViewModel MakeViewModel(Cinch.TestViewAwareStatus fakeVAS)
        {
            return new StringGapViewModel(fakeVAS, A.Fake <ICurrentItemEditorContext>());
        }
    }
}
