CREATE TABLE [dbo].[ListCustomBankPropertySelectedValue] (
    [resourceId]                    UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId]          UNIQUEIDENTIFIER NOT NULL,
    [listValueBankCustomPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ListCustomBankPropertySelectedValue] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC, [listValueBankCustomPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ListCustomBankPropertySelectedValue_ListCustomBankPropertyValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[ListCustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ListCustomBankPropertySelectedValue_ListValueCustomBankProperty] FOREIGN KEY ([listValueBankCustomPropertyId]) REFERENCES [dbo].[ListValueCustomBankProperty] ([listValueBankCustomPropertyId])
);

