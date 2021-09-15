
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

		/// <summary>
		/// Initializes a new instance of the <see cref="BankBranchIdHelper"/> class.
		/// </summary>
		/// <param name="permissionServiceOverride">The permission service override.</param>
		public BankBranchIdHelper(IPermissionService permissionServiceOverride)
		{
			PermissionService = permissionServiceOverride;
		}
		
		/// <summary>
		/// Gets the bank branche ids.
		/// </summary>
		/// <param name="bankId">The bank identifier.</param>
		/// <param name="withThePurposeOfFetchingRelatedEntity">The with the purpose of fetching related entity.</param>
		/// <param name="toDoThisWithTheFetchedIntances">To do this with the fetched intances.</param>
		/// <returns></returns>
		public int[] GetBankBrancheIds(int bankId, TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity, TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
		{
			return GetBankBrancheIds(bankId, true, withThePurposeOfFetchingRelatedEntity, toDoThisWithTheFetchedIntances);
		}

		public int[] GetBankBrancheIds(int bankId, bool onlyInRootDirection, TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity, TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
		{
			return GetBankBrancheIds(bankId, onlyInRootDirection, !onlyInRootDirection, withThePurposeOfFetchingRelatedEntity, toDoThisWithTheFetchedIntances);
		}

		/// <summary>
		/// Gets the bank branche ids.
		/// </summary>
		/// <param name="bankId"></param>
		/// <param name="includeParentBanks"></param>
		/// <param name="includeChildBanks"></param>
		/// <param name="withThePurposeOfFetchingRelatedEntity"></param>
		/// <param name="toDoThisWithTheFetchedIntances"></param>
		/// <returns></returns>
		public int[] GetBankBrancheIds(int bankId, bool includeParentBanks, bool includeChildBanks, TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity, TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
		{
			List<int> branchIds = new List<int>();
			var hierarchy = BankFactory.Instance.FetchAllRelations();
			// This one is cached, so we dont have to fetch banks

			// Add leaf bank to list if the user is permitted to access the bank related entity instances
			if (PermissionService.TryUserIsPermittedTo(toDoThisWithTheFetchedIntances, withThePurposeOfFetchingRelatedEntity, bankId))
            {
				branchIds.Add(bankId);
			}

			if (includeParentBanks)
            {
				var bankRelationEntity = hierarchy[bankId];
				// Walk up the tree and add bankentities to list
				while (bankRelationEntity.HasValue)
                {
					var currentBankId = bankRelationEntity.Value;
					bankRelationEntity = hierarchy[currentBankId];
					// If the user is permitted to access the bank related entity instances then we add this bank to the list
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

			// Return all ids for banks on same branche
			return branchIds.ToArray();
		}

	    /// <summary>
	    /// Gets the array of ids for bankentity by walking up the tree until we reach the root.
	    /// </summary>
	    /// <param name="bank">The bank.</param>
	    /// <param name="onlyInRootDirection"></param>
	    /// <param name="withThePurposeOfFetchingRelatedEntity"></param>
	    /// <param name="toDoThisWithTheFetchedIntances"></param>
	    /// <returns>array of bank ids</returns>
	    public int[] GetBankBrancheIds(
            BankEntity bank, 
            bool onlyInRootDirection, 
            TestBuilderPermissionTarget withThePurposeOfFetchingRelatedEntity, 
            TestBuilderPermissionAccess toDoThisWithTheFetchedIntances)
		{
			return GetBankBrancheIds(bank.Id, onlyInRootDirection, withThePurposeOfFetchingRelatedEntity, toDoThisWithTheFetchedIntances);
		}

		/// <summary>
		/// Gets the bank branche ids of the branche of which anchorBank is a member.
		/// </summary>
		/// <param name="anchorBank">The anchor bank.</param>
		/// <param name="onlyInRootDirection">if set to <c>true</c> only banks in the root direction are included.</param>
		/// <returns></returns>
		public int[] GetBankBrancheIds(BankEntity anchorBank, bool onlyInRootDirection)
		{
			return GetBankBrancheIds(anchorBank.Id, onlyInRootDirection);
		}

		/// <summary>
		/// Gets the bank branche ids.
		/// </summary>
		/// <param name="anchorBankId">The anchor bank identifier.</param>
		/// <param name="onlyInRootDirection">if set to <c>true</c> [only in root direction].</param>
		/// <returns></returns>
		public int[] GetBankBrancheIds(int anchorBankId, bool onlyInRootDirection)
		{
			List<int> bankIdList = new List<int>();

			var hierarchy = BankFactory.Instance.FetchAllRelations();
			// This one is cached, so we dont have to fetch banks

			// Always add leaf bank to list.
			bankIdList.Add(anchorBankId);

			var bankRelationEntity = hierarchy[anchorBankId];
			// Walk up the tree and add bankentities to list
			while (bankRelationEntity.HasValue)
            {
				var currentBankId = bankRelationEntity.Value;
				bankRelationEntity = hierarchy[currentBankId];
				// Add this bank to the list
				bankIdList.Add(currentBankId);
			}

			if (!onlyInRootDirection)
            {
				AddChildBanks(anchorBankId, hierarchy, ref bankIdList);
			}
			// return all ids for banks on same branche
			return bankIdList.ToArray();
		}

		/// <summary>
		/// Adds the child banks.
		/// </summary>
		/// <param name="rootBankId">The root bank unique identifier.</param>
		/// <param name="bankRelations">The bank relations.</param>
		/// <param name="collectedChildBanks">The collected child banks.</param>
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
