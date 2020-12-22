using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfDataSourceResourceDtoService : WcfResourceDtoServiceDecorator<DataSourceResourceDto>, IDataSourceResourceDtoRepository
    {
        public WcfDataSourceResourceDtoService(IDataSourceResourceDtoRepository decoree)
    : base(decoree)
        { }


        public IEnumerable<DataSourceResourceDto> GetListWithFilter(int bankId, bool? isTemplate, params string[] behaviours)
        {
            var list = new HashSet<DataSourceResourceDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(DataSourceResourceDto)), bankId);
            if (ids == null) return list;
            Parallel.ForEach(ids.ToList(), bId =>
            {
                var b = bId;
                var listPerBank = ((IDataSourceResourceDtoRepository)Decoree).GetListWithFilter(b, isTemplate, behaviours);
                if (listPerBank == null) return;
                CacheLock.EnterWriteLock();
                try { list.UnionWith(listPerBank.Where(c => c != null && c.BankId == b)); }
                finally { CacheLock.ExitWriteLock(); }
            });
            return list;
        }
    }
}