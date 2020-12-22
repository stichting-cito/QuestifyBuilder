using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Model.LlblGen.Proxy.Interface
{
    interface IConvertBank
    {
        BankDto Convert(BankEntity bankEntity);
    }
}
