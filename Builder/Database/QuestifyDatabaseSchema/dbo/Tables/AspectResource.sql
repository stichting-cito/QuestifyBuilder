CREATE TABLE [dbo].[AspectResource] (
    [resourceId] UNIQUEIDENTIFIER NOT NULL,
    [rawScore]   INT              NOT NULL,
    CONSTRAINT [PK_AspectResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_AspectResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

