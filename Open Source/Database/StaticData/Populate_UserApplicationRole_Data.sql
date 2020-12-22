﻿--MERGE generated by 'sp_generate_merge' stored procedure, Version 0.93
--Originally by Vyas (http://vyaskn.tripod.com); adapted for SQL Server 2008/2012 by Daniel Nolan (http://danere.com)

SET NOCOUNT ON

--Are user application roles present? If so, do nothing!
DECLARE @UARCnt int
SELECT @UARCnt = (select COUNT(*) from [dbo].[UserApplicationRole]  ) 
IF @UARCnt = 0
BEGIN

MERGE INTO [dbo].[UserApplicationRole] AS Target
USING (VALUES
  (1,1,GETDATE(),1,GETDATE(),1)
) AS Source ([userId],[applicationRoleId],[creationDate],[createdBy],[modifiedDate],[modifiedBy])
ON (Target.[applicationRoleId] = Source.[applicationRoleId] AND Target.[userId] = Source.[userId])
WHEN MATCHED AND (
	NULLIF(Source.[creationDate], Target.[creationDate]) IS NOT NULL OR NULLIF(Target.[creationDate], Source.[creationDate]) IS NOT NULL OR 
	NULLIF(Source.[createdBy], Target.[createdBy]) IS NOT NULL OR NULLIF(Target.[createdBy], Source.[createdBy]) IS NOT NULL OR 
	NULLIF(Source.[modifiedDate], Target.[modifiedDate]) IS NOT NULL OR NULLIF(Target.[modifiedDate], Source.[modifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[modifiedBy], Target.[modifiedBy]) IS NOT NULL OR NULLIF(Target.[modifiedBy], Source.[modifiedBy]) IS NOT NULL) THEN
 UPDATE SET
 [creationDate] = Source.[creationDate], 
[createdBy] = Source.[createdBy], 
[modifiedDate] = Source.[modifiedDate], 
[modifiedBy] = Source.[modifiedBy]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([userId],[applicationRoleId],[creationDate],[createdBy],[modifiedDate],[modifiedBy])
 VALUES(Source.[userId],Source.[applicationRoleId],Source.[creationDate],Source.[createdBy],Source.[modifiedDate],Source.[modifiedBy]);

END

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[UserApplicationRole]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[UserApplicationRole] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET NOCOUNT OFF
GO
