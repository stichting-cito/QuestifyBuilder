CREATE PROCEDURE [dbo].[ClearAndDeleteBankHierarchical]
	@bankId Integer,
	@isMain bit = 1
AS
	DECLARE @bid int;
	DECLARE tableCursor CURSOR LOCAL FOR
	SELECT id FROM [dbo].[Bank] WHERE parentBankId = @bankId

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;
	DECLARE @BankName NVARCHAR(4000);

	IF @isMain = 1
	BEGIN
		Print N'Clearing bank with ID: ' + CAST(@bankId AS VARCHAR)
	END
	ELSE
		PRINT N'Clearing SUBbank with ID: ' + CAST(@bankId AS VARCHAR)

	OPEN tableCursor
	FETCH NEXT FROM TableCursor INTO @bid;

	IF @@FETCH_STATUS <> 0
		PRINT N'No subbanks found!'
	ELSE
	BEGIN
		WHILE @@FETCH_STATUS = 0
		BEGIN
			PRINT N'Clearing Subbanks for bankId ' + CAST(@bid AS VARCHAR)
			EXEC ClearAndDeleteBankHierarchical @bankId = @bid, @isMain = 0

			PRINT N'Deleting bank with id: ' + CAST(@bid AS VARCHAR)
			BEGIN TRY
				EXEC ClearBank @bankId = @bid
			END TRY
			BEGIN CATCH
				SELECT @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();			
				SELECT @BankName = dbo.udf_GetCompleteBankName(@bid);
				SELECT @ErrorMessage = N'An error occurred trying to clear bank ' + @BankName + ': ' + ERROR_MESSAGE();

				RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
			END CATCH

			BEGIN TRY
				DELETE FROM [dbo].[Bank] WHERE id = @bid
			END TRY
			BEGIN CATCH
				SELECT @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();			
				SELECT @BankName = dbo.udf_GetCompleteBankName(@bid);
				SELECT @ErrorMessage = N'An error occurred trying to delete bank ' + @BankName + ': ' + ERROR_MESSAGE();

				RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
			END CATCH

			FETCH NEXT FROM tableCursor INTO @bid
		END
	END
	CLOSE TableCursor;
	DEALLOCATE TableCursor;

	IF @isMain = 1 
	BEGIN
		PRINT N'Deleting bank with id: ' + CAST(@bankId AS VARCHAR)
		BEGIN TRY
			EXEC ClearBank @bankId
		END TRY
		BEGIN CATCH
			SELECT @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();			
			SELECT @BankName = dbo.udf_GetCompleteBankName(@bankId);
			SELECT @ErrorMessage = N'An error occurred trying to clear bank ' + @BankName + ': ' + ERROR_MESSAGE();

			RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
		END CATCH

		BEGIN TRY
			DELETE FROM [dbo].[Bank] WHERE id = @bankId
		END TRY
		BEGIN CATCH
			SELECT @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();			
			SELECT @BankName = dbo.udf_GetCompleteBankName(@bid);
			SELECT @ErrorMessage = N'An error occurred trying to delete bank ' + @BankName + ': ' + ERROR_MESSAGE();

			RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
		END CATCH
	END
