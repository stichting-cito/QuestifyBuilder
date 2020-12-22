using System;
using System.ComponentModel.Composition;
using Cito.Tester.Common;
using FakeItEasy;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog
{
    [PartCreationPolicy(CreationPolicy.Shared), Export(typeof(IResourcePropertyDialogObjectFactory))]
    public class FakeResourcePropertyDialogObjectFactory : IResourcePropertyDialogObjectFactory
    {
        static IResourcePropertyDialogObjectFactory _fake;

        public static IResourcePropertyDialogObjectFactory MakeNewFake()
        {
            _fake = A.Fake<IResourcePropertyDialogObjectFactory>();
            return _fake;
        }



        public FakeResourcePropertyDialogObjectFactory()
        {
        }



        public IPropertyEntity GetRequiredObjectsForPropertyEntityWithId(Guid id, Type type)
        {
            return _fake.GetRequiredObjectsForPropertyEntityWithId(id, type);
        }

        public EntityCollection GetReferences(IPropertyEntity entity)
        {
            return _fake.GetReferences(entity);
        }

        public string SaveResourcePropertyDialog(IPropertyEntity entity, string pathToNewResource = null, bool identifierAndCodeFieldDiffer = false)
        {
            return _fake.SaveResourcePropertyDialog(entity, pathToNewResource, identifierAndCodeFieldDiffer);
        }

        public byte[] GetBinData(Guid id)
        {
            return _fake.GetBinData(id);
        }

        public string UpdateResourceHistory(ResourceHistoryEntity resourceHistoryEntity)
        {
            return _fake.UpdateResourceHistory(resourceHistoryEntity);
        }

        public ResourceHistoryEntity GetResourceHistory(ResourceHistoryEntity resourceHistory)
        {
            return _fake.GetResourceHistory(resourceHistory);
        }

        public EntityCollection GetResourceHistoryByResource(ResourceEntity resourceEntity)
        {
            return _fake.GetResourceHistoryByResource(resourceEntity);
        }

        public ResourceManagerBase GetResourceManager(int bankId)
        {
            return _fake.GetResourceManager(bankId);
        }

    }
}
