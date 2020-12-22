using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Security;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfCustomBankPropertyDtoService : BaseCustomPropertyDtoServiceDecorator
    {
        internal WcfCustomBankPropertyDtoService(ICustomBankPropertyDtoRepository decoree) : base(decoree)
        { }

        public override IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranch(int bankId)
        {
            var list = new HashSet<CustomBankPropertyDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(ItemResourceDto)), bankId);
            if (ids == null) return list;
            Parallel.ForEach(ids.ToList(), bId =>
            {
                var b = bId;
                var listPerBank = base.GetCustomBankPropertiesForBranch(b);
                if (listPerBank == null) return;
                CacheLock.EnterWriteLock();
                try { list.UnionWith(listPerBank.Where(c => c != null && c.BankId == b)); }
                finally { CacheLock.ExitWriteLock(); }
            });
            return list;
        }

        public override IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranchWithFilter(int bankId, string type)
        {
            var list = new HashSet<CustomBankPropertyDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(ItemResourceDto)), bankId);
            if (ids == null) return list;
            Parallel.ForEach(ids.ToList(), bId =>
            {
                var b = bId;
                var listPerBank = base.GetCustomBankPropertiesForBranchWithFilter(b, type);
                if (listPerBank == null) return;
                CacheLock.EnterWriteLock();
                try { list.UnionWith(listPerBank.Where(c => c != null && c.BankId == b)); }
                finally { CacheLock.ExitWriteLock(); }
            });
            return list;
        }

        private IEnumerable<int> GetGrantedBankPermissions(TestBuilderPermissionTarget fetchTarget, int bankId)
        {
            var bankBranchHelper = new BankBranchIdHelper();
            return bankBranchHelper.GetBankBrancheIds(bankId, fetchTarget, TestBuilderPermissionAccess.DALRead);
        }
    }
}