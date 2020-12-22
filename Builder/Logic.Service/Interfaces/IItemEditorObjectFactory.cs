using System;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.DTO;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IItemEditorObjectFactory
    {
        ItemEditorObjectFactoryResult GetRequiredObjectsForItemWithId(Guid id);

        ItemEditorObjectFactoryResult GetObjectsForNewItem(Guid layoutId, int bankId);

        string UpdateItemResource(ItemResourceEntity resource);

        bool RenameItem(ItemResourceEntity itemResourceEntity, AssessmentItem assessmentItem, out EntityCollection referencedResources);

        EntityCollection GetCustomBankPropertiesForBranch(int bankId);

        GenericResourceEntity GetGenericResource(int bankId, string resourceName);

        ConceptStructurePartCustomBankPropertyEntity PopulateConceptCustomBankPropertyHierarchy(Guid id);
    }
}
