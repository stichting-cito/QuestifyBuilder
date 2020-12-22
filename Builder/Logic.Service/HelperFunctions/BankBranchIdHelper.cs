
using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.HelperFunctions
{

    public class BankBranchIdHelper
    {

        public IPermissionService PermissionService { get; set; }

        public BankBranchIdHelper()
        {
            PermissionService = PermissionFactory.Instance;
        }

        public BankBranchIdHelper(IPermissionService permissionServiceOverride)
        {
            PermissionService = permissionServiceOverride;
        }

        public int[] GetBankBrancheIds(int bankId, TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity, TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
        {
            return GetBankBrancheIds(bankId, true, withThePurposeOfFetchingRelatedEntity, toDoThisWithTheFetchedIntances);
        }

        public int[] GetBankBrancheIds(int bankId, bool onlyInRootDirection, TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity, TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
        {
            return GetBankBrancheIds(bankId, onlyInRootDirection, !onlyInRootDirection, withThePurposeOfFetchingRelatedEntity, toDoThisWithTheFetchedIntances);
        }

        public int[] GetBankBrancheIds(int bankId, bool includeParentBanks, bool includeChildBanks, TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity, TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
        {
            List<int> branchIds = new List<int>();
            var hierarchy = BankFactory.Instance.FetchAllRelations();

            if (PermissionService.TryUserIsPermittedTo(toDoThisWithTheFetchedIntances, withThePurposeOfFetchingRelatedEntity, bankId))
            {
                branchIds.Add(bankId);
            }

            if (includeParentBanks)
            {
                var bankRelationEntity = hierarchy[bankId];
                while (bankRelationEntity.HasValue)
                {
                    var currentBankId = bankRelationEntity.Value;
                    bankRelationEntity = hierarchy[currentBankId];
                    if (PermissionService.TryUserIsPermittedTo(toDoThisWithTheFetchedIntances, withThePurposeOfFetchingRelatedEntity, currentBankId))
                    {
                        branchIds.Add(currentBankId);
                    }
                }
            }

            if (includeChildBanks)
            {
                AddChildBanks(bankId, hierarchy, ref branchIds);
            }

            return branchIds.ToArray();
        }

        public int[] GetBankBrancheIds(
    BankEntity bank,
    bool onlyInRootDirection,
    TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity,
    TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
        {
            return GetBankBrancheIds(bank.Id, onlyInRootDirection, withThePurposeOfFetchingRelatedEntity, toDoThisWithTheFetchedIntances);
        }

        public int[] GetBankBrancheIds(BankEntity anchorBank, bool onlyInRootDirection)
        {
            return GetBankBrancheIds(anchorBank.Id, onlyInRootDirection);
        }

        public int[] GetBankBrancheIds(int anchorBankId, bool onlyInRootDirection)
        {
            List<int> bankIdList = new List<int>();

            var hierarchy = BankFactory.Instance.FetchAllRelations();

            bankIdList.Add(anchorBankId);

            var bankRelationEntity = hierarchy[anchorBankId];
            while (bankRelationEntity.HasValue)
            {
                var currentBankId = bankRelationEntity.Value;
                bankRelationEntity = hierarchy[currentBankId];
                bankIdList.Add(currentBankId);
            }

            if (!onlyInRootDirection)
            {
                AddChildBanks(anchorBankId, hierarchy, ref bankIdList);
            }
            return bankIdList.ToArray();
        }

        private void AddChildBanks(int rootBankId, Dictionary<int, int?> bankRelations, ref List<int> collectedChildBanks)
        {
            var cb = from c in bankRelations where c.Value.HasValue && c.Value.Value == rootBankId select c.Key;
            foreach (int cbId in cb)
            {
                collectedChildBanks.Add(cbId);
                AddChildBanks(cbId, bankRelations, ref collectedChildBanks);
            }
        }

    }
}
