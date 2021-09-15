
using System;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.UnitTests.Fakes
{
    class TreeCreator
    {

        //SomeTree1
        // -Part 1
        //   -Part 1.1
        //   -Part 1.2
        // -Part 2
        //   -Part 2.1
        //     -Part 2.1.1
        //     -Part 2.1.2
        //   -Part 2.2
        //     -Part 2.2.1

        public const string EmptyTree = "c01c3870-0000-0000-0000-000000000000";
        public const string SomeTree1 = "c01c3870-0001-0000-0000-000000000000";
        public const string SomeTree1_Part1 = "0008A127-0001-0000-0000-000000000000";
        public const string SomeTree1_Part1_1 = "0008A127-0001-0001-0000-000000000000";
        public const string SomeTree1_Part1_2 = "0008A127-0001-0002-0000-000000000000";
        
        public const string SomeTree1_Part2 = "0008A127-0002-0000-0000-000000000000";
        public const string SomeTree1_Part2_1 = "0008A127-0002-0001-0000-000000000000";
        public const string SomeTree1_Part2_1_1 = "0008A127-0002-0001-1000-000000000000";
        public const string SomeTree1_Part2_1_2 = "0008A127-0002-0001-2000-000000000000";
        public const string SomeTree1_Part2_2 = "0008A127-0002-0002-0000-000000000000";
        public const string SomeTree1_Part2_2_1 = "0008A127-0002-0002-1000-000000000000";

        //SomeTree2
        // -Root
        //   -Part 1
        //     -Part A
        //     -Part B
        //   -Part 2
        //     -Part A
        //     -Part B
        public const string SomeTree2 = "c01c3870-0004-0000-0000-000000000000";
        public const string SomeTree2_Root = "0008A430-0001-0000-0000-000000000000";
        public const string SomeTree2_Part1 = "0008A430-0001-0001-0000-000000000000";
        public const string SomeTree2_Part2 = "0008A430-0002-0002-0000-000000000000";
        public const string SomeTree2_Part_a = "0008A430-0001-0000-000a-000000000000";
        public const string SomeTree2_Part_b = "0008A430-0001-0000-000b-000000000000";


        public EntityCollection GetTrees()
        {
            var ret = new EntityCollection();
            var tree = default(TreeStructureCustomBankPropertyEntity);
            var part = default(TreeStructurePartCustomBankPropertyEntity);
            var part2 = default(TreeStructurePartCustomBankPropertyEntity);
            var part3 = default(TreeStructurePartCustomBankPropertyEntity);
            #region Empty
            //Empty Tree
            tree = AddTreeToBank("Some Empty Tree", EmptyTree);
            ret.Add(tree);
            #endregion
            #region Some Tree Tree
            //Some Tree Tree
            tree = AddTreeToBank("Some Tree", SomeTree1);

            part = AddPart2Tree(tree, "Part 1", SomeTree1_Part1);
            part2 = AddPart2Part(part, "Part 1.1", SomeTree1_Part1_1);
            part2 = AddPart2Part(part, "Part 1.2", SomeTree1_Part1_2);

            part = AddPart2Tree(tree, "Part 2", SomeTree1_Part2);
            part2 = AddPart2Part(part, "Part 2.1", SomeTree1_Part2_1);
            part3 = AddPart2Part(part2, "Part 2.1.1", SomeTree1_Part2_1_1);
            part3 = AddPart2Part(part2, "Part 2.1.2", SomeTree1_Part2_1_2);

            part2 = AddPart2Part(part, "Part 2.2", SomeTree1_Part2_2);
            part3 = AddPart2Part(part2, "Part 2.2.1", SomeTree1_Part2_2_1);
            ret.Add(tree);
            #endregion

            #region SomeTree2
            //SomeTree4
            tree = AddTreeToBank("SomeTree4", SomeTree2);
            part = AddPart2Tree(tree, "Root", SomeTree2_Root);
            var part1 = AddPart2Part(part, "Part 1", SomeTree2_Part1);
            part2 = AddPart2Part(part, "Part 2", SomeTree2_Part2);
            var partA = AddPart2Part(part1, "Part a", SomeTree2_Part_a);
            var partB = AddPart2Part(part1, "Part b", SomeTree2_Part_b);
            AddPart2Part(part2, partA);
            AddPart2Part(part2, partB);
            ret.Add(tree);
            #endregion
            return ret;
        }



        #region Tree Creating
        private TreeStructureCustomBankPropertyEntity AddTreeToBank(string name, string id)
        {
            TreeStructureCustomBankPropertyEntity ret = new TreeStructureCustomBankPropertyEntity(Guid.Parse(id)) { Name = name };
            return ret;
        }

        private TreeStructurePartCustomBankPropertyEntity AddPart2Tree(TreeStructureCustomBankPropertyEntity Tree, string name, string id)
        {
            TreeStructurePartCustomBankPropertyEntity ret = new TreeStructurePartCustomBankPropertyEntity(Guid.Parse(id)) { Name = name };
            //hook to Tree
            Tree.TreeStructurePartCustomBankPropertyCollection.Add(ret);
            return ret;
        }

        private TreeStructurePartCustomBankPropertyEntity AddPart2Part(TreeStructurePartCustomBankPropertyEntity parent, string name, string id)
        {
            TreeStructurePartCustomBankPropertyEntity ret = new TreeStructurePartCustomBankPropertyEntity(Guid.Parse(id)) { Name = name };

            //hook to parent TreePart 
            ChildTreeStructurePartCustomBankPropertyEntity hook = new ChildTreeStructurePartCustomBankPropertyEntity(new Guid(), parent.CustomBankPropertyId, ret.CustomBankPropertyId);
            parent.ChildTreeStructurePartCustomBankPropertyCollection.Add(hook);

            return ret;
        }

        private void AddPart2Part(TreeStructurePartCustomBankPropertyEntity parent, TreeStructurePartCustomBankPropertyEntity child)
        {
            //hook to parent TreePart 
            ChildTreeStructurePartCustomBankPropertyEntity hook = new ChildTreeStructurePartCustomBankPropertyEntity(new Guid(), parent.CustomBankPropertyId, child.CustomBankPropertyId);
            parent.ChildTreeStructurePartCustomBankPropertyCollection.Add(hook);
        }

        #endregion
    }
}
