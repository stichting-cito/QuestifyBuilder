/****** Object:  UserDefinedFunction [dbo].[GetParentBanks]    Script Date: 09/05/2019 13:50:19 ******/
/** function that will return the bankid and also any parent bank IDs **/
create function [dbo].[GetParentBanks] 
(@bankId int)
returns table
as return 
WITH motherBanks
AS ( SELECT b.Id, b.parentBankId
     FROM   Bank AS b
     WHERE  b.iD = @bankId
     UNION ALL
     SELECT p.Id, p.parentBankId
     FROM   Bank AS p 
	 inner join MotherBanks m
     on p.Id = m.parentBankId
     )
SELECT id
FROM   motherBanks

GO

