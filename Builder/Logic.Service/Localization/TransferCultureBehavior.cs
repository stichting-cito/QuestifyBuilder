using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace Questify.Builder.Logic.Service.Localization
{
    public class TransferCultureBehavior : BehaviorExtensionElement, IClientMessageInspector, IEndpointBehavior
    {
        [DebuggerStepThrough]
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }

        [DebuggerStepThrough]
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            var cch = MessageHeader.CreateHeader("QbCurrentCulture", "http://www.Questify.eu", Thread.CurrentThread.CurrentCulture.Name);
            var cuch = MessageHeader.CreateHeader("QbCurrentUICulture", "http://www.Questify.eu", Thread.CurrentThread.CurrentUICulture.Name);
            request.Headers.Add(cch);
            request.Headers.Add(cuch);
            return null;
        }

        public override Type BehaviorType
        {
            get { return typeof(TransferCultureBehavior); }
        }

        protected override object CreateBehavior()
        {
            return this;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
