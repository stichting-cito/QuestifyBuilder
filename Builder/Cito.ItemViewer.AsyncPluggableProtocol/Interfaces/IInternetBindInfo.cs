using System.Runtime.InteropServices;
using System.Text;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    [
    ComImport,
    Guid("79EAC9E1-BAF9-11CE-8C82-00AA004BA90B"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
    ]
    public interface IInternetBindInfo
    {
        void GetBindInfo(out uint grfBINDF, ref _tagBINDINFO pbindinfo);
        void GetBindString(uint ulStringType, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] StringBuilder[] ppwzStr, uint cEl, ref uint pcElFetched);
    }
}