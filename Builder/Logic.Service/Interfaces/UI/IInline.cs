using System.Collections.Generic;

namespace Questify.Builder.Logic.Service.Interfaces.UI
{

    public interface IInline
    {
        bool CanAddInline { get; }

        int InlineControlCount { get; }

        IEnumerable<string> InlineControls { get; }

        void CreateInline(string title);
        string GetInlineTemplate(string name);

        string InlineIcon(string inlineTemplate);
    }
}
