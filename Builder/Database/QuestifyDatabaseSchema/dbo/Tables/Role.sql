CREATE TABLE [dbo].[Role] (
    [id]                INT          IDENTITY (1, 1) NOT NULL,
    [name]              NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [description]       NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [isApplicationRole] BIT          NULL,
    [creationDate]      DATETIME     NOT NULL,
    [createdBy]         INT          NOT NULL,
    [modifiedDate]      DATETIME     NOT NULL,
    [modifiedBy]        INT          NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_Role_UserCreatedBy] FOREIGN KEY ([createdBy]) REFERENCES [dbo].[User] ([id]),
    CONSTRAINT [FK_Role_UserModifiedBy] FOREIGN KEY ([modifiedBy]) REFERENCES [dbo].[User] ([id])
);

