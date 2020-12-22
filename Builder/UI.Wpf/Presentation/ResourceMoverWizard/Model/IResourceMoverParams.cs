using System;
using Questify.Builder.Logic.ContentModel;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.Model
{
    public interface IResourceMoverWizardParams
    {
        int SourceBankId { get; set; }

        Guid[] ResourcesToMoveIds { get; set; }

        int TargetBankId { get; set; }

        ResourceAndCustomBankPropertyMoveResult MovingResult { get; set; }
    }
}
