CREATE PROCEDURE GetTreeStructurePartName
	@Code uniqueidentifier
  , @BankId int
AS
BEGIN
	DECLARE @TreeStructurePartName varchar(50) = NULL

	WHILE @TreeStructurePartName IS NULL AND @BankId IS NOT NULL
	BEGIN
		SELECT @TreeStructurePartName = tsp.[name]
		FROM [dbo].[Bank] bnk
		LEFT JOIN [dbo].[CustomBankProperty] cbp ON cbp.[bankId] = bnk.[id]
		LEFT JOIN [dbo].[TreeStructurePartCustomBankProperty] tsp ON tsp.[customBankPropertyId] = cbp.[customBankPropertyId]
		WHERE bnk.[id] = @BankId
		  AND tsp.[code] = @Code

		SELECT @BankId = bnk.[parentBankId]
		FROM [dbo].[Bank] bnk
		WHERE bnk.[id] = @BankId
	END

	SELECT @TreeStructurePartName
END