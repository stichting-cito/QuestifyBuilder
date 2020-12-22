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
    internal class HandleFakeConcept
    {
        AddConceptAttribute _att = null;
        ConceptCreator _conceptCreator = new ConceptCreator();


        internal void DealWith(AddConceptAttribute att)
        {
            if (_att == null)
            {
                _att = att;
            }
            else
                throw new NotImplementedException("Using multiple AddConceptAttribute is not currently handled ");
        }

        internal void SetFakeBehavior(IItemEditorViewModel fake)
        {
            Debug.Assert(!string.IsNullOrEmpty(_att.ConceptId), "ConceptID SHOULD have a guid value, Please use the constants defined on ConceptCreator.");

            var conceptID = Guid.Parse(_att.ConceptId);
            var concepts = _conceptCreator.GetConcepts();
            var selectedConcept = concepts.FirstOrDefault(c => ((ConceptStructureCustomBankPropertyEntity)c).CustomBankPropertyId == conceptID);

            var lst = new List<EntityBase2>();
            lst.Add(selectedConcept);

            Guid conceptPartID = (string.IsNullOrEmpty(_att.ConceptPartId)) ? Guid.Empty : Guid.Parse(_att.ConceptPartId);

            A.CallTo(() => fake.CustomBankProperties).ReturnsLazily(l => lst);
            A.CallTo(() => fake.IsConceptDefinedOnBankBranch).ReturnsLazily(l => true);

            var selected = new ConceptStructureCustomBankPropertyValueEntity() { CustomBankPropertyId = conceptID };

            if (conceptPartID != Guid.Empty)
            {
                selected.ConceptStructureCustomBankPropertySelectedPartCollection.Add(new ConceptStructureCustomBankPropertySelectedPartEntity() { ConceptStructurePartId = conceptPartID });
            }

            fake.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Add(selected);

        }

        internal ConceptStructurePartCustomBankPropertyEntity GetPartById(Guid id)
        {
            return _conceptCreator.GetPartById(id);
        }


    }
}
