using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using Questify.Builder.Logic;
using Questify.Builder.Logic.Service.Interfaces;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace Questify.Builder.IoC
{
    public class IoCHelper
    {
        public static void Init(string pluginPath, bool checkForXhtmlEditorPlugins = false)
        {
            Container = new Container();

            var pluginAssemblies = (from file in new DirectoryInfo(pluginPath).GetFiles("*.dll")
                                    where file.Name.Contains("Plugins")
                                    select Assembly.Load(AssemblyName.GetAssemblyName(file.FullName))).ToList();

            List<KeyValuePair<Type, Type>> htmlEditorPlugins = new List<KeyValuePair<Type, Type>>();
            pluginAssemblies.ForEach(p => p.GetExportedTypes().ToList().ForEach(t =>
            {
                if (t.GetInterfaces().Any(i => i.Name.Equals("IXHtmlEditor", StringComparison.InvariantCultureIgnoreCase)))
                    htmlEditorPlugins.Add(new KeyValuePair<Type, Type>(typeof(IXHtmlEditor), t));
            }));

            pluginAssemblies.Add(typeof(GenericTestModelPlugin).Assembly);

            Container.Collection.Register<ITestEditorPlugin>(pluginAssemblies);
            Container.Collection.Register<ITestModelPlugin>(pluginAssemblies);
            Container.Collection.Register<ITestPackageEditorPlugin>(pluginAssemblies);
            Container.Collection.Register<ITestPackageModelPlugin>(pluginAssemblies);
            Container.Collection.Register<IReportHandler>(pluginAssemblies);
            Container.Collection.Register<IPublicationHandler>(pluginAssemblies);
            Container.Collection.Register<IValidateHandler>(pluginAssemblies);
            Container.Collection.Register<IMathMlEditorPlugin>(pluginAssemblies);

            if (checkForXhtmlEditorPlugins)
            {
                if (!htmlEditorPlugins.Any())
                {
                    throw new ArgumentException(Properties.Resources.NoHtmlEditorPlugin);
                }
                else if (htmlEditorPlugins.Count > 1)
                {
                    throw new ArgumentException(Properties.Resources.MultipleHtmlEditorPlugins);
                }

                htmlEditorPlugins.ForEach(ep =>
                {
                    Container.Register(ep.Key, ep.Value, Lifestyle.Transient);
                });
                var producer = Container.GetRegistration(typeof(IXHtmlEditor));
                producer?.Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Disposable component");
            }


            Container.Verify();
        }

        public static void Init<T>(IEnumerable<T> typesToRegister) where T : class
        {
            Container = new Container();
            Container.Collection.Register(typesToRegister);
            Container.Verify();
        }

        private static Container Container { get; set; }

        public static IEnumerable<T> GetInstances<T>() where T : class
        {
            return Container?.GetAllInstances<T>();
        }

        public static T GetInstance<T>() where T : class
        {
            return Container?.GetInstance<T>();
        }
    }
}