CREATE TABLE [dbo].[ConceptStructureCustomBankProperty] (
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ConceptStructureCustomBankProperty] PRIMARY KEY CLUSTERED ([customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ConceptStructureCustomBankProperty_CustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[CustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE
);

