CREATE PROCEDURE [dbo].[ClearBank] 
	@bankId Integer
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- First delete dependant resources
	DELETE FROM DependentResource
	FROM         Bank INNER JOIN
						  Resource ON Bank.id = Resource.bankId INNER JOIN
						  DependentResource ON Resource.resourceId = DependentResource.dependentResourceId
	WHERE     (Bank.id = @bankId)

	-- Delete resources
	DELETE FROM Resource
	WHERE     (bankId = @bankId)

	-- Delete CustomBankProperties
	DELETE FROM [CustomBankProperty] 
    WHERE (CustomBankProperty.bankId = @bankId)