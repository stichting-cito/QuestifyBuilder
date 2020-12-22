CREATE TABLE [dbo].[TestPackageResource] (
    [resourceId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TestPackageResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_TestPackageResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

