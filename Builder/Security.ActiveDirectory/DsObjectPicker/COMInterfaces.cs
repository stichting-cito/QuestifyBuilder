using System;
using System.Runtime.InteropServices;

namespace Questify.Builder.Security.ActiveDirectory.DsObjectPicker
{

    [ComImport, Guid("17D6CCD8-3B7B-11D2-B9E0-00C04FD8DBF7")]
    internal class DsObjectPicker
    {
    }

    [ComImport,
Guid("0C87E64E-3B7A-11D2-B9E0-00C04FD8DBF7"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDsObjectPicker
    {
        void Initialize(ref DSOP_INIT_INFO pInitInfo);
        void InvokeDialog(IntPtr HWND, out IDataObject lpDataObject);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("0000010e-0000-0000-C000-000000000046")]
    internal interface IDataObject
    {
        [PreserveSig()]
        int GetData(ref FORMATETC pFormatEtc, out STGMEDIUM b);
        void GetDataHere(ref FORMATETC pFormatEtc, ref STGMEDIUM b);
        [PreserveSig()]
        int QueryGetData(IntPtr a);
        [PreserveSig()]
        int GetCanonicalFormatEtc(IntPtr a, IntPtr b);
        [PreserveSig()]
        int SetData(IntPtr a, IntPtr b, int c);
        [PreserveSig()]
        int EnumFormatEtc(uint a, IntPtr b);
        [PreserveSig()]
        int DAdvise(IntPtr a, uint b, IntPtr c, ref uint d);
        [PreserveSig()]
        int DUnadvise(uint a);
        [PreserveSig()]
        int EnumDAdvise(IntPtr a);
    }
}