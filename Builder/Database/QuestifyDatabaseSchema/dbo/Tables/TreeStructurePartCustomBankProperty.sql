CREATE TABLE [dbo].[TreeStructurePartCustomBankProperty] (
    [treeStructurePartCustomBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId]                  UNIQUEIDENTIFIER NOT NULL,
    [name]                                  NVARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [title]                                 NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [code]                                  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TreeStructurePartCustomBankProperty] PRIMARY KEY CLUSTERED ([treeStructurePartCustomBankPropertyId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_TreeStructurePartCustomBankProperty_TreeStructureCustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[TreeStructureCustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE
);

