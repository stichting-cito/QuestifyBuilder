CREATE TABLE [dbo].[DependentResource] (
    [resourceId]          UNIQUEIDENTIFIER NOT NULL,
    [dependentResourceId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_DependentResource] PRIMARY KEY CLUSTERED ([resourceId] ASC, [dependentResourceId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_DependentResource_ReferenceToDependent] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE,
    CONSTRAINT [FK_DependentResource_Resource] FOREIGN KEY ([dependentResourceId]) REFERENCES [dbo].[Resource] ([resourceId])
);


GO
CREATE NONCLUSTERED INDEX [IX_dependentResourceID]
    ON [dbo].[DependentResource]([dependentResourceId] ASC) WITH (FILLFACTOR = 95);

