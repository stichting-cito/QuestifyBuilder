CREATE TABLE [dbo].[FreeValueCustomBankProperty] (
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_FreeValueCustomProperty] PRIMARY KEY CLUSTERED ([customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_FreeValueCustomProperty_CustomProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[CustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE
);

