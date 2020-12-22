using System;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface ICurrentItemEditorContext
    {

        bool IsActive { get; }

        string Title { get; }

        int BankIdentifier { get; }

        Guid ItemLayoutTemplateId { get; }

    }
}
