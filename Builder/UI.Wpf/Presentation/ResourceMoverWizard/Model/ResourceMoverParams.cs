using System;
using Questify.Builder.Logic.ContentModel;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.Model
{
    public class ResourceMoverParams : IResourceMoverWizardParams
    {
        public int SourceBankId { get; set; }

        public Guid[] ResourcesToMoveIds { get; set; }

        public int TargetBankId { get; set; }

        public ResourceAndCustomBankPropertyMoveResult MovingResult { get; set; }
    }
}
