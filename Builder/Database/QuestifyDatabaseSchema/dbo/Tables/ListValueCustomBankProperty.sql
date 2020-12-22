CREATE TABLE [dbo].[ListValueCustomBankProperty] (
    [listValueBankCustomPropertyId] UNIQUEIDENTIFIER CONSTRAINT [DF_ListValueCustomProperty_listCustomPropertyValueId] DEFAULT (newid()) NOT NULL,
    [customBankPropertyId]          UNIQUEIDENTIFIER NOT NULL,
    [name]                          NVARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [title]                         NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [code]                          UNIQUEIDENTIFIER CONSTRAINT [DF_ListValueCustomBankProperty_code] DEFAULT (newsequentialid()) NOT NULL,
    CONSTRAINT [PK_ListValueCustomProperty] PRIMARY KEY CLUSTERED ([listValueBankCustomPropertyId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_ListValueCustomProperty_ListCustomProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[ListCustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ListValueCustomBankProperty]
    ON [dbo].[ListValueCustomBankProperty]([customBankPropertyId] ASC) WITH (FILLFACTOR = 85);

