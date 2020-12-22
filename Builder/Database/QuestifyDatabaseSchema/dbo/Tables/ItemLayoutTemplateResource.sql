CREATE TABLE [dbo].[ItemLayoutTemplateResource] (
    [resourceId] UNIQUEIDENTIFIER NOT NULL,
    [itemType]   NVARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_ItemLayoutTemplateResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ItemLayoutTemplateResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

