using System;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface ISourceTextEditorService
    {
        void Show(Guid sourceTextEntityId);

        void ShowDialog(Guid sourceTextEntityId);

        void Make_NewSourceTextTemplate(int bankId, bool makeSourceTextTemplate);
    }
}
