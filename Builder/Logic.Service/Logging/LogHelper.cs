using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.ApplicationInsights;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Logging
{
    public class LogHelper
    {
        private static TelemetryClient _client;
        public static TelemetryClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new TelemetryClient();
                    _client.Context.User.Id = string.Format("{0} ({1})", Environment.UserName, Environment.MachineName);
                    _client.Context.Device.Id = Environment.MachineName;
                }

                return _client;
            }
        }

        public static string GetUser()
        {
            var identity = GetUserIdentity();
            if (identity != null)
                return identity.UserName;
            return string.Empty;
        }

        private static TestBuilderIdentity GetUserIdentity()
        {
            var principal = Thread.CurrentPrincipal as TestBuilderPrincipal;
            return principal?.Identity as TestBuilderIdentity;
        }

        public static string GetErrorMessage(Exception ex)
        {
            StringBuilder errorStringBuilder = new StringBuilder(ex.Message);
            var exCopy = ex;
            while (exCopy.InnerException != null && !string.IsNullOrEmpty(exCopy.InnerException.Message))
            {
                errorStringBuilder.AppendLine(exCopy.InnerException.Message);
                exCopy = exCopy.InnerException;
            }
            return errorStringBuilder.ToString();
        }

        public static void TrackEvent(EventsToTrack @event, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            var eventProperties = ConstructProperties(properties);
            Client.TrackEvent(@event.ToString(), eventProperties, metrics);
            Client.Flush();
        }

        public static void TrackException(Exception exception, bool isTerminating, IDictionary<string, string> properties = null)
        {
            var eventProperties = ConstructProperties(properties);
            if (isTerminating)
            {
                eventProperties[nameof(isTerminating)] = "true";
            }

            Client.TrackException(exception, properties);
        }

        private static IDictionary<string, string> ConstructProperties(IDictionary<string, string> properties)
        {
            var result = properties ?? new Dictionary<string, string>();
            result["UserId"] = GetUserIdentity()?.UserId.ToString();
            result["UserName"] = GetUserIdentity()?.UserName;

            return result;
        }
    }
}


