using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    internal class ResourceProtocolHandler : ProtocolHandler
    {

        public event EventHandler<ResourceNeededEventArgs> ResourceNeeded;

        protected void OnResourceNeeded(ResourceNeededEventArgs e)
        {
            ResourceNeeded?.Invoke(this, e);
        }


        protected override void ProtocolData(string url, Stream stream)
        {
            var args = new ResourceNeededEventArgs(url, stream);
            OnResourceNeeded(args);
            if (args?.ResourceStream != null)
            {
                var mimetype = GetMimeTypeFromStream(args.ResourceStream, null);
                ReportMimeType(mimetype);
            }
        }

        private static string GetMimeTypeFromStream(Stream stream, string mimeProposed)
        {
            var mimeRet = string.Empty;

            try
            {
                const int maxContent = 256;
                var dataBytes = new byte[maxContent];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(dataBytes, 0, maxContent);

                if (!string.IsNullOrEmpty(mimeProposed))
                {
                    mimeRet = mimeProposed;
                }

                IntPtr outPtr;
                var ret = UrlMon.FindMimeFromData(IntPtr.Zero, null, dataBytes, dataBytes.Length, mimeProposed, 0,
                    out outPtr, 0);
                if (ret == 0 && outPtr != IntPtr.Zero)
                {
                    mimeRet = Marshal.PtrToStringUni(outPtr);
                    Marshal.FreeCoTaskMem(outPtr);
                }

                stream.Seek(0, SeekOrigin.Begin);
            }
            catch (ObjectDisposedException ex)
            {
            }

            return mimeRet;
        }
    }
}