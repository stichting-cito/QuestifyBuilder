CREATE TABLE [dbo].[PermissionTarget] (
    [id]                  INT          NOT NULL,
    [name]                NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [targettedNamedTask]  NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [isApplicationTarget] BIT          NOT NULL,
    [creationDate]        DATETIME     NOT NULL,
    [createdBy]           INT          NOT NULL,
    [modifiedDate]        DATETIME     NOT NULL,
    [modifiedBy]          INT          NOT NULL,
    CONSTRAINT [PK_PermissionTarget] PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [IX_PermissionTarget] UNIQUE NONCLUSTERED ([name] ASC, [targettedNamedTask] ASC) WITH (FILLFACTOR = 95)
);

