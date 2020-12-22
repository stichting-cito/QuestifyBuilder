namespace Questify.Builder.Logic.Service.Domain.TransManager
{
    public interface ITransFactory
    {
        ITransManager CreateManager();
    }
}
