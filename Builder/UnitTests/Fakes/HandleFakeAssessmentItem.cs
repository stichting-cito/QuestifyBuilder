using Cito.Tester.ContentModel;

namespace Questify.Builder.UnitTests.Fakes
{
    class HandleFakeAssessmentItem
    {

        internal void InitializeFakes(global::Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.IItemEditorViewModel _fake)
        {
            var a = new AssessmentItem();
            _fake.AssessmentItem.DataValue = a;
        }


    }
}
