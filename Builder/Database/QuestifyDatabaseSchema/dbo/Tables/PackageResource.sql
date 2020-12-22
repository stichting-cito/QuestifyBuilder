CREATE TABLE [dbo].[PackageResource] (
    [ResourceId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_PackageResource] PRIMARY KEY CLUSTERED ([ResourceId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_PackageResource_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

