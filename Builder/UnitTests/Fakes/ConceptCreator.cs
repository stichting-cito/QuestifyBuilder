using System;
using System.Collections.Generic;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.UnitTests.Fakes
{
    class ConceptCreator
    {

        private Dictionary<Guid, ConceptStructurePartCustomBankPropertyEntity> partById = new Dictionary<Guid, ConceptStructurePartCustomBankPropertyEntity>();


        public const string EmptyConcept = "c01c3870-0000-0000-0000-000000000000";
        public const string SomeConcept = "c01c3870-0001-0000-0000-000000000000";
        public const string SomeConcept_Part1 = "0008A127-0001-0000-0000-000000000000";
        public const string SomeConcept_Part1_1 = "0008A127-0001-0001-0000-000000000000";
        public const string SomeConcept_Part1_2 = "0008A127-0001-0002-0000-000000000000";

        public const string SomeConcept_Part2 = "0008A127-0002-0000-0000-000000000000";
        public const string SomeConcept_Part2_1 = "0008A127-0002-0001-0000-000000000000";
        public const string SomeConcept_Part2_1_1 = "0008A127-0002-0001-1000-000000000000";
        public const string SomeConcept_Part2_1_2 = "0008A127-0002-0001-2000-000000000000";
        public const string SomeConcept_Part2_2 = "0008A127-0002-0002-0000-000000000000";
        public const string SomeConcept_Part2_2_1 = "0008A127-0002-0002-1000-000000000000";


        public const string ArjanConcept = "c01c3870-0002-0000-0000-000000000000";
        public const string ArjanConcept_ENG = "A121A000-0000-0000-0000-000000000000";
        public const string ArjanConcept_Part1 = "A121A000-0001-0000-0000-000000000000";
        public const string ArjanConcept_Part1_1 = "A121A000-0001-0001-0000-000000000000";
        public const string ArjanConcept_Part1_2 = "A121A000-0001-0002-0000-000000000000";
        public const string ArjanConcept_Part2 = "A121A000-0002-0000-0000-000000000000";
        public const string ArjanConcept_Part2_1 = "A121A000-0002-0001-0000-000000000000";
        public const string ArjanConcept_Part2_2 = "A121A000-0002-0002-0000-000000000000";
        public const string ArjanConcept_AS = "A121A000-0000-0000-0000-111111111111";



        public const string SomeConcept3 = "c01c3870-0003-0000-0000-000000000000";
        public const string SomeConcept3_Root = "0008A129-0001-0000-0000-000000000000";
        public const string SomeConcept3_Part1 = "0008A129-0001-0001-0000-000000000000";
        public const string SomeConcept3_Part1_1 = "0008A129-0001-0001-0001-000000000000";
        public const string SomeConcept3_Part1_2 = "0008A129-0001-0001-0002-000000000000";

        public const string SomeConcept3_Part2 = "0008A129-0001-0002-0000-000000000000";
        public const string SomeConcept3_Part2_1 = "0008A129-0001-0002-0001-000000000000";
        public const string SomeConcept3_Part2_2 = "0008A129-0001-0002-0002-000000000000";

        public const string SomeConcept4 = "c01c3870-0004-0000-0000-000000000000";
        public const string SomeConcept4_Root = "0008A430-0001-0000-0000-000000000000";
        public const string SomeConcept4_Part1 = "0008A430-0001-0001-0000-000000000000";
        public const string SomeConcept4_Part2 = "0008A430-0002-0002-0000-000000000000";
        public const string SomeConcept4_Part_a = "0008A430-0001-0000-000a-000000000000";
        public const string SomeConcept4_Part_b = "0008A430-0001-0000-000b-000000000000";


        public EntityCollection GetConcepts()
        {
            var ret = new EntityCollection();
            var concept = default(ConceptStructureCustomBankPropertyEntity);
            var part = default(ConceptStructurePartCustomBankPropertyEntity);
            var part2 = default(ConceptStructurePartCustomBankPropertyEntity);
            var part3 = default(ConceptStructurePartCustomBankPropertyEntity);
            concept = AddConceptToBank("Some Empty Concept", EmptyConcept);
            ret.Add(concept);
            concept = AddConceptToBank("Some Concept", SomeConcept);

            part = AddPart2Concept(concept, "Part 1", SomeConcept_Part1);
            part2 = AddPart2Part(part, "Part 1.1", SomeConcept_Part1_1);
            part2 = AddPart2Part(part, "Part 1.2", SomeConcept_Part1_2);

            part = AddPart2Concept(concept, "Part 2", SomeConcept_Part2);
            part2 = AddPart2Part(part, "Part 2.1", SomeConcept_Part2_1);
            part3 = AddPart2Part(part2, "Part 2.1.1", SomeConcept_Part2_1_1);
            part3 = AddPart2Part(part2, "Part 2.1.2", SomeConcept_Part2_1_2);

            part2 = AddPart2Part(part, "Part 2.2", SomeConcept_Part2_2);
            part3 = AddPart2Part(part2, "Part 2.2.1", SomeConcept_Part2_2_1);
            ret.Add(concept);
            concept = AddConceptToBank("Arjan Concept", ArjanConcept);
            part = AddPart2Concept(concept, "ENG", ArjanConcept_ENG);
            part2 = AddPart2Part(part, "Part 1", ArjanConcept_Part1);
            part3 = AddPart2Part(part2, "Part 1.1", ArjanConcept_Part1_1);
            part3 = AddPart2Part(part2, "Part 1.2", ArjanConcept_Part1_2);
            var AS = AddPart2Part(part3, "AS", ArjanConcept_AS);

            part2 = AddPart2Part(part, "Part 2", ArjanConcept_Part2);
            part3 = AddPart2Part(part2, "Part 2.1", ArjanConcept_Part2_1);
            part3 = AddPart2Part(part2, "Part 2.2", ArjanConcept_Part2_2);
            AddPart2Part(part3, AS);
            ret.Add(concept);
            concept = AddConceptToBank("SomeConcept3", SomeConcept3);
            part = AddPart2Concept(concept, "Root", SomeConcept3_Root);
            part2 = AddPart2Part(part, "Part 1", SomeConcept3_Part1);
            var Part_X = part2; part3 = AddPart2Part(part2, "Part 1.1", SomeConcept3_Part1_1);
            part3 = AddPart2Part(part2, "Part 1.2", SomeConcept3_Part1_2);



            part2 = AddPart2Part(part, "Part 2", SomeConcept3_Part2);
            AddPart2Part(part3, part2); part3 = AddPart2Part(part2, "Part 2.1", SomeConcept3_Part2_1);
            part3 = AddPart2Part(part2, "Part 2.2", SomeConcept3_Part2_2);

            AddPart2Part(part3, Part_X);
            ret.Add(concept);

            concept = AddConceptToBank("SomeConcept4", SomeConcept4);
            part = AddPart2Concept(concept, "Root", SomeConcept4_Root);
            var part1 = AddPart2Part(part, "Part 1", SomeConcept4_Part1);
            part2 = AddPart2Part(part, "Part 2", SomeConcept4_Part2);
            var partA = AddPart2Part(part1, "Part a", SomeConcept4_Part_a);
            var partB = AddPart2Part(part1, "Part b", SomeConcept4_Part_b);
            AddPart2Part(part2, partA);
            AddPart2Part(part2, partB);
            ret.Add(concept);
            return ret;
        }



        private ConceptStructureCustomBankPropertyEntity AddConceptToBank(string name, string id)
        {
            ConceptStructureCustomBankPropertyEntity ret = new ConceptStructureCustomBankPropertyEntity(Guid.Parse(id)) { Name = name };
            return ret;
        }

        private ConceptStructurePartCustomBankPropertyEntity AddPart2Concept(ConceptStructureCustomBankPropertyEntity concept, string name, string id)
        {
            ConceptStructurePartCustomBankPropertyEntity ret = new ConceptStructurePartCustomBankPropertyEntity(Guid.Parse(id)) { Name = name };
            ret.ConceptType = new ConceptTypeEntity { ApplicableToMask = 1 };
            concept.ConceptStructurePartCustomBankPropertyCollection.Add(ret);
            partById.Add(Guid.Parse(id), ret);
            return ret;
        }

        private ConceptStructurePartCustomBankPropertyEntity AddPart2Part(ConceptStructurePartCustomBankPropertyEntity parent, string name, string id)
        {
            ConceptStructurePartCustomBankPropertyEntity ret = new ConceptStructurePartCustomBankPropertyEntity(Guid.Parse(id)) { Name = name };
            ret.ConceptType = new ConceptTypeEntity { ApplicableToMask = 1 };

            ChildConceptStructurePartCustomBankPropertyEntity hook = new ChildConceptStructurePartCustomBankPropertyEntity(new Guid(), parent.CustomBankPropertyId, ret.CustomBankPropertyId);
            hook.ChildConceptStructurePartCustomBankProperty = ret;
            parent.ChildConceptStructurePartCustomBankPropertyCollection.Add(hook);
            partById.Add(Guid.Parse(id), ret);
            return ret;
        }

        private void AddPart2Part(ConceptStructurePartCustomBankPropertyEntity parent, ConceptStructurePartCustomBankPropertyEntity child)
        {
            ChildConceptStructurePartCustomBankPropertyEntity hook = new ChildConceptStructurePartCustomBankPropertyEntity(new Guid(), parent.CustomBankPropertyId, child.CustomBankPropertyId);
            hook.ChildConceptStructurePartCustomBankProperty = child;
            parent.ChildConceptStructurePartCustomBankPropertyCollection.Add(hook);
        }



        internal ConceptStructurePartCustomBankPropertyEntity GetPartById(Guid id)
        {
            return partById[id];
        }
    }
}
