using System;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.Logic.Service.Interfaces
{

    public interface IAuthorizationService
    {


        EntityCollection GetUsersWithPermissionsInBank(int bankId);

        EntityCollection GetBankRoleCollection();

        EntityCollection GetRolePermissionCollection();

        EntityCollection GetApplicationRoleCollection();

        string DeleteUserBankRoles(EntityCollection removedEntities);

        string DeleteUserApplicationRoles(EntityCollection removedEntities);

        string UpdateUserBankRoles(EntityCollection userBankRolesEntities);

        EntityCollection GetUsers();

        UserEntity GetUserWithRoles(UserEntity user);

        UserEntity GetUserWithRoles(UserEntity user, bool withUserSettings);

        bool UserIsApplicationAdministrator(UserEntity user);

        string UpdateUsers(EntityCollection users);

        string UpdateUser(UserEntity user);

        string DeleteUser(UserEntity user);

        DateTime? GetMaintenanceWindow();

        bool SetMaintenanceWindow(DateTime? plannedTimestamp);
    }
}
