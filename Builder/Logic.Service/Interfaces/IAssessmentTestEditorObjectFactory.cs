using System;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IAssessmentTestEditorObjectFactory
    {
        Tuple<AssessmentTestResourceEntity, AssessmentTest2, bool, ResourceManagerBase, ActionEntity> GetRequiredObjectsForAssessmentTestWithId(Guid id);
        string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource);
    }
}
