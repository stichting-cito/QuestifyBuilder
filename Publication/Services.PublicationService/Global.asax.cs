using System;
using System.IO;
using System.Linq;
using NLog;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic;
using Questify.Builder.IoC;

namespace Questify.Builder.Services.PublicationService
{
    public class Global : System.Web.HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start(object sender, EventArgs e)
        {
            IoCHelper.Init(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\Plugins"));

            AssessmentTestv2Factory.Plugins = IoCHelper.GetInstances<ITestModelPlugin>();
            TestPackageFactory.Plugins = IoCHelper.GetInstances<ITestPackageModelPlugin>();
            Publication.PublicationService.PublicationHandlers = IoCHelper.GetInstances<IPublicationHandler>();
            Validation.ValidationService.Validators = IoCHelper.GetInstances<IValidateHandler>();
            PluginHelper.MathMlPlugin = IoCHelper.GetInstances<IMathMlEditorPlugin>().FirstOrDefault();
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

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}