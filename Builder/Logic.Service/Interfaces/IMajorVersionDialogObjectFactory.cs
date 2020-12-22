using System;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IMajorVersionDialogObjectFactory
    {
        string UpdateMajorVersion(IPropertyEntity propertyEntity);
        IPropertyEntity GetRequiredObjectForPropertyEntity(Guid id, Type type);
        ResourceHistoryEntity GetLastResourceHistoryEntityForResource(IPropertyEntity propertyEntity);
    }
}
