using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interop;
using Cinch;
using Questify.Builder.UI.Wpf.Presentation.Helpers;
using Questify.Builder.UI.Wpf.Presentation.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views
{
    [PopupNameToViewLookupKeyMetadata("SourceTextEditor", typeof(SourceTextEditorWindow))]
    internal partial class SourceTextEditorWindow : RibbonWindow, IRibbonFocus
    {
        public SourceTextEditorWindow()
        {
            InitializeComponent();
        }

        public bool RibbonSelected
        {
            get { return Ribbon.IsKeyboardFocusWithin; }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WindowProc);
        }

        [DebuggerStepThrough]
        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    RibbonWindowService.WmGetMinMaxInfo(hwnd, lParam, (int)MinWidth, (int)MinHeight);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

    }
}
