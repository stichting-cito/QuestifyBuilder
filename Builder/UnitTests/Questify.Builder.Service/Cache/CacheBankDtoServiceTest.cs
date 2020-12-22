
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    [TestClass]
    public class CacheBankDtoServiceTest
    {
        private List<int> _updatedBanks;
        private List<int> _deletedBanks;
        private List<KeyValuePair<int, int?>> _currentBanks; private List<KeyValuePair<int, int?>> _addedBanks;
        [TestMethod, TestCategory("Cache")]
        public void GetAllIsCached()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());

            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.All();
            bankSrvDecorator.All();
            bankSrvDecorator.All();
            bankSrvDecorator.All();
            fakeGetAllBanks.MustHaveHappened(Repeated.Exactly.Times(1));
        }

        [TestMethod, TestCategory("Cache")]
        public void GetIsCached()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetBank = A.CallTo(() => bankDtoService.All());
            fakeGetBank.ReturnsLazily(args => GetAllBanks());
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.Get(1);
            bankSrvDecorator.Get(1);
            bankSrvDecorator.Get(1);
            bankSrvDecorator.Get(1);

            fakeGetBank.MustHaveHappened(Repeated.Exactly.Times(1));
        }

        [TestMethod, TestCategory("Cache")]
        public void GetAllIsInvalidatedInBankCollection()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            var fakeGetBank = A.CallTo(() => bankDtoService.Get(A<int>.Ignored));
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            fakeGetBank.ReturnsLazily(args => GetBank((int)args.Arguments[0]));
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.All();
            _updatedBanks.Add(2);
            bankSrvDecorator.EntityChanged(2); var list = bankSrvDecorator.All();

            var parentBank = list.FirstOrDefault(b => b.Id == 1);
            if (parentBank != null)
            {
                var childBank = parentBank.BankCollection.FirstOrDefault(cb => cb.Id == 2);
                if (childBank != null)
                {
                    Assert.IsTrue(childBank != null && childBank.Name == "updated");
                }
            }
            fakeGetAllBanks.MustHaveHappened(Repeated.Exactly.Times(2));
        }

        [TestMethod, TestCategory("Cache")]
        public void GetIsInvalidatedInBankCollectionWhenChildBankIsDeleted()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());

            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.Get(1);
            _deletedBanks.Add(2);
            bankSrvDecorator.EntityChanged(2); var parentBank = bankSrvDecorator.Get(1);

            BankDto childBank = null;
            if (parentBank.BankCollection != null)
            {
                childBank = parentBank.BankCollection.FirstOrDefault(cb => cb.Id == 2);
            }

            if (childBank != null)
            {
                Assert.IsTrue(childBank == null);
            }
        }

        [TestMethod, TestCategory("Cache")]
        public void NewBankIsAddedToCacheList()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            var fakeGetBank = A.CallTo(() => bankDtoService.Get(A<int>.Ignored));
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            fakeGetBank.ReturnsLazily(
                args => GetBank((int)args.Arguments[0])
                );
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.All();
            _addedBanks.Add(new KeyValuePair<int, int?>(99, 1));
            bankSrvDecorator.EntityChanged(99);
            var parentBank = bankSrvDecorator.Get(1);
            var all = bankSrvDecorator.All();
            Assert.IsTrue(parentBank.BankCollection.Any(cb => cb.Id == 99));
            Assert.IsTrue(all.FirstOrDefault(b => b.Id == 1).BankCollection.Any(cb => cb.Id == 99));
        }

        [TestMethod, TestCategory("Cache")]
        public void NewParentBankIsAddedToCacheList()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            var fakeGetBank = A.CallTo(() => bankDtoService.Get(A<int>.Ignored));
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            fakeGetBank.ReturnsLazily(
                args => GetBank((int)args.Arguments[0])
                );
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.All();
            _addedBanks.Add(new KeyValuePair<int, int?>(99, null));
            bankSrvDecorator.EntityChanged(99);
            var all = bankSrvDecorator.All();

            Assert.IsTrue(all.Any(b => b.Id == 99));
        }

        [TestMethod, TestCategory("Cache")]
        public void ParentBankIsUpdatedToCacheList()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            var fakeGetBank = A.CallTo(() => bankDtoService.Get(A<int>.Ignored));
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            fakeGetBank.ReturnsLazily(
                args => GetBank((int)args.Arguments[0])
                );
            _currentBanks.Add(new KeyValuePair<int, int?>(99, null));
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.All();
            _updatedBanks.Add(99);
            bankSrvDecorator.EntityChanged(99);
            var all = bankSrvDecorator.All();
            var single = bankSrvDecorator.Get(99);

            Assert.IsTrue(all.FirstOrDefault(b => b.Id == 99).Name == "updated");
            Assert.IsTrue(single.Name == "updated");
        }

        [TestMethod, TestCategory("Cache")]
        public void ChildBankIsUpdatedToCacheList()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            var fakeGetBank = A.CallTo(() => bankDtoService.Get(A<int>.Ignored));
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            fakeGetBank.ReturnsLazily(
                args => GetBank((int)args.Arguments[0])
                );
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.Get(1);
            bankSrvDecorator.All();
            _updatedBanks.Add(2); bankSrvDecorator.EntityChanged(2); var parentBank = bankSrvDecorator.Get(1);
            var list = bankSrvDecorator.All();
            Assert.IsTrue(parentBank.BankCollection.FirstOrDefault(cb => cb.Id == 2).Name == "updated");
            Assert.IsTrue(list.FirstOrDefault(b => b.Id == 1).BankCollection.FirstOrDefault(cb => cb.Id == 2).Name == "updated");
            fakeGetAllBanks.MustHaveHappened(Repeated.Exactly.Times(2));
        }

        [TestMethod, TestCategory("Cache")]
        public void ChildBankIsDeletedFromCacheList()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.Get(1);
            bankSrvDecorator.All();
            _deletedBanks.Add(2); bankSrvDecorator.EntityChanged(2); var parentBank = bankSrvDecorator.Get(1);
            var list = bankSrvDecorator.All();
            var collection = list.FirstOrDefault(b => b.Id == 1).BankCollection;
            Assert.IsTrue(parentBank.BankCollection == null || parentBank.BankCollection.All(cb => cb.Id != 2));
            Assert.IsTrue(collection == null || collection.Count == 0);

            fakeGetAllBanks.MustHaveHappened(Repeated.Exactly.Times(2));
        }

        [TestMethod, TestCategory("Cache")]
        public void ParentBankIsDeletedFromCacheList()
        {
            var bankDtoService = A.Fake<IBankDtoRepository>();
            var fakeGetAllBanks = A.CallTo(() => bankDtoService.All());
            var fakeGetBank = A.CallTo(() => bankDtoService.Get(A<int>.Ignored));
            fakeGetAllBanks.ReturnsLazily(args => GetAllBanks());
            fakeGetBank.ReturnsLazily(
                args => GetBank((int)args.Arguments[0]));
            _currentBanks.Add(new KeyValuePair<int, int?>(99, null));
            var bankSrvDecorator = new CacheBankDtoService(bankDtoService);
            bankSrvDecorator.All();
            _deletedBanks.Add(99);
            bankSrvDecorator.EntityChanged(99);
            var all = bankSrvDecorator.All();
            var single = bankSrvDecorator.Get(99);

            Assert.IsFalse(all.Any(b => b.Id == 99));
            Assert.IsTrue(single == null);
        }



        [TestInitialize()]
        public void Init()
        {
            _updatedBanks = new List<int>();
            _deletedBanks = new List<int>();
            _addedBanks = new List<KeyValuePair<int, int?>>();
            _currentBanks = new List<KeyValuePair<int, int?>>
            {
                new KeyValuePair<int, int?>(1, null),
                new KeyValuePair<int, int?>(2, 1)
            };
            FakeDal.Init();
        }

        [TestCleanup()]
        public void DeInit()
        {
            FakeDal.Deinit();
        }

        private IEnumerable<BankDto> GetAllBanks()
        {
            var collection = FillCollection(_currentBanks);
            collection = collection.Union(FillCollection(_addedBanks)).ToList();
            return collection;
        }

        private List<BankDto> FillCollection(IEnumerable<KeyValuePair<int, int?>> banks)
        {
            return banks.Where(b => b.Value.HasValue == false).Select(bank => GetBank(bank.Key)).Where(b => b != null).ToList();
        }

        private void AddBank(BankDto bankToAdd, ref List<BankDto> banks, ref bool isAdded)
        {
            if (bankToAdd.ParentBankId.HasValue)
            {
                foreach (var b in banks)
                {
                    if (b.Id == bankToAdd.ParentBankId.Value)
                    {
                        b.BankCollection = b.BankCollection.Union(new List<BankDto> { bankToAdd }).ToList();
                        isAdded = true;
                    }
                }
            }
        }

        private BankDto GetBank(int bankId)
        {
            return AddBank(bankId, null);
        }

        private BankDto AddBank(int bankId, int? parentBankId)
        {
            return AddBank(bankId, "Bank" + bankId, parentBankId);
        }

        private BankDto AddBank(int id, string name, int? parentBankId)
        {
            if (_deletedBanks.Contains(id)) { return null; }
            var isAdded = _addedBanks.Any(b => b.Key == id);
            var isCurrent = _currentBanks.Any(b => b.Key == id);
            if (isAdded)
            {
                parentBankId = _addedBanks.FirstOrDefault(b => b.Key == id).Value;
            }
            if (isCurrent)
            {
                parentBankId = _currentBanks.FirstOrDefault(b => b.Key == id).Value;
            }
            var bank = new BankDto()
            {
                Id = id,
                Name = name,
                ParentBankId = parentBankId
            };
            if (_updatedBanks.Contains(id))
            {
                bank.Name = "updated";
            }
            AddChildBanks(ref bank, _currentBanks);
            AddChildBanks(ref bank, _addedBanks);
            return bank;
        }

        private void AddChildBanks(ref BankDto bank, IEnumerable<KeyValuePair<int, int?>> banks)
        {
            var bankId = bank.Id;
            var collection = new List<BankDto>();
            if (bank.BankCollection != null)
            {
                collection = bank.BankCollection.ToList();
            }

            foreach (var bankInList in banks.Where(b => b.Value.HasValue && b.Value.Value == bankId))
            {
                var isAdded = false;
                var bankToAdd = AddBank(bankInList.Key, bankInList.Value);
                if (bankToAdd != null)
                {
                    AddBank(bankToAdd, ref collection, ref isAdded);
                    if (isAdded == false)
                    {
                        collection.Add(bankToAdd);
                    }
                }
            }
            if (collection.Any())
            {
                bank.BankCollection = collection;
            }
        }

    }
}
