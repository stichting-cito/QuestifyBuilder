using System;
using System.Drawing;
using System.Windows.Forms;
using Questify.Builder.Logic.Service.HelperFunctions.Symbols;

namespace Questify.Builder.Logic.Service.Interfaces.UI
{
    public interface IAddSymbolDialog
    {
        bool IsDisposed();
        event EventHandler<SpecialSymbolEventArgs> SpecialSymbolPicked;
        event EventHandler<EventArgs> DialogClosed;
        void Show(IWin32Window owner, Point windowLocation);
        void Dispose();
    }
}
