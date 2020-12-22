using Enums;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IValidateHandler : IReportValidationBase
    {
        bool IsReportAvailable { get; }

        string ResultText { get; }

        ValidationResult Validate();

        string GenerateReport();
    }
}
