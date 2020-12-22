using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using BankDto = Questify.Builder.Logic.Service.Model.Entities.BankDto;

namespace Questify.Builder.Logic.Service.HelperFunctions
{
    public static class Extensions
    {
        public static IEnumerable<BankDto> Flattened(this IEnumerable<BankDto> bankStructure)
        {
            if (bankStructure == null) return null;
            IEnumerable<BankDto> banks = new List<BankDto>();
            foreach (var rootBankLoop in bankStructure.ToList())
            {
                var rootbank = rootBankLoop;
                GetBanks(rootbank, ref banks);
            }
            return banks;
        }

        public static IEnumerable<BankDto> Flattened(this BankDto rootBank)
        {
            IEnumerable<BankDto> banks = new List<BankDto>();
            GetBanks(rootBank, ref banks);
            return banks;
        }

        public static IEnumerable<BankDto> Hierarchy(this IEnumerable<BankDto> flattenedBanks)
        {
            if (flattenedBanks == null) return null;
            var flattenedBankList = flattenedBanks.ToList();
            var banks = new List<BankDto>();
            foreach (var rootBankLoop in flattenedBankList.Where(b => b.ParentBankId.HasValue == false))
            {
                var rootbank = rootBankLoop;
                ReFillCollection(ref rootbank, flattenedBankList.ToList());
                banks.Add(rootbank);
            }
            return banks;
        }

        private static void ReFillCollection(ref BankDto currentBank, IList<BankDto> flattenedBanks)
        {
            currentBank.BankCollection = new List<BankDto>();
            var bId = currentBank.Id;
            IList<BankDto> childBanks = flattenedBanks.Where(b => b.ParentBankId.HasValue && b.ParentBankId.Value == bId).ToList();
            if (!childBanks.Any()) return;
            IList<BankDto> childBanksToAdd = new List<BankDto>();
            foreach (var c in childBanks)
            {
                var childBank = c;
                ReFillCollection(ref childBank, flattenedBanks);
                childBanksToAdd.Add(childBank);
            }
            currentBank.BankCollection = childBanksToAdd;
        }

        private static void GetBanks(BankDto rootBank, ref IEnumerable<BankDto> banks)
        {
            banks = banks.Union(new List<BankDto> { rootBank }.ToList());
            if (rootBank.BankCollection == null) return;
            banks = banks.Union(rootBank.BankCollection);
            foreach (var bankInLoop in rootBank.BankCollection)
            {
                var bank = bankInLoop;
                GetBanks(bank, ref banks);
            }
        }

        public static void WaitWithPumping(this Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            var nestedFrame = new DispatcherFrame();
            task.ContinueWith(x => nestedFrame.Continue = false);
            Dispatcher.PushFrame(nestedFrame);
            task.Wait();
        }
    }
}
