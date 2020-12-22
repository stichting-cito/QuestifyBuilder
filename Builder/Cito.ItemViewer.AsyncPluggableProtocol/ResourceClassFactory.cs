using System;
using System.Collections.Generic;

namespace Cito.ItemViewer.AsyncPluggableProtocol
{
    internal class ResourceClassFactory : ClassFactory
    {

        public event EventHandler<ResourceNeededEventArgs> ResourceNeeded;

        protected void OnResourceNeeded(ResourceNeededEventArgs e)
        {
            if (ResourceNeeded != null)
                ResourceNeeded(this, e);
        }



        private readonly List<ResourceProtocolHandler> _resourceProtocolHandlerPool;


        public ResourceClassFactory()
        {
            _resourceProtocolHandlerPool = new List<ResourceProtocolHandler>();
        }

        public void CleanUp()
        {
            foreach (var resourceProtocolHandler in _resourceProtocolHandlerPool)
            {
                resourceProtocolHandler.ResourceNeeded -= Handler_ResourceNeeded;
                resourceProtocolHandler.Dispose();
            }
            _resourceProtocolHandlerPool.Clear();
        }

        protected override IInternetProtocolInfo GetInstance()
        {
            var handler = new ResourceProtocolHandler();
            handler.ResourceNeeded += Handler_ResourceNeeded;

            _resourceProtocolHandlerPool.Add(handler);
            return handler;
        }

        private void Handler_ResourceNeeded(object sender, ResourceNeededEventArgs e)
        {
            OnResourceNeeded(e);
        }
    }
}