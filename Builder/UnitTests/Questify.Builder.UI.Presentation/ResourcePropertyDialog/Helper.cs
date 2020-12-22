using System;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Versioning.Retrieval;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ResourcePropertyDialog
{
    class Helper
    {
        private static DateTime _now = DateTime.Now;

        static internal AssessmentItem CreateAssessmentItem(string title)
        {
            return CreateAssessmentItem(title, null);
        }

        static internal AssessmentItem CreateAssessmentItem(string title, string iltName)
        {
            AssessmentItem assessmentItem = new AssessmentItem();

            assessmentItem.Identifier = Guid.NewGuid().ToString();
            assessmentItem.Title = title;
            assessmentItem.LayoutTemplateSourceName = iltName;

            return assessmentItem;
        }

        static internal IVersionable CreateResourceEntity<T>(object resourceData) where T : new()
        {
            IPropertyEntity propertyEntity = new T() as IPropertyEntity;

            propertyEntity.Id = Guid.NewGuid();
            propertyEntity.Name = "TestItem - Name";
            propertyEntity.Title = "TestItem - Title";
            propertyEntity.Description = "TestItem - Description";
            propertyEntity.StateId = null;
            propertyEntity.ModifiedDate = _now;
            propertyEntity.ModifiedBy = 1;
            propertyEntity.ResourceData = new ResourceDataEntity();

            if (resourceData != null)
            {
                propertyEntity.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(resourceData);
            }

            return (IVersionable)propertyEntity;
        }

        static internal Versioning.MetaData CreateMetaData(IPropertyEntity propertyEntity, String userName)
        {
            var metaData = new Versioning.MetaData(new CustomPropertiesRetriever(propertyEntity).CreateMetaData(),
                                         new ConceptStructureRetriever(propertyEntity).CreateMetaData(),
                                         new DependentResourcesRetriever(propertyEntity).CreateMetaData(),
                                         new PropertyEntityRetriever(propertyEntity, userName).CreateMetaData(),
                                         new TreeStructureRetriever(propertyEntity).CreateMetaData());

            return metaData;
        }

        static internal Versioning.MetaData DeserializeFromByteArray(byte[] metaData)
        {
            return (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(metaData, typeof(Versioning.MetaData));
        }

    }
}
