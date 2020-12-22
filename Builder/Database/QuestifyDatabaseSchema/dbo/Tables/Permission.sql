CREATE TABLE [dbo].[Permission] (
    [id]                 INT          NOT NULL,
    [name]               NVARCHAR(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [description]        NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [whenOwnerCondition] BIT          NOT NULL,
    [creationDate]       DATETIME     NOT NULL,
    [createdBy]          INT          NOT NULL,
    [modifiedDate]       DATETIME     NOT NULL,
    [modifiedBy]         INT          NOT NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 95)
);

