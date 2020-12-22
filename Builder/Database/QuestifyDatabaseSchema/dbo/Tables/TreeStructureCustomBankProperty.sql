CREATE TABLE [dbo].[TreeStructureCustomBankProperty] (
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TreeStructureCustomBankProperty] PRIMARY KEY CLUSTERED ([customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_TreeStructureCustomBankProperty_CustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[CustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE
);

