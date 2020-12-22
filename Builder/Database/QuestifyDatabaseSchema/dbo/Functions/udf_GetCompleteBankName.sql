CREATE FUNCTION udf_GetCompleteBankName(@bankid INT)
RETURNS VARCHAR(512)
AS
BEGIN
	DECLARE @bankname VARCHAR(512)
	SET @bankname = ''
	
	WHILE @bankid IS not NULL 
	BEGIN
		SELECT @bankname = [name] + '/' + @bankname FROM bank WHERE id = @bankid
		SELECT @bankid = [parentBankId] FROM bank WHERE id = @bankid
	END
	RETURN '/' + @bankname
END