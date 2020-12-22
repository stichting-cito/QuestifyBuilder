CREATE TABLE [dbo].[ItemResource] (
    [resourceId]          UNIQUEIDENTIFIER NOT NULL,
    [isSystemItem]        BIT              CONSTRAINT [DF_ItemResource_isSystemItem] DEFAULT ((0)) NOT NULL,
    [alternativesCount]   INT              NULL,
    [keyValues]           NVARCHAR(500)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [responseCount]       INT              NULL,
    [rawScore]            INT              NULL,
    [TesterSchemaVersion] INT              NULL,
    [ILTName]             NVARCHAR(255)    NULL,
    [ILTVersion]          INT              NULL,
    [MaxScore]            DECIMAL (9, 2)   NULL,
    [ItemAutoId] INT IDENTITY (33554432, 1), 
    [ItemId] VARCHAR(6) NULL, 
    CONSTRAINT [PK_ItemResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ItemResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

