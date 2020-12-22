CREATE TABLE [dbo].[ChildTreeStructurePartCustomBankProperty] (
    [id]                                         UNIQUEIDENTIFIER NOT NULL,
    [childTreeStructurePartCustomBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    [treeStructurePartCustomBankPropertyId]      UNIQUEIDENTIFIER NOT NULL,
    [visualOrder]                                INT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC, [treeStructurePartCustomBankPropertyId] ASC, [childTreeStructurePartCustomBankPropertyId] ASC),
    CONSTRAINT [FK_ChildTreeStructurePartCustomBankProperty_TreeStructurePartCustomBankProperty] FOREIGN KEY ([treeStructurePartCustomBankPropertyId]) REFERENCES [dbo].[TreeStructurePartCustomBankProperty] ([treeStructurePartCustomBankPropertyId]) ON DELETE CASCADE
);

