namespace Questify.Builder.Logic.Service.Messages
{
    public interface IBusinessExceptionManager
    {
        void HandleBusinessException(BusinessExceptionDto exceptionDto);
    }
}
