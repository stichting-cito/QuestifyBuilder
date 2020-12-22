using System;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface IResourceMoverWizardService
    {
        bool? ShowDialog(int sourceBankId, Guid[] resourcesToMoveIds);
    }
}
