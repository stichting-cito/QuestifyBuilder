CREATE TABLE [dbo].[GenericResource] (
    [resourceId] UNIQUEIDENTIFIER NOT NULL,
    [mediaType]  VARCHAR (255)    COLLATE SQL_Latin1_General_CP1_CI_AS CONSTRAINT [DF_GenericResource_mediaType] DEFAULT ('image') NOT NULL,
    [size]       INT              CONSTRAINT [DF_GenericResource_size] DEFAULT ((0)) NOT NULL,
    [dimensions] VARCHAR (50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [isTemplate] BIT              CONSTRAINT [DF_GenericResource_isTemplate] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_GenericResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_GenericResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

