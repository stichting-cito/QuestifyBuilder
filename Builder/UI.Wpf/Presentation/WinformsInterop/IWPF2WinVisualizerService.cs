using System;
using System.Collections.Generic;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.WinformsInterop
{
    public interface IWPF2WinVisualizerService
    {
        void Register(string key, Type winType);

        bool Unregister(string key);

        bool Show(string key, object state, bool setOwner,
    EventHandler<UICompletedEventArgs> completedProc);

        bool? ShowDialog(string key, object state, bool setQBMainWindowAsOwner);

        List<object> GetOpenWindows();

    }
}
