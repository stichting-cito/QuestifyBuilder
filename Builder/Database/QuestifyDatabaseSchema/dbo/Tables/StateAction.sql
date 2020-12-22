CREATE TABLE [dbo].[StateAction] (
    [target]   VARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [stateId]  INT          NOT NULL,
    [actionId] INT          NOT NULL,
    CONSTRAINT [PK_StateAction] PRIMARY KEY CLUSTERED ([target] ASC, [stateId] ASC, [actionId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_StateAction_action] FOREIGN KEY ([actionId]) REFERENCES [dbo].[Action] ([actionId]),
    CONSTRAINT [FK_StateAction_State] FOREIGN KEY ([stateId]) REFERENCES [dbo].[State] ([stateId])
);

