CREATE TABLE [dbo].[ControlTemplateResource] (
    [resourceId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ControlTemplateResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ControlTemplateResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

