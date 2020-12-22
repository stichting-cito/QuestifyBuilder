using System;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    public class ASyncResourceProtocolManager : IDisposable
    {
        public event EventHandler<ResourceNeededEventArgs> ResourceNeeded;

        protected void OnResourceNeeded(ResourceNeededEventArgs e)
        {
            ResourceNeeded?.Invoke(this, e);
        }

        private readonly string _protocol;
        private IInternetSession _session;
        private ResourceClassFactory _classFactory;

        private bool _disposed;


        public ASyncResourceProtocolManager(string protocol)
        {
            _protocol = protocol;

            UrlMon.CoInternetGetSession(0, out _session, 0);

            if (_session != null)
            {
                _classFactory = new ResourceClassFactory();
                _classFactory.ResourceNeeded += ClassFactoryResourceNeeded;
            }
            else
            {
                throw new ProtocolException("Failed to get IInternetSession.");
            }
        }

        ~ASyncResourceProtocolManager()
        {
            Dispose(false);
        }

        public void Register()
        {
            if (_classFactory != null)
            {
                var protocolGuid = Guid.NewGuid();
                _session.RegisterNameSpace(_classFactory, ref protocolGuid, _protocol, 0, null, 0);
            }
            else
            {
                throw new ProtocolException("Failed to get ClassFactory set.");
            }
        }

        public void Unregister()
        {
            if (_classFactory != null)
            {
                try
                {
                    _session.UnregisterNameSpace(_classFactory, _protocol);
                }
                catch (Exception)
                {
                }
                _classFactory.ResourceNeeded -= ClassFactoryResourceNeeded;
                _classFactory.CleanUp();
                _classFactory = null;
            }
            else
            {
                throw new ProtocolException("Failed to get ClassFactory not set.");
            }
        }

        public void CleanUp()
        {
            _classFactory?.CleanUp();
        }

        private void ClassFactoryResourceNeeded(object sender, ResourceNeededEventArgs e)
        {
            OnResourceNeeded(e);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

                if (_classFactory != null)
                {
                    Unregister();
                }
                _session = null;
            }

            _disposed = true;
        }

    }

}
