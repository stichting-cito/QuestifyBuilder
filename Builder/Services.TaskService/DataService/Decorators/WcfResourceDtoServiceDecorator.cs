using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Security;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfResourceDtoServiceDecorator<TResource> : BaseResourceDtoServiceDecorator<TResource> where TResource : ResourceDto
    {

        public WcfResourceDtoServiceDecorator(IResourceDtoRepository<TResource> decoree)
            : base(decoree)
        {
        }


        public override IEnumerable<TResource> GetResourcesForBank(int id)
        {
            var list = new HashSet<TResource>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(TResource)), id);
            if (ids == null) return list;
            ids.ToList().ForEach(bankId =>
            {
                var bId = bankId;
                CacheLock.EnterWriteLock();
                try
                {
                    var listPerBank = base.GetResourcesForBank(bId);
                    if (listPerBank != null)
                    {
                        list.UnionWith(listPerBank.Where(c => c != null && c.BankId == bId));
                    }
                }
                finally { CacheLock.ExitWriteLock(); }
            });
            return list;
        }


        protected IEnumerable<TResource> FilterGrantedBankPermissions(IList<TResource> list, int bankId)
        {
            var fetchTarget = TestBuilderPermissionTarget.Any;
            if (list.Any())
            {
                fetchTarget = Helper.ContentModelObjectToPermissionTarget(list[0]);
            }
            return list.Where(b => GetGrantedBankPermissions(fetchTarget, bankId, false).Contains(b.BankId));
        }

        protected IEnumerable<int> GetGrantedBankPermissions(TestBuilderPermissionTarget fetchTarget, int bankId, bool includeSubBanks = false)
        {
            var bankBranchHelper = new BankBranchIdHelper();
            return bankBranchHelper.GetBankBrancheIds(bankId, !includeSubBanks, fetchTarget, TestBuilderPermissionAccess.DALRead);
        }

    }
}