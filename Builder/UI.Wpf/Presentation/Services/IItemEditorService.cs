using System;
using System.Collections.Generic;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface IItemEditorService
    {

        void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward, bool asSecondItem, Guid itemToCloseEntityId);

        void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward, bool asSecondItem);

        void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward, bool asSecondItem, bool canChangeCode);

        void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward);

        void Make_NewItem(Guid itemLayoutTemplateId, int bankId, bool canMoveBackward, bool canMoveForward);

        bool FocusItem(Guid itemEntityId);

        List<Guid> ItemsToBeOpen { get; }
    }
}
