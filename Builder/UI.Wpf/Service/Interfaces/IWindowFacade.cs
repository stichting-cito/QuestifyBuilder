using System;
using System.Collections.Generic;

namespace Questify.Builder.UI.Wpf.Service.Interfaces
{
    public interface IWindowFacade
    {
        void OpenItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward, Guid previousItemId);

        void OpenItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward, bool canChangeCode);

        void OpenSecondItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward, Guid previousItemId);

        void OpenSecondItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward);

        void CreateNewItem(Guid itemLayoutTemplateId, int bankId, bool canMoveBackward, bool canMoveForward, Guid previousItemId);

        bool OpenResourcePropertyDialog(Guid id, Type type, int tabIndex = 0);
        void OpenSourceTextEditorById(Guid id);
        void OpenSourceTextEditorDialogById(Guid id);
        void CreateNewSourceText(int bankId, bool makeSourceTextTemplate);

        bool OpenMajorVersionDialog(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity entity);

        bool? ShowResourceMoverWizard(int sourceBankId, Guid[] resourcesToMoveIds);

        void OpenAnnouncementDialog();

        bool FocusItem(Guid itemEntityId);

        List<Guid> ItemsToBeOpen { get; }
    }
}
