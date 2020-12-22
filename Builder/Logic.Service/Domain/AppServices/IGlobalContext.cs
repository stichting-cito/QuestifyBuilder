using Questify.Builder.Logic.Service.Domain.TransManager;

namespace Questify.Builder.Logic.Service.Domain.AppServices
{
    public interface IGlobalContext
    {
        ITransFactory TransFactory { get; }
    }
}
