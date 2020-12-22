CREATE TABLE [dbo].[ChildConceptStructurePartCustomBankProperty] (
    [id]                                            UNIQUEIDENTIFIER NOT NULL,
    [conceptStructurePartCustomBankPropertyId]      UNIQUEIDENTIFIER NOT NULL,
    [childConceptStructurePartCustomBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    [visualOrder]                                   INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ChildConceptStructurePartCustomBankProperty] PRIMARY KEY CLUSTERED ([id] ASC, [conceptStructurePartCustomBankPropertyId] ASC, [childConceptStructurePartCustomBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ChildConceptStructurePartToConceptStructurePart] FOREIGN KEY ([childConceptStructurePartCustomBankPropertyId]) REFERENCES [dbo].[ConceptStructurePartCustomBankProperty] ([conceptStructurePartCustomBankPropertyId]),
    CONSTRAINT [FK_ConceptStructurePartToConceptStructurePart] FOREIGN KEY ([conceptStructurePartCustomBankPropertyId]) REFERENCES [dbo].[ConceptStructurePartCustomBankProperty] ([conceptStructurePartCustomBankPropertyId]) ON DELETE CASCADE
);

