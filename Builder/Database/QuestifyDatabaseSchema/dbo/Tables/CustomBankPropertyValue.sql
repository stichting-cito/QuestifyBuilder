CREATE TABLE [dbo].[CustomBankPropertyValue] (
    [resourceId]           UNIQUEIDENTIFIER NOT NULL,
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    [displayValue] NVARCHAR(255) NULL, 
    CONSTRAINT [PK_CustomBankPropertyValue] PRIMARY KEY CLUSTERED ([resourceId] ASC, [customBankPropertyId] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_CustomBankPropertyValue_CustomBankProperty] FOREIGN KEY ([customBankPropertyId]) REFERENCES [dbo].[CustomBankProperty] ([customBankPropertyId]),
    CONSTRAINT [FK_CustomBankPropertyValue_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

GO

CREATE NONCLUSTERED INDEX [IXNC_CustomBankPropertyValue_customBankPropertyId]
    ON [dbo].[CustomBankPropertyValue]([customBankPropertyId] ASC)
    INCLUDE([resourceId]);
GO