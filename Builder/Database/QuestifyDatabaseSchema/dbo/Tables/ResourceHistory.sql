CREATE TABLE [dbo].[ResourceHistory] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [ResourceId]   UNIQUEIDENTIFIER NOT NULL,
    [MajorVersion] SMALLINT         NOT NULL,
    [MinorVersion] SMALLINT         NOT NULL,
    [ModifiedBy]   NVARCHAR(255)    NOT NULL,
    [ModifiedDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    [Label]        NVARCHAR(4000)   NULL,
    [BinData]      VARBINARY (MAX)  NULL,
    [MetaData]     VARBINARY (MAX)  NULL,
    CONSTRAINT [PK_ResourceHistory] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ResourceHistory_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ResourceHistory_ResourceId]
    ON [dbo].[ResourceHistory]([ResourceId] ASC)
    INCLUDE([Id]) WITH (FILLFACTOR = 85);

