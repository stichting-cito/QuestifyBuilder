using System.Diagnostics;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Versioning.Retrieval;
using VersionHelper = Extended_Classes.VersionHelper;

namespace Questify.Builder.Logic.Service.Direct
{
    public sealed class ResourceHistoryCreator
    {
        public static ResourceHistoryEntity CreateResourceHistoryEntity(IVersionable versionableEntity, string createdBy)
        {
            if (!(versionableEntity is IPropertyEntity))
            {
                return null;
            }

            var propertyEntity = (IPropertyEntity)versionableEntity;
            var resourceHistoryEntity = new ResourceHistoryEntity();
            var majorVersion = 0;
            var minorVersion = 0;

            resourceHistoryEntity.ResourceId = propertyEntity.Id;
            resourceHistoryEntity.ModifiedBy = createdBy;
            resourceHistoryEntity.Label = versionableEntity.MajorVersionLabel;

            if (VersionHelper.TryParseVersion(propertyEntity.Version, ref majorVersion, ref minorVersion))
            {
                resourceHistoryEntity.MajorVersion = (short)majorVersion;
                resourceHistoryEntity.MinorVersion = (short)minorVersion;
            }
            else
            {
                resourceHistoryEntity.MajorVersion = 0;
                resourceHistoryEntity.MinorVersion = 1;
            }

            if (versionableEntity.SaveObjectAsBinary)
            {
                Debug.Assert(propertyEntity.ResourceData != null && propertyEntity.ResourceData.BinData != null);
                resourceHistoryEntity.BinData = propertyEntity.ResourceData.BinData;
            }

            if (string.IsNullOrEmpty(propertyEntity.StateName) && propertyEntity.StateId.HasValue)
            {
                propertyEntity.State = ResourceFactory.Instance.GetState(propertyEntity.StateId.Value);
            }
            resourceHistoryEntity.MetaData = SerializeHelper.XmlSerializeToByteArray(CreateMetaData(propertyEntity, createdBy));

            return resourceHistoryEntity;
        }

        private static Versioning.MetaData CreateMetaData(IPropertyEntity propertyEntity, string userName)
        {
            var metaData = new Versioning.MetaData(new CustomPropertiesRetriever(propertyEntity).CreateMetaData(),
                new ConceptStructureRetriever(propertyEntity).CreateMetaData(),
                new DependentResourcesRetriever(propertyEntity).CreateMetaData(),
                new PropertyEntityRetriever(propertyEntity, userName).CreateMetaData(),
                new TreeStructureRetriever(propertyEntity).CreateMetaData());

            return metaData;
        }
    }
}