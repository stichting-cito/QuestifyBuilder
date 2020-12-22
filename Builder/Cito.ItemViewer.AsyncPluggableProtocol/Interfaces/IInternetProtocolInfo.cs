using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    [ComImport,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("79eac9ec-baf9-11ce-8c82-00aa004ba90b")]
    public interface IInternetProtocolInfo
    {
        void ParseUrl([MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.U4)] ParseAction parseAction, UInt32 dwParseFlags,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwzResult, UInt32 cchResult, out UInt32 pcchResult, UInt32 dwReserved);
        void CombineUrl([MarshalAs(UnmanagedType.LPWStr)] string pwzBaseUrl, [MarshalAs(UnmanagedType.LPWStr)] string pwzRelativeUrl, UInt32 dwCombineFlags,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwzResult, UInt32 cchResult, out UInt32 pcchResult, UInt32 dwReserved);
        void CompareUrl([MarshalAs(UnmanagedType.LPWStr)] string pwzUrl1, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl2, UInt32 dwCompareFlags);
        void QueryInfo([MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.U4)] QueryOption queryOption, UInt32 dwQueryFlags,
            IntPtr pBuffer, UInt32 cbBuffer, ref UInt32 cbBuf, UInt32 dwReserved);
    }
}