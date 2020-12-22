CREATE TABLE [dbo].[RichTextValueCustomBankProperty] (
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RichTextValueCustomProperty] PRIMARY KEY CLUSTERED ([customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_RichTextValueCustomProperty_CustomProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[CustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE
);

