using System.Runtime.InteropServices;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    [ComImport,
    Guid("79EAC9E5-BAF9-11CE-8C82-00AA004BA90B"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInternetProtocolSink
    {
        void Switch(ref _tagPROTOCOLDATA pProtocolData);
        void ReportProgress(uint ulStatusCode, [MarshalAs(UnmanagedType.LPWStr)] string szStatusText);
        void ReportData(uint grfBSCF, uint ulProgress, uint ulProgressMax);
        void ReportResult(int hrResult, uint dwError, string szResult);
    }
}