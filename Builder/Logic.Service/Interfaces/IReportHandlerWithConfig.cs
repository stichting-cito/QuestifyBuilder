using Questify.Builder.Configuration;

namespace Questify.Builder.Logic.Service.Interfaces
{

    public interface IReportHandlerWithConfig : IReportHandler
    {

        PluginHandlerConfigCollection HandlerConfig { get; set; }
    }
}
