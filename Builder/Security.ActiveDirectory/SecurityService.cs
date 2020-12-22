using Questify.Builder.Model.ContentModel.DatabaseSpecific;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel;
using SD.LLBLGen.Pro.ORMSupportClasses;
using Questify.Builder.Logic.Service.Direct;
using System.Threading;
using System;
using System.Configuration;
using System.Collections.Generic;
using Enums;

namespace Questify.Builder.Security.ActiveDirectory
{
    public class SecurityService : ISecurityService
    {
        private UserEntity _user;
        private readonly ExcludeIncludeFieldsHelper _excludeIncludeFieldsHelper;
        private string _username;

        public SecurityService()
        {
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }

        public AuthenticationResult Authenticate(string username, string password, string type)
        {
            AuthenticationResult result = new AuthenticationResult();
            Signout();
            _username = username;

            UserEntity fetchedUser = null;

            bool foundUser = GetUser(ref fetchedUser);
            if (foundUser)
            {
                if (ValidateUser(fetchedUser, password))
                {
                    _user = new UserEntity
                    {
                        Fields = fetchedUser.Fields.Clone()
                    };
                    result.QuestifyBuilderIdentity = new TestBuilderIdentity(_user.Id, _user.UserName, type);
                }
                else
                {
                    result.AuthenticationActionState = (int)AuthenticationActionState.Failed;
                    result.QuestifyBuilderIdentity = new TestBuilderIdentity(type);
                    ClearUser();
                }

            }
            else
            {
                result.AuthenticationActionState = (int)AuthenticationActionState.Failed;
                result.QuestifyBuilderIdentity = new TestBuilderIdentity(type);
                ClearUser();
            }

            return result;
        }

        public bool IsAuthenticated()
        {
            if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal is TestBuilderPrincipal)
            {
                return Thread.CurrentPrincipal.Identity.IsAuthenticated;
            }

            return false;
        }

        public void Signout()
        {
            Thread.CurrentPrincipal = null;
            ClearUser();
        }

        public System.Security.Principal.IPrincipal GetAuthPrincipal(TestBuilderIdentity identity)
        {
            if ((identity == null))
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (identity.IsAuthenticated)
            {
                return new TestBuilderPrincipal(identity);
            }

            return null;
        }


        public bool ValidateUser(UserEntity user, string password)
        {
            if (user == null || !user.Active)
            {
                return false;
            }

            if ((user.AuthenticationType == AuthenticationType.ActiveDirectory.ToString()))
            {
                string domain = ConfigurationManager.AppSettings["AuthenticationDomain"];
                return ActiveDirectoryHelper.ValidateUser(domain, user.UserName, password);
            }

            return PasswordHashing.Verify(password, user.Password);
        }

        public SerializableDictionaryIntegerPermission FetchGrantedPermissions(int[] bankIds)
        {
            var ret = new SerializableDictionaryIntegerPermission();
            TestBuilderPrincipal currentPrincipal = ((TestBuilderPrincipal)(Thread.CurrentPrincipal));
            TestBuilderIdentity userIdentity = ((TestBuilderIdentity)(currentPrincipal.Identity));
            DataAccessAdapter adapter = new DataAccessAdapter();
            EntityCollection roles;
            UserEntity currentUser = new UserEntity(userIdentity.UserId);
            currentUser.GetRelationInfoRoleCollectionViaUserBankRole();
            roles = ReadRoles(adapter, userIdentity.UserId, true);
            TestBuilderPermission applicationPermission = new TestBuilderPermission();
            var applicationPermissionEntries = GetRolePermissionEntries(roles);
            applicationPermission.PermissionEntries.AddRange(applicationPermissionEntries);

            ret.Add(0, applicationPermission);
            var rolesForBank = GetRolesForBanks(adapter, userIdentity.UserId);

            foreach (UserBankRoleEntity bankRol in GetBankRoles(adapter, userIdentity.UserId))
            {
                var bankPermissionEntries = GetRolePermissionEntries(rolesForBank[bankRol.BankRoleId]);
                if (!ret.ContainsKey(bankRol.BankId))
                {
                    TestBuilderPermission grantedPermission = new TestBuilderPermission();
                    grantedPermission.PermissionEntries.AddRange(bankPermissionEntries);
                    ret.Add(bankRol.BankId, grantedPermission);
                }
                else
                {
                    TestBuilderPermission grantedPermission = ret[bankRol.BankId];
                    grantedPermission.PermissionEntries.AddRange(bankPermissionEntries);
                }
            }

            foreach (int bankId in bankIds)
            {
                TestBuilderPermission grantedPermission;
                if (ret.ContainsKey(bankId))
                {
                    grantedPermission = ret[bankId];
                }
                else
                {
                    grantedPermission = new TestBuilderPermission();
                    ret.Add(bankId, grantedPermission);
                }

                grantedPermission.PermissionEntries.AddRange(applicationPermissionEntries);
            }

            return ret;
        }

        public bool IsBankAssignedToUserThroughBankRole(int bankId)
        {
            TestBuilderPrincipal currentPrincipal = (TestBuilderPrincipal)(Thread.CurrentPrincipal);
            TestBuilderIdentity userIdentity = (TestBuilderIdentity)(currentPrincipal.Identity);
            UserEntity currentUser = new UserEntity(userIdentity.UserId);
            IRelationPredicateBucket relationPredicate = currentUser.GetRelationInfoRoleCollectionViaUserBankRole();
            PredicateExpression bankFilter = new PredicateExpression();
            bankFilter.Add(new FieldCompareValuePredicate(UserBankRoleFields.BankId, null, ComparisonOperator.Equal, bankId, "UserBankRole_"));
            relationPredicate.PredicateExpression.Add(bankFilter);
            DataAccessAdapter adapter = new DataAccessAdapter();
            EntityCollection roles = new EntityCollection(new RoleEntityFactory());
            adapter.FetchEntityCollection(roles, relationPredicate);
            return (roles.Count > 0);
        }

        private bool GetUser(ref UserEntity user)
        {
            if (string.IsNullOrEmpty(_username))
            {
                throw new SecurityException("Username can not be empty.");
            }

            EntityCollection users = new EntityCollection(new UserEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(UserFields.UserName == _username);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(users, _excludeIncludeFieldsHelper.GetUserExcludedFieldList(), filter);
            }

            if (users.Count != 1)
            {
                return false;
            }

            user = (UserEntity)users[0];
            return true;
        }

        private List<TestBuilderPermissionEntry> GetRolePermissionEntries(EntityCollection roles)
        {
            List<TestBuilderPermissionEntry> ret = new List<TestBuilderPermissionEntry>();
            foreach (RoleEntity role in roles)
            {
                ret.AddRange(GetRolePermissionEntries(role));
            }

            return ret;
        }
        private List<TestBuilderPermissionEntry> GetRolePermissionEntries(RoleEntity role)
        {
            List<TestBuilderPermissionEntry> ret = new List<TestBuilderPermissionEntry>();
            foreach (RolePermissionEntity rolePermission in role.RolePermissionCollection)
            {
                var permissionTarget = (TestBuilderPermissionTarget)Enum.Parse(typeof(TestBuilderPermissionTarget), rolePermission.PermissionTarget.Name);
                var targettedNamedTask = TestBuilderPermissionNamedTask.None;
                var permissionAccess = (TestBuilderPermissionAccess)Enum.Parse(typeof(TestBuilderPermissionAccess), rolePermission.Permission.Name);

                if (!string.IsNullOrEmpty(rolePermission.PermissionTarget.TargettedNamedTask))
                {
                    targettedNamedTask = (TestBuilderPermissionNamedTask)Enum.Parse(typeof(TestBuilderPermissionNamedTask), rolePermission.PermissionTarget.TargettedNamedTask);
                }

                ret.Add(new TestBuilderPermissionEntry(permissionTarget, targettedNamedTask, permissionAccess, rolePermission.Permission.WhenOwnerCondition));
            }

            return ret;
        }

        private EntityCollection ReadRoles(DataAccessAdapter adapter, int userId, bool applicationRoles)
        {
            EntityCollection roles = new EntityCollection(new RoleEntityFactory());
            PrefetchPath2 roleEntityPrefetchPath = new PrefetchPath2(((int)(EntityType.RoleEntity)));
            IRelationPredicateBucket relationPredicate = new RelationPredicateBucket();
            UserEntity currentUser = new UserEntity(userId);

            if (applicationRoles)
            {
                relationPredicate = currentUser.GetRelationInfoRoleCollectionViaUserApplicationRole();
            }

            IPrefetchPathElement2 addedPrefetchPathElement = roleEntityPrefetchPath.Add(RoleEntity.PrefetchPathRolePermissionCollection);
            addedPrefetchPathElement.SubPath.Add(RolePermissionEntity.PrefetchPathPermission);
            addedPrefetchPathElement.SubPath.Add(RolePermissionEntity.PrefetchPathPermissionTarget);
            adapter.FetchEntityCollection(roles, relationPredicate, roleEntityPrefetchPath);
            return roles;
        }


        private Dictionary<int, RoleEntity> GetRolesForBanks(DataAccessAdapter adapter, int userId)
        {
            var roles = ReadRoles(adapter, userId, false);
            var ret = new Dictionary<int, RoleEntity>();
            foreach (RoleEntity role in roles)
            {
                ret.Add(role.Id, role);
            }

            return ret;
        }

        private EntityCollection GetBankRoles(DataAccessAdapter adapter, int userId)
        {
            EntityCollection bankRoles = new EntityCollection(new UserBankRoleEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(UserBankRoleFields.UserId == userId);
            adapter.FetchEntityCollection(bankRoles, filter);
            return bankRoles;
        }

        public UserEntity User => _user;

        private void ClearUser()
        {
            _user = null;
        }
    }
}