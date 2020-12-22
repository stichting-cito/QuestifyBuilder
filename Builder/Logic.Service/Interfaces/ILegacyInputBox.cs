using System;
using Cito.Tester.Common;

namespace Questify.Builder.Logic.Service.Interfaces
{

    public interface ILegacyInputBox
    {
        InputBoxResult Show(string prompt, bool masked);

        InputBoxResult Show(string prompt, bool masked, string title);

        InputBoxResult Show(string prompt, bool masked, string title, string def);

        InputBoxResult Show(string prompt, bool masked, string title, string def, Func<string, string> validator);

        InputBoxResult Show(string prompt, bool masked, string title, string def, Func<string, string> validator, int XPos, int YPos);
    }
}
