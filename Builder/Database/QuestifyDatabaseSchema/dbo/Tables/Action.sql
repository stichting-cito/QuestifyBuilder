CREATE TABLE [dbo].[Action] (
    [actionId] INT          IDENTITY (1, 1) NOT NULL,
    [name]     VARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [title]    VARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_action] PRIMARY KEY CLUSTERED ([actionId] ASC) WITH (FILLFACTOR = 95)
);

