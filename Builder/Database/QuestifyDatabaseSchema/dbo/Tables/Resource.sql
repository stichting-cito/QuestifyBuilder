CREATE TABLE [dbo].[Resource] (
    [resourceId]      UNIQUEIDENTIFIER NOT NULL,
    [version]         VARCHAR (20)     CONSTRAINT [DF_Resource_version] DEFAULT ((0.1)) NOT NULL,
    [bankId]          INT              NOT NULL,
    [name]            NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [title]           NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [description]     NVARCHAR(MAX)    COLLATE SQL_Latin1_General_CP1_CI_AS  CONSTRAINT [DF_Resource_Description]  DEFAULT ('') NULL,
    [stateId]         INT              NULL,
    [creationDate]    DATETIME         NOT NULL,
    [createdBy]       INT              NOT NULL,
    [modifiedDate]    DATETIME         NOT NULL,
    [modifiedBy]      INT              NOT NULL,
    [originalVersion] VARCHAR (20)     NULL,
    [originalName]    NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_Resource_Bank] FOREIGN KEY ([bankId]) REFERENCES [dbo].[Bank] ([id]),
    CONSTRAINT [FK_Resource_State] FOREIGN KEY ([stateId]) REFERENCES [dbo].[State] ([stateId]),
    CONSTRAINT [FK_Resource_User] FOREIGN KEY ([createdBy]) REFERENCES [dbo].[User] ([id]),
    CONSTRAINT [FK_Resource_User1] FOREIGN KEY ([modifiedBy]) REFERENCES [dbo].[User] ([id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Resource]
    ON [dbo].[Resource]([bankId] ASC) WITH (FILLFACTOR = 95);


GO
CREATE NONCLUSTERED INDEX [IX_Resource_Name]
    ON [dbo].[Resource]([name] ASC) WITH (FILLFACTOR = 95);

GO

CREATE NONCLUSTERED INDEX [IXNC_Resource_bankId]
ON [dbo].[Resource] ([bankId])
INCLUDE ([resourceId],[name],[title],[modifiedDate],[modifiedBy])

GO

CREATE NONCLUSTERED INDEX [IXNC_Resource_bankId_stateId]
    ON [dbo].[Resource]([bankId] ASC)
    INCLUDE([resourceId], [stateId]);
GO

CREATE NONCLUSTERED INDEX [IXNC_Resource_bankId_createdBy]
    ON [dbo].[Resource]([bankId] ASC, [createdBy] ASC)
    INCLUDE([resourceId]);
GO

