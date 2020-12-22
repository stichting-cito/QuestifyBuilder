-- =============================================
-- Description:	Updates the bankId of a resource identified by its id.
-- =============================================

CREATE PROCEDURE [dbo].[UpdateResourceBankId]
	(
	@ResourceId UniqueIdentifier,
	@BankId Int
	)
AS
BEGIN
	SET NOCOUNT ON;

    -- Update the bank Id of the resource.
	Update dbo.Resource Set bankId = @BankId Where (resourceId = @ResourceId)

	RETURN 0;
END