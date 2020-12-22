CREATE TABLE [dbo].[FreeValueCustomBankPropertyValue] (
    [resourceId]                         UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId]               UNIQUEIDENTIFIER NOT NULL,
    [value]                              NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [freeValueCustomBankPropertyValueId] INT              IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_FreeValueCustomBankPropertyValue] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_FreeValueCustomBankPropertyValue_CustomBankPropertyValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[CustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_FreeValueCustomBankPropertyValue_FreeValueCustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[FreeValueCustomBankProperty] ([customBankPropertyId]),
    CONSTRAINT [IX_FreeValueCustomBankPropertyValueId] UNIQUE NONCLUSTERED ([freeValueCustomBankPropertyValueId] ASC) WITH (FILLFACTOR = 95)
);

