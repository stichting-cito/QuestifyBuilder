using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.InvalidateCache.Helper;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Security;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfBankDtoService : BaseBankDtoServiceDecorator
    {
        public WcfBankDtoService(IBankDtoRepository decoree)
    : base(decoree)
        {
        }



        public override void EntityChanged(int key)
        {
            using (new StopUsingBankCache())
            {
                BankFactory.Instance.FetchAllRelations();
            }

            using (new StopUsingPermissionAndSecurityCache())
            {
                All();
            }

            base.EntityChanged(key);
        }

        public override void EntitiesChanged(IEnumerable<int> keys)
        {
            using (new StopUsingBankCache())
            {
                BankFactory.Instance.FetchAllRelations();
            }
            using (new StopUsingPermissionAndSecurityCache())
            {
                All();
            }
            base.EntitiesChanged(keys);
        }

        public override BankDto Get(int id)
        {
            return PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.DALRead,
                TestBuilderPermissionTarget.BankEntity, id) ? base.Get(id) : null;
        }

        public override IEnumerable<BankDto> All()
        {
            return FilterBanksByPermissions(base.All().ToList());
        }



        private IEnumerable<BankDto> FilterBanksByPermissions(IList<BankDto> banks)
        {
            IList<BankDto> returnValue = new List<BankDto>();
            foreach (var rootBank in banks.Where(b => b != null && b.ParentBankId == null))
            {
                var clonedRootBank = DeepClone(rootBank);
                var userHasExplicitViewPermissionForRootBank = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, clonedRootBank.Id);
                var userHasAPermissionForRootBank = userHasExplicitViewPermissionForRootBank;
                if (!userHasAPermissionForRootBank)
                {
                    userHasAPermissionForRootBank = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, clonedRootBank.Id);
                }

                var flatBankStructure = clonedRootBank.Flattened();
                AddChildren(clonedRootBank, flatBankStructure.ToList(), userHasExplicitViewPermissionForRootBank);
                if (userHasAPermissionForRootBank || clonedRootBank.BankCollection.Count > 0)
                {
                    returnValue.Add(clonedRootBank);
                }
            }
            return returnValue;
        }

        private static BankDto DeepClone(BankDto bankDto)
        {
            BankDto result;
            using (var stream = new MemoryStream())
            {
                var dcs = new NetDataContractSerializer();
                dcs.WriteObject(stream, bankDto);
                stream.Position = 0;
                result = (BankDto)dcs.ReadObject(stream);
            }

            return result;
        }
        private static void AddChildren(BankDto rootBank, IList<BankDto> banks, bool userHasExplicitViewPermissonForAncestorBank)
        {
            var childBanks = banks.Where(c => c.ParentBankId.HasValue && c.ParentBankId.Value == rootBank.Id).ToList();
            if (childBanks.Count <= 0)
            {
                return;
            }
            var explicitViewPermissonForAncestorBank = userHasExplicitViewPermissonForAncestorBank;
            if (!userHasExplicitViewPermissonForAncestorBank)
            {
                explicitViewPermissonForAncestorBank = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, rootBank.Id);
            }

            foreach (var child in childBanks)
            {
                var userHasAPermissionForBank = explicitViewPermissonForAncestorBank;
                if (!userHasAPermissionForBank)
                {
                    userHasAPermissionForBank = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, child.Id);
                }

                AddChildren(child, banks, explicitViewPermissonForAncestorBank);
                if (!(userHasAPermissionForBank || child.BankCollection?.Count > 0))
                {
                    rootBank.BankCollection.Remove(child);
                }
            }
        }


    }
}