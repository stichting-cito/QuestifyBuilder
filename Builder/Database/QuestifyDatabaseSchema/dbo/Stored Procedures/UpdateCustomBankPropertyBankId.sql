-- =============================================
-- Description:	Updates the bankId of a resource identified by its id.
-- =============================================

CREATE PROCEDURE [dbo].[UpdateCustomBankPropertyBankId]
	(
	@CustomBankPropertyId UniqueIdentifier,
	@BankId Int
	)
AS
BEGIN
	SET NOCOUNT ON;

    -- Update the bank Id of the resource.
	Update dbo.CustomBankProperty Set bankId = @BankId Where (CustomBankPropertyId = @CustomBankPropertyId)

	RETURN 0;
END