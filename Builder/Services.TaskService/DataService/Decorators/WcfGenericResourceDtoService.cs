using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfGenericResourceDtoService : WcfResourceDtoServiceDecorator<GenericResourceDto>, IGenericResourceDtoRepository
    {
        public WcfGenericResourceDtoService(IGenericResourceDtoRepository decoree)
            : base(decoree)
        {
        }

        public IEnumerable<GenericResourceDto> GetListWithFilter(int bankId, string filter, string filePrefix, bool templatesOnly)
        {
            var list = new HashSet<GenericResourceDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(GenericResourceDto)), bankId);
            if (ids == null)
            {
                return list;
            }

            Parallel.ForEach(ids.ToList(), bId =>
            {
                var b = bId;
                var listPerBank = ((IGenericResourceDtoRepository)Decoree).GetListWithFilter(b, filter, filePrefix, templatesOnly);
                if (listPerBank == null)
                {
                    return;
                }
                CacheLock.EnterWriteLock();
                try
                {
                    list.UnionWith(listPerBank.Where(c => c != null && c.BankId == b));
                }
                finally
                {
                    CacheLock.ExitWriteLock();
                }
            });
            return list;
        }
    }
}