using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    [ComImport,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("79eac9e7-baf9-11ce-8c82-00aa004ba90b")]
    public interface IInternetSession
    {
        void RegisterNameSpace(
            IClassFactory pCF,
            ref Guid rclsid,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzProtocol,
            UInt32 cPatterns,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)]
            string[] ppwzPatterns,
            UInt32 dwReserved
            );

        void UnregisterNameSpace(
            IClassFactory pCF,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzProtocol
            );

        void RegisterMimeFilter(
            IClassFactory pCF,
            ref Guid rclsid,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzType
            );

        void UnregisterMimeFilter(
            IClassFactory pCF,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzType
            );

        void CreateBinding(
            IBindCtx pBC,
            [MarshalAs(UnmanagedType.LPWStr)] string szUrl,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnk,
            out IInternetProtocol ppOIOnetProt,
            UInt32 dwOption
            );

        void SetSessionOption(
            UInt32 dwOption,
            IntPtr pBuffer,
            UInt32 dwBufferLength,
            UInt32 dwReserved
            );

        void GetSessionOption(
            UInt32 dwOption,
            ref IntPtr pBuffer,
            ref UInt32 pdwBufferLength,
            UInt32 dwReserved
            );
    }
}