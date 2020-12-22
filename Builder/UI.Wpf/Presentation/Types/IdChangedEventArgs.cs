using System;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    public class IdChangedEventArgs : EventArgs
    {
        public Guid PreviousId { get; set; }

        public Guid NewId { get; set; }

        public IdChangedEventArgs(Guid previousId, Guid newId)
        {
            PreviousId = previousId;
            NewId = newId;
        }
    }
}
