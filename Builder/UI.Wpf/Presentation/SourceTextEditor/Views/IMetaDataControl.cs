using Cinch;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views
{
    interface IMetaDataControl : IWorkSpaceAware
    {
        void Update(ResourceEntity resourceEntity);
        void PreSaveTasks();
    }
}
