using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FakeItEasy;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.UnitTests.Fakes
{
    class HandleFakeTree
    {
        AddTreeAttribute _att = null;
        TreeCreator _treeCreator = new TreeCreator();

        internal void DealWith(AddTreeAttribute att)
        {
            if (_att == null)
            {
                _att = att;
            }
            else
                throw new NotImplementedException("Using multiple AddTreeAttribute is not currently handled ");
        }

        internal void SetFakeBehavior(IItemEditorViewModel fake)
        {
            Debug.Assert(!string.IsNullOrEmpty(_att.TreeId), "TreeID SHOULD have a guid value, Please use the constants defined on TreeCreator.");

            var treeID = Guid.Parse(_att.TreeId);
            var trees = _treeCreator.GetTrees();
            var selectedTree = trees.Where(c => ((TreeStructureCustomBankPropertyEntity)c).CustomBankPropertyId == treeID).FirstOrDefault();

            var lst = new List<EntityBase2>();
            lst.Add(selectedTree);

            Guid treePartID = (string.IsNullOrEmpty(_att.TreePartId)) ? Guid.Empty : Guid.Parse(_att.TreePartId);

            A.CallTo(() => fake.CustomBankProperties).ReturnsLazily(l => lst);

            var selected = new TreeStructureCustomBankPropertyValueEntity() { CustomBankPropertyId = treeID };

            if (treePartID != Guid.Empty)
            {
                selected.TreeStructureCustomBankPropertySelectedPartCollection.Add(new TreeStructureCustomBankPropertySelectedPartEntity() { TreeStructurePartId = treePartID });
            }

            fake.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Add(selected);

        }
    }
}
