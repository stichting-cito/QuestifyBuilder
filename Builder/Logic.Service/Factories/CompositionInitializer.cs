using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Questify.Builder.Logic.Service.Factories
{
    public class CompositionInitializer : IDisposable
    {

        private readonly System.ComponentModel.Composition.Hosting.AggregateCatalog _catalog;
        private readonly CompositionContainer _container;
        private bool _disposed = false;


        public CompositionInitializer()
        {
            _catalog = new AggregateCatalog();
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                _catalog.Catalogs.Add(new AssemblyCatalog(a));
            }
            _container = new CompositionContainer(_catalog);
        }

        public T Satisfy<T>(T part)
        {
            _container.ComposeParts(part);
            return part;
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
                _catalog?.Dispose();
                _container?.Dispose();
            }

            _disposed = true;
        }
    }




}
