using MEFedMVVM.Services.Contracts;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    public interface IUIContext<T> : IContextAware
        where T : class
    {
        T Instance { get; }
    }
}
