CREATE TABLE [dbo].[UserTokens]
(
	[userId] INT NOT NULL , 
    [token] NVARCHAR(MAX) NOT NULL, 
    [created] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([userId]) ,
	CONSTRAINT [FK_UserTokens_User] FOREIGN KEY ([userId]) REFERENCES [dbo].[User] ([id]) ON DELETE CASCADE
)
