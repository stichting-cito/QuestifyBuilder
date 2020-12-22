CREATE TABLE [dbo].[TreeStructureCustomBankPropertyValue] (
    [resourceId]           UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TreeStructureCustomBankPropertyValue] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_TreeStructureCustomBankPropertyValue_CustomBankPropertyValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[CustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TreeStructureCustomBankPropertyValue_TreeStructureCustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[TreeStructureCustomBankProperty] ([customBankPropertyId])
);
GO

CREATE NONCLUSTERED INDEX [IXNC_TreeStructureCustomBankPropertyValue_customBankPropertyId]
ON [dbo].[TreeStructureCustomBankPropertyValue] ([customBankPropertyId])
INCLUDE ([resourceId])
GO
