using System;
using System.Windows.Forms;
using Questify.Builder.Logic.Service.Interfaces.UI;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Helpers
{
    class FocusedControlHelper
    {
        public static Control FindFocusedControl2(Control control, int maxDepth)
        {
            if (maxDepth <= 0 || control == null)
            {
                return null;
            }

            if (control is IViewCommands)
            {
                return control;
            }

            return control.Parent != null ? FindFocusedControl2(control.Parent, maxDepth - 1) : null;
        }

        public static Control GetFocusedControl()
        {
            Control focusedControl = null;
            var focusedHandle = Win.GetFocus();
            if (focusedHandle != IntPtr.Zero)
            {
                focusedControl = FindWinFormstControl(focusedHandle, 5);
            }
            return focusedControl;
        }

        private static Control FindWinFormstControl(IntPtr focusedHandle, int maxDepth)
        {
            if (maxDepth <= 0 || focusedHandle == IntPtr.Zero)
            {
                return null;
            }
            var result = Control.FromHandle(focusedHandle);

            return result ?? FindWinFormstControl(Win.GetParent(focusedHandle), maxDepth - 1);
        }
    }
}
