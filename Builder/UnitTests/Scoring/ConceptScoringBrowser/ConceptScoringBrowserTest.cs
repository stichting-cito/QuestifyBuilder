using System;
using System.Linq;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.Scoring;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.Scoring.ConceptScoringBrowser
{
    [TestClass]
    public class ConceptScoringBrowserTest
    {
        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic")]
        public void ConceptScoringBrowserLoadsStructureAsExpected()
        {
            AssessmentItem assessmentItem = LoadAssessmentItemWithConceptScoring();
            ItemResourceEntity itemResource = new ItemResourceEntity(Guid.NewGuid());

            FakeConceptScoringBrowserCustomBankPropertiesReader fakeConceptReader = new FakeConceptScoringBrowserCustomBankPropertiesReader();

            var _2add = new ConceptStructureCustomBankPropertyValueEntity(itemResource.ResourceId, Guid.Parse(ConceptCreator.SomeConcept3));
            _2add.ConceptStructureCustomBankPropertySelectedPartCollection.Add(new ConceptStructureCustomBankPropertySelectedPartEntity() { ConceptStructurePartId = Guid.Parse(ConceptCreator.SomeConcept3_Root) });
            itemResource.CustomBankPropertyValueCollection.Add(_2add);

            itemResource.SetAssessmentItem(assessmentItem);

            var scoringParameters = assessmentItem.Parameters.DeepFetchInlineScoringParameters().ToList();
            Solution solution = assessmentItem.Solution;

            global::Questify.Builder.Logic.Scoring.ConceptScoringBrowser conceptScoringBrowser = new global::Questify.Builder.Logic.Scoring.ConceptScoringBrowser(fakeConceptReader, new FakeConceptScoringBrowserObjectFactory(), itemResource, scoringParameters, solution);

            Assert.AreEqual(6, conceptScoringBrowser.Structure.Count());
            Assert.AreEqual(1, conceptScoringBrowser.ScorableKeyCombinations.Count());

            Assert.AreEqual("C", conceptScoringBrowser.Structure[0].ConceptScorePart[0].ConceptId);
            Assert.AreEqual("A[1]", conceptScoringBrowser.Structure[0].ConceptScorePart[1].ConceptId);
            Assert.AreEqual("B[1]", conceptScoringBrowser.Structure[0].ConceptScorePart[2].ConceptId);
            Assert.AreEqual("D[1]", conceptScoringBrowser.Structure[0].ConceptScorePart[3].ConceptId);

            Assert.AreEqual("Part 1", conceptScoringBrowser.Structure[0].PartName);
            Assert.AreEqual(1, conceptScoringBrowser.Structure[0].ConceptScorePart[0].IntScore);
            Assert.AreEqual(2, conceptScoringBrowser.Structure[0].ConceptScorePart[1].IntScore);
            Assert.AreEqual(3, conceptScoringBrowser.Structure[0].ConceptScorePart[2].IntScore);
            Assert.AreEqual(4, conceptScoringBrowser.Structure[0].ConceptScorePart[3].IntScore);

            Assert.AreEqual("Part 2", conceptScoringBrowser.Structure[3].PartName);
            Assert.AreEqual(5, conceptScoringBrowser.Structure[3].ConceptScorePart[0].IntScore);
            Assert.AreEqual(6, conceptScoringBrowser.Structure[3].ConceptScorePart[1].IntScore);
            Assert.AreEqual(7, conceptScoringBrowser.Structure[3].ConceptScorePart[2].IntScore);
            Assert.AreEqual(8, conceptScoringBrowser.Structure[3].ConceptScorePart[3].IntScore);

            Assert.AreEqual("Part 2.1", conceptScoringBrowser.Structure[4].PartName);
            Assert.AreEqual(9, conceptScoringBrowser.Structure[4].ConceptScorePart[0].IntScore);
            Assert.AreEqual(10, conceptScoringBrowser.Structure[4].ConceptScorePart[1].IntScore);
            Assert.AreEqual(11, conceptScoringBrowser.Structure[4].ConceptScorePart[2].IntScore);
            Assert.AreEqual(12, conceptScoringBrowser.Structure[4].ConceptScorePart[3].IntScore);

            int[] structureIndexesThatShouldHaveNullScoreValues = { 1, 2, 5 };
            foreach (int index in structureIndexesThatShouldHaveNullScoreValues)
            {
                Assert.AreEqual(null, conceptScoringBrowser.Structure[index].ConceptScorePart[0].IntScore);
                Assert.AreEqual(null, conceptScoringBrowser.Structure[index].ConceptScorePart[1].IntScore);
                Assert.AreEqual(null, conceptScoringBrowser.Structure[index].ConceptScorePart[2].IntScore);
                Assert.AreEqual(null, conceptScoringBrowser.Structure[index].ConceptScorePart[3].IntScore);
            }
        }


        private class FakeConceptScoringBrowserCustomBankPropertiesReader : IConceptScoringBrowserDataProvider
        {
            private readonly ConceptCreator _conceptCreator;
            private readonly EntityCollection _conceptCustomBankProperties;

            internal FakeConceptScoringBrowserCustomBankPropertiesReader()
            {
                _conceptCreator = new ConceptCreator();
                _conceptCustomBankProperties = _conceptCreator.GetConcepts();
            }

            internal ConceptStructureCustomBankPropertyEntity GetFirstConceptStructureCustomBankProperty()
            {
                return _conceptCustomBankProperties.OfType<ConceptStructureCustomBankPropertyEntity>().First();
            }

            public ConceptStructurePartCustomBankPropertyEntity PopulateConceptCustomBankPropertyHierarchy(Guid id)
            {
                return _conceptCreator.GetPartById(id);
            }

            public ConceptStructureCustomBankPropertyEntity ReadConceptStructureCustomBankProperty(Guid customBankPropertyId)
            {
                return _conceptCustomBankProperties.OfType<ConceptStructureCustomBankPropertyEntity>().FirstOrDefault(x => x.CustomBankPropertyId == customBankPropertyId);
            }
        }

        private class FakeConceptScoringBrowserObjectFactory : IConceptScoringBrowserObjectFactory
        {

            public IConceptScoringBrowserHierarchyPart CreateHierarchyPart(ConceptStructurePartCustomBankPropertyEntity conceptPart, IConceptScoringBrowserHierarchyPart parent)
            {
                IConceptScoringBrowserHierarchyPart returnValue = FakeItEasy.A.Fake<IConceptScoringBrowserHierarchyPart>();

                FakeItEasy.A.CallTo(() => returnValue.Id).ReturnsLazily((x) => conceptPart.ConceptStructurePartCustomBankPropertyId);
                FakeItEasy.A.CallTo(() => returnValue.Part).ReturnsLazily((x) => conceptPart);
                FakeItEasy.A.CallTo(() => returnValue.PartName).ReturnsLazily((x) => conceptPart.Name);
                FakeItEasy.A.CallTo(() => returnValue.Depth).ReturnsLazily((x) => (parent == null) ? 0 : parent.Depth + 1);

                return returnValue;
            }

            public IConceptScoringBrowserScoreContainer CreatePartScoreContainer(IConceptScoringBrowserHierarchyPart conceptStructurePartTheScoreRelatesTo, string conceptId, int? score, IConceptScoreManipulator conceptScoreManipulator)
            {
                IConceptScoringBrowserScoreContainer returnValue = FakeItEasy.A.Fake<IConceptScoringBrowserScoreContainer>();

                FakeItEasy.A.CallTo(() => returnValue.ConceptId).ReturnsLazily((x) => conceptId);
                returnValue.IntScore = score;

                return returnValue;
            }
        }

        private AssessmentItem LoadAssessmentItemWithConceptScoring()
        {
            return (AssessmentItem)SerializeHelper.XmlDeserializeFromString(Properties.Resources.ItemWithConceptScoring, typeof(AssessmentItem));
        }
    }
}
