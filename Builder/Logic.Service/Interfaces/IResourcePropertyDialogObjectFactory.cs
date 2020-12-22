using System;
using Cito.Tester.Common;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IResourcePropertyDialogObjectFactory
    {
        IPropertyEntity GetRequiredObjectsForPropertyEntityWithId(Guid id, Type type);
        EntityCollection GetReferences(IPropertyEntity entity);
        string SaveResourcePropertyDialog(IPropertyEntity entity, string pathToNewResource = null, bool identifierAndCodeFieldDiffer = false);
        byte[] GetBinData(Guid id);
        string UpdateResourceHistory(ResourceHistoryEntity resourceHistoryEntity);
        ResourceHistoryEntity GetResourceHistory(ResourceHistoryEntity resourceHistory);
        EntityCollection GetResourceHistoryByResource(ResourceEntity resourceEntity);
        ResourceManagerBase GetResourceManager(int bankId);
    }
}
