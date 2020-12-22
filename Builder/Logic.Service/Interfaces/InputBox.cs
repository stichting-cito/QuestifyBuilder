using System;
using Cito.Tester.Common;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public class InputBox : ILegacyInputBox
    {
        public InputBoxResult Show(string prompt, bool masked)
        {
            return Cito.Tester.Common.InputBox.Show(prompt, masked);
        }

        public InputBoxResult Show(string prompt, bool masked, string title)
        {
            return Cito.Tester.Common.InputBox.Show(prompt, masked, title);
        }

        public InputBoxResult Show(string prompt, bool masked, string title, string def)
        {
            return Cito.Tester.Common.InputBox.Show(prompt, masked, title, def);
        }

        public InputBoxResult Show(string prompt, bool masked, string title, string def, Func<string, string> validator)
        {
            return Cito.Tester.Common.InputBox.Show(prompt, masked, title, def, new Cito.Tester.Common.InputBox.InputValidator(e => validator(e)));
        }

        public InputBoxResult Show(string prompt, bool masked, string title, string def, Func<string, string> validator, int XPos, int YPos)
        {
            return Cito.Tester.Common.InputBox.Show(prompt, masked, title, def, new Cito.Tester.Common.InputBox.InputValidator(e => validator(e)), XPos, YPos);
        }
    }
}
