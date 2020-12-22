using System;
using System.Web;
using NLog;

namespace Questify.Builder.Services.TasksService
{
    public class Global : HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exc = Server.GetLastError();
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(exc);
            }

            Server.ClearError();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Logger.Info("Session is terminating");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.Warn("Application is terminating!");
        }
    }
}