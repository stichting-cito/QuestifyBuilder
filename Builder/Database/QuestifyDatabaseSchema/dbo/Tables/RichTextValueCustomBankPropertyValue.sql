CREATE TABLE [dbo].[RichTextValueCustomBankPropertyValue] (
    [resourceId]                         UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId]               UNIQUEIDENTIFIER NOT NULL,
    [value]                              NVARCHAR(MAX)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [richTextValueCustomBankPropertyValueId] INT              IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_RichTextValueCustomBankPropertyValue] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_RichTextValueCustomBankPropertyValue_CustomBankPropertyValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[CustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_RichTextValueCustomBankPropertyValue_RichTextValueCustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[RichTextValueCustomBankProperty] ([customBankPropertyId]),
    CONSTRAINT [IX_RichTextValueCustomBankPropertyValueId] UNIQUE NONCLUSTERED ([richTextValueCustomBankPropertyValueId] ASC) WITH (FILLFACTOR = 95)
);

