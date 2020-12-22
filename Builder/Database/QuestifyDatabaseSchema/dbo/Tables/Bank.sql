CREATE TABLE [dbo].[Bank] (
    [id]                                 INT          IDENTITY (1, 1) NOT NULL,
    [parentBankId]                       INT          NULL,
    [name]                               NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [type]                               VARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [creationDate]                       DATETIME     NOT NULL,
    [createdBy]                          INT          NOT NULL,
    [modifiedDate]                       DATETIME     NOT NULL,
    [modifiedBy]                         INT          NOT NULL,
    CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_Bank_Bank] FOREIGN KEY ([parentBankId]) REFERENCES [dbo].[Bank] ([id]),
    CONSTRAINT [FK_Bank_CreatedByUser] FOREIGN KEY ([createdBy]) REFERENCES [dbo].[User] ([id]),
    CONSTRAINT [FK_Bank_ModifiedByUser] FOREIGN KEY ([modifiedBy]) REFERENCES [dbo].[User] ([id])
);

