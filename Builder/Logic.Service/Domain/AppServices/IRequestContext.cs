namespace Questify.Builder.Logic.Service.Domain.AppServices
{
    public interface IRequestContext
    {
        IBusinessNotifier Notifier { get; }
    }
}
