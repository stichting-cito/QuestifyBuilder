CREATE TABLE [dbo].[ListCustomBankProperty] (
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    [multipleSelect]       BIT              CONSTRAINT [DF_ListCustomProperty_multipleSelect] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ListCustomProperty] PRIMARY KEY CLUSTERED ([customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_ListCustomProperty_CustomProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[CustomBankProperty] ([customBankPropertyId]) ON DELETE CASCADE
);

