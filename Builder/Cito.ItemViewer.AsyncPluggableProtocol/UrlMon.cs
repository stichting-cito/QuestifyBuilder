using System;
using System.Runtime.InteropServices;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    public class UrlMon
    {
        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        public static extern int FindMimeFromData(
            IntPtr pBC,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer,
            int cbSize,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
            int dwMimeFlags,
            out IntPtr ppwzMimeOut,
            int dwReserved
            );

        [DllImport("UrlMon.dll")]
        public static extern UInt32 CoInternetGetSession(
            uint dwSessionMode,
            out IInternetSession ppIInternetSession,
            uint dwReserved
            );
    }

    public struct _tagPROTOCOLDATA
    {
        public uint GrfFlags { get; set; }
        public uint DwState { get; set; }
        public IntPtr PData { get; set; }
        public uint CbData { get; set; }
    }

    public struct _tagBINDINFO
    {
        public uint CbSize { get; }

        [MarshalAs(UnmanagedType.LPWStr)] internal string szExtraInfo;

        public STGMEDIUM StgmedData { get; }
        public uint GrfBindInfoF { get; }
        public uint DwBindVerb { get; }

        [MarshalAs(UnmanagedType.LPWStr)] internal string szCustomVerb;

        public uint CbstgmedData { get; }
        public uint DwOptions { get; }
        public uint DwOptionsFlags { get; }
        public uint DwCodePage { get; }
        public _SECURITY_ATTRIBUTES SecurityAttributes { get; }
        public Guid Iid { get; }
        public object Punk { get; }
        public uint DwReserved { get; }
    }
    public struct _LARGE_INTEGER
    {
        public long QuadPart { get; }
    }

    public struct _ULARGE_INTEGER
    {
        public ulong QuadPart { get; }
    }

    public struct _SECURITY_ATTRIBUTES
    {
        public uint NLength { get; }
        public IntPtr LpSecurityDescriptor { get; }
        public int BInheritHandle { get; }
    }

    public enum BSCF
    {
        FirstDataNotification = 0x1,
        IntermediateDataNotification = 0x2,
        LastDataNotification = 0x4,
        DataFullyAvailable = 0x8,
        AvailableDataSizeUnknown = 0x10
    }

    public enum ParseAction
    {
        PARSE_CANONICALIZE = 0x1,
        PARSE_FRIENDLY = 0x2,
        PARSE_SECURITY_URL = 0x3,
        PARSE_ROOTDOCUMENT = 0x4,
        PARSE_DOCUMENT = 0x5,
        PARSE_ANCHOR = 0x6,
        PARSE_ENCODE = 0x7,
        PARSE_DECODE = 0x8,
        PARSE_PATH_FROM_URL = 0x9,
        PARSE_URL_FROM_PATH = 0xA,
        PARSE_MIME = 0xB,
        PARSE_SERVER = 0xC,
        PARSE_SCHEMA = 0xD,
        PARSE_SITE = 0xE,
        PARSE_DOMAI = 0xF,
        PARSE_LOCATION = 0x10,
        PARSE_SECURITY_DOMAIN = 0x11,
        PARSE_ESCAPE = 0x12,
        PARSE_UNESCAPE = 0x13
    }

    public enum QueryOption
    {
        QUERY_EXPIRATION_DATE = 0x01,
        QUERY_TIME_OF_LAST_CHANGE = 0x02,
        QUERY_CONTENT_ENCODING = 0x03,
        QUERY_CONTENT_TYPE = 0x04,
        QUERY_REFRESH = 0x05,
        QUERY_RECOMBINE = 0x06,
        QUERY_CAN_NAVIGATE = 0x07,
        QUERY_USES_NETWORK = 0x08,
        QUERY_IS_CACHED = 0x09,
        QUERY_IS_INSTALLEDENTRY = 0x0A,
        QUERY_IS_CACHED_OR_MAPPED = 0x0B,
        QUERY_USES_CACHE = 0x0C,
        QUERY_IS_SECURE = 0x0D,
        QUERY_IS_SAFE = 0x0E
    }

    [Flags()]
    public enum PI_Flags
    {
        PARSE_URL = 0x00000001,
        FILTER_MODE = 0x00000002,
        FORCE_ASYNC = 0x00000004,
        USE_WORKERTHREAD = 0x00000008,
        MIMEVERIFICATION = 0x00000010,
        CLSIDLOOKUP = 0x00000020,
        DATAPROGRESS = 0x00000040,
        SYNCHRONOUS = 0x00000080,
        APARTMENTTHREADED = 0x00000100,
        CLASSINSTALL = 0x00000200,
        PASSONBINDCTX = 0x00002000,
        NOMIMEHANDLER = 0x00008000,
        LOADAPPDIRECT = 0x00004000,
        FORCE_SWITCH = 0x00010000,
        PREFERDEFAULTHANDLER = 0x00020000
    }

    public enum BindStatus
    {
        FINDINGRESOURCE = 1,
        CONNECTING,
        REDIRECTING,
        BEGINDOWNLOADDATA,
        DOWNLOADINGDATA,
        ENDDOWNLOADDATA,
        BEGINDOWNLOADCOMPONENTS,
        INSTALLINGCOMPONENTS,
        ENDDOWNLOADCOMPONENTS,
        USINGCACHEDCOPY,
        SENDINGREQUEST,
        CLASSIDAVAILABLE,
        MIMETYPEAVAILABLE,
        CACHEFILENAMEAVAILABLE,
        BEGINSYNCOPERATION,
        ENDSYNCOPERATION,
        BEGINUPLOADDATA,
        UPLOADINGDATA,
        ENDUPLOADDATA,
        PROTOCOLCLASSID,
        ENCODING,
        VERIFIEDMIMETYPEAVAILABLE,
        CLASSINSTALLLOCATION,
        DECODING,
        LOADINGMIMEHANDLER,
        CONTENTDISPOSITIONATTACH,
        FILTERREPORTMIMETYPE,
        CLSIDCANINSTANTIATE,
        IUNKNOWNAVAILABLE,
        DIRECTBIND,
        RAWMIMETYPE,
        PROXYDETECTING,
        ACCEPTRANGES,
        COOKIE_SENT,
        COMPACT_POLICY_RECEIVED,
        COOKIE_SUPPRESSED,
        COOKIE_STATE_UNKNOWN,
        COOKIE_STATE_ACCEPT,
        COOKIE_STATE_REJECT,
        COOKIE_STATE_PROMPT,
        COOKIE_STATE_LEASH,
        COOKIE_STATE_DOWNGRADE,
        POLICY_HREF,
        P3P_HEADER,
        SESSION_COOKIE_RECEIVED,
        PERSISTENT_COOKIE_RECEIVED,
        SESSION_COOKIES_ALLOWED
    }

    [Serializable()]
    public struct STGMEDIUM
    {
        public uint Tymed { get; }
        public IntPtr Data { get; }

        [MarshalAs(UnmanagedType.IUnknown)] internal object pUnkForRelease;
    }
}