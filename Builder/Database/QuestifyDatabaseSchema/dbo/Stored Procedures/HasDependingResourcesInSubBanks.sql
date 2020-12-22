CREATE PROCEDURE [dbo].[HasDependingResourcesInSubBanks]
	@bankId Integer	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Select all subbanks of bankId
	with SubBanks (Id, name, parentid)
	As
	(
		select b.Id, b.name, b.parentBankId
		from Bank b
		WHERE (Id = @bankId)

		union all

		select b.Id, b.name, b.parentBankId
		from Bank b
			inner join SubBanks as sb on b.parentBankId = sb.Id		
	)
	select top 1 Count(DependentResource.dependentResourceId)
	FROM Bank 
		INNER JOIN 	Resource ON Bank.id = Resource.bankId
		INNER JOIN 	DependentResource ON Resource.resourceId = DependentResource.resourceId
		INNER JOIN 	Resource r ON r.resourceId = DependentResource.dependentResourceId and r.bankId = @bankid
	where id <> @bankid

END
