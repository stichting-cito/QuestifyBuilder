using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Direct
{
    public class PermissionService : IPermissionService
    {
        public bool TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess access,
            TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, int bankId,
            int entityInstanceId)
        {
            var TBPermission = new TestBuilderPermission(permissionTarget, targettedNamedTask, access);

            var banks = BankFactory.Instance.FetchAllRelations();
            var grantedPermissions = SecurityFactory.Instance.FetchGrantedPermissions(banks.Keys.ToArray());
            var alternativeBankIdsWithAncestors = GetBankRelatives(bankId, true, banks);
            var alternativeBankIdsWithoutAncestors = GetBankRelatives(bankId, false, banks);

            return TBPermission.TryDemand(bankId, alternativeBankIdsWithAncestors,
                alternativeBankIdsWithoutAncestors, grantedPermissions);
        }

        public bool TryUserIsPermittedTo(TestBuilderPermissionAccess access,
    TestBuilderPermissionTarget permissionTarget, int bankId)
        {
            return TryUserIsPermittedToNamedTask(access, permissionTarget, TestBuilderPermissionNamedTask.None, bankId,
                0);
        }


        public bool UserIsPermittedTo(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget,
    int bankId)
        {
            return TryUserIsPermittedToNamedTask(access, permissionTarget, TestBuilderPermissionNamedTask.None, bankId,
                0);
        }

        public void UserIsPermittedToNamedTask(TestBuilderPermissionAccess access,
    TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, int bankId,
    int entityInstanceId)
        {
            var TBPermission = new TestBuilderPermission(permissionTarget, targettedNamedTask, access);

            var banks = BankFactory.Instance.FetchAllRelations();
            var grantedPermissions = SecurityFactory.Instance.FetchGrantedPermissions(banks.Keys.ToArray());
            var alternativeBankIdsWithAncestors = GetBankRelatives(bankId, true, banks);
            var alternativeBankIdsWithoutAncestors = GetBankRelatives(bankId, false, banks);
            TBPermission.Demand(bankId, alternativeBankIdsWithAncestors,
                alternativeBankIdsWithoutAncestors, grantedPermissions);
        }


        private int[] GetBankRelatives(int bankId, bool getAncestors, Dictionary<int, int?> bankRelations)
        {
            var resultingBanks = new List<int>();
            if (getAncestors) resultingBanks = GetAncestorBanks(bankId, bankRelations);
            else AddChildBanks(bankId, bankRelations, ref resultingBanks);

            if (resultingBanks.Count > 0) return resultingBanks.ToArray();

            return null;
        }

        private List<int> GetAncestorBanks(int baseBankId, Dictionary<int, int?> bankRelations)
        {
            var collectedAncestorBanks = new List<int>();
            if (bankRelations.ContainsKey(baseBankId))
            {
                var bankId = baseBankId;
                while (bankRelations.ContainsKey(bankId) && bankRelations[bankId].HasValue)
                {
                    bankId = bankRelations[bankId].Value;
                    collectedAncestorBanks.Add(bankId);
                }
            }
            return collectedAncestorBanks;
        }

        private void AddChildBanks(int rootBankId, Dictionary<int, int?> bankRelations,
    ref List<int> collectedChildBanks)
        {
            var cb = from c in bankRelations where c.Value.HasValue && c.Value.Value == rootBankId select c.Key;
            foreach (var cbId in cb)
            {
                collectedChildBanks.Add(cbId);
                AddChildBanks(cbId, bankRelations, ref collectedChildBanks);
            }
        }

    }
}