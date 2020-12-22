using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfTestResourceDtoService : WcfResourceDtoServiceDecorator<AssessmentTestResourceDto>, ITestResourceDtoRepository
    {

        public WcfTestResourceDtoService(ITestResourceDtoRepository decoree)
            : base(decoree)
        { }


        public IEnumerable<AssessmentTestResourceDto> GetTestsByCodes(IEnumerable<string> testCodeList, int bankId)
        {
            var list = new HashSet<AssessmentTestResourceDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(AssessmentTestResourceDto)), bankId);
            if (ids == null) return list;
            if (testCodeList == null) return list;
            var testCodes = testCodeList.ToList();
            Parallel.ForEach(ids.ToList(), bId =>
            {
                var b = bId;
                var listPerBank = ((ITestResourceDtoRepository)Decoree).GetTestsByCodes(testCodes, b); if (listPerBank == null) return;
                CacheLock.EnterWriteLock();
                try { list.UnionWith(listPerBank.Where(c => c != null && c.BankId == b)); }
                finally { CacheLock.ExitWriteLock(); }
            });
            return list;
        }


    }
}