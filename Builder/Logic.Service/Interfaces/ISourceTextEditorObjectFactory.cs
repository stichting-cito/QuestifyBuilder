using System;
using Cito.Tester.Common;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using GenericResourceDto = Questify.Builder.Logic.Service.Model.Entities.GenericResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface ISourceTextEditorObjectFactory
    {
        Tuple<GenericResourceEntity, int, ResourceManagerBase> GetRequiredObjectsForSourceTextWithId(Guid id);

        Tuple<GenericResourceEntity, int, ResourceManagerBase> GetRequiredObjectsForNewSourceText(int bankId, bool makeSourceTextTemplate);

        string UpdateSourceTextResource(GenericResourceEntity resource);

        EntityCollection GetCustomBankPropertiesForBranch(int bankId);

        EntityCollection GetAvailableStates();

        GenericResourceDto SelectStyleSheetToLink(int bankId, int? contextIdentifier, ResourceManagerBase resourceManager);
    }
}
