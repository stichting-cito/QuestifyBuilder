CREATE TABLE [dbo].[ConceptStructurePartCustomBankProperty] (
    [conceptStructurePartCustomBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId]                     UNIQUEIDENTIFIER NOT NULL,
    [name]                                     NVARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [title]                                    NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [code]                                     UNIQUEIDENTIFIER NOT NULL,
    [conceptTypeId]                            INT              NOT NULL,
    CONSTRAINT [PK_ConceptStructurePartCustomProperty] PRIMARY KEY CLUSTERED ([conceptStructurePartCustomBankPropertyId] ASC),
    CONSTRAINT [FK_ConceptStructure_ConceptStructureCustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[ConceptStructureCustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ConceptStructurePart_ToConceptType] FOREIGN KEY ([conceptTypeId]) REFERENCES [dbo].[ConceptType] ([conceptTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ConceptStructurePartCustomBankProperty]
    ON [dbo].[ConceptStructurePartCustomBankProperty]([customBankPropertyId] ASC) WITH (FILLFACTOR = 95);

