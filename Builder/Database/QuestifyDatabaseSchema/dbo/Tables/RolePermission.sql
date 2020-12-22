CREATE TABLE [dbo].[RolePermission] (
    [roleId]             INT      NOT NULL,
    [permissionTargetId] INT      NOT NULL,
    [permissionId]       INT      NOT NULL,
    [creationDate]       DATETIME NOT NULL,
    [createdBy]          INT      NOT NULL,
    [modifiedDate]       DATETIME NOT NULL,
    [modifiedBy]         INT      NOT NULL,
    CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED ([roleId] ASC, [permissionTargetId] ASC, [permissionId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY ([permissionId]) REFERENCES [dbo].[Permission] ([id]),
    CONSTRAINT [FK_RolePermission_PermissionTarget] FOREIGN KEY ([permissionTargetId]) REFERENCES [dbo].[PermissionTarget] ([id]),
    CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY ([roleId]) REFERENCES [dbo].[Role] ([id])
);

