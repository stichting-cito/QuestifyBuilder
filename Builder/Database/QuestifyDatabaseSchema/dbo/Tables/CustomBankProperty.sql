CREATE TABLE [dbo].[CustomBankProperty] (
    [customBankPropertyId] UNIQUEIDENTIFIER NOT NULL,
    [bankId]               INT              NOT NULL,
    [applicableToMask]     INT              NULL,
    [publishable]          BIT              CONSTRAINT [DF_CustomBankProperty_publishable] DEFAULT ((0)) NOT NULL,
    [scorable]             BIT              NOT NULL DEFAULT ((0)), 
    [name]                 NVARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [title]                NVARCHAR(255)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [description]          NVARCHAR(MAX)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [creationDate]         DATETIME         NOT NULL,
    [createdBy]            INT              NOT NULL,
    [modifiedDate]         DATETIME         NOT NULL,
    [modifiedBy]           INT              NOT NULL,
    [code]                 UNIQUEIDENTIFIER CONSTRAINT [DF_CustomBankProperty_code] DEFAULT (newsequentialid()) NOT NULL,
    [stateId]              INT              NULL,
    [version]              VARCHAR (20)     DEFAULT ((0.1)) NOT NULL,
    CONSTRAINT [PK_CustomProperty] PRIMARY KEY CLUSTERED ([customBankPropertyId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_CustomBankProperty_State] FOREIGN KEY ([stateId]) REFERENCES [dbo].[State] ([stateId]),
    CONSTRAINT [FK_CustomBankProperty_User] FOREIGN KEY ([createdBy]) REFERENCES [dbo].[User] ([id]),
    CONSTRAINT [FK_CustomBankProperty_User1] FOREIGN KEY ([modifiedBy]) REFERENCES [dbo].[User] ([id]),
    CONSTRAINT [FK_CustomProperty_Bank] FOREIGN KEY ([bankId]) REFERENCES [dbo].[Bank] ([id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AK_CustomBankProperty_bankId]
    ON [dbo].[CustomBankProperty]([bankId] ASC)
    INCLUDE([applicableToMask], [createdBy], [creationDate], [customBankPropertyId], [description], [modifiedBy], [modifiedDate], [name], [publishable], [title]) WITH (FILLFACTOR = 85);


GO
CREATE NONCLUSTERED INDEX [IX_CustomBankProperty_Name]
    ON [dbo].[CustomBankProperty]([name] ASC) WITH (FILLFACTOR = 85);

