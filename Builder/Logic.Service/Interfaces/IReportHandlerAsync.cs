
using System.Threading.Tasks;

namespace Questify.Builder.Logic.Service.Interfaces
{

    public interface IReportHandlerAsync : IReportHandler
    {

        Task<bool> GenerateDataAsync();

        void CancelReportGeneration();
    }
}
