CREATE TABLE [dbo].[UserApplicationRole] (
    [userId]            INT      NOT NULL,
    [applicationRoleId] INT      NOT NULL,
    [creationDate]      DATETIME NOT NULL,
    [createdBy]         INT      NOT NULL,
    [modifiedDate]      DATETIME NOT NULL,
    [modifiedBy]        INT      NOT NULL,
    CONSTRAINT [PK_UserApplicationRole] PRIMARY KEY CLUSTERED ([userId] ASC, [applicationRoleId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_UserApplicationRole_Role] FOREIGN KEY ([applicationRoleId]) REFERENCES [dbo].[Role] ([id]),
    CONSTRAINT [FK_UserApplicationRole_User] FOREIGN KEY ([userId]) REFERENCES [dbo].[User] ([id]) ON DELETE CASCADE
);

