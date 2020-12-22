-- =============================================
-- Description:	Make CreatedBy and ModifiedBy reference newUserId when it currently is referencing currentUserId
-- =============================================
CREATE PROCEDURE dbo.ChangeCreatorModifier
	(
	@currentUserId int,
	@newUserId int
	)

AS
	/* SET NOCOUNT ON */ 
	Update Bank set createdBy = @newUserId where createdBy=@currentUserId
	Update Bank set modifiedBy = @newUserId where modifiedBy=@currentUserId
	
	Update Resource set createdBy = @newUserId Where createdBy = @currentUserId
	Update Resource set modifiedBy = @newUserId where modifiedBy = @currentUserId
	
	Update Role set createdBy = @newUserId Where createdBy = @currentUserId
	Update Role set modifiedBy = @newUserId where modifiedBy= @currentUserId
	
	Update [User] set createdBy = @newUserId Where createdBy = @currentUserId
	Update [User] set modifiedBy = @newUserId where modifiedBy = @currentUserId

	Update [CustomBankProperty] set createdBy = @newUserId Where createdBy = @currentUserId
	Update [CustomBankProperty] set modifiedBy = @newUserId where modifiedBy = @currentUserId

	Update [UserBankRole] set createdBy = @newUserId Where createdBy = @currentUserId
	Update [UserBankRole] set modifiedBy = @newUserId where modifiedBy = @currentUserId

	Update [UserApplicationRole] set createdBy = @newUserId Where createdBy = @currentUserId
	Update [UserApplicationRole] set modifiedBy = @newUserId where modifiedBy = @currentUserId

	Update [Permission] set createdBy = @newUserId Where createdBy = @currentUserId
	Update [Permission] set modifiedBy = @newUserId where modifiedBy = @currentUserId
	
	Update [PermissionTarget] set createdBy = @newUserId Where createdBy = @currentUserId
	Update [PermissionTarget] set modifiedBy = @newUserId where modifiedBy = @currentUserId
	
	Update [RolePermission] set createdBy = @newUserId Where createdBy = @currentUserId
	Update [RolePermission] set modifiedBy = @newUserId where modifiedBy = @currentUserId
	
	-- also transfer bankroles
	insert into UserBankRole (userId, bankId, BankRoleId, creationDate, createdBy, modifiedDate, Modifiedby )
	select @newUserId, bankId, BankRoleId, creationDate, createdBy, modifiedDate, Modifiedby from userbankrole b 
	where userid = @currentUserId 
	and not exists (select * from UserBankRole s where userid = @newUserId and b.bankid = s.bankid and b.bankroleid = s.bankRoleId)

	RETURN