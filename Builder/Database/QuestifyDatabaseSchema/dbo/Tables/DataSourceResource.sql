CREATE TABLE [dbo].[DataSourceResource] (
    [resourceId]     UNIQUEIDENTIFIER NOT NULL,
    [dataSourceType] VARCHAR (255)    COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [isTemplate]     BIT              NOT NULL,
    CONSTRAINT [PK_DataSourceResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_DataSourceResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

