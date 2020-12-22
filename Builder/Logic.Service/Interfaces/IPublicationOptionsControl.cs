using System.Collections.Generic;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IPublicationOptionsControl
    {

        Dictionary<string, string> ExportedFiles { get; set; }
        Dictionary<string, string> ConfigurationOptions { get; }

        string ErrorMessage { get; set; }
    }
}

