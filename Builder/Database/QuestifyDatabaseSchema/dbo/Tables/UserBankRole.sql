CREATE TABLE [dbo].[UserBankRole] (
    [userId]       INT      NOT NULL,
    [bankId]       INT      NOT NULL,
    [bankRoleId]   INT      NOT NULL,
    [creationDate] DATETIME NOT NULL,
    [createdBy]    INT      NOT NULL,
    [modifiedDate] DATETIME NOT NULL,
    [modifiedBy]   INT      NOT NULL,
    CONSTRAINT [PK_UserBankRole] PRIMARY KEY CLUSTERED ([userId] ASC, [bankId] ASC, [bankRoleId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_UserBankRole_Bank] FOREIGN KEY ([bankId]) REFERENCES [dbo].[Bank] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserBankRole_Role] FOREIGN KEY ([bankRoleId]) REFERENCES [dbo].[Role] ([id]),
    CONSTRAINT [FK_UserBankRole_User] FOREIGN KEY ([userId]) REFERENCES [dbo].[User] ([id]) ON DELETE CASCADE
);

