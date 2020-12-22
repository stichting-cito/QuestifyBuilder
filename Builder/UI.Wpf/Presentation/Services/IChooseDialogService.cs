using System.Collections.Generic;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface IChooseDialogService
    {
        object ShowSelection(string title, string description, IEnumerable<object> choosables);
    }
}
