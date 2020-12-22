using System.Collections.Generic;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IPublicationSelection
    {
        string Title { get; }

        IEnumerable<string> TestNames { get; }
        IEnumerable<string> TestPackageNames { get; }
        IEnumerable<string> TestNamesToBeValidated { get; }

        bool IsEmpty { get; }

        bool ContainsItems { get; }

        bool IsBulkExport { get; }

        string WizardDescription { get; }

        void Initialise();

        bool AtLeastOneHandlerAvailable();

        string DefaultPublicationName(string fileExtension);
    }
}
