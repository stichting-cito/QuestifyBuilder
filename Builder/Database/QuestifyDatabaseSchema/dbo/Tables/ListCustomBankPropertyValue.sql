CREATE TABLE [dbo].[ListCustomBankPropertyValue] (
    [resourceId]           UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ListCustomBankPropertyValue] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ListCustomBankPropertyValue_CustomBankPropertyValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[CustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ListCustomBankPropertyValue_ListCustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[ListCustomBankProperty] ([customBankPropertyId])
);

