using System;
using System.Runtime.InteropServices;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    [ComImport,
    Guid("79eac9e4-baf9-11ce-8c82-00aa004ba90b"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInternetProtocol
    {
        void Start([MarshalAs(UnmanagedType.LPWStr)] string szUrl, IInternetProtocolSink pOIProtSink,
    IInternetBindInfo pOIBindInfo, uint grfPI, IntPtr dwReserved);
        void Continue(ref _tagPROTOCOLDATA pProtocolData);
        void Abort(int hrReason, uint dwOptions);
        void Terminate(uint dwOptions);
        void Suspend();
        void Resume();
        void Read(IntPtr pv, uint cb, out uint pcbRead);
        void Seek(_LARGE_INTEGER dlibMove, uint dwOrigin, out _ULARGE_INTEGER
            plibNewPosition);
        void LockRequest(uint dwOptions);
        void UnlockRequest();
    }
}