using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    public delegate void ProtocolDataHandler(string url, Stream stream);

    [DebuggerStepThrough]
    internal abstract class ProtocolHandler : IInternetProtocolRoot, IInternetProtocolInfo, IInternetProtocol, IDisposable
    {
        private const int S_OK = 0x00000000;
        private const int S_FALSE = 0x00000001;
        private const int E_PENDING = unchecked((int)0x8000000A);
        private const int E_INVALIDARG = unchecked((int)0x80070057);
        private const int INET_E_DEFAULT_ACTION = unchecked((int)0x800C0011);
        private const int INET_E_DATA_NOT_AVAILABLE = unchecked((int)0x800C0007);


        private IInternetProtocolSink _pProtSink;
        private MemoryStream _streamData;
        private bool _done;

        private void DoBind(string sUrl)
        {
            _streamData = new MemoryStream(); _done = false;

            ProtocolDataHandler data = ProtocolData;
            data?.BeginInvoke(sUrl, _streamData, Callback, data);
        }

        private void Callback(IAsyncResult ar)
        {
            try
            {
                if (ar != null)
                {
                    var data = (ProtocolDataHandler)ar.AsyncState;
                    data.EndInvoke(ar);

                    lock (_streamData)
                    {
                        _streamData.Flush();
                        _streamData.Seek(0, SeekOrigin.Begin);
                    }
                    _done = true;

                    const uint grfBscf = (uint)(BSCF.FirstDataNotification | BSCF.LastDataNotification | BSCF.DataFullyAvailable);
                    _pProtSink.ReportData(grfBscf, 0, 0);
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }



        protected abstract void ProtocolData(string url, Stream stream);

        protected virtual void ReportMimeType(string mimeType)
        {
            _pProtSink.ReportProgress((uint)BindStatus.VERIFIEDMIMETYPEAVAILABLE, mimeType);
        }



        public void ParseUrl(string pwzUrl, ParseAction parseAction, uint dwParseFlags, StringBuilder pwzResult, uint cchResult, out uint pcchResult, uint dwReserved)
        {
            string sResult = null;
            pcchResult = 0;

            switch (parseAction)
            {
                case ParseAction.PARSE_SECURITY_URL:
                    sResult = "http://localhost";
                    break;
                case ParseAction.PARSE_SECURITY_DOMAIN:
                    sResult = "localhost";
                    break;
                case ParseAction.PARSE_CANONICALIZE:
                case ParseAction.PARSE_LOCATION:
                    sResult = pwzUrl;
                    break;
                default:
                    Marshal.ThrowExceptionForHR(INET_E_DEFAULT_ACTION);
                    break;
            }

            if (sResult == null)
                return;

            pcchResult = (uint)sResult.Length;
            if (sResult.Length < cchResult)
            {
                pwzResult.Append(sResult);
            }
            else
            {
                Marshal.ThrowExceptionForHR(S_FALSE);
            }
        }

        public void CombineUrl(string pwzBaseUrl, string pwzRelativeUrl, uint dwCombineFlags, StringBuilder pwzResult, uint cchResult, out uint pcchResult, uint dwReserved)
        {
            pcchResult = 0;
            Marshal.ThrowExceptionForHR(INET_E_DEFAULT_ACTION);
        }

        public void CompareUrl(string pwzUrl1, string pwzUrl2, uint dwCompareFlags)
        {
            Marshal.ThrowExceptionForHR(INET_E_DEFAULT_ACTION);
        }

        public void QueryInfo(string pwzUrl, QueryOption queryOption, uint dwQueryFlags, IntPtr pBuffer, uint cbBuffer, ref uint cbBuf, uint dwReserved)
        {
            switch (queryOption)
            {
                case QueryOption.QUERY_USES_NETWORK:
                    Marshal.StructureToPtr(false, pBuffer, false);
                    break;
                case QueryOption.QUERY_IS_SECURE:
                    Marshal.StructureToPtr(true, pBuffer, false);
                    break;
                default:
                    Marshal.ThrowExceptionForHR(INET_E_DEFAULT_ACTION);
                    break;
            }
        }



        public void Start(string szUrl, IInternetProtocolSink pOIProtSink, IInternetBindInfo pOIBindInfo, uint grfPI, IntPtr dwReserved)
        {
            _pProtSink = pOIProtSink;

            if (Convert.ToBoolean(grfPI & (int)PI_Flags.PARSE_URL))
            {
                Marshal.ThrowExceptionForHR(0);
                return;
            }

            if (!Convert.ToBoolean(grfPI & (int)PI_Flags.FORCE_ASYNC))
            {
                DoBind(szUrl);
            }
            else
            {
                var protdata = new _tagPROTOCOLDATA
                {
                    GrfFlags = (uint)PI_Flags.FORCE_ASYNC,
                    DwState = 0,
                    PData = IntPtr.Zero,
                    CbData = 0
                };

                if (pOIProtSink != null)
                {
                    pOIProtSink.Switch(ref protdata);
                    Marshal.ThrowExceptionForHR(E_PENDING);
                }
                else
                {
                    Marshal.ThrowExceptionForHR(E_INVALIDARG);
                }
            }
        }

        public void Continue(ref _tagPROTOCOLDATA pProtocolData)
        {
        }

        public void Abort(int hrReason, uint dwOptions)
        {
        }

        public void Terminate(uint dwOptions)
        {
        }

        public void Suspend()
        {
        }

        public void Resume()
        {
        }



        public void Read(IntPtr pv, uint cb, out uint pcbRead)
        {
            pcbRead = 0;
            lock (_streamData)
            {
                if (_streamData.Position >= _streamData.Length && _done)
                {
                    Marshal.ThrowExceptionForHR(S_FALSE);
                    return;
                }

                if (_streamData.Length == 0)
                {
                    Marshal.ThrowExceptionForHR(INET_E_DATA_NOT_AVAILABLE);
                    return;
                }

                var data = new byte[cb];
                try
                {
                    pcbRead = (uint)_streamData.Read(data, 0, (int)cb);
                    if (pcbRead == 0)
                    {
                        return;
                    }

                    Marshal.Copy(data, 0, pv, (int)pcbRead);

                    if (_done)
                    {
                        _pProtSink.ReportResult(S_OK, 0, null);
                        Marshal.ThrowExceptionForHR(S_FALSE);
                    }
                    else
                    {
                        Marshal.ThrowExceptionForHR(S_OK);
                    }
                }
                catch (EndOfStreamException)
                {
                    if (_done)
                    {
                        _pProtSink.ReportResult(S_OK, 0, null);
                        Marshal.ThrowExceptionForHR(S_FALSE);
                    }
                    else
                    {
                        Marshal.ThrowExceptionForHR(E_PENDING);
                    }
                }
                finally
                {
                    Array.Resize(ref data, 0);
                }
            }
        }

        public void Seek(_LARGE_INTEGER dlibMove, uint dwOrigin, out _ULARGE_INTEGER plibNewPosition)
        {
            plibNewPosition = new _ULARGE_INTEGER();
        }

        public void LockRequest(uint dwOptions)
        {
        }

        public void UnlockRequest()
        {
        }

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue && disposing)
            {
                _streamData?.Close();
                _streamData?.Dispose();

                _pProtSink = null;
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }


    }
}