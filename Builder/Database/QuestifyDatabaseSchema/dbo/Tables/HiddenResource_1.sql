CREATE TABLE [dbo].[HiddenResource] (
    [ResourceId] UNIQUEIDENTIFIER NOT NULL,
    [BankId]     INT              NOT NULL,
    CONSTRAINT [PK_HiddenResource_1] PRIMARY KEY CLUSTERED ([ResourceId] ASC, [BankId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_HiddenResource_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([resourceId])
);

