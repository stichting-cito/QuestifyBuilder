using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    public class PresentationDataContext
    {
        public ParameterSetCollection Parameters { get; set; }
        public ItemResourceEntity ItemResource { get; set; }
        public ResourceManagerBase ResourceManager { get; set; }
    }
}
