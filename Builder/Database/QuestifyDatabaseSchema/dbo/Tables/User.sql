CREATE TABLE [dbo].[User] (
    [id]                 INT          IDENTITY (1, 1) NOT NULL,
    [userName]           NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [password]           NVARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [fullName]           NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Active]             BIT          NOT NULL,
    [creationDate]       DATETIME     NOT NULL,
    [createdBy]          INT          NOT NULL,
    [modifiedDate]       DATETIME     NOT NULL,
    [modifiedBy]         INT          NOT NULL,
    [authenticationType] VARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [userSettings]		 NVARCHAR(MAX)	COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT '', 
    [changePassword] BIT NOT NULL DEFAULT 0, 
    [allowedFeatures] NVARCHAR(2000) NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 95),
    CONSTRAINT [FK_User_CreatedBy] FOREIGN KEY ([createdBy]) REFERENCES [dbo].[User] ([id]),
    CONSTRAINT [FK_User_ModifiedBy] FOREIGN KEY ([modifiedBy]) REFERENCES [dbo].[User] ([id])
);

