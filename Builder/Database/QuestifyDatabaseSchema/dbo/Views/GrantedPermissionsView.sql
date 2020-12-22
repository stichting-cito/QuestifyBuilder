CREATE VIEW [dbo].[GrantedPermissionsView]
AS
/* Select the permissions via the application roles*/ SELECT dbo.[User].id, dbo.[User].userName, '' AS 'Bank id', 'nvt' AS 'Bank', dbo.Role.id As 'Role id', dbo.Role.description AS 'Role', 
                      dbo.Role.isApplicationRole, dbo.PermissionTarget.name AS 'Target', dbo.PermissionTarget.targettedNamedTask, 
                      dbo.Permission.name AS 'Permitted Access', dbo.Permission.whenOwnerCondition
FROM         dbo.[User] INNER JOIN
                      dbo.UserApplicationRole ON dbo.[User].id = dbo.UserApplicationRole.userId INNER JOIN
                      dbo.Role ON dbo.UserApplicationRole.applicationRoleId = dbo.Role.id INNER JOIN
                      dbo.RolePermission ON dbo.Role.id = dbo.RolePermission.roleId INNER JOIN
                      dbo.PermissionTarget ON dbo.RolePermission.permissionTargetId = dbo.PermissionTarget.id INNER JOIN
                      dbo.Permission ON dbo.RolePermission.permissionId = dbo.Permission.id
UNION
/*Union the permissions assigned via bank roles*/ SELECT dbo.[User].id, dbo.[User].userName, dbo.Bank.Id AS 'Bank id', dbo.Bank.Name AS 'Bank', 
                      dbo.Role.id As 'Role id', dbo.Role.description AS 'Role', dbo.Role.isApplicationRole, dbo.PermissionTarget.name AS 'Target', dbo.PermissionTarget.targettedNamedTask, 
                      dbo.Permission.name AS 'Permitted Access', dbo.Permission.whenOwnerCondition
FROM         dbo.[User] INNER JOIN
                      dbo.UserBankRole ON dbo.[User].id = dbo.UserBankRole.userId INNER JOIN
                      dbo.Role ON dbo.UserBankRole.bankRoleId = dbo.Role.id INNER JOIN
                      dbo.RolePermission ON dbo.Role.id = dbo.RolePermission.roleId INNER JOIN
                      dbo.PermissionTarget ON dbo.RolePermission.permissionTargetId = dbo.PermissionTarget.id INNER JOIN
                      dbo.Permission ON dbo.RolePermission.permissionId = dbo.Permission.id INNER JOIN
                      dbo.Bank ON dbo.UserBankRole.bankId = dbo.Bank.Id