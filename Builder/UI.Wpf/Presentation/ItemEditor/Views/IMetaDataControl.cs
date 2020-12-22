using System;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    interface IMetaDataControl : IWorkSpaceAware, ICommandSupport
    {
        void Update(AssessmentItem assessmentItem, ResourceEntity resourceEntity);
        Action DoItemRename { get; set; }
        Action DoSwitchTemplate { get; set; }
        Action DoOpenResourcePropertyDialog { get; set; }
        void Dispose();
    }
}
