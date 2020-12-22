using System;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface IResourcePropertyDialogService
    {
        bool Show(Guid resourceEntityId, Type type, int initialTabIndex = 0);
    }
}
