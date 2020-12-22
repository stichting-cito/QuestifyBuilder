using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface IMajorVersionDialogService
    {
        bool Show(ResourceEntity versionableEntity);
    }
}
