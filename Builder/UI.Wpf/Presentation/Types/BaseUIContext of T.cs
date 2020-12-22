namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    class BaseUIContext<T> : IUIContext<T>
    where T : class
    {
        void MEFedMVVM.Services.Contracts.IContextAware.InjectContext(object view)
        {
            T tmp;
            tmp = view as T;
            if (tmp != null)
            {
                Instance = tmp;
            }
        }

        public T Instance { get; private set; }

    }
}
