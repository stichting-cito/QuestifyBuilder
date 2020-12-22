using System;
using System.Diagnostics;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Questify.Builder.Logic.Service.DTO;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.UnitTests.Framework.Faketory;

namespace Questify.Builder.UnitTests.Fakes
{
    class HandlerFakeItemEditorLoader
    {
        private readonly IItemEditorObjectFactory _fakeFactory;
        private readonly ItemEditorObjectStrategy _strategy;
        private readonly ItemEditorBankObjectStrategy _bankObjectstrategy;
        private readonly bool _oldItem; private readonly Guid _conceptId;
        private readonly Guid _conceptPartId;
        private readonly Guid _treeId;
        private readonly Guid _treePartId;

        public HandlerFakeItemEditorLoader(IItemEditorObjectFactory fake_Factory, FakeObjectFactoryBehaviorAttribute attributte)
        {
            _fakeFactory = fake_Factory;
            _strategy = attributte.Strategy;
            _oldItem = attributte.IsOldItem;
            _bankObjectstrategy = attributte.BankProperties;

            if (!string.IsNullOrEmpty(attributte.ConceptId))
            {
                _conceptId = Guid.Parse(attributte.ConceptId);
                _conceptPartId = Guid.Parse(attributte.ConceptPartId);
            }

            if (!string.IsNullOrEmpty(attributte.TreeId))
            {
                _treeId = Guid.Parse(attributte.TreeId);
                _treePartId = Guid.Parse(attributte.TreePartId);
            }

            InitCallRewire();
            InitBankObjectsCallRewire();
        }


        private void InitCallRewire()
        {
            var CallToGetReqObjs = A.CallTo(() => _fakeFactory.GetRequiredObjectsForItemWithId(A<Guid>.Ignored));

            var CallNewItems = A.CallTo(() => _fakeFactory.GetObjectsForNewItem(A<Guid>.Ignored, A<int>.Ignored));

            switch (_strategy)
            {
                case ItemEditorObjectStrategy.ReturnNull:
                    CallToGetReqObjs.ReturnsLazily((arg) => ReturnNull(arg.GetArgument<Guid>(0)));
                    CallNewItems.ReturnsLazily((arg) => ReturnNull(arg.GetArgument<Guid>(0)));
                    break;
                case ItemEditorObjectStrategy.GiveException:
                    CallToGetReqObjs.ReturnsLazily((arg) => GiveException(arg.GetArgument<Guid>(0)));
                    CallNewItems.ReturnsLazily((arg) => GiveException(arg.GetArgument<Guid>(0)));
                    break;
                case ItemEditorObjectStrategy.DefaultObjects:
                    CallToGetReqObjs.ReturnsLazily((arg) => DefaultObjects(arg.GetArgument<Guid>(0)));
                    CallNewItems.ReturnsLazily((arg) => DefaultObjects(arg.GetArgument<Guid>(0)));
                    break;
                case ItemEditorObjectStrategy.AbsoluteValidMinimum:
                    CallToGetReqObjs.ReturnsLazily((arg) => AbsoluteValidMinimum(arg.GetArgument<Guid>(0), 1, false));
                    CallNewItems.ReturnsLazily((arg) => AbsoluteValidMinimum(arg.GetArgument<Guid>(0), arg.GetArgument<int>(1), true));
                    break;
                case ItemEditorObjectStrategy.ValidItem_WithConcept:
                    CallToGetReqObjs.ReturnsLazily((arg) => Valid_WithConcept(arg.GetArgument<Guid>(0), 1));
                    CallNewItems.ReturnsLazily((arg) => Valid_WithConcept(arg.GetArgument<Guid>(0), arg.GetArgument<int>(1)));
                    break;
                default:
                    Debug.Assert(false, "Not handled strategy!!");
                    break;
            }
        }

        private ItemEditorObjectFactoryResult ReturnNull(Guid id)
        {
            return null;
        }

        private ItemEditorObjectFactoryResult GiveException(Guid id)
        {
            throw new Exception();
        }

        private ItemEditorObjectFactoryResult DefaultObjects(Guid id)
        {
            return ItemEditorObjectFactoryResult.
                Create(new ItemResourceEntity(),
                    new AssessmentItem(),
                    new ParameterSetCollection(),
                    FakeResourceManager.MakeResourceManagerBase(),
                    new ActionEntity(),
                    _oldItem);
        }

        private ItemEditorObjectFactoryResult AbsoluteValidMinimum(Guid id, int bankId, bool isNew)
        {
            AssessmentItem a;
            if (isNew)
                a = new AssessmentItem() { LayoutTemplateSourceName = "FakeLayoutTemplateSourceName" };
            else
                a = new AssessmentItem() { Identifier = "Identifier", Title = "Title", LayoutTemplateSourceName = "FakeLayoutTemplateSourceName" };
            Debug.Assert(a.IsValid || isNew, "The assessment item to be returned is no longer valid.");
            return ItemEditorObjectFactoryResult.
                Create(
                    new ItemResourceEntity() { Bank = new BankEntity(bankId), ResourceData = new ResourceDataEntity() },
                    a,
                    new ParameterSetCollection(),
                    FakeResourceManager.MakeResourceManagerBase(),
                    null,
                    _oldItem);
        }

        private ItemEditorObjectFactoryResult Valid_WithConcept(Guid id, int bankId)
        {
            var ret = AbsoluteValidMinimum(id, bankId, false);
            Debug.Assert(_conceptId != Guid.Empty, "If you want to have Concepts defined, please fill ConceptId");
            Debug.Assert(_conceptPartId != Guid.Empty, "If you want to have Concepts defined, please fill ConceptPartId");

            var _2add = new ConceptStructureCustomBankPropertyValueEntity(id, _conceptId);
            _2add.ConceptStructureCustomBankPropertySelectedPartCollection.Add(new ConceptStructureCustomBankPropertySelectedPartEntity(_conceptPartId, id, _conceptId));
            ret.ItemResourceEntity.CustomBankPropertyValueCollection.Add(_2add);

            return ret;
        }

        private ItemEditorObjectFactoryResult Valid_WithTrees(Guid id, int bankId)
        {
            var ret = AbsoluteValidMinimum(id, bankId, false);
            Debug.Assert(_treeId != Guid.Empty, "If you want to have Trees defined, please fill TreeId");
            Debug.Assert(_treePartId != Guid.Empty, "If you want to have Trees defined, please fill TreePartId");

            var _2add = new TreeStructureCustomBankPropertyValueEntity(id, _treeId);
            _2add.TreeStructureCustomBankPropertySelectedPartCollection.Add(new TreeStructureCustomBankPropertySelectedPartEntity(_treePartId, id, _treeId));
            ret.ItemResourceEntity.CustomBankPropertyValueCollection.Add(_2add);

            return ret;
        }



        private void InitBankObjectsCallRewire()
        {
            var call = A.CallTo(() => _fakeFactory.GetCustomBankPropertiesForBranch(A<int>.Ignored));
            switch (_bankObjectstrategy)
            {
                case ItemEditorBankObjectStrategy.ReturnsNull:
                    break;
                case ItemEditorBankObjectStrategy.GiveException:
                    call.ReturnsLazily((arg) => BankProps_GiveException());
                    break;
                case ItemEditorBankObjectStrategy.StaticExample1:
                    call.ReturnsLazily((arg) => BankProps_GiveStaticExample1());
                    break;
                case ItemEditorBankObjectStrategy.StaticExample2:
                    call.ReturnsLazily((arg) => BankProps_GiveStaticExample2());
                    break;
                default:
                    Debug.Assert(false, "Not handled strategy!!");
                    break;
            }
        }

        private EntityCollection BankProps_GiveException()
        {
            throw new Exception();
        }

        private EntityCollection BankProps_GiveStaticExample1()
        {
            var conceptCreator = new ConceptCreator();
            return conceptCreator.GetConcepts();
        }

        private EntityCollection BankProps_GiveStaticExample2()
        {
            var treeCreator = new TreeCreator();
            return treeCreator.GetTrees();
        }


    }
}
