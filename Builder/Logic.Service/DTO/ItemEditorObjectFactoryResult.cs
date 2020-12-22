using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Logic.Service.DTO
{
    public sealed class ItemEditorObjectFactoryResult
    {
        public static ItemEditorObjectFactoryResult Create(
            ItemResourceEntity itemResourceEntity,
            AssessmentItem assessmentItem,
            ParameterSetCollection parameterSetCollection,
            ResourceManagerBase resourceManager,
            ActionEntity action,
            bool IsTransformedFromV1ToV2)
        {
            return new ItemEditorObjectFactoryResult()
            {
                ItemResourceEntity = itemResourceEntity,
                AssessmentItem = assessmentItem,
                ParameterSetCollection = parameterSetCollection,
                ResourceManagerBase = resourceManager,
                ActionEntity = action,
                IsTransformedFromV1ToV2 = IsTransformedFromV1ToV2
            };
        }

        private ItemEditorObjectFactoryResult()
        {
        }

        public ItemResourceEntity ItemResourceEntity { get; private set; }

        public AssessmentItem AssessmentItem { get; private set; }

        public ParameterSetCollection ParameterSetCollection { get; private set; }

        public ResourceManagerBase ResourceManagerBase { get; private set; }

        public ActionEntity ActionEntity { get; private set; }

        public bool IsTransformedFromV1ToV2 { get; private set; }
    }
}
