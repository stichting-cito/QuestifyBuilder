using System;
using System.Runtime.InteropServices;

namespace Questify.Builder.Security.ActiveDirectory
{
    internal static class NativeMethods
    {
        [DllImport("Kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("Kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);
    }
}

