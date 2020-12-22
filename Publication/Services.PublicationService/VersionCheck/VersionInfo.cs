using System;
using System.Reflection;

namespace Questify.Builder.Services.PublicationService.VersionCheck
{
    public class VersionInfo
    {
        public Version GetCurrentVersion()
        {
            return typeof(VersionInfo).Assembly.GetName().Version;
        }
    }
}
