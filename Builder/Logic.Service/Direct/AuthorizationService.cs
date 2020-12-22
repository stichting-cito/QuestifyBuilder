using System;
using Enums;
using Questify.Builder.Logic.Service.Exceptions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Properties;
using Questify.Builder.Model.ContentModel;
using Questify.Builder.Model.ContentModel.DatabaseSpecific;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Security;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.Logic.Service.Direct
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ExcludeIncludeFieldsHelper _excludeIncludeFieldsHelper;

        public AuthorizationService()
        {
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }


        public EntityCollection GetUsersWithPermissionsInBank(int bankId)
        {
            DataAccessAdapter adapter = null;
            var resultCollection = new EntityCollection(new UserBankRoleEntityFactory());

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(UserBankRoleFields.BankId == bankId);

            var path = new PrefetchPath2((int)EntityType.UserBankRoleEntity);
            path.Add(UserBankRoleEntity.PrefetchPathUser, _excludeIncludeFieldsHelper.GetUserExcludedFieldList());

            try
            {
                adapter = new DataAccessAdapter();
                adapter.FetchEntityCollection(resultCollection, filter, path);
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_FETCH_USERPERM_BANK, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return resultCollection;
        }

        public EntityCollection GetBankRoleCollection()
        {
            DataAccessAdapter adapter = null;
            var resultCollection = new EntityCollection(new RoleEntityFactory());

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(RoleFields.IsApplicationRole == false);

            try
            {
                adapter = new DataAccessAdapter();
                adapter.FetchEntityCollection(resultCollection, filter);
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_FETCH_BANKROLES, ex);
            }
            finally
            {
                adapter?.Dispose();
            }
            return resultCollection;
        }


        public EntityCollection GetRolePermissionCollection()
        {
            DataAccessAdapter adapter = null;
            var resultCollection = new EntityCollection(new RoleEntityFactory());


            IPrefetchPath2 prefetchPath = new PrefetchPath2((int)EntityType.RoleEntity);
            var addedPrefetchPathElement = prefetchPath.Add(RoleEntity.PrefetchPathRolePermissionCollection);
            addedPrefetchPathElement.SubPath.Add(RolePermissionEntity.PrefetchPathPermission);
            addedPrefetchPathElement.SubPath.Add(RolePermissionEntity.PrefetchPathPermissionTarget);

            try
            {
                adapter = new DataAccessAdapter();
                adapter.FetchEntityCollection(resultCollection, null, prefetchPath);
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_FETCH_BANKROLES, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return resultCollection;
        }


        public EntityCollection GetApplicationRoleCollection()
        {
            DataAccessAdapter adapter = null;
            var resultCollection = new EntityCollection(new RoleEntityFactory());

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(RoleFields.IsApplicationRole == true);

            try
            {
                adapter = new DataAccessAdapter();
                adapter.FetchEntityCollection(resultCollection, filter);
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_FETCH_APPROLES, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return resultCollection;
        }

        public string DeleteUserApplicationRoles(EntityCollection removedEntities)
        {
            return DeleteEntities(removedEntities);
        }

        public string DeleteUserBankRoles(EntityCollection removedEntities)
        {
            return DeleteEntities(removedEntities);
        }

        public string UpdateUserBankRoles(EntityCollection userBankRolesEntities)
        {
            return UpdateEntities(userBankRolesEntities);
        }

        public EntityCollection GetUsers()
        {
            DataAccessAdapter adapter = null;
            var resultCollection = new EntityCollection(new UserEntityFactory());

            IRelationPredicateBucket filter = new RelationPredicateBucket();

            IPrefetchPath2 prefetchPath = new PrefetchPath2((int)EntityType.UserEntity);
            prefetchPath.Add(UserEntity.PrefetchPathCreatedByUser,
                _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            prefetchPath.Add(UserEntity.PrefetchPathModifiedByUser,
                _excludeIncludeFieldsHelper.GetUserIncludedFieldList());

            try
            {
                adapter = new DataAccessAdapter();
                adapter.FetchEntityCollection(resultCollection, filter, 0, null, prefetchPath,
                    _excludeIncludeFieldsHelper.GetUserExcludedFieldList());
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_FETCH_USERS, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return resultCollection;
        }

        public UserEntity GetUserWithRoles(UserEntity user, bool withUserSettings)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var fetchedUser = new UserEntity(user.Id);
            DataAccessAdapter adapter = null;

            IPrefetchPath2 prefetchPath = new PrefetchPath2((int)EntityType.UserEntity);
            prefetchPath.Add(UserEntity.PrefetchPathCreatedByUser,
                _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            prefetchPath.Add(UserEntity.PrefetchPathModifiedByUser,
                _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            prefetchPath.Add(UserEntity.PrefetchPathUserApplicationRoleCollection);
            prefetchPath.Add(UserEntity.PrefetchPathUserBankRoleCollection).SubPath
                .Add(UserBankRoleEntity.PrefetchPathRole);

            try
            {
                adapter = new DataAccessAdapter();
                if (withUserSettings)
                {
                    adapter.FetchEntity(fetchedUser, prefetchPath);
                }
                else
                {
                    adapter.FetchEntity(fetchedUser, prefetchPath, null, _excludeIncludeFieldsHelper.GetUserExcludedFieldList());

                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_FETCH_USERS, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return fetchedUser;
        }

        public UserEntity GetUserWithRoles(UserEntity user)
        {
            return GetUserWithRoles(user, false);
        }

        public bool UserIsApplicationAdministrator(UserEntity user)
        {
            var usr = GetUserWithRoles(user);
            return usr.UserApplicationRoleCollection.GetFirstMatch(UserApplicationRoleFields.ApplicationRoleId == 2) !=
                   null;
        }

        public string UpdateUsers(EntityCollection users)
        {
            return UpdateEntities(users);
        }

        public string UpdateUser(UserEntity user)
        {
            if (user.AuthenticationType != null)
            {
                if (user.AuthenticationType == AuthenticationType.Default.ToString())
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        if (!user.Password.StartsWith(PasswordHashing.Prefix))
                        {
                            user.Password = PasswordHashing.CreateHash(user.Password);
                        }
                    }
                }
            }
            return UpdateEntity(user);
        }


        public string DeleteUser(UserEntity user)
        {
            return DeleteEntity(user);
        }

        public DateTime? GetMaintenanceWindow()
        {
            var resultSet = RetrievalProcedures.GetMaintenanceWindow();
            if (resultSet.Rows.Count > 0)
            {
                return (DateTime)resultSet.Rows[0][0];
            }

            return null;
        }

        public bool SetMaintenanceWindow(DateTime? plannedTimestamp)
        {
            var returnValue = false;
            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    DateTime planned = DateTime.MinValue;
                    if (plannedTimestamp.HasValue)
                    {
                        planned = plannedTimestamp.Value;
                    }
                    ActionProcedures.SetMaintenanceWindow(planned, adapter);
                    returnValue = true;
                }
                catch (Exception ex)
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }



        private static string DeleteEntities(EntityCollection removedEntities)
        {
            DataAccessAdapter adapter = null;
            var result = string.Empty;

            try
            {
                adapter = new DataAccessAdapter();

                adapter.DeleteEntityCollection(removedEntities);
            }
            catch (ORMEntityValidationException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_DELETE_ROLES, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return result;
        }

        private static string DeleteEntity(IEntity2 removedEntity)
        {
            DataAccessAdapter adapter = null;
            var result = string.Empty;

            try
            {
                adapter = new DataAccessAdapter();

                adapter.DeleteEntity(removedEntity);
            }
            catch (ORMEntityValidationException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                result = "Entity cannot be deleted.";
            }
            finally
            {
                adapter?.Dispose();
            }

            return result;
        }

        private static string UpdateEntities(EntityCollection updatedEntities)
        {
            DataAccessAdapter adapter = null;
            var result = string.Empty;

            try
            {
                adapter = new DataAccessAdapter();

                adapter.SaveEntityCollection(updatedEntities, true, true);
            }
            catch (ORMEntityValidationException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_UPDATE_ROLES, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return result;
        }

        private static string UpdateEntity(IEntity2 updatedEntitie)
        {
            DataAccessAdapter adapter = null;
            var result = string.Empty;

            try
            {
                adapter = new DataAccessAdapter();

                adapter.SaveEntity(updatedEntitie, true);
            }
            catch (ORMEntityValidationException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                throw new ServiceException(Resources.ERROR_UPDATE_ROLES, ex);
            }
            finally
            {
                adapter?.Dispose();
            }

            return result;
        }

    }
}