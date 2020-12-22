CREATE TABLE [dbo].[TreeStructureCustomBankPropertySelectedPart] (
    [treeStructurePartId]  UNIQUEIDENTIFIER NOT NULL,
    [resourceId]           UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TreeStructureCustomBankPropertySelectedPart] PRIMARY KEY CLUSTERED ([treeStructurePartId] ASC, [resourceId] ASC, [customBankPropertyId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_TreeStructureCustomBankPropertySelectedPart_TreeStructureCustomBankPropertyValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[TreeStructureCustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TreeStructureCustomBankPropertySelectedPart_TreeStructurePartCustomBankProperty] FOREIGN KEY ([treeStructurePartId]) REFERENCES [dbo].[TreeStructurePartCustomBankProperty] ([treeStructurePartCustomBankPropertyId]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IXNC_TreeStructureCustomBankPropertySelectedPart_customBankPropertyId_4AF6F]
    ON [dbo].[TreeStructureCustomBankPropertySelectedPart]([customBankPropertyId] ASC)
    INCLUDE([treeStructurePartId], [resourceId]);
GO

CREATE NONCLUSTERED INDEX [IXNC_TreeStructureCustomBankPropertySelectedPart_resourceId_customBankPropertyId_DDB6A]
    ON [dbo].[TreeStructureCustomBankPropertySelectedPart]([resourceId] ASC, [customBankPropertyId] ASC);
GO