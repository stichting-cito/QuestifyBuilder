using System.Collections.Generic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenAssessmentTestProxy : LlblGenResourceProxy<AssessmentTestResourceDto>, IConvertResource<AssessmentTestResourceDto, AssessmentTestResourceEntity>
    {
        public AssessmentTestResourceDto Convert(AssessmentTestResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var assessmentTest = base.GetResource(new AssessmentTestResourceDto(), resourceEntity, customProperties);
            assessmentTest.CanPropose = resourceEntity.CanPropose();
            assessmentTest.IsTemplate = resourceEntity.IsTemplate;
            return assessmentTest;
        }

        public AssessmentTestResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var assessmentTest = (AssessmentTestResourceEntity)resourceEntity;
            return assessmentTest == null ? null : Convert(assessmentTest, customProperties);
        }


    }
}
