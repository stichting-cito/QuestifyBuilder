CREATE TABLE [dbo].[ConceptType] (
    [conceptTypeId]    INT           NOT NULL,
    [name]             VARCHAR (255) NOT NULL,
    [applicableToMask] INT           NOT NULL,
    CONSTRAINT [PK_ConceptType] PRIMARY KEY CLUSTERED ([conceptTypeId] ASC) WITH (FILLFACTOR = 95)
);

