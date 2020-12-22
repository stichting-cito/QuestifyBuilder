CREATE TABLE [dbo].[ResourceData] (
    [resourceId]    UNIQUEIDENTIFIER NOT NULL,
    [binData]       IMAGE            NULL,
    [url]           NVARCHAR (500)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [fileExtension] VARCHAR (8)      COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ident]         BIGINT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_ResourceData] PRIMARY KEY NONCLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 90, PAD_INDEX = ON),
    CONSTRAINT [FK_ResourceData_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);


GO
CREATE CLUSTERED INDEX [CID_ResourceData]
    ON [dbo].[ResourceData]([ident] ASC) WITH (FILLFACTOR = 95);

