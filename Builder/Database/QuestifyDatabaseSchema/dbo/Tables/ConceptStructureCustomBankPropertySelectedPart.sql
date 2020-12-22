CREATE TABLE [dbo].[ConceptStructureCustomBankPropertySelectedPart] (
    [conceptStructurePartId] UNIQUEIDENTIFIER NOT NULL,
    [resourceId]             UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ConceptStructureCustomBankPropertySelectedPart] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC, [conceptStructurePartId] ASC),
    CONSTRAINT [FK_ConceptStructureCustomBankPropertySelectedPart_ToConceptStructurePart] FOREIGN KEY ([conceptStructurePartId]) REFERENCES [dbo].[ConceptStructurePartCustomBankProperty] ([conceptStructurePartCustomBankPropertyId]),
    CONSTRAINT [FK_ConceptStructureCustomBankPropertySelectedPart_ToConceptStructurePartValue] FOREIGN KEY ([resourceId], [customBankPropertyId]) REFERENCES [dbo].[ConceptStructureCustomBankPropertyValue] ([resourceId], [customBankPropertyId]) ON DELETE CASCADE
);

