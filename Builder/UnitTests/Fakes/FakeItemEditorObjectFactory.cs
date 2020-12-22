using System;
using System.ComponentModel.Composition;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Questify.Builder.Logic.Service.DTO;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.UnitTests.Fakes
{

    [PartCreationPolicy(CreationPolicy.Shared), Export(typeof(IItemEditorObjectFactory))]
    public class FakeItemEditorObjectFactory : IItemEditorObjectFactory
    {
        static IItemEditorObjectFactory _fake;

        public static IItemEditorObjectFactory MakeNewFake()
        {
            _fake = A.Fake<IItemEditorObjectFactory>();
            return _fake;
        }



        public FakeItemEditorObjectFactory()
        {
        }



        public ItemEditorObjectFactoryResult GetRequiredObjectsForItemWithId(Guid id)
        {
            return _fake.GetRequiredObjectsForItemWithId(id);
        }

        public ItemEditorObjectFactoryResult GetObjectsForNewItem(Guid layoutId, int bankId)
        {
            return _fake.GetObjectsForNewItem(layoutId, bankId);
        }

        public string UpdateItemResource(ItemResourceEntity resource)
        {
            return _fake.UpdateItemResource(resource);
        }

        public bool RenameItem(ItemResourceEntity itemResourceEntity,
            AssessmentItem assessmentItem,
            out EntityCollection referencedResources)
        {
            return _fake.RenameItem(itemResourceEntity, assessmentItem, out referencedResources);
        }

        public EntityCollection GetCustomBankPropertiesForBranch(int bankId)
        {
            return _fake.GetCustomBankPropertiesForBranch(bankId);
        }

        public GenericResourceEntity GetGenericResource(int bankId, string resourceName)
        {
            return _fake.GetGenericResource(bankId, resourceName);
        }

        public ConceptStructurePartCustomBankPropertyEntity PopulateConceptCustomBankPropertyHierarchy(Guid id)
        {
            return _fake.PopulateConceptCustomBankPropertyHierarchy(id);
        }

        public void PopulateTreeCustomBankPropertyHierarchy(ref TreeStructurePartCustomBankPropertyEntity rootPartEntity)
        {
        }


    }


}
