CREATE TABLE [dbo].[ConceptStructureCustomBankPropertyValue] (
    [resourceId]           UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ConceptStructureCustomBankPropertyValue] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC),
    CONSTRAINT [FK_ConceptStructureCustomBankPropertyValue_ConceptStructureCustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[ConceptStructureCustomBankProperty] ([customBankPropertyId]),
    CONSTRAINT [FK_ConceptStructureCustomBankPropertyValue_CustomBankPropertyValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[CustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE
);

