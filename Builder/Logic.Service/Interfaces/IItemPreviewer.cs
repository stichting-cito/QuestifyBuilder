using System;
using System.Collections.Generic;
using System.Drawing;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.Classes;
using Questify.Builder.Logic.Service.EventArguments;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IItemPreviewer
    {
        event EventHandler<ItemValidationRequiredEventArgs> ItemValidatingRequired;
        event EventHandler<EventArgs> ItemRenderingCompleted;

        int? ContextIdentifierForItemViewer { get; set; }

        void DisposePreviewerEngine(IItemPreviewHandler handler);

        string PreviewItem(IItemPreviewHandler handler, int bankId, AssessmentItem assessmentItem, bool force);

        string CreateScreenshot(IItemPreviewHandler handler, int bankId, AssessmentItem assessmentItem, string screenshotPath, Size size, int sequenceNumber, List<PublicationProperty> publicationProperties);

        void StopItemPreview(IItemPreviewHandler handler);

        void ResetRenderer(IItemPreviewHandler handler);
    }
}
