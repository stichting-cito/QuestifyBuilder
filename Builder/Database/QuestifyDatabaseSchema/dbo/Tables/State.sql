CREATE TABLE [dbo].[State] (
    [stateId]     INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR(50)   COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [title]       NVARCHAR(50)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [description] VARCHAR (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([stateId] ASC) WITH (FILLFACTOR = 95)
);

