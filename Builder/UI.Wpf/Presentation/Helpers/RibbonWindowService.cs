using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Questify.Builder.UI.Wpf.Presentation.Helpers
{
    public static class RibbonWindowService
    {
        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        public static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam, int minWidth, int minHeight)
        {
            var mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            const int MONITOR_DEFAULTTONEAREST = 0x00000002;
            var monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                var monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = new RECT(
                    monitorInfo.rcMonitor.left + 1,
                    monitorInfo.rcMonitor.top + 1,
                    monitorInfo.rcMonitor.right,
                    monitorInfo.rcMonitor.bottom);
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
                mmi.ptMinTrackSize.x = minWidth;
                mmi.ptMinTrackSize.y = minHeight;
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            internal POINT PtReserved;
            internal POINT ptMaxSize;
            internal POINT ptMaxPosition;
            internal POINT ptMinTrackSize;
            internal POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            internal int x;

            internal int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            internal readonly int left;

            internal readonly int top;

            internal readonly int right;

            internal readonly int bottom;

            public static readonly RECT Empty = new RECT();

            public int Width
            {
                get { return Math.Abs(right - left); }
            }

            public int Height
            {
                get { return bottom - top; }
            }

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }

            public bool IsEmpty
            {
                get
                {
                    return left >= right || top >= bottom;
                }
            }

            public override string ToString()
            {
                if (this == Empty)
                {
                    return "RECT {Empty}";
                }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Rect))
                {
                    return false;
                }
                return this == (RECT)obj;
            }

            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }

            public static bool operator ==(RECT rect1, RECT rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            public static bool operator !=(RECT rect1, RECT rect2)
            {
                return !(rect1 == rect2);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            internal int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            internal RECT rcMonitor = new RECT();

            internal RECT rcWork = new RECT();

            internal int dwFlags = 0;
        }
    }
}
