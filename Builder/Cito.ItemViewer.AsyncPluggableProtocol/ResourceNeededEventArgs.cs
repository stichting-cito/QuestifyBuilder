using System;
using System.IO;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    public class ResourceNeededEventArgs : EventArgs
    {

        public Stream ResourceStream { get; set; }

        public string Url { get; }


        public ResourceNeededEventArgs(string url, Stream inputStream)
        {
            ResourceStream = inputStream;
            Url = url;
        }
    }
}
