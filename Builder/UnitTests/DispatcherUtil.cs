using System;
using System.Security.Permissions;
using System.Windows.Threading;

namespace Questify.Builder.UnitTests
{
    public static class DispatcherUtil
    {

        [SecurityPermission(SecurityAction.Demand,
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            var frame = new DispatcherFrame();
            var waitfor = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                                                     new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
            waitfor.Wait(new TimeSpan(100));
        }

        private static object ExitFrame(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}
