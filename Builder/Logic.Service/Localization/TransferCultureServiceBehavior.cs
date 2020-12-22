using System;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace Questify.Builder.Logic.Service.Localization
{
    public class TransferCultureServiceBehavior : BehaviorExtensionElement, IDispatchMessageInspector, IServiceBehavior
    {
        [DebuggerStepThrough]
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            var index = OperationContext.Current.IncomingMessageHeaders.FindHeader("QbCurrentCulture", "http://www.Questify.eu");
            if (index >= 0)
            {
                var currentCulture = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("QbCurrentCulture", "http://www.Questify.eu");
                Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);
            }
            index = OperationContext.Current.IncomingMessageHeaders.FindHeader("QbCurrentUICulture", "http://www.Questify.eu");
            if (index >= 0)
            {
                var currentUICulture = new CultureInfo(OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("QbCurrentUICulture", "http://www.Questify.eu"));
                Thread.CurrentThread.CurrentUICulture = currentUICulture;
            }
            CultureInfo.DefaultThreadCurrentCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            return null;
        }

        [DebuggerStepThrough]
        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            return;
        }

        public override Type BehaviorType
        {
            get { return typeof(TransferCultureServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return this;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher chDisp in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher epDisp in chDisp.Endpoints)
                {
                    epDisp.DispatchRuntime.MessageInspectors.Add(new TransferCultureServiceBehavior());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }
    }
}
