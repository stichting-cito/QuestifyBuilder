using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Enums;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.ContentModel.Scoring.Validator;
using Questify.Builder.Logic.PublicationService;
using Questify.Builder.Logic.Scoring.Reporting;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.Scoring
{
    [TestClass]
    public class BugRepro27624
    {

        readonly Guid resourceId = Guid.NewGuid();
        readonly Guid customBankPropertyId = Guid.NewGuid();
        readonly Guid conceptStructurePartId = Guid.NewGuid();
        readonly Guid conceptStructurePartId_Branch = Guid.NewGuid();

        [TestInitialize]
        public void Init()
        {
            FakeDal.Init();
            var b = BankFactory.Instance;
            var callGetCustomBankProperties = A.CallTo(() => b.GetCustomBankPropertiesForBranchById(A<int>.Ignored, A<ResourceTypeEnum>.That.IsEqualTo(ResourceTypeEnum.ItemResource)));

            callGetCustomBankProperties.ReturnsLazily(args => returnData(args));
        }

        [TestCleanup]
        public void Cleanup()
        {
            FakeDal.Deinit();
        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void AssesmentIsValid()
        {
            var a = GetAssessment();
            ScoringValidator validator = new ScoringValidator();
            validator.Validate(a); Assert.IsTrue(true);
        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void FindCombinedScoringMapKey()
        {
            var a = GetAssessment();
            var sp = a.Parameters.DeepFetchScoringParameters();
            var solution = a.Solution;
            ScoringMap mapMaker = new ScoringMap(sp, solution);
            var combinedList = mapMaker.GetMap().ToList();
            var single = combinedList.SingleOrDefault(csmk => csmk.Name == "gelukt & gelukt" && csmk.IsGroup);
            Assert.IsNotNull(single);
        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void FindCombinedScoringMapKey_IsAlsoInConcept_Present()
        {
            var a = GetAssessment();
            var sp = a.Parameters.DeepFetchScoringParameters();
            var solution = a.Solution;
            ConceptScoringMap mapMaker = new ConceptScoringMap(sp, solution);
            var combinedList = mapMaker.GetMap().ToList();
            Assert.IsTrue(combinedList.Any(csmk => csmk.Name == "gelukt & gelukt" && csmk.IsGroup));
        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void GetConceptManipulator_ForGelukt()
        {
            var a = GetAssessment();
            var sp = a.Parameters.DeepFetchScoringParameters();
            var solution = a.Solution;
            ScoringMap mapMaker = new ScoringMap(sp, solution);
            CombinedScoringMapKey mapKey = mapMaker.GetMap().Single(csmk => csmk.Name == "gelukt & gelukt" && csmk.IsGroup);
            var x = mapKey.GetConceptManipulator(a.Solution); Assert.IsTrue(true);
        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void CompareSets()
        {
            var a = GetAssessment();
            var sp = a.Parameters.DeepFetchScoringParameters();
            var solution = a.Solution;
            var keyMapList = new ScoringMap(sp, solution).GetMap().ToList();
            var conceptMapList = new ConceptScoringMap(sp, solution).GetMap().ToList();
            foreach (var keyMap in keyMapList)
            {
                var match = conceptMapList.FirstOrDefault(csmk => csmk.Name == keyMap.Name);

                if (match == null) Assert.Fail();

                var mis = match.SetNumbers.Except(match.SetNumbers).ToList();
                if (mis.Count > 0)
                    Assert.Fail();
            }

            Assert.IsTrue(true);
        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void GetConceptManipulator2x()
        {
            var a = GetAssessment();
            var sp = a.Parameters.DeepFetchScoringParameters();
            var solution = a.Solution;
            var keyMapList = new ScoringMap(sp, solution).GetMap().ToList();

            keyMapList[27].GetConceptManipulator(solution);
            keyMapList[27].GetConceptManipulator(solution);

        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void GetConceptManipulator_ForAll()
        {
            var a = GetAssessment();
            var sp = a.Parameters.DeepFetchScoringParameters();
            var solution = a.Solution;
            ScoringMap mapMaker = new ScoringMap(sp, solution);
            List<CombinedScoringMapKey> mapKeys = mapMaker.GetMap().ToList();
            mapKeys.ForEach(mapKey => mapKey.GetConceptManipulator(a.Solution));

            Assert.IsTrue(true);
        }

        [TestMethod(), TestCategory("Concept"), TestCategory("Scoring"), TestCategory("Logic"), WorkItem(27624)]
        public void ItemScoringReportDataFetcher()
        {
            var a = GetAssessment();
            var itemResource = CreateItemResourceEntity();
            var testFactory = new TestPublicationServiceFactory();
            var fake = testFactory.Fake;

            var x = A.CallTo(() => fake.GetConceptRelatedResponseProcessingForReportingPurposes(A<string>.Ignored, A<Guid>.Ignored)).ReturnsLazily(args => GetRecordedData().ToArray());


            itemResource.SetAssessmentItem(a);

            var fetcher = new ItemScoringReportDataFetcher(0);
            fetcher.PublicationServiceFactory = testFactory;

            var result = fetcher.FetchConceptScoringReportData(itemResource, false);


        }

        private List<ConceptProcessingLabelEntry> GetRecordedData()
        {
            return data.To<List<ConceptProcessingLabelEntry>>();
        }

        private EntityCollection returnData(FakeItEasy.Core.IFakeObjectCall args)
        {
            var ret = new EntityCollection();
            var _ = new ConceptStructureCustomBankPropertyEntity(customBankPropertyId);

            var conceptStructureCustomBankPropertyEntity = new ConceptStructureCustomBankPropertyEntity(customBankPropertyId);

            var part = new ConceptStructurePartCustomBankPropertyEntity(conceptStructurePartId);
            conceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.Add(part);

            part.ChildConceptStructurePartCustomBankPropertyCollection.Add(new ChildConceptStructurePartCustomBankPropertyEntity(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()));
            part.ChildConceptStructurePartCustomBankPropertyCollection.Last().ChildConceptStructurePartCustomBankProperty = new ConceptStructurePartCustomBankPropertyEntity(conceptStructurePartId_Branch);

            part.ChildConceptStructurePartCustomBankPropertyCollection.Add(new ChildConceptStructurePartCustomBankPropertyEntity(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()));
            part.ChildConceptStructurePartCustomBankPropertyCollection.Last().ChildConceptStructurePartCustomBankProperty = new ConceptStructurePartCustomBankPropertyEntity(conceptStructurePartId_Branch);


            ret.AddRange(new CustomBankPropertyEntity[] { conceptStructureCustomBankPropertyEntity });

            return ret;
        }

        private ItemResourceEntity CreateItemResourceEntity()
        {
            var ret = new ItemResourceEntity(Guid.NewGuid())
            {
                ResourceData = new ResourceDataEntity()
            };

            var x = new ConceptStructureCustomBankPropertyValueEntity(resourceId, customBankPropertyId);

            x.ConceptStructureCustomBankPropertySelectedPartCollection.Add(new ConceptStructureCustomBankPropertySelectedPartEntity(conceptStructurePartId, resourceId, customBankPropertyId));

            ret.CustomBankPropertyValueCollection.Add(x);
            return ret;
        }

        private AssessmentItem GetAssessment()
        {
            return Cito.Tester.Common.SerializeHelper.XmlDeserializeFromString<AssessmentItem>(Properties.Resources.assessment27624);
        }

        class TestPublicationServiceFactory : ItemScoringReportDataFetcher.IPublicationServiceFactory
        {
            public TestPublicationServiceFactory()
            {
                Fake = A.Fake<IPublicationService>();
            }

            public IPublicationService Fake { get; private set; }



            IPublicationService ItemScoringReportDataFetcher.IPublicationServiceFactory.Create()
            {
                return Fake;
            }
        }


        readonly XElement data = XElement.Parse(@"<ArrayOfConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I68d37ba8-f71b-4875-a0fb-c647fd7b942d-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I68d37ba8-f71b-4875-a0fb-c647fd7b942d-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I68d37ba8-f71b-4875-a0fb-c647fd7b942d[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I68d37ba8-f71b-4875-a0fb-c647fd7b942d[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE2_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_I71cc98d0-409f-4bf4-aef9-1dc5289793b0</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE2_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_I71cc98d0-409f-4bf4-aef9-1dc5289793b0</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE2_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I71cc98d0-409f-4bf4-aef9-1dc5289793b0[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE2_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I71cc98d0-409f-4bf4-aef9-1dc5289793b0[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE3_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>Ief8662d1-e84d-4ee8-b445-d76dd957c131-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE3_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>Ief8662d1-e84d-4ee8-b445-d76dd957c131-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE3_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>Ief8662d1-e84d-4ee8-b445-d76dd957c131[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE3_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>Ief8662d1-e84d-4ee8-b445-d76dd957c131[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE4_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_I524bd5b5-fbb3-4340-aa9e-d4e03f05505e</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE4_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_I524bd5b5-fbb3-4340-aa9e-d4e03f05505e</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE4_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I524bd5b5-fbb3-4340-aa9e-d4e03f05505e[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE4_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I524bd5b5-fbb3-4340-aa9e-d4e03f05505e[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE5_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>Ice1a2acb-5c0f-4c58-b8d7-dfa35887c17f-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE5_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>Ice1a2acb-5c0f-4c58-b8d7-dfa35887c17f-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE5_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>Ice1a2acb-5c0f-4c58-b8d7-dfa35887c17f[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE5_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>Ice1a2acb-5c0f-4c58-b8d7-dfa35887c17f[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE6_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I46556b66-eea6-4dc7-8726-4e464b6a0992-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE6_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I46556b66-eea6-4dc7-8726-4e464b6a0992-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE6_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I46556b66-eea6-4dc7-8726-4e464b6a0992[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE6_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I46556b66-eea6-4dc7-8726-4e464b6a0992[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE7_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_If4480f77-9ef5-4fb7-9caf-7fb57e6d2c55</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE7_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_If4480f77-9ef5-4fb7-9caf-7fb57e6d2c55</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE7_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>If4480f77-9ef5-4fb7-9caf-7fb57e6d2c55[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE7_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>If4480f77-9ef5-4fb7-9caf-7fb57e6d2c55[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE8_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I87271b21-90f3-4a9d-98ba-4d7d51647c45-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE8_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I87271b21-90f3-4a9d-98ba-4d7d51647c45-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE8_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I87271b21-90f3-4a9d-98ba-4d7d51647c45[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE8_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I87271b21-90f3-4a9d-98ba-4d7d51647c45[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE9_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_I0bb84a0c-943a-4795-bcf8-adb867d1c860</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE9_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>1-Input_I0bb84a0c-943a-4795-bcf8-adb867d1c860</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE9_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I0bb84a0c-943a-4795-bcf8-adb867d1c860[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE9_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I0bb84a0c-943a-4795-bcf8-adb867d1c860[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE10_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I670e8897-738d-4bb8-9f96-e3bb26917ea2-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE10_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I670e8897-738d-4bb8-9f96-e3bb26917ea2-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE10_NED-4</ConceptResponseLabel>
    <FactIdFirstFact>I670e8897-738d-4bb8-9f96-e3bb26917ea2[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
  <ConceptProcessingLabelEntry>
    <ExtensionData />
    <ConceptCode>NED-4.3</ConceptCode>
    <ConceptResponseLabel>CONCEPTRESPONSE10_NED-4-3</ConceptResponseLabel>
    <FactIdFirstFact>I670e8897-738d-4bb8-9f96-e3bb26917ea2[*]-hottextController</FactIdFirstFact>
  </ConceptProcessingLabelEntry>
</ArrayOfConceptProcessingLabelEntry>");


    }
}
