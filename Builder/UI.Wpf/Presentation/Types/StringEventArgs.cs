using System;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{

    public class StringEventArgs : EventArgs
    {
        public StringEventArgs(string str)
        {
            StringValue = str;
        }

        public string StringValue { get; internal set; }
    }
}
