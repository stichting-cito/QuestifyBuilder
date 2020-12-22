/*
Post-Deployment Script Template for Questify Builder Open Source							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\StaticData\Populate_User_data.sql
:r .\StaticData\Populate_Role_data.sql
:r .\StaticData\Populate_Action_data.sql
:r .\StaticData\Populate_Bank_data.sql
:r .\StaticData\Populate_State_data.sql

:r .\StaticData\Populate_Permission_Data.sql
:r .\StaticData\Populate_PermissionTarget_Data.sql
:r .\StaticData\Populate_StateAction_data.sql
:r .\StaticData\Populate_UserApplicationRole_data.sql
:r .\StaticData\Populate_RolePermission_Data.sql

:r .\StaticData\Populate_UserBankRole_data.sql

:r .\StaticData\Populate_ConceptType_Data.sql