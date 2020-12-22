using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Questify.Builder.Logic.Service.Logging
{
    public class CustomTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleInstance = $"{Environment.UserName} ({Environment.MachineName})";
        }
    }
}
