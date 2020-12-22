CREATE FULLTEXT INDEX ON [dbo].[ConceptStructurePartCustomBankProperty]
    ([name] LANGUAGE 1043, [title] LANGUAGE 1043)
    KEY INDEX [PK_ConceptStructurePartCustomProperty]
    ON [Resource_Properties];


GO
CREATE FULLTEXT INDEX ON [dbo].[FreeValueCustomBankPropertyValue]
    ([value] LANGUAGE 1043)
    KEY INDEX [IX_FreeValueCustomBankPropertyValueId]
    ON [Resource_Properties];


GO
CREATE FULLTEXT INDEX ON [dbo].[ListValueCustomBankProperty]
    ([name] LANGUAGE 1043, [title] LANGUAGE 1043)
    KEY INDEX [PK_ListValueCustomProperty]
    ON [Resource_Properties];


GO
CREATE FULLTEXT INDEX ON [dbo].[Resource]
    ([name] LANGUAGE 1043, [title] LANGUAGE 1043, [description] LANGUAGE 1043)
    KEY INDEX [PK_Resource]
    ON [Resource_Properties];


GO
CREATE FULLTEXT INDEX ON [dbo].[ResourceData]
    ([binData] TYPE COLUMN [fileExtension] LANGUAGE 1043)
    KEY INDEX [PK_ResourceData]
    ON [Resource_Properties];


--This is excluded when the build is set to DEBUG.