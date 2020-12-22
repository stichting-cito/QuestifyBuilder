using System;
using System.Runtime.InteropServices;

namespace Questify.Builder.UI.Wpf.Presentation.WinformsInterop
{
    internal static class Win
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetParent(IntPtr hwnd);

    }
}
