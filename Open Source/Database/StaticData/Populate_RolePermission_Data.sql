﻿--MERGE generated by 'sp_generate_merge' stored procedure, Version 0.93
--Originally by Vyas (http://vyaskn.tripod.com); adapted for SQL Server 2008/2012 by Daniel Nolan (http://danere.com)

SET NOCOUNT ON

--Are role permissions present? If so, do nothing!
DECLARE @RPCnt int
SELECT @RPCnt = (select COUNT(*) from [dbo].[RolePermission]  ) 
IF @RPCnt = 0
BEGIN

MERGE INTO [dbo].[RolePermission] AS Target
USING (VALUES
   (1,20,30,GETDATE(),1,GETDATE(),1)
  ,(2,3,4,GETDATE(),1,GETDATE(),1)
  ,(2,5,4,GETDATE(),1,GETDATE(),1)
  ,(2,7,4,GETDATE(),1,GETDATE(),1)
  ,(2,29,4,GETDATE(),1,GETDATE(),1)
  ,(2,31,4,GETDATE(),1,GETDATE(),1)
  ,(2,3,5,GETDATE(),1,GETDATE(),1)
  ,(2,5,5,GETDATE(),1,GETDATE(),1)
  ,(2,7,5,GETDATE(),1,GETDATE(),1)
  ,(2,29,5,GETDATE(),1,GETDATE(),1)
  ,(2,31,5,GETDATE(),1,GETDATE(),1)
  ,(2,3,12,GETDATE(),1,GETDATE(),1)
  ,(2,5,12,GETDATE(),1,GETDATE(),1)
  ,(2,7,12,GETDATE(),1,GETDATE(),1)
  ,(2,29,12,GETDATE(),1,GETDATE(),1)
  ,(2,31,12,GETDATE(),1,GETDATE(),1)
) AS Source ([roleId],[permissionTargetId],[permissionId],[creationDate],[createdBy],[modifiedDate],[modifiedBy])
ON (Target.[permissionId] = Source.[permissionId] AND Target.[permissionTargetId] = Source.[permissionTargetId] AND Target.[roleId] = Source.[roleId])
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
 INSERT([roleId],[permissionTargetId],[permissionId],[creationDate],[createdBy],[modifiedDate],[modifiedBy])
 VALUES(Source.[roleId],Source.[permissionTargetId],Source.[permissionId],Source.[creationDate],Source.[createdBy],Source.[modifiedDate],Source.[modifiedBy])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

END

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[RolePermission]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[RolePermission] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET NOCOUNT OFF
GO