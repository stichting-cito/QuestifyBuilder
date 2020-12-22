using System;
using System.Runtime.InteropServices;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    internal abstract class ClassFactory : IClassFactory
    {
        private const int E_NOINTERFACE = unchecked((int)0x80004002);
        private readonly Guid IID_IInternetProtocolInfo = new Guid("{79eac9ec-baf9-11ce-8c82-00aa004ba90b}");
        private readonly Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");


        public void CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject)
        {
            ppvObject = IntPtr.Zero;

            if ((riid == IID_IInternetProtocolInfo) ||
                (riid == IID_IUnknown))
            {
                ppvObject = Marshal.GetComInterfaceForObject(GetInstance(), typeof(IInternetProtocolInfo));
            }
            else
            {
                Marshal.ThrowExceptionForHR(E_NOINTERFACE);
            }
        }

        public void LockServer(bool fLock)
        {
        }


        protected abstract IInternetProtocolInfo GetInstance();
    }
}